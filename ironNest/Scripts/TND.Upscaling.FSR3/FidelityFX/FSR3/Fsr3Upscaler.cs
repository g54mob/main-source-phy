using System;
using UnityEngine;

namespace FidelityFX.FSR3
{
	public static class Fsr3Upscaler
	{
		public enum QualityMode
		{
			NativeAA = 0,
			UltraQuality = 1,
			Quality = 2,
			Balanced = 3,
			Performance = 4,
			UltraPerformance = 5
		}

		[Flags]
		public enum InitializationFlags
		{
			EnableHighDynamicRange = 1,
			EnableDisplayResolutionMotionVectors = 2,
			EnableMotionVectorsJitterCancellation = 4,
			EnableDepthInverted = 8,
			EnableDepthInfinite = 0x10,
			EnableAutoExposure = 0x20,
			EnableDynamicResolution = 0x40,
			EnableFP16Usage = 0x80,
			EnableDebugChecking = 0x100
		}

		[Flags]
		public enum DispatchFlags
		{
			DrawDebugView = 1
		}

		public struct ContextDescription
		{
			public InitializationFlags Flags;

			public Vector2Int MaxRenderSize;

			public Vector2Int MaxUpscaleSize;

			public Fsr3UpscalerShaders Shaders;
		}

		public class DispatchDescription
		{
			public ResourceView Color;

			public ResourceView Depth;

			public ResourceView MotionVectors;

			public ResourceView Exposure;

			public ResourceView Reactive;

			public ResourceView TransparencyAndComposition;

			public ResourceView Output;

			public Vector2 JitterOffset;

			public Vector2 MotionVectorScale;

			public Vector2Int RenderSize;

			public Vector2Int UpscaleSize;

			public bool EnableSharpening;

			public float Sharpness;

			public float FrameTimeDelta;

			public float PreExposure;

			public bool Reset;

			public float CameraNear;

			public float CameraFar;

			public float CameraFovAngleVertical;

			public float ViewSpaceToMetersFactor;

			public float VelocityFactor;

			public float ReactivenessScale;

			public float ShadingChangeScale;

			public float AccumulationAddedPerFrame;

			public float MinDisocclusionAccumulation;

			public DispatchFlags Flags;

			public bool UseTextureArrays;

			public bool IsSrgbInput;
		}

		public class GenerateReactiveDescription
		{
			public ResourceView ColorOpaqueOnly;

			public ResourceView ColorPreUpscale;

			public ResourceView OutReactive;

			public Vector2Int RenderSize;

			public float Scale;

			public float CutoffThreshold;

			public float BinaryValue;

			public GenerateReactiveFlags Flags;
		}

		[Flags]
		public enum GenerateReactiveFlags
		{
			ApplyTonemap = 1,
			ApplyInverseTonemap = 2,
			ApplyThreshold = 4,
			UseComponentsMax = 8
		}

		[Serializable]
		internal struct UpscalerConstants
		{
			public Vector2Int renderSize;

			public Vector2Int previousFrameRenderSize;

			public Vector2Int upscaleSize;

			public Vector2Int previousFrameUpscaleSize;

			public Vector2Int maxRenderSize;

			public Vector2Int maxUpscaleSize;

			public Vector4 deviceToViewDepth;

			public Vector2 jitterOffset;

			public Vector2 previousFrameJitterOffset;

			public Vector2 motionVectorScale;

			public Vector2 downscaleFactor;

			public Vector2 motionVectorJitterCancellation;

			public float tanHalfFOV;

			public float jitterPhaseCount;

			public float deltaTime;

			public float deltaPreExposure;

			public float viewSpaceToMetersFactor;

			public float frameIndex;

			public float velocityFactor;

			public float reactivenessScale;

			public float shadingChangeScale;

			public float accumulationAddedPerFrame;

			public float minDisocclusionAccumulation;
		}

		[Serializable]
		internal struct SpdConstants
		{
			public uint mips;

			public uint numWorkGroups;

			public uint workGroupOffsetX;

			public uint workGroupOffsetY;

			public uint renderSizeX;

			public uint renderSizeY;
		}

		[Serializable]
		internal struct GenerateReactiveConstants
		{
			public float scale;

			public float threshold;

			public float binaryValue;

			public uint flags;
		}

		[Serializable]
		internal struct GenerateReactiveConstants2
		{
			public float autoTcThreshold;

			public float autoTcScale;

			public float autoReactiveScale;

			public float autoReactiveMax;
		}

		[Serializable]
		internal struct RcasConstants
		{
			public readonly uint sharpness;

			public readonly uint halfSharp;

			public readonly uint dummy0;

			public readonly uint dummy1;

			public RcasConstants(uint sharpness, uint halfSharp)
			{
				this.sharpness = 0u;
				this.halfSharp = 0u;
				dummy0 = 0u;
				dummy1 = 0u;
			}
		}

		public static Fsr3UpscalerContext CreateContext(Vector2Int displaySize, Vector2Int maxRenderSize, Fsr3UpscalerShaders shaders, InitializationFlags flags = (InitializationFlags)0)
		{
			return null;
		}

		public static float GetUpscaleRatioFromQualityMode(QualityMode qualityMode)
		{
			return 0f;
		}

		public static void GetRenderResolutionFromQualityMode(out int renderWidth, out int renderHeight, int displayWidth, int displayHeight, QualityMode qualityMode)
		{
			renderWidth = default(int);
			renderHeight = default(int);
		}

		public static float GetMipmapBiasOffset(int renderWidth, int displayWidth)
		{
			return 0f;
		}

		public static int GetJitterPhaseCount(int renderWidth, int displayWidth)
		{
			return 0;
		}

		public static void GetJitterOffset(out float outX, out float outY, int index, int phaseCount)
		{
			outX = default(float);
			outY = default(float);
		}

		private static float Halton(int index, int @base)
		{
			return 0f;
		}

		public static float Lanczos2(float value)
		{
			return 0f;
		}
	}
}
