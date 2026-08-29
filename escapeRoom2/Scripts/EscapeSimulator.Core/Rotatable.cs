using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Rotatable : Interactive
{
	[Header("Interacting configuration")]
	[DontSave]
	public float speed = 10f;

	[Tooltip("Sound played when interaction with this Rotatable starts.")]
	[DontSave]
	[HideInInspector]
	public EventReference startSound;

	[Tooltip("Sound played when this Rotatable is being turned.")]
	[DontSave]
	[HideInInspector]
	public EventReference turnSound;

	[Tooltip("Sound played when interaction with this Rotatable ends.")]
	[DontSave]
	[HideInInspector]
	public EventReference endSound;

	[Tooltip("Turn sound will be stopped if no turning is detected for this many frames. If negative, it will always be played while holding.")]
	[DontSave]
	[HideInInspector]
	public int idleFramesToStopTurnSound = 10;

	[NonSerialized]
	public int idleFrames;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Rotatable;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Rotate;
	}

	public override void init()
	{
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, turnSound);
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in idleFrames, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		idleFrames = reader.ReadInt32();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "idleFrames",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
