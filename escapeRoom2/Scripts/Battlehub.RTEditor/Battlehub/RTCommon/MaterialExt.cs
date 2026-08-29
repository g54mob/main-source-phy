using UnityEngine;

namespace Battlehub.RTCommon
{
	public static class MaterialExt
	{
		public static int MainTexturePropertyID = Shader.PropertyToID("_MainTex");

		public static int NormalTextureProperyID = Shader.PropertyToID("_BumpMap");

		public static int ColorPropertyID = Shader.PropertyToID("_Color");

		public static Texture MainTexture(this Material material)
		{
			if (material.HasProperty(RenderPipelineInfo.MainTexturePropertyID))
			{
				return material.GetTexture(RenderPipelineInfo.MainTexturePropertyID);
			}
			if (material.HasProperty(MainTexturePropertyID))
			{
				return material.GetTexture(MainTexturePropertyID);
			}
			return null;
		}

		public static void MainTexture(this Material material, Texture texture)
		{
			if (material.HasProperty(RenderPipelineInfo.MainTexturePropertyID))
			{
				material.SetTexture(RenderPipelineInfo.MainTexturePropertyID, texture);
			}
			else if (material.HasProperty(MainTexturePropertyID))
			{
				material.SetTexture(MainTexturePropertyID, texture);
			}
		}

		public static Vector2 MainTextureScale(this Material material)
		{
			if (material.HasProperty(RenderPipelineInfo.MainTexturePropertyID))
			{
				return material.GetTextureScale(RenderPipelineInfo.MainTexturePropertyID);
			}
			if (material.HasProperty(MainTexturePropertyID))
			{
				return material.GetTextureScale(MainTexturePropertyID);
			}
			return Vector2.one;
		}

		public static void MainTextureScale(this Material material, Vector2 scale)
		{
			if (material.HasProperty(RenderPipelineInfo.MainTexturePropertyID))
			{
				material.SetTextureScale(RenderPipelineInfo.MainTexturePropertyID, scale);
			}
			else if (material.HasProperty(MainTexturePropertyID))
			{
				material.SetTextureScale(MainTexturePropertyID, scale);
			}
		}

		public static Vector2 MainTextureOffset(this Material material)
		{
			if (material.HasProperty(RenderPipelineInfo.MainTexturePropertyID))
			{
				return material.GetTextureOffset(RenderPipelineInfo.MainTexturePropertyID);
			}
			if (material.HasProperty(MainTexturePropertyID))
			{
				return material.GetTextureOffset(MainTexturePropertyID);
			}
			return Vector2.zero;
		}

		public static void MainTextureOffset(this Material material, Vector2 offset)
		{
			if (material.HasProperty(RenderPipelineInfo.MainTexturePropertyID))
			{
				material.SetTextureOffset(RenderPipelineInfo.MainTexturePropertyID, offset);
			}
			else if (material.HasProperty(MainTexturePropertyID))
			{
				material.SetTextureOffset(MainTexturePropertyID, offset);
			}
		}

		public static Texture NormalTexture(this Material material)
		{
			if (material.HasProperty(NormalTextureProperyID))
			{
				return material.GetTexture(NormalTextureProperyID);
			}
			return null;
		}

		public static void NormalTexture(this Material material, Texture texture, float smoothness = -1f)
		{
			if (material.HasProperty(NormalTextureProperyID))
			{
				material.EnableKeyword("_NORMALMAP");
				material.SetTexture(NormalTextureProperyID, texture);
				if (smoothness != -1f && material.HasProperty("_Smoothness"))
				{
					material.SetFloat("_Smoothness", smoothness);
				}
			}
		}

		public static Color Color(this Material material)
		{
			if (material.HasProperty(RenderPipelineInfo.ColorPropertyID))
			{
				return material.GetColor(RenderPipelineInfo.ColorPropertyID);
			}
			if (material.HasProperty(ColorPropertyID))
			{
				return material.GetColor(ColorPropertyID);
			}
			return UnityEngine.Color.white;
		}

		public static void Color(this Material material, Color color)
		{
			if (material.HasProperty(RenderPipelineInfo.ColorPropertyID))
			{
				material.SetColor(RenderPipelineInfo.ColorPropertyID, color);
			}
			else if (material.HasProperty(ColorPropertyID))
			{
				material.SetColor(ColorPropertyID, color);
			}
		}
	}
}
