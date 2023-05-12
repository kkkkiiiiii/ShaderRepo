// Shader 시작. 셰이더의 폴더와 이름을 여기서 결정합니다.
Shader "URPTraining/URPBasic"
{
    Properties
    {
        // Properties Block : 셰이더에서 사용할 변수를 선언하고 이를 material inspector에 노출시킵니다
        _MyColor("Color",Color)=(1,1,1,1)
        _Intensity("Range test",Range(0,1)) = 0.5
        [Space(20)]
        _MainTex("RGB(A)", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            //Render type과 Render Queue를 여기서 결정합니다.
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
            //cg shader는 .cginc를 hlsl shader는 .hlsl을 include하게 됩니다.
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            //vertex buffer에서 읽어올 정보를 선언합니다.
            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            //보간기 버텍스 셰이더에서 계산한 것을 픽셀 셰이더로 전달할 정보를 선언합니다.
            struct VertexOutput
            {
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;
                // Texture를 Mesh에다가 Mapping하려면 UV 정보가 필요하다.
            };
            // 변수 타입을 쉐이더 내 지정
                float _Intensity;
                half4 _MyColor;
                sampler2D _MainTex;
                float4 _MainTex_ST;
            //버텍스 셰이더에서 계산하기
            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }
            
            //픽셀 셰이더 frag에다가 VertexOutput 보간기를 통해 읽어낸 값을 i를 붙여 읽는다.
            half4 frag(VertexOutput i) : SV_Target
            {
                float2 uv = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw; // Texture의 스케일값 offset값을 지정
                float4 color = tex2D(_MainTex,uv) * _MyColor * _Intensity; // 채널 4개짜리로 리턴해야 에러나지 않음.
                return color;
            }
            ENDHLSL
        }
    }
}