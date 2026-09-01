using Unity.Entities;

namespace Landfall.TABS.AI.Components
{
	public struct CanSeeTarget : IComponentData
	{
		public int CanSee;

		public int RaycastMask;
	}
}
