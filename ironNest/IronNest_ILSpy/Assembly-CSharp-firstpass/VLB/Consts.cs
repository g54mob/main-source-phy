using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public static class Consts
{
	public static class Help
	{
		public static class SD
		{
			public const string UrlBeam = "http://saladgamer.com/vlb-doc/comp-lightbeam-sd/";

			public const string UrlDynamicOcclusionRaycasting = "http://saladgamer.com/vlb-doc/comp-dynocclusion-sd-raycasting/";

			public const string UrlDynamicOcclusionDepthBuffer = "http://saladgamer.com/vlb-doc/comp-dynocclusion-sd-depthbuffer/";

			public const string UrlSkewingHandle = "http://saladgamer.com/vlb-doc/comp-skewinghandle-sd/";

			public const string AddComponentMenuSD = "VLB/SD/";

			public const string AddComponentMenuBeam = "VLB/SD/Volumetric Light Beam SD";

			public const string AddComponentMenuDynamicOcclusionRaycasting = "VLB/SD/Dynamic Occlusion (Raycasting)";

			public const string AddComponentMenuDynamicOcclusionDepthBuffer = "VLB/SD/Dynamic Occlusion (Depth Buffer)";
		}

		public static class HD
		{
			public const string UrlBeam = "http://saladgamer.com/vlb-doc/comp-lightbeam-hd/";

			public const string UrlShadow = "http://saladgamer.com/vlb-doc/comp-shadow-hd/";

			public const string UrlCookie = "http://saladgamer.com/vlb-doc/comp-cookie-hd/";

			public const string UrlTrackRealtimeChangesOnLight = "http://saladgamer.com/vlb-doc/comp-trackrealtimechanges-hd/";

			public const string AddComponentMenuHD = "VLB/HD/";

			public const string AddComponentMenuBeam3D = "VLB/HD/Volumetric Light Beam HD";

			public const string AddComponentMenuBeam2D = "VLB/HD/Volumetric Light Beam HD (2D)";

			public const string AddComponentMenuShadow = "VLB/HD/Volumetric Shadow HD";

			public const string AddComponentMenuCookie = "VLB/HD/Volumetric Cookie HD";

			public const string AddComponentMenuTrackRealtimeChangesOnLight = "VLB/HD/Track Realtime Changes On Light";
		}

		private const string UrlBase = "http://saladgamer.com/vlb-doc/";

		private const string UrlSuffix = "/";

		public const string UrlDustParticles = "http://saladgamer.com/vlb-doc/comp-dustparticles/";

		public const string UrlTriggerZone = "http://saladgamer.com/vlb-doc/comp-triggerzone/";

		public const string UrlEffectFlicker = "http://saladgamer.com/vlb-doc/comp-effect-flicker/";

		public const string UrlEffectPulse = "http://saladgamer.com/vlb-doc/comp-effect-pulse/";

		public const string UrlEffectFromProfile = "http://saladgamer.com/vlb-doc/comp-effect-from-profile/";

		public const string UrlLODBeamGroup = "http://saladgamer.com/vlb-doc/comp-lodbeamgroup/";

		public const string UrlConfig = "http://saladgamer.com/vlb-doc/config/";

		public const string AddComponentMenuBase = "VLB/";

		public const string AddComponentMenuCommon = "VLB/Common/";

		public const string AddComponentMenuDustParticles = "VLB/Common/Volumetric Dust Particles";

		public const string AddComponentMenuTriggerZone = "VLB/Common/Trigger Zone";

		public const string AddComponentMenuEffectFlicker = "VLB/Common/Effect Flicker";

		public const string AddComponentMenuEffectPulse = "VLB/Common/Effect Pulse";

		public const string AddComponentMenuEffectFromProfile = "VLB/Common/Effect From Profile";
	}

	public static class Internal
	{
		public static readonly bool ProceduralObjectsVisibleInEditor = true;

		public static HideFlags ProceduralObjectsHideFlags
		{
			get
			{
				//IL_0013: Expected I, but got O
				nint num = (nint)typeof(Internal);
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb eax,eax\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (Il2CppClass<VLB.Consts+Internal>)+B8]");
				return (HideFlags)((nint)0 + (nint)61);
			}
		}
	}

	public static class Beam
	{
		public static class SD
		{
			public const float FresnelPowMaxValue = 10f;

			public const float FresnelPow = 8f;

			public const float GlareFrontalDefault = 0.5f;

			public const float GlareBehindDefault = 0.5f;

			public const float GlareMin = 0f;

			public const float GlareMax = 1f;

			public static readonly Vector2 TiltDefault;

			public static readonly Vector3 SkewingLocalForwardDirectionDefault;

			public const Transform ClippingPlaneTransformDefault = null;

			static SD()
			{
				//IL_006c: Expected I, but got O
				//IL_008a: Expected I, but got O
				//IL_0018: Expected I, but got O
				//IL_0036: Expected I, but got O
				nint num = (nint)typeof(Vector2);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rax_v3 (Il2CppClass<UnityEngine.Vector2>)+B8]");
				nint num2 = 0;
				nint num3 = (nint)typeof(SD);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v4 (Il2CppClass<VLB.Consts+Beam+SD>)+B8]");
				nint num4 = 0;
				TiltDefault = Vector2.zeroVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
				_ = 0;
				nint num5 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num6 = 0;
				nint num7 = (nint)typeof(SD);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v7 (Il2CppClass<VLB.Consts+Beam+SD>)+B8]");
				nint num8 = 0;
				SkewingLocalForwardDirectionDefault = Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rcx_v5 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				_ = 0;
			}
		}

		public static class HD
		{
			public const AttenuationEquationHD AttenuationEquationDefault = AttenuationEquationHD.Quadratic;

			public const float SideSoftnessDefault = 1f;

			public const float SideSoftnessMin = 0.0001f;

			public const float SideSoftnessMax = 10f;

			public const float JitteringFactorDefault = 0f;

			public const float JitteringFactorMin = 0f;

			public const int JitteringFrameRateDefault = 60;

			public const int JitteringFrameRateMin = 0;

			public const int JitteringFrameRateMax = 120;

			public static readonly MinMaxRangeFloat JitteringLerpRange;

			static HD()
			{
				//IL_0013: Expected I, but got O
				//IL_002d: Expected O, but got I4
				nint num = (nint)typeof(HD);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2 (Il2CppClass<VLB.Consts+Beam+HD>)+B8]");
				nint num2 = 0;
				JitteringLerpRange = (MinMaxRangeFloat)0;
				_ = 1051260355;
			}
		}

		public static readonly Color FlatColor;

		public const ColorMode ColorModeDefault = ColorMode.Flat;

		public const float MultiplierDefault = 1f;

		public const float MultiplierMin = 0f;

		public const float IntensityDefault = 1f;

		public const float IntensityMin = 0f;

		public const float HDRPExposureWeightDefault = 0f;

		public const float HDRPExposureWeightMin = 0f;

		public const float HDRPExposureWeightMax = 1f;

		public const float SpotAngleDefault = 35f;

		public const float SpotAngleMin = 0.1f;

		public const float SpotAngleMax = 179.9f;

		public const float ConeRadiusStart = 0.1f;

		public const MeshType GeomMeshType = MeshType.Shared;

		public const int GeomSidesDefault = 18;

		public const int GeomSidesMin = 3;

		public const int GeomSidesMax = 256;

		public const int GeomSegmentsDefault = 5;

		public const int GeomSegmentsMin = 0;

		public const int GeomSegmentsMax = 64;

		public const bool GeomCap = false;

		public const bool ScalableDefault = true;

		public const AttenuationEquation AttenuationEquationDefault = AttenuationEquation.Quadratic;

		public const float AttenuationCustomBlendingDefault = 0.5f;

		public const float AttenuationCustomBlendingMin = 0f;

		public const float AttenuationCustomBlendingMax = 1f;

		public const float FallOffStart = 0f;

		public const float FallOffEnd = 3f;

		public const float FallOffDistancesMinThreshold = 0.01f;

		public const float DepthBlendDistance = 2f;

		public const float CameraClippingDistance = 0.5f;

		public const NoiseMode NoiseModeDefault = NoiseMode.Disabled;

		public const float NoiseIntensityMin = 0f;

		public const float NoiseIntensityMax = 1f;

		public const float NoiseIntensityDefault = 0.5f;

		public const float NoiseScaleMin = 0.01f;

		public const float NoiseScaleMax = 2f;

		public const float NoiseScaleDefault = 0.5f;

		public static readonly Vector3 NoiseVelocityDefault;

		public const BlendingMode BlendingModeDefault = BlendingMode.Additive;

		public const ShaderAccuracy ShaderAccuracyDefault = ShaderAccuracy.Fast;

		public const float FadeOutBeginDefault = -150f;

		public const float FadeOutEndDefault = -200f;

		public const Dimensions DimensionsDefault = Dimensions.Dim3D;

		static Beam()
		{
			//IL_0016: Expected O, but got I
			//IL_0024: Expected I, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
			FlatColor = (Color)0;
			nint num = (nint)typeof(Beam);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v3 (Il2CppClass<VLB.Consts+Beam>)+B8]");
			nint num2 = 0;
			Vector3 noiseVelocityDefault = default(Vector3);
			NoiseVelocityDefault = noiseVelocityDefault;
			_ = 0.05f;
		}
	}

	public static class DustParticles
	{
		public const float AlphaDefault = 0.5f;

		public const float SizeDefault = 0.01f;

		public const ParticlesDirection DirectionDefault = ParticlesDirection.Random;

		public static readonly Vector3 VelocityDefault;

		public const float DensityDefault = 5f;

		public const float DensityMin = 0f;

		public const float DensityMax = 1000f;

		public static readonly MinMaxRangeFloat SpawnDistanceRangeDefault;

		public const bool CullingEnabledDefault = false;

		public const float CullingMaxDistanceDefault = 10f;

		public const float CullingMaxDistanceMin = 1f;

		static DustParticles()
		{
			//IL_0013: Expected I, but got O
			//IL_0040: Expected I, but got O
			//IL_005a: Expected O, but got I4
			nint num = (nint)typeof(DustParticles);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2 (Il2CppClass<VLB.Consts+DustParticles>)+B8]");
			nint num2 = 0;
			Vector3 velocityDefault = default(Vector3);
			VelocityDefault = velocityDefault;
			_ = 0.03f;
			nint num3 = (nint)typeof(DustParticles);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rax_v3 (Il2CppClass<VLB.Consts+DustParticles>)+B8]");
			nint num4 = 0;
			SpawnDistanceRangeDefault = (MinMaxRangeFloat)0;
			_ = 1060320051;
		}
	}

	public static class DynOcclusion
	{
		public static readonly LayerMask LayerMaskDefault;

		public const DynamicOcclusionUpdateRate UpdateRateDefault = DynamicOcclusionUpdateRate.EveryXFrames;

		public const int WaitFramesCountDefault = 3;

		public const Dimensions RaycastingDimensionsDefault = Dimensions.Dim3D;

		public const bool RaycastingConsiderTriggersDefault = false;

		public const float RaycastingMinOccluderAreaDefault = 0f;

		public const float RaycastingMinSurfaceRatioDefault = 0.5f;

		public const float RaycastingMinSurfaceRatioMin = 50f;

		public const float RaycastingMinSurfaceRatioMax = 100f;

		public const float RaycastingMaxSurfaceDotDefault = 0.25f;

		public const float RaycastingMaxSurfaceAngleMin = 45f;

		public const float RaycastingMaxSurfaceAngleMax = 90f;

		public const PlaneAlignment RaycastingPlaneAlignmentDefault = PlaneAlignment.Surface;

		public const float RaycastingPlaneOffsetDefault = 0.1f;

		public const float RaycastingFadeDistanceToSurfaceDefault = 0.25f;

		public const int DepthBufferDepthMapResolutionDefault = 128;

		public const bool DepthBufferOcclusionCullingDefault = true;

		public const float DepthBufferFadeDistanceToSurfaceDefault = 0f;

		static DynOcclusion()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			LayerMask layerMaskDefault = default(LayerMask);
			LayerMaskDefault = layerMaskDefault;
		}
	}

	public static class Effects
	{
		public const EffectAbstractBase.ComponentsToChange ComponentsToChangeDefault = (EffectAbstractBase.ComponentsToChange)2147483647;

		public const bool RestoreIntensityOnDisableDefault = true;

		public const float FrequencyDefault = 10f;

		public const bool PerformPausesDefault = false;

		public const bool RestoreIntensityOnPauseDefault = false;

		public static readonly MinMaxRangeFloat FlickeringDurationDefault;

		public static readonly MinMaxRangeFloat PauseDurationDefault;

		public static readonly MinMaxRangeFloat IntensityAmplitudeDefault;

		public const float SmoothingDefault = 0.05f;

		static Effects()
		{
			//IL_0013: Expected I, but got O
			//IL_002d: Expected O, but got I4
			//IL_0041: Expected I, but got O
			//IL_005b: Expected O, but got I4
			//IL_006f: Expected I, but got O
			//IL_008d: Expected O, but got I8
			nint num = (nint)typeof(Effects);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2 (Il2CppClass<VLB.Consts+Effects>)+B8]");
			nint num2 = 0;
			FlickeringDurationDefault = (MinMaxRangeFloat)1065353216;
			_ = 1082130432;
			nint num3 = (nint)typeof(Effects);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v26 @ rax_v3 (Il2CppClass<VLB.Consts+Effects>)+B8]");
			nint num4 = 0;
			PauseDurationDefault = (MinMaxRangeFloat)0;
			_ = 1065353216;
			nint num5 = (nint)typeof(Effects);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v4 (Il2CppClass<VLB.Consts+Effects>)+B8]");
			nint num6 = 0;
			IntensityAmplitudeDefault = (MinMaxRangeFloat)3212836864L;
			_ = 1065353216;
		}
	}

	public static class Shadow
	{
		public const float StrengthDefault = 1f;

		public const float StrengthMin = 0f;

		public const float StrengthMax = 1f;

		public static readonly LayerMask LayerMaskDefault;

		public const ShadowUpdateRate UpdateRateDefault = ShadowUpdateRate.EveryXFrames;

		public const int WaitFramesCountDefault = 3;

		public const int DepthMapResolutionDefault = 128;

		public const int DepthMapDepthDefault = 16;

		public const bool OcclusionCullingDefault = true;

		public static string GetErrorChangeRuntimeDepthMapResolution(VolumetricShadowHD comp)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C73]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			if ((object)comp != null)
			{
				string name = comp.name;
				return $"Can't change {name} Shadow.depthMapResolution property at runtime after DepthCamera initialization";
			}
			return (string)(object)new NullReferenceException();
		}

		public static string GetErrorChangeRuntimeDepthMapDepth(VolumetricShadowHD comp)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C74]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			if ((object)comp != null)
			{
				string name = comp.name;
				return $"Can't change {name} Shadow.depthMapDepth property at runtime after DepthCamera initialization";
			}
			return (string)(object)new NullReferenceException();
		}

		static Shadow()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			LayerMask layerMaskDefault = default(LayerMask);
			LayerMaskDefault = layerMaskDefault;
		}
	}

	public static class Cookie
	{
		public const float ContributionDefault = 1f;

		public const float ContributionMin = 0f;

		public const float ContributionMax = 1f;

		public const Texture CookieTextureDefault = null;

		public const CookieChannel ChannelDefault = CookieChannel.Alpha;

		public const bool NegativeDefault = false;

		public static readonly Vector2 TranslationDefault;

		public const float RotationDefault = 0f;

		public static readonly Vector2 ScaleDefault;

		static Cookie()
		{
			//IL_006c: Expected I, but got O
			//IL_008a: Expected I, but got O
			//IL_0018: Expected I, but got O
			//IL_0036: Expected I, but got O
			nint num = (nint)typeof(Vector2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rax_v3 (Il2CppClass<UnityEngine.Vector2>)+B8]");
			nint num2 = 0;
			nint num3 = (nint)typeof(Cookie);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v4 (Il2CppClass<VLB.Consts+Cookie>)+B8]");
			nint num4 = 0;
			TranslationDefault = Vector2.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r8_v1 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
			_ = 0;
			nint num5 = (nint)typeof(Vector2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v6 (Il2CppClass<UnityEngine.Vector2>)+B8]");
			nint num6 = 0;
			nint num7 = (nint)typeof(Cookie);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v7 (Il2CppClass<VLB.Consts+Cookie>)+B8]");
			nint num8 = 0;
			ScaleDefault = Vector2.oneVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rdx_v2 (Il2CppStaticFields<UnityEngine.Vector2>)+C]");
			_ = 0;
		}
	}

	public static class Config
	{
		public static class HD
		{
			public const RenderQueue GeometryRenderQueueDefault = (RenderQueue)3100;

			public const float CameraBlendingDistance = 0.5f;

			public const int RaymarchingQualitiesStepsMin = 2;
		}

		public const bool GeometryOverrideLayerDefault = true;

		public const int GeometryLayerIDDefault = 1;

		public const string GeometryTagDefault = "Untagged";

		public const string FadeOutCameraTagDefault = "MainCamera";

		public const RenderQueue GeometryRenderQueueDefault = RenderQueue.Transparent;

		public const RenderPipeline GeometryRenderPipelineDefault = RenderPipeline.BuiltIn;

		public const RenderingMode GeometryRenderingModeDefault = RenderingMode.Default;

		public const int Noise3DSizeDefault = 64;

		public const float DitheringFactor = 0f;

		public const bool UseLightColorTemperatureDefault = true;

		public const bool FeatureEnabledDefault = true;

		public const FeatureEnabledColorGradient FeatureEnabledColorGradientDefault = FeatureEnabledColorGradient.HighOnly;

		public const int SharedMeshSidesDefault = 24;

		public const int SharedMeshSidesMin = 3;

		public const int SharedMeshSidesMax = 256;

		public const int SharedMeshSegmentsDefault = 5;

		public const int SharedMeshSegmentsMin = 0;

		public const int SharedMeshSegmentsMax = 64;
	}

	public const string PluginFolder = "VolumetricLightBeam";
}
