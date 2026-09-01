using HTraceAO.Scripts.Extensions;
using HTraceAO.Scripts.Wrappers;
using UnityEngine;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Passes.Shared.AO
{
	internal static class SSAO
	{
		private enum HDepthPyramidSSAOKernel
		{
			DepthDownsample_1 = 0,
			DepthDownsample_2 = 1
		}

		private enum HRenderSSAOKernel
		{
			RenderOcclusion = 0
		}

		private enum HDenoiseSSAOKernel
		{
			DenoiseOcclusion_A = 0,
			DenoiseOcclusion_B = 1
		}

		internal static ComputeShader HRenderSSAO;

		internal static ComputeShader HDenoiseSSAO;

		internal static ComputeShader HDepthPyramidSSAO;

		internal static ProfilingSamplerHTrace DepthPyramidGenerationSampler;

		internal static ProfilingSamplerHTrace RenderOcclusionSampler;

		internal static ProfilingSamplerHTrace InterpolationSampler;

		internal static RTWrapper DepthTiled_SSAO;

		internal static RTWrapper DepthPyramid_SSAO;

		internal static RTWrapper DepthIntermediatePyramid_SSAO;

		internal static RTWrapper Occlusion_SSAO_1;

		internal static RTWrapper Occlusion_SSAO_2;

		internal static RTWrapper Occlusion_SSAO_3;

		internal static RTWrapper Occlusion_SSAO_4;

		internal static RTWrapper OcclusionCombined_SSAO_0;

		internal static RTWrapper OcclusionCombined_SSAO_1;

		internal static RTWrapper OcclusionCombined_SSAO_2;

		internal static RTWrapper OcclusionCombined_SSAO_3;

		internal const string _DepthTiled = "_DepthTiled";

		internal const string _DepthPyramid_SSAO = "_DepthPyramid_SSAO";

		internal const string _DepthIntermediatePyramid_SSAO = "_DepthIntermediatePyramid_SSAO";

		internal const string _Occlusion_1 = "_Occlusion_1";

		internal const string _Occlusion_2 = "_Occlusion_2";

		internal const string _Occlusion_3 = "_Occlusion_3";

		internal const string _Occlusion_4 = "_Occlusion_4";

		internal const string _OcclusionCombined_0 = "_OcclusionCombined_0";

		internal const string _OcclusionCombined_1 = "_OcclusionCombined_1";

		internal const string _OcclusionCombined_2 = "_OcclusionCombined_2";

		internal const string _OcclusionCombined_3 = "_OcclusionCombined_3";

		private static RenderTextureDescriptor RTDescriptor;

		private static readonly float[] SampleThickness;

		private static readonly float[] InvThicknessTable;

		private static readonly float[] SampleWeightTable;

		public static void MaterialsShadersSetup()
		{
		}

		private static void UpdateTables(Vector2 depthRes, float tanHalfFovH, float screenspaceDiameter)
		{
		}

		public static void Execute(CommandBuffer cmd, Camera camera, int cameraWidth, int cameraHeight)
		{
		}
	}
}
