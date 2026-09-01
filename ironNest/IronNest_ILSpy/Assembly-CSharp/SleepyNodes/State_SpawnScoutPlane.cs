using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_SpawnScoutPlane : StateNode
{
	public StateNode To;

	public GameObject PlanePrefab;

	public LocationSelection LocationToSpawn;

	public bool RandomBearing = true;

	public ContextVariableOrInline_Float Bearing;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_02fd: Expected I, but got O
		//IL_030d: Expected O, but got I
		//IL_031d: Expected O, but got I
		//IL_00e5: Expected I, but got O
		//IL_00ed: Expected I, but got O
		//IL_00fd: Expected O, but got I
		//IL_017d: Expected O, but got I4
		//IL_0139: Expected O, but got I
		//IL_016f: Expected O, but got I4
		//IL_0266: Expected O, but got Ref
		//IL_01eb: Expected O, but got Ref
		while (true)
		{
			base.OnEnter(state);
			GameObject gameObject = GameObject.FindWithTag("MissionParent");
			if (gameObject != null)
			{
				break;
			}
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ r9_v1 (Il2CppClass<SleepyNodes.State_SpawnScoutPlane>)+218]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ r9_v1 (Il2CppClass<SleepyNodes.State_SpawnScoutPlane>)+220]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v98 @ rax_v6 (should have been resolved before IL gen)");
		}
		ImpactMarkerManager impactMarkerManager = Object.FindFirstObjectByType<ImpactMarkerManager>();
		Vector3[] array;
		object obj5;
		if (impactMarkerManager != null)
		{
			if (FireMission._003CInstance_003Ek__BackingField != null)
			{
				array = new Vector3[4];
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				RectTransform rectTransform = default(RectTransform);
				rectTransform.GetWorldCorners(array);
				NodeGraph nodeGraph = graph;
				if ((object)graph == null)
				{
					goto IL_0349;
				}
				nint num2 = (nint)typeof(MissionGraph);
				nint num3 = (nint)nodeGraph;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v715 @ r8_v28 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v716 @ r9_v19 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v715 @ r8_v28 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
				if (num4 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v716 @ r9_v19 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v763 @ rax_v58+FFFFFFF8+v717 @ rax_v53*8]");
					if (0 == (nint)typeof(MissionGraph))
					{
						obj5 = 1;
						goto IL_0379;
					}
				}
				obj5 = 0;
				goto IL_0379;
			}
			base.OnExit(state, "To");
			return;
		}
		base.OnExit(state, "To");
		return;
		IL_0379:
		if (obj5 == null)
		{
			goto IL_0349;
		}
		goto IL_0391;
		IL_0349:
		MissionGraph missionGraph = default(MissionGraph);
		Vector3[] gridBounds = default(Vector3[]);
		GridReference gridReference = LocationToSpawn.Resolve(FireMission._003CInstance_003Ek__BackingField, null, state, missionGraph, gridBounds);
		LocationSelection locationToSpawn = LocationToSpawn;
		Vector3 location = gridReference.GetLocation(array, locationToSpawn.FuzzyLocation);
		Transform transform = impactMarkerManager.transform;
		GameObject gameObject2 = Object.Instantiate(PlanePrefab, transform);
		Transform transform2 = gameObject2.transform;
		Vector3 euler = default(Vector3);
		transform2.position = (Vector3)(&euler);
		if (RandomBearing)
		{
			int num5 = Random.Range(0, 360);
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
		}
		goto IL_0391;
		IL_0391:
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		Transform transform3 = gameObject2.transform;
		Transform transform4 = gameObject2.transform;
		Quaternion localRotation = transform4.localRotation;
		float num6 = default(float);
		transform3.localRotation = (Quaternion)(&num6);
		GameObject gameObject3 = gameObject2.gameObject;
		gameObject3.SetActive(value: true);
		object obj6 = default(object);
		object arg = (Vector3)obj6;
		string message = $"Spawning Scout Plane At: {arg} ({gridReference})";
		Debug.Log(message);
		base.OnExit(state, "To");
	}
}
