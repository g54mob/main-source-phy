using Unity.Entities;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Components
{
	public struct Direction : IComponentData
	{
		public float3 LastValue;

		public float3 Value;

		public float AngleToDirection;
	}
}
