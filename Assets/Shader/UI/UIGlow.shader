Shader "Custom/UIGlow"
{
    Properties
    {
        [PerRendererData]_MainTex ("Texture", 2D) = "white" {}
        [Space(20)]
        [Header(Glow1)]
        [Toggle(_CUSTOM_EFFECT_GLOW1_ON)] _Glow1_On("Enable Glow1", Float) = 1
        _VoronoiAngleOffset ("VoronoiAngleOffset", Float) = 2
        _VoronoiCellDensity ("VoronoiCellDensity", Float) = 3
        _Glow1Intensity ("Intensity", Float) = 1
        _Glow1Opacity ("Opacity", Range(0.0, 1.0)) = 0.3
        _Glow1ScrollSpeed ("Scroll Speed", Float) = 0.5
        
        [Space(20)]
        [Header(Glow2)]
        [Toggle(_CUSTOM_EFFECT_GLOW2_ON)] _Glow2_On("Enable Glow2", Float) = 1
        _Glow2Intensity ("Intensity", Float) = 1
        _Glow2Opacity ("Opacity", Range(0.0, 1.0)) = 0.3
        _Glow2TillingOffset ("Tilling Offset", Vector) = (1, 1, 0, 0)
        _Glow2ScrollSpeed ("Scroll Speed", Vector) = (0.5, 0.5, 0.0, 0.0)
        
        // HideProp
        [HideInInspector]_StencilComp ("Stencil Comparison", Float) = 8.000000
        [HideInInspector]_Stencil ("Stencil ID", Float) = 0.000000
        [HideInInspector]_StencilOp ("Stencil Operation", Float) = 0.000000
        [HideInInspector]_StencilWriteMask ("Stencil Write Mask", Float) = 255.000000
        [HideInInspector]_StencilReadMask ("Stencil Read Mask", Float) = 255.000000
        [HideInInspector]_ColorMask ("Color Mask", Float) = 15
    }
    SubShader
    {
        Tags 
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }
        
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _ _CUSTOM_EFFECT_GLOW1_ON
            #pragma multi_compile _ _CUSTOM_EFFECT_GLOW2_ON

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "../Library/Random.hlsl"
            #include "../Library/Macro.hlsl"
            #include "../Library/Gradient.hlsl"
            #include "../Library/Blend.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                half4 vertColor : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                half4 vertColor : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)

            float4 _MainTex_ST;

            // Voronoi
            float _VoronoiAngleOffset;
            float _VoronoiCellDensity;;
            float _Glow1Intensity;
            float _Glow1Opacity;
            float _Glow1ScrollSpeed;

            // Glow2
            float _Glow2Intensity;
            float _Glow2Opacity;
            float4 _Glow2TillingOffset;
            float2 _Glow2ScrollSpeed;
            
            CBUFFER_END

            Varyings vert (Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                output.vertColor = input.vertColor;
                output.texcoord = TRANSFORM_TEX(input.texcoord, _MainTex);
                return output;
            }

            half4 frag (Varyings input) : SV_Target
            {
                float2 uv = input.texcoord;

                half4 texCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv) * input.vertColor;
                half4 col = texCol;
                
#if defined(_CUSTOM_EFFECT_GLOW1_ON)
                float voronoiValue = 0;
                float voronoiCell = 0;

                Voronoi(uv, _VoronoiAngleOffset, _VoronoiCellDensity, voronoiValue, voronoiCell);

                voronoiCell = remap(voronoiCell, 0, 1, -5, 10);
                voronoiCell *= _Time.y * _Glow1ScrollSpeed;
                voronoiCell = frac(voronoiCell);
                
                half4 voroGlow = half4(GradientColor01(voronoiCell), 0.0H) * _Glow1Intensity;
                BlendMultiply(col, voroGlow, _Glow1Opacity, col);
#endif

                float2 glow2UV = uv * _Glow2TillingOffset.xy + _Glow2TillingOffset.zw;
                glow2UV += _Time.y * _Glow2ScrollSpeed.xy;
                half3 glow2 = GradientColor01(glow2UV.x + glow2UV.y) * _Glow2Intensity;
                BlendMultiply(col, half4(glow2, 0.0H), _Glow2Opacity, col);
                
                return half4(col.rgb, texCol.a);
            }
            ENDHLSL
        }
    }
}
