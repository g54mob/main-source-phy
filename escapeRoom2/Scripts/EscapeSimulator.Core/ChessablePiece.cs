using System;
using System.Collections.Generic;

public class ChessablePiece : Interactive
{
	[NonSerialized]
	[DontSave]
	public Chessable chessable;

	[ReadOnly]
	[DontSave]
	public char symbol;

	public void initChessable(Chessable chessable)
	{
		this.chessable = chessable;
		symbol = base.name[0];
	}

	public bool isWhite()
	{
		return char.IsUpper(symbol);
	}

	public string getLocalizationKey()
	{
		return symbol.ToString().ToLower() switch
		{
			"p" => "ChessPawn", 
			"n" => "ChessKnight", 
			"b" => "ChessBishop", 
			"r" => "ChessRook", 
			"q" => "ChessQueen", 
			"k" => "ChessKing", 
			_ => "", 
		};
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.ChessablePiece;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		if (!chessable.targetable)
		{
			return Game.PCCrosshair.Locked;
		}
		return base.getBaseCursor();
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
	}
}
