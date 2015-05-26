Shader "Custom/Illustrative2" {
	Properties {
		 _MainTex("Main Texture",2D) = "white"{}
		 _BumpMap("Bump Map",2D) = "normal"{}
		 _Color("Color",Color) = (1.0,1.0,1.0,1.0)
		 
		 //Gooch: warm color for light, cold color for shadow
		 _Ramp("Ramp",2D) = "white"{}
		 
		 //Better control over Lambert 
		 _DiffuseScale("Diffuse Scale",Float) = 1
		 _DiffuseBias("Diffuse Bias",Float) = 0
		 _DiffuseExponent("Diffuse Exponent",Float) = 1
		  
		  //Rim Lighting
		  _RimColor("Rim Color",Color) = (0.26,0.19,0.16,0.0)
		  _RimPower("Rim Power", Range(0.5,0.8)) = 3.0
		  
		  //Specular 
		  _SpecColor("Specular Color",Color) = (0.0,0.0,0.0,1.0)
		  _SpecPower("Specular Power",Range(0.5,128.0)) = 3.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma surface surf NPR

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		uniform sampler2D _MainTex;
		uniform sampler2D _BumpMap;
		uniform sampler2D _Ramp;
		uniform float4 _Color;
		uniform float _DiffuseScale, _DiffuseBias, _DiffuseExponent;
		uniform float4 _RimColor;
		uniform float _RimPower;
		//uniform float4 _SpecColor;	//why no need to declare Spec Color?
		uniform float _SpecPower;                          
		
		// Use Lambert's diffuse term as a position in the lightmap.
		half4 LightingNPR(SurfaceOutput o, half3 lightdir, half3 viewdir, half atten)
		{
			//put light equations and return a color.
			
			//LambertLighting
			//saturate:
				//return 1 if greater than 1, 0 less than 0, origin number otherwise.
			float lambert = saturate(dot(o.Normal,lightdir));
			//wrapped Lambertain diffuse term
			lambert = pow(lambert * _DiffuseScale + _DiffuseBias,_DiffuseExponent);
			
			half4 diff = half4(_LightColor0.rgb * o.Albedo.rgb * atten * lambert, 1.0);

			//tex2D generate a half4
			//the ramp picture is 1D thus no need of y coodinate.			
			diff *= tex2D(_Ramp,float2(lambert,0.0));	//how this color combine? by *?
			
			
			//Rim Lighting
			float rim_term = 1.0 - saturate(dot(normalize(viewdir), o.Normal));
			rim_term = pow(rim_term,_RimPower);	//strengthen it a little
			half4 rim = half4(_RimColor.rgb * rim_term,1.0);
			
			//Phong's specular term
			half3 r = reflect (-lightdir,o.Normal);
			float phong = pow(saturate(dot(r,viewdir)),_SpecPower);
			half4 specular = half4(phong * _SpecColor * atten);
			
			return diff+rim+specular;	//why add here?
			}

		struct Input {
			//Input to the surface function
			//Valid fields are listed at
			//http://docs.unity3d.com/Documentation/Components/SL-SurfaceShaders.html 
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) 
		{
			// prepare the surface for lighting by propagating a surface output structure
			half4 c = tex2D(_MainTex, IN.uv_MainTex);	//sample the texture
			o.Albedo = _Color.rgb * c;	//modulate by main color
			o.Alpha = 1.0;
			
			//Apply bump map
			o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_MainTex));
		}
		 
		
		
		ENDCG
	} 
	FallBack "Diffuse"
}
