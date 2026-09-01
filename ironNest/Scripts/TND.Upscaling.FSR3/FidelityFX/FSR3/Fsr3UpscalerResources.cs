using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace FidelityFX.FSR3
{
	internal class Fsr3UpscalerResources
	{
		public Texture2D LanczosLut;

		public Texture2D DefaultExposure;

		public Texture2D DefaultReactive;

		public RenderTexture SpdAtomicCounter;

		public RenderTexture SpdMips;

		public RenderTexture DilatedVelocity;

		public RenderTexture DilatedDepth;

		public RenderTexture ReconstructedPrevNearestDepth;

		public RenderTexture FrameInfo;

		public readonly RenderTexture[] Accumulation;

		public readonly RenderTexture[] Luma;

		public readonly RenderTexture[] InternalUpscaled;

		public readonly RenderTexture[] LumaHistory;

		public void Create(Fsr3Upscaler.ContextDescription contextDescription)
		{
		}

		public static void CreateAliasableResources(CommandBuffer commandBuffer, Fsr3Upscaler.ContextDescription contextDescription, Fsr3Upscaler.DispatchDescription dispatchParams)
		{
		}

		public static void DestroyAliasableResources(CommandBuffer commandBuffer)
		{
		}

		private static void CreateDoubleBufferedResource(RenderTexture[] resource, string name, Vector2Int size, GraphicsFormat format)
		{
		}

		public void Destroy()
		{
		}

		private static void DestroyResource(ref Texture2D resource)
		{
		}

		private static void DestroyResource(ref RenderTexture resource)
		{
		}

		private static void DestroyResource(RenderTexture[] resource)
		{
		}
	}
}
