using System.Text;
using UnityEngine;

public sealed class EmotePacket : Packet
{
	public Vector2 emoteValue;

	public override byte getTypeId()
	{
		return 88;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteVector2(in emoteValue);
	}

	public override void readData(FastBinaryReader reader)
	{
		emoteValue = reader.ReadVector2();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("emoteValue: " + $"{emoteValue}");
		return stringBuilder.ToString();
	}
}
