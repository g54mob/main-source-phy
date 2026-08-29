using System;
using System.Collections.Generic;

public class ToolTarget : Interactive
{
	public override ItemCheck itemCheck(Item item)
	{
		if (item != null && (item.toolType == ItemTool.AlwaysActive || Array.IndexOf(item.toolTargets, this) != -1))
		{
			return ItemCheck.Passes;
		}
		return ItemCheck.Fails;
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
