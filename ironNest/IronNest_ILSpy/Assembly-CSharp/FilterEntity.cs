using System;
using Cpp2ILInjected;
using SleepyNodes;

[Serializable]
public class FilterEntity
{
	public enum FilterEntityTypes
	{
		ID,
		Role,
		Health,
		Armour,
		State,
		Stars,
		DistanceFromEntity,
		DistanceFromLocation
	}

	public enum OperationTypes
	{
		Equals,
		NotEquals,
		LessThan,
		GreaterThan,
		LessThanOrEquals,
		GreaterThanOrEquals,
		Contains,
		NotContains
	}

	public enum IdTypes
	{
		Graph,
		Explict
	}

	public FilterEntityTypes FilterEntityType;

	public OperationTypes Operation;

	public IdTypes IdType;

	public string StringValue;

	public EntityRoles RoleValue;

	public int IntValue;

	public MapEntityStates StateValue;

	public EntityContextKeys DistanceEntityKey;

	public LocationContextKeys DistanceLocationKey;

	public float FloatValue;

	public bool BoolValue;

	public unsafe bool Execute(MapEntity entity, StateNode.NodeExecutionState state)
	{
		//IL_0008: Expected O, but got Ref
		//IL_003a: Expected O, but got I8
		//IL_0054: Expected O, but got I8
		object obj2 = default(object);
		object obj = (object)(&obj2);
		FilterEntityTypes filterEntityType = FilterEntityType;
		_ = 0;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 51 Invalid \"Jump target not found in method: 0x18054A4ED\"");
		object obj3 = 6442450944L;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ r14_v1+54AB34+v39 @ rax_v2 (FilterEntity+FilterEntityTypes)*4]");
		object obj4 = 0 + 6442450944L;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v59 @ rax_v4 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
