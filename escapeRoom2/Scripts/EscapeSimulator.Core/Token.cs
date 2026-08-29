using System.Collections.Generic;

public class Token : Interactive
{
	[DontSave]
	public bool overridePickedUpTexture;

	public int roomEditorId;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Token;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Use;
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in roomEditorId, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		roomEditorId = reader.ReadInt32();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "roomEditorId",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
