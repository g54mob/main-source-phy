using System.Collections.Generic;
using System.Text;

public sealed class HostCustomLevelPickerPacket : Packet
{
	public string packId;

	public List<string> levelIds = new List<string>();

	public override byte getTypeId()
	{
		return 13;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(packId);
		writer.WriteList(levelIds, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
	}

	public override void readData(FastBinaryReader reader)
	{
		packId = reader.ReadString();
		levelIds = reader.ReadList((FastBinaryReader r) => r.ReadString());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("packId: " + ToStringHelper.Stringify(packId));
		stringBuilder.Append("levelIds: " + ToStringHelper.Stringify(levelIds, (string e) => ToStringHelper.Stringify(e)));
		return stringBuilder.ToString();
	}
}
