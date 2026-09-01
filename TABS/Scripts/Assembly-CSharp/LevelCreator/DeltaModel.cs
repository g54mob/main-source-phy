using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class DeltaModel
	{
		public int HistoryId;

		public Dictionary<Vector3Int, Level.VolumeChunk> previousVolumeChunks;

		public Dictionary<Vector3Int, Level.VolumeChunk> nextVolumeChunks;

		public List<Level.FlatEntity> NewEntities;

		public List<Level.FlatEntity> OldEntities;
	}
}
