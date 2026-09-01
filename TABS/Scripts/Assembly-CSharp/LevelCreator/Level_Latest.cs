using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class Level_Latest
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

			public float heightOffset;
		}

		[Serializable]
		public class Scene
		{
			public List<Entity> entities = new List<Entity>();
		}

		[Serializable]
		public struct CompressedChunk
		{
			public Vector3Int position;

			public string voxelDensities;

			public string materialDensities;

			public string foliageDensities;
		}

		[Serializable]
		public class Chunks
		{
			public const float defaultVoxelDensityPrecision = 255f;

			public const float defaultMaterialDensityPrecision = 255f;

			public const float defaultFoliageDensityPrecision = 255f;

			public float voxelDensityPrecision;

			public float materialDensityPrecision;

			public float foliageDensityPrecision;

			public List<CompressedChunk> compressedVoxelChunks = new List<CompressedChunk>();
		}

		[Serializable]
		public class Settings
		{
			public bool showWater = true;

			public float waterLevel;

			public int weatherIndex;

			public int musicIndex;

			public string presetName = "LevelPreset_Medieval";

			public Quaternion timeOfDay = Quaternion.Euler(80f, 0f, 0f);

			public Color sunColor = Color.white;

			public float sunIntensity = 0.8f;

			public Color ambientSkyColor = Color.white;

			public Color ambientEquatorColor = Color.white;

			public Color ambientGroundColor = Color.white;

			public float skyboxDayBlend;

			public float skyboxNightBlend;
		}

		public class DataSets
		{
			public const byte SingleValue = 0;

			public const byte MultiValue = 1;
		}

		public Scene scene;

		public Chunks chunks;

		public Settings settings;

		public static string SerializeVoxelDensities(Level.VoxelChunk voxelChunk, float densityPrecision)
		{
			List<byte> list = new List<byte>();
			bool flag = true;
			float num = voxelChunk.densities[0, 0, 0];
			float[,,] densities = voxelChunk.densities;
			foreach (float num2 in densities)
			{
				flag = flag && num2 == num;
			}
			if (flag)
			{
				list.Add(0);
				list.Add((byte)Mathf.Clamp01(num * densityPrecision));
			}
			else
			{
				byte[,,] array = new byte[voxelChunk.densities.GetLength(2), voxelChunk.densities.GetLength(1), voxelChunk.densities.GetLength(0)];
				for (int l = 0; l < array.GetLength(0); l++)
				{
					for (int m = 0; m < array.GetLength(1); m++)
					{
						for (int n = 0; n < array.GetLength(2); n++)
						{
							array[l, m, n] = 0;
						}
					}
				}
				for (int num3 = 0; num3 < voxelChunk.densities.GetLength(0) - 1; num3++)
				{
					for (int num4 = 0; num4 < voxelChunk.densities.GetLength(1) - 1; num4++)
					{
						for (int num5 = 0; num5 < voxelChunk.densities.GetLength(2) - 1; num5++)
						{
							byte b = (byte)((((double)voxelChunk.densities[num3 + 1, num4, num5] < 0.5) ? 1 : 2) | (((double)voxelChunk.densities[num3 + 1, num4, num5 + 1] < 0.5) ? 1 : 2) | (((double)voxelChunk.densities[num3, num4, num5 + 1] < 0.5) ? 1 : 2) | (((double)voxelChunk.densities[num3, num4, num5] < 0.5) ? 1 : 2) | (((double)voxelChunk.densities[num3 + 1, num4 + 1, num5] < 0.5) ? 1 : 2) | (((double)voxelChunk.densities[num3 + 1, num4 + 1, num5 + 1] < 0.5) ? 1 : 2) | (((double)voxelChunk.densities[num3, num4 + 1, num5 + 1] < 0.5) ? 1 : 2) | (((double)voxelChunk.densities[num3, num4 + 1, num5] < 0.5) ? 1 : 2));
							array[num3 + 1, num4, num5] |= b;
							array[num3 + 1, num4, num5 + 1] |= b;
							array[num3, num4, num5 + 1] |= b;
							array[num3, num4, num5] |= b;
							array[num3 + 1, num4 + 1, num5] |= b;
							array[num3 + 1, num4 + 1, num5 + 1] |= b;
							array[num3, num4 + 1, num5 + 1] |= b;
							array[num3, num4 + 1, num5] |= b;
							if (num5 == 0 || num5 == voxelChunk.densities.GetLength(2) - 1 || num4 == 0 || num4 == voxelChunk.densities.GetLength(1) - 1 || num3 == 0 || num3 == voxelChunk.densities.GetLength(0) - 1)
							{
								array[num3, num4, num5] |= 3;
							}
						}
					}
				}
				list.Add(1);
				for (int num6 = 0; num6 < voxelChunk.densities.GetLength(0) - 1; num6++)
				{
					for (int num7 = 0; num7 < voxelChunk.densities.GetLength(1) - 1; num7++)
					{
						for (int num8 = 0; num8 < voxelChunk.densities.GetLength(2) - 1; num8++)
						{
							switch (array[num6, num7, num8])
							{
							case 1:
								list.Add(0);
								break;
							case 2:
								list.Add(byte.MaxValue);
								break;
							default:
								list.Add((byte)(Mathf.Clamp01(voxelChunk.densities[num6, num7, num8]) * densityPrecision));
								break;
							}
						}
					}
				}
			}
			return Convert.ToBase64String(list.ToArray());
		}

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

		public static string SerializeChunkDensities(float[,,] densities, float densityPrecision)
		{
			List<byte> list = new List<byte>();
			bool flag = true;
			float num = densities[0, 0, 0];
			foreach (float num2 in densities)
			{
				flag = flag && num2 == num;
			}
			if (flag)
			{
				list.Add(0);
				list.Add((byte)(Mathf.Clamp01(num) * densityPrecision));
			}
			else
			{
				list.Add(1);
				for (int l = 0; l < densities.GetLength(0) - 1; l++)
				{
					for (int m = 0; m < densities.GetLength(1) - 1; m++)
					{
						for (int n = 0; n < densities.GetLength(2) - 1; n++)
						{
							list.Add((byte)(Mathf.Clamp01(densities[l, m, n]) * densityPrecision));
						}
					}
				}
			}
			return Convert.ToBase64String(list.ToArray());
		}

		private void FillBordersWithNeighbouringData(Dictionary<Vector3Int, Level.VolumeChunk> volumeChunks, Vector3Int extent, Func<Level.VolumeChunk, float[,,]> getVolumeData)
		{
			foreach (KeyValuePair<Vector3Int, Level.VolumeChunk> volumeChunk in volumeChunks)
			{
				float[,,] array = getVolumeData(volumeChunk.Value);
				if (volumeChunks.TryGetValue(volumeChunk.Key + new Vector3Int(Level.VoxelChunk.noOfCells.x, 0, 0), out var value))
				{
					float[,,] array2 = getVolumeData(value);
					for (int i = 0; i < extent.z; i++)
					{
						for (int j = 0; j < extent.y; j++)
						{
							array[i, j, extent.x] = array2[i, j, 0];
						}
					}
				}
				if (volumeChunks.TryGetValue(volumeChunk.Key + new Vector3Int(0, Level.VoxelChunk.noOfCells.y, 0), out value))
				{
					float[,,] array3 = getVolumeData(value);
					for (int k = 0; k < extent.z; k++)
					{
						for (int l = 0; l < extent.x; l++)
						{
							array[k, extent.y, l] = array3[k, 0, l];
						}
					}
				}
				if (volumeChunks.TryGetValue(volumeChunk.Key + new Vector3Int(0, 0, Level.VoxelChunk.noOfCells.z), out value))
				{
					float[,,] array4 = getVolumeData(value);
					for (int m = 0; m < extent.y; m++)
					{
						for (int n = 0; n < extent.x; n++)
						{
							array[extent.z, m, n] = array4[0, m, n];
						}
					}
				}
				if (volumeChunks.TryGetValue(volumeChunk.Key + new Vector3Int(Level.VoxelChunk.noOfCells.x, Level.VoxelChunk.noOfCells.y, 0), out value))
				{
					float[,,] array5 = getVolumeData(value);
					for (int num = 0; num < extent.z; num++)
					{
						array[num, extent.y, extent.x] = array5[num, 0, 0];
					}
				}
				if (volumeChunks.TryGetValue(volumeChunk.Key + new Vector3Int(Level.VoxelChunk.noOfCells.x, 0, Level.VoxelChunk.noOfCells.z), out value))
				{
					float[,,] array6 = getVolumeData(value);
					for (int num2 = 0; num2 < extent.y; num2++)
					{
						array[extent.z, num2, extent.x] = array6[0, num2, 0];
					}
				}
				if (volumeChunks.TryGetValue(volumeChunk.Key + new Vector3Int(0, Level.VoxelChunk.noOfCells.y, Level.VoxelChunk.noOfCells.z), out value))
				{
					float[,,] array7 = getVolumeData(value);
					for (int num3 = 0; num3 < extent.x; num3++)
					{
						array[extent.z, extent.y, num3] = array7[0, 0, num3];
					}
				}
				if (volumeChunks.TryGetValue(volumeChunk.Key + Level.VoxelChunk.noOfCells, out value))
				{
					float[,,] array8 = getVolumeData(value);
					array[extent.z, extent.y, extent.x] = array8[0, 0, 0];
				}
			}
		}

		private static Dictionary<string, string> buildCustomData(List<string> keys, List<string> values)
		{
			if (keys == null || values == null)
			{
				return null;
			}
			int num = Mathf.Min(keys.Count, values.Count);
			if (num == 0)
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = 0; i < num; i++)
			{
				dictionary.Add(keys[i], values[i]);
			}
			return dictionary;
		}

		public Level BuildLevel()
		{
			if (scene == null || chunks == null || settings == null)
			{
				throw new Exception("Level missing data.");
			}
			Level level = new Level
			{
				settings = new Level.Settings
				{
					showWater = settings.showWater,
					waterLevel = settings.waterLevel,
					weatherIndex = settings.weatherIndex,
					musicIndex = settings.musicIndex,
					presetName = settings.presetName,
					timeOfDay = settings.timeOfDay,
					sunColor = settings.sunColor,
					sunIntensity = settings.sunIntensity,
					ambientSkyColor = settings.ambientSkyColor,
					ambientEquatorColor = settings.ambientEquatorColor,
					ambientGroundColor = settings.ambientGroundColor,
					skyboxDayBlend = settings.skyboxDayBlend,
					skyboxNightBlend = settings.skyboxNightBlend
				}
			};
			foreach (Entity entity in scene.entities)
			{
				level.scene.flatEntities.Add(new Level.FlatEntity
				{
					entity = new Level.Entity
					{
						guid = Guid.Parse(entity.id),
						objectTypeId = entity.objectTypeId,
						position = entity.position,
						slope = entity.slope,
						rotation = entity.rotation,
						scale = entity.scale,
						customData = buildCustomData(entity.customDataKeys, entity.customDataValues),
						heightOffset = entity.heightOffset
					},
					parentGuid = Guid.Parse(entity.parentId)
				});
			}
			foreach (CompressedChunk compressedVoxelChunk in chunks.compressedVoxelChunks)
			{
				level.volume.volumeChunks.Add(compressedVoxelChunk.position, new Level.VolumeChunk
				{
					voxelChunk = new Level.VoxelChunk
					{
						version = 0,
						densities = DeserializeChunkDensities(compressedVoxelChunk.voxelDensities, Level.VoxelChunk.noOfCells, chunks.voxelDensityPrecision)
					},
					materialChunk = new Level.MaterialChunk
					{
						version = 0,
						densities = DeserializeChunkDensities(compressedVoxelChunk.materialDensities, Level.MaterialChunk.noOfCells, chunks.materialDensityPrecision)
					},
					foliageChunk = new Level.FoliageChunk
					{
						version = 0,
						densities = DeserializeChunkDensities(compressedVoxelChunk.foliageDensities, Level.FoliageChunk.noOfCells, chunks.voxelDensityPrecision)
					}
				});
			}
			FillBordersWithNeighbouringData(level.volume.volumeChunks, Level.VoxelChunk.noOfCells, (Level.VolumeChunk volumeChunk) => volumeChunk.voxelChunk.densities);
			FillBordersWithNeighbouringData(level.volume.volumeChunks, Level.MaterialChunk.noOfCells, (Level.VolumeChunk volumeChunk) => volumeChunk.materialChunk.densities);
			FillBordersWithNeighbouringData(level.volume.volumeChunks, Level.FoliageChunk.noOfCells, (Level.VolumeChunk volumeChunk) => volumeChunk.foliageChunk.densities);
			return level;
		}

		public static Level_Latest ParseLevel(Level level)
		{
			Level_Latest level_Latest = new Level_Latest();
			if (level.settings != null)
			{
				level_Latest.settings = new Settings
				{
					showWater = level.settings.showWater,
					waterLevel = level.settings.waterLevel,
					weatherIndex = level.settings.weatherIndex,
					musicIndex = level.settings.musicIndex,
					presetName = level.settings.presetName,
					timeOfDay = level.settings.timeOfDay,
					sunColor = level.settings.sunColor,
					sunIntensity = level.settings.sunIntensity,
					ambientSkyColor = level.settings.ambientSkyColor,
					ambientEquatorColor = level.settings.ambientEquatorColor,
					ambientGroundColor = level.settings.ambientGroundColor,
					skyboxDayBlend = level.settings.skyboxDayBlend,
					skyboxNightBlend = level.settings.skyboxNightBlend
				};
			}
			if (level.volume != null)
			{
				level_Latest.chunks = new Chunks
				{
					voxelDensityPrecision = 255f,
					materialDensityPrecision = 255f,
					foliageDensityPrecision = 255f
				};
				foreach (KeyValuePair<Vector3Int, Level.VolumeChunk> volumeChunk in level.volume.volumeChunks)
				{
					level_Latest.chunks.compressedVoxelChunks.Add(new CompressedChunk
					{
						position = volumeChunk.Key,
						voxelDensities = SerializeVoxelDensities(volumeChunk.Value.voxelChunk, level_Latest.chunks.voxelDensityPrecision),
						materialDensities = SerializeChunkDensities(volumeChunk.Value.materialChunk.densities, level_Latest.chunks.materialDensityPrecision),
						foliageDensities = SerializeChunkDensities(volumeChunk.Value.foliageChunk.densities, level_Latest.chunks.foliageDensityPrecision)
					});
				}
			}
			if (level.scene != null)
			{
				level_Latest.scene = new Scene();
				foreach (Level.FlatEntity flatEntity in level.scene.flatEntities)
				{
					List<Entity> entities = level_Latest.scene.entities;
					Entity item = new Entity
					{
						objectTypeId = flatEntity.entity.objectTypeId,
						position = flatEntity.entity.position,
						slope = flatEntity.entity.slope,
						rotation = flatEntity.entity.rotation,
						scale = flatEntity.entity.scale,
						id = flatEntity.entity.guid.ToString()
					};
					Guid parentGuid = flatEntity.parentGuid;
					item.parentId = parentGuid.ToString();
					item.customDataKeys = ((flatEntity.entity.customData == null || flatEntity.entity.customData.Count == 0) ? null : flatEntity.entity.customData.Keys.ToList());
					item.customDataValues = ((flatEntity.entity.customData == null || flatEntity.entity.customData.Count == 0) ? null : flatEntity.entity.customData.Values.ToList());
					item.heightOffset = flatEntity.entity.heightOffset;
					entities.Add(item);
				}
			}
			return level_Latest;
		}
	}
}
