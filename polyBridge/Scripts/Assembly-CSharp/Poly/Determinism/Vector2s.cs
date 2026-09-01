using System;
using UnityEngine;

namespace Poly.Determinism
{
	[Serializable]
	public struct Vector2s
	{
		public float x;

		public float y;

		public static implicit operator Vector2s(Vec2 v)
		{
			return new Vector2s
			{
				x = v.x,
				y = v.y
			};
		}

		public static implicit operator Vector2s(Vector2 v)
		{
			return new Vector2s
			{
				x = v.x,
				y = v.y
			};
		}

		public static implicit operator Vector2s(Vector3 v)
		{
			return new Vector2s
			{
				x = v.x,
				y = v.y
			};
		}

		public static bool operator ==(Vector2s a, Vector2s b)
		{
			if (a.x == b.x)
			{
				return a.y == b.y;
			}
			return false;
		}

		public static bool operator !=(Vector2s a, Vector2s b)
		{
			if (a.x == b.x)
			{
				return a.y != b.y;
			}
			return true;
		}

		public override bool Equals(object other)
		{
			if (other is Vector2s)
			{
				return this == (Vector2s)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		public override string ToString()
		{
			return $"[{x:0.0000}, {y:0.0000}]";
		}

		public string ToString(int precision)
		{
			string text = ((precision > 0) ? "0." : "0");
			for (int i = 1; i < precision && i < 10; i++)
			{
				text += "0";
			}
			return string.Format("[{0:" + text + "}, {1:" + text + "}]", x, y);
		}

		public static Vector2s operator -(Vector2s a, Vector2s b)
		{
			return new Vector2s
			{
				x = a.x - b.x,
				y = a.y - b.y
			};
		}
	}
}
