using Unity.Entities;

namespace Landfall.TABS.AI.Components.Pathfinding
{
	public struct Navmesh : IComponentData
	{
		public NavmeshType Value;
	}
}
