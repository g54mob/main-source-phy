using System;
using UnityEngine;

namespace INab.CommonVFX
{
	public class MeshData
	{
		public struct Vertex
		{
			public Vector3 position;

			public Color color;

			public Vector3 normal;

			public Vector4 tangent;

			public Vector4[] uvs;

			public static Vertex operator +(Vertex a, Vertex b)
			{
				if (a.uvs.Length != b.uvs.Length)
				{
					throw new InvalidOperationException("Adding compatible vertex");
				}
				Vertex result = new Vertex
				{
					position = a.position + b.position,
					color = a.color + b.color,
					normal = a.normal + b.normal,
					tangent = a.tangent + b.tangent,
					uvs = new Vector4[a.uvs.Length]
				};
				for (int i = 0; i < a.uvs.Length; i++)
				{
					result.uvs[i] = a.uvs[i] + b.uvs[i];
				}
				return result;
			}

			public static Vertex operator *(float a, Vertex b)
			{
				Vertex result = new Vertex
				{
					position = a * b.position,
					color = a * b.color,
					normal = a * b.normal,
					tangent = a * b.tangent,
					uvs = new Vector4[b.uvs.Length]
				};
				for (int i = 0; i < b.uvs.Length; i++)
				{
					result.uvs[i] = a * b.uvs[i];
				}
				return result;
			}
		}

		public struct Triangle
		{
			public uint a;

			public uint b;

			public uint c;
		}

		public Vertex[] vertices;

		public Triangle[] triangles;

		public double[] accumulatedTriangleArea;
	}
}
