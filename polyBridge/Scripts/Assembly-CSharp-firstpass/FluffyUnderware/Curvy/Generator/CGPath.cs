using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[CGDataInfo(0.13f, 0.59f, 0.95f, 1f)]
	public class CGPath : CGShape
	{
		public Vector3[] Direction = new Vector3[0];

		public CGPath()
		{
		}

		public CGPath(CGPath source)
			: base(source)
		{
			Direction = (Vector3[])source.Direction.Clone();
		}

		public override T Clone<T>()
		{
			return new CGPath(this) as T;
		}

		public static void Copy(CGPath dest, CGPath source)
		{
			CGShape.Copy(dest, source);
			Array.Resize(ref dest.Direction, source.Direction.Length);
			source.Direction.CopyTo(dest.Direction, 0);
		}

		public void Interpolate(float f, out Vector3 position, out Vector3 direction, out Vector3 up)
		{
			float frag;
			int fIndex = GetFIndex(f, out frag);
			position = Vector3.LerpUnclamped(Position[fIndex], Position[fIndex + 1], frag);
			direction = Vector3.SlerpUnclamped(Direction[fIndex], Direction[fIndex + 1], frag);
			up = Vector3.SlerpUnclamped(Normal[fIndex], Normal[fIndex + 1], frag);
		}

		public void Interpolate(float f, float angleF, out Vector3 pos, out Vector3 dir, out Vector3 up)
		{
			Interpolate(f, out pos, out dir, out up);
			if (angleF != 0f)
			{
				Quaternion quaternion = Quaternion.AngleAxis(angleF * -360f, dir);
				up = quaternion * up;
			}
		}

		public Vector3 InterpolateDirection(float f)
		{
			float frag;
			int fIndex = GetFIndex(f, out frag);
			return Vector3.SlerpUnclamped(Direction[fIndex], Direction[fIndex + 1], frag);
		}

		public override void Recalculate()
		{
			base.Recalculate();
			for (int i = 1; i < Count; i++)
			{
				Direction[i].x = Position[i].x - Position[i - 1].x;
				Direction[i].y = Position[i].y - Position[i - 1].y;
				Direction[i].z = Position[i].z - Position[i - 1].z;
				Direction[i] = Vector3.Normalize(Direction[i]);
			}
		}
	}
}
