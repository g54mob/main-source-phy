using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class Level_1
	{
		[Serializable]
		public class Entity
		{
			public string objectTypeId;

			public Vector3 position;

			public Quaternion slope;

			public Quaternion rotation;

			public Vector3 scale;
		}

		[Serializable]
		public class CompressedVoxelChunk
		{
			public Vector3Int position;

			public string densities;
		}

		public class ChunkTypes
		{
			public const byte AllEmpty = 0;

			public const byte AllSet = 1;

			public const byte Data = 2;
		}

		public bool showWater = true;

		public float waterLevel;

		public Color32 topColor = new Color32(103, 115, 51, 1);

		public Color32 dirtColor = new Color32(115, 104, 51, 1);

		public Color32 rockColor = new Color32(77, 87, 91, 1);

		public List<Entity> entities = new List<Entity>();

		public float densityPrecision;

		public List<CompressedVoxelChunk> compressedVoxelChunk = new List<CompressedVoxelChunk>();

		public const float defaultDensityPrecision = 255f;

		public Level_Latest Upgrade()
		{
			Level_2 level_ = new Level_2();
			level_.showWater = showWater;
			level_.waterLevel = waterLevel;
			level_.topColor = topColor;
			level_.dirtColor = dirtColor;
			level_.rockColor = rockColor;
			foreach (Entity entity in entities)
			{
				level_.entities.Add(new Level_2.Entity
				{
					objectTypeId = entity.objectTypeId,
					position = entity.position,
					slope = entity.slope,
					rotation = entity.rotation,
					scale = entity.scale
				});
			}
			level_.voxelDensityPrecision = densityPrecision;
			level_.foliageDensityPrecision = 255f;
			string foliageDensities = Convert.ToBase64String(new byte[2]);
			foreach (CompressedVoxelChunk item in compressedVoxelChunk)
			{
				byte[] array = Convert.FromBase64String(item.densities);
				List<byte> list = new List<byte>(array.GetLength(0));
				int num = 0;
				byte b = array[num];
				num++;
				switch (b)
				{
				case 0:
					list.Add(0);
					list.Add(0);
					break;
				case 1:
					list.Add(0);
					list.Add((byte)(1f * level_.voxelDensityPrecision));
					break;
				default:
				{
					list.Add(1);
					for (int i = 0; i <= 16; i++)
					{
						for (int j = 0; j <= 16; j++)
						{
							for (int k = 0; k <= 16; k++)
							{
								if (k < 16 && j < 16 && i < 16)
								{
									list.Add(array[num]);
								}
								num++;
							}
						}
					}
					break;
				}
				}
				level_.compressedVoxelChunks.Add(new Level_2.CompressedChunk
				{
					position = item.position,
					voxelDensities = Convert.ToBase64String(list.ToArray()),
					foliageDensities = foliageDensities
				});
			}
			return level_.Upgrade();
		}
	}
}
