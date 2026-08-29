using System.Text;

public sealed class CharacterPoseLeavePacket : Packet
{
	public CharacterPose pose;

	public override byte getTypeId()
	{
		return 28;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(pose);
	}

	public override void readData(FastBinaryReader reader)
	{
		pose = reader.ReadComponent<CharacterPose>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("pose: " + $"{pose}");
		return stringBuilder.ToString();
	}
}
