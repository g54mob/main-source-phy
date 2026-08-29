using System;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, ISaveable
{
	public const int RESET_INDEX = -400;

	public const int UNLOCK_INDEX = -500;

	[HideInInspector]
	public bool isUnlocked;

	[HideInInspector]
	public bool isActive = true;

	[DontSave]
	public LockType lockType;

	[DontSave]
	public LockLogicType lockLogicType;

	[DontSave]
	public bool lockNegateValue;

	[HideInInspector]
	[DontSave]
	public RoomEditorLinks onUnlock = new RoomEditorLinks();

	[HideInInspector]
	[DontSave]
	public RoomEditorLinks onLock = new RoomEditorLinks();

	public int[] currentValues;

	public int[] password = Array.Empty<int>();

	[HideInInspector]
	public int nextValueIndex;

	[NonSerialized]
	public int revisionNumber;

	[DontSave]
	public bool exitZoomOnUnlock = true;

	[DontSave]
	public bool deactivateLockOnUnlock;

	public void init()
	{
		currentValues = new int[password.Length];
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in isUnlocked, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isActive, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(currentValues, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(password, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in nextValueIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in revisionNumber, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		isUnlocked = reader.ReadBoolean();
		isActive = reader.ReadBoolean();
		currentValues = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		password = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		nextValueIndex = reader.ReadInt32();
		revisionNumber = reader.ReadInt32();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isUnlocked",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isActive",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentValues[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "password[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "nextValueIndex",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "revisionNumber",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
