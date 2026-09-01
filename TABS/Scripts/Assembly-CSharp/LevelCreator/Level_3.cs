using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class Level_3
	{
		[Serializable]
		public struct Entity
		{
			public string objectTypeId;

			public Vector3 position;

			public Quaternion slope;

			public Quaternion rotation;

			public Vector3 scale;

			public string id;

			public string parentId;

			public List<string> customDataKeys;

			public List<string> customDataValues;
		}

		[Serializable]
		public struct CompressedChunk
		{
			public Vector3Int position;

			public string voxelDensities;

			public string materialDensities;

			public string foliageDensities;
		}

		public class DataSets
		{
			public const byte SingleValue = 0;

			public const byte MultiValue = 1;
		}

		public bool showWater = true;

		public float waterLevel;

		public int weatherIndex;

		public int musicIndex;

		public string presetName = "LevelPreset_Medieval";

		public Quaternion timeOfDay = Quaternion.Euler(80f, 0f, 0f);

		public Color sunColor = Color.white;

		public float sunIntensity = 0.8f;

		public List<Entity> entities = new List<Entity>();

		public float voxelDensityPrecision;

		public float materialDensityPrecision;

		public float foliageDensityPrecision;

		public List<CompressedChunk> compressedVoxelChunks = new List<CompressedChunk>();

		public const float defaultVoxelDensityPrecision = 255f;

		public const float defaultMaterialDensityPrecision = 255f;

		public const float defaultFoliageDensityPrecision = 255f;

		public static float[,,] DeserializeChunkDensities(string compressedDensities, Vector3Int noOfCells, float densityPrecision)
		{
			float[,,] array = new float[noOfCells.x + 1, noOfCells.y + 1, noOfCells.z + 1];
			if (compressedDensities != null)
			{
				byte[] array2 = Convert.FromBase64String(compressedDensities);
				int num = 0;
				bool flag = array2[num++] == 0;
				float num2 = (flag ? ((float)(int)array2[num++] / densityPrecision) : 0f);
				for (int i = 0; i <= noOfCells.z; i++)
				{
					for (int j = 0; j <= noOfCells.y; j++)
					{
						for (int k = 0; k <= noOfCells.x; k++)
						{
							array[i, j, k] = ((flag || k == noOfCells.x || j == noOfCells.y || i == noOfCells.z) ? num2 : ((float)(int)array2[num++] / densityPrecision));
						}
					}
				}
			}
			return array;
		}

		public Level_Latest Upgrade()
		{
			Level_Latest level_Latest = new Level_Latest
			{
				scene = new Level_Latest.Scene(),
				chunks = new Level_Latest.Chunks
				{
					voxelDensityPrecision = voxelDensityPrecision,
					materialDensityPrecision = materialDensityPrecision,
					foliageDensityPrecision = foliageDensityPrecision
				},
				settings = new Level_Latest.Settings
				{
					showWater = showWater,
					waterLevel = waterLevel,
					weatherIndex = weatherIndex,
					musicIndex = musicIndex,
					presetName = presetName,
					timeOfDay = timeOfDay,
					sunColor = sunColor,
					sunIntensity = sunIntensity
				}
			};
			foreach (Entity entity in entities)
			{
				level_Latest.scene.entities.Add(new Level_Latest.Entity
				{
					objectTypeId = entity.objectTypeId,
					position = entity.position,
					slope = entity.slope,
					rotation = entity.rotation,
					scale = entity.scale,
					id = entity.id,
					parentId = entity.parentId,
					customDataKeys = entity.customDataKeys,
					customDataValues = entity.customDataValues
				});
			}
			foreach (CompressedChunk compressedVoxelChunk in compressedVoxelChunks)
			{
				level_Latest.chunks.compressedVoxelChunks.Add(new Level_Latest.CompressedChunk
				{
					position = compressedVoxelChunk.position,
					voxelDensities = compressedVoxelChunk.voxelDensities,
					foliageDensities = compressedVoxelChunk.foliageDensities
				});
			}
			return level_Latest;
		}
	}
}
