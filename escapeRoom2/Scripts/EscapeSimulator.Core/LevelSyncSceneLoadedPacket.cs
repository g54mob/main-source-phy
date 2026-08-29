using System.Text;

public sealed class LevelSyncSceneLoadedPacket : Packet
{
	public string levelId;

	public override byte getTypeId()
	{
		return 4;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(levelId);
	}

	public override void readData(FastBinaryReader reader)
	{
		levelId = reader.ReadString();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("levelId: " + ToStringHelper.Stringify(levelId));
		return stringBuilder.ToString();
	}
}
