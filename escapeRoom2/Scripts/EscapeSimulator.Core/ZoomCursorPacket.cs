using System.Text;
using UnityEngine;

public sealed class ZoomCursorPacket : Packet
{
	public Vector2 screenPositionFromCenter;

	public float screenHeight;

	public override byte getTypeId()
	{
		return 90;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteVector2(in screenPositionFromCenter);
		writer.Write(in screenHeight, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		screenPositionFromCenter = reader.ReadVector2();
		screenHeight = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("screenPositionFromCenter: " + $"{screenPositionFromCenter}");
		stringBuilder.Append("screenHeight: " + $"{screenHeight}");
		return stringBuilder.ToString();
	}
}
