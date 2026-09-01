using Unity.Entities;

namespace Landfall.TABS.AI.Components
{
	public struct TargetData : IComponentData
	{
		public float DistanceToTarget;

		public int TargetInPreferredRange;

		public int TargetInAttackRange;

		public static TargetData Null => default(TargetData);
	}
}
