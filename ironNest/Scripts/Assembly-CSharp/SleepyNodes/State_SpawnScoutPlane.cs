using UnityEngine;

namespace SleepyNodes
{
	[CreateNodeMenu("Entity/Spawn Scout Plane")]
	[NodeWidth(400)]
	[NodeName("Spawn Scout Plane")]
	public class State_SpawnScoutPlane : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public GameObject PlanePrefab;

		public LocationSelection LocationToSpawn;

		public bool RandomBearing;

		public ContextVariableOrInline_Float Bearing;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
