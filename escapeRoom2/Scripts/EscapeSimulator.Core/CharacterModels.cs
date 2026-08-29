using System.Collections.Generic;

public class CharacterModels : Interactive
{
	[DontSave]
	public CharacterBuild[] builds;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.OtherPlayer;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Human;
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
