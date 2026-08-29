using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Dial : Interactive
{
	public enum ControllerDialType
	{
		Vertical = 0,
		Horizontal = 1
	}

	[Header("Events")]
	[Space(5f)]
	[DontSave]
	public List<LockArgument> locks;

	[Header("Configuration")]
	[Space(5f)]
	[DontSave]
	public Transform root;

	[DontSave]
	public Vector3 rotationAxis = Vector3.up;

	[DontSave]
	public int valueCount = 20;

	[DontSave]
	public bool snapToPosition;

	[DontSave]
	public bool soundOnPoints = true;

	[DontSave]
	public bool soundOnEveryValue;

	[DontSave]
	public int soundTurnStopDelay;

	public int clickDirection = 1;

	public bool useAngleLimits;

	public float angleLimit1;

	public float angleLimit2;

	[DontSave]
	public TweenState tweenState;

	[DontSave]
	public MaterialState materialState;

	[DontSave]
	public float hoverSpeed = 1f;

	[Header("Controller specific settings")]
	[Tooltip("Vertical should be used on vertically set dials, Horizontal should be used on horizontally set dials.")]
	[DontSave]
	public ControllerDialType controllerDialType;

	[Tooltip("Factor by which final angle will be scaled by. Making this value negative will invert dial movement.")]
	[DontSave]
	public float controllerRotationSpeed = 1f;

	[Tooltip("Used to determine which direction should dial rotate based on player position when using left and right mode.")]
	[DontSave]
	public Vector3 originToHandleDirection = Vector3.forward;

	[Header("VR specific settings")]
	[Tooltip("If true, this dial can only be tweaked manually by hand (as opposed to at a distance by raycast).")]
	[DontSave]
	public bool vrHandOnly;

	[Tooltip("If true, this dial can be tweaked via grip while held with other hand (instead of grip taking item).")]
	[DontSave]
	public bool vrHasPriorityInHand;

	[Tooltip("Sound played when interaction with this Dial starts.")]
	[DontSave]
	[HideInInspector]
	public EventReference startSound;

	[Tooltip("Sound played when this Dial is being moved/dragged.")]
	[DontSave]
	[HideInInspector]
	public EventReference soundTurn;

	[Tooltip("Sound played when interaction with this Dial ends.")]
	[DontSave]
	[HideInInspector]
	public EventReference endSound;

	[DontSave]
	[HideInInspector]
	public EventReference soundTurnHotspot;

	[NonSerialized]
	public float currentAngle;

	[NonSerialized]
	public float targetAngle;

	[NonSerialized]
	public int lastValue;

	[NonSerialized]
	[DontSave]
	public float angleChange;

	[NonSerialized]
	[DontSave]
	public Quaternion originalRotation;

	[NonSerialized]
	[DontSave]
	public Vector3 cursorViewPosLast;

	[NonSerialized]
	[DontSave]
	public float currentControllerRotationSpeed;

	[NonSerialized]
	[DontSave]
	public int dialAudioTurnOffDelay;

	[NonSerialized]
	[DontSave]
	public float dialControllerTimeAtZero;

	[NonSerialized]
	[DontSave]
	public float interactionDuration;

	public float minAngle
	{
		get
		{
			if (!useAngleLimits)
			{
				return 0f;
			}
			return Mathf.Min(angleLimit1, angleLimit2);
		}
	}

	public float maxAngle
	{
		get
		{
			if (!useAngleLimits)
			{
				return 360f;
			}
			return Mathf.Max(angleLimit1, angleLimit2);
		}
	}

	public float stepAngle
	{
		get
		{
			if (!useAngleLimits)
			{
				return totalAngle / (float)valueCount;
			}
			return totalAngle / (float)(valueCount - 1);
		}
	}

	public float totalAngle
	{
		get
		{
			if (!useAngleLimits)
			{
				return 360f;
			}
			return Mathf.Abs(angleLimit1 - angleLimit2);
		}
	}

	public bool hasValueChanged => lastValue != value;

	public int value
	{
		get
		{
			valueCount = Mathf.Max(valueCount, (!useAngleLimits) ? 1 : 2);
			int num = Mathf.RoundToInt((currentAngle - minAngle) / stepAngle) % valueCount;
			if (num < 0)
			{
				return num + valueCount;
			}
			return num;
		}
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Dial;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Use;
	}

	public Transform rootTransform()
	{
		if (!(root != null))
		{
			return base.transform;
		}
		return root;
	}

	public override void init()
	{
		originalRotation = rootTransform().localRotation;
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, soundTurn);
		if (valueCount == 0)
		{
			valueCount = 1;
			Debug.LogError("Setting Dial value count to 1 to avoid divide by 0!");
		}
	}

	public void setValue(int value)
	{
		currentAngle = (float)value * 360f / (float)valueCount;
		rootTransform().localRotation = originalRotation * Quaternion.Euler(rotationAxis * currentAngle);
	}

	public Plane getRotationPlane()
	{
		Vector3 inNormal = base.transform.TransformDirection(rotationAxis);
		Vector3 position = base.transform.position;
		return new Plane(inNormal, position);
	}

	private void OnDrawGizmosSelected()
	{
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in clickDirection, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useAngleLimits, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in angleLimit1, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in angleLimit2, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentAngle, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetAngle, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in lastValue, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		clickDirection = reader.ReadInt32();
		useAngleLimits = reader.ReadBoolean();
		angleLimit1 = reader.ReadSingle();
		angleLimit2 = reader.ReadSingle();
		currentAngle = reader.ReadSingle();
		targetAngle = reader.ReadSingle();
		lastValue = reader.ReadInt32();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "clickDirection",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "useAngleLimits",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "angleLimit1",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "angleLimit2",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentAngle",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "targetAngle",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num6 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "lastValue",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
