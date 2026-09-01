using System;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_TriggerImpact : StateNode
{
	public StateNode To;

	public ShellDefinition Shell;

	public LocationSelection Location;

	public bool UsePrefabEffect;

	public bool TriggerNormalEvents;

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_010e: Expected I, but got O
		//IL_0116: Expected I, but got O
		//IL_0126: Expected O, but got I
		//IL_01a6: Expected O, but got I4
		//IL_0162: Expected O, but got I
		//IL_01d3: Expected O, but got Ref
		//IL_0198: Expected O, but got I4
		base.OnEnter(state);
		Vector3[] gridBounds;
		object obj3;
		if (FireMission._003CInstance_003Ek__BackingField != null && Shell != null)
		{
			gridBounds = FireMission._003CInstance_003Ek__BackingField.GetGridBounds();
			if (gridBounds == null || gridBounds.Length < 4)
			{
				goto IL_0269;
			}
			if (Location == null)
			{
				LocationSelection location = new LocationSelection();
				Location = location;
			}
			NodeGraph nodeGraph = graph;
			if ((object)graph == null)
			{
				goto IL_02a5;
			}
			nint num = (nint)typeof(MissionGraph);
			nint num2 = (nint)nodeGraph;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v457 @ r8_v18 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v458 @ r9_v13 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v457 @ r8_v18 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v458 @ r9_v13 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v504 @ rax_v39+FFFFFFF8+v459 @ rax_v34*8]");
				if (0 == (nint)typeof(MissionGraph))
				{
					obj3 = 1;
					goto IL_02d5;
				}
			}
			obj3 = 0;
			goto IL_02d5;
		}
		base.OnExit(state, "To");
		return;
		IL_0269:
		base.OnExit(state, "To");
		return;
		IL_02a5:
		MissionGraph missionGraph = default(MissionGraph);
		Vector3[] gridBounds2 = default(Vector3[]);
		GridReference gridReference = Location.Resolve(FireMission._003CInstance_003Ek__BackingField, null, state, missionGraph, gridBounds2);
		LocationSelection location2 = Location;
		Vector3 location3 = gridReference.GetLocation(gridBounds, location2.FuzzyLocation);
		object obj4 = default(object);
		Vector2 impactLocation = FireMission._003CInstance_003Ek__BackingField.ToLocalSpace((Vector3)(&obj4));
		if (UsePrefabEffect)
		{
			ShellDefinition shell = Shell;
			if (shell.ImpactEffectPrefab != null)
			{
				SpawnImpactPrefab(impactLocation);
				goto IL_0269;
			}
		}
		ImpactTracker.EvaluateImpact(Shell, impactLocation, TriggerNormalEvents);
		goto IL_0269;
		IL_02d5:
		if (obj3 != null)
		{
			return;
		}
		goto IL_02a5;
	}

	private unsafe void SpawnImpactPrefab(Vector2 impactLocation)
	{
		//IL_0064: Expected O, but got Ref
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0092: Expected O, but got I4
		//IL_009b: Expected O, but got I4
		//IL_00c4: Expected O, but got I
		//IL_0113: Expected O, but got I
		//IL_00f7: Expected O, but got I
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0141: Expected O, but got I
		ImpactMarkerManager impactMarkerManager = UnityEngine.Object.FindFirstObjectByType<ImpactMarkerManager>();
		ShellDefinition shell = Shell;
		Transform transform = impactMarkerManager.transform;
		ImpactLocation impactLocation2 = UnityEngine.Object.Instantiate(shell.ImpactEffectPrefab, transform);
		Transform transform2 = impactLocation2.transform;
		object obj = default(object);
		transform2.localPosition = (Vector3)(&obj);
		ImpactVisualCorrections[] componentsInChildren = impactLocation2.GetComponentsInChildren<ImpactVisualCorrections>(includeInactive: true);
		object obj2 = componentsInChildren + 32;
		object obj3 = 0;
		object obj4 = 0;
		while ((nint)obj4 < componentsInChildren.Length)
		{
			UnityEngine.Object obj5 = (UnityEngine.Object)obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rbx_v6 (UnityEngine.Object)+38]");
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rbx_v6 (UnityEngine.Object)+38]");
				UnityEngine.Object.Destroy((UnityEngine.Object)0);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rbx_v6 (UnityEngine.Object)+40]");
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rbx_v6 (UnityEngine.Object)+40]");
				GameObject gameObject = ((Component)0).gameObject;
				UnityEngine.Object.Destroy(gameObject);
			}
			UnityEngine.Object.Destroy(obj5);
			obj3++;
			obj2 += 8;
			obj4 = obj3;
		}
		impactLocation2.Init(Shell, TriggerNormalEvents);
	}

	public State_TriggerImpact()
	{
		LocationSelection location = new LocationSelection();
		Location = location;
		UsePrefabEffect = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
