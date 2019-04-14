// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/mossTest"
{
Properties {
	  _TintColor("Tint Color", Color) = (1.0,1.0,1.0,1.0)
      _MainTex ("Texture", 2D) = "white" {}
	  _MossTex ("Moss texture", 2D) = "gray" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
	  _MossBump ("MossBumpmap", 2D) = "bump" {}
	  _Direction ("Direction", Vector) = (0, 1, 0)
      _Amount ("Amount", Range(0, 1)) = 1
	  _MossDepth("Moss Depth", Range(0,1)) = 0.1
	  _RoughnessMap ("Roughness Map", 2D) = "white" {}
	  _Roughness ("Roughness", Range(0.01, 1)) = 0.01 
	  _MossCancel("Moss Cancel", Range(-1,1)) = 0

    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      
	  CGPROGRAM
	  #pragma surface surf Lambert addshadow 
	  #pragma target 3.0
	  #pragma glsl

	  sampler2D _MainTex;
	  sampler2D _MossTex;
	  sampler2D _BumpMap;
	  sampler2D _MossBump;
	  sampler2D _RoughnessMap;
	  float _Roughness;
	  float3 _Direction;
      fixed _Amount;
	  float _MossDepth;
	  float _MossCancel;
	  float4 _TintColor;

      struct Input {
        float2 uv_MainTex;
		float2 uv_MossTex;
        float2 uv_BumpMap;
		float2 uv_MossBump;
		float2 uv_Roughness;
		
		float3 worldNormal;
		float3 worldRefl;
		INTERNAL_DATA
      };
	  /*
	  void vert (inout appdata_full v){
	  float3 snormal = normalize(_Direction.xyz);
	  
	  float3 sn = mul((float3x3)unity_WorldToObject, snormal).xyz;

	   if(dot(v.normal, sn.xyz) >= lerp(1,-1, ((_Amount*2)/3)))
            {
               v.vertex.xyz += (sn + v.normal) * _MossDepth * _Amount;
            }
	  }*/

      void surf (Input IN, inout SurfaceOutput o) {
		float roughness = _Roughness * tex2D(_RoughnessMap, IN.uv_Roughness).r;
        half4 c = tex2D (_MainTex, IN.uv_MainTex);
		half4 cM = tex2D (_MossTex, IN.uv_MossTex);
		half3 n = tex2D (_BumpMap, IN.uv_BumpMap);
		half3 nm = tex2D (_MossBump, IN.uv_MossBump);

		float difference = dot(WorldNormalVector(IN, o.Normal), _Direction.xyz) - lerp(0, -1, _Amount);

		cM *= _TintColor;
			o.Albedo = difference * cM.rgb + (1 - difference) * c.rgb;

		/*if(dot(WorldNormalVector(IN, o.Normal), _Direction.xyz)>=lerp(1,-1,_Amount)){
			o.Albedo = cM.rgb;
			o.Normal = nm.rgb;
			
		}
		else{
			o.Albedo = c.rgb;
			o.Normal = n.rgb;
		}*/
		o.Alpha = 1;
		o.Gloss = roughness;
      }
	  
      ENDCG
     }
	
    Fallback "Diffuse"
  }