using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SleepyNodes
{
	[CreateNodeMenu("Branches/Random")]
	[NodeName("Random Branch")]
	[NodeWidth(300)]
	public class State_RandomBranch : StateNode
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public enum SelectionTypes
		{
			Random = 0,
			RoundRobin = 1,
			Weighted = 2
		}

		[Serializable]
		public class Path
		{
			public int Weight;
		}

		public SelectionTypes SelectionType;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never, dynamicPortList = true)]
		public int[] To;

		private int lastIndex;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}
	}
}
