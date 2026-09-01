using System;
using UnityEngine;

[Serializable]
public class GridReference
{
	public GridLocations Location;

	public int X;

	public int Y;

	public override string ToString()
	{
		return null;
	}

	public void RandomiseSubGrid()
	{
	}

	public Vector3 GetLocation(Vector3[] gridBounds, bool fuzzyLocation = false)
	{
		return default(Vector3);
	}

	public Vector3 GetLocationBottomLeft(Vector3[] gridBounds)
	{
		return default(Vector3);
	}

	public static GridReference FromLocalSpace(Vector2 localSpace, float cellWidth, float cellHeight, bool yIncreasesUp)
	{
		return null;
	}

	public static GridReference FromLocalSpace(Vector3 localSpace, float cellWidth, float cellHeight, bool yIncreasesUp)
	{
		return null;
	}

	public void Move(Vector2Int delta)
	{
	}

	public static Vector3 ClampToGridBounds(Vector3 targetPos, Vector3[] gridBounds)
	{
		return default(Vector3);
	}

	public static Vector3 ClampToGridBoundsLocal(Vector3 localTargetPos, Transform localSpace, Vector3[] worldBounds)
	{
		return default(Vector3);
	}
}
