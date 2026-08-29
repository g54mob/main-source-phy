using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointToPose : MonoBehaviour, ISaveable
{
	[NonSerialized]
	public bool active = true;

	[DontSave]
	public CharacterPose characterPose;

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in active, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		active = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "active",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
