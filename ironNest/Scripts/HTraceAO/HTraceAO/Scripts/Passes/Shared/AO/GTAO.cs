using HTraceAO.Scripts.Extensions;
using HTraceAO.Scripts.Extensions.CameraHistorySystem;
using HTraceAO.Scripts.Wrappers;
using UnityEngine;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Passes.Shared.AO
{
	internal static class GTAO
	{
		private enum HDepthPyramidKernel
		{
			GenerateDepthPyramid_1 = 0
		}

		private enum HDenoiseGTAOKernel
		{
			TemporalReprojection = 0,
			TemporalAccumulation = 1,
			SpatialFiltering = 2
		}

		private enum HCheckerboardingKernel
		{
			CheckerboardClassification = 0,
			IndirectArguments = 1
		}

		private enum HRenderGTAOKernel
		{
			RenderGTAO = 0
		}

		private enum HInterpolationKernel
		{
			Interpolation = 0
		}

		internal struct HistoryCameraDataGTAO : ICameraHistoryData
		{
			private int hash;

			public RTWrapper NormalHistory_GTAO;

			public RTWrapper OcclusionHistory_GTAO;

			public HistoryCameraDataGTAO(int hash = 0)
			{
				this.hash = 0;
				NormalHistory_GTAO = null;
				OcclusionHistory_GTAO = null;
			}

			public int GetHash()
			{
				return 0;
			}

			public void SetHash(int hashIn)
			{
			}
		}

		internal static readonly int _DepthPyramid;

		internal static readonly int _DepthIntermediate;

		internal static readonly int _DepthIntermediate_Output;

		internal static readonly int _DepthPyramid_OutputMIP0;

		internal static readonly int _DepthPyramid_OutputMIP1;

		internal static readonly int _DepthPyramid_OutputMIP2;

		internal static readonly int _DepthPyramid_OutputMIP3;

		internal static readonly int _DepthPyramid_OutputMIP4;

		internal static readonly int _DepthPyramid_OutputMIP5;

		internal static readonly int _DepthPyramid_OutputMIP6;

		internal static readonly int _DepthPyramid_OutputMIP7;

		internal static readonly int _DepthPyramid_OutputMIP8;

		internal const string _NormalHistory = "_NormalHistory";

		internal const string _Occlusion = "_Occlusion";

		internal const string _OcclusionAccumulated = "_OcclusionAccumulated";

		internal const string _OcclusionReprojected = "_OcclusionReprojected";

		internal const string _OcclusionHistory = "_OcclusionHistory";

		internal const string _OcclusionFiltered = "_OcclusionFiltered";

		internal const string _OcclusionInterpolated = "_OcclusionInterpolated";

		internal const string _DepthPyramid2 = "_DepthPyramid";

		private const string NORMAL_REJECTION_TEMPORAL = "NORMAL_REJECTION_TEMPORAL";

		private const string LANCZOS_REPROJECTION = "LANCZOS_REPROJECTION";

		private const string CHECKERBOARDING = "CHECKERBOARDING";

		private const string FALLOFF = "FALLOFF";

		private const string VR_COMPATIBILITY = "VR_COMPATIBILITY";

		private const string INTERPOLATION_LINEAR_5 = "INTERPOLATION_LINEAR_5";

		private const string INTERPOLATION_LINEAR_9 = "INTERPOLATION_LINEAR_9";

		private const string INTERPOLATION_LANCZOS_12 = "INTERPOLATION_LANCZOS_12";

		private const string NORMAL_REJECTION_SPATIAL = "NORMAL_REJECTION_SPATIAL";

		private const string NORMAL_REJECTION = "NORMAL_REJECTION";

		private const string FINAL_OUTPUT_ONLY = "FINAL_OUTPUT_ONLY";

		private const string TEMPORAL_ACCUMULATION = "TEMPORAL_ACCUMULATION";

		private const string VISIBILITY_BITMASKS = "VISIBILITY_BITMASKS";

		private const string RADIUS_1 = "RADIUS_1";

		private const string RADIUS_2 = "RADIUS_2";

		private const string RADIUS_3 = "RADIUS_3";

		private const string RADIUS_4 = "RADIUS_4";

		internal static ComputeShader HRenderGTAO;

		internal static ComputeShader HDenoiseGTAO;

		internal static ComputeShader HInterpolationGTAO;

		internal static ComputeShader HCheckerboardingGTAO;

		internal static ComputeShader HDepthPyramid;

		internal static ProfilingSamplerHTrace DepthPyramidGenerationSampler;

		internal static ProfilingSamplerHTrace CheckerboardingSampler;

		internal static ProfilingSamplerHTrace RenderOcclusionSampler;

		internal static ProfilingSamplerHTrace TemporalAccumulationSampler;

		internal static ProfilingSamplerHTrace SpatialFilterSampler;

		internal static ProfilingSamplerHTrace InterpolationSampler;

		internal static readonly CameraHistorySystem<HistoryCameraDataGTAO> CameraHistorySystem;

		internal static RTWrapper Occlusion_GTAO;

		internal static RTWrapper OcclusionFiltered_GTAO;

		internal static RTWrapper OcclusionInterpolated_GTAO;

		internal static RTWrapper OcclusionAccumulated_GTAO;

		internal static RTWrapper OcclusionReprojected_GTAO;

		internal static RTWrapper NormalHistory_GTAO_BIRP;

		internal static RTWrapper OcclusionHistory_GTAO_BIRP;

		internal static RTWrapper DepthPyramidRT;

		internal static RTWrapper DepthIntermediate_Pyramid;

		internal static ComputeBuffer IndirectArguments;

		internal static HDynamicBuffer IndirectCoords;

		internal static ComputeBuffer RayCounter;

		public static void Execute(CommandBuffer cmd, Camera camera, int cameraWidth, int cameraHeight)
		{
		}

		private static void KeywordSwitch(ComputeShader compute, bool state, string keyword)
		{
		}
	}
}
