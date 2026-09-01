using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class VolumeBrushes
	{
		public static Brush CreateBrush(VolumeBrushRow brushSettings)
		{
			if (!brushSettings.useTextures)
			{
				return CreateSphereBrush(brushSettings.size, brushSettings.randomness, brushSettings.densityOffset);
			}
			return CreateBrushFromTextures(brushSettings.size, brushSettings.randomness, brushSettings.xOffset, brushSettings.yOffset, brushSettings.zOffset, brushSettings.xTexture, brushSettings.yTexture, brushSettings.zTexture, brushSettings.xRotation, brushSettings.yRotation, brushSettings.zRotation, brushSettings.textureRandomRotation);
		}

		public static Brush CreateSDFBrush(Vector3Int size, float randomness, float densityOffset, Vector3 scale, Quaternion rotation, Func<Vector3, float> func)
		{
			Vector3 vector = new Vector3(size.x - 1, size.y - 1, size.z - 1);
			float[,,] array = new float[size.z, size.y, size.x];
			for (int i = 0; i < size.z; i++)
			{
				for (int j = 0; j < size.y; j++)
				{
					for (int k = 0; k < size.x; k++)
					{
						Vector3 vector2 = new Vector3((0f - vector.x + 2f * (float)k) / vector.x, (0f - vector.y + 2f * (float)j) / vector.y, (0f - vector.z + 2f * (float)i) / vector.z);
						Vector3 vector3 = Vector3.Scale(scale, rotation * vector2);
						float num = Mathf.Clamp01(Perlin.Noise(vector3 * 3f) * randomness + 0.5f - func(vector3));
						array[i, j, k] = Mathf.Max(num - densityOffset, 0f);
					}
				}
			}
			return new Brush
			{
				Field = array,
				Pivot = vector / 2f,
				UsingTextures = false
			};
		}

		public static Brush CreateSphereBrush(int size, float randomness, float densityOffset)
		{
			float num = (float)size / 2f;
			float[,,] array = new float[size, size, size];
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					for (int k = 0; k < size; k++)
					{
						float num2 = Mathf.Clamp01(UnityEngine.Random.Range((0f - randomness) / 2f, randomness / 2f) + 1f - Mathf.Pow(new Vector3((float)k - num, (float)j - num, (float)i - num).magnitude / (num + 1f), 3f));
						array[i, j, k] = UnityEngine.Random.Range(0f, randomness) + Mathf.Max(num2 - densityOffset, 0f);
					}
				}
			}
			return new Brush
			{
				Field = array,
				Pivot = new Vector3(num, num, num),
				UsingTextures = false
			};
		}

		public static Brush CreateCylinderBrush(int size, int height, float randomness, float densityOffset)
		{
			float num = (float)size / 2f;
			float[,,] array = new float[size, height, size];
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < height; j++)
				{
					for (int k = 0; k < size; k++)
					{
						float num2 = Mathf.Clamp01(UnityEngine.Random.Range((0f - randomness) / 2f, randomness / 2f) + 1f - Mathf.Pow(new Vector2((float)k - num, (float)i - num).magnitude / (num + 1f), 3f));
						array[i, j, k] = UnityEngine.Random.Range(0f, randomness) + Mathf.Max(num2 - densityOffset, 0f);
					}
				}
			}
			return new Brush
			{
				Field = array,
				Pivot = new Vector3(num, (float)height / 2f, num),
				UsingTextures = false
			};
		}

		public static Brush CreateConeBrush(int size, int height, float randomness, float densityOffset)
		{
			float num = (float)size / 2f;
			float[,,] array = new float[size, height, size];
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < height; j++)
				{
					for (int k = 0; k < size; k++)
					{
						float num2 = Mathf.Clamp01(UnityEngine.Random.Range((0f - randomness) / 2f, randomness / 2f) + 1f - Mathf.Pow(new Vector2((float)k - num, (float)i - num).magnitude / (num + 1f), 3f) - 0.5f * (float)(height - j) / (float)height);
						array[i, j, k] = UnityEngine.Random.Range(0f, randomness) + Mathf.Max(num2 - densityOffset, 0f);
					}
				}
			}
			return new Brush
			{
				Field = array,
				Pivot = new Vector3(num, (float)height / 2f, num),
				UsingTextures = false
			};
		}

		public static Brush CreateBrushFromTextures(int size, float randomness, float XOffset, float YOffset, float ZOffset, Texture2D X, Texture2D Y, Texture2D Z, float XRotation, float YRotation, float ZRotation, bool randomRotation)
		{
			float num = (float)size / 2f;
			float rotation = (randomRotation ? UnityEngine.Random.Range(0f, 360f) : XRotation);
			float rotation2 = (randomRotation ? UnityEngine.Random.Range(0f, 360f) : YRotation);
			float rotation3 = (randomRotation ? UnityEngine.Random.Range(0f, 360f) : ZRotation);
			Vector2 mid = new Vector2(num, num);
			float[,,] array = new float[size, size, size];
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					for (int k = 0; k < size; k++)
					{
						Vector2 vector = rotateUV(new Vector2(k, j), rotation, mid);
						Vector2 vector2 = rotateUV(new Vector2(k, i), rotation2, mid);
						Vector2 vector3 = rotateUV(new Vector2(i, j), rotation3, mid);
						Color color = ((X == null) ? Color.white : X.GetPixel((int)vector.x, (int)vector.y));
						Color color2 = ((Y == null) ? Color.white : Y.GetPixel((int)vector2.x, (int)vector2.y));
						Color color3 = ((Z == null) ? Color.white : Z.GetPixel((int)vector3.x, (int)vector3.y));
						float a = Mathf.Clamp01((color.grayscale - XOffset) * (color2.grayscale - YOffset) * (color3.grayscale - ZOffset));
						array[i, j, k] = UnityEngine.Random.Range(0f, randomness) + Mathf.Max(0f, Mathf.Max(a, 0f));
					}
				}
			}
			return new Brush
			{
				Field = array,
				Pivot = new Vector3(num, num, num),
				UsingTextures = true
			};
		}

		public static Vector2 rotateUV(Vector2 uv, float rotation, Vector2 mid)
		{
			return new Vector2(Mathf.Cos(rotation) * (uv.x - mid.x) + Mathf.Sin(rotation) * (uv.y - mid.y) + mid.x, Mathf.Cos(rotation) * (uv.y - mid.y) - Mathf.Sin(rotation) * (uv.x - mid.x) + mid.y);
		}

		public static Mesh GenerateBrushPreview(Brush brush, MeshData meshData = null)
		{
			if (meshData == null)
			{
				meshData = GenerateBrushMeshData(brush);
			}
			Mesh mesh = new Mesh();
			List<Vector3> verts = new List<Vector3>();
			List<Vector3> normals = new List<Vector3>();
			_ = brush.Pivot;
			meshData.vertices.ForEach(delegate(MeshData.Vertex v)
			{
				verts.Add(v.position - brush.Pivot);
				normals.Add(v.normal);
			});
			mesh.SetVertices(verts);
			mesh.SetNormals(normals);
			mesh.SetIndices(meshData.indices.ToArray(), MeshTopology.Triangles, 0);
			return mesh;
		}

		public static MeshData GenerateBrushMeshData(Brush brush)
		{
			Vector3Int vector3Int = brush.Size + new Vector3Int(2, 2, 2);
			float[,,] array = new float[vector3Int.z, vector3Int.y, vector3Int.x];
			for (int i = 1; i < vector3Int.z - 1; i++)
			{
				for (int j = 1; j < vector3Int.y - 1; j++)
				{
					for (int k = 1; k < vector3Int.x - 1; k++)
					{
						array[i, j, k] = brush.Field[i - 1, j - 1, k - 1];
					}
				}
			}
			MeshData meshData = new MeshData();
			MeshBuilder.BuildMeshData(meshData, array, null, Vector3Int.zero);
			return meshData;
		}

		private static float GetDensity(Brush brush, Vector3Int position)
		{
			if (position.x < 0 || position.x >= brush.Field.GetLength(2) || position.y < 0 || position.y >= brush.Field.GetLength(1) || position.z < 0 || position.z >= brush.Field.GetLength(0))
			{
				return 0f;
			}
			return brush.Field[position.z, position.y, position.x];
		}

		private static float GetLerpedDensity(Brush brush, Vector3 position)
		{
			int num = (int)position.x;
			float t = position.x - (float)num;
			int num2 = (int)position.y;
			float t2 = position.y - (float)num2;
			int num3 = (int)position.z;
			float t3 = position.z - (float)num3;
			return Mathf.Lerp(Mathf.Lerp(Mathf.Lerp(GetDensity(brush, new Vector3Int(num, num2, num3)), GetDensity(brush, new Vector3Int(num + 1, num2, num3)), t), Mathf.Lerp(GetDensity(brush, new Vector3Int(num, num2 + 1, num3)), GetDensity(brush, new Vector3Int(num + 1, num2 + 1, num3)), t), t2), Mathf.Lerp(Mathf.Lerp(GetDensity(brush, new Vector3Int(num, num2, num3 + 1)), GetDensity(brush, new Vector3Int(num + 1, num2, num3 + 1)), t), Mathf.Lerp(GetDensity(brush, new Vector3Int(num, num2 + 1, num3 + 1)), GetDensity(brush, new Vector3Int(num + 1, num2 + 1, num3 + 1)), t), t2), t3);
		}

		public static Brush CreateTransformedBrush(Vector3Int newSize, Brush brush, Matrix4x4 transform)
		{
			float[,,] array = new float[newSize.z, newSize.y, newSize.x];
			for (int i = 0; i < newSize.z; i++)
			{
				for (int j = 0; j < newSize.y; j++)
				{
					for (int k = 0; k < newSize.x; k++)
					{
						array[i, j, k] = GetLerpedDensity(brush, transform.MultiplyPoint3x4(new Vector3(k, j, i)));
					}
				}
			}
			return new Brush
			{
				Field = array,
				Pivot = new Vector3((float)newSize.x / 2f, (float)newSize.y / 2f, (float)newSize.z / 2f),
				UsingTextures = brush.UsingTextures
			};
		}

		public static Brush CreateRotatedBrush(Brush brush, Vector3 angles)
		{
			return CreateTransformedBrush(brush.Size, brush, Matrix4x4.Translate(brush.Pivot) * Matrix4x4.Rotate(Quaternion.Euler(angles)) * Matrix4x4.Translate(-brush.Pivot));
		}

		public static Brush CreateScaledBrush(Brush brush, Vector3 scale)
		{
			Vector3Int newSize = Vector3Int.RoundToInt(new Vector3(brush.Size.x, brush.Size.y, brush.Size.z) * Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z)));
			Vector3 vector = new Vector3((float)newSize.x / 2f, (float)newSize.y / 2f, (float)newSize.z / 2f);
			if (scale.x == 0f)
			{
				scale.x = 0.01f;
			}
			if (scale.y == 0f)
			{
				scale.y = 0.01f;
			}
			if (scale.z == 0f)
			{
				scale.z = 0.01f;
			}
			return CreateTransformedBrush(newSize, brush, Matrix4x4.Translate(brush.Pivot) * Matrix4x4.Scale(new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z)) * Matrix4x4.Translate(-vector));
		}

		public static Brush CreateScaledAndRotatedBrush(Brush brush, Vector3 scale, Vector3 angles)
		{
			Vector3Int newSize = Vector3Int.RoundToInt(new Vector3(brush.Size.x, brush.Size.y, brush.Size.z) * Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z)));
			Vector3 vector = new Vector3((float)newSize.x / 2f, (float)newSize.y / 2f, (float)newSize.z / 2f);
			if (scale.x == 0f)
			{
				scale.x = 0.01f;
			}
			if (scale.y == 0f)
			{
				scale.y = 0.01f;
			}
			if (scale.z == 0f)
			{
				scale.z = 0.01f;
			}
			return CreateTransformedBrush(newSize, brush, Matrix4x4.Translate(brush.Pivot) * Matrix4x4.Rotate(Quaternion.Euler(angles)) * Matrix4x4.Scale(new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z)) * Matrix4x4.Translate(-vector));
		}
	}
}
