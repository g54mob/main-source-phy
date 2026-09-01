using System;
using System.Collections.Generic;

namespace SleepyNodes
{
	[CreateNodeMenu("Cards/Start")]
	[NodeName("Punchard Activated")]
	[NodeWidth(300)]
	public class State_CardActionStart : StateNodeEntry
	{
		[Serializable]
		public class PunchardVariableSetup
		{
			public string PunchardVariableID;

			public PunchcardVariable.VariableTypes VariableType;
		}

		public List<PunchardVariableSetup> Variables;

		public override void Run(StateNode.NodeExecutionState state)
		{
		}
	}
}
