using Unity.Entities;

namespace Landfall.TABS.AI.Components.Tags
{
	public struct HasTargetTag : IComponentData
	{
		public Entity Target;
	}
}
