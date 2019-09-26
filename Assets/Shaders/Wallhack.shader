Shader "Custom/Wallhack"
 {
  Properties
     { 
         _Color ("Main Color", Color) = (0,191,255,1)
         _MainTex ("Texture", 2D) = "white" {}
     }

     Category
     {
         SubShader
         {
         Tags
         {
            "DisableBatching"="True"
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "ForceNoShadowCasting"="True"
            "PreviewType"="Plane"
         }
         
             Pass
             {
                 ZWrite Off
                 ZTest Greater
                 Color (0,0.5,1,1)
             }
      
             Pass
             {
                 //Blend SrcAlpha OneMinusSrcAlpha
                 ZTest Less
                 SetTexture [_MainTex] {combine texture}
             }
     
         }
     }
      
     FallBack "Diffuse"
}