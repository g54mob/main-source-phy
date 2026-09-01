using Unity.Entities;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Components.Pathfinding
{
	public struct PathPoint : IBufferElementData
	{
		public float3 Value;
	}
}
