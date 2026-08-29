using System.Collections.Generic;
using System.Text;

public sealed class HintRequestPacket : Packet
{
	public List<int> interactedPuzzleOrder;

	public override byte getTypeId()
	{
		return 106;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteList(interactedPuzzleOrder, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
	}

	public override void readData(FastBinaryReader reader)
	{
		interactedPuzzleOrder = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("interactedPuzzleOrder: " + ToStringHelper.Stringify(interactedPuzzleOrder, (int e) => $"{e}"));
		return stringBuilder.ToString();
	}
}
