using System;
using System.Text;
using UnityEngine;

[Serializable]
public class TextData : IReadWrite
{
	public string text;

	public int fontSize = 12;

	public Color color = Color.black;

	public bool isBold;

	public bool isItalic;

	public Vector2 canvasSize = new Vector2(1f, 1f);

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(text);
		writer.Write(in fontSize, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in color);
		writer.Write(in isBold, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isItalic, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector2(in canvasSize);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		text = reader.ReadString();
		fontSize = reader.ReadInt32();
		color = reader.ReadColor();
		isBold = reader.ReadBoolean();
		isItalic = reader.ReadBoolean();
		canvasSize = reader.ReadVector2();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("text: " + ToStringHelper.Stringify(text));
		stringBuilder.AppendLine("fontSize: " + $"{fontSize}");
		stringBuilder.AppendLine("color: " + $"{color}");
		stringBuilder.AppendLine("isBold: " + $"{isBold}");
		stringBuilder.AppendLine("isItalic: " + $"{isItalic}");
		stringBuilder.Append("canvasSize: " + $"{canvasSize}");
		return stringBuilder.ToString();
	}
}
