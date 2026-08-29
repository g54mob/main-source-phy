using System.Collections.Generic;
using System.Text;

public sealed class RequestAdvancedSearchPacket : Packet
{
	public List<string> tagsToInclude = new List<string>();

	public List<string> tagsToExclude = new List<string>();

	public int sortIndex;

	public int daysIndex;

	public override byte getTypeId()
	{
		return 14;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteList(tagsToInclude, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
		writer.WriteList(tagsToExclude, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
		writer.Write(in sortIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in daysIndex, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		tagsToInclude = reader.ReadList((FastBinaryReader r) => r.ReadString());
		tagsToExclude = reader.ReadList((FastBinaryReader r) => r.ReadString());
		sortIndex = reader.ReadInt32();
		daysIndex = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("tagsToInclude: " + ToStringHelper.Stringify(tagsToInclude, (string e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("tagsToExclude: " + ToStringHelper.Stringify(tagsToExclude, (string e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("sortIndex: " + $"{sortIndex}");
		stringBuilder.Append("daysIndex: " + $"{daysIndex}");
		return stringBuilder.ToString();
	}
}
