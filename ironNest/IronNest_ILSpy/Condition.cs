using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using SleepyNodes;

[Serializable]
public class Condition
{
	public enum ConditionTypes
	{
		Context,
		Entity,
		Filter,
		Comparison
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

	public enum ContextConditions
	{
		RequisitionPoints,
		PowederCharges,
		TimerExists,
		TimerRunning,
		TimerTimeRemaining,
		MissionIsCompleteThisRun,
		MissionIsCompletePreviously,
		MedalsEarnedThisRun,
		MedalsEarnedBest,
		TimeSinceMissionStart,
		MissionIsFailedThisRun,
		TimerExpired,
		GenericTimerValue,
		TurretMoving
	}

	public enum EnittyConditions
	{
		ID,
		Role,
		Health,
		Armour,
		State,
		Stars,
		DistanceFromEntity,
		DistanceFromLocation,
		DistanceFromTurret
	}

	public enum FilterConditions
	{
		Count
	}

	public enum ComparisonConditions
	{
		Distance,
		Medals
	}

	[Serializable]
	public class EntityIDLookup
	{
		public enum IdTypes
		{
			Graph,
			Explict,
			Context,
			Filter
		}

		public IdTypes IdType;

		public string Value;

		public EntityContextKeys ContextKey;

		public int FilterIndex;

		public MapEntity Resolve(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
		{
			//IL_0015: Expected O, but got I4
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			bool flag = IdType == IdTypes.Graph;
			MapEntity result;
			if (!flag)
			{
				object obj = IdType - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						bool flag2 = (nint)obj2 != 1;
						result = null;
						if (!flag2)
						{
							if (filteredEntities == null)
							{
								goto IL_01b2;
							}
							bool flag3 = filteredEntities._size <= FilterIndex;
							result = null;
							if (!flag3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								MapEntity mapEntity = default(MapEntity);
								result = mapEntity;
							}
						}
					}
					else
					{
						if (state == null)
						{
							goto IL_01b2;
						}
						bool flag4 = state.TryGet<MapEntity>(ContextKey, out var value);
						bool flag5 = !flag4;
						result = null;
						if (!flag5)
						{
							result = value;
						}
					}
				}
				else
				{
					if ((object)FireMission._003CInstance_003Ek__BackingField == null)
					{
						goto IL_01b2;
					}
					bool flag6 = FireMission._003CInstance_003Ek__BackingField.TryGetMapEntity(Value, out var entity);
					bool flag7 = !flag6;
					result = null;
					if (!flag7)
					{
						result = entity;
					}
				}
			}
			else
			{
				if ((object)FireMission._003CInstance_003Ek__BackingField == null)
				{
					goto IL_01b2;
				}
				bool flag8 = FireMission._003CInstance_003Ek__BackingField.TryGetMapEntity(Value, out var entity2);
				bool flag9 = !flag8;
				result = null;
				if (!flag9)
				{
					result = entity2;
				}
			}
			return result;
			IL_01b2:
			return (MapEntity)(object)new NullReferenceException();
		}
	}

	public ConditionTypes ConditionType;

	public OperationTypes Operation;

	public ContextConditions ContextCondition;

	public EnittyConditions EnittyCondition;

	public EntityRoles RoleValue;

	public MapEntityStates StateValue;

	public LocationContextKeys DistanceLocationKey;

	public EntityIDLookup Entity1 = new EntityIDLookup
	{
		IdType = EntityIDLookup.IdTypes.Filter
	};

	public EntityIDLookup Entity2 = new EntityIDLookup
	{
		IdType = EntityIDLookup.IdTypes.Filter
	};

	public FilterConditions FilterCondition;

	public ComparisonConditions ComparisonCondition;

	public string StringValue;

	public int IntValue;

	public float FloatValue;

	public bool BoolValue;

