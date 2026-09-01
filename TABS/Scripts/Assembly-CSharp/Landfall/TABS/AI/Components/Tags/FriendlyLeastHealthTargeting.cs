using System;
using System.Runtime.InteropServices;
using Unity.Entities;

namespace Landfall.TABS.AI.Components.Tags
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct FriendlyLeastHealthTargeting : ITargetingComponent, IComponentData
	{
	}
}
