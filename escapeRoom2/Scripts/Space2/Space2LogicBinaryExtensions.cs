using UnityEngine;

public static class Space2LogicBinaryExtensions
{
	public static void WriteStarchartLine(this FastBinaryWriter writer, Space2Logic.StarchartLine data)
	{
		writer.Write(in data.point1, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.point2, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.lineIndex, default(FastBinaryWriter.ForPrimitives));
	}

	public static Space2Logic.StarchartLine ReadStarchartLine(this FastBinaryReader reader)
	{
		return new Space2Logic.StarchartLine
		{
			point1 = reader.ReadInt32(),
			point2 = reader.ReadInt32(),
			lineIndex = reader.ReadInt32()
		};
	}

	public static void WriteAuxNumber(this FastBinaryWriter writer, Space2Logic.AuxNumber data)
	{
		writer.WriteArray(data.numbers, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
	}

	public static Space2Logic.AuxNumber ReadAuxNumber(this FastBinaryReader reader)
	{
		return new Space2Logic.AuxNumber
		{
			numbers = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject())
		};
	}
}
