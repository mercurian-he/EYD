Shader"Custom/Toon"
{
	Properties{
		_MainTex("Texture",2D) = "white"{}
		_Bump("Bump",2D) = "bump"{}
		_Color("Color",Color) = (1.0,1.0,1.0,1.0)
		
		_Tooniness("Tooniness",Range(0.1,20)) = 4	//limit bump
		_ColorMerge("Color Merge", Range(0.1,20000)) = 8	//limit color
		_Ramp("Ramp",2D) = "white"{}
		
		_Outline("Outline",Range(0,1))=0.4
	}
	SubShader{
		Tags{"RenderType"="Opaque"}
		LOD 200
		
		CGPROGRAM
		////we want to write a function called final
		//////#pragma surface surf Lambert finalcolor:final
		#pragma exclude_renderers xbox360
		#pragma surface surf Toon
		
		sampler2D _MainTex;
		sampler2D _Bump;
		sampler2D _Ramp;
		float4 _Color;
		float _Tooniness;
		float _ColorMerge;
		float _Outline;
		
		
		struct Input{
			float2 uv_MainTex;
			float2 uv_Bump;
			float3 viewDir;
		};
		
		void surf (Input IN, inout SurfaceOutput o){
			half4 c = tex2D (_MainTex,IN.uv_MainTex);
			c.rgb *= _Color.rgb;
			o.Normal = UnpackNormal(tex2D(_Bump,IN.uv_Bump));
			half edge = saturate(dot(o.Normal,normalize(IN.viewDir)));
			edge = edge < _Outline? edge/4 : 1;	// dive 4 just to make it dark
			o.Albedo = (floor(c.rgb * _ColorMerge) / _ColorMerge) * edge;
			o.Alpha = c.a;
		}
		
		//return the color of the pixel
		//I don't know why but this should be put behind surf
		half4 LightingToon(SurfaceOutput s, half3 lightDir, half atten)
		{
			half4 c;
												//both Normal and lightDir have be normalized before
			half NdotL = dot(s.Normal,lightDir);//N dot L determine the lightness
		//	NdotL = NdotL * 0.5 + 0.5;			//still a linear trans
			NdotL = floor(NdotL * _Tooniness)/_Tooniness;		
			NdotL = saturate(tex2D(_Ramp,float2(NdotL,0.5)));	//the 0.5 can be any value
																//Ramp:left dark, right light
			
			
			c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten * 2;	
												//LightColor0 : the light
												//???why mult 2
			c.a = s.Alpha;
			return c;
		}
		
		//void final(Input IN, SurfaceOutput o, inout fixed4 color)
		//{
		//	color = floor(color * _Tooniness)/_Tooniness;		
		//}
		ENDCG
	}
	FallBack "Diffuse"
}