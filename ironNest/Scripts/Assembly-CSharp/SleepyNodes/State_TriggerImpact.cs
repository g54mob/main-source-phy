using UnityEngine;

namespace SleepyNodes
{
	[CreateNodeMenu("Impact/Trigger Impact")]
	[NodeName("Trigger Impact")]
	[NodeWidth(400)]
	public class State_TriggerImpact : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public ShellDefinition Shell;

		public LocationSelection Location;

		public bool UsePrefabEffect;

		public bool TriggerNormalEvents;

		public override void OnEnter(NodeExecutionState state)
		{
		}

		private void SpawnImpactPrefab(Vector2 impactLocation)
		{
		}
	}
}
