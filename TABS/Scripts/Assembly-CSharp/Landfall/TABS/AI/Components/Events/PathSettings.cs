using Unity.Entities;

namespace Landfall.TABS.AI.Components.Events
{
	public struct PathSettings : IComponentData
	{
		public float RepathRate;

		public float CurrentRate;
	}
}
