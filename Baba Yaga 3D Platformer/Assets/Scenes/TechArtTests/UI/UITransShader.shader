﻿Shader "Custom/UITransShader"
{
    Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_ScrollXSpeed("X Scroll Speed", Range(0,10)) = 2
		_ScrollYSpeed("Y Scroll Speed", Range(0,10)) = 2

	}
    SubShader
    {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:blend

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };


        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed2 scrolledUV = IN.uv_MainTex;

			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;

			scrolledUV += fixed2(xScrollValue, yScrollValue);

			fixed4 c = tex2D(_MainTex, scrolledUV) * _Color;
            // Albedo comes from a texture tinted by color

            o.Albedo = c.rgb;

            o.Alpha = c.r;
        }
        ENDCG
    }
    FallBack "Diffuse"
}