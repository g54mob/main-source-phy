using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[CGDataInfo(0.98f, 0.5f, 0f, 1f)]
	public class CGVMesh : CGBounds
	{
		public Vector3[] Vertex;

		public Vector2[] UV;

		public Vector2[] UV2;

		public Vector3[] Normal;

		public Vector4[] Tangents;

		public CGVSubMesh[] SubMeshes;

		public override int Count => Vertex.Length;

		public bool HasUV => UV.Length != 0;

		public bool HasUV2 => UV2.Length != 0;

		public bool HasNormals => Normal.Length != 0;

		public bool HasTangents => Tangents.Length != 0;

		public int TriangleCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < SubMeshes.Length; i++)
				{
					num += SubMeshes[i].Triangles.Length;
				}
				return num / 3;
			}
		}

		public CGVMesh()
			: this(0)
		{
		}

		public CGVMesh(int vertexCount, bool addUV = false, bool addUV2 = false, bool addNormals = false, bool addTangents = false)
		{
			Vertex = new Vector3[vertexCount];
			UV = (addUV ? new Vector2[vertexCount] : new Vector2[0]);
			UV2 = (addUV2 ? new Vector2[vertexCount] : new Vector2[0]);
			Normal = (addNormals ? new Vector3[vertexCount] : new Vector3[0]);
			Tangents = (addTangents ? new Vector4[vertexCount] : new Vector4[0]);
			SubMeshes = new CGVSubMesh[0];
		}

		public CGVMesh(CGVolume volume)
			: this(volume.Vertex.Length)
		{
			Array.Copy(volume.Vertex, Vertex, volume.Vertex.Length);
		}

		public CGVMesh(CGVolume volume, IntRegion subset)
			: this((subset.LengthPositive + 1) * volume.CrossSize, addUV: false, addUV2: false, addNormals: true)
		{
			int sourceIndex = subset.Low * volume.CrossSize;
			Array.Copy(volume.Vertex, sourceIndex, Vertex, 0, Vertex.Length);
			Array.Copy(volume.VertexNormal, sourceIndex, Normal, 0, Normal.Length);
		}

		public CGVMesh(CGVMesh source)
			: base(source)
		{
			Vertex = (Vector3[])source.Vertex.Clone();
			UV = (Vector2[])source.UV.Clone();
			UV2 = (Vector2[])source.UV2.Clone();
			Normal = (Vector3[])source.Normal.Clone();
			Tangents = (Vector4[])source.Tangents.Clone();
			SubMeshes = new CGVSubMesh[source.SubMeshes.Length];
			for (int i = 0; i < source.SubMeshes.Length; i++)
			{
				SubMeshes[i] = new CGVSubMesh(source.SubMeshes[i]);
			}
		}

		public CGVMesh(CGMeshProperties meshProperties)
			: this(meshProperties.Mesh, meshProperties.Material, meshProperties.Matrix)
		{
		}

		public CGVMesh(Mesh source, Material[] materials, Matrix4x4 trsMatrix)
		{
			Name = source.name;
			Vertex = (Vector3[])source.vertices.Clone();
			Normal = (Vector3[])source.normals.Clone();
			Tangents = (Vector4[])source.tangents.Clone();
			UV = (Vector2[])source.uv.Clone();
			UV2 = (Vector2[])source.uv2.Clone();
			SubMeshes = new CGVSubMesh[source.subMeshCount];
			for (int i = 0; i < source.subMeshCount; i++)
			{
				SubMeshes[i] = new CGVSubMesh(source.GetTriangles(i), (materials.Length > i) ? materials[i] : null);
			}
			base.Bounds = source.bounds;
			if (!trsMatrix.isIdentity)
			{
				TRS(trsMatrix);
			}
		}

		public override T Clone<T>()
		{
			return new CGVMesh(this) as T;
		}

		public static CGVMesh Get(CGVMesh data, CGVolume source, bool addUV, bool reverseNormals)
		{
			return Get(data, source, new IntRegion(0, source.Count - 1), addUV, reverseNormals);
		}

		public static CGVMesh Get(CGVMesh data, CGVolume source, IntRegion subset, bool addUV, bool reverseNormals)
		{
			int sourceIndex = subset.Low * source.CrossSize;
			int num = (subset.LengthPositive + 1) * source.CrossSize;
			if (data == null)
			{
				data = new CGVMesh(num, addUV, addUV2: false, addNormals: true);
			}
			else
			{
				if (data.Vertex.Length != num)
				{
					data.Vertex = new Vector3[num];
				}
				if (data.Normal.Length != num)
				{
					data.Normal = new Vector3[num];
				}
				int num2 = (addUV ? source.Vertex.Length : 0);
				if (data.UV.Length != num2)
				{
					data.UV = new Vector2[num2];
				}
				if (data.UV2.Length != 0)
				{
					data.UV2 = new Vector2[0];
				}
				if (data.Tangents.Length != 0)
				{
					data.Tangents = new Vector4[0];
				}
			}
			Array.Copy(source.Vertex, sourceIndex, data.Vertex, 0, num);
			Array.Copy(source.VertexNormal, sourceIndex, data.Normal, 0, num);
			if (reverseNormals)
			{
				for (int i = 0; i < data.Normal.Length; i++)
				{
					data.Normal[i].x = 0f - data.Normal[i].x;
					data.Normal[i].y = 0f - data.Normal[i].y;
					data.Normal[i].z = 0f - data.Normal[i].z;
				}
			}
			return data;
		}

		public void SetSubMeshCount(int count)
		{
			Array.Resize(ref SubMeshes, count);
		}

		public void AddSubMesh(CGVSubMesh submesh = null)
		{
			SubMeshes = SubMeshes.Add(submesh);
		}

		public void MergeVMesh(CGVMesh source)
		{
			int count = Count;
			copyData(ref source.Vertex, ref Vertex, count, source.Count);
			MergeUVsNormalsAndTangents(source, count);
			for (int i = 0; i < source.SubMeshes.Length; i++)
			{
				GetMaterialSubMesh(source.SubMeshes[i].Material).Add(source.SubMeshes[i], count);
			}
			mBounds = null;
		}

		public void MergeVMesh(CGVMesh source, Matrix4x4 matrix)
		{
			int count = Count;
			Array.Resize(ref Vertex, Count + source.Count);
			int count2 = Count;
			for (int i = count; i < count2; i++)
			{
				Vertex[i] = matrix.MultiplyPoint3x4(source.Vertex[i - count]);
			}
			MergeUVsNormalsAndTangents(source, count);
			for (int j = 0; j < source.SubMeshes.Length; j++)
			{
				GetMaterialSubMesh(source.SubMeshes[j].Material).Add(source.SubMeshes[j], count);
			}
			mBounds = null;
		}

		public void MergeVMeshes(List<CGVMesh> vMeshes, int startIndex, int endIndex)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			Dictionary<Material, List<int[]>> dictionary = new Dictionary<Material, List<int[]>>();
			Dictionary<Material, int> dictionary2 = new Dictionary<Material, int>();
			for (int i = startIndex; i <= endIndex; i++)
			{
				CGVMesh cGVMesh = vMeshes[i];
				num += cGVMesh.Count;
				flag |= cGVMesh.HasNormals;
				flag2 |= cGVMesh.HasTangents;
				flag3 |= cGVMesh.HasUV;
				flag4 |= cGVMesh.HasUV2;
				for (int j = 0; j < cGVMesh.SubMeshes.Length; j++)
				{
					CGVSubMesh cGVSubMesh = cGVMesh.SubMeshes[j];
					if (!dictionary.ContainsKey(cGVSubMesh.Material))
					{
						dictionary[cGVSubMesh.Material] = new List<int[]>(1);
						dictionary2[cGVSubMesh.Material] = 0;
					}
					dictionary[cGVSubMesh.Material].Add(cGVSubMesh.Triangles);
				}
			}
			Vertex = new Vector3[num];
			if (flag)
			{
				Normal = new Vector3[num];
			}
			if (flag2)
			{
				Tangents = new Vector4[num];
			}
			if (flag3)
			{
				UV = new Vector2[num];
			}
			if (flag4)
			{
				UV2 = new Vector2[num];
			}
			foreach (KeyValuePair<Material, List<int[]>> item in dictionary)
			{
				List<int[]> value = item.Value;
				CGVSubMesh cGVSubMesh2 = new CGVSubMesh(item.Key);
				int num2 = 0;
				for (int k = 0; k < item.Value.Count; k++)
				{
					num2 += value[k].Length;
				}
				cGVSubMesh2.Triangles = new int[num2];
				AddSubMesh(cGVSubMesh2);
			}
			int num3 = 0;
			for (int l = startIndex; l <= endIndex; l++)
			{
				CGVMesh cGVMesh2 = vMeshes[l];
				Array.Copy(cGVMesh2.Vertex, 0, Vertex, num3, cGVMesh2.Vertex.Length);
				if (flag && cGVMesh2.HasNormals)
				{
					Array.Copy(cGVMesh2.Normal, 0, Normal, num3, cGVMesh2.Normal.Length);
				}
				if (flag2 && cGVMesh2.HasTangents)
				{
					Array.Copy(cGVMesh2.Tangents, 0, Tangents, num3, cGVMesh2.Tangents.Length);
				}
				if (flag3 && cGVMesh2.HasUV)
				{
					Array.Copy(cGVMesh2.UV, 0, UV, num3, cGVMesh2.UV.Length);
				}
				if (flag4 && cGVMesh2.HasUV2)
				{
					Array.Copy(cGVMesh2.UV2, 0, UV2, num3, cGVMesh2.UV2.Length);
				}
				for (int m = 0; m < cGVMesh2.SubMeshes.Length; m++)
				{
					CGVSubMesh obj = cGVMesh2.SubMeshes[m];
					Material material = obj.Material;
					int[] triangles = obj.Triangles;
					int num4 = triangles.Length;
					int[] triangles2 = GetMaterialSubMesh(material).Triangles;
					int num5 = dictionary2[material];
					if (num4 == 0)
					{
						continue;
					}
					if (num3 == 0)
					{
						Array.Copy(triangles, 0, triangles2, num5, num4);
					}
					else
					{
						for (int n = 0; n < num4; n++)
						{
							triangles2[num5 + n] = triangles[n] + num3;
						}
					}
					dictionary2[material] = num5 + num4;
				}
				num3 += cGVMesh2.Vertex.Length;
			}
		}

		private void MergeUVsNormalsAndTangents(CGVMesh source, int preMergeVertexCount)
		{
			int count = source.Count;
			if (count == 0)
			{
				return;
			}
			int num = preMergeVertexCount + count;
			if (HasUV || source.HasUV)
			{
				Vector2[] uV = UV;
				UV = new Vector2[num];
				if (HasUV)
				{
					Array.Copy(uV, UV, preMergeVertexCount);
				}
				if (source.HasUV)
				{
					Array.Copy(source.UV, 0, UV, preMergeVertexCount, count);
				}
			}
			if (HasUV2 || source.HasUV2)
			{
				Vector2[] uV2 = UV2;
				UV2 = new Vector2[num];
				if (HasUV2)
				{
					Array.Copy(uV2, UV2, preMergeVertexCount);
				}
				if (source.HasUV2)
				{
					Array.Copy(source.UV2, 0, UV2, preMergeVertexCount, count);
				}
			}
			if (HasNormals || source.HasNormals)
			{
				Vector3[] normal = Normal;
				Normal = new Vector3[num];
				if (HasNormals)
				{
					Array.Copy(normal, Normal, preMergeVertexCount);
				}
				if (source.HasNormals)
				{
					Array.Copy(source.Normal, 0, Normal, preMergeVertexCount, count);
				}
			}
			if (HasTangents || source.HasTangents)
			{
				Vector4[] tangents = Tangents;
				Tangents = new Vector4[num];
				if (HasTangents)
				{
					Array.Copy(tangents, Tangents, preMergeVertexCount);
				}
				if (source.HasTangents)
				{
					Array.Copy(source.Tangents, 0, Tangents, preMergeVertexCount, count);
				}
			}
		}

		public CGVSubMesh GetMaterialSubMesh(Material mat, bool createIfMissing = true)
		{
			for (int i = 0; i < SubMeshes.Length; i++)
			{
				if (SubMeshes[i].Material == mat)
				{
					return SubMeshes[i];
				}
			}
			if (createIfMissing)
			{
				CGVSubMesh cGVSubMesh = new CGVSubMesh(mat);
				AddSubMesh(cGVSubMesh);
				return cGVSubMesh;
			}
			return null;
		}

		public Mesh AsMesh()
		{
			Mesh msh = new Mesh();
			ToMesh(ref msh);
			return msh;
		}

		public void ToMesh(ref Mesh msh)
		{
			msh.vertices = Vertex;
			if (HasUV)
			{
				msh.uv = UV;
			}
			if (HasUV2)
			{
				msh.uv2 = UV2;
			}
			if (HasNormals)
			{
				msh.normals = Normal;
			}
			if (HasTangents)
			{
				msh.tangents = Tangents;
			}
			msh.subMeshCount = SubMeshes.Length;
			for (int i = 0; i < SubMeshes.Length; i++)
			{
				msh.SetTriangles(SubMeshes[i].Triangles, i);
			}
		}

		public Material[] GetMaterials()
		{
			List<Material> list = new List<Material>();
			for (int i = 0; i < SubMeshes.Length; i++)
			{
				list.Add(SubMeshes[i].Material);
			}
			return list.ToArray();
		}

		public override void RecalculateBounds()
		{
			if (Count == 0)
			{
				mBounds = new Bounds(Vector3.zero, Vector3.zero);
				return;
			}
			Bounds value = new Bounds(Vertex[0], Vector3.zero);
			int num = Vertex.Length;
			for (int i = 1; i < num; i++)
			{
				value.Encapsulate(Vertex[i]);
			}
			mBounds = value;
		}

		public void RecalculateUV2()
		{
			UV2 = CGUtility.CalculateUV2(UV);
		}

		public void TRS(Matrix4x4 matrix)
		{
			int count = Count;
			for (int i = 0; i < count; i++)
			{
				Vertex[i] = matrix.MultiplyPoint3x4(Vertex[i]);
			}
			count = Normal.Length;
			for (int j = 0; j < count; j++)
			{
				Normal[j] = matrix.MultiplyVector(Normal[j]);
			}
			count = Tangents.Length;
			for (int k = 0; k < count; k++)
			{
				Vector3 vector = matrix.MultiplyVector(Tangents[k]);
				Tangents[k].x = vector.x;
				Tangents[k].y = vector.y;
				Tangents[k].z = vector.z;
			}
			mBounds = null;
		}

		private void copyData<T>(ref T[] src, ref T[] dst, int currentSize, int extraSize)
		{
			if (extraSize != 0)
			{
				T[] sourceArray = dst;
				dst = new T[currentSize + extraSize];
				Array.Copy(sourceArray, dst, currentSize);
				Array.Copy(src, 0, dst, currentSize, extraSize);
			}
		}
	}
}
