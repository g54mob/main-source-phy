using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Poly.Extension
{
	[DebuggerDisplay("({x}, {y})")]
	public struct Vec2Int
	{
		public int x;

		public int y;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vec2Int(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vec2Int(Vector2Int v)
		{
			return new Vec2Int(v.x, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2Int(Vec2Int v)
		{
			return new Vector2Int(v.x, v.y);
		}
	}
}
