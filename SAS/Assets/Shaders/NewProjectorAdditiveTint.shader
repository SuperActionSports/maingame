Shader "Projector/NewAdditiveTint" {
	Properties {
		_Color ("Tint Color", Color) = (1,1,1,1)
		_ShadowTex ("Cookie", 2D) = "gray" {}
		_FalloffTex ("FallOff", 2D) = "white" {}
	}
	Subshader {
		Tags { "Queue"="Transparent" }
		Pass {
			ZWrite Off
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha // Additive blending
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 uvFalloff : TEXCOORD1;
				float4 pos : SV_POSITION;
			};
			
			float4x4 _Projector;
			float4x4 _ProjectorClip;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, vertex);
				o.uvShadow = mul (_Projector, vertex);
				o.uvFalloff = mul (_ProjectorClip, vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			sampler2D _ShadowTex;
			sampler2D _FalloffTex;
			fixed4 _Color;
			
			fixed4 frag (v2f i) : SV_Target
			{
				// Apply alpha mask
				fixed4 texCookieS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
				fixed4 texCookieF = tex2Dproj (_FalloffTex, UNITY_PROJ_COORD(i.uvFalloff));
				fixed4 res = lerp(fixed4(1,1,1,0), texCookieS, texCookieF.a);
				fixed4 grayColor = (_Color.r+_Color.b+_Color.g)/3;
				fixed4 outColor = grayColor * res.a;
				return outColor;
			}
			ENDCG
		}
			Pass {
			ZWrite Off
			ColorMask RGB
			Blend SrcAlpha One // Additive blending
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 uvFalloff : TEXCOORD1;
				float4 pos : SV_POSITION;
			};
			
			float4x4 _Projector;
			float4x4 _ProjectorClip;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, vertex);
				o.uvShadow = mul (_Projector, vertex);
				o.uvFalloff = mul (_ProjectorClip, vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			sampler2D _ShadowTex;
			sampler2D _FalloffTex;
			fixed4 _Color;
			
			fixed4 frag (v2f i) : SV_Target
			{
				// Apply alpha mask
				fixed4 texCookieS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
				fixed4 texCookieF = tex2Dproj (_FalloffTex, UNITY_PROJ_COORD(i.uvFalloff));
				fixed4 res = lerp(fixed4(1,1,1,0), texCookieS, texCookieF.a);
				fixed4 grayColor = (_Color.r+_Color.b+_Color.g)/3;
				fixed4 outColor = grayColor * res.a;
				return outColor;
			}
			ENDCG
		}
	
		Pass {
			ZWrite Off
			ColorMask RGB
			Blend SrcAlpha One // Additive blending
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 uvFalloff : TEXCOORD1;
				float4 pos : SV_POSITION;
			};
			
			float4x4 _Projector;
			float4x4 _ProjectorClip;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, vertex);
				o.uvShadow = mul (_Projector, vertex);
				o.uvFalloff = mul (_ProjectorClip, vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			sampler2D _ShadowTex;
			sampler2D _FalloffTex;
			fixed4 _Color;
			
			fixed4 frag (v2f i) : SV_Target
			{
				// Apply alpha mask
				fixed4 texCookieS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
				fixed4 texCookieF = tex2Dproj (_FalloffTex, UNITY_PROJ_COORD(i.uvFalloff));
				fixed4 res = lerp(fixed4(1,1,1,0), texCookieS, texCookieF.a);
				fixed4 outColor = _Color * res.a;
				return outColor;
			}
			ENDCG
		}
	}
}
