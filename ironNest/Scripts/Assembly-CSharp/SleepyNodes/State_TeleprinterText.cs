using System;
using System.Collections.Generic;
using Localisation;

namespace SleepyNodes
{
	[CreateNodeMenu("Teleprinter/Send Text")]
	[NodeWidth(400)]
	[NodeName("Teleprinter Text")]
	public class State_TeleprinterText : StateNode
	{
		[Serializable]
		public class StringReplacement
		{
			public string Text;

			public EntityContextKeys EntityContextKey;
		}

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public Teleprinter.Teleprinters Printer;

		public bool OnlyQueue;

		public bool WaitUntilComplete;

		public TextIdentifier Text;

		public Teleprinter.TeleprinterAlarmState AlarmState;

		public List<StringReplacement> EntityIDToReplace;

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}
	}
}
