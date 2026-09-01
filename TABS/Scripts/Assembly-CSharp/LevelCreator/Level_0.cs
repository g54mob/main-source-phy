using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class Level_0
	{
		[Serializable]
		public class Entity
		{
			public string objectTypeId;

			public Vector3 position;

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

		public int version;

		public bool showWater = true;

		public float waterLevel;

		public List<Entity> entities = new List<Entity>();

		public List<CompressedVoxelChunk> compressedVoxelChunk = new List<CompressedVoxelChunk>();

		public const float densityPrecision = 2047f;

		public Level_Latest Upgrade()
		{
			Level_1 level_ = new Level_1();
			level_.showWater = showWater;
			level_.waterLevel = waterLevel;
			foreach (Entity entity in entities)
			{
				level_.entities.Add(new Level_1.Entity
				{
					objectTypeId = entity.objectTypeId,
					position = entity.position,
					slope = entity.rotation,
					rotation = Quaternion.identity,
					scale = entity.scale
				});
			}
			level_.densityPrecision = 255f;
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
					break;
				case 1:
					list.Add(1);
					break;
				default:
				{
					list.Add(2);
					for (int i = 0; i < 4913; i++)
					{
						list.Add((byte)(level_.densityPrecision * (float)(array[num] + array[num + 1] * 256) / 2047f));
						num += 2;
					}
					break;
				}
				}
				level_.compressedVoxelChunk.Add(new Level_1.CompressedVoxelChunk
				{
					position = item.position,
					densities = Convert.ToBase64String(list.ToArray())
				});
			}
			return level_.Upgrade();
		}
	}
}
