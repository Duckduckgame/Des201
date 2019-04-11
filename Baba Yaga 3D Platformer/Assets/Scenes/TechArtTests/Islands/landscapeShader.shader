Shader "Custom/landscapeShader"
{
    Properties
    {
        _MainTex ("1st Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("2nd Albedo (RGB)", 2D) = "white" {}
		_GlossTex("1st Gloss (G)", 2D) = "white" {}
		_GlossTex2("2nd Gloss (G)", 2D) = "white" {}
		_NormalTex("1st Normal (RGB)", 2D) = "bump" {}
		_NormalTex2("2nd Normal (RGB)", 2D) = "bump" {}

		_Mask("Mask (G)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.5

        sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _GlossTex;
		sampler2D _GlossTex2;
		sampler2D _NormalTex;
		sampler2D _NormalTex2;
		sampler2D _Mask;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv_GlossTex;
			float2 uv_GlossTex2;
			float2 uv_NormalTex;
			float2 uv_NormalTex2;
			float2 uv_Mask;
        };

        
        

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float4 blendData = tex2D(_Mask, IN.uv_Mask);

			float4 c1 = tex2D(_MainTex, IN.uv_MainTex);
			float4 c2 = tex2D(_MainTex2, IN.uv_MainTex2);
			float4 g1 = tex2D(_GlossTex, IN.uv_GlossTex);
			float4 g2 = tex2D(_GlossTex2, IN.uv_GlossTex2);
			float3 n1 = UnpackNormal (tex2D(_NormalTex, IN.uv_NormalTex));
			float3 n2 = UnpackNormal (tex2D(_NormalTex2, IN.uv_NormalTex2));

			float4 finalColor;
			float3 finalNormal;
			float4 finalGloss;
			finalColor = lerp(c1, c2, blendData.r);
			finalNormal = lerp(n1, n2, blendData.r);
			finalGloss = lerp(g1, g2, blendData.r);

			finalColor = saturate(finalColor);

			o.Albedo = finalColor.rgb;
			o.Smoothness = finalGloss;
			o.Normal = finalNormal;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
