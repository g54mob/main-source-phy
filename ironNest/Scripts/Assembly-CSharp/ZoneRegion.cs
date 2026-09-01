using System;

[Serializable]
public class ZoneRegion
{
	public enum RegionTypes
	{
		Add = 0,
		Subtract = 1
	}

	public RegionTypes RegionType;

	public GridReference BottomLeft;

	public float Width;

	public float Height;
}
