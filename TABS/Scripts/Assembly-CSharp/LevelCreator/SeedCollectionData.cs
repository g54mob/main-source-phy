using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelCreator
{
	[Serializable]
	public class SeedCollectionData
	{
		[FormerlySerializedAs("EditorObjectId")]
		public string editorObjectId;

		[FormerlySerializedAs("CountMultiplier")]
		public int countMultiplier = 1;

		[FormerlySerializedAs("ScaleMultiplierMinMax")]
		public Vector2 scaleMultiplierMinMax = new Vector2(0.7f, 1.3f);

		[FormerlySerializedAs("DownOffsetMinMax")]
		public Vector2 downOffsetMinMax = new Vector2(0.1f, 0.3f);

		[FormerlySerializedAs("SplitForceMinMax")]
		public Vector2 splitForceMinMax = new Vector2(4f, 6f);
	}
}
