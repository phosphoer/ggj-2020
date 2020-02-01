Shader "Custom/Opaque"
{
  Properties
  {
    _Color ("Color", Color) = (1,1,1,1)
    _MainTex ("Texture", 2D) = "white" {}
    _LightRamp ("Light Ramp", 2D) = "white" {}
    _FogScale ("Fog Scale", float) = 1
    _LightingScale ("Lighting Scale", float) = 0.1
    _ExtraLight ("Extra Light", float) = 0.1

    [Enum(Off,0,On,1)] 
    _ZWrite ("ZWrite", Float) = 1
    
    [Enum(UnityEngine.Rendering.CompareFunction)] 
    _ZTest ("ZTest", Float) = 4

    [Enum(UnityEngine.Rendering.CullMode)] 
    _CullMode ("Cull Mode", Float) = 2
  }
  SubShader
  {
    ZWrite [_ZWrite]
    ZTest [_ZTest]
    Cull [_CullMode]
    
    Pass
    {
      Tags { "RenderType"="Opaque" "LightMode" = "ForwardBase" }

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile_fog			
      #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight

      #include "UnityCG.cginc"
      #include "UnityLightingCommon.cginc"
      #include "Lighting.cginc"
      #include "AutoLight.cginc"

      struct appdata
      {
        float4 vertex : POSITION;
        fixed4 color : COLOR;
        fixed3 normal: NORMAL;
        float2 uv : TEXCOORD0;
      };

      struct v2f
      {
        float4 pos : SV_POSITION;
        float3 worldNormal : NORMAL;
        fixed4 color : COLOR;
        float2 uv : TEXCOORD0;
        UNITY_FOG_COORDS(1)
        SHADOW_COORDS(2)
      };

      sampler2D _MainTex;
      sampler2D _LightRamp;
      fixed4 _Color;
      fixed _FogScale;
      fixed _LightingScale;
      float _ExtraLight;

      v2f vert (appdata v)
      {
        v2f o;
        o.pos = UnityObjectToClipPos(v.vertex);
        o.worldNormal = UnityObjectToWorldNormal(v.normal);
        o.uv = v.uv;
        o.color = v.color;

        TRANSFER_SHADOW(o);
        UNITY_TRANSFER_FOG(o, o.pos);

        return o;
      }

      fixed4 frag (v2f i) : SV_Target
      {
        fixed4 color = _Color * tex2D(_MainTex, i.uv) * i.color;
        
        float lightDot = max(dot(normalize(i.worldNormal), normalize(_WorldSpaceLightPos0.xyz)), 0);
        lightDot *= SHADOW_ATTENUATION(i);
        lightDot += _ExtraLight;

        fixed3 lightRamp = tex2D(_LightRamp, float2(0.5, lightDot)).rgb;
        lightDot = lightRamp;
        color = lerp(color, lightDot * color * _LightColor0, _LightingScale);

        fixed4 fogColor = color;
        UNITY_APPLY_FOG(i.fogCoord, fogColor);
        return lerp(color, fogColor, _FogScale); 
      }
      ENDCG
    }
  }

  FallBack "VertexLit"
}
