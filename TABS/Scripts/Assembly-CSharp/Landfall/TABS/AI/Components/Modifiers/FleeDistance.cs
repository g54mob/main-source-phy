using Unity.Entities;

namespace Landfall.TABS.AI.Components.Modifiers
{
	public struct FleeDistance : IMovementComponent, IComponentData
	{
		public float Value;
	}
}
