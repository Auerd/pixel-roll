Shader "Sprites/SpaceBetweenPixels/and"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Transparency ("Transparency", Integer) = 1
		_OffsetX ("Offset X", Integer) = 0
		_OffsetY ("Offset Y", Integer) = 0
	    _Width ("Width", Float) = .1
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;

				return OUT;
			}

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
            float _Width;
			int _OffsetX;
			int _OffsetY;
            int _Transparency;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D (_MainTex, IN.texcoord) * IN.color;
				float2 pixelCoord = ceil(_MainTex_TexelSize.zw * IN.texcoord / _Width) + float2(_OffsetX, _OffsetY);
				c.a *= (pixelCoord.y % _Transparency) == float2(0., 0.) && (pixelCoord.x % _Transparency) == float2(0., 0.);
				c.rgb *= c.a;
                
                return c;
			}
		ENDCG
		}
	}
}