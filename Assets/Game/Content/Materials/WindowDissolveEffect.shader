Shader "Custom/WindowDissolveEffect"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _EdgeMask ("Edge Mask Texture", 2D) = "white" {} // Маска для рваных краёв
        _DissolveProgress ("Dissolve Progress", Range(0, 1)) = 0
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _DissolveSoftness ("Dissolve Softness", Range(1, 50)) = 10.0
        _NoiseScale ("Noise Scale", Range(0.1, 5.0)) = 1.0 // Масштаб текстуры шума
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Name "WindowRoughEdgesPass"
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            sampler2D _EdgeMask; 
            float4 _MainTex_ST;
            float4 _MainColor;
            float _DissolveProgress;
            float _DissolveSoftness; 
            float _NoiseScale; 

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Main texture
                fixed4 mainColor = tex2D(_MainTex, i.uv);

                // Noise texture for dissolve
                float2 noiseUV = i.uv * _NoiseScale;
                fixed4 noise = tex2D(_NoiseTex, noiseUV);

                // Edge mask for rough borders
                fixed4 edgeMask = tex2D(_EdgeMask, i.uv);

                // Invert _DissolveProgress for correct direction
                float invertedProgress = 1.0 - _DissolveProgress;

                // Combine edge mask and dissolve with softness
                float dissolveValue = saturate((noise.r - invertedProgress) * _DissolveSoftness);
                dissolveValue *= edgeMask.r;

                // Apply transparency
                mainColor.a *= dissolveValue;

                return mainColor * _MainColor;
            }
            ENDHLSL
        }
    }
    FallBack "Unlit/Transparent"
}
