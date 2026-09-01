using Unity.Entities;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Components
{
	public struct PredictedPosition : IComponentData
	{
		public float3 Value;
	}
}
