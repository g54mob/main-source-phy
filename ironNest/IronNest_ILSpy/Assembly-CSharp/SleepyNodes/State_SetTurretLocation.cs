using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_SetTurretLocation : StateNode
{
	public StateNode To;

	public LocationSelection LocationToMoveTo = new LocationSelection
	{
		LocationType = LocationSelection.LocationTypes.Relative,
		RelativeTo = LocationSelection.RelativeReferenceTypes.Self,
		RelativeDirection = LocationSelection.RelativeDirections.BearingDistance
	};

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_00de: Expected O, but got F4
		//IL_012c: Expected I, but got O
		//IL_0134: Expected I, but got O
		//IL_0144: Expected O, but got I
		//IL_01c4: Expected O, but got I4
		//IL_0180: Expected O, but got I
		//IL_01f0: Expected O, but got Ref
		//IL_01b6: Expected O, but got I4
		base.OnEnter(state);
		GameObject gameObject = GameObject.FindWithTag("MissionParent");
		if (!(gameObject != null))
		{
			return;
		}
		TurretController turretController = Object.FindFirstObjectByType<TurretController>();
		if (!(turretController != null) || !(FireMission._003CInstance_003Ek__BackingField != null))
		{
			return;
		}
		Vector3[] array = new Vector3[4];
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		RectTransform rectTransform = default(RectTransform);
		rectTransform.GetWorldCorners(array);
		MapEntity mapEntity = new MapEntity();
		Vector3 localPosition = turretController.turretBase.localPosition;
		mapEntity.Position = (Vector3)localPosition.x;
		_ = localPosition.z;
		NodeGraph nodeGraph = graph;
		if ((object)graph == null)
		{
			goto IL_022c;
		}
		nint num = (nint)typeof(MissionGraph);
		nint num2 = (nint)nodeGraph;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v463 @ r8_v17 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v464 @ r9_v11 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v463 @ r8_v17 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
		object obj3;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v464 @ r9_v11 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v511 @ rax_v36+FFFFFFF8+v465 @ rax_v31*8]");
			if (0 == (nint)typeof(MissionGraph))
			{
				obj3 = 1;
				goto IL_025f;
			}
		}
		obj3 = 0;
		goto IL_025f;
		IL_025f:
		if (obj3 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Warning: Method ends with non empty stack (-78), the output could be wrong!");
			/*Error: End of method reached without returning.*/;
		}
		goto IL_022c;
		IL_022c:
		MissionGraph missionGraph = default(MissionGraph);
		Vector3[] gridBounds = default(Vector3[]);
		GridReference gridReference = LocationToMoveTo.Resolve(FireMission._003CInstance_003Ek__BackingField, mapEntity, state, missionGraph, gridBounds);
		LocationSelection locationToMoveTo = LocationToMoveTo;
		Vector3 location = gridReference.GetLocation(array, locationToMoveTo.FuzzyLocation);
		object obj4 = default(object);
		turretController.SetTurretLocation((Vector3)(&obj4));
		base.OnExit(state, "To");
	}
}
