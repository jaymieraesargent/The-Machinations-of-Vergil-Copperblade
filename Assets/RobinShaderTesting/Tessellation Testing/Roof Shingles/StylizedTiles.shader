// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "StylizedTiles"
{
	Properties
	{
		_Albedo1("Albedo ", 2D) = "white" {}
		_Normal1("Normal ", 2D) = "white" {}
		_Height1("Height", 2D) = "white" {}
		_Tessellation1("Tessellation ", Range( 0 , 50)) = 0
		_VertexPosition1("Vertex Position ", Range( -1 , 1)) = 0
		_Smoothness1("Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Height1;
		uniform float4 _Height1_ST;
		uniform float _VertexPosition1;
		uniform sampler2D _Normal1;
		uniform float4 _Normal1_ST;
		uniform sampler2D _Albedo1;
		uniform float4 _Albedo1_ST;
		uniform float _Smoothness1;
		uniform float _Tessellation1;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			float4 temp_cast_2 = (_Tessellation1).xxxx;
			return temp_cast_2;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 uv_Height1 = v.texcoord * _Height1_ST.xy + _Height1_ST.zw;
			float3 ase_vertexNormal = v.normal.xyz;
			float2 uv_Normal1 = v.texcoord * _Normal1_ST.xy + _Normal1_ST.zw;
			float4 tex2DNode4 = tex2Dlod( _Normal1, float4( uv_Normal1, 0, 0.0) );
			v.vertex.xyz += ( tex2Dlod( _Height1, float4( uv_Height1, 0, 0.0) ) * float4( ase_vertexNormal , 0.0 ) * _VertexPosition1 * tex2DNode4 ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal1 = i.uv_texcoord * _Normal1_ST.xy + _Normal1_ST.zw;
			float4 tex2DNode4 = tex2D( _Normal1, uv_Normal1 );
			o.Normal = tex2DNode4.rgb;
			float2 uv_Albedo1 = i.uv_texcoord * _Albedo1_ST.xy + _Albedo1_ST.zw;
			o.Albedo = tex2D( _Albedo1, uv_Albedo1 ).rgb;
			o.Smoothness = _Smoothness1;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
379;73;1150;598;2206.803;761.8091;3.379703;True;True
Node;AmplifyShaderEditor.CommentaryNode;9;-1287.373,51.12811;Inherit;False;908.0944;619.7551;This controls the mesh deformation with the Vertex Position slider;4;1;2;3;5;Vertex Offset;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;4;-959.761,-204.6223;Inherit;True;Property;_Normal1;Normal ;1;0;Create;True;0;0;False;0;-1;4312b595602806b47a8969edb540a6de;1b8276c553759074680071dc04170d5e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;1;-880.1051,382.0273;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;2;-926.5713,554.8831;Inherit;False;Property;_VertexPosition1;Vertex Position ;4;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-1237.373,101.1281;Inherit;True;Property;_Height1;Height;2;0;Create;True;0;0;False;0;-1;5841905dc0be6dd44980373722a04514;643ea4b3cb04353498173809dc04aa2d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-381.2064,507.3806;Inherit;False;Property;_Tessellation1;Tessellation ;3;0;Create;True;0;0;False;0;0;0;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-328.7158,173.1631;Inherit;False;Property;_Smoothness1;Smoothness;5;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-531.8504,-326.5014;Inherit;True;Property;_Albedo1;Albedo ;0;0;Create;True;0;0;False;0;-1;8d729438ba327a94184ce379d5fac179;f277a671923647c4ca260b4c31bd3db5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-541.2781,389.1345;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;StylizedTiles;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;3;0
WireConnection;5;1;1;0
WireConnection;5;2;2;0
WireConnection;5;3;4;0
WireConnection;0;0;7;0
WireConnection;0;1;4;0
WireConnection;0;4;8;0
WireConnection;0;11;5;0
WireConnection;0;14;6;0
ASEEND*/
//CHKSM=7CC525C7EBDA3F679AF9CC194D3C560C7B59ECEF