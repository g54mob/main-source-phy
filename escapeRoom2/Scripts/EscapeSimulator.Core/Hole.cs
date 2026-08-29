using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class Hole : IReadWrite
{
	public List<Vector2> vertices = new List<Vector2>();

	public bool isValidPolygon()
	{
		if (vertices.Count >= 3)
		{
			return !Maths.isPolygonSelfIntersecting(vertices);
		}
		return false;
	}

	public Hole copy()
	{
		return new Hole
		{
			vertices = new List<Vector2>(vertices)
		};
	}

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteList(vertices, delegate(FastBinaryWriter w, Vector2 e)
		{
			w.WriteVector2(in e);
		});
	}

	public virtual void Read(FastBinaryReader reader)
	{
		vertices = reader.ReadList((FastBinaryReader r) => r.ReadVector2());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("vertices: " + ToStringHelper.Stringify(vertices, (Vector2 e) => $"{e}"));
		return stringBuilder.ToString();
	}
}
