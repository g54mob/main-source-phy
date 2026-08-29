public static class ChessableBinaryExtensions
{
	public static void WritePiecePosition(this FastBinaryWriter writer, Chessable.PiecePosition data)
	{
		writer.WriteComponent(data.piece);
		writer.WriteComponent(data.tile);
	}

	public static Chessable.PiecePosition ReadPiecePosition(this FastBinaryReader reader)
	{
		return new Chessable.PiecePosition
		{
			piece = reader.ReadComponent<ChessablePiece>(),
			tile = reader.ReadComponent<ChessableTile>()
		};
	}
}
