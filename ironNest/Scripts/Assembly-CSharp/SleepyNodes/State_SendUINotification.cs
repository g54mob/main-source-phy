using Localisation;
using UnityEngine;

namespace SleepyNodes
{
	[CreateNodeMenu("Mission/Send UI Notification")]
	[NodeWidth(400)]
	[NodeName("Send UI Notification")]
	public class State_SendUINotification : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public TextIdentifier Text_Title;

		public TextIdentifier Text_Description;

		public float Duration;

		public Color Tint;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
