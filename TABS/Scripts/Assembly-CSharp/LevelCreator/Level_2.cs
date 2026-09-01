using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class Level_2
	{
		[Serializable]
		public class Entity
		{
			public string objectTypeId;

			public Vector3 position;

			public Quaternion slope;

			public Quaternion rotation;

			public Vector3 scale;

			public List<Entity> childs;
		}

		[Serializable]
		public class CompressedChunk
		{
			public Vector3Int position;

			public string voxelDensities;

			public string foliageDensities;
		}

		public class DataSets
		{
			public const byte SingleValue = 0;

			public const byte MultiValue = 1;
		}

		public bool showWater = true;

		public float waterLevel;

		public Color32 topColor = new Color32(103, 115, 51, 1);

		public Color32 dirtColor = new Color32(115, 104, 51, 1);

		public Color32 rockColor = new Color32(77, 87, 91, 1);

		public List<Entity> entities = new List<Entity>();

		public float voxelDensityPrecision;

		public float foliageDensityPrecision;

		public List<CompressedChunk> compressedVoxelChunks = new List<CompressedChunk>();

		public const float defaultVoxelDensityPrecision = 255f;

		public const float defaultFoliageDensityPrecision = 255f;

		private void AddEntities(Level_3 level_3, List<Entity> entities, string parentIdString)
		{
			foreach (Entity entity in entities)
			{
				string text = Guid.NewGuid().ToString();
				level_3.entities.Add(new Level_3.Entity
				{
					objectTypeId = entity.objectTypeId,
					position = entity.position,
					slope = entity.slope,
					rotation = entity.rotation,
					scale = entity.scale,
					id = text,
					parentId = parentIdString
				});
				if (entity.childs != null)
				{
					AddEntities(level_3, entity.childs, text);
				}
			}
		}

		public Level_Latest Upgrade()
		{
			Level_3 level_ = new Level_3();
			level_.showWater = showWater;
			level_.waterLevel = waterLevel;
			AddEntities(level_, entities, Guid.Empty.ToString());
			level_.voxelDensityPrecision = voxelDensityPrecision;
			level_.foliageDensityPrecision = foliageDensityPrecision;
			foreach (CompressedChunk compressedVoxelChunk in compressedVoxelChunks)
			{
				level_.compressedVoxelChunks.Add(new Level_3.CompressedChunk
				{
					position = compressedVoxelChunk.position,
					voxelDensities = compressedVoxelChunk.voxelDensities,
					foliageDensities = compressedVoxelChunk.foliageDensities
				});
			}
			return level_.Upgrade();
		}
	}
}
