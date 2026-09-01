using System;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct InventoryChangeRecord
	{
		public ItemChangeRecord[] changes;

		public bool HasChanges => false;

		public long TotalQuantityBefore => 0L;

		public long TotalQuantityAfter => 0L;

		public long TotalQuantityChange => 0L;
	}
}
