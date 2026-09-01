using UnityEngine;

namespace TND.Upscaling.Framework
{
	public struct UpscalerInitParams
	{
		public Camera camera;

		public bool useTextureArrays;

		public int numTextureSlices;

		public Vector2Int maxRenderSize;

		public Vector2Int upscaleSize;

		public bool enableHDR;

		public bool invertedDepth;

		public bool highResMotionVectors;

		public bool jitteredMotionVectors;
	}
}
