using System;
using System.Text;

[Serializable]
public class FloorData : PolygonData
{
	public float wallHeight = 3f;

	public float wallThickness;

	public bool connectWalls = true;

	public override void Write(FastBinaryWriter writer)
	{
		base.Write(writer);
		writer.Write(in wallHeight, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in wallThickness, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in connectWalls, default(FastBinaryWriter.ForPrimitives));
	}

	public override void Read(FastBinaryReader reader)
	{
		base.Read(reader);
		wallHeight = reader.ReadSingle();
		wallThickness = reader.ReadSingle();
		connectWalls = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("wallHeight: " + $"{wallHeight}");
		stringBuilder.AppendLine("wallThickness: " + $"{wallThickness}");
		stringBuilder.Append("connectWalls: " + $"{connectWalls}");
		return stringBuilder.ToString();
	}
}
