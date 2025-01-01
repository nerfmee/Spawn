using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UI
{
    [CustomEditor(typeof(WindowRegistry))]
    public class WindowRegistryEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update Registry"))
            {
                UpdateRegistry();
            }
        }

        private void UpdateRegistry()
        {
            var windowRegistry = (WindowRegistry)target;

            string[] guids = AssetDatabase.FindAssets("t:GameObject", new[] { "Assets/Game/Resources/Dialogs" });
            var windowPrefabs = new List<GameObject>();

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (prefab != null)
                {
                    windowPrefabs.Add(prefab);
                }
            }

            windowRegistry.UpdateWindowPrefabs(windowPrefabs);

            EditorUtility.SetDirty(windowRegistry);

            Debug.Log("WindowRegistry updated successfully.");
        }
    }
}