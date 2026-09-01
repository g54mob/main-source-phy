using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace LevelCreator
{
	public class Volume : MonoBehaviour
	{
		private class VolumeManipulator
		{
			private Densities m_densityCopyBuffer;

			private void CopyFromVolume(Vector3Int startPosition, Vector3Int size, Vector3Int noOfCells, Func<Vector3Int, Densities> getModifiableChunkDensities)
			{
				if (m_densityCopyBuffer == null)
				{
					m_densityCopyBuffer = new Densities
					{
						oldConstDensities = new float[size.z, size.y, size.x],
						newModifiableDensites = new float[size.z, size.y, size.x]
					};
				}
				else if (m_densityCopyBuffer.newModifiableDensites.GetLength(0) < size.z || m_densityCopyBuffer.newModifiableDensites.GetLength(1) < size.y || m_densityCopyBuffer.newModifiableDensites.GetLength(2) < size.x)
				{
					int num = Mathf.Max(m_densityCopyBuffer.newModifiableDensites.GetLength(2), size.x);
					int num2 = Mathf.Max(m_densityCopyBuffer.newModifiableDensites.GetLength(1), size.y);
					int num3 = Mathf.Max(m_densityCopyBuffer.newModifiableDensites.GetLength(0), size.z);
					m_densityCopyBuffer = new Densities
					{
						oldConstDensities = new float[num3, num2, num],
						newModifiableDensites = new float[num3, num2, num]
					};
				}
				Vector3Int vector3Int = startPosition + size;
				int num4 = 0;
				int num7;
				for (int i = startPosition.z; i < vector3Int.z; i += num7)
				{
					int num5 = Utility.PositiveModulo(i, noOfCells.z);
					int num6 = i - num5;
					num7 = Mathf.Min(num6 + noOfCells.z, vector3Int.z) - i;
					int num8 = 0;
					int num11;
					for (int j = startPosition.y; j < vector3Int.y; j += num11)
					{
						int num9 = Utility.PositiveModulo(j, noOfCells.y);
						int num10 = j - num9;
						num11 = Mathf.Min(num10 + noOfCells.y, vector3Int.y) - j;
						int num12 = 0;
						int num15;
						for (int k = startPosition.x; k < vector3Int.x; k += num15)
						{
							int num13 = Utility.PositiveModulo(k, noOfCells.x);
							int num14 = k - num13;
							num15 = Mathf.Min(num14 + noOfCells.x, vector3Int.x) - k;
							Densities densities = getModifiableChunkDensities(Vector3Int.Scale(new Vector3Int(num14 / noOfCells.x, num10 / noOfCells.y, num6 / noOfCells.z), new Vector3Int(Level.VoxelChunk.noOfCells.x, Level.VoxelChunk.noOfCells.y, Level.VoxelChunk.noOfCells.z)));
							for (int l = 0; l < num7; l++)
							{
								for (int m = 0; m < num11; m++)
								{
									for (int n = 0; n < num15; n++)
									{
										m_densityCopyBuffer.oldConstDensities[num4 + l, num8 + m, num12 + n] = ((densities != null) ? densities.oldConstDensities[num5 + l, num9 + m, num13 + n] : 0f);
										m_densityCopyBuffer.newModifiableDensites[num4 + l, num8 + m, num12 + n] = ((densities != null) ? densities.newModifiableDensites[num5 + l, num9 + m, num13 + n] : 0f);
									}
								}
							}
							num12 += num15;
						}
						num8 += num11;
					}
					num4 += num7;
				}
			}

			private void CopyToVolume(Vector3Int startPosition, Vector3Int size, Vector3Int noOfCells, BoundsInt bounds, Func<Vector3Int, Densities> getModifiableChunkDensities)
			{
				Vector3Int vector3Int = new Vector3Int(0, 0, 0);
				if (startPosition.x < bounds.xMin)
				{
					vector3Int.x = bounds.xMin - startPosition.x;
					startPosition.x = bounds.xMin;
				}
				if (startPosition.y < bounds.yMin)
				{
					vector3Int.y = bounds.yMin - startPosition.y;
					startPosition.y = bounds.yMin;
				}
				if (startPosition.z < bounds.zMin)
				{
					vector3Int.z = bounds.zMin - startPosition.z;
					startPosition.z = bounds.zMin;
				}
				Vector3Int vector3Int2 = Vector3Int.Min(bounds.max, startPosition + size - vector3Int);
				Vector3Int vector3Int3 = new Vector3Int(Mathf.Max(0, (startPosition.x - 1) / noOfCells.x), Mathf.Max(0, (startPosition.y - 1) / noOfCells.y), Mathf.Max(0, (startPosition.z - 1) / noOfCells.z));
				Vector3Int vector3Int4 = new Vector3Int(Mathf.Max(0, (vector3Int2.x - 1) / noOfCells.x), Mathf.Max(0, (vector3Int2.y - 1) / noOfCells.y), Mathf.Max(0, (vector3Int2.z - 1) / noOfCells.z));
				int num = vector3Int.z;
				int i = vector3Int3.z;
				int num2 = startPosition.z;
				for (; i <= vector3Int4.z; i++)
				{
					int num3 = i * noOfCells.z;
					int num4 = Mathf.Min(num3 + noOfCells.z + 1, vector3Int2.z);
					int num5 = num4 - num2;
					int num6 = vector3Int.y;
					int j = vector3Int3.y;
					int num7 = startPosition.y;
					for (; j <= vector3Int4.y; j++)
					{
						int num8 = j * noOfCells.y;
						int num9 = Mathf.Min(num8 + noOfCells.y + 1, vector3Int2.y);
						int num10 = num9 - num7;
						int num11 = vector3Int.x;
						int k = vector3Int3.x;
						int num12 = startPosition.x;
						for (; k <= vector3Int4.x; k++)
						{
							int num13 = k * noOfCells.x;
							int num14 = Mathf.Min(num13 + noOfCells.x + 1, vector3Int2.x);
							int num15 = num14 - num12;
							Vector3Int arg = Vector3Int.Scale(new Vector3Int(num13 / noOfCells.x, num8 / noOfCells.y, num3 / noOfCells.z), new Vector3Int(Level.VoxelChunk.noOfCells.x, Level.VoxelChunk.noOfCells.y, Level.VoxelChunk.noOfCells.z));
							Densities densities = getModifiableChunkDensities(arg);
							if (densities != null)
							{
								for (int l = 0; l < num5; l++)
								{
									for (int m = 0; m < num10; m++)
									{
										for (int n = 0; n < num15; n++)
										{
											densities.newModifiableDensites[num2 - num3 + l, num7 - num8 + m, num12 - num13 + n] = Mathf.Clamp01(Mathf.Round(m_densityCopyBuffer.newModifiableDensites[num + l, num6 + m, num11 + n] * 255f) / 255f);
										}
									}
								}
							}
							num11 += num14 - num12 - 1;
							num12 = num14 - 1;
						}
						num6 += num9 - num7 - 1;
						num7 = num9 - 1;
					}
					num += num4 - num2 - 1;
					num2 = num4 - 1;
				}
			}

			public void WithVolumeCopy(Vector3 position, Brush brush, Vector3Int noOfCells, BoundsInt bounds, Func<Vector3Int, Densities> getModifiableChunkDensities, Action<Densities> action)
			{
				if (brush == null)
				{
					throw new Exception("Empty brush");
				}
				Vector3Int startPosition = new Vector3Int(Mathf.FloorToInt(position.x * (float)noOfCells.x / (float)Level.VoxelChunk.noOfCells.x + 0.5f - brush.Pivot.x), Mathf.FloorToInt(position.y * (float)noOfCells.y / (float)Level.VoxelChunk.noOfCells.y + 0.5f - brush.Pivot.y), Mathf.FloorToInt(position.z * (float)noOfCells.z / (float)Level.VoxelChunk.noOfCells.z + 0.5f - brush.Pivot.z));
				CopyFromVolume(startPosition, brush.Size, noOfCells, getModifiableChunkDensities);
				action(m_densityCopyBuffer);
				CopyToVolume(startPosition, brush.Size, noOfCells, bounds, getModifiableChunkDensities);
			}

			public void Add(Vector3 position, Brush brush, Vector3Int noOfCells, BoundsInt bounds, float lerpIntensity, Func<Vector3Int, Densities> getModifiableChunkDensities)
			{
				WithVolumeCopy(position, brush, noOfCells, bounds, getModifiableChunkDensities, delegate(Densities densityCopyBuffer)
				{
					Vector3Int size = brush.Size;
					for (int i = 0; i < size.z; i++)
					{
						for (int j = 0; j < size.y; j++)
						{
							for (int k = 0; k < size.x; k++)
							{
								float num = densityCopyBuffer.oldConstDensities[i, j, k];
								float num2 = densityCopyBuffer.newModifiableDensites[i, j, k];
								float num3 = brush.Field[i, j, k];
								if (!(num3 < num2))
								{
									float num4 = Mathf.Min(1f, num + num3);
									float b = ((num4 > num2 - 0.00882353f) ? Mathf.Lerp(num2, num4, lerpIntensity) : num4);
									densityCopyBuffer.newModifiableDensites[i, j, k] = Mathf.Max(num2, b);
								}
							}
						}
					}
				});
			}

			public void Subtract(Vector3 position, Brush brush, Vector3Int noOfCells, BoundsInt bounds, float lerpIntensity, Func<Vector3Int, Densities> getModifiableChunkDensities)
			{
				WithVolumeCopy(position, brush, noOfCells, bounds, getModifiableChunkDensities, delegate(Densities volumeCopy)
				{
					Vector3Int size = brush.Size;
					for (int i = 0; i < size.z; i++)
					{
						for (int j = 0; j < size.y; j++)
						{
							for (int k = 0; k < size.x; k++)
							{
								float num = volumeCopy.oldConstDensities[i, j, k];
								float num2 = volumeCopy.newModifiableDensites[i, j, k];
								float num3 = brush.Field[i, j, k];
								if (!((double)num3 < 0.5) || !(num3 < num))
								{
									float num4 = Mathf.Max(0f, num - brush.Field[i, j, k]);
									float b = ((num4 < num2 + 0.00882353f) ? Mathf.Lerp(num2, num4, lerpIntensity) : num4);
									volumeCopy.newModifiableDensites[i, j, k] = Mathf.Min(num2, b);
								}
							}
						}
					}
				});
			}

			public void Blur(Vector3 position, Brush brush, Vector3Int noOfCells, BoundsInt bounds, Func<Vector3Int, Densities> getModifiableChunkDensities)
			{
				WithVolumeCopy(position, brush, noOfCells, bounds, getModifiableChunkDensities, delegate(Densities volumeCopy)
				{
					Vector3Int size = brush.Size;
					Vector3Int vector3Int = default(Vector3Int);
					for (int i = 0; i < 3; i++)
					{
						int index = i % 3;
						int index2 = (i + 1) % 3;
						int index3 = (i + 2) % 3;
						for (int j = 0; j < size[index]; j++)
						{
							for (int k = 0; k < size[index2]; k++)
							{
								vector3Int[index] = j;
								vector3Int[index2] = k;
								for (int l = 1; l < size[index3] - 1; l++)
								{
									vector3Int[index3] = l - 1;
									float num = volumeCopy.newModifiableDensites[vector3Int[0], vector3Int[1], vector3Int[2]];
									vector3Int[index3] = l + 1;
									float num2 = volumeCopy.newModifiableDensites[vector3Int[0], vector3Int[1], vector3Int[2]];
									vector3Int[index3] = l;
									float num3 = volumeCopy.newModifiableDensites[vector3Int[0], vector3Int[1], vector3Int[2]];
									float num4 = Mathf.Pow(brush.Field[vector3Int[0], vector3Int[1], vector3Int[2]], 0.5f);
									volumeCopy.newModifiableDensites[vector3Int[0], vector3Int[1], vector3Int[2]] = Mathf.Clamp01(num4 * (0.05f * num + 0.05f * num2) + (num4 * 0.9f + 1f - num4) * num3);
								}
							}
						}
					}
				});
			}

			public void LerpCyclic(Vector3 position, Brush brush, float targetValue, Vector3Int noOfCells, BoundsInt bounds, float lerpIntensity, Func<Vector3Int, Densities> getModifiableChunkDensities)
			{
				WithVolumeCopy(position, brush, noOfCells, bounds, getModifiableChunkDensities, delegate(Densities volumeCopy)
				{
					Vector3Int size = brush.Size;
					for (int i = 0; i < size.z; i++)
					{
						for (int j = 0; j < size.y; j++)
						{
							for (int k = 0; k < size.x; k++)
							{
								volumeCopy.newModifiableDensites[i, j, k] = Utility.LerpCyclic(m_densityCopyBuffer.newModifiableDensites[i, j, k], targetValue, brush.Field[i, j, k] * lerpIntensity);
							}
						}
					}
				});
			}
		}

		private class Densities
		{
			public float[,,] oldConstDensities;

			public float[,,] newModifiableDensites;
		}

		public static float fullLerpIntensity = 1f;

		public static float defaultLerpIntensity = 0.1f;

		private Dictionary<Vector3Int, VolumeMeshChunk> m_volumeMeshChunks = new Dictionary<Vector3Int, VolumeMeshChunk>();

		private VolumeMeshChunkUpdater m_volumeMeshChunkUpdater;

		public HashSet<VolumeMeshChunk> volumeMeshChunksWithOldFoliage = new HashSet<VolumeMeshChunk>();

		public VolumeMeshChunk VolumeMeshChunkPrefab;

		public Material volumeMaterial;

		private List<VolumeMeshChunk> m_tempVolumeMeshChunks = new List<VolumeMeshChunk>();

		private int m_currentTempVolumeMeshChunkIndex;

		public DateTime m_lastUpdateFoliageTimestamp;

		private VolumeManipulator m_volumeManipulator = new VolumeManipulator();

		private List<FoliageData> foliageItems;

		public void ForEachChunk(Action<Vector3Int, VolumeMeshChunk> callback)
		{
			foreach (KeyValuePair<Vector3Int, VolumeMeshChunk> volumeMeshChunk in m_volumeMeshChunks)
			{
				callback(volumeMeshChunk.Key, volumeMeshChunk.Value);
			}
		}

		public Level.VoxelChunk GetVoxelChunk(Vector3Int chunkPosition)
		{
			if (!m_volumeMeshChunks.TryGetValue(chunkPosition, out var value))
			{
				return null;
			}
			return value.GetReadableVoxelChunk();
		}

		private Densities GetModifiableVoxelChunkDensities(Vector3Int chunkPosition)
		{
			if (!m_volumeMeshChunks.TryGetValue(chunkPosition, out var value))
			{
				return null;
			}
			Level.VoxelChunk modifiableVoxelChunk = value.GetModifiableVoxelChunk();
			m_volumeMeshChunkUpdater.ScheduleUpdate(value);
			return new Densities
			{
				oldConstDensities = value.GetOldVoxelDensities(),
				newModifiableDensites = modifiableVoxelChunk.densities
			};
		}

		private Densities GetModifiableMaterialChunkDensities(Vector3Int chunkPosition)
		{
			if (!m_volumeMeshChunks.TryGetValue(chunkPosition, out var value))
			{
				return null;
			}
			Level.MaterialChunk modifiableMaterialChunk = value.GetModifiableMaterialChunk();
			m_volumeMeshChunkUpdater.ScheduleUpdate(value);
			return new Densities
			{
				oldConstDensities = value.GetOldMaterialDensities(),
				newModifiableDensites = modifiableMaterialChunk.densities
			};
		}

		private Densities GetModifiableFoliageChunkDensities(Vector3Int chunkPosition)
		{
			if (!m_volumeMeshChunks.TryGetValue(chunkPosition, out var value))
			{
				return null;
			}
			volumeMeshChunksWithOldFoliage.Add(value);
			Level.FoliageChunk modifiableFoliageChunk = value.GetModifiableFoliageChunk();
			return new Densities
			{
				oldConstDensities = value.GetOldFoliageDensities(),
				newModifiableDensites = modifiableFoliageChunk.densities
			};
		}

		public bool HasChunksUnderConstruction(Vector3 position, float distanceY)
		{
			int num = Mathf.FloorToInt(position.x);
			int num2 = Mathf.FloorToInt(position.y - distanceY);
			int num3 = Mathf.FloorToInt(position.y + distanceY);
			int num4 = Mathf.FloorToInt(position.z);
			int num5 = Utility.PositiveModulo(num, Level.VoxelChunk.noOfCells.x);
			int num6 = Utility.PositiveModulo(num2, Level.VoxelChunk.noOfCells.y);
			int num7 = Utility.PositiveModulo(num3, Level.VoxelChunk.noOfCells.y);
			int num8 = Utility.PositiveModulo(num4, Level.VoxelChunk.noOfCells.z);
			int x = num - num5;
			int num9 = num2 - num6;
			int num10 = num3 - num7;
			int z = num4 - num8;
			for (int i = num9; i <= num10; i += Level.VoxelChunk.noOfCells.y)
			{
				if (m_volumeMeshChunks.TryGetValue(new Vector3Int(x, i, z), out var value) && m_volumeMeshChunkUpdater.IsScheduledForUpdate(value))
				{
					return true;
				}
			}
			return false;
		}

		public float GetVoxel(int x, int y, int z)
		{
			int num = Utility.PositiveModulo(x, Level.VoxelChunk.noOfCells.x);
			int num2 = Utility.PositiveModulo(y, Level.VoxelChunk.noOfCells.y);
			int num3 = Utility.PositiveModulo(z, Level.VoxelChunk.noOfCells.z);
			int x2 = x - num;
			int y2 = y - num2;
			int z2 = z - num3;
			Level.VoxelChunk voxelChunk = GetVoxelChunk(new Vector3Int(x2, y2, z2));
			if (voxelChunk == null)
			{
				return 0f;
			}
			return voxelChunk.densities[num3, num2, num];
		}

		public void SetVoxel(int x, int y, int z, float value)
		{
			int num = Utility.PositiveModulo(x, Level.VoxelChunk.noOfCells.x);
			int num2 = Utility.PositiveModulo(y, Level.VoxelChunk.noOfCells.y);
			int num3 = Utility.PositiveModulo(z, Level.VoxelChunk.noOfCells.z);
			int num4 = x - num;
			int num5 = y - num2;
			int num6 = z - num3;
			for (int i = 0; i < 8; i++)
			{
				bool flag = (i & 1) == 1;
				bool flag2 = (i & 2) == 2;
				bool flag3 = (i & 4) == 4;
				if ((!flag || num == 0) && (!flag2 || num2 == 0) && (!flag3 || num3 == 0))
				{
					Densities modifiableVoxelChunkDensities = GetModifiableVoxelChunkDensities(new Vector3Int(flag ? (num4 - Level.VoxelChunk.noOfCells.x) : num4, flag2 ? (num5 - Level.VoxelChunk.noOfCells.y) : num5, flag3 ? (num6 - Level.VoxelChunk.noOfCells.z) : num6));
					if (modifiableVoxelChunkDensities != null)
					{
						modifiableVoxelChunkDensities.newModifiableDensites[(flag3 && num3 == 0) ? Level.VoxelChunk.noOfCells.x : num3, (flag2 && num2 == 0) ? Level.VoxelChunk.noOfCells.y : num2, (flag && num == 0) ? Level.VoxelChunk.noOfCells.z : num] = value;
					}
				}
			}
		}

		public float Get(Vector3 position)
		{
			return GetVoxel(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), Mathf.FloorToInt(position.z));
		}

		public void Set(Vector3 position, float density)
		{
			SetVoxel(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), Mathf.FloorToInt(position.z), density);
		}

		public Bounds GetBounds()
		{
			Bounds result = new Bounds(Vector3.zero, Vector3.zero);
			bool flag = true;
			foreach (KeyValuePair<Vector3Int, VolumeMeshChunk> volumeMeshChunk in m_volumeMeshChunks)
			{
				Vector3 vector = volumeMeshChunk.Key;
				Vector3 vector2 = vector + new Vector3(16f, 16f, 16f);
				result.min = (flag ? vector : Vector3.Min(result.min, vector));
				result.max = (flag ? vector2 : Vector3.Max(result.max, vector2));
				flag = false;
			}
			return result;
		}

		public Bounds GetBounds(Vector3 position, Brush brush)
		{
			Vector3Int vector3Int = Vector3Int.FloorToInt(position - brush.Pivot);
			Vector3Int vector3Int2 = Vector3Int.CeilToInt(position + brush.Pivot);
			Bounds result = default(Bounds);
			result.SetMinMax(vector3Int, vector3Int2);
			return result;
		}

		public void AddVolume(Vector3 position, Brush brush, float lerpIntensity)
		{
			m_volumeManipulator.Add(position, brush, Level.VoxelChunk.noOfCells, Level.VoxelChunk.voxelBounds, lerpIntensity, (Vector3Int chunkPosition) => GetModifiableVoxelChunkDensities(chunkPosition));
		}

		public void SubtractVolume(Vector3 position, Brush brush, float lerpIntensity)
		{
			m_volumeManipulator.Subtract(position, brush, Level.VoxelChunk.noOfCells, Level.VoxelChunk.voxelBounds, lerpIntensity, (Vector3Int chunkPosition) => GetModifiableVoxelChunkDensities(chunkPosition));
		}

		public void BlurVolume(Vector3 position, Brush brush)
		{
			m_volumeManipulator.Blur(position, brush, Level.VoxelChunk.noOfCells, Level.VoxelChunk.voxelBounds, (Vector3Int chunkPosition) => GetModifiableVoxelChunkDensities(chunkPosition));
		}

		public void LerpMaterial(Vector3 position, Brush brush, float targetValue, float lerpIntensity)
		{
			m_volumeManipulator.LerpCyclic(position, brush, targetValue, Level.MaterialChunk.noOfCells, Level.MaterialChunk.materialBounds, lerpIntensity, (Vector3Int chunkPosition) => GetModifiableMaterialChunkDensities(chunkPosition));
		}

		public void AddFoliage(Vector3 position, Brush brush, float lerpIntensity)
		{
			m_volumeManipulator.Add(position, brush, Level.FoliageChunk.noOfCells, Level.FoliageChunk.foliageBounds, lerpIntensity, (Vector3Int chunkPosition) => GetModifiableFoliageChunkDensities(chunkPosition));
		}

		public void SubtractFoliage(Vector3 position, Brush brush, float lerpIntensity)
		{
			m_volumeManipulator.Subtract(position, brush, Level.FoliageChunk.noOfCells, Level.FoliageChunk.foliageBounds, lerpIntensity, (Vector3Int chunkPosition) => GetModifiableFoliageChunkDensities(chunkPosition));
		}

		public void SetAllFoliage(float value)
		{
			ForEachChunk(delegate(Vector3Int pos, VolumeMeshChunk volumeMeshChunk)
			{
				Level.FoliageChunk modifiableFoliageChunk = volumeMeshChunk.GetModifiableFoliageChunk();
				for (int i = 0; i < Level.FoliageChunk.noOfCells.z; i++)
				{
					for (int j = 0; j < Level.FoliageChunk.noOfCells.y; j++)
					{
						for (int k = 0; k < Level.FoliageChunk.noOfCells.x; k++)
						{
							modifiableFoliageChunk.densities[i, j, k] = 1f;
						}
					}
				}
				volumeMeshChunksWithOldFoliage.Add(volumeMeshChunk);
			});
		}

		public void UpdateMaterials(LevelPresetData levelPreset)
		{
			volumeMaterial.SetColor("_TopColor", levelPreset.TopColor);
			volumeMaterial.SetColor("_DirtColor", levelPreset.DirtColor);
			volumeMaterial.SetColor("_RockColor", levelPreset.RockColor);
			volumeMaterial.SetFloat("_BaseMetal", levelPreset.BaseMetallic);
			volumeMaterial.SetFloat("_BaseSmooth", levelPreset.BaseSmoothness);
			volumeMaterial.SetColor("_SecondCol", levelPreset.SecondColor);
			volumeMaterial.SetFloat("_SecondMetal", levelPreset.SecondMetallic);
			volumeMaterial.SetFloat("_SecondSmooth", levelPreset.SecondSmoothness);
			volumeMaterial.SetColor("_ThirdCol", levelPreset.ThirdColor);
			volumeMaterial.SetFloat("_ThirdMetal", levelPreset.ThirdMetallic);
			volumeMaterial.SetFloat("_ThirdSmooth", levelPreset.ThirdSmoothness);
		}

		public void InvalidateAllChunks()
		{
			foreach (KeyValuePair<Vector3Int, VolumeMeshChunk> volumeMeshChunk in m_volumeMeshChunks)
			{
				volumeMeshChunk.Value.Invalidate();
			}
		}

		public void SetChunks(Dictionary<Vector3Int, Level.VolumeChunk> volumeChunks)
		{
			List<Vector3Int> list = new List<Vector3Int>();
			foreach (KeyValuePair<Vector3Int, VolumeMeshChunk> volumeMeshChunk in m_volumeMeshChunks)
			{
				if (!volumeChunks.ContainsKey(volumeMeshChunk.Key))
				{
					list.Add(volumeMeshChunk.Key);
				}
			}
			foreach (Vector3Int item in list)
			{
				m_volumeMeshChunks.TryGetValue(item, out var value);
				m_volumeMeshChunks.Remove(item);
				UnityEngine.Object.Destroy(value.gameObject);
			}
			foreach (KeyValuePair<Vector3Int, Level.VolumeChunk> volumeChunk in volumeChunks)
			{
				SetChunk(volumeChunk.Key, volumeChunk.Value);
			}
		}

		public void SetChunk(Vector3Int chunkPosition, Level.VolumeChunk volumeChunk)
		{
			if (m_volumeMeshChunks.TryGetValue(chunkPosition, out var value))
			{
				bool flag = value.VoxelVersion() != volumeChunk.voxelChunk.version;
				bool flag2 = value.MaterialVersion() != volumeChunk.materialChunk.version;
				bool num = value.FoliageVersion() != volumeChunk.foliageChunk.version;
				value.SetVolumeChunk(volumeChunk);
				if (flag || flag2)
				{
					m_volumeMeshChunkUpdater.ScheduleUpdate(value);
				}
				if (num && !m_volumeMeshChunkUpdater.IsScheduledForUpdate(value))
				{
					value.BuildFoliage(instantGrow: true, chunkPosition);
				}
			}
			else
			{
				VolumeMeshChunk volumeMeshChunk = UnityEngine.Object.Instantiate(VolumeMeshChunkPrefab, chunkPosition, Quaternion.identity, base.transform);
				volumeMeshChunk.Init(chunkPosition, volumeChunk);
				m_volumeMeshChunks.Add(chunkPosition, volumeMeshChunk);
				m_volumeMeshChunkUpdater.ScheduleUpdate(volumeMeshChunk);
			}
		}

		private void Awake()
		{
			m_volumeMeshChunkUpdater = new VolumeMeshChunkUpdater();
		}

		private void OnApplicationQuit()
		{
			m_volumeMeshChunkUpdater.Stop();
		}

		public void BuildFoliage()
		{
			foreach (VolumeMeshChunk item in volumeMeshChunksWithOldFoliage)
			{
				if (!m_volumeMeshChunkUpdater.IsScheduledForUpdate(item))
				{
					item.BuildFoliage(instantGrow: false, item.chunkPosition);
				}
			}
			volumeMeshChunksWithOldFoliage.Clear();
		}

		public void Update()
		{
			BuildFoliage();
			m_volumeMeshChunkUpdater.updateQuotaInSeconds = Mathf.Clamp(m_volumeMeshChunkUpdater.updateQuotaInSeconds + Time.deltaTime * 0.25f, -1f, 1f / 60f);
			m_volumeMeshChunkUpdater.Update(foliageItems);
			foreach (KeyValuePair<Vector3Int, VolumeMeshChunk> volumeMeshChunk in m_volumeMeshChunks)
			{
				m_tempVolumeMeshChunks.Add(volumeMeshChunk.Value);
			}
			for (int i = 0; i < m_tempVolumeMeshChunks.Count; i++)
			{
				if (m_volumeMeshChunkUpdater.updateQuotaInSeconds <= 0f)
				{
					break;
				}
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				m_currentTempVolumeMeshChunkIndex = (m_currentTempVolumeMeshChunkIndex + 1) % m_tempVolumeMeshChunks.Count;
				if (m_tempVolumeMeshChunks[m_currentTempVolumeMeshChunkIndex].BuildFoliageMeshIfDirty(foliageItems))
				{
					m_lastUpdateFoliageTimestamp = DateTime.Now;
				}
				stopwatch.Stop();
				m_volumeMeshChunkUpdater.updateQuotaInSeconds -= (float)(stopwatch.Elapsed.TotalMilliseconds / 1000.0);
			}
			m_tempVolumeMeshChunks.Clear();
		}

		public void BuildAllChunks()
		{
			m_volumeMeshChunkUpdater.BuildAllChunks(foliageItems);
		}

		public void UpdateFoliage(SeedCollectionData[] seedCollectionData)
		{
			foliageItems = FoliageBuilder.CreateFoliageData(DMEditor.Instance.editorObjectTable, seedCollectionData);
			foreach (KeyValuePair<Vector3Int, VolumeMeshChunk> volumeMeshChunk in m_volumeMeshChunks)
			{
				volumeMeshChunk.Value.DirtyFlagFoliageMesh();
			}
		}

		public Vector3? LineCast(Vector3 start, Vector3 end)
		{
			Vector3Int vector3Int = Vector3Int.FloorToInt(Vector3.Min(start, end));
			Vector3Int vector3Int2 = Vector3Int.FloorToInt(Vector3.Max(start, end));
			int num = vector3Int.x - Utility.PositiveModulo(vector3Int.x, Level.VoxelChunk.noOfCells.x);
			int num2 = vector3Int.y - Utility.PositiveModulo(vector3Int.y, Level.VoxelChunk.noOfCells.y);
			int num3 = vector3Int.z - Utility.PositiveModulo(vector3Int.z, Level.VoxelChunk.noOfCells.z);
			int num4 = vector3Int2.x - Utility.PositiveModulo(vector3Int2.x, Level.VoxelChunk.noOfCells.x) + Level.VoxelChunk.noOfCells.x;
			int num5 = vector3Int2.y - Utility.PositiveModulo(vector3Int2.y, Level.VoxelChunk.noOfCells.y) + Level.VoxelChunk.noOfCells.y;
			int num6 = vector3Int2.z - Utility.PositiveModulo(vector3Int2.z, Level.VoxelChunk.noOfCells.z) + Level.VoxelChunk.noOfCells.z;
			Vector3? result = null;
			for (int i = num3; i < num6; i += Level.VoxelChunk.noOfCells.z)
			{
				for (int j = num2; j < num5; j += Level.VoxelChunk.noOfCells.y)
				{
					for (int k = num; k < num4; k += Level.VoxelChunk.noOfCells.x)
					{
						if (m_volumeMeshChunks.TryGetValue(new Vector3Int(k, j, i), out var value))
						{
							Vector3? vector = value.LineCast(start, end);
							if (!result.HasValue)
							{
								result = vector;
							}
							else if (vector.HasValue && (vector.Value - start).sqrMagnitude < (result.Value - start).sqrMagnitude)
							{
								result = vector;
							}
						}
					}
				}
			}
			return result;
		}

		public UpdateTimestamps GetUpdateTimestamps()
		{
			return new UpdateTimestamps
			{
				UpdateChunkTimestamp = m_volumeMeshChunkUpdater.GetLastUpdateChunkTimestamp(),
				UpdateFoliageTimestamp = m_lastUpdateFoliageTimestamp
			};
		}
	}
}
