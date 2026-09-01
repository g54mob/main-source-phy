using System;
using System.Collections.Generic;

namespace SleepyNodes
{
	[CreateNodeMenu("Objectives/Start Objective")]
	[NodeWidth(400)]
	[NodeName("Start Objective")]
	public class State_Objective : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public ObjectiveGraph Objective;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode OnSuccess;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode OnFailure;

		[NonSerialized]
		public List<ObjectiveGraph> Running;

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public void SendNotification(string notifID)
		{
		}

		public void CheckEvents(EventNode.EventData data)
		{
		}

		public void UpdateObjectives()
		{
		}

		public void OnResult(ObjectiveGraph child, ObjectiveGraph.ObjectiveResults results)
		{
		}
	}
}
