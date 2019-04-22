Shader "Custom/LeafTubes"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_GradientTex("Gradient Map", 2D) = "White" {}
		_MaskTex("Mask Map", 2D) = "White" {}


		_ScrollXSpeed("X Scroll Speed", Range(-20,20)) = 2
		_ScrollYSpeed("Y Scroll Speed", Range(-20,20)) = 2
    }
    SubShader
    {
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Tags { "ForceNoShadowCasting" = "True"}
        LOD 200
		ZWrite Off

		

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;
        sampler2D _MainTex;
		sampler2D _GradientTex;
		sampler2D _MaskTex;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_GradientTex;
			float2 uv_MaskTex;
        };

        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
			fixed2 scrolledUV = IN.uv_MainTex;

			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;

			scrolledUV += fixed2(xScrollValue, yScrollValue);

			fixed4 map = tex2D(_MainTex, scrolledUV);
			fixed4 mapN = tex2D(_MainTex, IN.uv_MainTex);

            o.Albedo = map.rgb;
            
			fixed4 gMap = tex2D(_GradientTex, IN.uv_GradientTex);
			fixed4 mask = tex2D(_MaskTex, IN.uv_MaskTex);

			fixed alpha = gMap.r - mask.b - 0.2;
			o.Alpha = gMap.r -0.3;
			
			clip(map.a - 0.1);
			clip(gMap.r - 0.1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
