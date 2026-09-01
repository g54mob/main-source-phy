using System;
using UnityEngine;

namespace Poly.Math
{
	[Serializable]
	public struct Transform3
	{
		public Vector3 position;

		public Quaternion rotation;

		public static Transform3 identity => new Transform3(Vector3.zero, Quaternion.identity);

		public Transform3 inverse
		{
			get
			{
				Transform3 result = default(Transform3);
				result.position = -((result.rotation = Quaternion.Inverse(rotation)) * position);
				return result;
			}
		}

		public Transform3(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}

		public static implicit operator Transform3(Transform t)
		{
			return new Transform3(t.position, t.rotation);
		}

		public static implicit operator Transform2(Transform3 t)
		{
			return new Transform2((Vec2)t.position, t.rotation.eulerAngles.z);
		}
	}
}
