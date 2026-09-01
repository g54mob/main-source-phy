using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zone
{
	public enum ZoneShapes
	{
		Single = 0,
		Composite = 1
	}

	public string ID;

	public string Name;

	public EntityRoles Role;

	public ZoneShapes ZoneShape;

	public GridReference BottomLeft;

	public float Width;

	public float Height;

	public List<ZoneRegion> Regions;

	public bool Contains(GridReference location)
	{
		return false;
	}

	private static bool ContainsRect(int x, int y, GridReference bottomLeft, float width, float height)
	{
		return false;
	}

	public static float DistanceToZone(GridReference location, Zone zone, FireMission fireMission, Vector3[] gridBounds)
	{
		return 0f;
	}

	public static GridReference Offset(GridReference origin, int offsetX, int offsetY)
	{
		return null;
	}

	private static void Decode(GridReference bl, out int baseX, out int baseY)
	{
		baseX = default(int);
		baseY = default(int);
	}

	public GridReference GetRandomGridPosition(System.Random rng)
	{
		return null;
	}

	public void ZoneToWorldCorners(Vector3[] gridBounds, ref Vector3[] corners)
	{
	}

	public void RegionToWorldCorners(ZoneRegion region, Vector3[] gridBounds, ref Vector3[] corners)
	{
	}
}
