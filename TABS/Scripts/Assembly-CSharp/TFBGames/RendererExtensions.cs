using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	public static class RendererExtensions
	{
		private static Dictionary<int, Material[]> materialArrays = new Dictionary<int, Material[]>();

		private static List<Material> materials = new List<Material>();

		public static Material[] GetSharedMaterialsNonAlloc(this Renderer renderer)
		{
			renderer.GetSharedMaterials(materials);
			return GetMaterialArrayNonAlloc(materials);
		}

		public static Material[] GetMaterialsNonAlloc(this Renderer renderer)
		{
			renderer.GetMaterials(materials);
			return GetMaterialArrayNonAlloc(materials);
		}

		private static Material[] GetMaterialArrayNonAlloc(List<Material> matList)
		{
			int count = matList.Count;
			if (!materialArrays.ContainsKey(count))
			{
				materialArrays.Add(count, new Material[count]);
			}
			return GetCachedMaterialArray(materialArrays[count], matList);
		}

		private static Material[] GetCachedMaterialArray(Material[] matArray, List<Material> matList)
		{
			int num = matArray.Length;
			for (int i = 0; i < num; i++)
			{
				matArray[i] = matList[i];
			}
			return matArray;
		}
	}
}
