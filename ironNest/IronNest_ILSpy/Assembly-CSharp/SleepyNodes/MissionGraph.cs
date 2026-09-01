using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Localisation;
using UnityEngine;

namespace SleepyNodes;

public class MissionGraph : StateGraph
{
	public enum MissionTypes
	{
		Tutorial,
		Campaign,
		Challange,
		Chill
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<Node> _003C_003E9__26_0;

		public static Converter<Node, State_Objective> _003C_003E9__26_1;

		public static Predicate<Node> _003C_003E9__27_0;

		public static Converter<Node, State_Objective> _003C_003E9__27_1;

		public static Predicate<Node> _003C_003E9__30_0;

		public static Converter<Node, State_Objective> _003C_003E9__30_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003COnNotification_003Eb__26_0(Node x)
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			bool flag = (object)x == null;
			Node node = null;
			Node node2;
			if (!flag)
			{
				nint num = (nint)typeof(State_Objective);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(State_Objective);
					node2 = (Node)1;
					if (flag2)
					{
						goto IL_00d3;
					}
				}
				node2 = null;
				goto IL_00d3;
			}
			goto IL_00f5;
			IL_00d3:
			bool flag3 = (object)node2 == null;
			node = null;
			if (!flag3)
			{
				node = x;
			}
			goto IL_00f5;
			IL_00f5:
			bool flag4 = (object)node == null;
			return !flag4;
		}

		internal State_Objective _003COnNotification_003Eb__26_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(State_Objective);
			if ((object)x == null)
			{
				return (State_Objective)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(State_Objective))
				{
					return (State_Objective)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			State_Objective result = default(State_Objective);
			return result;
		}

		internal bool _003CCheckEvents_003Eb__27_0(Node x)
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			bool flag = (object)x == null;
			Node node = null;
			Node node2;
			if (!flag)
			{
				nint num = (nint)typeof(State_Objective);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(State_Objective);
					node2 = (Node)1;
					if (flag2)
					{
						goto IL_00d3;
					}
				}
				node2 = null;
				goto IL_00d3;
			}
			goto IL_00f5;
			IL_00d3:
			bool flag3 = (object)node2 == null;
			node = null;
			if (!flag3)
			{
				node = x;
			}
			goto IL_00f5;
			IL_00f5:
			bool flag4 = (object)node == null;
			return !flag4;
		}

		internal State_Objective _003CCheckEvents_003Eb__27_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(State_Objective);
			if ((object)x == null)
			{
				return (State_Objective)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(State_Objective))
				{
					return (State_Objective)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			State_Objective result = default(State_Objective);
			return result;
		}

		internal bool _003CUpdate_003Eb__30_0(Node x)
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			bool flag = (object)x == null;
			Node node = null;
			Node node2;
			if (!flag)
			{
				nint num = (nint)typeof(State_Objective);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(State_Objective);
					node2 = (Node)1;
					if (flag2)
					{
						goto IL_00d3;
					}
				}
				node2 = null;
				goto IL_00d3;
			}
			goto IL_00f5;
			IL_00d3:
			bool flag3 = (object)node2 == null;
			node = null;
			if (!flag3)
			{
				node = x;
			}
			goto IL_00f5;
			IL_00f5:
			bool flag4 = (object)node == null;
			return !flag4;
		}

		internal State_Objective _003CUpdate_003Eb__30_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(State_Objective);
			if ((object)x == null)
			{
				return (State_Objective)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(State_Objective))
				{
					return (State_Objective)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			State_Objective result = default(State_Objective);
			return result;
		}
	}

	public string MissionID;

	public TextIdentifier MissionName;

	public TextIdentifier MissionDescription;

	public MissionTypes MissionType;

	public Sprite MapOverride;

	public Sprite MapTopographyOverride;

	public bool ShowCreditsAfterSummary;

	public AchievementType achievementForClearing;

	public AchievementType achievementForGolding;

	public bool ResetTurretPlacement = true;

	public List<MedalCategoryDefinition> Medals;

	public List<Zone> Zones;

	public MissionSceneReference SceneReference;

	public MissionPassiveGraph[] PassiveGraphs;

	public List<MutatorDefinition> mutators;

	public int RequisitionPoints;

	public int PowderCharges;

	public List<PunchcardDefinitionV2> RequiredPunchcards;

	public List<PunchcardDefinitionV2> UnlockedPunchcards;

	public override List<Type> NodeRestriction
	{
		get
		{
			List<Type> list = new List<Type>();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(StateNode));
			if (list != null)
			{
				list.Add(typeFromHandle);
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_Start));
				list.Add(typeFromHandle2);
				Type typeFromHandle3 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(EventNode));
				list.Add(typeFromHandle3);
				return list;
			}
			return (List<Type>)(object)new NullReferenceException();
		}
	}

	public override List<Type> NodeTypeExludes
	{
		get
		{
			List<Type> list = new List<Type>();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ObjectiveStateNode));
			if (list != null)
			{
				list.Add(typeFromHandle);
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ObjectiveResultNode));
				list.Add(typeFromHandle2);
				return list;
			}
			return (List<Type>)(object)new NullReferenceException();
		}
	}

	public void OnMissionLoaded()
	{
		if (MissionType == MissionTypes.Challange || MissionType == MissionTypes.Chill)
		{
			MissionStatsTracker.Instance.SetRequisitionPoints(RequisitionPoints);
			PowderChargeInventory._003CInstance_003Ek__BackingField.CurrentCharges = PowderCharges;
		}
		int requisitionPoints = MissionStatsTracker.Instance.RequisitionPoints;
		if (requisitionPoints < RequisitionPoints)
		{
			MissionStatsTracker.Instance.SetRequisitionPoints(RequisitionPoints);
		}
		PowderChargeInventory powderChargeInventory = PowderChargeInventory._003CInstance_003Ek__BackingField;
		if (powderChargeInventory._currentCharges < PowderCharges)
		{
			PowderChargeInventory._003CInstance_003Ek__BackingField.CurrentCharges = PowderCharges;
		}
		if (ResetTurretPlacement)
		{
			MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
			missionManager.TurretGrid.ResetAllToGrid();
		}
	}

	public void OnMissionUnloaded()
	{
	}

	public unsafe void OnNotification(string notifID)
	{
		//IL_00a1: Expected O, but got Ref
		//IL_00bf: Expected O, but got I
		//IL_0150: Expected I, but got O
		//IL_03f6: Expected I, but got O
		//IL_040c: Expected O, but got I
		//IL_0432: Expected O, but got I
		//IL_0449: Expected O, but got I
		//IL_01a1: Expected O, but got I4
		//IL_0487: Expected I, but got O
		//IL_049d: Expected O, but got I
		//IL_01d1: Expected O, but got I4
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_0263: Expected O, but got I4
		//IL_026c: Expected O, but got I4
		//IL_023d: Expected O, but got I4
		//IL_0245: Expected O, but got Ref
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Expected O, but got Unknown
		StateNode.NodeExecutionState currentState = CurrentState;
		if (CurrentState != null && (object)currentState.Node != null)
		{
			currentState.Node.OnNotification(CurrentState, notifID);
		}
		if (SideExecutionPaths != null)
		{
			Dictionary<string, StateNode.NodeExecutionState>.ValueCollection values = SideExecutionPaths.Values;
			if (values != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
				Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator enumerator = default(Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator);
				State_Objective state_Objective = default(State_Objective);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = (object)state_Objective == null;
					Dictionary<string, StateNode.NodeExecutionState> dictionary = (Dictionary<string, StateNode.NodeExecutionState>)(&enumerator);
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ stack_20_v8 (SleepyNodes.State_Objective)+18]");
						object obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ stack_20_v8 (SleepyNodes.State_Objective)+18]");
						if ((nint)0 != 0)
						{
							object obj2 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v530 @ r9_v11+1D8] (should have been resolved before IL gen)");
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				Predicate<Node> match = _003C_003Ec._003C_003E9__26_0;
				if (_003C_003Ec._003C_003E9__26_0 == null)
				{
					Predicate<Node> predicate = (_003C_003Ec._003C_003E9__26_0 = delegate(Node x)
					{
						//IL_0013: Expected I, but got O
						//IL_001b: Expected I, but got O
						//IL_002b: Expected O, but got I
						//IL_0067: Expected O, but got I
						//IL_008c: Expected O, but got I4
						bool flag5 = (object)x == null;
						Node node = null;
						Node node2;
						if (!flag5)
						{
							nint num5 = (nint)typeof(State_Objective);
							nint num6 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
							object obj6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
							if (num7 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj7 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
								bool flag6 = 0 == (nint)typeof(State_Objective);
								node2 = (Node)1;
								if (flag6)
								{
									goto IL_00d3;
								}
							}
							node2 = null;
							goto IL_00d3;
						}
						goto IL_00f5;
						IL_00d3:
						bool flag7 = (object)node2 == null;
						node = null;
						if (!flag7)
						{
							node = x;
						}
						goto IL_00f5;
						IL_00f5:
						bool flag8 = (object)node == null;
						return !flag8;
					});
					nint num = (nint)typeof(_003C_003Ec);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v613 @ rax_v62 (Il2CppClass<SleepyNodes.MissionGraph+<>c>)+B8]");
					Dictionary<string, StateNode.NodeExecutionState> dictionary = (Dictionary<string, StateNode.NodeExecutionState>)((nint)0 + (nint)8);
					match = predicate;
				}
				if (nodes != null)
				{
					List<Node> list = nodes.FindAll(match);
					nint num2 = (nint)typeof(_003C_003Ec);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v617 @ rax_v24 (Il2CppClass<SleepyNodes.MissionGraph+<>c>)+B8]");
					nint num3 = 0;
					bool flag2 = ((Dictionary<string, StateNode.NodeExecutionState>)num3)._buckets != null;
					Converter<Node, State_Objective> converter = (Converter<Node, State_Objective>)(object)((Dictionary<string, StateNode.NodeExecutionState>)num3)._buckets;
					if (!flag2)
					{
						Converter<Node, State_Objective> converter2 = (_003C_003Ec._003C_003E9__26_1 = delegate(Node x)
						{
							//IL_00ad: Expected I, but got O
							//IL_0012: Expected I, but got O
							//IL_0022: Expected O, but got I
							//IL_005e: Expected O, but got I
							nint num5 = (nint)typeof(State_Objective);
							if ((object)x == null)
							{
								return (State_Objective)x;
							}
							nint num6 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
							object obj6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
							if (num7 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj7 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
								if (0 == (nint)typeof(State_Objective))
								{
									return (State_Objective)x;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
							State_Objective result = default(State_Objective);
							return result;
						});
						nint num4 = (nint)typeof(_003C_003Ec);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v691 @ rax_v51 (Il2CppClass<SleepyNodes.MissionGraph+<>c>)+B8]");
						Dictionary<string, StateNode.NodeExecutionState> dictionary = (Dictionary<string, StateNode.NodeExecutionState>)((nint)0 + (nint)16);
						converter = converter2;
					}
					if (list != null)
					{
						List<State_Objective> list2 = list.ConvertAll(converter);
						bool flag3 = list2 == null;
						Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator enumerator2 = (Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator)0;
						if (!flag3)
						{
							bool flag4 = list2._size <= 0;
							enumerator2 = (Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator)0;
							if (!flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								List<State_Objective>.Enumerator enumerator3 = default(List<State_Objective>.Enumerator);
								while (enumerator3.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									if ((object)state_Objective != null)
									{
										state_Objective.SendNotification(notifID);
										continue;
									}
									throw new NullReferenceException();
								}
								enumerator3.Dispose();
								enumerator2 = (Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator)0;
								Dictionary<string, StateNode.NodeExecutionState> dictionary = (Dictionary<string, StateNode.NodeExecutionState>)(&enumerator3);
							}
						}
						MissionPassiveGraph[] passiveGraphs = PassiveGraphs;
						if (PassiveGraphs != null)
						{
							object obj3 = PassiveGraphs + 32;
							object obj4 = 0;
							object obj5 = 0;
							while (true)
							{
								if ((nint)obj5 < passiveGraphs.Length)
								{
									if ((UnityEngine.Object)obj3 != null)
									{
										if (obj3 == null)
										{
											break;
										}
										((MissionPassiveGraph)obj3).SendNotification(notifID);
									}
									obj4++;
									obj3 += 8;
									obj5 = obj4;
									continue;
								}
								EventData_Notification eventData_Notification = new EventData_Notification();
								if (eventData_Notification == null)
								{
									break;
								}
								eventData_Notification._003CNotificationID_003Ek__BackingField = notifID;
								CheckEvents(eventData_Notification);
								return;
							}
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void CheckEvents(EventNode.EventData evt)
	{
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Expected O, but got Unknown
		//IL_02cb: Expected O, but got I4
		//IL_02d4: Expected O, but got I4
		//IL_02ad: Expected O, but got Ref
		//IL_0128: Expected I, but got O
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Expected O, but got Unknown
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Expected O, but got Unknown
		//IL_01a7: Expected I, but got O
		//IL_0486: Expected O, but got I
		//IL_0486: Expected O, but got I
		List<EventNode> eventNodes = base.EventNodes;
		State_Objective state_Objective = default(State_Objective);
		State_Objective state_Objective2;
		if (eventNodes != null)
		{
			List<EventNode> eventNodes2 = base.EventNodes;
			if (eventNodes2._size > 0)
			{
				List<EventNode> eventNodes3 = base.EventNodes;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<EventNode>.Enumerator enumerator = default(List<EventNode>.Enumerator);
				object obj = default(object);
				object obj3 = default(object);
				string text = default(string);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = (object)state_Objective == null;
					state_Objective2 = state_Objective;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ stack_20_v12 (SleepyNodes.State_Objective)+49]");
						if ((nint)0 == 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ stack_20_v12 (SleepyNodes.State_Objective)+4B]");
							if ((nint)0 == 0)
							{
								continue;
							}
						}
						if ((object)state_Objective.Objective != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ stack_20_v12 (SleepyNodes.State_Objective)+4A]");
							if ((nint)0 != 0)
							{
								continue;
							}
						}
						nint num = (nint)state_Objective;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v906 @ r8_v31 (Il2CppClass<SleepyNodes.State_Objective>)+238] (should have been resolved before IL gen)");
						if (obj != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
							object obj2 = obj3;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1048 @ rdx_v45+1B8] (should have been resolved before IL gen)");
							string message = "[EVENT] Starting: " + text;
							Debug.Log(message);
							StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
							nint num2 = (nint)state_Objective;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1122 @ r8_v35 (Il2CppClass<SleepyNodes.State_Objective>)+248] (should have been resolved before IL gen)");
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
			}
		}
		Predicate<Node> match = _003C_003Ec._003C_003E9__27_0;
		if (_003C_003Ec._003C_003E9__27_0 == null)
		{
			match = (_003C_003Ec._003C_003E9__27_0 = delegate(Node x)
			{
				//IL_0013: Expected I, but got O
				//IL_001b: Expected I, but got O
				//IL_002b: Expected O, but got I
				//IL_0067: Expected O, but got I
				//IL_008c: Expected O, but got I4
				bool flag6 = (object)x == null;
				Node node = null;
				Node node2;
				if (!flag6)
				{
					nint num3 = (nint)typeof(State_Objective);
					nint num4 = (nint)x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
					if (num5 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
						object obj8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
						bool flag7 = 0 == (nint)typeof(State_Objective);
						node2 = (Node)1;
						if (flag7)
						{
							goto IL_00d3;
						}
					}
					node2 = null;
					goto IL_00d3;
				}
				goto IL_00f5;
				IL_00d3:
				bool flag8 = (object)node2 == null;
				node = null;
				if (!flag8)
				{
					node = x;
				}
				goto IL_00f5;
				IL_00f5:
				bool flag9 = (object)node == null;
				return !flag9;
			});
		}
		List<Node> list = nodes.FindAll(match);
		Converter<Node, State_Objective> converter = _003C_003Ec._003C_003E9__27_1;
		if (_003C_003Ec._003C_003E9__27_1 == null)
		{
			converter = (_003C_003Ec._003C_003E9__27_1 = delegate(Node x)
			{
				//IL_00ad: Expected I, but got O
				//IL_0012: Expected I, but got O
				//IL_0022: Expected O, but got I
				//IL_005e: Expected O, but got I
				nint num3 = (nint)typeof(State_Objective);
				if ((object)x == null)
				{
					return (State_Objective)x;
				}
				nint num4 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				if (num5 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
					if (0 == (nint)typeof(State_Objective))
					{
						return (State_Objective)x;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				State_Objective result = default(State_Objective);
				return result;
			});
		}
		List<State_Objective> list2 = list.ConvertAll(converter);
		bool flag2 = list2 == null;
		List<Node> list3 = list;
		if (!flag2)
		{
			bool flag3 = list2._size <= 0;
			list3 = list;
			if (!flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<State_Objective>.Enumerator enumerator2 = default(List<State_Objective>.Enumerator);
				while (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag4 = (object)state_Objective == null;
					state_Objective2 = state_Objective;
					if (!flag4)
					{
						state_Objective.CheckEvents(evt);
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator2.Dispose();
				list3 = (List<Node>)(&enumerator2);
			}
		}
		MissionPassiveGraph[] passiveGraphs = PassiveGraphs;
		object obj4 = PassiveGraphs + 32;
		object obj5 = 0;
		object obj6 = 0;
		state_Objective2 = (State_Objective)(object)list3;
		Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator enumerator3 = default(Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator);
		IntPtr intPtr = default(IntPtr);
		while (true)
		{
			if ((nint)obj6 < passiveGraphs.Length)
			{
				if ((nint)obj5 >= passiveGraphs.Length)
				{
					break;
				}
				bool flag5 = (UnityEngine.Object)obj4 != null;
				state_Objective2 = (State_Objective)obj4;
				if (flag5)
				{
					((MissionPassiveGraph)obj4).CheckEvents(evt);
					state_Objective2 = (State_Objective)obj4;
				}
				obj5++;
				obj4 += 8;
				obj6 = obj5;
				continue;
			}
			if (CurrentState != null)
			{
				StateNode.NodeExecutionState currentState = CurrentState;
				if (currentState.ListeningToEvents)
				{
					currentState.Node.OnEvent(evt, CurrentState);
				}
			}
			Dictionary<string, StateNode.NodeExecutionState>.ValueCollection values = SideExecutionPaths.Values;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
			while (true)
			{
				if (enumerator3.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (intPtr == (IntPtr)0)
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1015 @ stack_20_v8 (Il2CppMethodInfo)+30]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1015 @ stack_20_v8 (Il2CppMethodInfo)+18]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1015 @ stack_20_v8 (Il2CppMethodInfo)+18]");
						((StateNode)0).OnEvent(evt, (StateNode.NodeExecutionState)(nint)intPtr);
					}
					continue;
				}
				enumerator3.Dispose();
				return;
			}
			throw new NullReferenceException();
		}
		throw new IndexOutOfRangeException();
	}

	public virtual void ResetNodes()
	{
		//IL_004f: Expected I, but got O
		//IL_005d: Expected I, but got O
		//IL_006d: Expected O, but got I
		//IL_00a9: Expected O, but got I
		//IL_010a: Expected I, but got O
		//IL_0118: Expected I, but got O
		//IL_0128: Expected O, but got I
		//IL_0164: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<Node>.Enumerator enumerator = default(List<Node>.Enumerator);
		object obj = default(object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			bool flag = obj == null;
			num = 0;
			if (flag)
			{
				continue;
			}
			num = (nint)obj;
			nint num2 = (nint)typeof(StateNode);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rdx_v6 (Il2CppClass<SleepyNodes.StateNode>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppMethodInfo)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rdx_v6 (Il2CppClass<SleepyNodes.StateNode>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppMethodInfo)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rax_v15+FFFFFFF8+v228 @ rax_v9*8]");
				if (0 == (nint)typeof(StateNode))
				{
					if (obj == null)
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v82 @ r8_v2 (Il2CppMethodInfo)+1C8] (should have been resolved before IL gen)");
				}
			}
			if (obj == null)
			{
				continue;
			}
			num = (nint)obj;
			nint num4 = (nint)typeof(EventNode);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdx_v8 (Il2CppClass<SleepyNodes.EventNode>)+130]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppMethodInfo)+130]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdx_v8 (Il2CppClass<SleepyNodes.EventNode>)+130]");
			if (num5 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppMethodInfo)+C8]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rax_v12+FFFFFFF8+v89 @ rax_v11*8]");
				if (0 == (nint)typeof(EventNode) && obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v82 @ r8_v2 (Il2CppMethodInfo)+1C8] (should have been resolved before IL gen)");
				}
			}
		}
		enumerator.Dispose();
	}

	public override void Run()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_001e: Expected O, but got I4
		//IL_0027: Expected O, but got I4
		//IL_005f: Expected I, but got O
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		ResetNodes();
		base.Run();
		MissionPassiveGraph[] passiveGraphs = PassiveGraphs;
		object obj = PassiveGraphs + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < passiveGraphs.Length)
		{
			UnityEngine.Object obj4 = (UnityEngine.Object)obj;
			if ((UnityEngine.Object)obj != null)
			{
				nint num = (nint)obj4;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v202 @ rdx_v9 (Il2CppClass<UnityEngine.Object>)+1F8] (should have been resolved before IL gen)");
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}

	public override void Update()
	{
		//IL_0255: Expected O, but got I4
		//IL_02b6: Expected O, but got I4
		//IL_02cc: Expected O, but got I4
		//IL_02d6: Expected O, but got I4
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_0117: Expected O, but got I4
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Expected O, but got Unknown
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Expected O, but got Unknown
		//IL_0188: Expected I, but got O
		base.Update();
		Predicate<Node> match = _003C_003Ec._003C_003E9__30_0;
		if (_003C_003Ec._003C_003E9__30_0 == null)
		{
			Predicate<Node> predicate = (_003C_003Ec._003C_003E9__30_0 = delegate(Node x)
			{
				//IL_0013: Expected I, but got O
				//IL_001b: Expected I, but got O
				//IL_002b: Expected O, but got I
				//IL_0067: Expected O, but got I
				//IL_008c: Expected O, but got I4
				bool flag = (object)x == null;
				Node node = null;
				Node node2;
				if (!flag)
				{
					nint num2 = (nint)typeof(State_Objective);
					nint num3 = (nint)x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
					if (num4 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
						object obj7 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
						bool flag2 = 0 == (nint)typeof(State_Objective);
						node2 = (Node)1;
						if (flag2)
						{
							goto IL_00d3;
						}
					}
					node2 = null;
					goto IL_00d3;
				}
				goto IL_00f5;
				IL_00d3:
				bool flag3 = (object)node2 == null;
				node = null;
				if (!flag3)
				{
					node = x;
				}
				goto IL_00f5;
				IL_00f5:
				bool flag4 = (object)node == null;
				return !flag4;
			});
			object obj = 0;
			match = predicate;
		}
		if (nodes != null)
		{
			List<Node> list = nodes.FindAll(match);
			Converter<Node, State_Objective> converter = _003C_003Ec._003C_003E9__30_1;
			if (_003C_003Ec._003C_003E9__30_1 == null)
			{
				Converter<Node, State_Objective> converter2 = (_003C_003Ec._003C_003E9__30_1 = delegate(Node x)
				{
					//IL_00ad: Expected I, but got O
					//IL_0012: Expected I, but got O
					//IL_0022: Expected O, but got I
					//IL_005e: Expected O, but got I
					nint num2 = (nint)typeof(State_Objective);
					if ((object)x == null)
					{
						return (State_Objective)x;
					}
					nint num3 = (nint)x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
					if (num4 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
						object obj7 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
						if (0 == (nint)typeof(State_Objective))
						{
							return (State_Objective)x;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					State_Objective result = default(State_Objective);
					return result;
				});
				object obj = 0;
				converter = converter2;
			}
			if (list != null)
			{
				List<State_Objective> list2 = list.ConvertAll(converter);
				object obj2;
				if (list2 != null && list2._size > 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<State_Objective>.Enumerator enumerator = default(List<State_Objective>.Enumerator);
					State_Objective state_Objective = default(State_Objective);
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if ((object)state_Objective != null)
						{
							state_Objective.UpdateObjectives();
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					List<State_Objective>.Enumerator enumerator3 = default(List<State_Objective>.Enumerator);
					List<State_Objective>.Enumerator enumerator2 = enumerator3;
					obj2 = 0;
				}
				else
				{
					List<State_Objective>.Enumerator enumerator2 = (List<State_Objective>.Enumerator)0;
					obj2 = 0;
				}
				MissionPassiveGraph[] passiveGraphs = PassiveGraphs;
				if (PassiveGraphs != null)
				{
					object obj3 = PassiveGraphs + 32;
					object obj4 = obj2;
					while (true)
					{
						if ((nint)obj4 < passiveGraphs.Length)
						{
							UnityEngine.Object obj5 = (UnityEngine.Object)obj3;
							if ((UnityEngine.Object)obj3 != null)
							{
								if (obj3 == null)
								{
									break;
								}
								nint num = (nint)obj5;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v649 @ rdx_v15 (Il2CppClass<UnityEngine.Object>)+208] (should have been resolved before IL gen)");
							}
							obj2++;
							obj3 += 8;
							obj4 = obj2;
							continue;
						}
						EventData_Timer eventData_Timer = new EventData_Timer();
						float time = Time.time;
						if (eventData_Timer == null)
						{
							break;
						}
						eventData_Timer.Time = time;
						CheckEvents(eventData_Timer);
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public MissionGraph()
	{
		List<MedalCategoryDefinition> medals = new List<MedalCategoryDefinition>();
		Medals = medals;
		Zones = new List<Zone>();
		mutators = new List<MutatorDefinition>();
		RequisitionPoints = 100;
		PowderCharges = 100;
		RequiredPunchcards = new List<PunchcardDefinitionV2>();
		UnlockedPunchcards = new List<PunchcardDefinitionV2>();
		base._002Ector();
	}
}
