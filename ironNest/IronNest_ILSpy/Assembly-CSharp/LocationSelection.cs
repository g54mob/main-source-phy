using System;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;

[Serializable]
public class LocationSelection
{
	public enum LocationTypes
	{
		GridLocation,
		Zone,
		Entity,
		ContextLocation,
		ContextEntity,
		Relative,
		Turret
	}

	public enum RelativeReferenceTypes
	{
		Self,
		EntityFromFilter,
		GridLocation,
		ContextLocation,
		ContextEntity,
		Turret
	}

	public enum RelativeDirections
	{
		Offset,
		Towards,
		Away,
		RandomInRadius,
		BearingDistance
	}

	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public LocationSelection _003C_003E4__this;

		public StateNode.NodeExecutionState state;

		internal bool _003CResolve_003Eb__0(Zone x)
		{
			//IL_0074: Expected I4, but got O
			if (x != null)
			{
				LocationSelection locationSelection = _003C_003E4__this;
				if (_003C_003E4__this != null)
				{
					return x.ID == locationSelection.ZoneID;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CResolve_003Eb__1(MapEntity x)
		{
			//IL_007a: Expected I4, but got O
			LocationSelection locationSelection = _003C_003E4__this;
			if (_003C_003E4__this != null && locationSelection.TargetFilter != null)
			{
				return locationSelection.TargetFilter.Resolve(x, state);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass20_0
	{
		public LocationSelection _003C_003E4__this;

		public StateNode.NodeExecutionState state;

		internal bool _003CResolveRelative_003Eb__0(MapEntity x)
		{
			//IL_007a: Expected I4, but got O
			LocationSelection locationSelection = _003C_003E4__this;
			if (_003C_003E4__this != null && locationSelection.TargetFilter != null)
			{
				return locationSelection.TargetFilter.Resolve(x, state);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public LocationTypes LocationType;

	public ContextVariableOrInline_GridRefence GridLocation;

	public string ZoneID;

	public FilterEntitySet TargetFilter;

	public ContextKey_Location ContextLocationKey;

	public ContextKey_Entity ContextEntityKey;

	public RelativeReferenceTypes RelativeTo;

	public RelativeDirections RelativeDirection;

	public Vector2Int OffsetMin;

	public Vector2Int OffsetMax;

	public ContextVariableOrInline_Float DistanceMin;

	public ContextVariableOrInline_Float DistanceMax;

	public ContextVariableOrInline_Float Bearing;

	public bool FuzzyLocation = true;

	public bool RandomiseSubgrid;

	[NonSerialized]
	public bool DidClampToMap;

	public unsafe GridReference Resolve(FireMission fireMission, MapEntity self, StateNode.NodeExecutionState state, MissionGraph missionGraph, Vector3[] gridBounds)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0048: Expected O, but got I8
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_003C_003Ec__DisplayClass19_0 obj3 = new _003C_003Ec__DisplayClass19_0();
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 91 Invalid \"Jump target not found in method: 0x180550061\"");
		obj3._003C_003E4__this = this;
		obj3.state = state;
		DidClampToMap = false;
		System.Random random = new System.Random();
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 121 Invalid \"Jump target not found in method: 0x18055003B\"");
		return (GridReference)6442450944L;
	}

	private GridReference ResolveRelative(FireMission fireMission, MapEntity self, StateNode.NodeExecutionState state, MissionGraph missionGraph, Vector3[] gridBounds)
	{
		//IL_0057: Expected O, but got I8
		//IL_0071: Expected O, but got I8
		_003C_003Ec__DisplayClass20_0 obj = new _003C_003Ec__DisplayClass20_0();
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.state = state;
			RelativeReferenceTypes relativeTo = RelativeTo;
			if (RelativeTo <= RelativeReferenceTypes.Turret)
			{
				object obj2 = 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rdx_v6+54F330+v76 @ rax_v8 (LocationSelection+RelativeReferenceTypes)*4]");
				object obj3 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v133 @ rcx_v9 (should have been resolved before IL gen)");
			}
			if (GridLocation != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
				GridReference result = default(GridReference);
				return result;
			}
		}
		return (GridReference)(object)new NullReferenceException();
	}
}
