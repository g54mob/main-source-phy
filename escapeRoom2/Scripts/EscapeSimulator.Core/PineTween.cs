using System.Collections.Generic;
using UnityEngine;

public class PineTween : ISaveable
{
	public int handle = -1;

	public Interpolation interpolation;

	public float delay;

	public float duration = 1f;

	public bool finished;

	public bool deactivate;

	public bool activate;

	public bool destroy;

	public bool paused;

	public float elapsedTime;

	public float elapsedFixedTime;

	public Vector3? localPositionFrom;

	public Vector3? localPositionTo;

	public Vector3? positionFrom;

	public Vector3? positionTo;

	public Quaternion? rotationFrom;

	public Quaternion? rotationTo;

	public Quaternion? localRotationFrom;

	public Quaternion? localRotationTo;

	public Vector3? scaleFrom;

	public Vector3? scaleTo;

	public Vector3? anchoredPositionFrom;

	public Vector3? anchoredPositionTo;

	public Vector2? sizeDeltaFrom;

	public Vector2? sizeDeltaTo;

	public Color? colorFrom;

	public Color? colorTo;

	public float? alphaFrom;

	public float? alphaTo;

	public bool hasAnimation;

	public bool hasCustom;

	public int dataInt;

	public GameObject gameObject;

	public Rigidbody rigidbody;

	[DontSave]
	public CustomAnimation animation;

	[DontSave]
	public CustomProcessor custom;

	[DontSave]
	public OnComplete onComplete;

