using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace TND.Upscaling.Framework
{
	public static class UpscalerUtils
	{
		private static readonly Dictionary<string, ShaderRef> ShaderRefs;

		private static readonly (GraphicsFormat, GraphicsFormat)[] GraphicsFormats;

		private static readonly HashSet<GraphicsFormat> SRGBFormats;

		public static bool TryLoadMaterial(string shaderResourceName, ref Material material, ref ShaderRef shaderRef)
		{
			return false;
		}

		public static void UnloadMaterial(ref Material material, ref ShaderRef shaderRef)
		{
		}

		[Obsolete]
		public static bool TryLoadMaterial(string shaderResourceName, ref Material material, ref Shader shader)
		{
			return false;
		}

		[Obsolete]
		public static void UnloadMaterial(ref Material material, ref Shader shader)
		{
		}

		public static void RunWithCommandBuffer(Action<CommandBuffer> action)
		{
		}

		public static ComputeBuffer CreateComputeBuffer<TElem>(string name, int count)
		{
			return null;
		}

		public static Texture2D CreateLookupTexture(string name, GraphicsFormat format, Color data)
		{
			return null;
		}

		public static Texture2DArray CreateLookupTextureArray(string name, GraphicsFormat format, int slices, Color data)
		{
			return null;
		}

		public static Texture2D CreateLookupTexture<T>(string name, in Vector2Int size, GraphicsFormat format, T[] data)
		{
			return null;
		}

		public static RenderTexture CreateRenderTexture(string name, in Vector2Int size, GraphicsFormat format, bool enableRandomWrite)
		{
			return null;
		}

		public static RenderTexture CreateRenderTexture(string name, in Vector2Int size, RenderTextureFormat format, bool enableRandomWrite)
		{
			return null;
		}

		public static RenderTexture CreateRenderTextureArray(string name, in Vector2Int size, int slices, GraphicsFormat format, bool enableRandomWrite)
		{
			return null;
		}

		public static RenderTexture CreateRenderTextureArray(string name, in Vector2Int size, int slices, RenderTextureFormat format, bool enableRandomWrite)
		{
			return null;
		}

		public static RenderTexture CreateRenderTextureMips(string name, in Vector2Int size, GraphicsFormat format, bool enableRandomWrite)
		{
			return null;
		}

		public static RenderTexture CreateRenderTextureMips(string name, in Vector2Int size, RenderTextureFormat format, bool enableRandomWrite)
		{
			return null;
		}

		public static void CreateRenderTextures(RenderTexture[] rtArray, string name, in Vector2Int size, GraphicsFormat format, bool enableRandomWrite)
		{
		}

		public static void CreateRenderTextures(RenderTexture[] rtArray, string name, in Vector2Int size, RenderTextureFormat format, bool enableRandomWrite)
		{
		}

		public static Texture CreateMatchingLookupTexture(this UpscalerInitParams initParams, string name, GraphicsFormat format, Color data)
		{
			return null;
		}

		public static RenderTexture CreateMatchingRenderTexture(this UpscalerInitParams initParams, string name, in Vector2Int size, GraphicsFormat format, bool enableRandomWrite)
		{
			return null;
		}

		public static RenderTexture CreateMatchingRenderTexture(this UpscalerInitParams initParams, string name, in Vector2Int size, RenderTextureFormat format, bool enableRandomWrite)
		{
			return null;
		}

		public static void GetMatchingTemporaryRT(this UpscalerInitParams initParams, CommandBuffer commandBuffer, int nameID, in Vector2Int size, GraphicsFormat format, bool enableRandomWrite)
		{
		}

		public static void DestroyTexture(ref Texture texture)
		{
		}

		public static void DestroyRenderTexture(ref RenderTexture renderTexture)
		{
		}

		public static void DestroyComputeBuffer(ref ComputeBuffer computeBuffer)
		{
		}

		public static void DestroyRenderTextures(RenderTexture[] rtArray)
		{
		}

		public static void DestroyObject(UnityEngine.Object obj)
		{
		}

		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat renderTextureFormat, bool isSRGB)
		{
			return default(GraphicsFormat);
		}

		public static bool IsSRGBFormat(GraphicsFormat graphicsFormat)
		{
			return false;
		}
	}
}
