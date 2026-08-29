using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Joystick : Interactive
{
	public enum JoystickAxis
	{
		X = 0,
		Y = 1,
		Z = 2,
		MinusX = 3,
		MinusY = 4,
		MinusZ = 5
	}

	[Header("Configuration")]
	[DontSave]
	public Vector2 axisAngleLimits;

	[DontSave]
	public JoystickAxis horizontalAxis;

	[DontSave]
	public JoystickAxis verticalAxis = JoystickAxis.Y;

	[DontSave]
	public float speed = 1f;

	[DontSave]
	public float returnSpeed = 2f;

	[Header("Audio")]
	[DontSave]
	public EventReference soundBegin;

	[DontSave]
	public EventReference soundMove;

	[DontSave]
	public EventReference soundEnd;

	[NonSerialized]
	[DontSave]
	public Quaternion initialRotation;

	[NonSerialized]
	public Vector2 currentRotation;

	[NonSerialized]
	public Vector2 rotationOnBackgroundStart;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Joystick;
	}

	public override void init()
	{
		initialRotation = base.transform.localRotation;
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, soundMove);
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.WriteVector2(in currentRotation);
		writer.WriteVector2(in rotationOnBackgroundStart);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		currentRotation = reader.ReadVector2();
		rotationOnBackgroundStart = reader.ReadVector2();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		Vector2 vector = reader.ReadVector2();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentRotation",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector2 vector2 = reader.ReadVector2();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "rotationOnBackgroundStart",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
