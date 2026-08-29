public static class SpaceDarkest1LogicBinaryExtensions
{
	public static void WriteCratePuzzleSegmentInfo(this FastBinaryWriter writer, SpaceDarkest1Logic.CratePuzzleSegmentInfo data)
	{
		writer.Write(in data.row, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.column, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.startingRow, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.startingColumn, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.rotation, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in data.startingLocalPosition);
		writer.WriteQuaternion(in data.startingLocalRotation);
		int value = (int)data.type;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public static SpaceDarkest1Logic.CratePuzzleSegmentInfo ReadCratePuzzleSegmentInfo(this FastBinaryReader reader)
	{
		return new SpaceDarkest1Logic.CratePuzzleSegmentInfo
		{
			row = reader.ReadInt32(),
			column = reader.ReadInt32(),
			startingRow = reader.ReadInt32(),
			startingColumn = reader.ReadInt32(),
			rotation = reader.ReadSingle(),
			startingLocalPosition = reader.ReadVector3(),
			startingLocalRotation = reader.ReadQuaternion(),
			type = (SpaceDarkest1Logic.CratePuzzleType)reader.ReadInt32()
		};
	}

	public static void WriteCratePuzzleInstruction(this FastBinaryWriter writer, SpaceDarkest1Logic.CratePuzzleInstruction data)
	{
		writer.WriteComponent(data.piece);
		writer.WriteComponent(data.swappedEmpty);
		writer.Write(in data.row, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.column, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.move, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.rotate, default(FastBinaryWriter.ForPrimitives));
	}

	public static SpaceDarkest1Logic.CratePuzzleInstruction ReadCratePuzzleInstruction(this FastBinaryReader reader)
	{
		return new SpaceDarkest1Logic.CratePuzzleInstruction
		{
			piece = reader.ReadComponent<SwapperPiece>(),
			swappedEmpty = reader.ReadComponent<SwapperPiece>(),
			row = reader.ReadInt32(),
			column = reader.ReadInt32(),
			move = reader.ReadInt32(),
			rotate = reader.ReadInt32()
		};
	}
}
