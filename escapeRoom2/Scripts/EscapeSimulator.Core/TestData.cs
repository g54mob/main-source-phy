using System;
using System.Text;
using UnityEngine;

[Serializable]
public class TestData : IReadWrite
{
	public int integer;

	public int integerRange;

	public float single;

	public float singleRange;

	public Vector3 vector3;

	public Vector2 vector2;

	public string text;

	public Color color1;

	public Color color2;

	public bool boolean;

	public Test.SomeOption option;

	public string textureId;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in integer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in integerRange, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in single, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in singleRange, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in vector3);
		writer.WriteVector2(in vector2);
		writer.Write(text);
		writer.WriteColor(in color1);
		writer.WriteColor(in color2);
		writer.Write(in boolean, default(FastBinaryWriter.ForPrimitives));
		int value = (int)option;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(textureId);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		integer = reader.ReadInt32();
		integerRange = reader.ReadInt32();
		single = reader.ReadSingle();
		singleRange = reader.ReadSingle();
		vector3 = reader.ReadVector3();
		vector2 = reader.ReadVector2();
		text = reader.ReadString();
		color1 = reader.ReadColor();
		color2 = reader.ReadColor();
		boolean = reader.ReadBoolean();
		option = (Test.SomeOption)reader.ReadInt32();
		textureId = reader.ReadString();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("integer: " + $"{integer}");
		stringBuilder.AppendLine("integerRange: " + $"{integerRange}");
		stringBuilder.AppendLine("single: " + $"{single}");
		stringBuilder.AppendLine("singleRange: " + $"{singleRange}");
		stringBuilder.AppendLine("vector3: " + $"{vector3}");
		stringBuilder.AppendLine("vector2: " + $"{vector2}");
		stringBuilder.AppendLine("text: " + ToStringHelper.Stringify(text));
		stringBuilder.AppendLine("color1: " + $"{color1}");
		stringBuilder.AppendLine("color2: " + $"{color2}");
		stringBuilder.AppendLine("boolean: " + $"{boolean}");
		stringBuilder.AppendLine("option: " + $"{option}");
		stringBuilder.Append("textureId: " + ToStringHelper.Stringify(textureId));
		return stringBuilder.ToString();
	}
}
