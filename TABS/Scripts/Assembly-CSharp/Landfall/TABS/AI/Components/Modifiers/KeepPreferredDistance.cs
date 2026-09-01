using System.Runtime.InteropServices;
using Unity.Entities;

namespace Landfall.TABS.AI.Components.Modifiers
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct KeepPreferredDistance : IMovementComponent, IComponentData
	{
	}
}
