using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace MagicaCloth2
{
	internal static class NativeReferenceExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int MC2InterlockedStartIndex(this ref NativeReference<int> counter, int dataCount)
		{
			return Interlocked.Add(ref *counter.GetUnsafePtr(), dataCount) - dataCount;
		}
	}
}
