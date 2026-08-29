using UnityEngine;

namespace HDRPVolumePostProcessPack
{
	public static class TextureTool
	{
		public static Texture2D CreateSimpleNoiseTexture(int size, float noiseScale = 1f, TextureFormat format = TextureFormat.Alpha8)
		{
			ComputeShader obj = (ComputeShader)Resources.Load("ComputeShader/PerlinNoise");
			int kernelIndex = obj.FindKernel("PerlinNoise");
			RenderTexture temporary = RenderTexture.GetTemporary(size, size, 0, RenderTextureFormat.ARGB32);
			temporary.enableRandomWrite = true;
			temporary.Create();
			RenderTexture active = RenderTexture.active;
			obj.SetTexture(kernelIndex, "Result", temporary);
			obj.SetInt("Size", size);
			obj.SetFloat("Scale", noiseScale);
			obj.Dispatch(kernelIndex, size / 8, size / 8, 1);
			RenderTexture.active = temporary;
			Texture2D texture2D = CreateClearTexture(size, size, TextureFormat.ARGB32);
			texture2D.ReadPixels(new Rect(0f, 0f, texture2D.width, texture2D.height), 0, 0, recalculateMipMaps: false);
			texture2D.Apply();
			RenderTexture.active = active;
			RenderTexture.ReleaseTemporary(temporary);
			return texture2D;
		}

		public static Texture2D CreateClearTexture(int width, int height, TextureFormat format)
		{
			return CreateFilledTexture(width, height, format, Color.clear);
		}

		public static Texture2D CreateFilledTexture(int width, int height, TextureFormat format, Color fillColor)
		{
			Texture2D texture2D = new Texture2D(width, height, format, mipChain: false);
			Color32[] array = new Color32[width * height];
			int num = 0;
			Color32[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = ref array2[i];
				array[num] = fillColor;
				num++;
			}
			texture2D.SetPixels32(array);
			texture2D.Apply();
			return texture2D;
		}
	}
}
