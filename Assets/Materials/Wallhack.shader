Shader "Custom/Wallhack"
 {
  Properties
     { 
         _Color ("Main Color", Color) = (0,0,1,1)
         _MainTex ("Texture", 2D) = "white" {}
     }

     Category
     {
         SubShader
         {
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