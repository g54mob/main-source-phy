using System;
using UnityEngine;

[Serializable]
public struct MapBounds
{
	public Vector2Int BoundsCenterInMeters;

	public Vector2Int BoundsSizeInMeters;

	public Vector2 GetSizeInUnits()
	{
		return new Vector2((float)BoundsSizeInMeters.x / 0.82397f, (float)BoundsSizeInMeters.y / 0.82397f);
	}

	public Vector2 GetCenterInUnits()
	{
		return new Vector2((float)BoundsCenterInMeters.x / 0.82397f, (float)BoundsCenterInMeters.y / 0.82397f);
	}

	public Vector2 GetMinInUnits()
	{
		Vector2 centerInUnits = GetCenterInUnits();
		Vector2 sizeInUnits = GetSizeInUnits();
		return centerInUnits - sizeInUnits / 2f;
	}

	public Vector2 GetMaxInUnits()
	{
		Vector2 centerInUnits = GetCenterInUnits();
		Vector2 sizeInUnits = GetSizeInUnits();
		return centerInUnits + sizeInUnits / 2f;
	}
}
