using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace HTraceAO.Scripts.Extensions
{
	public class ExtensionsURP
	{
		private static RenderTextureDescriptor _dscr;

		public static void UseTexture(IUnsafeRenderGraphBuilder builder, RenderGraph renderGraph, RTHandle targetTexture, ref TextureHandle passTextureHandle, AccessFlags accessFlags = AccessFlags.ReadWrite)
		{
		}

		public static void UseTexture(IRasterRenderGraphBuilder builder, RenderGraph renderGraph, RTHandle targetTexture, ref TextureHandle passTextureHandle, AccessFlags accessFlags = AccessFlags.ReadWrite)
		{
		}

		public static void ReAllocateIfNeeded(string name, ref RTHandle rtHandle, ref RenderTextureDescriptor inputDescriptor, int width = -1, int height = -1, GraphicsFormat graphicsFormat = GraphicsFormat.None, TextureDimension dimension = TextureDimension.Unknown, bool useMipMap = false)
		{
		}
	}
}
