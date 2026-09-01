using System.Runtime.CompilerServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal
{
	internal struct BorderVertex
	{
		public int index;

		public int hash;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BorderVertex(int index, int hash)
		{
			this.index = 0;
			this.hash = 0;
		}
	}
}
