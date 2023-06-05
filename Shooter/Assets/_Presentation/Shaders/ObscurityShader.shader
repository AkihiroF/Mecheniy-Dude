Shader "Custom/ObscurityShader"
{
    Properties
	{
		[PerRendererData]
		_MainTex ("Main Texture", 2D) = "white" {}
		_Color ("Color" , Color) = (1,1,1,1)
		_DisplacementPower ("Displacement Power" , Float) = 0
	}
    SubShader
    {
        Tags{"Queue" = "Transparent+1" "RenderType" = "Transparent"}
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };
            
            fixed4 _Color;
            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.color = v.color;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {

                fixed4 texColor = tex2D(_MainTex, i.uv)*i.color;                                
                return texColor;
                return 0;
            }
            ENDCG
        }
    }
}