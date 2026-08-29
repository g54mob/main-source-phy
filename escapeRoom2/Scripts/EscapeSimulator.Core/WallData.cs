using System;

[Serializable]
public class WallData : PolygonData
{
	public override void Write(FastBinaryWriter writer)
	{
		base.Write(writer);
	}

	public override void Read(FastBinaryReader reader)
	{
		base.Read(reader);
	}
}
