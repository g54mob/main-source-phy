using System.Collections.Generic;
using UnityEngine;

namespace SleepyNodes
{
	[CreateNodeMenu("Impact/Start")]
	[NodeName("Impact")]
	[NodeWidth(400)]
	public class State_ImpactStart : StateNodeEntry
	{
		public bool OverrideRadius;

		public float MinRadius;

		public float MaxRadius;

		public FilterEntitySet EntityConditions;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode ForEachEntityHit;

		public EntityContextKeys EntityHit;

		public LocationContextKeys ClosestGridLocation;

		private Vector2 lastImpactLocation;

		public List<MapEntity> StartImpact(StateNode.NodeExecutionState state, ShellDefinition shell, Vector2 impactLocation)
		{
			return null;
		}
	}
}
