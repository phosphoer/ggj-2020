Shader "Custom/UnlitVertColorAlpha"
{
    Properties
    {
        _MainTex ("Base(RGB) Alpha(A)", 2D) = "white" {}
        _VertColor ("Vert color intensity", Range(0,1)) = 1
        _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

    }
    SubShader
    {
        Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
        LOD 100

        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _VertColor;
            float _Cutoff;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 vertCol = saturate(i.color + (fixed4(1,1,1,1) * (1-_VertColor)));
                vertCol.a = 1;
                col *= vertCol;

                
                //Do alpha
                if(col.a - _Cutoff <= 0) discard;

                return col;
            }
            ENDCG
        }
    }
}
