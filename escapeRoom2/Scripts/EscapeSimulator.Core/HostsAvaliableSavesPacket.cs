using System.Collections.Generic;
using System.Text;

public sealed class HostsAvaliableSavesPacket : Packet
{
	public List<string> levelsWithSaves = new List<string>();

	public override byte getTypeId()
	{
		return 21;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteList(levelsWithSaves, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
	}

	public override void readData(FastBinaryReader reader)
	{
		levelsWithSaves = reader.ReadList((FastBinaryReader r) => r.ReadString());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("levelsWithSaves: " + ToStringHelper.Stringify(levelsWithSaves, (string e) => ToStringHelper.Stringify(e)));
		return stringBuilder.ToString();
	}
}
