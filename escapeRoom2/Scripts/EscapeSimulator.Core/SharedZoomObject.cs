using System.Collections.Generic;

public class SharedZoomObject : Interactive
{
	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Zoomable;
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.SharedZoom;
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
