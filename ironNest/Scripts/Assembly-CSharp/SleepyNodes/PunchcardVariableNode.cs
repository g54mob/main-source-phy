namespace SleepyNodes
{
	[CreateNodeMenu("Cards/Punchcard Variable")]
	[NodeWidth(400)]
	[NodeName("Punchcard Variable")]
	public class PunchcardVariableNode : Node
	{
		public string ID;

		public PunchcardVariable.VariableTypes VariableType;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, backingValue = ShowBackingValue.Never)]
		public int VariableInt;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, backingValue = ShowBackingValue.Never)]
		public float VariableFloat;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, backingValue = ShowBackingValue.Never)]
		public string VariableText;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, backingValue = ShowBackingValue.Never)]
		public bool VariableBool;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, backingValue = ShowBackingValue.Never)]
		public GridReference VariableCoordinate;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, backingValue = ShowBackingValue.Never)]
		public ShellSlotPool.ShellSlotSides VariableShellSlot;

		public override object GetValue(NodePort port)
		{
			return null;
		}
	}
}
