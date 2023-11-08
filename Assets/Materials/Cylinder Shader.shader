Shader"Custom/CylinderShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Radius ("Cylinder Radius", Range(0.1, 10)) = 1.0
    }
 
    SubShader {
        Tags { "Queue"="Transparent" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float2 texcoord : TEXCOORD0;
};

struct v2f
{
    float2 texcoord : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

float _Radius;

v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);

                // Calculate the texture coordinates based on the vertex position
    float2 cylinderCoord;
    cylinderCoord.x = atan2(v.vertex.z, v.vertex.x) / (2* 3.14159265359) + 0.5f;
    cylinderCoord.y = (v.vertex.y + _Radius) / (2 * _Radius);

    o.texcoord = cylinderCoord;

    return o;
}

sampler2D _MainTex;
float4 _Color;

half4 frag(v2f i) : SV_Target
{
    half4 col = tex2D(_MainTex, i.texcoord) * _Color;
    return col;
}
            ENDCG
        }
    }
}
