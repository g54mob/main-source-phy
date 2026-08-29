using System.Text;

public sealed class SceneLoadPacket : Packet
{
	public string sceneName;

	public override byte getTypeId()
	{
		return 19;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(sceneName);
	}

	public override void readData(FastBinaryReader reader)
	{
		sceneName = reader.ReadString();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("sceneName: " + ToStringHelper.Stringify(sceneName));
		return stringBuilder.ToString();
	}
}
