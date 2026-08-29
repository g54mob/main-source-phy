using System.Collections.Generic;

public class SecretLevel : LevelLogic, ISaveable
{
	public override void onInit()
	{
	}

	public override void onUpdate()
	{
	}

	public virtual void save(FastBinaryWriter writer)
	{
	}

	public virtual void load(FastBinaryReader reader)
	{
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
	}
}
