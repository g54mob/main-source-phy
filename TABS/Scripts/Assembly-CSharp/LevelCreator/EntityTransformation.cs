using UnityEngine;

namespace LevelCreator
{
	public struct EntityTransformation
	{
		public Vector3 position;

		public Quaternion rotation;

		public Vector3 scale;

		public float heightOffset;

		public static EntityTransformation Id = new EntityTransformation
		{
			position = Vector3.zero,
			rotation = Quaternion.identity,
			scale = Vector3.one,
			heightOffset = 0f
		};

		public override string ToString()
		{
			return string.Concat("{ position: ", position, ", rotation: ", rotation, ", scale: ", scale, ", heightOffset: ", heightOffset, " }");
		}

		public static EntityTransformation operator *(EntityTransformation et1, EntityTransformation et2)
		{
			return new EntityTransformation
			{
				position = et1.position + Vector3.Scale(et1.rotation * et2.position, et1.scale),
				rotation = et1.rotation * et2.rotation,
				scale = Vector3.Scale(et1.scale, et2.scale),
				heightOffset = et1.heightOffset * et2.heightOffset
			};
		}

		public EntityTransformation Inverse()
		{
			Vector3 b = new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z);
			Quaternion quaternion = Quaternion.Inverse(rotation);
			float num = heightOffset;
			return new EntityTransformation
			{
				position = Vector3.Scale(quaternion * -position, b),
				rotation = quaternion,
				scale = b,
				heightOffset = num
			};
		}

		public Vector3 TransformPosition(Vector3 position)
		{
			return Vector3.Scale(rotation * (position + this.position), scale);
		}

		public Vector3 Rotate(Vector3 normal)
		{
			return rotation * normal;
		}

		public Quaternion Rotate(Quaternion rotation)
		{
			return this.rotation * rotation;
		}

		public Vector3 Scale(Vector3 scale)
		{
			return Vector3.Scale(this.scale, scale);
		}

		public static EntityTransformation Lerp(EntityTransformation a, EntityTransformation b, float t)
		{
			return new EntityTransformation
			{
				position = Vector3.Lerp(a.position, b.position, t),
				rotation = Quaternion.Lerp(a.rotation, b.rotation, t),
				scale = Vector3.Lerp(a.scale, b.scale, t),
				heightOffset = Mathf.Lerp(a.heightOffset, b.heightOffset, t)
			};
		}

		public static bool AlmostSame(EntityTransformation a, EntityTransformation b)
		{
			float num = a.rotation.x - b.rotation.x;
			float num2 = a.rotation.y - b.rotation.y;
			float num3 = a.rotation.z - b.rotation.z;
			float num4 = a.rotation.w - b.rotation.w;
			if (Vector3.Distance(a.position, b.position) < 0.01f && num4 * num4 + num * num + num2 * num2 + num3 * num3 < 0.001f && Vector3.Distance(a.scale, b.scale) < 0.001f)
			{
				return Mathf.Abs(a.heightOffset - b.heightOffset) < 0.001f;
			}
			return false;
		}
	}
}
