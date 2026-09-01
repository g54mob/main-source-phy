using System.Runtime.CompilerServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal
{
	internal class UVChannels<TVec>
	{
		private static readonly int UVChannelCount;

		private ResizableArray<TVec>[] channels;

		private TVec[][] channelsData;

		public TVec[][] Data
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return null;
			}
		}

		public ResizableArray<TVec> this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return null;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public void Resize(int capacity, bool trimExess = false)
		{
		}
	}
}
