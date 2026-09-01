using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace SleepyNodes;

public class State_SetEntityState : StateNode
{
	public enum Operations
	{
		Add,
		Remove
	}

	public StateNode To;

	public TargetSelection Entity;

	public Operations Operation;

	public MapEntityStates State = MapEntityStates.Moving;

	public override void ResetNode()
	{
	}

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_004e: Expected O, but got I4
		//IL_00cd: Expected O, but got Ref
		//IL_016c: Expected O, but got Ref
		//IL_00f6: Expected O, but got I4
		//IL_0195: Expected O, but got I4
		base.OnEnter(state);
		if (Entity != null)
		{
			List<MapEntity> list = Entity.Resolve(FireMission._003CInstance_003Ek__BackingField, state);
			if (list != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				object obj = 0;
				List<MapEntity>.Enumerator enumerator = default(List<MapEntity>.Enumerator);
				MapEntity mapEntity = default(MapEntity);
				FireMission fireMission = default(FireMission);
				object obj2 = default(object);
				MapEntityStates newState = default(MapEntityStates);
				FireMission fireMission2 = default(FireMission);
				MapEntityStates newState2 = default(MapEntityStates);
				while (true)
				{
					if (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (Operation == Operations.Add)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
							bool flag = mapEntity == null;
							TargetSelection targetSelection = null;
							if (flag)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D71C0");
							bool flag2 = (object)fireMission == null;
							targetSelection = (TargetSelection)(&obj2);
							if (flag2)
							{
								break;
							}
							fireMission.SetEntityState(mapEntity, newState);
							obj = 0;
						}
						else if (Operation == Operations.Remove)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
							bool flag3 = mapEntity == null;
							TargetSelection targetSelection = null;
							if (flag3)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D74B0");
							bool flag4 = (object)fireMission2 == null;
							targetSelection = (TargetSelection)(&obj2);
							if (flag4)
							{
								throw new NullReferenceException();
							}
							fireMission2.SetEntityState(mapEntity, newState2);
							obj = 0;
						}
						continue;
					}
					enumerator.Dispose();
					base.OnExit(state, "To");
					return;
				}
				throw new NullReferenceException();
			}
		}
		throw new NullReferenceException();
	}

	public State_SetEntityState()
	{
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
