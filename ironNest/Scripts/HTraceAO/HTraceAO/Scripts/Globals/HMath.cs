using UnityEngine;

namespace HTraceAO.Scripts.Globals
{
	public static class HMath
	{
		private static Vector2 RemapVoxelsCoeff;

		public static float Remap(float input, float oldLow, float oldHigh, float newLow, float newHigh)
		{
			return 0f;
		}

		public static Vector2 ThicknessBias(float baseThickness, Camera camera)
		{
			return default(Vector2);
		}

		public static Vector2Int DepthResolutionFunc(Vector2Int size)
		{
			return default(Vector2Int);
		}

		public static int DepthResolutionFunc(int res)
		{
			return 0;
		}

		public static Vector4 ComputeViewportScaleAndLimit(Vector2Int viewportSize, Vector2Int bufferSize)
		{
			return default(Vector4);
		}

		public static float PixelSpreadTangent(float Fov, int Width, int Height)
		{
			return 0f;
		}

		public static float CalculateVoxelSizeInCM_UI(int bounds, float density)
		{
			return 0f;
		}

		public static float TexturesSizeInMB_UI(int voxelBounds, float density, bool overrideGroundEnable, int GroundLevel)
		{
			return 0f;
		}

		public static float TexturesSizeInMB_UI(Vector3Int voxelsRelosution)
		{
			return 0f;
		}

		public static Vector3Int CalculateVoxelResolution_UI(int voxelBounds, float density, bool overrideGroundEnable, int GroundLevel)
		{
			return default(Vector3Int);
		}

		public static Vector3 Truncate(this Vector3 input, int digits)
		{
			return default(Vector3);
		}

		public static Vector3 Ceil(this Vector3 input, int digits)
		{
			return default(Vector3);
		}

		public static float RoundTail(this float value, int digits)
		{
			return 0f;
		}

		public static float RoundToCeilTail(this float value, int digits)
		{
			return 0f;
		}

		public static Vector2Int CalculateDepthPyramidResolution(Vector2Int screenResolution, int lowestMipLevel)
		{
			return default(Vector2Int);
		}

		public static int CalculateStepCountSSGI(float giRadius, float giAccuracy)
		{
			return 0;
		}

		private static int DevisionBy4(int value)
		{
			return 0;
		}

		private static float ComputeViewportScale(int viewportSize, int bufferSize)
		{
			return 0f;
		}

		private static float ComputeViewportLimit(int viewportSize, int bufferSize)
		{
			return 0f;
		}
	}
}
