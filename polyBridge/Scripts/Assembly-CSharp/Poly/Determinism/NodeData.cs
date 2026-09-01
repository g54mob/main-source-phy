using System;

namespace Poly.Determinism
{
	[Serializable]
	public struct NodeData
	{
		public int objectId;

		public float invMass;

		public Vector2s pos;

		public Vector2s oldPos;

		public string dataString => "#" + objectId + " " + pos.ToString();

		public static bool operator ==(NodeData a, NodeData b)
		{
			return !(a != b);
		}

		public static bool operator !=(NodeData a, NodeData b)
		{
			if (a.objectId == b.objectId && a.invMass == b.invMass && !(a.pos != b.pos))
			{
				return a.oldPos != b.oldPos;
			}
			return true;
		}

		public override bool Equals(object other)
		{
			if (other is NodeData)
			{
				return this == (NodeData)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return objectId.GetHashCode() ^ invMass.GetHashCode() ^ pos.GetHashCode() ^ oldPos.GetHashCode();
		}
	}
}
