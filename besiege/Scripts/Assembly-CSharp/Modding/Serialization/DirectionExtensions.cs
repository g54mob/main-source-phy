using System;
using UnityEngine;

namespace Modding.Serialization
{
	public static class DirectionExtensions
	{
		public static UnityEngine.Vector3 ToAxisVector(this Direction dir)
		{
			switch (dir)
			{
			case Direction.X:
				return new UnityEngine.Vector3(1f, 0f, 0f);
			case Direction.Y:
				return new UnityEngine.Vector3(0f, 1f, 0f);
			case Direction.Z:
				return new UnityEngine.Vector3(0f, 0f, 1f);
			default:
				throw new InvalidOperationException();
			}
		}

		public static float GetAxisComponent(this Direction dir, UnityEngine.Vector3 other)
		{
			switch (dir)
			{
			case Direction.X:
				return other.x;
			case Direction.Y:
				return other.y;
			case Direction.Z:
				return other.z;
			default:
				throw new InvalidOperationException();
			}
		}
	}
}
