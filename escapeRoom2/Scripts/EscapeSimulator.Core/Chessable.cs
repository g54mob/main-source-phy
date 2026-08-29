using System;
using System.Collections.Generic;

public class Chessable : Interactive
{
	public class PiecePosition
	{
		public ChessablePiece piece;

		public ChessableTile tile;

		public PiecePosition(ChessablePiece piece, ChessableTile tile)
		{
			this.piece = piece;
			this.tile = tile;
		}

		public PiecePosition()
		{
		}
	}

	public enum PieceState
	{
		None = 0,
		SelectingPiece = 1,
		DeselectingPiece = 2,
		MovingPiece = 3
	}

	[NonSerialized]
	[DontSave]
	public List<ChessableTile> tiles = new List<ChessableTile>();

	[NonSerialized]
	[DontSave]
	public List<ChessablePiece> pieces = new List<ChessablePiece>();

	[NonSerialized]
	public List<PiecePosition> piecesOnTiles = new List<PiecePosition>();

	[NonSerialized]
	public ChessablePiece currentSelectedPiece;

	[NonSerialized]
	public ChessableTile moveTile;

	[NonSerialized]
	public PieceState pieceState;

	private const string INITIAL_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

	public override void init()
	{
		tiles = new List<ChessableTile>(GetComponentsInChildren<ChessableTile>());
		pieces = new List<ChessablePiece>(GetComponentsInChildren<ChessablePiece>());
		tiles.ForEach(delegate(ChessableTile x)
		{
			x.initChessable(this);
		});
		pieces.ForEach(delegate(ChessablePiece x)
		{
			x.initChessable(this);
		});
		setFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
	}

	public ChessableTile getTileForPiece(ChessablePiece piece)
	{
		return piecesOnTiles.Find((PiecePosition x) => x.piece == piece).tile;
	}

	public ChessablePiece getPieceForTile(ChessableTile tile)
	{
		return piecesOnTiles.Find((PiecePosition x) => x.tile == tile)?.piece;
	}

	public void setFen(string fen)
	{
		pieces.ForEach(delegate(ChessablePiece x)
		{
			x.gameObject.SetActive(value: true);
		});
		string[] array = fen.Split(' ')[0].Split('/');
		int row = 8;
		piecesOnTiles.Clear();
		List<ChessablePiece> usedPieces = new List<ChessablePiece>();
		string[] array2 = array;
		foreach (string obj in array2)
		{
			int col = 1;
			string text = obj;
			foreach (char symbol in text)
			{
				if (char.IsDigit(symbol))
				{
					col += (int)char.GetNumericValue(symbol);
					continue;
				}
				ChessableTile chessableTile = tiles.Find((ChessableTile t) => t.tileId == $"{(char)(97 + col - 1)}{row}");
				if (chessableTile != null)
				{
					ChessablePiece chessablePiece = pieces.Find((ChessablePiece p) => p.symbol == symbol && !usedPieces.Contains(p));
					if (chessablePiece != null)
					{
						chessablePiece.transform.position = chessableTile.transform.position;
						usedPieces.Add(chessablePiece);
						piecesOnTiles.Add(new PiecePosition(chessablePiece, chessableTile));
					}
				}
				int num3 = col;
				col = num3 + 1;
			}
			int num2 = row;
			row = num2 - 1;
		}
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.WriteList(piecesOnTiles, delegate(FastBinaryWriter w, PiecePosition e)
		{
			w.WritePiecePosition(e);
		});
		writer.WriteComponent(currentSelectedPiece);
		writer.WriteComponent(moveTile);
		int value = (int)pieceState;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		piecesOnTiles = reader.ReadList((FastBinaryReader r) => r.ReadPiecePosition());
		currentSelectedPiece = reader.ReadComponent<ChessablePiece>();
		moveTile = reader.ReadComponent<ChessableTile>();
		pieceState = (PieceState)reader.ReadInt32();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		List<PiecePosition> list = reader.ReadList((FastBinaryReader r) => r.ReadPiecePosition());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "piecesOnTiles[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ChessablePiece arg = reader.ReadComponent<ChessablePiece>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSelectedPiece",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ChessableTile arg2 = reader.ReadComponent<ChessableTile>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "moveTile",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		PieceState pieceState = (PieceState)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pieceState",
			fieldValue = $"{pieceState}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
