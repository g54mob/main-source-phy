using System.Text;

public sealed class SceneCheckRequestPacket : Packet
{
	public string levelName;

	public string versionToCheck;

	public override byte getTypeId()
	{
		return 17;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(levelName);
		writer.Write(versionToCheck);
	}

	public override void readData(FastBinaryReader reader)
	{
		levelName = reader.ReadString();
		versionToCheck = reader.ReadString();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("levelName: " + ToStringHelper.Stringify(levelName));
		stringBuilder.Append("versionToCheck: " + ToStringHelper.Stringify(versionToCheck));
		return stringBuilder.ToString();
	}
}
