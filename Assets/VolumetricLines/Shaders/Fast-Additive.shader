Shader "VolumetricLine/Fast-Additive" {
	Properties {
		[NoScaleOffset] _MainTex ("Base (RGB)", 2D) = "white" {}
		_LineWidth ("Line Width", Range(0.01, 100)) = 1.0
		_LineScale ("Line Scale", Float) = 1.0
        _Color ("Main Color", Color) = (1,1,1,1)
	}
	SubShader {
		// batching is forcefully disabled here because the shader simply won't work with it:
		Tags {
			"DisableBatching"="True"
			"RenderType"="Transparent"
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"ForceNoShadowCasting"="True"
			"PreviewType"="Plane"
		}
		LOD 200
		
		Pass {
			
			Cull Off
			ZWrite Off
			ZTest LEqual
			Blend One One
			Lighting On
			
			CGPROGRAM
				#pragma glsl_no_auto_normalization
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile __ FOV_SCALING_OFF
				
				// tell the cginc file that this is a simplified version of the shader:
				#define VOL_LINE_SHDMODE_FAST
				
				#include "_SimpleShader.cginc"
			ENDCG
		}
	}
	FallBack "Diffuse"
}