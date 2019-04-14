Shader "Custom/ghostplatform02"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MaskTex("Mask Texture (RBGA)", 2D) = "white" {}

		_ScrollXSpeed("X Scroll Speed", Range(-20,20)) = 2
		_ScrollYSpeed("Y Scroll Speed", Range(-20,20)) = 2
			_AlphaLevel("Alpha Level", Range(-1,1)) = 0.2
    }
    SubShader
    {
	   Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        LOD 200
		Cull Off
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:blend

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
	sampler2D _MaskTex;


	fixed _ScrollXSpeed;
	fixed _ScrollYSpeed;
	fixed _AlphaLevel;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_MaskTex;
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
			fixed2 scrolledUV = IN.uv_MaskTex;

			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;

			scrolledUV += fixed2(xScrollValue, yScrollValue);

			fixed4 mask = tex2D(_MaskTex, scrolledUV);

            // Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;

			fixed alpha = _AlphaLevel + (mask.r - 0.5);
			o.Emission = mask.b * _Color;

            o.Alpha = alpha;
			clip(mask.r - (_AlphaLevel - 1));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
