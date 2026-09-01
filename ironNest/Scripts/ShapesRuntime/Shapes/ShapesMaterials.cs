using UnityEngine;

namespace Shapes
{
	internal class ShapesMaterials
	{
		private const bool USE_INSTANCING = true;

		public const string SHAPES_SHADER_PATH_PREFIX = "Shapes/";

		private readonly Material[] materials;

		public Material this[ShapesBlendMode type] => null;

		public ShapesMaterials(string shaderName, params string[] keywords)
		{
		}

		public static string GetMaterialName(string shaderName, string blendModeSuffix, params string[] keywords)
		{
			return null;
		}

		public static void ApplyDefaultGlobalProperties(Material mat)
		{
		}

		private static Material CreateShapesMaterial(Shader shader, HideFlags hideFlags, params string[] keywords)
		{
			return null;
		}

		private static Material InitMaterial(string shaderName, string blendModeSuffix, params string[] keywords)
		{
			return null;
		}
	}
}
