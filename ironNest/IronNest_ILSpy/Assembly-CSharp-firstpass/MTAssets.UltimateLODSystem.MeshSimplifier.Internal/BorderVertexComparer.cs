using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal;

internal class BorderVertexComparer : IComparer<BorderVertex>
{
	public static readonly BorderVertexComparer instance;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Compare(BorderVertex x, BorderVertex y)
	{
		//IL_000e: Expected I4, but got O
		int value = (object)y >> 32;
		int num = default(int);
		return num.CompareTo(value);
	}

	static BorderVertexComparer()
	{
		BorderVertexComparer borderVertexComparer = new BorderVertexComparer();
		instance = borderVertexComparer;
	}
}
