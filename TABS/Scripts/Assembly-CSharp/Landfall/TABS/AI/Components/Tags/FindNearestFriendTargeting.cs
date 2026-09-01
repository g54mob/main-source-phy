using System;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.AI.Components.Tags
{
	[Serializable]
	public struct FindNearestFriendTargeting : ITargetingComponent, IComponentData
	{
		[SerializeField]
		public int PrioritizeMount;
	}
}
