Shader "Hidden/FlatKit/OutlineFilter" {
	Properties {
		[HideInInspector] _BaseMap ("Base (RGB)", 2D) = "white" {}
		_EdgeColor ("Outline Color", Vector) = (1,1,1,1)
		_Thickness ("Thickness", Range(0, 5)) = 1
		[Space(15)] [Toggle(OUTLINE_USE_DEPTH)] _UseDepth ("Use Depth", Float) = 1
		_DepthThresholdMin ("Min Depth Threshold", Range(0, 1)) = 0
		_DepthThresholdMax ("Max Depth Threshold", Range(0, 1)) = 0.25
		[Space(15)] [Toggle(OUTLINE_USE_NORMALS)] _UseNormals ("Use Normals", Float) = 0
		_NormalThresholdMin ("Min Normal Threshold", Range(0, 1)) = 0.5
		_NormalThresholdMax ("Max Normal Threshold", Range(0, 1)) = 1
		[Space(15)] [Toggle(OUTLINE_USE_COLOR)] _UseColor ("Use Color", Float) = 0
		_ColorThresholdMin ("Min Color Threshold", Range(0, 1)) = 0
		_ColorThresholdMax ("Max Color Threshold", Range(0, 1)) = 0.25
		[Space(15)] [Toggle(OUTLINE_ONLY)] _OutlineOnly ("Outline Only", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
}