using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace MoreMountains.Tools
{
	public class MMGridGenerator
	{
		public static int[,] PrepareGrid(ref int width, ref int height)
		{
			return null;
		}

		public static bool SetGridCoordinate(int[,] grid, int x, int y, int value)
		{
			return false;
		}

		public static int[,] TilemapToGrid(Tilemap tilemap, int width, int height)
		{
			return null;
		}

		public static void DebugGrid(int[,] grid, int width, int height)
		{
		}

		public static int GetValueAtGridCoordinate(int[,] grid, int x, int y, int errorValue)
		{
			return 0;
		}

		public static int[,] InvertGrid(int[,] grid)
		{
			return null;
		}

		public static int[,] SmoothenGrid(int[,] grid)
		{
			return null;
		}

		public static int[,] ApplySafeSpots(int[,] grid, List<MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot> safeSpots)
		{
			return null;
		}

		public static int[,] BindGrid(int[,] grid, bool top, bool bottom, bool left, bool right)
		{
			return null;
		}

		public static int GetAdjacentWallsCount(int[,] grid, int x, int y)
		{
			return 0;
		}
	}
}
