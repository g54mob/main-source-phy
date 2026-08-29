using System.Text;

public sealed class SceneCheckResponsePacket : Packet
{
	public string sceneName;

	public Game.CanLoadSceneResult result;

	public override byte getTypeId()
	{
		return 18;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(sceneName);
		int value = (int)result;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		sceneName = reader.ReadString();
		result = (Game.CanLoadSceneResult)reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("sceneName: " + ToStringHelper.Stringify(sceneName));
		stringBuilder.Append("result: " + $"{result}");
		return stringBuilder.ToString();
	}
}
