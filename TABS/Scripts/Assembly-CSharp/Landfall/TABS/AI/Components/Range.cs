using Unity.Entities;

namespace Landfall.TABS.AI.Components
{
	public struct Range : IComponentData
	{
		public float AttackRange;

		public float PreferredRange;
	}
}
