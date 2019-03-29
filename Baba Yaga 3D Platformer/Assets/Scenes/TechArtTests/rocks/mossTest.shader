// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/mossTest"
{
Properties {
      _MainTex ("Texture", 2D) = "white" {}
	  _MossTex ("Moss texture", 2D) = "gray" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
	  _MossBump ("MossBumpmap", 2D) = "bump" {}
	  _Direction ("Direction", Vector) = (0, 1, 0)
      _Amount ("Amount", Range(0, 1)) = 1
	  _MossDepth("Moss Depth", Range(0,0.3)) = 0.1
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      
	  CGPROGRAM
	  #pragma surface surf Lambert vertex:vert

	  sampler2D _MainTex;
	  sampler2D _MossTex;
	  sampler2D _BumpMap;
	  sampler2D _MossBump;
	  float3 _Direction;
      fixed _Amount;
	  float _MossDepth;

      struct Input {
        float2 uv_MainTex;
		float2 uv_MossTex;
        float2 uv_BumpMap;
		float2 uv_MossBump;
		
		float3 worldNormal;
		INTERNAL_DATA
      };

	  void vert (inout appdata_full v){
	  float3 snormal = normalize(_Direction.xyz);
	  float3 sn = mul((float3x3)unity_WorldToObject, snormal).xyz;

	   if(dot(v.normal, sn) >= lerp(1,-1, _Amount))
            {
               v.vertex.xyz += normalize(sn + v.normal) * _MossDepth * _Amount;
            }
	  }

      void surf (Input IN, inout SurfaceOutput o) {
        half4 c = tex2D (_MainTex, IN.uv_MainTex);
		half4 cM = tex2D (_MossTex, IN.uv_MossTex);
		half3 n = tex2D (_BumpMap, IN.uv_BumpMap);
		half3 nm = tex2D (_MossBump, IN.uv_MossBump);

		if(dot(WorldNormalVector(IN, o.Normal), _Direction.xyz)>=lerp(1,-1,_Amount)){
			o.Albedo = cM.rgb;
			o.Normal = nm.rbg;
		}
		else{
			o.Albedo = c.rgb;
			o.Normal = n.rbg;
		}
		o.Alpha = 1;
      }
	  
      ENDCG
     }
	
    Fallback "Diffuse"
  }