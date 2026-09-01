using System;

namespace SleepyNodes
{
	[CreateNodeMenu("Teleprinter/Clear Signal Alarm")]
	[NodeWidth(400)]
	[NodeName("Clear Signal Alarm")]
	public class State_ClearSignalAlarm : StateNode
	{
		[Flags]
		public enum Printers
		{
			None = 0,
			Primary = 1,
			Secondary = 2
		}

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		private static readonly (Printers flag, Teleprinter.Teleprinters tp)[] Map;

		public Printers Printer;

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}
	}
}
