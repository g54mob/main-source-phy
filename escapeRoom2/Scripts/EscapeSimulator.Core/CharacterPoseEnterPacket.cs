using System.Text;

public sealed class CharacterPoseEnterPacket : Packet
{
	public CharacterPose pose;

	public bool transitionIn;

	public override byte getTypeId()
	{
		return 27;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(pose);
		writer.Write(in transitionIn, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		pose = reader.ReadComponent<CharacterPose>();
		transitionIn = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("pose: " + $"{pose}");
		stringBuilder.Append("transitionIn: " + $"{transitionIn}");
		return stringBuilder.ToString();
	}
}
