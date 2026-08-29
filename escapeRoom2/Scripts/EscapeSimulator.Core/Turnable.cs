using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Turnable : Interactive
{
	[Header("Events")]
	[Space(5f)]
	[DontSave]
	public List<LockArgument> locks;

	[Header("Configuration")]
	[Space(5f)]
	[DontSave]
	public Transform root;

	[DontSave]
	public Vector3 worldAxis;

	[DontSave]
	public Vector2 screenAxis;

	[DontSave]
	public int steps;

	[DontSave]
	public bool useHotspots = true;

	public bool useAngleLimits;

	public float angleLimit1;

	public float angleLimit2;

	public int clickDirection = 1;

	[DontSave]
	public TurnableType type;

	[DontSave]
	public float speed = 1f;

	[Header("Audio")]
	[Tooltip("Sound played when interaction with this Turnable starts.")]
	[DontSave]
	[HideInInspector]
	public EventReference startSound;

	[Tooltip("Sound played when this Turnable is being moved/dragged.")]
	[DontSave]
	[HideInInspector]
	public EventReference soundTurn;

	[Tooltip("Sound played when interaction with this Turnable ends.")]
	[DontSave]
	[HideInInspector]
	public EventReference endSound;

	[DontSave]
	[HideInInspector]
	public EventReference soundTurnHotspot;

	[NonSerialized]
	public float currentRotation;

	[NonSerialized]
	public float rotationGoal;

	[NonSerialized]
	public int lastValue;

	[NonSerialized]
	[DontSave]
	public float interactionDuration;

	[NonSerialized]
	[DontSave]
	public Quaternion originalRotation;

	[NonSerialized]
	[DontSave]
	public Renderer targetRenderer;

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
				return totalAngle / (float)steps;
			}
			return totalAngle / (float)(steps - 1);
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
			steps = Mathf.Max(steps, (!useAngleLimits) ? 1 : 2);
			int num = Mathf.RoundToInt((currentRotation - minAngle) / stepAngle) % steps;
			if (num < 0)
			{
				return num + steps;
			}
			return num;
		}
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Turnable;
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
		targetRenderer = rootTransform().GetComponentInChildren<Renderer>();
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, soundTurn);
	}

	private void OnDrawGizmosSelected()
	{
		if (useAngleLimits)
		{
			drawAngleCone(base.transform.position, worldAxis, angleLimit1, angleLimit2, 0.3f);
		}
		void drawAngleCone(Vector3 origin, Vector3 axis, float startAngle, float endAngle, float radius)
		{
			BoxCollider componentInChildren = GetComponentInChildren<BoxCollider>();
			Vector3 vector = new Vector3(componentInChildren.center.x * Mathf.Abs(axis.x), componentInChildren.center.y * Mathf.Abs(axis.y), componentInChildren.center.z * Mathf.Abs(axis.z));
			Vector3 vector2 = base.transform.TransformDirection(vector);
			Vector3 vector3 = base.transform.position + vector2;
			axis = base.transform.TransformDirection(axis);
			axis = axis.normalized;
			float a = MathF.PI / 180f * startAngle;
			float b = MathF.PI / 180f * endAngle;
			int num = 20;
			Vector3 direction = componentInChildren.center - vector;
			direction = base.transform.TransformDirection(direction);
			direction.Normalize();
			Vector3 vector4 = Quaternion.AngleAxis(startAngle, axis) * direction;
			Vector3 vector5 = Quaternion.AngleAxis(endAngle, axis) * direction;
			Vector3 vector6 = vector3 + vector4 * radius;
			Gizmos.color = Color.yellow;
			for (int i = 1; i <= num; i++)
			{
				float t = (float)i / (float)num;
				float num2 = Mathf.Lerp(a, b, t);
				Vector3 vector7 = vector3 + Quaternion.AngleAxis(57.29578f * num2, axis) * direction * radius;
				Gizmos.DrawLine(vector6, vector7);
				vector6 = vector7;
			}
			Gizmos.DrawLine(vector3, base.transform.position + vector4 * radius + vector2);
			Gizmos.DrawLine(vector3, base.transform.position + vector5 * radius + vector2);
		}
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in useAngleLimits, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in angleLimit1, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in angleLimit2, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in clickDirection, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentRotation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in rotationGoal, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in lastValue, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		useAngleLimits = reader.ReadBoolean();
		angleLimit1 = reader.ReadSingle();
		angleLimit2 = reader.ReadSingle();
		clickDirection = reader.ReadInt32();
		currentRotation = reader.ReadSingle();
		rotationGoal = reader.ReadSingle();
		lastValue = reader.ReadInt32();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
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
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "angleLimit1",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "angleLimit2",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num3 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "clickDirection",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentRotation",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "rotationGoal",
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
