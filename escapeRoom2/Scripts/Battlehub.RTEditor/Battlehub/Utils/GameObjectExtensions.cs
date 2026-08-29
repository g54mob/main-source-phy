using System;
using UnityEngine;

namespace Battlehub.Utils
{
	public static class GameObjectExtensions
	{
		public static bool IsPrefab(this GameObject go)
		{
			if (go == null)
			{
				return false;
			}
			if (Application.isEditor && !Application.isPlaying)
			{
				throw new InvalidOperationException("Does not work in edit mode");
			}
			if (go.scene.buildIndex < 0)
			{
				return go.scene.path == null;
			}
			return false;
		}

		public static Bounds CalculateBounds(this GameObject g)
		{
			return g.transform.CalculateBounds();
		}
	}
}
