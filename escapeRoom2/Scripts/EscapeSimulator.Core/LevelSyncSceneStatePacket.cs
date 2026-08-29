using System.Collections.Generic;
using System.Text;

public sealed class LevelSyncSceneStatePacket : Packet
{
	public byte[] levelState;

	public List<int> foundTokens;

	public override byte getTypeId()
	{
		return 5;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteByteArray(levelState);
		writer.WriteList(foundTokens, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
	}

	public override void readData(FastBinaryReader reader)
	{
		levelState = reader.ReadByteArray();
		foundTokens = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("levelState: " + ToStringHelper.Stringify(levelState, (byte e) => $"{e}"));
		stringBuilder.Append("foundTokens: " + ToStringHelper.Stringify(foundTokens, (int e) => $"{e}"));
		return stringBuilder.ToString();
	}
}
