Shader "Custom/scrollingGhost"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_SecColor("Secondary Color", Color) = (1,1,1,1)
        _MainTex ("Alpha (A)", 2D) = "white" {}		
		_ScrollXSpeed ("X Scroll Speed", Range(0,10)) = 2
		_ScrollYSpeed ("Y Scroll Speed", Range(0,10)) = 2

		_DotProduct("Rim effect", Range(-1,1)) = 0.25
		_SwayAmout("Sway Amount", Range(-1,1)) = 0

    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType"="Transparent" }
        LOD 200
		ZWrite Off

		//Cull Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert vertex:vert alpha:blend

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;
        sampler2D _MainTex;
		

		float _DotProduct;
		float _SwayAmout;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldNormal;
			float3 viewDir;
        };


        fixed4 _Color;
		fixed4 _SecColor;

		// Vertex Manipulation Function
			void vert(inout appdata_full i) {

				//Gets the vertex's World Position 
			   float3 worldPos = mul(unity_ObjectToWorld, i.vertex).xyz;
			   /*
			   //Tree Movement and Wiggle
			   i.vertex.x += _SinTime * _SwayAmout;
			   i.vertex.y += _SinTime * _SwayAmout;
			   i.vertex.x += _SinTime * _SwayAmout;*/

			   }

        void surf (Input IN, inout SurfaceOutput o)
        {
			fixed2 scrolledUV = IN.uv_MainTex;

			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;

			scrolledUV += fixed2(xScrollValue, yScrollValue);

			fixed4 map = tex2D(_MainTex, scrolledUV);

            fixed4 c = _Color;

			c *= map.r;
			c += map.g;
			c -= map.b;

			//c *= (map.a * _SecColor * 0.9);
            
			
			

			float border = 1- (abs(dot(IN.viewDir, IN.worldNormal)));
			float alpha = (border * (1 - _DotProduct) + _DotProduct);

			o.Albedo = c;

            o.Alpha = 1;
			
			o.Emission = _Color;
			
        }
        ENDCG
    }
    FallBack "Diffuse"
}
