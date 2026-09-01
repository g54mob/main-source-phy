using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace HTraceAO.Scripts.Infrastructure.URP
{
	[VolumeComponentMenu("Lighting/HTrace: Ambient Occlusion")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[VolumeRequiresRendererFeatures(new Type[] { typeof(HTraceAORendererFeature) })]
	[HelpURL("https://ipgames.gitbook.io/htrace-ao")]
	public sealed class HTraceAOVolume : VolumeComponent, IPostProcessComponent
	{
		[Tooltip("Enable HTrace Ambient Occlusion.")]
		public BoolParameter Enable;

		public AmbientOcclusionModeParameter AmbientOcclusionMode;

		[InspectorName("Buffer")]
		[Tooltip("Visualizes the debug mode for different buffers.")]
		public HBufferParameter HBuffer;

		[InspectorName("Intensity")]
		[Tooltip("Guides the final intensity of the ambient occlusion, higher values result in darker ambient occlusion.")]
		public ClampedFloatParameter Intensity;

		[InspectorName("Debug Mode")]
		[Tooltip("Visualizes the debug mode for different rendering components of H-Trace.")]
		public DebugModeSSAOParameter DebugModeSSAO;

		[InspectorName("Thickness")]
		[Tooltip("Control the thickness of the surfaces on screen. Because the screen-space algorithms can not distinguish thin objects from thick ones, this property helps trace rays behind objects, treating them uniformly.")]
		public ClampedFloatParameter Thickness;

		public ClampedIntParameter Radius;

		[InspectorName("Direct Lighting Occlusion")]
		[Tooltip("Defines how visible the effect is in areas exposed to direct lighting. It's recommended to use 0 for maximum physical accuracy.")]
		public ClampedFloatParameter DirectLightingOcclusion;

		[InspectorName("Debug Mode")]
		[Tooltip("Visualizes the debug mode for different rendering components of H-Trace.")]
		public DebugModeGTAOParameter DebugModeGTAO;

		[InspectorName("Full Resolution")]
		[Tooltip("Determines whether the effect is rendered at full resolution or half resolution.")]
		public BoolParameter FullResolution;

		[InspectorName("Thickness")]
		[Tooltip("Control the thickness of the surfaces on screen. Because the screen-space algorithms can not distinguish thin objects from thick ones, this property helps trace rays behind objects, treating them uniformly.")]
		public ClampedFloatParameter GTAOThickness;

		[InspectorName("World Space Radius")]
		[Tooltip("Defines the maximum distance (in meters) for occluder search. RTAO produces darker results with the same distance due to being more physically correct and not needing a falloff.")]
		public ClampedFloatParameter GTAOWorldSpaceRadius;

		[InspectorName("Slice Count")]
		[Tooltip("Specifies the number of directions (slices) used to evaluate occlusion. The final sample count is calculated as \"Slice Count × Step Count × 2\". Each increment in the Slice Count significantly increases the number of samples. This parameter has the greatest impact on noise reduction (aside from the denoiser itself) but comes with a substantial performance cost.")]
		public ClampedIntParameter GTAOSliceCount;

		[InspectorName("Step Count")]
		[Tooltip("Specifies the number of steps taken along each direction (slice). This parameter determines the accuracy of occlusion and affects noise levels, although to a lesser extent than the Slice Count.")]
		public ClampedIntParameter GTAOStepCount;

		[InspectorName("Visibility Bitmasks")]
		[Tooltip("This approach provides more accurate results, especially in areas with thin geometry such as bars, fences, grills, and thin pillars, at a moderate performance cost.")]
		public BoolParameter GTAOVisibilityBitmasks;

		[InspectorName("Falloff")]
		[Tooltip("Specifies whether falloff should be applied during the occlusion evaluation. Bitmasks are originally designed to work without falloff, which makes them faster.")]
		public BoolParameter GTAOFalloff;

		[InspectorName("Checkerboarding")]
		[Tooltip("Specifies whether the effect should be rendered using a checkerboard pattern, which processes only half of the pixels per frame. This option is designed to minimize visual impact while improving calculation times by up to 50%. It is recommended to enable this feature whenever possible.")]
		public BoolParameter GTAOCheckerboarding;

		[InspectorName("Sample Count")]
		[Tooltip("Specifies the number of temporally accumulated frames. More samples lead to better noise reduction with no additional performance cost, while fewer samples make occlusion more reactive.")]
		public ClampedIntParameter GTAOSampleCountTemporal;

		[InspectorName("Motion Rejection")]
		[Tooltip("Controls the strictness of temporal history rejection. Lower values accept all history, producing smoother output with less noise, but can cause ghosting and trailing near moving objects or during camera translation/rotation. Higher values reject potentially invalid history samples, but may result in noisier output.")]
		public ClampedFloatParameter GTAOMotionRejection;

		[InspectorName("Normal Rejection")]
		[Tooltip("Specifies whether the difference in surface normals should be considered during temporal history reprojection. This option can mitigate reprojection artifacts, such as the one in the screenshot where color from the frontal plane of the cube \"leaks\" onto its newly revealed side during camera panning.")]
		public ClampedFloatParameter GTAONormalRejectionTemporal;

		[InspectorName("Rejection Strength")]
		[Tooltip("Defines the overall strictness of temporal history rejection. Similar to Normal Rejection, setting this parameter to high values close to 1.0 can cause temporal instability on small and thin details, such as foliage. Using very low values close to 0.0 can lead to ghosting in certain scenarios.")]
		public ClampedFloatParameter GTAORejectionStrengthTemporal;

		[InspectorName("Reprojection Filter")]
		[Tooltip("Defines a reprojection filter used for temporal history fetch. Bilinear is fast but introduces blur, while Lanczos is approximately three times slower but much sharper. This option affects only the sharpness of the reprojection, not its effectiveness.")]
		public ReprojectionFilterParameter GTAOReprojectionFilter;

		[InspectorName("Pixel Radius")]
		[Tooltip("Controls the spatial denoiser radius in pixels. The wider the radius, the better the noise reduction at the cost of additional blur and performance.")]
		public ClampedIntParameter GTAOPixelRadius;

		[InspectorName("Filter Strength")]
		[Tooltip("Controls the strictness of spatial neighbors' rejection. Higher values better preserve details, while lower values reduce noise more efficiently. Check the first two screenshots in the comparison below to see the difference between the 0 and 1 values of this parameter.")]
		public ClampedFloatParameter GTAOFilterStrength;

		[InspectorName("Normal Rejection")]
		[Tooltip("Specifies whether the difference in surface normals should be considered for spatial neighbors' rejection. Enabling this option further enhances detail preservation at the cost of performance.")]
		public BoolParameter GTAONormalRejectionSpatial;

		[InspectorName("Upscaling Quality")]
		[Tooltip("Defines the filter used for upscaling the half-resolution occlusion buffer.")]
		public UpscalingQualityParameter GTAOUpscalingQuality;

		[InspectorName("Normal Rejection")]
		[Tooltip("Specifies whether the difference between surface normals is considered during the upscaling calculation.")]
		public BoolParameter GTAOUpscalingNormalRejection;

		[InspectorName("Debug Mode")]
		[Tooltip("Visualizes the debug mode for different rendering components of H-Trace.")]
		public DebugModeRTAOParameter DebugModeRTAO;

		[InspectorName("World Space Radius")]
		[Tooltip("Defines the maximum distance (in meters) for occluder search. RTAO produces darker results with the same distance due to being more physically correct and not needing a falloff.")]
		public ClampedFloatParameter RTAOWorldSpaceRadius;

		[InspectorName("Max Ray Bias")]
		[Tooltip("Controls the maximum ray bias (offset) from a surface. This parameter allows to avoid self-intersection with geometry and is especially useful when Unity's TAA is active.")]
		public ClampedFloatParameter RTAOMaxRayBias;

		[InspectorName("Layer Mask")]
		[Tooltip("Use this option to exclude objects from the Ray Tracing Acceleration Structure on a per-layer basis.")]
		public LayerEnumParameter RTAOLayerMask;

		[InspectorName("Ray Count")]
		[Tooltip("Specifies the number of rays launched into the scene to evaluate occlusion. This parameter represents the primary tradeoff between noise level and performance.")]
		public ClampedIntParameter RTAORayCount;

		[InspectorName("Full Resolution")]
		[Tooltip("Determines whether the effect is rendered at full resolution or half resolution.")]
		public BoolParameter RTAOFullResolution;

		[InspectorName("Checkerboarding")]
		[Tooltip("Specifies whether the effect should be rendered using a checkerboard pattern, which processes only half of the pixels per frame. This option is designed to minimize visual impact while improving calculation times by up to 50%. It is recommended to enable this feature whenever possible.")]
		public BoolParameter RTAOCheckerboarding;

		[InspectorName("Sample Count Temporal")]
		[Tooltip("Specifies the number of temporally accumulated frames. More samples lead to better noise reduction with no additional performance cost, while fewer samples make occlusion more reactive.")]
		public ClampedIntParameter RTAOSampleCountTemporal;

		[InspectorName("Motion Rejection")]
		[Tooltip("Controls the strictness of temporal history rejection. Lower values accept all history, producing smoother output with less noise, but can cause ghosting and trailing near moving objects or during camera translation/rotation. Higher values reject potentially invalid history samples, but may result in noisier output.")]
		public ClampedFloatParameter RTAOMotionRejection;

		[InspectorName("Normal Rejection Temporal")]
		[Tooltip("Specifies whether the difference in surface normals should be considered during temporal history reprojection. This option can mitigate reprojection artifacts, such as the one in the screenshot where color from the frontal plane of the cube \"leaks\" onto its newly revealed side during camera panning.")]
		public ClampedFloatParameter RTAONormalRejectionTemporal;

		[InspectorName("Rejection Strength Temporal")]
		[Tooltip("Defines the overall strictness of temporal history rejection. Similar to Normal Rejection, setting this parameter to high values close to 1.0 can cause temporal instability on small and thin details, such as foliage. Using very low values close to 0.0 can lead to ghosting in certain scenarios.")]
		public ClampedFloatParameter RTAORejectionStrengthTemporal;

		[InspectorName("Reprojection Filter")]
		[Tooltip("Defines a reprojection filter used for temporal history fetch. Bilinear is fast but introduces blur, while Lanczos is approximately three times slower but much sharper. This option affects only the sharpness of the reprojection, not its effectiveness.")]
		public ReprojectionFilterParameter RTAOReprojectionFilter;

		[InspectorName("Pixel Radius")]
		[Tooltip("Controls the spatial denoiser radius in pixels. The wider the radius, the better the noise reduction at the cost of additional blur and performance.")]
		public ClampedIntParameter RTAOPixelRadius;

		[InspectorName("Filter Strength")]
		[Tooltip("Controls the strictness of spatial neighbors' rejection. Higher values better preserve details, while lower values reduce noise more efficiently. Check the first two screenshots in the comparison below to see the difference between the 0 and 1 values of this parameter.")]
		public ClampedFloatParameter RTAOFilterStrength;

		[InspectorName("Normal Rejection")]
		[Tooltip("Specifies whether the difference in surface normals should be considered for spatial neighbors' rejection. Enabling this option further enhances detail preservation at the cost of performance.")]
		public BoolParameter RTAONormalRejectionSpatial;

		[InspectorName("Upscaling Quality")]
		[Tooltip("Defines the filter used for upscaling the half-resolution occlusion buffer.")]
		public UpscalingQualityParameter RTAOUpscalingQuality;

		[InspectorName("Normal Rejection")]
		[Tooltip("Specifies whether the difference between surface normals is considered during the upscaling calculation.")]
		public BoolParameter RTAOUpscalingNormalRejection;

		public bool IsActive()
		{
			return false;
		}
	}
}
