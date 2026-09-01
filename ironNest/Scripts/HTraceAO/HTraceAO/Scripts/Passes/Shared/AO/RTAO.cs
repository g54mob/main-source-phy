using HTraceAO.Scripts.Extensions;
using HTraceAO.Scripts.Extensions.CameraHistorySystem;
using HTraceAO.Scripts.Wrappers;
using UnityEngine;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Passes.Shared.AO
{
	internal static class RTAO
	{
		private enum HDenoiseRTAOKernel
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

		private enum HRenderRTAOKernel
		{
			RenderRTAO = 0
		}

		private enum HInterpolationKernel
		{
			Interpolation = 0
		}

		internal struct HistoryCameraDataRTAO : ICameraHistoryData
		{
			private int hash;

			public RTWrapper NormalHistory_RTAO;

			public RTWrapper OcclusionHistory_RTAO;

			public HistoryCameraDataRTAO(int hash = 0)
			{
				this.hash = 0;
				NormalHistory_RTAO = null;
				OcclusionHistory_RTAO = null;
			}

			public int GetHash()
			{
				return 0;
			}

			public void SetHash(int hashIn)
			{
			}
		}

		private const string NORMAL_REJECTION_TEMPORAL = "NORMAL_REJECTION_TEMPORAL";

		private const string LANCZOS_REPROJECTION = "LANCZOS_REPROJECTION";

		private const string CHECKERBOARDING = "CHECKERBOARDING";

		private const string CULL_BACK_FACES = "CULL_BACK_FACES";

		private const string VR_COMPATIBILITY = "VR_COMPATIBILITY";

		private const string INTERPOLATION_LINEAR_5 = "INTERPOLATION_LINEAR_5";

		private const string INTERPOLATION_LINEAR_9 = "INTERPOLATION_LINEAR_9";

		private const string INTERPOLATION_LANCZOS_12 = "INTERPOLATION_LANCZOS_12";

		private const string NORMAL_REJECTION_SPATIAL = "NORMAL_REJECTION_SPATIAL";

		private const string NORMAL_REJECTION = "NORMAL_REJECTION";

		private const string FINAL_OUTPUT_ONLY = "FINAL_OUTPUT_ONLY";

		private const string RADIUS_1 = "RADIUS_1";

		private const string RADIUS_2 = "RADIUS_2";

		private const string RADIUS_3 = "RADIUS_3";

		private const string RADIUS_4 = "RADIUS_4";

		internal static ComputeShader HRenderRTAO;

		internal static ComputeShader HDenoiseRTAO;

		internal static ComputeShader HInterpolationRTAO;

		internal static ComputeShader HCheckerboardingRTAO;

		internal static RayTracingShader HRayTraceRTAO;

		internal static RayTracingAccelerationStructure RTAS;

		internal static ProfilingSamplerHTrace CheckerboardingSampler;

		internal static ProfilingSamplerHTrace RenderOcclusionSampler;

		internal static ProfilingSamplerHTrace TemporalAccumulationSampler;

		internal static ProfilingSamplerHTrace SpatialFilterSampler;

		internal static ProfilingSamplerHTrace InterpolationSampler;

		internal static readonly CameraHistorySystem<HistoryCameraDataRTAO> CameraHistorySystem;

		internal static RTWrapper NormalHistory_RTAO;

		internal static RTWrapper OcclusionHistory_RTAO;

		internal static RTWrapper Occlusion_RTAO;

		internal static RTWrapper OcclusionFiltered_RTAO;

		internal static RTWrapper OcclusionInterpolated_RTAO;

		internal static RTWrapper OcclusionAccumulated_RTAO;

		internal static RTWrapper OcclusionReprojected_RTAO;

		internal static RTWrapper DepthPyramid_RTAO;

		internal static RTWrapper VelocityHistory_RTAO;

		internal static RTWrapper VelocityReprojected_RTAO;

		internal static GraphicsBuffer IndirectArguments;

		internal static HDynamicBuffer IndirectCoords;

		internal static ComputeBuffer RayCounter;

		internal const string _DepthPyramid = "_DepthPyramid";

		internal const string _Occlusion = "_Occlusion";

		internal const string _NormalHistory = "_NormalHistory";

		internal const string _VelocityHistory = "_VelocityHistory";

		internal const string _VelocityReprojected = "_VelocityReprojected";

		internal const string _OcclusionAccumulated = "_OcclusionAccumulated";

		internal const string _OcclusionReprojected = "_OcclusionReprojected";

		internal const string _OcclusionHistory = "_OcclusionHistory";

		internal const string _OcclusionFiltered = "_OcclusionFiltered";

		internal const string _OcclusionInterpolated = "_OcclusionInterpolated";

		private static RenderTextureDescriptor RTDescriptor;

		private static RayTracingInstanceCullingTest[] _instanceTests;

		private static RayTracingInstanceCullingConfig _cullingConfig;

		private static RayTracingInstanceCullingTest _instanceTest;

		internal static void SetupRTAS(Camera camera, int cameraHeight)
		{
		}

		private static void KeywordSwitch(ComputeShader Compute, bool State, string Keyword)
		{
		}

		public static void Execute(CommandBuffer cmd, Camera camera, int cameraWidth, int cameraHeight)
		{
		}
	}
}
