using Unity.Entities;

namespace Landfall.TABS.AI.Components.Tags
{
	public struct IsTarget : IComponentData
	{
		public Entity Targetee;
	}
}
