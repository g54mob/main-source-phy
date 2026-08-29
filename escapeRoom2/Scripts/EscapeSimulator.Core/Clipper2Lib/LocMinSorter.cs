using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Clipper2Lib
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct LocMinSorter : IComparer<LocalMinima>
	{
		public readonly int Compare(LocalMinima locMin1, LocalMinima locMin2)
		{
			long y = locMin2.vertex.pt.Y;
			return y.CompareTo(locMin1.vertex.pt.Y);
		}
	}
}
