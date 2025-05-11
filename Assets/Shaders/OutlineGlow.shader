Shader "Hidden/OutlineGlow"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Name "Outline"

            // Render setup
            Cull Front
            ZWrite Off
            ZTest Less
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            float _Thickness = 0.03;
            float4 _OutlineColor = float4(1, 1, 0, 1);

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                float3 normalWS = TransformObjectToWorldNormal(IN.normalOS);
                float3 positionWS = TransformObjectToWorld(IN.positionOS.xyz);

                // Expand vertex along normal by _Thickness
                positionWS += normalWS * _Thickness;

                OUT.positionHCS = TransformWorldToHClip(positionWS);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                return _OutlineColor;
            }

            ENDHLSL
        }
    }
}
