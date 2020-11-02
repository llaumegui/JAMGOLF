// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Jauge"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_JaugeTex("JaugeTex", 2D) = "white" {}
		_Float0("Float 0", Range( 0 , 1)) = 1
		[HDR]_Color0("Color 0", Color) = (1,0,0,0)
		[HDR]_Color1("Color 1", Color) = (1,0.5537271,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color0;
		uniform float4 _Color1;
		uniform float _Float0;
		uniform sampler2D _JaugeTex;
		SamplerState sampler_JaugeTex;
		uniform float4 _JaugeTex_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 lerpResult16 = lerp( _Color0 , _Color1 , ( 1.0 - _Float0 ));
			float temp_output_19_0 = ( 1.0 - _Float0 );
			float2 uv_JaugeTex = i.uv_texcoord * _JaugeTex_ST.xy + _JaugeTex_ST.zw;
			float4 tex2DNode1 = tex2D( _JaugeTex, uv_JaugeTex );
			float smoothstepResult15 = smoothstep( temp_output_19_0 , temp_output_19_0 , tex2DNode1.r);
			o.Emission = ( ( lerpResult16 * smoothstepResult15 ) + 0.2 ).rgb;
			o.Alpha = 1;
			clip( tex2DNode1.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18701
300;73;1064;482;640.3723;279.0485;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;6;-1233.663,132.1797;Inherit;False;Property;_Float0;Float 0;2;0;Create;True;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1172.671,-174.3603;Inherit;True;Property;_JaugeTex;JaugeTex;1;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;19;-891.39,46.15759;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;23;-452.5372,-423.1413;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-764.1663,-814.3898;Inherit;False;Property;_Color0;Color 0;3;1;[HDR];Create;True;0;0;False;0;False;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;17;-854.1886,-615.3799;Inherit;False;Property;_Color1;Color 1;4;1;[HDR];Create;True;0;0;False;0;False;1,0.5537271,0,0;1,0.5537271,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;15;-605.9432,-231.6102;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;-273.9751,-621.697;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-213.2711,-231.788;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;25;16.35767,0.5589905;Inherit;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;False;0;False;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;167.6484,-77.58023;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;353.9945,-100.5539;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/Jauge;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;TransparentCutout;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;6;0
WireConnection;23;0;6;0
WireConnection;15;0;1;1
WireConnection;15;1;19;0
WireConnection;15;2;19;0
WireConnection;16;0;10;0
WireConnection;16;1;17;0
WireConnection;16;2;23;0
WireConnection;22;0;16;0
WireConnection;22;1;15;0
WireConnection;24;0;22;0
WireConnection;24;1;25;0
WireConnection;0;2;24;0
WireConnection;0;10;1;1
ASEEND*/
//CHKSM=715FA4F4F2260DB20F40E186624F469D22932D2E