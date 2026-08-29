using System;
using System.Text;
using UnityEngine;

[Serializable]
public class ObjectState : IReadWrite
{
	public GameObject gameObject;

	public string path;

	public int flags;

	public Color Image_color;

	public Color Text_color;

	public int Text_fontSize;

	public Quaternion Transform_localRotation;

	public Vector3 Transform_localRotationEuler;

	public Vector3 Transform_localScale;

	public Vector3 Transform_localPosition;

	public float AnimationSampler_unitTime;

	public Vector3 Item_examinePivotOffset;

	public float Item_examineScaleModifier;

	public Vector3 Item_examineBaseRotation;

	public Vector3 Item_groundRotation;

	public int Interactive_targetPriority;

	public float[] MaterialState;

	public float[] tweenState;

	public Color Light_Filter;

	public float Light_Temperature;

	public float Light_Intensity;

	[NonSerialized]
	public ObjectState defaultState;

	[NonSerialized]
	public float[] allWeights;

	[NonSerialized]
	public bool validState;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteGameObject(gameObject);
		writer.Write(path);
		writer.Write(in flags, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in Image_color);
		writer.WriteColor(in Text_color);
		writer.Write(in Text_fontSize, default(FastBinaryWriter.ForPrimitives));
		writer.WriteQuaternion(in Transform_localRotation);
		writer.WriteVector3(in Transform_localRotationEuler);
		writer.WriteVector3(in Transform_localScale);
		writer.WriteVector3(in Transform_localPosition);
		writer.Write(in AnimationSampler_unitTime, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in Item_examinePivotOffset);
		writer.Write(in Item_examineScaleModifier, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in Item_examineBaseRotation);
		writer.WriteVector3(in Item_groundRotation);
		writer.Write(in Interactive_targetPriority, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(MaterialState, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(tweenState, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteColor(in Light_Filter);
		writer.Write(in Light_Temperature, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in Light_Intensity, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(defaultState);
		writer.WriteArray(allWeights, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in validState, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		gameObject = reader.ReadGameObject();
		path = reader.ReadString();
		flags = reader.ReadInt32();
		Image_color = reader.ReadColor();
		Text_color = reader.ReadColor();
		Text_fontSize = reader.ReadInt32();
		Transform_localRotation = reader.ReadQuaternion();
		Transform_localRotationEuler = reader.ReadVector3();
		Transform_localScale = reader.ReadVector3();
		Transform_localPosition = reader.ReadVector3();
		AnimationSampler_unitTime = reader.ReadSingle();
		Item_examinePivotOffset = reader.ReadVector3();
		Item_examineScaleModifier = reader.ReadSingle();
		Item_examineBaseRotation = reader.ReadVector3();
		Item_groundRotation = reader.ReadVector3();
		Interactive_targetPriority = reader.ReadInt32();
		MaterialState = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		tweenState = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		Light_Filter = reader.ReadColor();
		Light_Temperature = reader.ReadSingle();
		Light_Intensity = reader.ReadSingle();
		defaultState = reader.ReadIReadWrite<ObjectState>();
		allWeights = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		validState = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("gameObject: " + $"{gameObject}");
		stringBuilder.AppendLine("path: " + ToStringHelper.Stringify(path));
		stringBuilder.AppendLine("flags: " + $"{flags}");
		stringBuilder.AppendLine("Image_color: " + $"{Image_color}");
		stringBuilder.AppendLine("Text_color: " + $"{Text_color}");
		stringBuilder.AppendLine("Text_fontSize: " + $"{Text_fontSize}");
		stringBuilder.AppendLine("Transform_localRotation: " + $"{Transform_localRotation}");
		stringBuilder.AppendLine("Transform_localRotationEuler: " + $"{Transform_localRotationEuler}");
		stringBuilder.AppendLine("Transform_localScale: " + $"{Transform_localScale}");
		stringBuilder.AppendLine("Transform_localPosition: " + $"{Transform_localPosition}");
		stringBuilder.AppendLine("AnimationSampler_unitTime: " + $"{AnimationSampler_unitTime}");
		stringBuilder.AppendLine("Item_examinePivotOffset: " + $"{Item_examinePivotOffset}");
		stringBuilder.AppendLine("Item_examineScaleModifier: " + $"{Item_examineScaleModifier}");
		stringBuilder.AppendLine("Item_examineBaseRotation: " + $"{Item_examineBaseRotation}");
		stringBuilder.AppendLine("Item_groundRotation: " + $"{Item_groundRotation}");
		stringBuilder.AppendLine("Interactive_targetPriority: " + $"{Interactive_targetPriority}");
		stringBuilder.AppendLine("MaterialState: " + ToStringHelper.Stringify(MaterialState, (float e) => $"{e}"));
		stringBuilder.AppendLine("tweenState: " + ToStringHelper.Stringify(tweenState, (float e) => $"{e}"));
		stringBuilder.AppendLine("Light_Filter: " + $"{Light_Filter}");
		stringBuilder.AppendLine("Light_Temperature: " + $"{Light_Temperature}");
		stringBuilder.AppendLine("Light_Intensity: " + $"{Light_Intensity}");
		stringBuilder.AppendLine("defaultState: " + ToStringHelper.Stringify(defaultState));
		stringBuilder.AppendLine("allWeights: " + ToStringHelper.Stringify(allWeights, (float e) => $"{e}"));
		stringBuilder.Append("validState: " + $"{validState}");
		return stringBuilder.ToString();
	}
}
