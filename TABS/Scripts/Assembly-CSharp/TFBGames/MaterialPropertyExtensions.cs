using UnityEngine;

namespace TFBGames
{
	public static class MaterialPropertyExtensions
	{
		private const string ShaderRenderTypeTag = "RenderType";

		private const string ShaderTransparentRenderTypeResult = "Transparent";

		private const string EmissionKeyword = "_EMISSION";

		private static int ColorShaderPropertyId = Shader.PropertyToID("_Color");

		private static int EmissionShaderPropertyId = Shader.PropertyToID("_EmissionColor");

		public static Color SafeColor(this Material material)
		{
			if (material.IsKeywordEnabled("_EMISSION"))
			{
				return material.GetColor(EmissionShaderPropertyId);
			}
			if (material.HasProperty(ColorShaderPropertyId))
			{
				return material.color;
			}
			Debug.LogError("Material does not have either an emission color or a normal color.This means the material is using an unexpected shader in the incorrect way.Investigate the shader " + material.shader.name + " on the material " + material.name, material.shader);
			return Color.black;
		}

		public static bool IsMaterialTransparent(this Material material)
		{
			return material.GetTag("RenderType", searchFallbacks: true, string.Empty) == "Transparent";
		}
	}
}
