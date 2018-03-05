﻿Shader "SergiShaders/ScreenEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_SecondaryTex("Secondary Texture", 2D) = "white" {}
	_OffsetNoiseX("Offset Noise X", float) = 0.0
		_OffsetNoiseY("Offset Noise Y", float) = 0.0
		_OffsetPosY("Offset position Y", float) = 0.0
		_OffsetColor("Offset Color", Range(0, 0.1)) = 0
		_OffsetDistortion("Offset Distortion", float) = 200
		_Intensity("Mask Intensity", Range(0.0, 1)) = 1.0
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader
	{
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct v2f
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float2 uv2 : TEXCOORD1;
	};

	half _OffsetNoiseX;
	half _OffsetNoiseY;

	v2f vert(appdata_base v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		o.uv2 = v.texcoord + float2(_OffsetNoiseX - 0.2f, _OffsetNoiseY);
		return o;
	}

	sampler2D _MainTex;
	sampler2D _SecondaryTex;

	fixed _Intensity;
	float _OffsetColor;
	half _OffsetPosY;
	half _OffsetDistortion;
	float4 _Color;

	fixed4 frag(v2f i) : SV_Target
	{
		i.uv = float2(frac(i.uv.x + cos((i.uv.y + _CosTime.y) * 100) / _OffsetDistortion), frac(i.uv.y + _OffsetPosY));

	fixed4 col = tex2D(_MainTex, i.uv);
	col.g = tex2D(_MainTex, i.uv + float2(_OffsetColor, _OffsetColor)).g;
	col.b = tex2D(_MainTex, i.uv + float2(-_OffsetColor, -_OffsetColor)).b;
	return col * _Color;
	}
		ENDCG
	}
	}
}