using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class Event_ShellLanded : EventNode
{
	public enum HitTypes
	{
		Any,
		Hit,
		Miss
	}

	public ShellDefinition Shell;

	public HitTypes HitType;

	public FilterEntitySet EntityFilter;

	public LocationFilter LocationFilter;

	public EntityContextKeys EntityHit = EntityContextKeys.EntityEffected;

	public LocationContextKeys LocationHit;

	private MapEntity cachedEntity;

	private GridReference cachedLocation;

	protected override bool ShouldRun(EventData data)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_00fe: Expected O, but got I
		//IL_04dd: Expected I4, but got O
		//IL_012b: Expected O, but got I
		//IL_0193: Expected O, but got I
		//IL_03be: Expected O, but got I4
		//IL_02f2: Expected I, but got O
		//IL_02fa: Expected I, but got O
		//IL_030a: Expected O, but got I
		//IL_0439: Expected O, but got I
		//IL_038a: Expected O, but got I4
		//IL_0346: Expected O, but got I
		//IL_0481: Expected O, but got I
		//IL_03fd: Expected O, but got I
		//IL_037c: Expected O, but got I4
		if (data != null)
		{
			nint num = (nint)typeof(EventData_Impact);
			nint num2 = (nint)data;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdx_v3 (Il2CppClass<SleepyNodes.EventData_Impact>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ r8_v3 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdx_v3 (Il2CppClass<SleepyNodes.EventData_Impact>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ r8_v3 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ rax_v6+FFFFFFF8+v54 @ rax_v5*8]");
				if (0 == (nint)typeof(EventData_Impact))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+20]");
					if ((nint)0 != 0)
					{
						if (Shell != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
							if ((UnityEngine.Object)0 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
								object obj3 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
								if ((nint)0 != 0)
								{
									ShellDefinition shell = Shell;
									if ((object)Shell != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rcx_v41+18]");
										if ((string)0 == shell.ShellId)
										{
											goto IL_01a5;
										}
										goto IL_041a;
									}
								}
								goto IL_04cf;
							}
						}
						goto IL_01a5;
					}
				}
			}
		}
		goto IL_041a;
		IL_0537:
		object obj4;
		bool result = default(bool);
		if (obj4 != null)
		{
			return result;
		}
		goto IL_04fa;
		IL_01a5:
		GameObject gameObject = GameObject.FindWithTag("MissionParent");
		if (!(gameObject != null))
		{
			goto IL_041a;
		}
		FireMission fireMission = UnityEngine.Object.FindFirstObjectByType<FireMission>();
		GridReference location;
		if ((object)fireMission != null)
		{
			Vector2 localSpace = default(Vector2);
			location = GridReference.FromLocalSpace(localSpace, fireMission.cellWidth, fireMission.cellHeight, fireMission.yIncreasesUp);
			Vector3[] fourCornersArray = new Vector3[4];
			if ((object)gameObject != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				RectTransform rectTransform = default(RectTransform);
				if ((object)rectTransform != null)
				{
					rectTransform.GetWorldCorners(fourCornersArray);
					if (LocationFilter == null)
					{
						goto IL_038f;
					}
					NodeExecutionState newState = NodeExecutionState.NewState;
					NodeGraph nodeGraph = graph;
					if ((object)graph == null)
					{
						goto IL_04fa;
					}
					nint num4 = (nint)typeof(MissionGraph);
					nint num5 = (nint)nodeGraph;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v646 @ rdx_v23 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v647 @ r9_v10 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
					nint num6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v646 @ rdx_v23 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
					if (num6 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v647 @ r9_v10 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
						object obj6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v693 @ rax_v39+FFFFFFF8+v648 @ rax_v35*8]");
						if (0 == (nint)typeof(MissionGraph))
						{
							obj4 = 1;
							goto IL_0537;
						}
					}
					obj4 = 0;
					goto IL_0537;
				}
			}
		}
		goto IL_04cf;
		IL_04fa:
		NodeExecutionState state = default(NodeExecutionState);
		MissionGraph missionGraph = default(MissionGraph);
		Vector3[] gridBounds = default(Vector3[]);
		if (LocationFilter.Resolve(location, fireMission, null, state, missionGraph, gridBounds))
		{
			goto IL_038f;
		}
		goto IL_041a;
		IL_041a:
		return false;
		IL_038f:
		bool flag = HitType == HitTypes.Any;
		if (!flag)
		{
			object obj7 = HitType - 1;
			if (flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+28]");
				if (Enumerable.Any((IEnumerable<MapEntity>)0))
				{
					Func<MapEntity, bool> predicate = delegate(MapEntity x)
					{
						//IL_0052: Expected I4, but got O
						NodeExecutionState newState2 = NodeExecutionState.NewState;
						if (EntityFilter == null)
						{
							NullReferenceException ex2 = new NullReferenceException();
							return (byte)(int)ex2 != 0;
						}
						return EntityFilter.Resolve(x, newState2);
					};
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+28]");
					if (Enumerable.Any((IEnumerable<MapEntity>)0, predicate))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
						MapEntity mapEntity = default(MapEntity);
						cachedEntity = mapEntity;
						goto IL_04b7;
					}
				}
				goto IL_041a;
			}
			if ((nint)obj7 == 1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+28]");
				if (Enumerable.Any((IEnumerable<MapEntity>)0))
				{
					goto IL_041a;
				}
			}
		}
		goto IL_04b7;
		IL_04cf:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_04b7:
		cachedLocation = location;
		return true;
	}

	public override void Run(NodeExecutionState state)
	{
		state.Set(EntityHit, cachedEntity);
		state.Set(LocationHit, cachedLocation);
		base.Run(state);
	}

	public Event_ShellLanded()
	{
		EnableOnStart = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}

	private bool _003CShouldRun_003Eb__9_0(MapEntity x)
	{
		//IL_0052: Expected I4, but got O
		NodeExecutionState newState = NodeExecutionState.NewState;
		if (EntityFilter != null)
		{
			return EntityFilter.Resolve(x, newState);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
