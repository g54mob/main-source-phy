using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Trackable : Interactive
{
	[Header("Audio")]
	[Tooltip("Sound played when interaction with this Trackable starts.")]
	[DontSave]
	[HideInInspector]
	public EventReference startSound;

	[Tooltip("Sound played when this Trackable is being moved/dragged.")]
	[DontSave]
	[HideInInspector]
	public EventReference soundTurn;

	[Tooltip("Sound played when interaction with this Trackable ends.")]
	[DontSave]
	[HideInInspector]
	public EventReference endSound;

	[NonSerialized]
	public Vector3 currentValue;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Trackable;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Use;
	}

	public override void init()
	{
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, soundTurn);
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.WriteVector3(in currentValue);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		currentValue = reader.ReadVector3();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentValue",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
