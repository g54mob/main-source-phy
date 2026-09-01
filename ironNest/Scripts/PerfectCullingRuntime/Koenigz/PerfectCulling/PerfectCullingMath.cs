using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public static class PerfectCullingMath
	{
		public static int FlattenXYZDouble(int x, int y, int z, Vector3 cellCount)
		{
			return 0;
		}

		public static void UnflattenToXYZ(int index, out int x, out int y, out int z, Vector3 cellCount)
		{
			x = default(int);
			y = default(int);
			z = default(int);
		}

		public static bool IsXYZInBounds(int x, int y, int z, Vector3 cellCount)
		{
			return false;
		}

		public static int CalculateNumberOfCells(Vector3 scale, Vector3 cellSize)
		{
			return 0;
		}

		public static Vector3 CalculateCellCount(Vector3 scale, Vector3 cellSize)
		{
			return default(Vector3);
		}

		public static int GetIndexForWorldPos(Vector3 worldPos, Vector3 gridOrigin, Quaternion gridCurrentOrientation, Vector3 gridScale, Quaternion gridBakeOrientation, Vector3 cellCount, Vector3 cellSize, out bool isOutOfBounds)
		{
			isOutOfBounds = default(bool);
			return 0;
		}
	}
}
