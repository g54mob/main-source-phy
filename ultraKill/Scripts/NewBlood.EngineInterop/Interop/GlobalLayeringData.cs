namespace Interop
{
	public struct GlobalLayeringData
	{
		public uint layerAndOrder;

		public uint sortingGroupAll;

		public uint order
		{
			readonly get
			{
				return BitHelper.ExtractRange(sortingGroupAll, 0, 12);
			}
			set
			{
				BitHelper.SetRange(ref sortingGroupAll, 0, 12, value);
			}
		}

		public uint id
		{
			readonly get
			{
				return BitHelper.ExtractRange(sortingGroupAll, 12, 20);
			}
			set
			{
				BitHelper.SetRange(ref sortingGroupAll, 12, 20, value);
			}
		}
	}
}
