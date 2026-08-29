using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GLTFast.Schema;

namespace GLTFast
{
	internal class MeshComparer : IEqualityComparer<MeshPrimitiveBase>, IEqualityComparer<IReadOnlyList<MeshPrimitiveBase>>
	{
		public bool Equals(IReadOnlyList<MeshPrimitiveBase> x, IReadOnlyList<MeshPrimitiveBase> y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null)
			{
				return false;
			}
			if (y == null)
			{
				return false;
			}
			if (x.Count != y.Count)
			{
				return false;
			}
			for (int i = 0; i < x.Count; i++)
			{
				if (!Equals(x[i], y[i]))
				{
					return false;
				}
			}
			return true;
		}

		public int GetHashCode(IReadOnlyList<MeshPrimitiveBase> obj)
		{
			int num = 17;
			foreach (MeshPrimitiveBase item in obj)
			{
				num = num * 31 + GetHashCode(item);
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(MeshPrimitiveBase x, MeshPrimitiveBase y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null)
			{
				return false;
			}
			if (y == null)
			{
				return false;
			}
			if (x.GetType() != y.GetType())
			{
				return false;
			}
			if (x.indices == y.indices && Equals(x.attributes, y.attributes))
			{
				return Equals(x.targets, y.targets);
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetHashCode(MeshPrimitiveBase primitive)
		{
			return ((17 * 31 + primitive.indices) * 31 + GetHashCode(primitive.attributes)) * 31 + GetHashCode(primitive.targets);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int GetHashCode(Attributes x)
		{
			if (x == null)
			{
				return 0;
			}
			return (((((((((((((17 * 31 + x.POSITION) * 31 + x.NORMAL) * 31 + x.TANGENT) * 31 + x.TEXCOORD_0) * 31 + x.TEXCOORD_1) * 31 + x.TEXCOORD_2) * 31 + x.TEXCOORD_3) * 31 + x.TEXCOORD_4) * 31 + x.TEXCOORD_5) * 31 + x.TEXCOORD_6) * 31 + x.TEXCOORD_7) * 31 + x.COLOR_0) * 31 + x.JOINTS_0) * 31 + x.WEIGHTS_0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int GetHashCode(MorphTarget[] x)
		{
			if (x == null)
			{
				return 0;
			}
			int num = 17;
			num = num * 31 + x.Length;
			foreach (MorphTarget morphTarget in x)
			{
				if (morphTarget != null)
				{
					num = num * 31 + morphTarget.POSITION;
					num = num * 31 + morphTarget.NORMAL;
					num = num * 31 + morphTarget.TANGENT;
				}
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool Equals(MorphTarget[] x, MorphTarget[] y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if (!Equals(x[i], y[i]))
				{
					return false;
				}
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool Equals(MorphTarget x, MorphTarget y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.POSITION == y.POSITION && x.NORMAL == y.NORMAL)
			{
				return x.TANGENT == y.TANGENT;
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool Equals(Attributes x, Attributes y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.POSITION == y.POSITION && x.NORMAL == y.NORMAL && x.TANGENT == y.TANGENT && x.TEXCOORD_0 == y.TEXCOORD_0 && x.TEXCOORD_1 == y.TEXCOORD_1 && x.TEXCOORD_2 == y.TEXCOORD_2 && x.TEXCOORD_3 == y.TEXCOORD_3 && x.TEXCOORD_4 == y.TEXCOORD_4 && x.TEXCOORD_5 == y.TEXCOORD_5 && x.TEXCOORD_6 == y.TEXCOORD_6 && x.TEXCOORD_7 == y.TEXCOORD_7 && x.COLOR_0 == y.COLOR_0 && x.JOINTS_0 == y.JOINTS_0)
			{
				return x.WEIGHTS_0 == y.WEIGHTS_0;
			}
			return false;
		}
	}
}
