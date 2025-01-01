using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UnityConfigs
{
    public class UnityConfigs: MonoBehaviour
    {
        [SerializeField] private SpawnConfig _spawnConfig;
        public SpawnConfig SpawnConfig => _spawnConfig;

    }

     public class SimpleWindow : MonoBehaviour
    {
        public enum State
        {
            None,
            Show,
            Shown,
            Hide,
            Hidden
        }
 
        public event Action Shown;
        public event Action Hidden;
 
        [SerializeField]
        private Transform windowBody;
        
        [SerializeField]
        private float showTime;
        
        [SerializeField]
        private float hideTime;
 
        private State currentState = State.None;
 
        private Coroutine stateWork;
 
        public void SetState(State newState)
        {
            if (currentState == newState)
            {
                return;
            }
 
            currentState = newState;
 
            switch (currentState)
            {
                case State.None:
                    gameObject.SetActive(false);
                    break;
                case State.Show:
                    if (stateWork != null)
                    {
                        StopCoroutine(stateWork);
                    }
 
                    stateWork = StartCoroutine(Show());
                    break;
                case State.Shown:
                    Shown?.Invoke();
                    break;
                case State.Hide:
                    if (stateWork != null)
                    {
                        StopCoroutine(stateWork);
                    }
 
                    stateWork = StartCoroutine(Hide());
                    break;
                case State.Hidden:
                    Hidden?.Invoke();
                    break;
            }
        }
 
        private IEnumerator Show()
        {
            yield return new WaitForSeconds(showTime);
            windowBody.gameObject.SetActive(true);
            SetState(State.Shown);
        }
 
        private IEnumerator Hide()
        {
            yield return new WaitForSeconds(hideTime);
            windowBody.gameObject.SetActive(false);
            SetState(State.Hidden);
        }
    }
 
    public static class Main
    {
        static void Start()
        {
            SimpleWindow window = new SimpleWindow();
 
            window.Shown += OnShown;
            window.Hidden += OnHidden;
 
            window.SetState(SimpleWindow.State.Show);
 
            void OnShown()
            {
                Debug.Log("Window is shown");
                window.SetState(SimpleWindow.State.Hide);
            }
            
            void OnHidden()
            {
                Debug.Log("Window is hidden");
            }
        }
    }

   

}