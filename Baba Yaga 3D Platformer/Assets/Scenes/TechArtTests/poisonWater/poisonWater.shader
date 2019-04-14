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

		_PulseAmount("Pulse amount", Range(0,0.5)) = 0.1
		_PulsePeriod("Pulse period", Float) = 1

    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType"="Transparent" }
        LOD 200
		ZWrite Off

		//Cull Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;
        sampler2D _MainTex;
		

		float _DotProduct;
		float _SwayAmout;

		float _PulseAmount;
		float _PulsePeriod;

        struct Input
        {
            float2 uv_MainTex;			
			float3 worldNormal;
			float3 viewDir;
        };


        fixed4 _Color;
		fixed4 _SecColor;

		// Vertex Manipulation Function
			void vert(inout appdata_full v) {

				fixed4 c = tex2Dlod(_MainTex, float4(v.texcoord.xy, 0, 0));
				float pulse = c.b; // Uses the blue channel

				// Time and position component
				const float TAU = 6.28318530718;
				float time = (sin(_Time.y / _PulsePeriod * TAU) + 1.0) / 2.0; // [0,1]

				v.vertex.xyz += v.normal * pulse * time * _PulseAmount;
			}

			  

        void surf (Input IN, inout SurfaceOutput o)
        {
			fixed2 scrolledUV = IN.uv_MainTex;

			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;

			scrolledUV += fixed2(xScrollValue, yScrollValue);

			fixed4 map = tex2D(_MainTex, scrolledUV);
			fixed4 mapN = tex2D(_MainTex, IN.uv_MainTex);

            fixed4 c = _Color;

			c *= map.r;
			c += mapN.g;
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
