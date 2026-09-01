using System;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct ItemChangeRecord
	{
		public ItemDefinitionSettings item;

		public ItemInstanceChangeRecord[] changes;

		public bool HasChanges => false;

		public long TotalQuantityBefore => 0L;

		public long TotalQuantityAfter => 0L;

		public long TotalQuantityChange => 0L;
	}
}
