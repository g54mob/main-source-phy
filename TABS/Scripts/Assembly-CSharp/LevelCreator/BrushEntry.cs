using System;
using UnityEngine;

namespace LevelCreator
{
	public class BrushEntry
	{
		public Func<Vector3Int, BrushInfo, Brush> mCreateBrush;

		public Vector3Int mStandardSize;
	}
}
