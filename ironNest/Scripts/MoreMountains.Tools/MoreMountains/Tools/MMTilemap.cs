using UnityEngine;
using UnityEngine.Tilemaps;

namespace MoreMountains.Tools
{
	public class MMTilemap : MonoBehaviour
	{
		public static Vector2 GetRandomPosition(Tilemap targetTilemap, Grid grid, int width, int height, bool shouldBeFilled = true, int maxIterations = 1000)
		{
			return default(Vector2);
		}

		public static Vector2 GetRandomPositionOnGround(Tilemap targetTilemap, Grid grid, int width, int height, int startingHeight, int xMin, int xMax, bool shouldBeFilled = true, int maxIterations = 1000)
		{
			return default(Vector2);
		}
	}
}