	public float remainingTime()
	{
		return delay + duration - elapsedTime;
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in handle, default(FastBinaryWriter.ForPrimitives));
		int value = (int)interpolation;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in delay, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in duration, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in finished, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in deactivate, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in activate, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in destroy, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in paused, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in elapsedTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in elapsedFixedTime, default(FastBinaryWriter.ForPrimitives));
		writer.WriteNullable(localPositionFrom, delegate(FastBinaryWriter w, Vector3 n)
		{
			w.WriteVector3(in n);
		});
		writer.WriteNullable(localPositionTo, delegate(FastBinaryWriter w, Vector3 n)
		{
			w.WriteVector3(in n);
		});
		writer.WriteNullable(positionFrom, delegate(FastBinaryWriter w, Vector3 n)
		{
			w.WriteVector3(in n);
		});
		writer.WriteNullable(positionTo, delegate(FastBinaryWriter w, Vector3 n)
		{
			w.WriteVector3(in n);
		});
		writer.WriteNullable(rotationFrom, delegate(FastBinaryWriter w, Quaternion n)
		{
			w.WriteQuaternion(in n);
		});
		writer.WriteNullable(rotationTo, delegate(FastBinaryWriter w, Quaternion n)
		{
			w.WriteQuaternion(in n);
		});
		writer.WriteNullable(localRotationFrom, delegate(FastBinaryWriter w, Quaternion n)
		{
			w.WriteQuaternion(in n);
		});
		writer.WriteNullable(localRotationTo, delegate(FastBinaryWriter w, Quaternion n)
		{
			w.WriteQuaternion(in n);
		});
		writer.WriteNullable(scaleFrom, delegate(FastBinaryWriter w, Vector3 n)
		{
			w.WriteVector3(in n);
		});
		writer.WriteNullable(scaleTo, delegate(FastBinaryWriter w, Vector3 n)
		{
			w.WriteVector3(in n);
		});
		writer.WriteNullable(anchoredPositionFrom, delegate(FastBinaryWriter w, Vector3 n)
		{
			w.WriteVector3(in n);
		});
		writer.WriteNullable(anchoredPositionTo, delegate(FastBinaryWriter w, Vector3 n)
		{
			w.WriteVector3(in n);
		});
		writer.WriteNullable(sizeDeltaFrom, delegate(FastBinaryWriter w, Vector2 n)
		{
			w.WriteVector2(in n);
		});
		writer.WriteNullable(sizeDeltaTo, delegate(FastBinaryWriter w, Vector2 n)
		{
			w.WriteVector2(in n);
		});
		writer.WriteNullable(colorFrom, delegate(FastBinaryWriter w, Color n)
		{
			w.WriteColor(in n);
		});
		writer.WriteNullable(colorTo, delegate(FastBinaryWriter w, Color n)
		{
			w.WriteColor(in n);
		});
		writer.WriteNullable(alphaFrom, delegate(FastBinaryWriter w, float n)
		{
			w.Write(in n, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteNullable(alphaTo, delegate(FastBinaryWriter w, float n)
		{
			w.Write(in n, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in hasAnimation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hasCustom, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in dataInt, default(FastBinaryWriter.ForPrimitives));
		writer.WriteGameObject(gameObject);
		writer.WriteComponent(rigidbody);
	}

	public virtual void load(FastBinaryReader reader)
	{
		handle = reader.ReadInt32();
		interpolation = (Interpolation)reader.ReadInt32();
		delay = reader.ReadSingle();
		duration = reader.ReadSingle();
		finished = reader.ReadBoolean();
		deactivate = reader.ReadBoolean();
		activate = reader.ReadBoolean();
		destroy = reader.ReadBoolean();
		paused = reader.ReadBoolean();
		elapsedTime = reader.ReadSingle();
		elapsedFixedTime = reader.ReadSingle();
		localPositionFrom = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		localPositionTo = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		positionFrom = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		positionTo = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		rotationFrom = reader.ReadNullable((FastBinaryReader r) => r.ReadQuaternion());
		rotationTo = reader.ReadNullable((FastBinaryReader r) => r.ReadQuaternion());
		localRotationFrom = reader.ReadNullable((FastBinaryReader r) => r.ReadQuaternion());
		localRotationTo = reader.ReadNullable((FastBinaryReader r) => r.ReadQuaternion());
		scaleFrom = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		scaleTo = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		anchoredPositionFrom = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		anchoredPositionTo = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		sizeDeltaFrom = reader.ReadNullable((FastBinaryReader r) => r.ReadVector2());
		sizeDeltaTo = reader.ReadNullable((FastBinaryReader r) => r.ReadVector2());
		colorFrom = reader.ReadNullable((FastBinaryReader r) => r.ReadColor());
		colorTo = reader.ReadNullable((FastBinaryReader r) => r.ReadColor());
		alphaFrom = reader.ReadNullable((FastBinaryReader r) => r.ReadSingle());
		alphaTo = reader.ReadNullable((FastBinaryReader r) => r.ReadSingle());
		hasAnimation = reader.ReadBoolean();
		hasCustom = reader.ReadBoolean();
		dataInt = reader.ReadInt32();
		gameObject = reader.ReadGameObject();
		rigidbody = reader.ReadComponent<Rigidbody>();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "handle",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Interpolation interpolation = (Interpolation)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "interpolation",
			fieldValue = $"{interpolation}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "delay",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "duration",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "finished",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "deactivate",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "activate",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "destroy",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "paused",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "elapsedTime",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "elapsedFixedTime",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3? vector = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "localPositionFrom",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3? vector2 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "localPositionTo",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3? vector3 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "positionFrom",
			fieldValue = $"{vector3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3? vector4 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "positionTo",
			fieldValue = $"{vector4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion? quaternion = reader.ReadNullable((FastBinaryReader r) => r.ReadQuaternion());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "rotationFrom",
			fieldValue = $"{quaternion}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion? quaternion2 = reader.ReadNullable((FastBinaryReader r) => r.ReadQuaternion());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "rotationTo",
			fieldValue = $"{quaternion2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion? quaternion3 = reader.ReadNullable((FastBinaryReader r) => r.ReadQuaternion());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "localRotationFrom",
			fieldValue = $"{quaternion3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion? quaternion4 = reader.ReadNullable((FastBinaryReader r) => r.ReadQuaternion());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "localRotationTo",
			fieldValue = $"{quaternion4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3? vector5 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "scaleFrom",
			fieldValue = $"{vector5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3? vector6 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "scaleTo",
			fieldValue = $"{vector6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3? vector7 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "anchoredPositionFrom",
			fieldValue = $"{vector7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3? vector8 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "anchoredPositionTo",
			fieldValue = $"{vector8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector2? vector9 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector2());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sizeDeltaFrom",
			fieldValue = $"{vector9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector2? vector10 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector2());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sizeDeltaTo",
			fieldValue = $"{vector10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Color? color = reader.ReadNullable((FastBinaryReader r) => r.ReadColor());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "colorFrom",
			fieldValue = $"{color}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Color? color2 = reader.ReadNullable((FastBinaryReader r) => r.ReadColor());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "colorTo",
			fieldValue = $"{color2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float? num6 = reader.ReadNullable((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "alphaFrom",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float? num7 = reader.ReadNullable((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "alphaTo",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hasAnimation",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hasCustom",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num8 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dataInt",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gameObject",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Rigidbody arg2 = reader.ReadComponent<Rigidbody>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "rigidbody",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
