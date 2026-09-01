using Unity.Entities;

namespace Landfall.TABS.AI.Components
{
	public struct UnitType : IComponentData
	{
		public DatabaseID Value;

		public int IsRider;
	}
}
