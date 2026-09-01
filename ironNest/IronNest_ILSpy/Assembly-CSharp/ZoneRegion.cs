using System;

[Serializable]
public class ZoneRegion
{
	public enum RegionTypes
	{
		Add,
		Subtract
	}

	public RegionTypes RegionType;

	public GridReference BottomLeft;

	public float Width = 10f;

	public float Height = 10f;
}
