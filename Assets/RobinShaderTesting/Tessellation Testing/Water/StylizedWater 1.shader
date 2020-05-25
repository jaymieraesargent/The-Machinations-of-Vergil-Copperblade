// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "StylizedWater"
{
	Properties
	{
		_VertexDirection("Vertex Direction ", Range( 0 , 1)) = 0
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 15
		_VoronoiScale1("Voronoi Scale", Float) = 10
		_VoronoiAngleChangeSpeed1("Voronoi Angle Change Speed", Range( 1 , 10)) = 6
		[Toggle]_CustomDisplacementTextureEnable1("Custom Displacement Texture Enable", Float) = 0
		_DisplacementTexture1("Displacement Texture", 2D) = "white" {}
		_VoronoiSpeed1("Voronoi Speed", Vector) = (0.1,0,0,0)
		_VoronoiColour1("Voronoi Colour", Color) = (0,0,0,0)
		[Toggle]_NoiseColourMultiplyAddToggle1("Noise Colour Multiply/Add Toggle", Float) = 1
		[Toggle]_ToggleSwitch7("Toggle Switch6", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard alpha:fade keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _CustomDisplacementTextureEnable1;
		uniform float _VoronoiScale1;
		uniform float _VoronoiAngleChangeSpeed1;
		uniform float2 _VoronoiSpeed1;
		uniform sampler2D _DisplacementTexture1;
		uniform float _VertexDirection;
		uniform float _ToggleSwitch7;
		uniform float _NoiseColourMultiplyAddToggle1;
		uniform float4 _VoronoiColour1;
		uniform float _EdgeLength;


		float2 voronoihash47( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi47( float2 v, float time, inout float2 id, float smoothness )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mr = 0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash47( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = g - f + o;
					float d = 0.5 * dot( r, r );
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			 		}
			 	}
			}
			return F1;
		}


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float time47 = ( _Time.y / _VoronoiAngleChangeSpeed1 );
			float2 panner43 = ( _Time.y * _VoronoiSpeed1 + v.texcoord.xy);
			float2 coords47 = panner43 * _VoronoiScale1;
			float2 id47 = 0;
			float voroi47 = voronoi47( coords47, time47,id47, 0 );
			float4 temp_cast_0 = (voroi47).xxxx;
			float4 Voronoise46 = (( _CustomDisplacementTextureEnable1 )?( tex2Dlod( _DisplacementTexture1, float4( panner43, 0, 0.0) ) ):( temp_cast_0 ));
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( Voronoise46 * float4( ase_vertexNormal , 0.0 ) * _VertexDirection ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color63 = IsGammaSpace() ? float4(0,0.7868185,1,0) : float4(0,0.581726,1,0);
			float time47 = ( _Time.y / _VoronoiAngleChangeSpeed1 );
			float2 panner43 = ( _Time.y * _VoronoiSpeed1 + i.uv_texcoord);
			float2 coords47 = panner43 * _VoronoiScale1;
			float2 id47 = 0;
			float voroi47 = voronoi47( coords47, time47,id47, 0 );
			float4 temp_cast_0 = (voroi47).xxxx;
			float4 Voronoise46 = (( _CustomDisplacementTextureEnable1 )?( tex2D( _DisplacementTexture1, panner43 ) ):( temp_cast_0 ));
			float4 Albedo60 = (( _ToggleSwitch7 )?( ( color63 * (( _NoiseColourMultiplyAddToggle1 )?( ( Voronoise46 + _VoronoiColour1 ) ):( ( Voronoise46 * _VoronoiColour1 ) )) ) ):( ( color63 + (( _NoiseColourMultiplyAddToggle1 )?( ( Voronoise46 + _VoronoiColour1 ) ):( ( Voronoise46 * _VoronoiColour1 ) )) ) ));
			o.Albedo = Albedo60.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
0;738;1530;381;895.2679;-163.7616;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;65;-2410.935,-372.584;Inherit;False;1422.052;517.5147;This generates the water pattern with a Voronoi, as well as sliders to control it ;14;48;50;38;49;39;37;40;43;42;41;47;44;45;46;Voronoi//Thanks Jordy ;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-2360.935,-322.584;Inherit;False;Property;_VoronoiAngleChangeSpeed1;Voronoi Angle Change Speed;7;0;Create;True;0;0;False;0;6;1.47;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-2074.04,21.20704;Inherit;False;Property;_VoronoiScale1;Voronoi Scale;6;0;Create;True;0;0;False;0;10;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;37;-2087.899,-242.2551;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;39;-2064.06,-316.2053;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;49;-2346.417,-243.4432;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;50;-2328.38,-116.5013;Inherit;False;Property;_VoronoiSpeed1;Voronoi Speed;10;0;Create;True;0;0;False;0;0.1,0;0.1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;38;-2295.035,33.93079;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;41;-1850.935,-136.584;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;43;-2061.612,-156.5942;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;42;-1892.246,-311.0942;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;-10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;44;-1789.868,-114.4063;Inherit;True;Property;_DisplacementTexture1;Displacement Texture;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VoronoiNode;47;-1721.143,-266.3112;Inherit;False;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;2;FLOAT;0;FLOAT;1
Node;AmplifyShaderEditor.ToggleSwitchNode;45;-1501.648,-229.7021;Inherit;False;Property;_CustomDisplacementTextureEnable1;Custom Displacement Texture Enable;8;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;46;-1212.883,-229.4061;Inherit;True;Voronoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;66;-2438.69,258.8486;Inherit;False;1424.383;539.1541;This controls the voronoi albedo for the water;10;53;61;54;55;63;56;57;58;59;60;Water Albedo//Thanks Jordy  ;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;53;-2388.69,586.0027;Inherit;False;Property;_VoronoiColour1;Voronoi Colour;11;0;Create;True;0;0;False;0;0,0,0,0;0.09433956,0.09433956,0.09433956,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;61;-2357.977,509.1656;Inherit;False;46;Voronoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-2131.722,509.9547;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;-2131.815,605.8738;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;56;-1965.688,511.8235;Inherit;True;Property;_NoiseColourMultiplyAddToggle1;Noise Colour Multiply/Add Toggle;12;0;Create;True;0;0;False;0;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;63;-1863.198,308.8486;Inherit;False;Constant;_AlbedoColour;AlbedoColour;13;0;Create;True;0;0;False;0;0,0.7868185,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-1605.748,533.8677;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;58;-1599.161,439.9858;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;59;-1462.813,443.3248;Inherit;True;Property;_ToggleSwitch7;Toggle Switch6;13;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;67;-632.8741,260.1666;Inherit;False;643.473;489.2985;Comment;5;68;52;69;35;70;Vertex Offset Input ;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;60;-1238.308,445.2418;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;-601.4998,307.3636;Inherit;False;46;Voronoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.NormalVertexDataNode;69;-607.4183,389.0858;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;52;-606.0385,540.444;Inherit;False;Property;_VertexDirection;Vertex Direction ;0;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;64;-248.4808,-28.12284;Inherit;False;60;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-280.3846,371.4988;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;70;-547.4835,363.8428;Inherit;False;-1;;1;0;OBJECT;;False;1;OBJECT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;StylizedWater;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;37;0;48;0
WireConnection;41;0;40;0
WireConnection;43;0;49;0
WireConnection;43;2;50;0
WireConnection;43;1;38;0
WireConnection;42;0;39;0
WireConnection;42;1;37;0
WireConnection;44;1;43;0
WireConnection;47;0;43;0
WireConnection;47;1;42;0
WireConnection;47;2;41;0
WireConnection;45;0;47;0
WireConnection;45;1;44;0
WireConnection;46;0;45;0
WireConnection;54;0;61;0
WireConnection;54;1;53;0
WireConnection;55;0;61;0
WireConnection;55;1;53;0
WireConnection;56;0;54;0
WireConnection;56;1;55;0
WireConnection;57;0;63;0
WireConnection;57;1;56;0
WireConnection;58;0;63;0
WireConnection;58;1;56;0
WireConnection;59;0;58;0
WireConnection;59;1;57;0
WireConnection;60;0;59;0
WireConnection;68;0;35;0
WireConnection;68;1;69;0
WireConnection;68;2;52;0
WireConnection;0;0;64;0
WireConnection;0;11;68;0
ASEEND*/
//CHKSM=8877AF4B4B50570ACF8CFBEC1C218E3EC1B85C47