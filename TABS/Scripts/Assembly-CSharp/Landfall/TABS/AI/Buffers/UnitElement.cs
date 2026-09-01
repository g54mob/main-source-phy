using Unity.Entities;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Buffers
{
	public struct UnitElement : IBufferElementData
	{
		public Entity Entity;

		public float3 Position;
	}
}
