using System;
using System.Globalization;

namespace FluffyUnderware.Curvy.Generator
{
	public struct SamplePointUData : IEquatable<SamplePointUData>
	{
		public int Vertex;

		public bool UVEdge;

		public float FirstU;

		public float SecondU;

		public SamplePointUData(int vt, bool uvEdge, float uv0, float uv1)
		{
			Vertex = vt;
			UVEdge = uvEdge;
			FirstU = uv0;
			SecondU = uv1;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SamplePointUData (Vertex={0},Edge={1},FirstU={2},SecondU={3}", Vertex, UVEdge, FirstU, SecondU);
		}

		public bool Equals(SamplePointUData other)
		{
			if (Vertex == other.Vertex && UVEdge == other.UVEdge && FirstU.Equals(other.FirstU))
			{
				return SecondU.Equals(other.SecondU);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is SamplePointUData)
			{
				return Equals((SamplePointUData)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (((((Vertex * 397) ^ UVEdge.GetHashCode()) * 397) ^ FirstU.GetHashCode()) * 397) ^ SecondU.GetHashCode();
		}

		public static bool operator ==(SamplePointUData left, SamplePointUData right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(SamplePointUData left, SamplePointUData right)
		{
			return !left.Equals(right);
		}
	}
}
