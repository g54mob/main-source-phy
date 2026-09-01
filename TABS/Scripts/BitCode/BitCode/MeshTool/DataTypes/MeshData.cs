using System;
using UnityEngine;

namespace BitCode.MeshTool.DataTypes
{
	public struct MeshData
	{
		public Vector3[] Vertices;

		public SubmeshData[] Submeshes;

		public Vector3[] Normals;

		public Vector4[] Tangents;

		public Color[] VertexColors;

		public Vector2[] UV0;

		public Vector2[] UV1;

		public Vector2[] UV2;

		public Vector2[] UV3;

		public BoneWeightData[] BoneWeights;

		public BindposeData[] BindPoses;

		public BlendshapeFrameData[] BlendShapes;

		public string OverrideBoneName;

		public SkinQuality SkinningQuality;

		public int[] GetTriangles(int submeshIndex)
		{
			if (submeshIndex >= 0)
			{
				while (true)
				{
					int num = 1279830714;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x17BAA3BD)) % 4)
						{
						case 0u:
							break;
						case 3u:
						{
							int num3;
							int num4;
							if (submeshIndex < Submeshes.Length)
							{
								num3 = -1762876247;
								num4 = num3;
							}
							else
							{
								num3 = -1468451478;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -81879077);
							continue;
						}
						case 1u:
							return Submeshes[submeshIndex].TriangleList;
						default:
							goto end_IL_0004;
						}
						break;
					}
					continue;
					end_IL_0004:
					break;
				}
			}
			throw new IndexOutOfRangeException("Submesh index is not valid for this mesh.");
		}

		public bool IsSkinned()
		{
			if (BoneWeights != null)
			{
				while (true)
				{
					uint num;
					switch ((num = 1590101248u) % 3)
					{
					case 2u:
						continue;
					case 1u:
						return BindPoses != null;
					}
					break;
				}
			}
			return false;
		}

		public int VertexCount()
		{
			if (Vertices == null)
			{
				while (true)
				{
					uint num;
					switch ((num = 1024114088u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						throw new InvalidOperationException("Vertex array for this MeshData struct is not initialized.");
					}
					break;
				}
			}
			return Vertices.Length;
		}
	}
}
