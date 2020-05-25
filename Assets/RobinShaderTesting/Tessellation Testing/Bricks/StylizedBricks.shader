// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "StylizedBricks"
{
	Properties
	{
		_Albedo("Albedo ", 2D) = "white" {}
		_Normal("Normal ", 2D) = "white" {}
		_Height("Height", 2D) = "white" {}
		_Tessellation("Tessellation ", Range( 0 , 50)) = 0
		_VertexPosition("Vertex Position ", Range( -1 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
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

		uniform sampler2D _Height;
		uniform float4 _Height_ST;
		uniform float _VertexPosition;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _Smoothness;
		uniform float _Tessellation;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			float4 temp_cast_2 = (_Tessellation).xxxx;
			return temp_cast_2;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 uv_Height = v.texcoord * _Height_ST.xy + _Height_ST.zw;
			float3 ase_vertexNormal = v.normal.xyz;
			float2 uv_Normal = v.texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float4 tex2DNode7 = tex2Dlod( _Normal, float4( uv_Normal, 0, 0.0) );
			v.vertex.xyz += ( tex2Dlod( _Height, float4( uv_Height, 0, 0.0) ) * float4( ase_vertexNormal , 0.0 ) * _VertexPosition * tex2DNode7 ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float4 tex2DNode7 = tex2D( _Normal, uv_Normal );
			o.Normal = tex2DNode7.rgb;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = tex2D( _Albedo, uv_Albedo ).rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
379;73;1150;598;2143.621;495.8021;2.428786;True;True
Node;AmplifyShaderEditor.CommentaryNode;9;-1387.497,33.99387;Inherit;False;908.0943;619.7551;This controls the mesh deformation with the Vertex Position slider;4;2;3;1;4;Vertex Offset;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;7;-1030.454,-249.7231;Inherit;True;Property;_Normal;Normal ;1;0;Create;True;0;0;False;0;-1;bb06cb1f7268e9145a69c9052482e6be;1b8276c553759074680071dc04170d5e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;2;-980.2302,364.8931;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-1026.696,537.749;Inherit;False;Property;_VertexPosition;Vertex Position ;4;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1337.497,83.99387;Inherit;True;Property;_Height;Height;2;0;Create;True;0;0;False;0;-1;f2438b907a18759468765934a7a63389;643ea4b3cb04353498173809dc04aa2d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-575.7755,-351.0116;Inherit;True;Property;_Albedo;Albedo ;0;0;Create;True;0;0;False;0;-1;57a30ebf0473c6647bd3b0f725dd307a;f277a671923647c4ca260b4c31bd3db5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-399.4088,128.0622;Inherit;False;Property;_Smoothness;Smoothness;5;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-641.4031,372.0002;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-408.4996,483.9797;Inherit;False;Property;_Tessellation;Tessellation ;3;0;Create;True;0;0;False;0;0;0;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;StylizedBricks;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;4;2;3;0
WireConnection;4;3;7;0
WireConnection;0;0;6;0
WireConnection;0;1;7;0
WireConnection;0;4;8;0
WireConnection;0;11;4;0
WireConnection;0;14;5;0
ASEEND*/
//CHKSM=6D8518A014B14ACDFE629134515E939491708212