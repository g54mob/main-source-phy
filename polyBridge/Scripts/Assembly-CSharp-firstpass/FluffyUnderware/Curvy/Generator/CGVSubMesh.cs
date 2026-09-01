using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public class CGVSubMesh : CGData
	{
		public int[] Triangles;

		public Material Material;

		public override int Count => Triangles.Length;

		public CGVSubMesh(Material material = null)
		{
			Material = material;
			Triangles = new int[0];
		}

		public CGVSubMesh(int[] triangles, Material material = null)
		{
			Material = material;
			Triangles = triangles;
		}

		public CGVSubMesh(int triangleCount, Material material = null)
		{
			Material = material;
			Triangles = new int[triangleCount];
		}

		public CGVSubMesh(CGVSubMesh source)
		{
			Material = source.Material;
			Triangles = (int[])source.Triangles.Clone();
		}

		public override T Clone<T>()
		{
			return new CGVSubMesh(this) as T;
		}

		public static CGVSubMesh Get(CGVSubMesh data, int triangleCount, Material material = null)
		{
			if (data == null)
			{
				return new CGVSubMesh(triangleCount, material);
			}
			Array.Resize(ref data.Triangles, triangleCount);
			data.Material = material;
			return data;
		}

		public void ShiftIndices(int offset, int startIndex = 0)
		{
			for (int i = startIndex; i < Triangles.Length; i++)
			{
				Triangles[i] += offset;
			}
		}

		public void Add(CGVSubMesh other, int shiftIndexOffset = 0)
		{
			int num = Triangles.Length;
			int num2 = other.Triangles.Length;
			if (num2 != 0)
			{
				int[] triangles = Triangles;
				Triangles = new int[num + num2];
				Array.Copy(triangles, Triangles, num);
				Array.Copy(other.Triangles, 0, Triangles, num, num2);
				if (shiftIndexOffset != 0)
				{
					ShiftIndices(shiftIndexOffset, num);
				}
			}
		}
	}
}
