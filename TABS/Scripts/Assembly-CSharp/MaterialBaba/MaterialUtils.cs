using UnityEngine;

namespace MaterialBaba
{
	public static class MaterialUtils
	{
		public static int GetHashCode(Mesh mesh, Material[] materials)
		{
			int num = 17;
			num = (num * 397) ^ mesh.GetHashCode();
			foreach (Material material in materials)
			{
				if (IsMaterialValid(material))
				{
					num = (num * 397) ^ material.GetHashCode();
				}
			}
			return num;
		}

		public static int GetHashCode(Material material)
		{
			return 0x1A5D ^ material.GetHashCode();
		}

		public static bool IsMaterialValid(Material mat)
		{
			if (mat == null)
			{
				return false;
			}
			if (mat.shader.name != "Standard")
			{
				return false;
			}
			if (mat.GetFloat("_Mode") != 0f)
			{
				return false;
			}
			if (mat.GetTexture("_MainTex") != null)
			{
				return false;
			}
			if (mat.GetTexture("_MetallicGlossMap") != null)
			{
				return false;
			}
			if (mat.GetTexture("_BumpMap") != null)
			{
				return false;
			}
			return true;
		}
	}
}
