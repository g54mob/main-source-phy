using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[ExecuteInEditMode]
public class Switch3D : Interactive
{
	[Header("Switch3D")]
	[DontSave]
	public Switch3DType switchType;

	[DontSave]
	public TweenState tweenState;

	[DontSave]
	public MaterialState materialState;

	[DontSave]
	public Sequence sequence;

	public float transitionSpeed = 1f;

	[DontSave]
	public float hoverSpeed = 1f;

	public bool hasRigidbody;

	[NonSerialized]
	[DontSave]
	internal Rigidbody rigidbody;

	[DontSave]
	public bool autoplay;

	[DontSave]
	public float pauseOnLoop;

	[DontSave]
	public bool disableDragInZoom;

	[HideInInspector]
	public float currentPauseTime;

	[Header("Sound")]
	[DontSave]
	[HideInInspector]
	public EventReference soundTurnOn;

	[DontSave]
	[HideInInspector]
	public EventReference soundTurnOff;

	[DontSave]
	[HideInInspector]
	public EventReference soundStinger;

	[DontSave]
	[HideInInspector]
	public bool playStingerWhenOn = true;

	[ReadOnly]
	public bool playedStinger;

	[Header("Room Editor")]
	[ReadOnly]
	[DontSave]
	public RoomEditorLinks onLinks = new RoomEditorLinks();

	[ReadOnly]
	[DontSave]
	public RoomEditorLinks offLinks = new RoomEditorLinks();

	[DontSave]
	[HideInInspector]
	public bool isAnimation;

	[DontSave]
	public float duration = 1f;

	public const string DOWN_TWEEN_STATE = "Down";

	[NonSerialized]
	internal float currentValue;

	[NonSerialized]
	internal float targetValue;

	public override bool isInternallyTargetable
	{
		get
		{
			if (isAnimation)
			{
				return false;
			}
			if (switchType == Switch3DType.OnlyOnce)
			{
				return currentValue == 0f;
			}
			return true;
		}
	}

	public Switch3DState state
	{
		get
		{
			if (currentValue == 0f)
			{
				return Switch3DState.Off;
			}
			if (currentValue == targetValue && targetValue != 0f)
			{
				return Switch3DState.On;
			}
			return Switch3DState.Moving;
		}
	}

	public float getValue()
	{
		return currentValue;
	}

	public void setValue(float value)
	{
		currentValue = value;
		targetValue = value;
		if (tweenState != null)
		{
			tweenState.setWeight("Down", value);
		}
		if (sequence != null)
		{
			sequence.setTime(value);
		}
	}

	public override void init()
	{
		if (game.isCustomLevel())
		{
			transitionSpeed = 1f / duration;
		}
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Switch3D;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Use;
	}

	private void OnValidate()
	{
		if (tweenState != null && sequence != null)
		{
			Debug.LogError("Switch3D (" + base.name + ") must not have both 'TweenState' and 'Sequence' (only 1 or none)!");
		}
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in transitionSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hasRigidbody, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentPauseTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in playedStinger, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetValue, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		transitionSpeed = reader.ReadSingle();
		hasRigidbody = reader.ReadBoolean();
		currentPauseTime = reader.ReadSingle();
		playedStinger = reader.ReadBoolean();
		currentValue = reader.ReadSingle();
		targetValue = reader.ReadSingle();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "transitionSpeed",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hasRigidbody",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentPauseTime",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playedStinger",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentValue",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "targetValue",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
