using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_WaitShellLanded : StateNode
{
	private sealed class _003C_003Ec__DisplayClass10_0
	{
		public EventData_Impact impactEvent;

		internal bool _003COnEvent_003Eb__0(MapEntity x)
		{
			//IL_0050: Expected I4, but got O
			EventData_Impact eventData_Impact = impactEvent;
			if (impactEvent != null && eventData_Impact.ImpactEntities != null)
			{
				return eventData_Impact.ImpactEntities.Contains(x);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public StateNode To;

	public StateNode Cancel;

	public StateNode OnCancelled;

	public TargetSelection EntityFilter;

	public LocationFilter LocationFilter;

	public ShellDefinition Shell;

	[NonSerialized]
	private bool cancelCalled;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		cancelCalled = false;
	}

	public override void OnEnter(NodeExecutionState state)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7CF]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		base.OnEnter(state);
		bool flag = state.lastFieldPort == "Cancel";
		if (!flag)
		{
			cancelCalled = flag;
			state.ListeningToEvents = true;
		}
		else
		{
			cancelCalled = true;
			base.OnExit(state, null, null);
		}
	}

	public override void OnExecute(NodeExecutionState state)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7D0]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (cancelCalled)
		{
			state.ListeningToEvents = false;
			base.OnExit(state, "OnCancelled");
		}
	}

	public override void OnEvent(EventNode.EventData data, NodeExecutionState state)
	{
		//IL_0042: Expected I, but got O
		//IL_004a: Expected I, but got O
		//IL_005a: Expected O, but got I
		//IL_0096: Expected O, but got I
		//IL_00bb: Expected O, but got I4
		//IL_052e: Expected I, but got O
		//IL_0536: Expected I, but got O
		//IL_0546: Expected O, but got I
		//IL_00f0: Expected O, but got I
		//IL_0115: Expected O, but got I4
		//IL_0312: Expected I, but got O
		//IL_031a: Expected I, but got O
		//IL_032a: Expected O, but got I
		//IL_0366: Expected O, but got I
		//IL_038b: Expected O, but got I4
		_003C_003Ec__DisplayClass10_0 CS_0024_003C_003E8__locals8 = new _003C_003Ec__DisplayClass10_0();
		if (data == null)
		{
			CS_0024_003C_003E8__locals8.impactEvent = null;
			goto IL_0452;
		}
		nint num = (nint)typeof(EventData_Impact);
		nint num2 = (nint)data;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rdx_v23 (Il2CppClass<SleepyNodes.EventData_Impact>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v278 @ r8_v20 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rdx_v23 (Il2CppClass<SleepyNodes.EventData_Impact>)+130]");
		EventData_Impact eventData_Impact;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v278 @ r8_v20 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v327 @ rax_v61+FFFFFFF8+v279 @ rax_v49*8]");
			bool flag = 0 == (nint)typeof(EventData_Impact);
			eventData_Impact = (EventData_Impact)1;
			if (flag)
			{
				goto IL_0474;
			}
		}
		eventData_Impact = null;
		goto IL_0474;
		IL_03a3:
		FireMission fireMission;
		if (EntityFilter != null)
		{
			List<MapEntity> source = EntityFilter.Resolve(fireMission, state);
			Func<MapEntity, bool> predicate = delegate(MapEntity x)
			{
				//IL_0050: Expected I4, but got O
				EventData_Impact impactEvent5 = CS_0024_003C_003E8__locals8.impactEvent;
				if (CS_0024_003C_003E8__locals8.impactEvent == null || impactEvent5.ImpactEntities == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				return impactEvent5.ImpactEntities.Contains(x);
			};
			if (!Enumerable.Any(source, predicate))
			{
				return;
			}
		}
		state.ListeningToEvents = false;
		base.OnExit(state, "To");
		return;
		IL_0513:
		EventNode.EventData impactEvent = default(EventNode.EventData);
		CS_0024_003C_003E8__locals8.impactEvent = (EventData_Impact)impactEvent;
		nint num4 = (nint)typeof(EventData_Impact);
		nint num5 = (nint)data;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v556 @ rdx_v24 (Il2CppClass<SleepyNodes.EventData_Impact>)+130]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v295 @ r8_v21 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v556 @ rdx_v24 (Il2CppClass<SleepyNodes.EventData_Impact>)+130]");
		EventData_Impact eventData_Impact2;
		if (num6 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v295 @ r8_v21 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v583 @ rax_v57+FFFFFFF8+v557 @ rax_v53*8]");
			bool flag2 = 0 == (nint)typeof(EventData_Impact);
			eventData_Impact2 = (EventData_Impact)1;
			if (flag2)
			{
				goto IL_0496;
			}
		}
		eventData_Impact2 = null;
		goto IL_0496;
		IL_04fb:
		EventData_Impact eventData_Impact3;
		if (eventData_Impact3 == null)
		{
			goto IL_04be;
		}
		goto IL_0513;
		IL_0474:
		bool flag3 = eventData_Impact == null;
		impactEvent = null;
		if (!flag3)
		{
			impactEvent = data;
		}
		goto IL_0513;
		IL_04be:
		GridReference location;
		NodeExecutionState state2 = default(NodeExecutionState);
		MissionGraph missionGraph = default(MissionGraph);
		Vector3[] gridBounds = default(Vector3[]);
		if (!LocationFilter.Resolve(location, fireMission, null, state2, missionGraph, gridBounds))
		{
			return;
		}
		goto IL_03a3;
		IL_0496:
		if (eventData_Impact2 != null)
		{
			return;
		}
		goto IL_0452;
		IL_0452:
		if (CS_0024_003C_003E8__locals8.impactEvent == null)
		{
			return;
		}
		EventData_Impact impactEvent2 = CS_0024_003C_003E8__locals8.impactEvent;
		if (!impactEvent2.TriggerNormalEvents)
		{
			return;
		}
		if (Shell != null)
		{
			EventData_Impact impactEvent3 = CS_0024_003C_003E8__locals8.impactEvent;
			if (impactEvent3.ImpactShell != null)
			{
				EventData_Impact impactEvent4 = CS_0024_003C_003E8__locals8.impactEvent;
				ShellDefinition impactShell = impactEvent4.ImpactShell;
				ShellDefinition shell = Shell;
				if (!(impactShell.ShellId == shell.ShellId))
				{
					return;
				}
			}
		}
		GameObject gameObject = GameObject.FindWithTag("MissionParent");
		if (!(gameObject != null))
		{
			return;
		}
		fireMission = FireMission._003CInstance_003Ek__BackingField;
		Vector2 localSpace = default(Vector2);
		location = GridReference.FromLocalSpace(localSpace, fireMission.cellWidth, fireMission.cellHeight, fireMission.yIncreasesUp);
		Vector3[] fourCornersArray = new Vector3[4];
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		RectTransform rectTransform = default(RectTransform);
		rectTransform.GetWorldCorners(fourCornersArray);
		if (LocationFilter == null)
		{
			goto IL_03a3;
		}
		NodeGraph nodeGraph = graph;
		if ((object)graph == null)
		{
			goto IL_04be;
		}
		nint num7 = (nint)typeof(MissionGraph);
		nint num8 = (nint)nodeGraph;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v764 @ r8_v17 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v765 @ r9_v12 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
		nint num9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v764 @ r8_v17 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
		if (num9 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v765 @ r9_v12 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v823 @ rax_v38+FFFFFFF8+v766 @ rax_v33*8]");
			bool flag4 = 0 == (nint)typeof(MissionGraph);
			eventData_Impact3 = (EventData_Impact)1;
			if (flag4)
			{
				goto IL_04fb;
			}
		}
		eventData_Impact3 = null;
		goto IL_04fb;
	}
}
