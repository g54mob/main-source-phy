using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;

public class MissionAdvanceReaction : MonoBehaviour
{
	public enum Trigger
	{
		OnMissionChanging,
		OnMissionChanged,
		OnPhaseChanged
	}

	public enum PhaseFilter
	{
		Any,
		EnterMainMenu,
		EnterBrowsingMap,
		EnterMissionActive
	}

	public enum ExitPhaseFilter
	{
		Any,
		ExitMainMenu,
		ExitBrowsingMap,
		ExitMissionActive
	}

	public enum TargetSelection
	{
		SelfOnly,
		ChildrenOnly,
		SelfAndChildren
	}

	public enum ActionType
	{
		None,
		Disable,
		Destroy
	}

	[Serializable]
	public class GameObjectEvent : UnityEvent<GameObject>
	{
	}

	private sealed class _003CRetrySubscribeRoutine_003Ed__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MissionAdvanceReaction _003C_003E4__this;

		private int _003Cattempts_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRetrySubscribeRoutine_003Ed__33(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0056: Expected I4, but got I8
			//IL_0241: Expected I4, but got O
			//IL_0073: Expected O, but got I4
			//IL_0163: Expected O, but got I4
			MissionAdvanceReaction missionAdvanceReaction = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003Cattempts_003E5__2 = _003C_003E1__state;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_022d;
				}
				_003C_003E1__state = -1;
				bool flag = MissionManager._003CInstance_003Ek__BackingField == null;
				object obj = 0;
				if (!flag)
				{
					if ((object)_003C_003E4__this != null)
					{
						_003C_003E4__this.Subscribe();
						obj = 0;
						goto IL_0168;
					}
					goto IL_0233;
				}
			}
			if ((object)_003C_003E4__this != null)
			{
				if (!missionAdvanceReaction.subscribed)
				{
					if (_003Cattempts_003E5__2 < 10)
					{
						int num = _003Cattempts_003E5__2 + 1;
						_003Cattempts_003E5__2 = num;
						WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 1;
						return true;
					}
					goto IL_0168;
				}
				goto IL_021e;
			}
			goto IL_0233;
			IL_0233:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_022d:
			return false;
			IL_021e:
			missionAdvanceReaction.subscribeRetryRoutine = null;
			goto IL_022d;
			IL_0168:
			if (!missionAdvanceReaction.subscribed && missionAdvanceReaction.verbose)
			{
				int instanceID = _003C_003E4__this.GetInstanceID();
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				string name = _003C_003E4__this.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				string message = $"[{arg}] MissionAdvanceReaction '{name}': Failed to find MissionManager after {arg2} attempts.";
				Debug.LogWarning(message, _003C_003E4__this);
			}
			goto IL_021e;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private Trigger trigger;

	private PhaseFilter phaseFilter = PhaseFilter.EnterBrowsingMap;

	private ExitPhaseFilter exitPhaseFilter;

	private bool treatReturnToMainMenuAsMissionChange = true;

	private GameObject root;

	private TargetSelection targetSelection;

	private bool recursiveChildren = true;

	private bool includeInactiveChildren;

	private bool skipChildrenWithReaction = true;

	private ActionType action = ActionType.Disable;

	private bool runOnce = true;

	private bool ignoreFirstTrigger;

	private UnityEvent onTriggered;

	private GameObjectEvent onBeforeEach;

	private GameObjectEvent onAfterEach;

	private UnityEvent onCompleted;

	private bool verbose;

	private bool subscribed;

	private bool hasRun;

	private bool hasIgnoredFirst;

	private Coroutine subscribeRetryRoutine;

	private const int MainMenuSentinelIndex = -1;

	private void Reset()
	{
		phaseFilter = PhaseFilter.EnterBrowsingMap;
		trigger = Trigger.OnMissionChanging;
		targetSelection = TargetSelection.SelfOnly;
		verbose = false;
		treatReturnToMainMenuAsMissionChange = true;
		recursiveChildren = true;
		skipChildrenWithReaction = true;
		action = ActionType.Disable;
		runOnce = true;
	}

	private void Awake()
	{
		if (root == null)
		{
			GameObject gameObject = base.gameObject;
			root = gameObject;
		}
	}

	private void OnEnable()
	{
		if (subscribed)
		{
			return;
		}
		if (MissionManager._003CInstance_003Ek__BackingField == null)
		{
			if (subscribeRetryRoutine == null)
			{
				_003CRetrySubscribeRoutine_003Ed__33 obj = new _003CRetrySubscribeRoutine_003Ed__33(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				Coroutine coroutine = StartCoroutine(obj);
				subscribeRetryRoutine = coroutine;
			}
		}
		else
		{
			Subscribe();
		}
	}

	private void OnDisable()
	{
		if (subscribeRetryRoutine != null)
		{
			StopCoroutine(subscribeRetryRoutine);
			subscribeRetryRoutine = null;
		}
		Unsubscribe();
	}

	private void TrySubscribeOrRetry()
	{
		if (subscribed)
		{
			return;
		}
		if (MissionManager._003CInstance_003Ek__BackingField == null)
		{
			if (subscribeRetryRoutine == null)
			{
				_003CRetrySubscribeRoutine_003Ed__33 obj = new _003CRetrySubscribeRoutine_003Ed__33(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				Coroutine coroutine = StartCoroutine(obj);
				subscribeRetryRoutine = coroutine;
			}
		}
		else
		{
			Subscribe();
		}
	}

	private IEnumerator RetrySubscribeRoutine()
	{
		_003CRetrySubscribeRoutine_003Ed__33 obj = new _003CRetrySubscribeRoutine_003Ed__33(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void StopRetry()
	{
		if (subscribeRetryRoutine != null)
		{
			StopCoroutine(subscribeRetryRoutine);
			subscribeRetryRoutine = null;
		}
	}

	private void Subscribe()
	{
		//IL_040f: Expected O, but got I4
		//IL_017c: Expected O, but got I4
		//IL_07b3: Expected O, but got I
		//IL_07bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Expected O, but got Unknown
		//IL_0094: Expected O, but got I4
		//IL_0497: Expected O, but got I4
		//IL_0676: Expected O, but got I
		//IL_0457: Expected I, but got O
		//IL_0460: Expected O, but got I4
		//IL_0465: Expected I, but got O
		//IL_061b: Expected O, but got I
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Expected O, but got Unknown
		//IL_021a: Expected O, but got I4
		//IL_01be: Expected I, but got O
		//IL_01cc: Expected I, but got O
		//IL_01d5: Expected O, but got I4
		//IL_01da: Expected I, but got O
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		//IL_00ce: Expected I, but got O
		//IL_00dc: Expected I, but got O
		//IL_00e5: Expected O, but got I4
		//IL_00ea: Expected I, but got O
		//IL_050b: Expected I4, but got O
		//IL_0140: Expected O, but got I4
		//IL_0559: Expected I4, but got O
		//IL_0566: Expected I4, but got O
		//IL_073a: Expected O, but got I
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Expected O, but got Unknown
		//IL_06d1: Expected O, but got I
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Expected O, but got Unknown
		//IL_0375: Expected I, but got O
		//IL_037d: Expected I, but got O
		//IL_0386: Expected O, but got I4
		//IL_038b: Expected I, but got O
		//IL_03d3: Expected O, but got I4
		//IL_02c0: Expected I, but got O
		//IL_02c8: Expected I, but got O
		//IL_02d1: Expected O, but got I4
		//IL_02d6: Expected I, but got O
		//IL_031e: Expected O, but got I4
		if (subscribed)
		{
			return;
		}
		UnityEngine.Object obj = MissionManager._003CInstance_003Ek__BackingField;
		if (!(MissionManager._003CInstance_003Ek__BackingField != null))
		{
			return;
		}
		nint num2;
		Delegate obj7 = default(Delegate);
		object obj14;
		Delegate obj15;
		Delegate obj11;
		nint num4;
		if (trigger != Trigger.OnPhaseChanged)
		{
			if (trigger != Trigger.OnMissionChanging)
			{
				Action<MissionGraph, MissionGraph> b = HandleMissionEvent;
				bool flag = (object)MissionManager._003CInstance_003Ek__BackingField == null;
				object obj2 = 0;
				nint num = 0;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v859 @ rdi_v6 (UnityEngine.Object)+80]");
					Delegate obj3 = (Delegate)0;
					while (true)
					{
						Delegate obj4 = Delegate.Combine(obj3, b);
						bool flag2 = (object)obj4 == null;
						Delegate obj5 = obj4;
						if (!flag2)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag3 = (object)obj5 == null;
							num2 = (nint)obj4;
							nint num3 = (nint)typeof(Action<MissionGraph, MissionGraph>);
							obj2 = 0;
							num = unchecked((nint)null);
							if (flag3)
							{
								break;
							}
						}
						object obj6 = obj + 128;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
						bool flag4 = (object)obj7 != obj3;
						obj3 = obj7;
						if (flag4)
						{
							continue;
						}
						goto IL_0137;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					goto IL_06e4;
				}
			}
			else
			{
				Action<MissionGraph, MissionGraph> b2 = HandleMissionEvent;
				bool flag5 = (object)MissionManager._003CInstance_003Ek__BackingField == null;
				object obj2 = 0;
				nint num = 0;
				if (!flag5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v859 @ rdi_v6 (UnityEngine.Object)+78]");
					Delegate obj8 = (Delegate)0;
					Delegate obj13 = default(Delegate);
					while (true)
					{
						Delegate obj9 = Delegate.Combine(obj8, b2);
						bool flag6 = (object)obj9 == null;
						Delegate obj10 = obj9;
						if (!flag6)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag7 = (object)obj10 == null;
							obj11 = (Delegate)(object)obj;
							num4 = (nint)obj9;
							nint num3 = (nint)typeof(Action<MissionGraph, MissionGraph>);
							obj2 = 0;
							num = unchecked((nint)null);
							if (flag7)
							{
								break;
							}
						}
						object obj12 = obj + 120;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
						bool flag8 = (object)obj13 != obj8;
						obj14 = 0;
						obj15 = obj8;
						obj8 = obj13;
						if (flag8)
						{
							continue;
						}
						goto IL_0238;
					}
					goto IL_067b;
				}
			}
		}
		else
		{
			Action<MissionManager.GamePhase, MissionManager.GamePhase> b3 = HandlePhaseChanged;
			bool flag9 = (object)MissionManager._003CInstance_003Ek__BackingField == null;
			object obj2 = 0;
			nint num = 0;
			if (!flag9)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v859 @ rdi_v6 (UnityEngine.Object)+28]");
				Delegate obj16 = (Delegate)0;
				object obj17 = MissionManager._003CInstance_003Ek__BackingField + 40;
				Delegate obj20 = default(Delegate);
				while (true)
				{
					Delegate obj18 = Delegate.Combine(obj16, b3);
					bool flag10 = (object)obj18 == null;
					Delegate obj19 = obj18;
					if (!flag10)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						bool flag11 = (object)obj19 == null;
						obj11 = obj18;
						num4 = (nint)typeof(Action<MissionManager.GamePhase, MissionManager.GamePhase>);
						obj2 = 0;
						num = unchecked((nint)null);
						if (flag11)
						{
							break;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
					bool flag12 = (object)obj20 != obj16;
					obj14 = 0;
					obj15 = obj16;
					obj16 = obj20;
					if (flag12)
					{
						continue;
					}
					goto IL_04b5;
				}
				goto IL_07c7;
			}
		}
		throw new NullReferenceException();
		IL_067b:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_07c7;
		IL_0137:
		obj14 = 0;
		obj15 = obj7;
		goto IL_0238;
		IL_0315:
		obj14 = 0;
		Delegate obj21 = default(Delegate);
		obj15 = obj21;
		goto IL_04b5;
		IL_06e4:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_074d;
		IL_03ca:
		obj14 = 0;
		Delegate obj22 = default(Delegate);
		obj15 = obj22;
		goto IL_04b5;
		IL_074d:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		obj11 = (Delegate)(object)obj;
		num4 = num2;
		goto IL_067b;
		IL_0238:
		if (!treatReturnToMainMenuAsMissionChange)
		{
			goto IL_04b5;
		}
		if (trigger != Trigger.OnMissionChanging)
		{
			Action<string> b4 = HandleMainMenuLoaded;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v859 @ rdi_v6 (UnityEngine.Object)+90]");
			Delegate obj23 = (Delegate)0;
			object obj24 = obj + 144;
			while (true)
			{
				Delegate obj25 = Delegate.Combine(obj23, b4);
				bool flag13 = (object)obj25 == null;
				Delegate obj26 = obj25;
				if (!flag13)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag14 = (object)obj26 == null;
					obj = (UnityEngine.Object)(object)obj25;
					num2 = (nint)typeof(Action<string>);
					nint num3 = (nint)obj23;
					object obj2 = 0;
					nint num = unchecked((nint)null);
					if (flag14)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag15 = (object)obj21 != obj23;
				obj23 = obj21;
				if (flag15)
				{
					continue;
				}
				goto IL_0315;
			}
			goto IL_06e4;
		}
		Action<string> b5 = HandleMainMenuLoading;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v859 @ rdi_v6 (UnityEngine.Object)+88]");
		Delegate obj27 = (Delegate)0;
		object obj28 = obj + 136;
		while (true)
		{
			Delegate obj29 = Delegate.Combine(obj27, b5);
			bool flag16 = (object)obj29 == null;
			Delegate obj30 = obj29;
			if (!flag16)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				bool flag17 = (object)obj30 == null;
				obj = (UnityEngine.Object)(object)obj29;
				num2 = (nint)typeof(Action<string>);
				nint num3 = (nint)obj27;
				object obj2 = 0;
				nint num = unchecked((nint)null);
				if (flag17)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag18 = (object)obj22 != obj27;
			obj27 = obj22;
			if (flag18)
			{
				continue;
			}
			goto IL_03ca;
		}
		goto IL_074d;
		IL_04b5:
		bool flag19 = !verbose;
		subscribed = true;
		if (flag19)
		{
			return;
		}
		int instanceID = GetInstanceID();
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		string arg = base.name;
		object obj31 = default(object);
		object arg2 = (Trigger)obj31;
		object arg3 = default(object);
		string text = $"[{arg3}] MissionAdvanceReaction '{arg}': Subscribed to {arg2}";
		string text2;
		if (trigger == Trigger.OnPhaseChanged)
		{
			object obj32 = default(object);
			object arg4 = (PhaseFilter)obj32;
			object obj33 = default(object);
			object arg5 = (ExitPhaseFilter)obj33;
			text2 = $" (enter filter: {arg4}, exit filter: {arg5}).";
		}
		else
		{
			bool flag20 = !treatReturnToMainMenuAsMissionChange;
			text2 = ".";
			if (!flag20)
			{
				text2 = " (+ Main Menu bridging).";
			}
		}
		string message = text + text2;
		Debug.Log(message, this);
		return;
		IL_07c7:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private void Unsubscribe()
	{
		//IL_0467: Expected O, but got I4
		//IL_018c: Expected O, but got I4
		//IL_06dd: Expected O, but got I
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Expected O, but got Unknown
		//IL_00a4: Expected O, but got I4
		//IL_05b6: Expected O, but got I
		//IL_04a7: Expected I, but got O
		//IL_04b8: Expected O, but got I4
		//IL_04bd: Expected I, but got O
		//IL_055b: Expected O, but got I
		//IL_0505: Expected O, but got I4
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_022a: Expected O, but got I4
		//IL_01c6: Expected I, but got O
		//IL_01dc: Expected I, but got O
		//IL_01e5: Expected O, but got I4
		//IL_01ea: Expected I, but got O
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_00ec: Expected I, but got O
		//IL_00f5: Expected O, but got I4
		//IL_00fa: Expected I, but got O
		//IL_0150: Expected O, but got I4
		//IL_0629: Expected I, but got O
		//IL_0611: Expected O, but got I
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Expected O, but got Unknown
		//IL_02b1: Expected I, but got O
		//IL_02ba: Expected O, but got I4
		//IL_02bf: Expected I, but got O
		//IL_0674: Expected O, but got I
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Expected O, but got Unknown
		//IL_039e: Expected O, but got I4
		//IL_035e: Expected I, but got O
		//IL_0367: Expected O, but got I4
		//IL_036c: Expected I, but got O
		if (!subscribed)
		{
			return;
		}
		UnityEngine.Object obj = MissionManager._003CInstance_003Ek__BackingField;
		bool flag = MissionManager._003CInstance_003Ek__BackingField != null;
		bool flag2 = !flag;
		Delegate obj2 = null;
		Delegate obj9 = default(Delegate);
		Delegate obj20 = default(Delegate);
		nint num3;
		object obj15;
		if (!flag2)
		{
			if (trigger != Trigger.OnPhaseChanged)
			{
				if (trigger != Trigger.OnMissionChanging)
				{
					Action<MissionGraph, MissionGraph> value = HandleMissionEvent;
					bool flag3 = (object)MissionManager._003CInstance_003Ek__BackingField == null;
					object obj3 = 0;
					nint num = 0;
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v808 @ rsi_v7 (UnityEngine.Object)+80]");
						Delegate obj4 = (Delegate)0;
						while (true)
						{
							Delegate obj5 = Delegate.Remove(obj4, value);
							bool flag4 = (object)obj5 == null;
							Delegate obj6 = obj5;
							if (!flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								bool flag5 = (object)obj6 == null;
								Delegate obj7 = obj5;
								nint num2 = (nint)typeof(Action<MissionGraph, MissionGraph>);
								obj3 = 0;
								num = unchecked((nint)null);
								if (flag5)
								{
									break;
								}
							}
							object obj8 = obj + 128;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
							bool flag6 = (object)obj9 != obj4;
							obj4 = obj9;
							if (flag6)
							{
								continue;
							}
							goto IL_0147;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						goto IL_0687;
					}
				}
				else
				{
					Action<MissionGraph, MissionGraph> value2 = HandleMissionEvent;
					bool flag7 = (object)MissionManager._003CInstance_003Ek__BackingField == null;
					object obj3 = 0;
					nint num = 0;
					if (!flag7)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v808 @ rsi_v7 (UnityEngine.Object)+78]");
						Delegate obj10 = (Delegate)0;
						Delegate obj14 = default(Delegate);
						while (true)
						{
							Delegate obj11 = Delegate.Remove(obj10, value2);
							bool flag8 = (object)obj11 == null;
							Delegate obj12 = obj11;
							if (!flag8)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								bool flag9 = (object)obj12 == null;
								num3 = (nint)obj;
								Delegate obj7 = obj11;
								nint num2 = (nint)typeof(Action<MissionGraph, MissionGraph>);
								obj3 = 0;
								num = unchecked((nint)null);
								if (flag9)
								{
									break;
								}
							}
							object obj13 = obj + 120;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
							bool flag10 = (object)obj14 != obj10;
							obj15 = 0;
							obj2 = obj10;
							obj10 = obj14;
							if (flag10)
							{
								continue;
							}
							goto IL_0248;
						}
						goto IL_05bb;
					}
				}
			}
			else
			{
				Action<MissionManager.GamePhase, MissionManager.GamePhase> value3 = HandlePhaseChanged;
				bool flag11 = (object)MissionManager._003CInstance_003Ek__BackingField == null;
				object obj3 = 0;
				nint num = 0;
				if (!flag11)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v808 @ rsi_v7 (UnityEngine.Object)+28]");
					Delegate obj16 = (Delegate)0;
					object obj17 = MissionManager._003CInstance_003Ek__BackingField + 40;
					while (true)
					{
						Delegate obj18 = Delegate.Remove(obj16, value3);
						bool flag12 = (object)obj18 == null;
						Delegate obj19 = obj18;
						if (!flag12)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag13 = (object)obj19 == null;
							num3 = (nint)typeof(Action<MissionManager.GamePhase, MissionManager.GamePhase>);
							Delegate obj7 = obj18;
							obj3 = 0;
							num = unchecked((nint)null);
							if (flag13)
							{
								break;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
						bool flag14 = (object)obj20 != obj16;
						obj16 = obj20;
						if (flag14)
						{
							continue;
						}
						goto IL_04fc;
					}
					goto IL_06f1;
				}
			}
			throw new NullReferenceException();
		}
		goto IL_03bc;
		IL_030c:
		Action<string> value4 = HandleMainMenuLoaded;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v808 @ rsi_v7 (UnityEngine.Object)+90]");
		Delegate obj21 = (Delegate)0;
		object obj22 = obj + 144;
		Delegate obj25 = default(Delegate);
		while (true)
		{
			Delegate obj23 = Delegate.Remove(obj21, value4);
			bool flag15 = (object)obj23 == null;
			Delegate obj24 = obj23;
			if (!flag15)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				bool flag16 = (object)obj24 == null;
				obj = (UnityEngine.Object)(object)typeof(Action<string>);
				Delegate obj7 = obj23;
				nint num2 = (nint)obj21;
				object obj3 = 0;
				nint num = unchecked((nint)null);
				if (flag16)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag17 = (object)obj25 != obj21;
			obj15 = 0;
			obj2 = obj21;
			obj21 = obj25;
			if (flag17)
			{
				continue;
			}
			goto IL_03bc;
		}
		goto IL_0687;
		IL_0147:
		obj15 = 0;
		obj2 = obj9;
		goto IL_0248;
		IL_06f1:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_0248:
		if (!treatReturnToMainMenuAsMissionChange)
		{
			goto IL_03bc;
		}
		Action<string> value5 = HandleMainMenuLoading;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v808 @ rsi_v7 (UnityEngine.Object)+88]");
		Delegate obj26 = (Delegate)0;
		Delegate obj30 = default(Delegate);
		while (true)
		{
			Delegate obj27 = Delegate.Remove(obj26, value5);
			bool flag18 = (object)obj27 == null;
			Delegate obj28 = obj27;
			if (!flag18)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				bool flag19 = (object)obj28 == null;
				Delegate obj7 = obj27;
				nint num2 = (nint)typeof(Action<string>);
				object obj3 = 0;
				nint num = unchecked((nint)null);
				if (flag19)
				{
					break;
				}
			}
			object obj29 = obj + 136;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag20 = (object)obj30 != obj26;
			obj26 = obj30;
			if (flag20)
			{
				continue;
			}
			goto IL_030c;
		}
		goto IL_0616;
		IL_05bb:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_06f1;
		IL_0616:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		num3 = (nint)obj;
		goto IL_05bb;
		IL_03bc:
		bool flag21 = !verbose;
		subscribed = false;
		if (!flag21)
		{
			int instanceID = GetInstanceID();
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			string arg = base.name;
			object arg2 = default(object);
			string message = $"[{arg2}] MissionAdvanceReaction '{arg}': Unsubscribed.";
			Debug.Log(message, this);
		}
		return;
		IL_04fc:
		obj15 = 0;
		obj2 = obj20;
		goto IL_03bc;
		IL_0687:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_0616;
	}

	private void HandleMissionEvent(MissionGraph fromIndex, MissionGraph toIndex)
	{
		HandleTriggerInternal(fromIndex, toIndex, isMainMenuTransition: false);
	}

	private void HandleMainMenuLoading(string sceneName)
	{
		//IL_043d: Expected I, but got O
		//IL_00bb: Expected O, but got I
		//IL_0082: Expected I, but got O
		//IL_01bf: Expected I4, but got O
		//IL_00ef: Expected I, but got O
		//IL_00ff: Expected O, but got I
		//IL_0165: Expected I, but got O
		//IL_0175: Expected O, but got I
		//IL_01ed: Expected I, but got O
		//IL_01fd: Expected O, but got I
		//IL_02a4: Expected I, but got O
		//IL_02b4: Expected O, but got I
		nint num = (nint)typeof(MissionManager);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v3 (Il2CppClass<MissionManager>)+B8]");
		nint num2 = 0;
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		bool flag = !verbose;
		MissionGraph missionGraph = missionManager._003CCurrentMission_003Ek__BackingField;
		if (!flag)
		{
			object[] array = new object[6];
			int instanceID = GetInstanceID();
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			IntPtr intPtr = default(IntPtr);
			if (intPtr != (IntPtr)0)
			{
				nint num3 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text = default(string);
					throw text;
				}
			}
			array[0] = (nint)intPtr;
			string text2 = base.name;
			if (text2 != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v468 @ rdx_v40 (Il2CppClass<System.Object[]>)+40]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				bool flag2 = obj3 == null;
				string text3 = text2;
				if (flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text4 = default(string);
					throw text4;
				}
			}
			array[1] = text2;
			string text5 = default(string);
			if (text5 != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v532 @ rdx_v38 (Il2CppClass<System.Object[]>)+40]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj5 = default(object);
				bool flag3 = obj5 == null;
				string text6 = text5;
				if (flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj6 = default(object);
					throw obj6;
				}
			}
			array[2] = text5;
			object obj8 = default(object);
			object obj7 = (Trigger)obj8;
			if (obj7 != null)
			{
				nint num6 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ rdx_v36 (Il2CppClass<System.Object[]>)+40]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj10 = default(object);
				bool flag4 = obj10 == null;
				object obj11 = obj7;
				if (flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text7 = default(string);
					throw text7;
				}
			}
			array[3] = obj7;
			string text8;
			if ((object)missionManager._003CCurrentMission_003Ek__BackingField != null)
			{
				string missionID = missionGraph.MissionID;
				bool flag5 = missionGraph.MissionID == null;
				text8 = missionGraph.MissionID;
				if (!flag5)
				{
					nint num7 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v668 @ rdx_v34 (Il2CppClass<System.Object[]>)+40]");
					object obj12 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj13 = default(object);
					if (obj13 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj14 = default(object);
						throw obj14;
					}
					text8 = missionGraph.MissionID;
				}
			}
			else
			{
				text8 = null;
			}
			array[4] = text8;
			array[5] = null;
			string message = string.Format("[{0}] MissionAdvanceReaction '{1}': MainMenuLoading('{2}') bridged as {3} (from {4} to {5}).", array);
			Debug.Log(message, this);
		}
		HandleTriggerInternal(missionManager._003CCurrentMission_003Ek__BackingField, null, isMainMenuTransition: true);
	}

	private void HandleMainMenuLoaded(string sceneName)
	{
		//IL_043d: Expected I, but got O
		//IL_00bb: Expected O, but got I
		//IL_0082: Expected I, but got O
		//IL_01bf: Expected I4, but got O
		//IL_00ef: Expected I, but got O
		//IL_00ff: Expected O, but got I
		//IL_0165: Expected I, but got O
		//IL_0175: Expected O, but got I
		//IL_01ed: Expected I, but got O
		//IL_01fd: Expected O, but got I
		//IL_02a4: Expected I, but got O
		//IL_02b4: Expected O, but got I
		nint num = (nint)typeof(MissionManager);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v3 (Il2CppClass<MissionManager>)+B8]");
		nint num2 = 0;
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		bool flag = !verbose;
		MissionGraph missionGraph = missionManager._003CCurrentMission_003Ek__BackingField;
		if (!flag)
		{
			object[] array = new object[6];
			int instanceID = GetInstanceID();
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			IntPtr intPtr = default(IntPtr);
			if (intPtr != (IntPtr)0)
			{
				nint num3 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text = default(string);
					throw text;
				}
			}
			array[0] = (nint)intPtr;
			string text2 = base.name;
			if (text2 != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v468 @ rdx_v40 (Il2CppClass<System.Object[]>)+40]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				bool flag2 = obj3 == null;
				string text3 = text2;
				if (flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text4 = default(string);
					throw text4;
				}
			}
			array[1] = text2;
			string text5 = default(string);
			if (text5 != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v532 @ rdx_v38 (Il2CppClass<System.Object[]>)+40]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj5 = default(object);
				bool flag3 = obj5 == null;
				string text6 = text5;
				if (flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj6 = default(object);
					throw obj6;
				}
			}
			array[2] = text5;
			object obj8 = default(object);
			object obj7 = (Trigger)obj8;
			if (obj7 != null)
			{
				nint num6 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ rdx_v36 (Il2CppClass<System.Object[]>)+40]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj10 = default(object);
				bool flag4 = obj10 == null;
				object obj11 = obj7;
				if (flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text7 = default(string);
					throw text7;
				}
			}
			array[3] = obj7;
			string text8;
			if ((object)missionManager._003CCurrentMission_003Ek__BackingField != null)
			{
				string missionID = missionGraph.MissionID;
				bool flag5 = missionGraph.MissionID == null;
				text8 = missionGraph.MissionID;
				if (!flag5)
				{
					nint num7 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v668 @ rdx_v34 (Il2CppClass<System.Object[]>)+40]");
					object obj12 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj13 = default(object);
					if (obj13 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj14 = default(object);
						throw obj14;
					}
					text8 = missionGraph.MissionID;
				}
			}
			else
			{
				text8 = null;
			}
			array[4] = text8;
			array[5] = null;
			string message = string.Format("[{0}] MissionAdvanceReaction '{1}': MainMenuLoaded('{2}') bridged as {3} (from {4} to {5}).", array);
			Debug.Log(message, this);
		}
		HandleTriggerInternal(missionManager._003CCurrentMission_003Ek__BackingField, null, isMainMenuTransition: true);
	}

	private unsafe void HandlePhaseChanged(MissionManager.GamePhase prev, MissionManager.GamePhase next)
	{
		//IL_0015: Expected O, but got I4
		//IL_00f8: Expected O, but got I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_00a2: Expected O, but got I4
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0185: Expected O, but got I4
		//IL_0078: Expected O, but got I4
		//IL_063e: Expected O, but got Ref
		//IL_015b: Expected O, but got I4
		//IL_06c2: Expected O, but got I
		//IL_0251: Expected O, but got Ref
		//IL_0671: Expected I, but got O
		//IL_0681: Expected O, but got I
		//IL_0750: Expected I4, but got O
		//IL_0276: Expected O, but got Ref
		//IL_06f6: Expected I, but got O
		//IL_0706: Expected O, but got I
		//IL_07d8: Expected I4, but got O
		//IL_028c: Expected I, but got O
		//IL_029c: Expected O, but got I
		//IL_02cd: Expected O, but got I
		//IL_077e: Expected I, but got O
		//IL_078e: Expected O, but got I
		//IL_0317: Expected O, but got I
		//IL_033e: Expected O, but got I4
		//IL_0343: Expected I, but got O
		//IL_0885: Expected I4, but got O
		//IL_0806: Expected I, but got O
		//IL_0816: Expected O, but got I
		//IL_08ae: Expected O, but got Ref
		//IL_08bc: Expected I, but got O
		//IL_0359: Expected I, but got O
		//IL_0369: Expected O, but got I
		//IL_038a: Expected I, but got O
		//IL_039a: Expected O, but got I
		//IL_03ed: Expected I4, but got O
		//IL_03f1: Expected I, but got O
		//IL_040d: Expected O, but got Ref
		//IL_041b: Expected I, but got O
		//IL_08ef: Expected I, but got O
		//IL_08ff: Expected O, but got I
		//IL_0431: Expected I, but got O
		//IL_0441: Expected O, but got I
		//IL_0472: Expected O, but got I
		//IL_09e0: Expected I4, but got O
		//IL_04bc: Expected O, but got I
		//IL_04c5: Expected I4, but got O
		//IL_04c9: Expected I, but got O
		//IL_097d: Expected I, but got O
		//IL_098d: Expected O, but got I
		//IL_04e5: Expected O, but got Ref
		//IL_04f3: Expected I, but got O
		//IL_0a0e: Expected I, but got O
		//IL_0a1e: Expected O, but got I
		//IL_0509: Expected I, but got O
		//IL_0519: Expected O, but got I
		//IL_054a: Expected O, but got I
		//IL_0594: Expected O, but got I
		//IL_0a9c: Expected I, but got O
		//IL_0aac: Expected O, but got I
		bool flag = phaseFilter == PhaseFilter.Any;
		bool flag2;
		if (!flag)
		{
			object obj = phaseFilter - 1;
			MissionManager.GamePhase gamePhase = default(MissionManager.GamePhase);
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						flag2 = false;
					}
					else
					{
						object obj3 = gamePhase - 2;
						bool flag3 = obj3 == null;
						flag2 = flag3;
					}
				}
				else
				{
					object obj4 = gamePhase - 1;
					bool flag4 = obj4 == null;
					flag2 = flag4;
				}
			}
			else
			{
				bool flag5 = gamePhase == MissionManager.GamePhase.MainMenu;
				flag2 = flag5;
			}
		}
		else
		{
			flag2 = true;
		}
		bool flag6 = exitPhaseFilter == ExitPhaseFilter.Any;
		bool flag7;
		if (!flag6)
		{
			object obj5 = exitPhaseFilter - 1;
			if (!flag6)
			{
				object obj6 = obj5 - 1;
				if (!flag6)
				{
					if ((nint)obj6 != 1)
					{
						flag7 = false;
					}
					else
					{
						object obj7 = prev - 2;
						bool flag8 = obj7 == null;
						flag7 = flag8;
					}
				}
				else
				{
					object obj8 = prev - 1;
					bool flag9 = obj8 == null;
					flag7 = flag9;
				}
			}
			else
			{
				bool flag10 = prev == MissionManager.GamePhase.MainMenu;
				flag7 = flag10;
			}
		}
		else
		{
			flag7 = true;
		}
		object obj15 = default(object);
		object obj18 = default(object);
		object obj12;
		nint num3;
		if (flag2 && flag7)
		{
			if (verbose)
			{
				object[] array = new object[4];
				int instanceID = GetInstanceID();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				bool flag11 = array == null;
				object obj10 = default(object);
				object obj9 = (object)(&obj10);
				if (!flag11)
				{
					IntPtr intPtr = default(IntPtr);
					bool flag12 = intPtr == (IntPtr)0;
					obj9 = (object)(&obj10);
					if (!flag12)
					{
						nint num2 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v619 @ rdx_v101 (Il2CppClass<System.Object[]>)+40]");
						obj9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj11 = default(object);
						bool flag13 = obj11 == null;
						num = intPtr;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v619 @ rdx_v101 (Il2CppClass<System.Object[]>)+40]");
						obj12 = 0;
						num3 = intPtr;
						if (flag13)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							string text = default(string);
							throw text;
						}
					}
					if (array.Length > 0)
					{
						array[0] = (nint)intPtr;
						string text2 = base.name;
						bool flag14 = text2 == null;
						obj9 = 0;
						num = (nint)this;
						if (!flag14)
						{
							nint num4 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1016 @ rdx_v99 (Il2CppClass<System.Object[]>)+40]");
							obj9 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj13 = default(object);
							bool flag15 = obj13 == null;
							num = (nint)text2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1016 @ rdx_v99 (Il2CppClass<System.Object[]>)+40]");
							object obj14 = 0;
							string text3 = text2;
							if (flag15)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								IntPtr intPtr2 = default(IntPtr);
								throw intPtr2;
							}
						}
						if (array.Length > 1)
						{
							array[1] = text2;
							nint num5 = (nint)(object)(MissionManager.GamePhase)obj15;
							bool flag16 = num5 == 0;
							obj9 = (object)(&obj15);
							num = (nint)typeof(MissionManager.GamePhase);
							if (!flag16)
							{
								nint num6 = (nint)array;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1188 @ rdx_v97 (Il2CppClass<System.Object[]>)+40]");
								obj9 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj16 = default(object);
								bool flag17 = obj16 == null;
								num = num5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1188 @ rdx_v97 (Il2CppClass<System.Object[]>)+40]");
								object obj17 = 0;
								IntPtr intPtr3 = num5;
								if (flag17)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									IntPtr intPtr4 = default(IntPtr);
									throw intPtr4;
								}
							}
							if (array.Length > 2)
							{
								array[2] = num5;
								nint num7 = (nint)(object)(MissionManager.GamePhase)obj18;
								bool flag18 = num7 == 0;
								obj9 = (object)(&obj18);
								num = (nint)typeof(MissionManager.GamePhase);
								if (!flag18)
								{
									nint num8 = (nint)array;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1307 @ rdx_v95 (Il2CppClass<System.Object[]>)+40]");
									obj9 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj19 = default(object);
									bool flag19 = obj19 == null;
									num = num7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1307 @ rdx_v95 (Il2CppClass<System.Object[]>)+40]");
									object obj20 = 0;
									IntPtr intPtr5 = num7;
									if (flag19)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										IntPtr intPtr6 = default(IntPtr);
										throw intPtr6;
									}
								}
								if (array.Length > 3)
								{
									array[3] = num7;
									string message = string.Format("[{0}] MissionAdvanceReaction '{1}': PhaseChanged {2}→{3} passed both filters, triggering.", array);
									Debug.Log(message, this);
									goto IL_05bf;
								}
							}
						}
					}
					throw new IndexOutOfRangeException();
				}
				throw new NullReferenceException();
			}
			goto IL_05bf;
		}
		if (!verbose)
		{
			return;
		}
		object[] array2 = new object[4];
		int instanceID2 = GetInstanceID();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		bool flag20 = array2 == null;
		obj12 = (object)(&obj18);
		if (!flag20)
		{
			IntPtr intPtr7 = default(IntPtr);
			if (intPtr7 != (IntPtr)0)
			{
				nint num9 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v579 @ rdx_v75 (Il2CppClass<System.Object[]>)+40]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj21 = default(object);
				bool flag21 = obj21 == null;
				nint num = intPtr7;
				if (flag21)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text4 = default(string);
					throw text4;
				}
			}
			array2[0] = (nint)intPtr7;
			string text5 = base.name;
			if (text5 != null)
			{
				nint num10 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v950 @ rdx_v73 (Il2CppClass<System.Object[]>)+40]");
				object obj22 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj23 = default(object);
				bool flag22 = obj23 == null;
				string text6 = text5;
				if (flag22)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj24 = default(object);
					throw obj24;
				}
			}
			array2[1] = text5;
			object obj25 = (MissionManager.GamePhase)obj15;
			if (obj25 != null)
			{
				nint num11 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1172 @ rdx_v71 (Il2CppClass<System.Object[]>)+40]");
				object obj26 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj27 = default(object);
				bool flag23 = obj27 == null;
				object obj28 = obj25;
				if (flag23)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj29 = default(object);
					throw obj29;
				}
			}
			array2[2] = obj25;
			object obj31 = default(object);
			object obj30 = (MissionManager.GamePhase)obj31;
			if (obj30 != null)
			{
				nint num12 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1294 @ rdx_v69 (Il2CppClass<System.Object[]>)+40]");
				object obj32 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj33 = default(object);
				bool flag24 = obj33 == null;
				object obj34 = obj30;
				if (flag24)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj35 = default(object);
					throw obj35;
				}
			}
			array2[3] = obj30;
			string text7 = string.Format("[{0}] MissionAdvanceReaction '{1}': PhaseChanged {2}→{3} skipped ", array2);
			object[] array3 = new object[4];
			object obj37 = default(object);
			object obj36 = (PhaseFilter)obj37;
			bool flag25 = array3 == null;
			MissionManager.GamePhase gamePhase = MissionManager.GamePhase.MainMenu;
			obj12 = (object)(&obj37);
			num3 = (nint)typeof(PhaseFilter);
			if (!flag25)
			{
				if (obj36 != null)
				{
					nint num13 = (nint)array3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1434 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
					object obj38 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj39 = default(object);
					bool flag26 = obj39 == null;
					gamePhase = MissionManager.GamePhase.MainMenu;
					object obj40 = obj36;
					if (flag26)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj41 = default(object);
						throw obj41;
					}
				}
				array3[0] = obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj42 = default(object);
				if (obj42 != null)
				{
					nint num14 = (nint)array3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1523 @ rdx_v65 (Il2CppClass<System.Object[]>)+40]");
					object obj43 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj44 = default(object);
					bool flag27 = obj44 == null;
					gamePhase = MissionManager.GamePhase.MainMenu;
					object obj45 = obj42;
					if (flag27)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj46 = default(object);
						throw obj46;
					}
				}
				array3[1] = obj42;
				object obj48 = default(object);
				object obj47 = (ExitPhaseFilter)obj48;
				if (obj47 != null)
				{
					nint num15 = (nint)array3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1552 @ rdx_v63 (Il2CppClass<System.Object[]>)+40]");
					object obj49 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj50 = default(object);
					bool flag28 = obj50 == null;
					gamePhase = MissionManager.GamePhase.MainMenu;
					object obj51 = obj47;
					if (flag28)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj52 = default(object);
						throw obj52;
					}
				}
				array3[2] = obj47;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj53 = default(object);
				if (obj53 != null)
				{
					nint num16 = (nint)array3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1580 @ rdx_v61 (Il2CppClass<System.Object[]>)+40]");
					object obj54 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj55 = default(object);
					bool flag29 = obj55 == null;
					gamePhase = MissionManager.GamePhase.MainMenu;
					object obj56 = obj53;
					if (flag29)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj57 = default(object);
						throw obj57;
					}
				}
				array3[3] = obj53;
				string text8 = string.Format("(enter filter: {0}={1}, exit filter: {2}={3}).", array3);
				string message2 = text7 + text8;
				Debug.Log(message2, this);
				return;
			}
		}
		throw new NullReferenceException();
		IL_05bf:
		HandleTriggerInternal(null, null, isMainMenuTransition: false);
	}

	private unsafe void HandleTriggerInternal(MissionGraph fromIndex, MissionGraph toIndex, bool isMainMenuTransition)
	{
		//IL_066f: Expected O, but got I
		//IL_0690: Expected O, but got Ref
		//IL_0b67: Expected O, but got I
		//IL_06b5: Expected O, but got Ref
		//IL_007e: Expected O, but got I
		//IL_009f: Expected O, but got Ref
		//IL_06cb: Expected I, but got O
		//IL_06db: Expected O, but got I
		//IL_070c: Expected O, but got I
		//IL_00c4: Expected O, but got Ref
		//IL_0bb3: Expected I, but got O
		//IL_0bc3: Expected O, but got I
		//IL_00da: Expected I, but got O
		//IL_00ea: Expected O, but got I
		//IL_0794: Expected I, but got O
		//IL_07a4: Expected O, but got I
		//IL_07d5: Expected O, but got I
		//IL_0828: Expected I4, but got O
		//IL_082c: Expected I, but got O
		//IL_0c5f: Expected I, but got O
		//IL_0c6f: Expected O, but got I
		//IL_0848: Expected O, but got Ref
		//IL_0856: Expected I, but got O
		//IL_0190: Expected I, but got O
		//IL_01a0: Expected O, but got I
		//IL_01d9: Expected O, but got I
		//IL_0ce0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce5: Expected O, but got Unknown
		//IL_08df: Expected O, but got I
		//IL_0239: Expected I, but got O
		//IL_086c: Expected I, but got O
		//IL_087c: Expected O, but got I
		//IL_08ad: Expected O, but got I
		//IL_0255: Expected O, but got Ref
		//IL_0263: Expected I, but got O
		//IL_0d13: Expected O, but got I
		//IL_08ff: Expected O, but got I
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Expected O, but got Unknown
		//IL_02f4: Expected O, but got I
		//IL_09d7: Expected O, but got I
		//IL_0279: Expected I, but got O
		//IL_0289: Expected O, but got I
		//IL_02c2: Expected O, but got I
		//IL_0d42: Expected I, but got O
		//IL_0d52: Expected O, but got I
		//IL_0314: Expected O, but got I
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Expected O, but got Unknown
		//IL_0dc8: Expected I, but got O
		//IL_09bd: Expected O, but got I
		//IL_03f4: Expected O, but got I
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a01: Expected O, but got Unknown
		//IL_0969: Expected I, but got O
		//IL_03da: Expected O, but got I
		//IL_0df3: Expected I, but got O
		//IL_0e03: Expected O, but got I
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Expected O, but got Unknown
		//IL_037e: Expected I, but got O
		//IL_0e6c: Expected O, but got I
		//IL_0e75: Expected I4, but got O
		//IL_0e79: Expected I, but got O
		//IL_0a6a: Expected I, but got O
		//IL_0a7a: Expected O, but got I
		//IL_0ea4: Expected I, but got O
		//IL_0eb4: Expected O, but got I
		//IL_048f: Expected I, but got O
		//IL_049f: Expected O, but got I
		//IL_04dd: Expected O, but got I
		//IL_0f1d: Expected O, but got I
		//IL_0f2d: Expected O, but got I
		//IL_0f61: Expected I, but got O
		//IL_0f71: Expected O, but got I
		//IL_100e: Expected I, but got O
		//IL_101e: Expected O, but got I
		bool flag16 = default(bool);
		string message;
		MissionGraph missionGraph2 = default(MissionGraph);
		if (ignoreFirstTrigger && !hasIgnoredFirst)
		{
			hasIgnoredFirst = true;
			if (!verbose)
			{
				return;
			}
			object[] array = new object[5];
			int instanceID = GetInstanceID();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
			MissionAdvanceReaction missionAdvanceReaction = (MissionAdvanceReaction)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			bool flag = array == null;
			int num = default(int);
			string text = (string)(&num);
			if (!flag)
			{
				MissionAdvanceReaction missionAdvanceReaction2 = default(MissionAdvanceReaction);
				bool flag2 = (object)missionAdvanceReaction2 == null;
				text = (string)(&num);
				if (!flag2)
				{
					nint num2 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v590 @ rdx_v168 (Il2CppClass<System.Object[]>)+40]");
					text = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj = default(object);
					bool flag3 = obj == null;
					missionAdvanceReaction = missionAdvanceReaction2;
					MissionGraph missionGraph = null;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						string text2 = default(string);
						throw text2;
					}
				}
				if (array.Length > 0)
				{
					array[0] = missionAdvanceReaction2;
					string text3 = base.name;
					bool flag4 = text3 == null;
					text = null;
					string text4 = (string)(object)this;
					if (!flag4)
					{
						nint num3 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1111 @ rdx_v166 (Il2CppClass<System.Object[]>)+40]");
						text = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj2 = default(object);
						bool flag5 = obj2 == null;
						text4 = text3;
						MissionGraph missionGraph = missionGraph2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1111 @ rdx_v166 (Il2CppClass<System.Object[]>)+40]");
						string text5 = (string)0;
						string text6 = text3;
						if (flag5)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							IntPtr intPtr = default(IntPtr);
							throw intPtr;
						}
					}
					bool flag6 = array.Length <= 1;
					missionAdvanceReaction = (MissionAdvanceReaction)(object)text4;
					if (!flag6)
					{
						array[1] = text3;
						Trigger trigger = default(Trigger);
						nint num4 = (nint)(object)trigger;
						bool flag7 = num4 == 0;
						text = (string)(&trigger);
						nint num5 = (nint)typeof(Trigger);
						if (!flag7)
						{
							nint num6 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1579 @ rdx_v164 (Il2CppClass<System.Object[]>)+40]");
							text = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj3 = default(object);
							bool flag8 = obj3 == null;
							num5 = num4;
							MissionGraph missionGraph = missionGraph2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1579 @ rdx_v164 (Il2CppClass<System.Object[]>)+40]");
							string text7 = (string)0;
							IntPtr intPtr2 = num4;
							if (flag8)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								string text8 = default(string);
								throw text8;
							}
						}
						bool flag9 = array.Length <= 2;
						missionAdvanceReaction = (MissionAdvanceReaction)num5;
						if (!flag9)
						{
							array[2] = num4;
							string text9 = (string)(array + 48);
							string text11;
							string text13;
							if ((object)fromIndex != null)
							{
								text9 = fromIndex.MissionID;
								bool flag10 = fromIndex.MissionID == null;
								nint num7 = num4;
								if (!flag10)
								{
									nint num8 = (nint)array;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1855 @ rdx_v162 (Il2CppClass<System.Object[]>)+40]");
									num7 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj4 = default(object);
									bool flag11 = obj4 == null;
									MissionGraph missionGraph = missionGraph2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1855 @ rdx_v162 (Il2CppClass<System.Object[]>)+40]");
									nint num9 = 0;
									if (flag11)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										string text10 = default(string);
										throw text10;
									}
								}
								text11 = null;
								string text12 = (string)num7;
								text13 = text9;
							}
							else
							{
								text11 = null;
								string text12 = (string)num4;
								text13 = null;
							}
							if (array.Length > 3)
							{
								array[3] = text13;
								text9 = (string)(array + 56);
								bool flag12 = (object)missionGraph2 == null;
								string text12 = text13;
								if (!flag12)
								{
									text9 = missionGraph2.MissionID;
									bool flag13 = missionGraph2.MissionID == null;
									text11 = missionGraph2.MissionID;
									text12 = text13;
									if (!flag13)
									{
										nint num10 = (nint)array;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2160 @ rdx_v159 (Il2CppClass<System.Object[]>)+40]");
										text12 = (string)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj5 = default(object);
										bool flag14 = obj5 == null;
										text11 = missionGraph2.MissionID;
										MissionGraph missionGraph = missionGraph2;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2160 @ rdx_v159 (Il2CppClass<System.Object[]>)+40]");
										string text14 = (string)0;
										string missionID = missionGraph2.MissionID;
										if (flag14)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											MissionAdvanceReaction missionAdvanceReaction3 = default(MissionAdvanceReaction);
											throw missionAdvanceReaction3;
										}
									}
								}
								if (array.Length > 4)
								{
									array[4] = text11;
									string text15 = string.Format("[{0}] MissionAdvanceReaction '{1}': Ignoring first trigger at {2} (from {3} to {4})", array);
									bool flag15 = !flag16;
									string text16 = ".";
									if (!flag15)
									{
										text16 = " [MainMenu bridged].";
									}
									message = text15 + text16;
									goto IL_0613;
								}
							}
							goto IL_12bf;
						}
					}
				}
				throw new IndexOutOfRangeException();
			}
			throw new NullReferenceException();
		}
		if (runOnce && hasRun)
		{
			if (verbose)
			{
				int instanceID2 = GetInstanceID();
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				string arg = base.name;
				object arg2 = default(object);
				message = $"[{arg2}] MissionAdvanceReaction '{arg}': Ignoring event (already ran once).";
				goto IL_0613;
			}
			return;
		}
		string text24;
		if (verbose)
		{
			object[] array2 = new object[5];
			int instanceID3 = GetInstanceID();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
			string text9 = (string)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			bool flag17 = array2 == null;
			int num11 = default(int);
			string text12 = (string)(&num11);
			if (!flag17)
			{
				MissionAdvanceReaction missionAdvanceReaction4 = default(MissionAdvanceReaction);
				bool flag18 = (object)missionAdvanceReaction4 == null;
				text12 = (string)(&num11);
				if (!flag18)
				{
					nint num12 = (nint)array2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v666 @ rdx_v134 (Il2CppClass<System.Object[]>)+40]");
					text12 = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj6 = default(object);
					bool flag19 = obj6 == null;
					text9 = (string)(object)missionAdvanceReaction4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v666 @ rdx_v134 (Il2CppClass<System.Object[]>)+40]");
					string text = (string)0;
					MissionAdvanceReaction missionAdvanceReaction = missionAdvanceReaction4;
					if (flag19)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						string text17 = default(string);
						throw text17;
					}
				}
				if (array2.Length > 0)
				{
					array2[0] = missionAdvanceReaction4;
					string text18 = base.name;
					bool flag20 = text18 == null;
					text12 = null;
					text9 = (string)(object)this;
					if (!flag20)
					{
						nint num13 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1173 @ rdx_v132 (Il2CppClass<System.Object[]>)+40]");
						text12 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj7 = default(object);
						bool flag21 = obj7 == null;
						text9 = text18;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1173 @ rdx_v132 (Il2CppClass<System.Object[]>)+40]");
						string text19 = (string)0;
						string text20 = text18;
						if (flag21)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							IntPtr intPtr3 = default(IntPtr);
							throw intPtr3;
						}
					}
					if (array2.Length > 1)
					{
						array2[1] = text18;
						object obj8 = default(object);
						nint num14 = (nint)(object)(Trigger)obj8;
						bool flag22 = num14 == 0;
						text12 = (string)(&obj8);
						nint num15 = (nint)typeof(Trigger);
						if (!flag22)
						{
							nint num16 = (nint)array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1595 @ rdx_v130 (Il2CppClass<System.Object[]>)+40]");
							text12 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj9 = default(object);
							bool flag23 = obj9 == null;
							num15 = num14;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1595 @ rdx_v130 (Il2CppClass<System.Object[]>)+40]");
							string text21 = (string)0;
							IntPtr intPtr4 = num14;
							if (flag23)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								string text22 = default(string);
								throw text22;
							}
						}
						bool flag24 = array2.Length <= 2;
						text9 = (string)num15;
						if (!flag24)
						{
							array2[2] = num14;
							text9 = (string)(array2 + 48);
							string text25;
							if ((object)fromIndex != null)
							{
								text9 = fromIndex.MissionID;
								bool flag25 = fromIndex.MissionID == null;
								nint num17 = num14;
								if (!flag25)
								{
									nint num18 = (nint)array2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1897 @ rdx_v128 (Il2CppClass<System.Object[]>)+40]");
									num17 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj10 = default(object);
									bool flag26 = obj10 == null;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1897 @ rdx_v128 (Il2CppClass<System.Object[]>)+40]");
									nint num19 = 0;
									if (flag26)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										string text23 = default(string);
										throw text23;
									}
								}
								text24 = null;
								text12 = (string)num17;
								text25 = text9;
							}
							else
							{
								text24 = null;
								text12 = (string)num14;
								text25 = null;
							}
							if (array2.Length > 3)
							{
								array2[3] = text25;
								text9 = (string)(array2 + 56);
								string text26;
								if ((object)missionGraph2 != null)
								{
									text9 = missionGraph2.MissionID;
									bool flag27 = missionGraph2.MissionID == null;
									text12 = text25;
									text26 = missionGraph2.MissionID;
									if (!flag27)
									{
										nint num20 = (nint)array2;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2163 @ rdx_v125 (Il2CppClass<System.Object[]>)+40]");
										text12 = (string)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj11 = default(object);
										if (obj11 == null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											string text27 = default(string);
											throw text27;
										}
										text26 = missionGraph2.MissionID;
									}
								}
								else
								{
									text12 = text25;
									text26 = text24;
								}
								if (array2.Length > 4)
								{
									array2[4] = text26;
									string text28 = string.Format("[{0}] MissionAdvanceReaction '{1}': Triggered at {2} (from {3} to {4})", array2);
									bool flag28 = !flag16;
									string text29 = ".";
									if (!flag28)
									{
										text29 = " [MainMenu bridged].";
									}
									string message2 = text28 + text29;
									Debug.Log(message2, this);
									goto IL_13fd;
								}
							}
							goto IL_12bf;
						}
					}
				}
				throw new IndexOutOfRangeException();
			}
			throw new NullReferenceException();
		}
		text24 = null;
		goto IL_13fd;
		IL_1441:
		throw new NullReferenceException();
		IL_0613:
		Debug.Log(message, this);
		return;
		IL_12bf:
		throw new IndexOutOfRangeException();
		IL_13fd:
		hasRun = true;
		SafeInvoke(onTriggered, "onTriggered");
		List<GameObject> list = BuildTargetList();
		if (!verbose)
		{
			goto IL_10b1;
		}
		object[] array3 = new object[7];
		int instanceID4 = GetInstanceID();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		string text30 = (string)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		UnityEngine.Object obj19 = default(UnityEngine.Object);
		if (array3 != null)
		{
			string text31 = default(string);
			if (text31 != null)
			{
				nint num21 = (nint)array3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1223 @ rdx_v103 (Il2CppClass<System.Object[]>)+40]");
				string text12 = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj12 = default(object);
				bool flag29 = obj12 == null;
				missionGraph2 = null;
				string text9 = text31;
				if (flag29)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text32 = default(string);
					throw text32;
				}
			}
			if (array3.Length > 0)
			{
				array3[0] = text31;
				string text33 = base.name;
				if (text33 != null)
				{
					nint num22 = (nint)array3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1611 @ rdx_v101 (Il2CppClass<System.Object[]>)+40]");
					object obj13 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj14 = default(object);
					bool flag30 = obj14 == null;
					missionGraph2 = null;
					string text34 = text33;
					if (flag30)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						string text35 = default(string);
						throw text35;
					}
				}
				if (array3.Length > 1)
				{
					array3[1] = text33;
					text30 = (string)(array3 + 40);
					if (list == null)
					{
						goto IL_1441;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
					text30 = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string text36 = default(string);
					if (text36 != null)
					{
						nint num23 = (nint)array3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2063 @ rdx_v99 (Il2CppClass<System.Object[]>)+40]");
						object obj15 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj16 = default(object);
						bool flag31 = obj16 == null;
						missionGraph2 = null;
						string text37 = text36;
						if (flag31)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							IntPtr intPtr5 = default(IntPtr);
							throw intPtr5;
						}
					}
					if (array3.Length > 2)
					{
						array3[2] = text36;
						ActionType actionType = default(ActionType);
						nint num24 = (nint)(object)actionType;
						if (num24 != 0)
						{
							nint num25 = (nint)array3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2235 @ rdx_v97 (Il2CppClass<System.Object[]>)+40]");
							object obj17 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj18 = default(object);
							bool flag32 = obj18 == null;
							missionGraph2 = null;
							IntPtr intPtr6 = num24;
							if (flag32)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								IntPtr intPtr7 = default(IntPtr);
								throw intPtr7;
							}
						}
						if (array3.Length > 3)
						{
							array3[3] = num24;
							nint num26 = (nint)(object)(TargetSelection)obj19;
							if (num26 != 0)
							{
								nint num27 = (nint)array3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2305 @ rdx_v95 (Il2CppClass<System.Object[]>)+40]");
								object obj20 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj21 = default(object);
								bool flag33 = obj21 == null;
								missionGraph2 = null;
								IntPtr intPtr8 = num26;
								if (flag33)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									string text38 = default(string);
									throw text38;
								}
							}
							if (array3.Length > 4)
							{
								array3[4] = num26;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
								text30 = (string)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								string text39 = default(string);
								if (text39 != null)
								{
									nint num28 = (nint)array3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2337 @ rdx_v93 (Il2CppClass<System.Object[]>)+40]");
									object obj22 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj23 = default(object);
									bool flag34 = obj23 == null;
									missionGraph2 = null;
									string text40 = text39;
									if (flag34)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										string text41 = default(string);
										throw text41;
									}
								}
								if (array3.Length > 5)
								{
									array3[5] = text39;
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									string text42 = default(string);
									if (text42 != null)
									{
										nint num29 = (nint)array3;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2366 @ rdx_v91 (Il2CppClass<System.Object[]>)+40]");
										object obj24 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj25 = default(object);
										bool flag35 = obj25 == null;
										missionGraph2 = null;
										string text43 = text42;
										if (flag35)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											string text44 = default(string);
											throw text44;
										}
									}
									if (array3.Length > 6)
									{
										array3[6] = text42;
										string message3 = string.Format("[{0}] '{1}': Processing {2} object(s). Action={3}, Selection={4}, Recursive={5}, IncludeInactive={6}", array3);
										Debug.Log(message3, this);
										goto IL_10b1;
									}
								}
							}
						}
					}
				}
			}
			throw new IndexOutOfRangeException();
		}
		goto IL_1441;
		IL_10b1:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj19 != null)
			{
				SafeInvoke(onBeforeEach, "onBeforeEach", (GameObject)obj19);
				ExecuteAction((GameObject)obj19);
				SafeInvoke(onAfterEach, "onAfterEach", (GameObject)obj19);
			}
		}
		enumerator.Dispose();
		SafeInvoke(onCompleted, "onCompleted");
		if (runOnce)
		{
			if (verbose)
			{
				int instanceID5 = GetInstanceID();
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				string arg3 = base.name;
				object arg4 = default(object);
				string message4 = $"[{arg4}] MissionAdvanceReaction '{arg3}': Run-once complete. Unsubscribing.";
				Debug.Log(message4, this);
			}
			Unsubscribe();
		}
	}

	private List<GameObject> BuildTargetList()
	{
		//IL_0171: Expected O, but got I4
		//IL_0263: Expected O, but got I4
		List<GameObject> list = new List<GameObject>();
		bool flag;
		bool flag4;
		if (root != null)
		{
			if (targetSelection == TargetSelection.SelfOnly)
			{
				flag = true;
			}
			else
			{
				object obj = targetSelection - 2;
				bool flag2 = obj == null;
				bool flag3 = targetSelection != TargetSelection.ChildrenOnly;
				flag = flag2;
				if (!flag3)
				{
					flag4 = true;
					flag = flag2;
					goto IL_027f;
				}
			}
			object obj2 = targetSelection - 2;
			bool flag5 = obj2 == null;
			flag4 = flag5;
			goto IL_027f;
		}
		if (verbose)
		{
			int instanceID = GetInstanceID();
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			string arg = base.name;
			object arg2 = default(object);
			string message = $"[{arg2}] MissionAdvanceReaction '{arg}': No Root assigned.";
			Debug.LogWarning(message, this);
		}
		goto IL_0231;
		IL_0236:
		return (List<GameObject>)(object)new NullReferenceException();
		IL_0231:
		return list;
		IL_027f:
		if (flag)
		{
			if (list == null)
			{
				goto IL_0236;
			}
			list.Add(root);
		}
		if (flag4)
		{
			if ((object)root == null)
			{
				goto IL_0236;
			}
			Transform rootTransform = root.transform;
			CollectChildren(rootTransform, list);
			if (targetSelection == TargetSelection.ChildrenOnly)
			{
				if (list == null)
				{
					goto IL_0236;
				}
				if (list.Contains(root))
				{
					bool flag6 = list.Remove(root);
					return list;
				}
			}
		}
		goto IL_0231;
	}

	private void CollectChildren(Transform rootTransform, List<GameObject> results)
	{
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_019d: Expected O, but got I4
		//IL_01a6: Expected O, but got I4
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		bool flag = rootTransform == null;
		if (flag)
		{
			return;
		}
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (recursiveChildren == flag)
		{
			int childCount = rootTransform.childCount;
			bool flag2 = childCount <= 0;
			int num = 0;
			if (flag2)
			{
				return;
			}
			do
			{
				Transform child = rootTransform.GetChild(num);
				if (child != null)
				{
					GameObject gameObject = child.gameObject;
					if (includeInactiveChildren || gameObject.activeInHierarchy)
					{
						if (skipChildrenWithReaction)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							if (obj != null)
							{
								goto IL_0145;
							}
						}
						results.Add(gameObject);
					}
				}
				goto IL_0145;
				IL_0145:
				num++;
			}
			while (num < childCount);
			return;
		}
		Transform[] componentsInChildren = rootTransform.GetComponentsInChildren<Transform>(includeInactive: true);
		object obj2 = componentsInChildren + 32;
		object obj3 = 0;
		for (object obj4 = 0; (nint)obj4 < componentsInChildren.Length; obj3++, obj2 += 8, obj4 = obj3)
		{
			if (!((UnityEngine.Object)obj2 != null) || !((UnityEngine.Object)obj2 != rootTransform))
			{
				continue;
			}
			GameObject gameObject2 = ((Component)obj2).gameObject;
			if (!includeInactiveChildren && !gameObject2.activeInHierarchy)
			{
				continue;
			}
			if (skipChildrenWithReaction)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				if (obj != null)
				{
					continue;
				}
			}
			results.Add(gameObject2);
		}
	}

	private void ExecuteAction(GameObject obj)
	{
		//IL_0015: Expected O, but got I4
		bool flag = action == ActionType.None;
		if (!flag)
		{
			object obj2 = action - 1;
			if (!flag)
			{
				if ((nint)obj2 == 1)
				{
					if (verbose)
					{
						int instanceID = GetInstanceID();
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string arg = base.name;
						string arg2 = ((!(obj != null)) ? "<null>" : obj.name);
						object arg3 = default(object);
						string message = $"[{arg3}] '{arg}': Destroying '{arg2}'.";
						Debug.Log(message, obj);
					}
					UnityEngine.Object.Destroy(obj);
				}
			}
			else
			{
				if (verbose)
				{
					int instanceID2 = GetInstanceID();
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string arg4 = base.name;
					string arg5 = ((!(obj != null)) ? "<null>" : obj.name);
					object arg6 = default(object);
					string message2 = $"[{arg6}] '{arg4}': Disabling '{arg5}'.";
					Debug.Log(message2, obj);
				}
				obj.SetActive(value: false);
			}
		}
		else if (verbose)
		{
			int instanceID3 = GetInstanceID();
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			string arg7 = base.name;
			string arg8 = ((!(obj != null)) ? "<null>" : obj.name);
			object arg9 = default(object);
			string message3 = $"[{arg9}] '{arg7}': None action for '{arg8}'.";
			Debug.Log(message3, this);
		}
	}

	private static void SafeInvoke(UnityEvent evt, string label)
	{
		evt?.Invoke();
	}

	private static void SafeInvoke(GameObjectEvent evt, string label, GameObject arg)
	{
		evt?.Invoke(arg);
	}

	private static string SafeName(GameObject go)
	{
		if (go != null)
		{
			if ((object)go != null)
			{
				return go.name;
			}
			return (string)(object)new NullReferenceException();
		}
		return "<null>";
	}

	private void TestTriggerNow()
	{
		if (verbose)
		{
			int instanceID = GetInstanceID();
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			string arg = base.name;
			object arg2 = default(object);
			string message = $"[{arg2}] MissionAdvanceReaction '{arg}': TestTriggerNow invoked.";
			Debug.Log(message, this);
		}
		SafeInvoke(onTriggered, "onTriggered(Test)");
		List<GameObject> list = BuildTargetList();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj != null)
			{
				SafeInvoke(onBeforeEach, "onBeforeEach(Test)", (GameObject)obj);
				ExecuteAction((GameObject)obj);
				SafeInvoke(onAfterEach, "onAfterEach(Test)", (GameObject)obj);
			}
		}
		enumerator.Dispose();
		SafeInvoke(onCompleted, "onCompleted(Test)");
	}
}
