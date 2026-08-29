using System.Collections.Generic;

public class ChessableTile : Interactive
{
	[DontSave]
	public Chessable chessable;

	[ReadOnly]
	public string tileId;

	public void initChessable(Chessable chessable)
	{
		this.chessable = chessable;
		tileId = base.name + base.transform.parent.name;
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.ChessableTile;
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
		writer.Write(tileId);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		tileId = reader.ReadString();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		string text = reader.ReadString();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tileId",
			fieldValue = (text ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
