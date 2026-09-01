using UnityEngine;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator
{
	public class QualityPreset
	{
		public int particleRaycastBudget;

		public bool softVegetation;

		public int vSyncCount;

		public int antiAliasing;

		public int asyncUploadTimeSlice;

		public int asyncUploadBufferSize;

		public bool asyncUploadPersistentBuffer;

		public bool realtimeReflectionProbes;

		public bool billboardsFaceCameraPosition;

		public float resolutionScalingFixedDPIFactor;

		public bool softParticles;

		public RenderPipelineAsset renderPipeline;

		public SkinWeights skinWeights;

		public bool streamingMipmapsActive;

		public float streamingMipmapsMemoryBudget;

		public int streamingMipmapsRenderersPerFrame;

		public int streamingMipmapsMaxLevelReduction;

		public bool streamingMipmapsAddAllCameras;

		public int streamingMipmapsMaxFileIORequests;

		public int maxQueuedFrames;

		public ColorSpace desiredColorSpace;

		public ColorSpace activeColorSpace;

		public int globalTextureMipmapLimit;

		public int pixelLightCount;

		public int maximumLODLevel;

		public ShadowProjection shadowProjection;

		public int shadowCascades;

		public float shadowDistance;

		public ShadowQuality shadows;

		public ShadowmaskMode shadowmaskMode;

		public float shadowNearPlaneOffset;

		public float shadowCascade2Split;

		public Vector3 shadowCascade4Split;

		public float lodBias;

		public AnisotropicFiltering anisotropicFiltering;

		public ShadowResolution shadowResolution;

		public static QualityPreset CreateFromCurrentLevel()
		{
			return null;
		}

		public void ApplyToCurrentLevel()
		{
		}

		protected void applyToCurrentLevelURP()
		{
		}
	}
}
