Shader "Custom/Training05"
{

    Properties
    {
        _MainTex("RGB 01", 2D) = "white" {}
        _MainTex02("RGB 02", 2D) = "white" {}
        
        _MaskTex("Mask Tex",2D) = "white"{}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 color : COLOR;
            };

            struct VertexOutput
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float3 color : COLOR;
            };

            float4 _MainTex_ST, _MainTex02_ST;
            Texture2D _MainTex, _MainTex02, _MaskTex;
            SamplerState sampler_MainTex;
            

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uv2 = v.uv.xy * _MainTex02_ST.xy + _MainTex02_ST.zw;
                o.color = TransformObjectToWorld(v.vertex.xyz);
                return o;
            }

            half4 frag(VertexOutput i) : SV_Target
            {
                float4 tex01 = _MainTex.Sample(sampler_MainTex, i.uv);
                float4 tex02 = _MainTex02.Sample(sampler_MainTex, i.uv2);

                float4 mask = _MaskTex.Sample(sampler_MainTex, i.uv);
                float4 color = lerp(tex01, tex02, i.color.r);
                return color;
            }
            ENDHLSL
        }
    }
}