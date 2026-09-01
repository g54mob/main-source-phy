using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Wrappers
{
	public class RTWrapper
	{
		private RenderTextureDescriptor _dscr;

		public RTHandle rt;

		public void HTextureAlloc(string name, Vector2 scaleFactor, GraphicsFormat graphicsFormat, int volumeDepthOrSlices = -1, int depthBufferBits = 0, TextureDimension textureDimension = TextureDimension.Unknown, bool useMipMap = false, bool autoGenerateMips = false, bool enableRandomWrite = true, bool useDynamicScale = true)
		{
		}

		public void HTextureAlloc(string name, ScaleFunc scaleFunc, GraphicsFormat graphicsFormat, int volumeDepthOrSlices = -1, int depthBufferBits = 0, TextureDimension textureDimension = TextureDimension.Unknown, bool useMipMap = false, bool autoGenerateMips = false, bool enableRandomWrite = true, bool useDynamicScale = true)
		{
		}

		public void HTextureAlloc(string name, int width, int height, GraphicsFormat graphicsFormat, int volumeDepthOrSlices = -1, int depthBufferBits = 0, TextureDimension textureDimension = TextureDimension.Unknown, bool useMipMap = false, bool autoGenerateMips = false, bool enableRandomWrite = true, bool useDynamicScale = true)
		{
		}

		public void HRelease()
		{
		}

		public void ReAllocateIfNeeded(string name, ref RenderTextureDescriptor inputDescriptor, int width = -1, int height = -1, int depth = -1, GraphicsFormat graphicsFormat = GraphicsFormat.None, TextureDimension dimension = TextureDimension.Unknown, bool useMipMap = false)
		{
		}
	}
}
