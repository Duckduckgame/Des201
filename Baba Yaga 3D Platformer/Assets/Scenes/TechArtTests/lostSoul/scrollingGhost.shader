Shader "Custom/scrollingGhost"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Alpha (A)", 2D) = "white" {}
		_ColTex ("Color (RBG)", 2D) = "White" {}
		_ScrollXSpeed ("X Scroll Speed", Range(0,10)) = 2
		_ScrollYSpeed ("Y Scroll Speed", Range(0,10)) = 2

    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType"="Transparent" }
        LOD 200
		ZWrite Off

		Cull Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:blend

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;
        sampler2D _MainTex;
		sampler2D _ColTex;

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
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, scrolledUV) * _Color;
			fixed4 c2 = tex2D(_ColTex, scrolledUV * _Color);
            o.Albedo = c2.rgb;
            // Metallic and smoothness come from slider variables

            o.Alpha = c.a - 0.2;
			clip(c.a - 0.6);
			o.Emission = c.rbga - 0.5;
			
        }
        ENDCG
    }
    FallBack "Diffuse"
}
