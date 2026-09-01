namespace SleepyNodes
{
	[CreateNodeMenu("Entity/Damage Entity")]
	[NodeWidth(400)]
	[NodeName("Damage Entity")]
	public class State_DamageEntity : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public TargetSelection EntitiesToDamage;

		public int Damage;

		public ShellDefinition Shell;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode OnEntityDestroyed;

		public EntityContextKeys EntityDestroyed;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
