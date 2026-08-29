using System.Collections.Generic;
using System.Text;

public sealed class EditorPuzzleCompletedPacket : Packet
{
	public List<EditorPuzzle> solvedPuzzles;

	public override byte getTypeId()
	{
		return 110;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteList(solvedPuzzles, delegate(FastBinaryWriter w, EditorPuzzle e)
		{
			w.WriteComponent(e);
		});
	}

	public override void readData(FastBinaryReader reader)
	{
		solvedPuzzles = reader.ReadList((FastBinaryReader r) => r.ReadComponent<EditorPuzzle>());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("solvedPuzzles: " + ToStringHelper.Stringify(solvedPuzzles, (EditorPuzzle e) => $"{e}"));
		return stringBuilder.ToString();
	}
}
