using System.Runtime.InteropServices;
using Unity.Entities;

namespace Landfall.TABS.AI.Components
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct IsInPool : IComponentData
	{
	}
}
