using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class InteractionLockBrokerDisableWhileLockedBridge : MonoBehaviour
{
	public enum LockConditionMode
	{
		FreezePlayerController
	}

	[Serializable]
	public class TargetEntry
	{
		public GameObject Target;

		public bool RestoreOriginalStateOnUnlock = true;

		[NonSerialized]
		public bool HasCapturedOriginal;

		[NonSerialized]
		public bool OriginalActive;
	}

	private InteractionLockBroker broker;

	private string brokerTag = "LockBroker";

	private bool retryResolveIfMissing = true;

	private float retryResolveIntervalSeconds = 0.5f;

	private LockConditionMode lockCondition;

	private List<TargetEntry> targets;

	private bool logStateChanges;

	private bool forceReapplyEveryUpdate;

	private bool _hasAnyBroker;

	private bool _lastLocked;

	private bool _initializedOriginalStates;

	private float _nextResolveAttemptTime;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC45]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		CaptureOriginalStatesIfNeeded();
		ResolveBrokerIfNeeded(force: true);
		ApplyIfNeeded("Awake", forceApply: true);
	}

	private void OnEnable()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC46]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		CaptureOriginalStatesIfNeeded();
		ResolveBrokerIfNeeded(force: false);
		ApplyIfNeeded("OnEnable", forceApply: true);
	}

	private void Update()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC47]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_hasAnyBroker)
		{
			ApplyIfNeeded("Update", forceReapplyEveryUpdate);
		}
		else
		{
			if (!retryResolveIfMissing)
			{
				return;
			}
			float unscaledTime = Time.unscaledTime;
			if (!(_nextResolveAttemptTime > unscaledTime))
			{
				float unscaledTime2 = Time.unscaledTime;
				bool flag = !(0.05f < retryResolveIntervalSeconds);
				float num = 0.05f;
				if (!flag)
				{
					num = retryResolveIntervalSeconds;
				}
				float nextResolveAttemptTime = num + unscaledTime2;
				_nextResolveAttemptTime = nextResolveAttemptTime;
				ResolveBrokerIfNeeded(force: false);
				if (_hasAnyBroker)
				{
					ApplyIfNeeded("Resolve(Update)", forceApply: true);
				}
			}
		}
	}

	public void RecaptureOriginalStates()
	{
		_initializedOriginalStates = false;
		CaptureOriginalStatesIfNeeded();
	}

	public void ForceRefresh(string reason = "ForceRefresh")
	{
		ResolveBrokerIfNeeded(force: true);
		ApplyIfNeeded(reason, forceApply: true);
	}

	private void CaptureOriginalStatesIfNeeded()
	{
		//IL_0023: Expected O, but got I4
		//IL_002c: Expected O, but got I4
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_006f: Expected O, but got I
		//IL_0092: Expected O, but got I
		if (_initializedOriginalStates)
		{
			return;
		}
		List<TargetEntry> list = targets;
		_initializedOriginalStates = true;
		object obj = 0;
		object obj2 = 0;
		object obj3 = default(object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ stack_8_v3+10]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ stack_8_v3+10]");
					bool activeSelf = ((GameObject)0).activeSelf;
					_ = 1;
				}
				else
				{
					_ = 0;
				}
			}
			list = targets;
			obj++;
			obj2 = obj;
		}
	}

	private void ResolveBrokerIfNeeded(bool force)
	{
		if ((!force && !(broker == null)) || !(broker == null))
		{
			_hasAnyBroker = true;
			return;
		}
		InteractionLockBroker interactionLockBroker = InteractionLockBroker.FindOrNull(brokerTag);
		broker = interactionLockBroker;
		if ((_hasAnyBroker = broker != null) && logStateChanges)
		{
			string text = broker.name;
			string message = "[LockBrokerDisableBridge] Resolved broker by tag '" + brokerTag + "': " + text;
			Debug.Log(message, this);
		}
	}

	private bool GetLockedStateFromBroker()
	{
		//IL_010b: Expected I4, but got O
		if (broker != null && lockCondition == LockConditionMode.FreezePlayerController)
		{
			InteractionLockBroker interactionLockBroker = broker;
			if ((object)broker != null && interactionLockBroker._requests != null)
			{
				int count = interactionLockBroker._requests.Count;
				int num = count ^ count;
				int num2 = count & num;
				bool flag = num2 < 0;
				bool flag2 = count < 0;
				bool flag3 = count == 0;
				bool flag4 = flag2 == flag;
				bool flag5 = !flag3;
				return flag5 & flag4;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private unsafe void ApplyIfNeeded(string reason, bool forceApply)
	{
		//IL_0151: Expected O, but got I4
		//IL_015a: Expected O, but got I4
		//IL_0163: Expected O, but got I4
		//IL_0189: Expected O, but got Ref
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_01ae: Expected O, but got I
		//IL_01bb: Expected O, but got I4
		//IL_027c: Expected O, but got I
		//IL_0365: Expected O, but got I4
		//IL_0259: Expected O, but got I
		//IL_022f: Expected O, but got I
		if (broker != null)
		{
			bool flag6;
			if (broker != null && lockCondition == LockConditionMode.FreezePlayerController)
			{
				InteractionLockBroker interactionLockBroker = broker;
				int count = interactionLockBroker._requests.Count;
				int num = count ^ count;
				int num2 = count & num;
				bool flag = num2 < 0;
				bool flag2 = count < 0;
				bool flag3 = count == 0;
				bool flag4 = flag2 == flag;
				bool flag5 = !flag3;
				flag6 = flag5 & flag4;
			}
			else
			{
				flag6 = false;
			}
			if (!forceApply && flag6 == _lastLocked)
			{
				return;
			}
			List<TargetEntry> list = targets;
			_lastLocked = flag6;
			IntPtr intPtr = default(IntPtr);
			nint num3 = intPtr;
			object obj = 0;
			object obj2 = 0;
			object obj4 = default(object);
			for (object obj3 = 0; (nint)obj2 < list._size; list = targets, obj3++, num3 = 0, obj2 = obj3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool flag7 = obj4 == null;
				obj = (object)(&obj4);
				if (flag7)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_8+10]");
				bool flag8 = (UnityEngine.Object)0 != null;
				obj = 0;
				if (!flag8)
				{
					continue;
				}
				GameObject gameObject;
				bool active;
				if (!flag6)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_8+18]");
					if ((nint)0 != (flag6 ? 1 : 0))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_8+19]");
						if ((nint)0 != (flag6 ? 1 : 0))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_8+10]");
							gameObject = (GameObject)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_8+1A]");
							active = false;
							goto IL_034f;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_8+10]");
					gameObject = (GameObject)0;
					active = true;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_8+10]");
					gameObject = (GameObject)0;
					active = false;
				}
				goto IL_034f;
				IL_034f:
				gameObject.SetActive(active);
				obj = 0;
			}
			if (logStateChanges)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				string message = $"[LockBrokerDisableBridge] Apply ({reason}) locked={arg} targets={arg2}";
				Debug.Log(message, this);
			}
		}
		else
		{
			_hasAnyBroker = false;
		}
	}

	public InteractionLockBrokerDisableWhileLockedBridge()
	{
		List<TargetEntry> list = new List<TargetEntry>(8);
		targets = list;
		base._002Ector();
	}
}
