using System.Runtime.CompilerServices;

namespace Poly
{
	public static class Values
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Swap<T>(ref T a, ref T b)
		{
			T val = a;
			a = b;
			b = val;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SwapIf<T>(bool condition, ref T a, ref T b)
		{
			if (condition)
			{
				T val = a;
				a = b;
				b = val;
			}
		}
	}
}
