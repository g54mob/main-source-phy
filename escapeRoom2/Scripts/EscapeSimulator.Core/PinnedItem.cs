using System;
using System.Collections.Generic;
using UnityEngine;

public class PinnedItem : Interactive
{
	public Vector3[] cornersLocal;

	public Vector3 xLocal;

	[NonSerialized]
	[DontSave]
	public Vector3 originalScale = Vector3.one;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.PinnedItem;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Draggable;
	}

	public override ItemCheck itemCheck(Item item)
	{
		if (item != null)
		{
			return ItemCheck.Passes;
		}
		return ItemCheck.NotRequired;
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.WriteArray(cornersLocal, delegate(FastBinaryWriter w, Vector3 e)
		{
			w.WriteVector3(in e);
		});
		writer.WriteVector3(in xLocal);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		cornersLocal = reader.ReadArray((FastBinaryReader r) => r.ReadVector3());
		xLocal = reader.ReadVector3();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		Vector3[] array = reader.ReadArray((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cornersLocal[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "xLocal",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
