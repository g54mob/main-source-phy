using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMGridGeneratorRandomWalkAvoider : MMGridGenerator
	{
		public static int[,] Generate(int width, int height, int seed, int fillPercentage, Vector2Int startingPoint, int[,] obstacles, int obstacleDistance, int maxIterations)
		{
			return null;
		}

		private static bool ObstacleAt(int[,] obstacles, int x, int y)
		{
			return false;
		}

		private static int[,] Carve(int[,] grid, int x, int y, ref int fillCounter)
		{
			return null;
		}
	}
}
