using System;
using System.Text;
using UnityEngine;

[Serializable]
public class EditorDisplayData : IReadWrite
{
	public InstanceID targetLock;

	public int columns;

	public int rows;

	public Vector2 padding;

	public int spriteSheetColumns;

	public int spriteSheetRows;

	public string spriteSheetFileName;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteIReadWrite(targetLock);
		writer.Write(in columns, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in rows, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector2(in padding);
		writer.Write(in spriteSheetColumns, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in spriteSheetRows, default(FastBinaryWriter.ForPrimitives));
		writer.Write(spriteSheetFileName);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		targetLock = reader.ReadIReadWrite<InstanceID>();
		columns = reader.ReadInt32();
		rows = reader.ReadInt32();
		padding = reader.ReadVector2();
		spriteSheetColumns = reader.ReadInt32();
		spriteSheetRows = reader.ReadInt32();
		spriteSheetFileName = reader.ReadString();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("targetLock: " + ToStringHelper.Stringify(targetLock));
		stringBuilder.AppendLine("columns: " + $"{columns}");
		stringBuilder.AppendLine("rows: " + $"{rows}");
		stringBuilder.AppendLine("padding: " + $"{padding}");
		stringBuilder.AppendLine("spriteSheetColumns: " + $"{spriteSheetColumns}");
		stringBuilder.AppendLine("spriteSheetRows: " + $"{spriteSheetRows}");
		stringBuilder.Append("spriteSheetFileName: " + ToStringHelper.Stringify(spriteSheetFileName));
		return stringBuilder.ToString();
	}
}
