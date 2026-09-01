using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using SleepyNodes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MapCard : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public MissionGraph mission;

		public OperationState state;

		internal bool _003CInit_003Eb__0(MissionNode x)
		{
			//IL_0091: Expected I4, but got O
			if ((object)x != null)
			{
				MissionGraph missionGraph = x.Mission;
				string text = (((object)x.Mission == null) ? null : missionGraph.MissionID);
				MissionGraph missionGraph2 = mission;
				if ((object)mission != null)
				{
					return text == missionGraph2.MissionID;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CInit_003Eb__1(MissionNode previousMission)
		{
			//IL_00e0: Expected I4, but got O
			if (state != null)
			{
				OperationState operationState = state;
				if ((object)previousMission != null)
				{
					MissionGraph missionGraph = previousMission.Mission;
					bool flag = (object)previousMission.Mission == null;
					string key = null;
					if (!flag)
					{
						key = missionGraph.MissionID;
					}
					if (operationState.MissionStates != null)
					{
						if (!operationState.MissionStates.TryGetValue(key, out var value))
						{
							goto IL_00cc;
						}
						if (value != null)
						{
							return value.Completed;
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_00cc;
			IL_00cc:
			return false;
		}

		internal bool _003CInit_003Eb__2(MissionNode previousMission)
		{
			//IL_00e0: Expected I4, but got O
			if (state != null)
			{
				OperationState operationState = state;
				if ((object)previousMission != null)
				{
					MissionGraph missionGraph = previousMission.Mission;
					bool flag = (object)previousMission.Mission == null;
					string key = null;
					if (!flag)
					{
						key = missionGraph.MissionID;
					}
					if (operationState.MissionStates != null)
					{
						if (!operationState.MissionStates.TryGetValue(key, out var value))
						{
							goto IL_00cc;
						}
						if (value != null)
						{
							return value.Completed;
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_00cc;
			IL_00cc:
			return false;
		}
	}

	public TMP_Text Text_Title;

	public TMP_Text Text_Description;

	public OperationGraph Campaign;

	public MissionGraph Mission;

	public List<MissionCardMedalSlot> Medals;

	public string TargetTag;

	public UnityEvent OnState_NotUnlocked;

	public UnityEvent OnState_Unlocked_NotComplete;

	public UnityEvent OnState_Unlocked_Complete;

	public unsafe void Init(MissionGraph mission)
	{
		//IL_0152: Expected O, but got I4
		//IL_015b: Expected O, but got I4
		//IL_0164: Expected O, but got I4
		//IL_016f: Expected I, but got O
		//IL_01a2: Expected O, but got I4
		//IL_01ab: Expected O, but got I4
		//IL_01b4: Expected O, but got I4
		//IL_01bf: Expected I, but got O
		//IL_01ed: Expected O, but got I4
		//IL_01f6: Expected O, but got I4
		//IL_01ff: Expected O, but got I4
		//IL_020a: Expected I, but got O
		//IL_02c1: Expected O, but got I4
		//IL_02ca: Expected O, but got I4
		//IL_0273: Expected O, but got I4
		//IL_027c: Expected O, but got I4
		//IL_0281: Expected I, but got O
		//IL_0311: Expected O, but got I4
		//IL_0316: Expected I, but got O
		//IL_0299: Expected I, but got O
		//IL_032e: Expected I, but got O
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Expected O, but got Unknown
		//IL_03f1: Expected I, but got O
		//IL_043e: Expected O, but got I
		//IL_0787: Expected O, but got I4
		//IL_0473: Expected O, but got I
		_003C_003Ec__DisplayClass9_0 CS_0024_003C_003E8__locals18 = new _003C_003Ec__DisplayClass9_0();
		CS_0024_003C_003E8__locals18.mission = mission;
		Mission = CS_0024_003C_003E8__locals18.mission;
		OperationGraph campaign = Campaign;
		OperationState operation = ProgressionManager._003CInstance_003Ek__BackingField.GetOperation(campaign.OperationID);
		CS_0024_003C_003E8__locals18.state = operation;
		if (CS_0024_003C_003E8__locals18.state != null)
		{
			OperationState state = CS_0024_003C_003E8__locals18.state;
			MissionGraph mission2 = Mission;
			if (state.MissionStates.TryGetValue(mission2.MissionID, out var _))
			{
				goto IL_00f4;
			}
		}
		OperationState.MissionState missionState = new OperationState.MissionState();
		MissionGraph mission3 = Mission;
		missionState.MissionID = mission3.MissionID;
		goto IL_00f4;
		IL_085c:
		MissionGraph mission4 = CS_0024_003C_003E8__locals18.mission;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		object arg2 = default(object);
		string message = $"[MapCard Init] '{mission4.MissionID}' = Unlocked: {arg} | Completed: {arg2}";
		Debug.Log(message);
		Dictionary<string, int> dictionary;
		((dictionary == null) ? OnState_NotUnlocked : (missionState.Completed ? OnState_Unlocked_Complete : OnState_Unlocked_NotComplete))?.Invoke();
		List<MissionCardMedalSlot> medals = Medals;
		string text = (string)(object)dictionary;
		string text2 = null;
		List<MissionNode> list = default(List<MissionNode>);
		Component component = default(Component);
		string text4 = default(string);
		for (string text3 = null; (nint)text3 < medals._size; text2++, medals = Medals, text3 = text2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			MissionGraph mission5 = CS_0024_003C_003E8__locals18.mission;
			List<MedalCategoryDefinition> medals2 = mission5.Medals;
			if (medals2._size > (nint)text2)
			{
				MissionGraph mission6 = CS_0024_003C_003E8__locals18.mission;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if ((UnityEngine.Object)(object)list != null)
				{
					GameObject gameObject = component.gameObject;
					gameObject.SetActive(value: true);
					MissionGraph mission7 = CS_0024_003C_003E8__locals18.mission;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					MissionGraph mission8 = CS_0024_003C_003E8__locals18.mission;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806910A0");
					if ((nint)text4 >= 0)
					{
						bool flag = (nint)text4 > 3;
						string text5 = (string)3;
						if (!flag)
						{
							text5 = text4;
						}
					}
					else
					{
						string text5 = null;
					}
					_ = 1;
					((MissionCardMedalSlot)component).Refresh();
					text = null;
					continue;
				}
			}
			GameObject gameObject2 = component.gameObject;
			gameObject2.SetActive(value: false);
		}
		return;
		IL_086e:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		OperationState.MissionState value2 = null;
		UnityEngine.Object obj = default(UnityEngine.Object);
		list = (List<MissionNode>)(object)obj;
		object obj3 = default(object);
		object obj2 = obj3;
		List<MissionNode>.Enumerator enumerator2 = default(List<MissionNode>.Enumerator);
		List<MissionNode>.Enumerator enumerator = enumerator2;
		string text6;
		dictionary = (Dictionary<string, int>)(object)text6;
		nint num = 0;
		List<MissionNode>.Enumerator enumerator3 = default(List<MissionNode>.Enumerator);
		MissionNode missionNode = default(MissionNode);
		List<MissionNode>.Enumerator enumerator4 = default(List<MissionNode>.Enumerator);
		List<MissionNode> list2 = default(List<MissionNode>);
		UnityEngine.Object obj4 = default(UnityEngine.Object);
		object obj6 = default(object);
		List<MissionNode>.Enumerator enumerator5 = default(List<MissionNode>.Enumerator);
		nint num2;
		while (enumerator3.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if ((object)missionNode != null)
			{
				bool flag2 = missionNode.NextUnlockCondition != MissionNode.NextUnlockConditions.Exclusive;
				num = 0;
				if (flag2)
				{
					continue;
				}
				List<MissionNode> unlocks = missionNode.GetUnlocks();
				if (unlocks != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					num = 0;
					while (enumerator4.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag3 = (UnityEngine.Object)(object)list2 != obj4;
						list = list2;
						num = unchecked((nint)null);
						if (!flag3)
						{
							continue;
						}
						OperationState state2 = CS_0024_003C_003E8__locals18.state;
						if (CS_0024_003C_003E8__locals18.state != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1230 @ stack_-D0_v12 (System.Collections.Generic.List`1<SleepyNodes.MissionNode>)+40]");
							object obj5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1230 @ stack_-D0_v12 (System.Collections.Generic.List`1<SleepyNodes.MissionNode>)+40]");
							string key;
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1452 @ rax_v99+58]");
								key = (string)0;
							}
							else
							{
								key = null;
							}
							bool flag4 = state2.MissionStates.TryGetValue(key, out value2);
							bool flag5 = !flag4;
							list = list2;
							num2 = 0;
							num = (nint)(&value2);
							if (!flag5)
							{
								bool flag6 = !value2.Completed;
								list = list2;
								num2 = 0;
								num = (nint)(&value2);
								if (!flag6)
								{
									list = list2;
									text = null;
									dictionary = null;
									num2 = 0;
									num = (nint)(&value2);
								}
							}
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator4.Dispose();
					obj2 = obj6;
					enumerator = enumerator5;
					continue;
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}
		enumerator3.Dispose();
		obj = (UnityEngine.Object)(object)list;
		goto IL_085c;
		IL_00f4:
		List<MissionNode> missions = Campaign.Missions;
		Func<MissionNode, bool> func = delegate(MissionNode x)
		{
			//IL_0091: Expected I4, but got O
			if ((object)x != null)
			{
				MissionGraph mission9 = x.Mission;
				string text7 = (((object)x.Mission == null) ? null : mission9.MissionID);
				MissionGraph mission10 = CS_0024_003C_003E8__locals18.mission;
				if ((object)CS_0024_003C_003E8__locals18.mission != null)
				{
					return text7 == mission10.MissionID;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		};
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
		bool flag7 = obj4 != null;
		bool flag8 = !flag7;
		obj2 = 0;
		enumerator = (List<MissionNode>.Enumerator)0;
		dictionary = (Dictionary<string, int>)1;
		num2 = 0;
		num = unchecked((nint)null);
		if (!flag8)
		{
			List<MissionNode> unlockedBy = ((MissionNode)obj4).GetUnlockedBy();
			bool flag9 = unlockedBy == null;
			obj2 = 0;
			enumerator = (List<MissionNode>.Enumerator)0;
			dictionary = (Dictionary<string, int>)1;
			num2 = 0;
			num = unchecked((nint)null);
			if (!flag9)
			{
				bool flag10 = unlockedBy._size <= 0;
				obj2 = 0;
				enumerator = (List<MissionNode>.Enumerator)0;
				dictionary = (Dictionary<string, int>)1;
				num2 = 0;
				num = unchecked((nint)null);
				if (!flag10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ stack_-E0_v3 (UnityEngine.Object)+48]");
					if ((nint)0 == 0)
					{
						Func<MissionNode, bool> predicate = delegate(MissionNode previousMission)
						{
							//IL_00e0: Expected I4, but got O
							if (CS_0024_003C_003E8__locals18.state != null)
							{
								OperationState state3 = CS_0024_003C_003E8__locals18.state;
								if ((object)previousMission != null)
								{
									MissionGraph mission9 = previousMission.Mission;
									bool flag14 = (object)previousMission.Mission == null;
									string key2 = null;
									if (!flag14)
									{
										key2 = mission9.MissionID;
									}
									if (state3.MissionStates != null)
									{
										if (!state3.MissionStates.TryGetValue(key2, out var value3))
										{
											goto IL_00cc;
										}
										if (value3 != null)
										{
											return value3.Completed;
										}
									}
								}
								NullReferenceException ex = new NullReferenceException();
								return (byte)(int)ex != 0;
							}
							goto IL_00cc;
							IL_00cc:
							return false;
						};
						bool flag11 = Enumerable.Any(unlockedBy, predicate);
						text = (string)1;
						text6 = (string)1;
						num2 = unchecked((nint)null);
						if (flag11)
						{
							goto IL_086e;
						}
						text6 = null;
						num2 = unchecked((nint)null);
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ stack_-E0_v3 (UnityEngine.Object)+48]");
						bool flag12 = (nint)0 != 1;
						text = (string)1;
						text6 = (string)1;
						num2 = 0;
						if (flag12)
						{
							goto IL_086e;
						}
						Func<MissionNode, bool> predicate2 = delegate(MissionNode previousMission)
						{
							//IL_00e0: Expected I4, but got O
							if (CS_0024_003C_003E8__locals18.state != null)
							{
								OperationState state3 = CS_0024_003C_003E8__locals18.state;
								if ((object)previousMission != null)
								{
									MissionGraph mission9 = previousMission.Mission;
									bool flag14 = (object)previousMission.Mission == null;
									string key2 = null;
									if (!flag14)
									{
										key2 = mission9.MissionID;
									}
									if (state3.MissionStates != null)
									{
										if (!state3.MissionStates.TryGetValue(key2, out var value3))
										{
											goto IL_00cc;
										}
										if (value3 != null)
										{
											return value3.Completed;
										}
									}
								}
								NullReferenceException ex = new NullReferenceException();
								return (byte)(int)ex != 0;
							}
							goto IL_00cc;
							IL_00cc:
							return false;
						};
						bool flag13 = Enumerable.All(unlockedBy, predicate2);
						text6 = (string)1;
						num2 = unchecked((nint)null);
						if (!flag13)
						{
							text6 = null;
							num2 = unchecked((nint)null);
						}
					}
					text = text6;
					goto IL_086e;
				}
			}
		}
		goto IL_085c;
	}

	public void PopulateMissionInfo()
	{
		string text3;
		string text4;
		if (Mission != null)
		{
			if (!string.IsNullOrEmpty(TargetTag))
			{
				GameObject gameObject = GameObject.FindWithTag(TargetTag);
				if (gameObject != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					UnityEngine.Object obj = default(UnityEngine.Object);
					if (obj != null)
					{
						MissionGraph mission = Mission;
						string missionName = mission.MissionName.Get();
						MissionGraph mission2 = Mission;
						string missionDescription = mission2.MissionDescription.Get();
						MissionGraph mission3 = Mission;
						MapCard sourceCard = default(MapCard);
						((MissionInfoDisplay)obj).Populate(missionName, missionDescription, mission3.MapTopographyOverride, sourceCard);
						MissionGraph mission4 = Mission;
						string text = gameObject.name;
						string message = "[MapCard] PopulateMissionInfo → sent '" + mission4.MissionID + "' data to '" + text + "'.";
						Debug.Log(message);
					}
					else
					{
						string text2 = gameObject.name;
						string message2 = "[MapCard] PopulateMissionInfo: GameObject '" + text2 + "' (tag '" + TargetTag + "') does not have a MissionInfoDisplay component.";
						Debug.LogWarning(message2, this);
					}
				}
				else
				{
					string message3 = "[MapCard] PopulateMissionInfo: no active GameObject found with tag '" + TargetTag + "'. Ensure the target scene is loaded and the tag is correct.";
					Debug.LogWarning(message3, this);
				}
				return;
			}
			text3 = base.name;
			text4 = "' but TargetTag is empty. Set a tag in the Inspector.";
		}
		else
		{
			text3 = base.name;
			text4 = "' but Mission is null. Assign a MissionGraph in the Inspector.";
		}
		string message4 = "[MapCard] PopulateMissionInfo called on '" + text3 + text4;
		Debug.LogWarning(message4, this);
	}

	public void ActivateMission()
	{
		Debug.Log("[MapCard] Attempting Activate");
		if (Campaign != null)
		{
			if (Mission != null)
			{
				List<MissionNode> missions = Campaign.Missions;
				Func<MissionNode, bool> predicate = delegate(MissionNode x)
				{
					//IL_00a3: Expected I4, but got O
					if ((object)x != null)
					{
						MissionGraph mission = x.Mission;
						if ((object)x.Mission != null)
						{
							MissionGraph mission2 = Mission;
							if ((object)Mission != null)
							{
								return mission.MissionID == mission2.MissionID;
							}
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				};
				if (Enumerable.Any(missions, predicate))
				{
					MissionManager._003CInstance_003Ek__BackingField.StartOperation(Campaign, Mission);
				}
				else
				{
					Debug.Log("[MapCard] Unable to activate as Mission was not found in the Campaign");
				}
			}
			else
			{
				Debug.Log("[MapCard] Unable to activate as Mission is not set");
			}
		}
		else
		{
			Debug.Log("[MapCard] Unable to activate as Campaign is not set");
		}
	}

	public MapCard()
	{
		List<MissionCardMedalSlot> medals = new List<MissionCardMedalSlot>();
		Medals = medals;
		TargetTag = "MissionInfoDisplay";
		base._002Ector();
	}

	private bool _003CActivateMission_003Eb__11_0(MissionNode x)
	{
		//IL_00a3: Expected I4, but got O
		if ((object)x != null)
		{
			MissionGraph mission = x.Mission;
			if ((object)x.Mission != null)
			{
				MissionGraph mission2 = Mission;
				if ((object)Mission != null)
				{
					return mission.MissionID == mission2.MissionID;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
