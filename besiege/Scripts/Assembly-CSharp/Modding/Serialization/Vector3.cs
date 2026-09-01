using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Modding.Serialization
{
	[Serializable]
	public struct Vector3
	{
		public static readonly Vector3 zero = new Vector3(0f, 0f, 0f);

		public static readonly Vector3 one = new Vector3(1f, 1f, 1f);

		[XmlAttribute]
		public float x;

		[XmlAttribute]
		public float y;

		[XmlAttribute]
		public float z;

		public Vector3(float pX, float pY, float pZ)
		{
			x = pX;
			y = pY;
			z = pZ;
		}

		public Vector3(UnityEngine.Vector3 o)
			: this(o.x, o.y, o.z)
		{
		}

		public override string ToString()
		{
			return string.Format("({0}, {1}, {2})", x, y, z);
		}

		public static implicit operator UnityEngine.Vector3(Vector3 sV)
		{
			return new UnityEngine.Vector3
			{
				x = sV.x,
				y = sV.y,
				z = sV.z
			};
		}

		public static implicit operator Vector3(UnityEngine.Vector3 v)
		{
			return new Vector3(v);
		}
	}
}
