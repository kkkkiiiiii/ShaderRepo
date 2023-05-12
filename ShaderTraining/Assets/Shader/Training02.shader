Shader "2ndshader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainTex02 ("Texture02", 2D) = "white" {}
        _TintColor("Color",color)=(1,1,1,1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="AlphaTest"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata // vertex buffer에서 읽어온 것
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f // 보간기
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            half4 _TintColor;
            float4 _MainTex_ST;
            Texture2D _MainTex;
            Texture2D _MainTex02;

            SamplerState sampler_MainTex;

            v2f vert(appdata v) // vertex shader
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target // pixel shader
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                half4 col01 = _MainTex.Sample(sampler_MainTex, i.uv);
                //half4 col02 = _MainTex02.Sample(sampler_MainTex,i.uv);

                half4 color = col01;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return color;
            }
            ENDCG
        }
    }
}