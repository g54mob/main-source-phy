using Unity.Entities;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Components
{
	public struct Velocity : IComponentData
	{
		public float3 Value;
	}
}
