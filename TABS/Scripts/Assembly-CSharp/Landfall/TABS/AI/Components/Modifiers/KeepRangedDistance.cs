using Unity.Entities;

namespace Landfall.TABS.AI.Components.Modifiers
{
	public struct KeepRangedDistance : IMovementComponent, IComponentData
	{
		public float WaitingDistance;
	}
}
