using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_TestNode : StateNode
{
	public StateNode To;

	public TargetSelection Targets;

	public LocationSelection Location;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0163: Expected I, but got O
		//IL_0171: Expected I, but got O
		//IL_0181: Expected O, but got I
		//IL_01bd: Expected O, but got I
		//IL_01e2: Expected O, but got I4
		base.OnEnter(state);
		GameObject gameObject = GameObject.FindWithTag("MissionParent");
		if (!(gameObject != null) || !(FireMission._003CInstance_003Ek__BackingField != null))
		{
			return;
		}
		Vector3[] fourCornersArray = new Vector3[4];
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		RectTransform rectTransform = default(RectTransform);
		rectTransform.GetWorldCorners(fourCornersArray);
		List<MapEntity> list = Targets.Resolve(FireMission._003CInstance_003Ek__BackingField, state);
		if (list == null || Enumerable.Count(list) == 0)
		{
			Debug.LogError("No Entities Found For Moving");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MapEntity>.Enumerator enumerator = default(List<MapEntity>.Enumerator);
		MapEntity mapEntity = default(MapEntity);
		MissionGraph missionGraph = default(MissionGraph);
		Vector3[] gridBounds = default(Vector3[]);
		while (true)
		{
			NodeGraph nodeGraph2;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (mapEntity == null)
				{
					break;
				}
				NodeGraph nodeGraph = graph;
				if ((object)graph == null)
				{
					goto IL_024b;
				}
				nint num = (nint)nodeGraph;
				nint num2 = (nint)typeof(MissionGraph);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v524 @ r8_v14 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ r9_v7 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v524 @ r8_v14 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ r9_v7 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v556 @ rax_v35+FFFFFFF8+v525 @ rax_v30*8]");
					bool flag = 0 == (nint)typeof(MissionGraph);
					nodeGraph2 = (NodeGraph)1;
					if (flag)
					{
						goto IL_0275;
					}
				}
				nodeGraph2 = null;
				goto IL_0275;
			}
			enumerator.Dispose();
			return;
			IL_0275:
			if ((object)nodeGraph2 == null)
			{
			}
			goto IL_024b;
			IL_024b:
			GridReference gridReference = Location.Resolve(FireMission._003CInstance_003Ek__BackingField, mapEntity, state, missionGraph, gridBounds);
		}
		throw new NullReferenceException();
	}

	public override void OnExecute(NodeExecutionState state)
	{
		//IL_0038: Expected I, but got O
		//IL_0048: Expected O, but got I
		//IL_0058: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7D4]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ r9_v1 (Il2CppClass<SleepyNodes.State_TestNode>)+218]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ r9_v1 (Il2CppClass<SleepyNodes.State_TestNode>)+220]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v34 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override object GetValue(NodePort port)
	{
		return null;
	}

	public State_TestNode()
	{
		TargetSelection targets = new TargetSelection();
		Targets = targets;
		Location = new LocationSelection();
		base._002Ector();
	}
}
