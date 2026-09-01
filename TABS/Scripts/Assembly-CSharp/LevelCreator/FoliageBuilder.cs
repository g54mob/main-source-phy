using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class FoliageBuilder
	{
		public static float GetRandomValue(float x, float y, float z, float w)
		{
			int num = Perlin.perm[(uint)x % Perlin.perm.Length];
			int num2 = Perlin.perm[((uint)y + num) % Perlin.perm.Length];
			int num3 = Perlin.perm[((uint)z + num2) % Perlin.perm.Length];
			return (float)Perlin.perm[((uint)w + num3) % Perlin.perm.Length] / 255f;
		}

		public static float GetRandomValueExpensive(float x, float y, float z, float w)
		{
			int num = Perlin.perm[(uint)x % Perlin.perm.Length];
			int num2 = Perlin.perm[((uint)y + num) % Perlin.perm.Length];
			int num3 = Perlin.perm[((uint)z + num2) % Perlin.perm.Length];
			int num4 = Perlin.perm[((uint)w + num3) % Perlin.perm.Length];
			int num5 = Perlin.perm[((uint)x / 256 + num4) % Perlin.perm.Length];
			int num6 = Perlin.perm[((uint)y / 256 + num5) % Perlin.perm.Length];
			int num7 = Perlin.perm[((uint)z / 256 + num6) % Perlin.perm.Length];
			return (float)Perlin.perm[((uint)w / 256 + num7) % Perlin.perm.Length] / 255f;
		}

		public static float GetRandomValue(Vector3 v, float w)
		{
			return GetRandomValue(v.x, v.y, v.z, w);
		}

		public static float GetRandomValueExpensive(Vector3 v, float w)
		{
			return GetRandomValueExpensive(v.x, v.y, v.z, w);
		}

		public static void ForeachPlant(Level.FoliageChunk foliageChunk, Vector3Int chunkPosition, MeshData meshData, Action<Vector3> callback)
		{
			Vector3Int vector3Int = Vector3Int.Min(new Vector3Int(foliageChunk.densities.GetLength(2) - 1, foliageChunk.densities.GetLength(1) - 1, foliageChunk.densities.GetLength(0) - 1), new Vector3Int(Level.FoliageChunk.foliageBounds.max.x - 1 - Level.FoliageChunk.noOfCells.x * (chunkPosition.x / Level.VoxelChunk.noOfCells.x), Level.FoliageChunk.foliageBounds.max.y - 1 - Level.FoliageChunk.noOfCells.y * (chunkPosition.y / Level.VoxelChunk.noOfCells.y), Level.FoliageChunk.foliageBounds.max.z - 1 - Level.FoliageChunk.noOfCells.z * (chunkPosition.z / Level.VoxelChunk.noOfCells.z)));
			if (meshData.indices.Count <= 0)
			{
				return;
			}
			for (int i = 0; i + 2 < meshData.indices.Count; i += 3)
			{
				Vector3 position = meshData.vertices[meshData.indices[i]].position;
				Vector3 position2 = meshData.vertices[meshData.indices[i + 1]].position;
				Vector3 position3 = meshData.vertices[meshData.indices[i + 2]].position;
				Vector3 vector = position;
				Vector3 vector2 = Vector3.Cross(position2 - position, position3 - position);
				if (vector2.y > 0.75f * vector2.magnitude)
				{
					float num = foliageChunk.densities[Mathf.Clamp(Mathf.RoundToInt((float)Level.FoliageChunk.noOfCells.z * (vector.z / (float)Level.VoxelChunk.noOfCells.z)), 0, vector3Int.x), Mathf.Clamp(Mathf.RoundToInt((float)Level.FoliageChunk.noOfCells.y * (vector.y / (float)Level.VoxelChunk.noOfCells.y)), 0, vector3Int.y), Mathf.Clamp(Mathf.RoundToInt((float)Level.FoliageChunk.noOfCells.x * (vector.x / (float)Level.VoxelChunk.noOfCells.x)), 0, vector3Int.z)];
					if (num == 1f || (num > 0f && GetRandomValue(position, 7f) < num))
					{
						float randomValueExpensive = GetRandomValueExpensive(position, 0f);
						float randomValueExpensive2 = GetRandomValueExpensive(position, 1f);
						float randomValueExpensive3 = GetRandomValueExpensive(position, 2f);
						Vector3 obj = (position * randomValueExpensive + position2 * randomValueExpensive2 + position3 * randomValueExpensive3) / (randomValueExpensive + randomValueExpensive2 + randomValueExpensive3);
						callback(obj);
					}
				}
			}
		}

		public static int GetPlantSeedIndex(Vector3 position, int seedCount)
		{
			return Mathf.Min((int)(GetRandomValueExpensive(position, 4f) * (float)seedCount), seedCount - 1);
		}

		public static Quaternion GetPlantRotation(Vector3 position)
		{
			return Quaternion.Euler(0f, GetRandomValueExpensive(position, 6f) * 360f, 0f);
		}

		public static float GetPlantScale(Vector3 position, Vector2 scaleMultiplierMinMax)
		{
			return scaleMultiplierMinMax.x + (scaleMultiplierMinMax.y - scaleMultiplierMinMax.x) * GetRandomValueExpensive(position, 5f);
		}

		public static List<FoliageData> CreateFoliageData(DMEditorObjectTable editorObjectTable, SeedCollectionData[] seedCollectionData)
		{
			if (seedCollectionData == null)
			{
				return null;
			}
			List<FoliageData> list = new List<FoliageData>();
			foreach (SeedCollectionData seedCollectionData2 in seedCollectionData)
			{
				DMEditorObjectRow rowValue = editorObjectTable.GetRowValue(seedCollectionData2.editorObjectId);
				if (rowValue == null || rowValue.EditorObject == null)
				{
					throw new Exception("Missing foliage object: " + seedCollectionData2.editorObjectId);
				}
				GameObject editorObject = rowValue.EditorObject;
				list.Add(new FoliageData
				{
					ScaleMultiplierMinMax = seedCollectionData2.scaleMultiplierMinMax,
					sharedMesh = editorObject.GetComponentInChildren<MeshFilter>().sharedMesh,
					sharedMaterial = editorObject.GetComponentInChildren<MeshRenderer>().sharedMaterial
				});
			}
			return list;
		}
	}
}
