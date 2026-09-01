using Unity.Entities;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Components
{
	public struct GroundPosition : IComponentData
	{
		public float3 Value;
	}
}
