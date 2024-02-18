Shader "Custom/UIOutline"
{
    Properties
    {
        [PerRendererData]_MainTex ("Texture", 2D) = "white" {}
        [Space(20)]
        [Header(Outline)]
        [KeywordEnum(Blend, Overwrite)] _Outline_Type ("Outline Type", Float) = 0
        [HDR]_OutlineColor ("Outline Color", Color) = (1, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0.0, 1.0)) = 0.1
        
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
        
        LOD 100
        Pass

        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest [unity_GUIZTestMode]
            ZWrite Off
            Cull Off
            ColorMask [_ColorMask]
            
            Stencil
            {
                Ref [_Stencil]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
                Comp [_StencilComp]
                Pass [_StencilOp]
            }
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // CustomKeyword
            #pragma multi_compile _ _OUTLINE_ENABLE
            #pragma multi_compile _OUTLINE_TYPE_BLEND _OUTLINE_TYPE_OVERWRITE

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "../Library/Macro.hlsl"

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

            // Outline
            half4 _OutlineColor;
            float _OutlineWidth;
            
            CBUFFER_END

            Varyings vert (Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                output.texcoord = TRANSFORM_TEX(input.texcoord, _MainTex);
                output.vertColor = input.vertColor;
                return output;
            }

            half4 frag (Varyings input) : SV_Target
            {
                float2 uv = input.texcoord;

                _OutlineWidth *= 0.05F;
                
                half alphaUp = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(uv.x, uv.y + _OutlineWidth)).a;
                half alphaRight = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(uv.x + _OutlineWidth, uv.y)).a;
                half alphaDown = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(uv.x, uv.y - _OutlineWidth)).a;
                half alphaLeft = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(uv.x - _OutlineWidth, uv.y)).a;

                half4 outlineCol = half4(_OutlineColor.rgb, max4(alphaUp, alphaRight, alphaDown, alphaLeft));
                
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.texcoord) * input.vertColor;

#if defined(_OUTLINE_TYPE_BLEND)
                col.rgb *= col.a;
                outlineCol *= 1.0H - col.a;
                col += outlineCol;
#elif defined(_OUTLINE_TYPE_OVERWRITE)
                if (col.a <= 0.0001H)
                {
                    col = outlineCol;
                }
#endif
                
                return col;
            }
            ENDHLSL
        }
    }
}
