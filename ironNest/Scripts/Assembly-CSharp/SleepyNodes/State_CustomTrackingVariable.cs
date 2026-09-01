using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SleepyNodes
{
	[CreateNodeMenu("Mission/Custom Tracking Variable")]
	[NodeName("Custom Tracking Variable")]
	[NodeWidth(400)]
	public class State_CustomTrackingVariable : StateNode
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public enum Operations
		{
			Set = 0,
			Add = 1,
			Subtract = 2
		}

		[JsonConverter(typeof(StringEnumConverter))]
		public enum Sources
		{
			Inline = 0,
			CurrentTime = 1,
			FilterCount = 2
		}

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public string TrackingVariable;

		public Operations Operation;

		public Sources Source;

		public float Value;

		public TargetSelection Filter;

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public float ResolveValue(NodeExecutionState state)
		{
			return 0f;
		}
	}
}
