Shader "Custom/scrollingGhost"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Alpha (A)", 2D) = "white" {}
		_ColTex ("Color (RBG)", 2D) = "White" {}
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
		sampler2D _ColTex;

		float _DotProduct;
		float _SwayAmout;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldNormal;
			float3 viewDir;
        };


        fixed4 _Color;

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

            fixed4 c = tex2D (_MainTex, scrolledUV) * _Color;

			fixed4 c2 = tex2D(_ColTex, scrolledUV) * _Color;
            
			
			

			float border = 1- (abs(dot(IN.viewDir, IN.worldNormal)));
			float alpha = (border * (1 - _DotProduct) + _DotProduct);

			o.Albedo = _Color * (alpha * 3);

            o.Alpha = 1;
			clip((c.a - 0.6) * (-1 * alpha));
			o.Emission = _Color;
			
        }
        ENDCG
    }
    FallBack "Diffuse"
}
