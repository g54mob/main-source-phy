using System.Text;
using UnityEngine;

public sealed class FinishLevelChangeFacePacket : Packet
{
	public Vector2 faceAnim;

	public override byte getTypeId()
	{
		return 119;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteVector2(in faceAnim);
	}

	public override void readData(FastBinaryReader reader)
	{
		faceAnim = reader.ReadVector2();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("faceAnim: " + $"{faceAnim}");
		return stringBuilder.ToString();
	}
}
