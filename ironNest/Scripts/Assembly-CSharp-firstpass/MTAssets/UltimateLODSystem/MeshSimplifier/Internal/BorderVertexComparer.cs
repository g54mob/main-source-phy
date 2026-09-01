using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal
{
	internal class BorderVertexComparer : IComparer<BorderVertex>
	{
		public static readonly BorderVertexComparer instance;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int Compare(BorderVertex x, BorderVertex y)
		{
			return 0;
		}
	}
}
