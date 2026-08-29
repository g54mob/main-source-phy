using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Pine/Draggable")]
public class Draggable : Interactive
{
	public enum ManipulationType
	{
		Horizontal = 0,
		HorizontalNoRotation = 1,
		Free = 2,
		OnlyRotation = 3
	}

	public enum ForcePoint
	{
		HitPoint = 0,
		CenterOfMass = 1,
		CustomLocal = 2
	}

	[Header("Physics")]
	public RigidbodyConstraints dragConstraintFlags;

	[DontSave]
	public ManipulationType manipulationType;

	[DontSave]
	public ForcePoint forcePoint;

	[DontSave]
	public Vector3 customLocal = Vector3.zero;

	[DontSave]
	public float angularDragOverride = 1f;

	[NonSerialized]
	[DontSave]
	public Vector3 originalUp = Vector3.zero;

	[DontSave]
	[HideInInspector]
	public EventReference soundDragStart;

	[DontSave]
	[HideInInspector]
	public EventReference soundDragLoop;

	[DontSave]
	[HideInInspector]
	public EventReference soundDragEnd;

	[DontSave]
	[HideInInspector]
	public EventReference soundHit;

	public bool isKinematic;

	[NonSerialized]
	[DontSave]
	public Rigidbody draggableRigidbody;

	[NonSerialized]
	[DontSave]
	internal TransformSync transformSync;

	[NonSerialized]
	[DontSave]
	internal float idleTime;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Draggable;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Draggable;
	}

	public override void init()
	{
		originalUp = base.transform.InverseTransformDirection(Vector3.up);
		draggableRigidbody = GetComponent<Rigidbody>();
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, soundDragLoop);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!soundHit.IsNull && !collision.collider.isTrigger && collision.relativeVelocity.sqrMagnitude > 4f)
		{
			PineFmod.playOneShotSoundAttached(soundHit, base.gameObject);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(base.transform.TransformPoint(customLocal), 0.05f);
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		int value = (int)dragConstraintFlags;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isKinematic, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		dragConstraintFlags = (RigidbodyConstraints)reader.ReadInt32();
		isKinematic = reader.ReadBoolean();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		RigidbodyConstraints rigidbodyConstraints = (RigidbodyConstraints)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dragConstraintFlags",
			fieldValue = $"{rigidbodyConstraints}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isKinematic",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
