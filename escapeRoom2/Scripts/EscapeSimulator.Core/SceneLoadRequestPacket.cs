using System.Text;

public sealed class SceneLoadRequestPacket : Packet
{
	public string sceneName;

	public bool shouldLoadLatestSave;

	public override byte getTypeId()
	{
		return 16;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(sceneName);
		writer.Write(in shouldLoadLatestSave, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		sceneName = reader.ReadString();
		shouldLoadLatestSave = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("sceneName: " + ToStringHelper.Stringify(sceneName));
		stringBuilder.Append("shouldLoadLatestSave: " + $"{shouldLoadLatestSave}");
		return stringBuilder.ToString();
	}
}
