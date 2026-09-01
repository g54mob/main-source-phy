using Unity.Entities;

namespace Landfall.TABS.AI.Components
{
	public struct Health : IComponentData
	{
		public float CurrentHealth;

		public float MaxHealth;
	}
}
