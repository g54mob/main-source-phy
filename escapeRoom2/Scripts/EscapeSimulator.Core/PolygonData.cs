using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class PolygonData : IReadWrite
{
	public List<Vector2> vertices;

	public List<Hole> holes;

	public float baseThickness;

	public float extrusionThickness;

	public Polygon.ExtrusionType extrusionType;

	public Polygon.CollisionType collisionType;

	public bool hasCollider = true;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteList(vertices, delegate(FastBinaryWriter w, Vector2 e)
		{
			w.WriteVector2(in e);
		});
		writer.WriteList(holes, delegate(FastBinaryWriter w, Hole e)
		{
			w.WriteIReadWrite(e);
		});
		writer.Write(in baseThickness, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in extrusionThickness, default(FastBinaryWriter.ForPrimitives));
		int value = (int)extrusionType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)collisionType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hasCollider, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		vertices = reader.ReadList((FastBinaryReader r) => r.ReadVector2());
		holes = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<Hole>());
		baseThickness = reader.ReadSingle();
		extrusionThickness = reader.ReadSingle();
		extrusionType = (Polygon.ExtrusionType)reader.ReadInt32();
		collisionType = (Polygon.CollisionType)reader.ReadInt32();
		hasCollider = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("vertices: " + ToStringHelper.Stringify(vertices, (Vector2 e) => $"{e}"));
		stringBuilder.AppendLine("holes: " + ToStringHelper.Stringify(holes, (Hole e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("baseThickness: " + $"{baseThickness}");
		stringBuilder.AppendLine("extrusionThickness: " + $"{extrusionThickness}");
		stringBuilder.AppendLine("extrusionType: " + $"{extrusionType}");
		stringBuilder.AppendLine("collisionType: " + $"{collisionType}");
		stringBuilder.Append("hasCollider: " + $"{hasCollider}");
		return stringBuilder.ToString();
	}
}
