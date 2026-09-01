using UnityEngine;

namespace LevelCreator
{
	public class Brush
	{
		public float[,,] Field;

		public Vector3 Pivot;

		public bool UsingTextures;

		public Vector3Int Size => new Vector3Int(Field.GetLength(2), Field.GetLength(1), Field.GetLength(0));
	}
}