	public bool Execute(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		//IL_002f: Expected O, but got I4
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_0229: Expected I4, but got O
		//IL_013d: Expected O, but got I8
		//IL_0157: Expected O, but got I8
		bool flag = ConditionType == ConditionTypes.Context;
		if (!flag)
		{
			object obj = ConditionType - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				List<MapEntity> list = filteredEntities;
				Condition condition = this;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						goto IL_0083;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 45 Invalid \"Jump target not found in method: 0x180542BB0\"");
					List<MapEntity> list2 = default(List<MapEntity>);
					list = list2;
					Condition condition2 = default(Condition);
					condition = condition2;
				}
				if (list == null)
				{
					List<MapEntity> list3 = new List<MapEntity>();
					list = list3;
				}
				if (condition.FilterCondition != FilterConditions.Count)
				{
					goto IL_0083;
				}
				if (list == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				OperationTypes operation = condition.Operation;
				if (condition.Operation > OperationTypes.GreaterThanOrEquals)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					object arg2 = default(object);
					string message = $"Operation Type: {arg} Not supported for Condition Type: {arg2}";
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					Exception ex2 = new Exception(message);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex2;
				}
				object obj3 = 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v4+542B94+v95 @ rax_v7 (Condition+OperationTypes)*4]");
				object obj4 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v139 @ rcx_v23 (should have been resolved before IL gen)");
			}
			return Resolve_Entity(state, filteredEntities);
		}
		return Resolve_Context(state, filteredEntities);
		IL_0083:
		return true;
	}

	public unsafe bool Resolve_Context(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		//IL_0008: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 62 Invalid \"Jump target not found in method: 0x1805433E9\"");
		return (byte)ContextCondition != 0;
	}

	public bool Resolve_Entity(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		//IL_003b: Expected O, but got I8
		//IL_0055: Expected O, but got I8
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 47 Invalid \"Jump target not found in method: 0x18054490D\"");
		MapEntity mapEntity = Entity1.Resolve(state, filteredEntities);
		if (mapEntity != null)
		{
			EnittyConditions enittyCondition = EnittyCondition;
			if (EnittyCondition <= EnittyConditions.DistanceFromTurret)
			{
				object obj = 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ r15_v2+544EB0+v73 @ rax_v5 (Condition+EnittyConditions)*4]");
				object obj2 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v90 @ rcx_v4 (should have been resolved before IL gen)");
			}
			return true;
		}
		return false;
	}

	public bool Resolve_Filter(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		//IL_011c: Expected I, but got O
		//IL_007d: Expected O, but got I8
		//IL_0097: Expected O, but got I8
		bool flag = filteredEntities != null;
		nint num = (nint)state;
		List<MapEntity> list = filteredEntities;
		if (!flag)
		{
			List<MapEntity> list2 = new List<MapEntity>();
			num = 0;
			list = list2;
		}
		if (FilterCondition != FilterConditions.Count)
		{
			return true;
		}
		OperationTypes operation = Operation;
		if (Operation <= OperationTypes.GreaterThanOrEquals)
		{
			object obj = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ r8_v3+545128+v93 @ rax_v17 (Condition+OperationTypes)*4]");
			object obj2 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v96 @ rcx_v16 (should have been resolved before IL gen)");
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		object arg2 = default(object);
		string message = $"Operation Type: {arg} Not supported for Condition Type: {arg2}";
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception(message);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public unsafe bool Resolve_Comparison(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Expected Ref, but got Unknown
		//IL_0235: Expected I, but got O
		//IL_0252: Expected O, but got I
		//IL_026f: Expected O, but got I
		//IL_028c: Expected O, but got I
		//IL_01d8: Expected O, but got I
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Expected O, but got Unknown
		//IL_0161: Expected O, but got I8
		//IL_017b: Expected O, but got I8
		//IL_011f: Expected F8, but got I4
		_ = 0;
		if (ComparisonCondition == ComparisonConditions.Distance)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 55 Invalid \"Jump target not found in method: 0x180542F95\"");
			MapEntity mapEntity = Entity1.Resolve(state, filteredEntities);
			if (mapEntity != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 83 Invalid \"Jump target not found in method: 0x180542F95\"");
				MapEntity mapEntity2 = Entity2.Resolve(state, filteredEntities);
				if (mapEntity2 != null)
				{
					_ = mapEntity2.Position;
					_ = mapEntity.Position;
					nint num = (nint)typeof(Math);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-50]");
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-40]");
					object obj = num2 - 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-4C]");
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-3C]");
					object obj2 = num3 - 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rax_v27 (MapEntity)+4C]");
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rax_v28 (MapEntity)+4C]");
					object obj3 = num4 - 0;
					object obj4 = obj2 * obj2;
					object obj5 = obj * obj;
					object obj6 = obj3 * obj3;
					object obj7 = obj4 + obj5;
					double d = (double)obj7 + (double)obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v355 @ rcx_v26 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 <= (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
						double num5 = 0.0;
					}
					else
					{
						double num5 = Math.Sqrt(d);
					}
					OperationTypes operation = Operation;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 179 Invalid \"Jump target not found in method: 0x180542F9B\"");
					object obj8 = 6442450944L;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v278 @ rcx_v27+5430DC+v442 @ rax_v31 (Condition+OperationTypes)*4]");
					object obj9 = 0 + 6442450944L;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v275 @ rax_v33 (should have been resolved before IL gen)");
					goto IL_0185;
				}
			}
		}
		else
		{
			if (ComparisonCondition != ComparisonConditions.Medals)
			{
				goto IL_0185;
			}
			MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 335 Invalid \"Jump target not found in method: 0x180542F95\"");
			OperationGraph operationGraph = missionManager._003CCurrentOperation_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 347 Invalid \"Jump target not found in method: 0x180542F95\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 358 Invalid \"Jump target not found in method: 0x180542F95\"");
			OperationState operation2 = ProgressionManager._003CInstance_003Ek__BackingField.GetOperation(operationGraph.OperationID);
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 373 Invalid \"Jump target not found in method: 0x180542F95\"");
			MissionManager missionManager2 = MissionManager._003CInstance_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 403 Invalid \"Jump target not found in method: 0x180542F95\"");
			MissionGraph missionGraph = missionManager2._003CCurrentMission_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 415 Invalid \"Jump target not found in method: 0x180542F95\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 426 Invalid \"Jump target not found in method: 0x180542F95\"");
			object obj10 = default(object);
			if (operation2.MissionStates.TryGetValue(missionGraph.MissionID, out *(OperationState.MissionState*)(obj10 - 88)))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-58]");
				object obj11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 454 Invalid \"Jump target not found in method: 0x180542F95\"");
				object obj12 = obj10 + 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180690EA0");
				MissionManager missionManager3 = MissionManager._003CInstance_003Ek__BackingField;
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 488 Invalid \"Jump target not found in method: 0x180542F95\"");
				MissionManager.MissionState currentMissionState = missionManager3.CurrentMissionState;
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 500 Invalid \"Jump target not found in method: 0x180542F95\"");
				object obj13 = obj10 - 96;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180690EA0");
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 520 Invalid \"Jump target not found in method: 0x18054303A\"");
				return (byte)Operation != 0;
			}
		}
		return false;
		IL_0185:
		return true;
	}

	public Condition()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
