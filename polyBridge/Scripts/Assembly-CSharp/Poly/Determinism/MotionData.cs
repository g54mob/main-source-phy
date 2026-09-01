using System;

namespace Poly.Determinism
{
	[Serializable]
	public struct MotionData
	{
		public int objectId;

		public Vector2s pos;

		public Vector2s oldPos;

		public string dataString => "#" + objectId + " " + pos.ToString();

		public static bool operator ==(MotionData a, MotionData b)
		{
			return !(a != b);
		}

		public static bool operator !=(MotionData a, MotionData b)
		{
			if (a.objectId == b.objectId && !(a.pos != b.pos))
			{
				return a.oldPos != b.oldPos;
			}
			return true;
		}

		public override bool Equals(object other)
		{
			if (other is MotionData)
			{
				return this == (MotionData)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return objectId.GetHashCode() ^ pos.GetHashCode() ^ oldPos.GetHashCode();
		}
	}
}
