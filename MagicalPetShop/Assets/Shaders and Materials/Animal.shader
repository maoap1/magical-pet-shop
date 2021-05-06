Shader "Unlit/Animal"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (3.732132, 0, 0, 1)
		[NoScaleOffset]_MainTex("MainTex", 2D) = "white" {}
		[NoScaleOffset]_BloomTex("BloomTex", 2D) = "white" {}
		[HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
	}
	SubShader
	{
		Tags
		{
			"RenderPipeline" = "UniversalPipeline"
			"RenderType" = "Transparent"
			"UniversalMaterialType" = "Unlit"
			"Queue" = "Transparent"
		}
		Pass
		{
		// Name: <None>
			Tags
			{
				// LightMode: <None>
			}

			// Render State
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}
		ColorMask[_ColorMask]

			// Debug
			// <None>

			// --------------------------------------------------
			// Pass

			HLSLPROGRAM

			// Pragmas
			#pragma target 2.0
		#pragma exclude_renderers d3d11_9x
		#pragma vertex vert
		#pragma fragment frag

			// DotsInstancingOptions: <None>
			// HybridV1InjectedBuiltinProperties: <None>

			// Keywords
			// PassKeywords: <None>
			// GraphKeywords: <None>

			// Defines
			#define _SURFACE_TYPE_TRANSPARENT 1
			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define ATTRIBUTES_NEED_TEXCOORD0
			#define ATTRIBUTES_NEED_COLOR
			#define VARYINGS_NEED_TEXCOORD0
			#define VARYINGS_NEED_COLOR
			#define FEATURES_GRAPH_VERTEX
			/* WARNING: $splice Could not find named fragment 'PassInstancing' */
			#define SHADERPASS SHADERPASS_SPRITEUNLIT
			/* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

			// Includes
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			// --------------------------------------------------
			// Structs and Packing

			struct Attributes
		{
			float3 positionOS : POSITION;
			float3 normalOS : NORMAL;
			float4 tangentOS : TANGENT;
			float4 uv0 : TEXCOORD0;
			float4 color : COLOR;
			#if UNITY_ANY_INSTANCING_ENABLED
			uint instanceID : INSTANCEID_SEMANTIC;
			#endif
		};
		struct Varyings
		{
			float4 positionCS : SV_POSITION;
			float4 texCoord0;
			float4 color;
			#if UNITY_ANY_INSTANCING_ENABLED
			uint instanceID : CUSTOM_INSTANCE_ID;
			#endif
			#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
			uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
			#endif
			#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
			uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
			#endif
			#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
			FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
			#endif
		};
		struct SurfaceDescriptionInputs
		{
			float4 uv0;
		};
		struct VertexDescriptionInputs
		{
			float3 ObjectSpaceNormal;
			float3 ObjectSpaceTangent;
			float3 ObjectSpacePosition;
		};
		struct PackedVaryings
		{
			float4 positionCS : SV_POSITION;
			float4 interp0 : TEXCOORD0;
			float4 interp1 : TEXCOORD1;
			#if UNITY_ANY_INSTANCING_ENABLED
			uint instanceID : CUSTOM_INSTANCE_ID;
			#endif
			#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
			uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
			#endif
			#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
			uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
			#endif
			#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
			FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
			#endif
		};

			PackedVaryings PackVaryings(Varyings input)
		{
			PackedVaryings output;
			output.positionCS = input.positionCS;
			output.interp0.xyzw = input.texCoord0;
			output.interp1.xyzw = input.color;
			#if UNITY_ANY_INSTANCING_ENABLED
			output.instanceID = input.instanceID;
			#endif
			#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
			output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
			#endif
			#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
			output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
			#endif
			#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
			output.cullFace = input.cullFace;
			#endif
			return output;
		}
		Varyings UnpackVaryings(PackedVaryings input)
		{
			Varyings output;
			output.positionCS = input.positionCS;
			output.texCoord0 = input.interp0.xyzw;
			output.color = input.interp1.xyzw;
			#if UNITY_ANY_INSTANCING_ENABLED
			output.instanceID = input.instanceID;
			#endif
			#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
			output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
			#endif
			#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
			output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
			#endif
			#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
			output.cullFace = input.cullFace;
			#endif
			return output;
		}

		// --------------------------------------------------
		// Graph

		// Graph Properties
		CBUFFER_START(UnityPerMaterial)
	float4 _Color;
	float4 _MainTex_TexelSize;
	float4 _BloomTex_TexelSize;
	CBUFFER_END

		// Object and Global properties
		TEXTURE2D(_MainTex);
		SAMPLER(sampler_MainTex);
		TEXTURE2D(_BloomTex);
		SAMPLER(sampler_BloomTex);
		SAMPLER(SamplerState_Linear_Repeat);

			// Graph Functions

		void Unity_InvertColors_float(float In, float InvertColors, out float Out)
		{
			Out = abs(InvertColors - In);
		}

		void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
		{
			Out = UV * Tiling + Offset;
		}

		void Unity_Subtract_float(float A, float B, out float Out)
		{
			Out = A - B;
		}

		void Unity_Add_float(float A, float B, out float Out)
		{
			Out = A + B;
		}

		void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
		{
			Out = A * B;
		}

		void Unity_Add_float4(float4 A, float4 B, out float4 Out)
		{
			Out = A + B;
		}

			// Graph Vertex
			struct VertexDescription
		{
			float3 Position;
			float3 Normal;
			float3 Tangent;
		};

		VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
		{
			VertexDescription description = (VertexDescription)0;
			description.Position = IN.ObjectSpacePosition;
			description.Normal = IN.ObjectSpaceNormal;
			description.Tangent = IN.ObjectSpaceTangent;
			return description;
		}

			// Graph Pixel
			struct SurfaceDescription
		{
			float3 BaseColor;
			float Alpha;
		};

		SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
		{
			SurfaceDescription surface = (SurfaceDescription)0;
			UnityTexture2D _Property_4c0886b90215425580ddf07f1a947098_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
			float4 _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_RGBA_0 = SAMPLE_TEXTURE2D(_Property_4c0886b90215425580ddf07f1a947098_Out_0.tex, _Property_4c0886b90215425580ddf07f1a947098_Out_0.samplerstate, IN.uv0.xy);
			float _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_R_4 = _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_RGBA_0.r;
			float _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_G_5 = _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_RGBA_0.g;
			float _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_B_6 = _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_RGBA_0.b;
			float _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_A_7 = _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_RGBA_0.a;
			float _InvertColors_9f4b8f00b96a4f46af18429e4ea098ba_Out_1;
			float _InvertColors_9f4b8f00b96a4f46af18429e4ea098ba_InvertColors = float(1
		);    Unity_InvertColors_float(_SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_A_7, _InvertColors_9f4b8f00b96a4f46af18429e4ea098ba_InvertColors, _InvertColors_9f4b8f00b96a4f46af18429e4ea098ba_Out_1);
			float4 _Property_48d7cab3796941d7a0d36ad623824a94_Out_0 = IsGammaSpace() ? LinearToSRGB(_Color) : _Color;
			float2 _TilingAndOffset_1760631ae5f24d6aaebdec098f9b1c2e_Out_3;
			Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), float2 (-0.02, -0.02), _TilingAndOffset_1760631ae5f24d6aaebdec098f9b1c2e_Out_3);
			float4 _SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_RGBA_0 = SAMPLE_TEXTURE2D(_Property_4c0886b90215425580ddf07f1a947098_Out_0.tex, _Property_4c0886b90215425580ddf07f1a947098_Out_0.samplerstate, _TilingAndOffset_1760631ae5f24d6aaebdec098f9b1c2e_Out_3);
			float _SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_R_4 = _SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_RGBA_0.r;
			float _SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_G_5 = _SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_RGBA_0.g;
			float _SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_B_6 = _SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_RGBA_0.b;
			float _SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_A_7 = _SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_RGBA_0.a;
			float _Subtract_3aefeeec4acf46bea9363aed113f8c1a_Out_2;
			Unity_Subtract_float(_SampleTexture2D_0eb26b1718174e59bfa7ab96d05f0b6a_A_7, _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_A_7, _Subtract_3aefeeec4acf46bea9363aed113f8c1a_Out_2);
			float2 _TilingAndOffset_6b0644ec7c284897a4293b5eb03dee3c_Out_3;
			Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), float2 (0.02, -0.02), _TilingAndOffset_6b0644ec7c284897a4293b5eb03dee3c_Out_3);
			float4 _SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_RGBA_0 = SAMPLE_TEXTURE2D(_Property_4c0886b90215425580ddf07f1a947098_Out_0.tex, _Property_4c0886b90215425580ddf07f1a947098_Out_0.samplerstate, _TilingAndOffset_6b0644ec7c284897a4293b5eb03dee3c_Out_3);
			float _SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_R_4 = _SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_RGBA_0.r;
			float _SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_G_5 = _SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_RGBA_0.g;
			float _SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_B_6 = _SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_RGBA_0.b;
			float _SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_A_7 = _SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_RGBA_0.a;
			float _Subtract_304755c558704fd6a50fd680838dd319_Out_2;
			Unity_Subtract_float(_SampleTexture2D_a9d1f39a3fb7467bb1bcaf522038e732_A_7, _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_A_7, _Subtract_304755c558704fd6a50fd680838dd319_Out_2);
			float _Add_fb6a12c23f8e48e7abc71a383195c809_Out_2;
			Unity_Add_float(_Subtract_3aefeeec4acf46bea9363aed113f8c1a_Out_2, _Subtract_304755c558704fd6a50fd680838dd319_Out_2, _Add_fb6a12c23f8e48e7abc71a383195c809_Out_2);
			float2 _TilingAndOffset_3cffe83c38ab4ad4b0dddf08d0187255_Out_3;
			Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), float2 (-0.02, 0.02), _TilingAndOffset_3cffe83c38ab4ad4b0dddf08d0187255_Out_3);
			float4 _SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_RGBA_0 = SAMPLE_TEXTURE2D(_Property_4c0886b90215425580ddf07f1a947098_Out_0.tex, _Property_4c0886b90215425580ddf07f1a947098_Out_0.samplerstate, _TilingAndOffset_3cffe83c38ab4ad4b0dddf08d0187255_Out_3);
			float _SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_R_4 = _SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_RGBA_0.r;
			float _SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_G_5 = _SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_RGBA_0.g;
			float _SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_B_6 = _SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_RGBA_0.b;
			float _SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_A_7 = _SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_RGBA_0.a;
			float _Subtract_436f362f52214b048c7edd0e79830061_Out_2;
			Unity_Subtract_float(_SampleTexture2D_ea999e24b62940d9abdda330c3e274cf_A_7, _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_A_7, _Subtract_436f362f52214b048c7edd0e79830061_Out_2);
			float2 _TilingAndOffset_a8acf869a3d24e45922b92c21c8dca57_Out_3;
			Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), float2 (0.02, 0.02), _TilingAndOffset_a8acf869a3d24e45922b92c21c8dca57_Out_3);
			float4 _SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_RGBA_0 = SAMPLE_TEXTURE2D(_Property_4c0886b90215425580ddf07f1a947098_Out_0.tex, _Property_4c0886b90215425580ddf07f1a947098_Out_0.samplerstate, _TilingAndOffset_a8acf869a3d24e45922b92c21c8dca57_Out_3);
			float _SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_R_4 = _SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_RGBA_0.r;
			float _SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_G_5 = _SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_RGBA_0.g;
			float _SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_B_6 = _SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_RGBA_0.b;
			float _SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_A_7 = _SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_RGBA_0.a;
			float _Subtract_a3d67838421f45e2844871a50d4ee6f1_Out_2;
			Unity_Subtract_float(_SampleTexture2D_7b3a0b51271745479fcfd2097e208b19_A_7, _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_A_7, _Subtract_a3d67838421f45e2844871a50d4ee6f1_Out_2);
			float _Add_c0fc9a68e1bd4e44863a9fa19272ba63_Out_2;
			Unity_Add_float(_Subtract_436f362f52214b048c7edd0e79830061_Out_2, _Subtract_a3d67838421f45e2844871a50d4ee6f1_Out_2, _Add_c0fc9a68e1bd4e44863a9fa19272ba63_Out_2);
			float _Add_90d3bd8f0888401fa101ec656ff5a097_Out_2;
			Unity_Add_float(_Add_fb6a12c23f8e48e7abc71a383195c809_Out_2, _Add_c0fc9a68e1bd4e44863a9fa19272ba63_Out_2, _Add_90d3bd8f0888401fa101ec656ff5a097_Out_2);
			float4 _Multiply_df6b57e09b6f41f182364a8997f5f1bf_Out_2;
			Unity_Multiply_float(_Property_48d7cab3796941d7a0d36ad623824a94_Out_0, (_Add_90d3bd8f0888401fa101ec656ff5a097_Out_2.xxxx), _Multiply_df6b57e09b6f41f182364a8997f5f1bf_Out_2);
			float4 _Multiply_35a8ff2e5d4f4720a8422608099d9bc8_Out_2;
			Unity_Multiply_float((_InvertColors_9f4b8f00b96a4f46af18429e4ea098ba_Out_1.xxxx), _Multiply_df6b57e09b6f41f182364a8997f5f1bf_Out_2, _Multiply_35a8ff2e5d4f4720a8422608099d9bc8_Out_2);
			float4 _Multiply_df32c03992cb4eb681689daf016e7148_Out_2;
			Unity_Multiply_float(_SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_RGBA_0, (_SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_A_7.xxxx), _Multiply_df32c03992cb4eb681689daf016e7148_Out_2);
			UnityTexture2D _Property_70367e0107a743c8a9fb352e471e5f87_Out_0 = UnityBuildTexture2DStructNoScale(_BloomTex);
			float4 _SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_RGBA_0 = SAMPLE_TEXTURE2D(_Property_70367e0107a743c8a9fb352e471e5f87_Out_0.tex, _Property_70367e0107a743c8a9fb352e471e5f87_Out_0.samplerstate, IN.uv0.xy);
			float _SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_R_4 = _SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_RGBA_0.r;
			float _SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_G_5 = _SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_RGBA_0.g;
			float _SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_B_6 = _SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_RGBA_0.b;
			float _SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_A_7 = _SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_RGBA_0.a;
			float4 Color_30a7567bdd9449778ece54ba086ba25c = IsGammaSpace() ? LinearToSRGB(float4(16, 16, 16, 1)) : float4(16, 16, 16, 1);
			float4 _Multiply_cf7aee6b1a254f96968dbbcd97e1f420_Out_2;
			Unity_Multiply_float(_SampleTexture2D_b606137aa0b144dfb0a1f8651cb10fda_RGBA_0, Color_30a7567bdd9449778ece54ba086ba25c, _Multiply_cf7aee6b1a254f96968dbbcd97e1f420_Out_2);
			float4 _Add_3baa3c93696c4356934247a96616a65a_Out_2;
			Unity_Add_float4(_Multiply_df32c03992cb4eb681689daf016e7148_Out_2, _Multiply_cf7aee6b1a254f96968dbbcd97e1f420_Out_2, _Add_3baa3c93696c4356934247a96616a65a_Out_2);
			float4 _Add_833ef030830b47bf825d90b65d618f97_Out_2;
			Unity_Add_float4(_Multiply_35a8ff2e5d4f4720a8422608099d9bc8_Out_2, _Add_3baa3c93696c4356934247a96616a65a_Out_2, _Add_833ef030830b47bf825d90b65d618f97_Out_2);
			float _Split_23957f85dd2c4e30a9999c05254ef079_R_1 = _Multiply_35a8ff2e5d4f4720a8422608099d9bc8_Out_2[0];
			float _Split_23957f85dd2c4e30a9999c05254ef079_G_2 = _Multiply_35a8ff2e5d4f4720a8422608099d9bc8_Out_2[1];
			float _Split_23957f85dd2c4e30a9999c05254ef079_B_3 = _Multiply_35a8ff2e5d4f4720a8422608099d9bc8_Out_2[2];
			float _Split_23957f85dd2c4e30a9999c05254ef079_A_4 = _Multiply_35a8ff2e5d4f4720a8422608099d9bc8_Out_2[3];
			float _Add_0bbd89154f5e4ac88e27d852dfc80640_Out_2;
			Unity_Add_float(_Split_23957f85dd2c4e30a9999c05254ef079_A_4, _SampleTexture2D_c887d2dcb8834f469f634a608b9cd540_A_7, _Add_0bbd89154f5e4ac88e27d852dfc80640_Out_2);
			surface.BaseColor = (_Add_833ef030830b47bf825d90b65d618f97_Out_2.xyz);
			surface.Alpha = _Add_0bbd89154f5e4ac88e27d852dfc80640_Out_2;
			return surface;
		}

			// --------------------------------------------------
			// Build Graph Inputs

			VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
		{
			VertexDescriptionInputs output;
			ZERO_INITIALIZE(VertexDescriptionInputs, output);

			output.ObjectSpaceNormal = input.normalOS;
			output.ObjectSpaceTangent = input.tangentOS;
			output.ObjectSpacePosition = input.positionOS;

			return output;
		}
			SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
		{
			SurfaceDescriptionInputs output;
			ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





			output.uv0 = input.texCoord0;
		#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
		#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
		#else
		#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
		#endif
		#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

			return output;
		}

			// --------------------------------------------------
			// Main

			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SpriteUnlitPass.hlsl"

			ENDHLSL
		}
	}
	FallBack "Hidden/Shader Graph/FallbackError"
}