using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class LookAtTargetUnlockSequence5 : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public int captured;

		public LookAtTargetUnlockSequence5 _003C_003E4__this;

		internal void _003CEnsureSubscribed_003Eb__0()
		{
			_003C_003E4__this.HandleSlotClicked(captured);
		}

		internal void _003CEnsureSubscribed_003Eb__1()
		{
			_003C_003E4__this.HandleSlotClicked(captured);
		}
	}

	private LookAtTarget slot1;

	private LookAtTarget slot2;

	private LookAtTarget slot3;

	private LookAtTarget slot4;

	private LookAtTarget slot5;

	private int startUnlockedSlotCount = 1;

	private bool unlockNextOnClick = true;

	private bool advanceOnClickUp;

	private bool allowBackClicksToUnlockNext;

	private bool trackToggleState;

	private bool clearTogglesOnReset = true;

	private bool initializeOnAwake;

	private bool resetClickStateWhenLocking = true;

	private UnityEvent onUnlockedChanged;

	private UnityEvent onFullyUnlocked;

	private bool debugLogs;

	private int debugUnlockedSlotCount;

	private int debugToggledOnCount;

	private LookAtTarget[] _slots;

	private bool[] _toggledOn;

	private int _unlockedCount;

	private bool _subscribed;

	public float ToggledOnCount
	{
		get
		{
			//IL_000f: Expected F4, but got I4
			int toggledOnCountInt = GetToggledOnCountInt();
			return toggledOnCountInt;
		}
	}

	public int ToggledOnCountInt => GetToggledOnCountInt();

	private void Awake()
	{
		//IL_011b: Expected O, but got I4
		//IL_0132: Expected O, but got I4
		//IL_009d: Expected O, but got I4
		//IL_00a6: Expected O, but got I4
		//IL_00af: Expected O, but got I4
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		EnsureInitialized();
		EnsureSubscribed();
		if (initializeOnAwake)
		{
			EnsureInitialized();
			SetUnlockedSlotCount(startUnlockedSlotCount);
			if (trackToggleState && clearTogglesOnReset)
			{
				bool[] toggledOn = _toggledOn;
				object obj = 0;
				object obj2 = 0;
				object obj3 = 0;
				while ((nint)obj < toggledOn.Length)
				{
					bool[] toggledOn2 = _toggledOn;
					_ = 0;
					obj2++;
					toggledOn = _toggledOn;
					obj3++;
					obj = obj2;
				}
			}
		}
		bool flag = _unlockedCount == 0;
		object obj4 = 72;
		if (!flag)
		{
			obj4 = 136;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rax_v5+this @ rcx (LookAtTargetUnlockSequence5)]");
		int num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rax_v5+this @ rcx (LookAtTargetUnlockSequence5)]");
		if ((nint)0 >= (nint)1)
		{
			if (num > 5)
			{
				num = 5;
			}
		}
		else
		{
			num = 1;
		}
		debugUnlockedSlotCount = num;
		int toggledOnCountInt = GetToggledOnCountInt();
		debugToggledOnCount = toggledOnCountInt;
	}

	private void EnsureInitialized()
	{
		//IL_0068: Expected I, but got O
		//IL_00ca: Expected I, but got O
		//IL_00da: Expected O, but got I
		//IL_0146: Expected I, but got O
		//IL_0156: Expected O, but got I
		//IL_01c2: Expected I, but got O
		//IL_01d2: Expected O, but got I
		//IL_023e: Expected I, but got O
		//IL_024e: Expected O, but got I
		if (_slots != null)
		{
			LookAtTarget[] slots = _slots;
			if (slots.Length == 5)
			{
				goto IL_02a2;
			}
		}
		LookAtTarget[] array = new LookAtTarget[5];
		if ((object)slot1 != null)
		{
			nint num = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj = default(object);
			if (obj == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				LookAtTarget lookAtTarget = default(LookAtTarget);
				throw lookAtTarget;
			}
		}
		array[0] = slot1;
		if ((object)slot2 != null)
		{
			nint num2 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v341 @ rdx_v34 (Il2CppClass<LookAtTarget[]>)+40]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj3 = default(object);
			bool flag = obj3 == null;
			LookAtTarget lookAtTarget2 = slot2;
			if (flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				LookAtTarget lookAtTarget3 = default(LookAtTarget);
				throw lookAtTarget3;
			}
		}
		array[1] = slot2;
		if ((object)slot3 != null)
		{
			nint num3 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rdx_v32 (Il2CppClass<LookAtTarget[]>)+40]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj5 = default(object);
			bool flag2 = obj5 == null;
			LookAtTarget lookAtTarget4 = slot3;
			if (flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				LookAtTarget lookAtTarget5 = default(LookAtTarget);
				throw lookAtTarget5;
			}
		}
		array[2] = slot3;
		if ((object)slot4 != null)
		{
			nint num4 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v457 @ rdx_v30 (Il2CppClass<LookAtTarget[]>)+40]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj7 = default(object);
			bool flag3 = obj7 == null;
			LookAtTarget lookAtTarget6 = slot4;
			if (flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				LookAtTarget lookAtTarget7 = default(LookAtTarget);
				throw lookAtTarget7;
			}
		}
		array[3] = slot4;
		if ((object)slot5 != null)
		{
			nint num5 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v515 @ rdx_v28 (Il2CppClass<LookAtTarget[]>)+40]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj9 = default(object);
			bool flag4 = obj9 == null;
			LookAtTarget lookAtTarget8 = slot5;
			if (flag4)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj10 = default(object);
				throw obj10;
			}
		}
		array[4] = slot5;
		_slots = array;
		goto IL_02a2;
		IL_02a2:
		if (_toggledOn != null)
		{
			bool[] toggledOn = _toggledOn;
			if (toggledOn.Length == 5)
			{
				return;
			}
		}
		bool[] toggledOn2 = new bool[5];
		_toggledOn = toggledOn2;
	}

	private void EnsureSubscribed()
	{
		//IL_0023: Expected O, but got I4
		//IL_0086: Expected O, but got I
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_011d: Expected O, but got I
		//IL_00e8: Expected O, but got I
		if (_subscribed)
		{
			return;
		}
		EnsureInitialized();
		LookAtTarget[] slots = _slots;
		object obj = 32;
		int num = 0;
		for (int num2 = 0; num2 < slots.Length; num2 = num)
		{
			_003C_003Ec__DisplayClass28_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass28_0();
			CS_0024_003C_003E8__locals6._003C_003E4__this = this;
			CS_0024_003C_003E8__locals6.captured = num;
			LookAtTarget[] slots2 = _slots;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r14_v5+v188 @ rbp_v6 (LookAtTarget[])]");
			bool flag = (UnityEngine.Object)0 == null;
			if (!flag)
			{
				if (advanceOnClickUp == flag)
				{
					UnityAction action = delegate
					{
						CS_0024_003C_003E8__locals6._003C_003E4__this.HandleSlotClicked(CS_0024_003C_003E8__locals6.captured);
					};
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r14_v5+v188 @ rbp_v6 (LookAtTarget[])]");
					((LookAtTarget)0).RegisterOnClickDown(action);
				}
				else
				{
					UnityAction action2 = delegate
					{
						CS_0024_003C_003E8__locals6._003C_003E4__this.HandleSlotClicked(CS_0024_003C_003E8__locals6.captured);
					};
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r14_v5+v188 @ rbp_v6 (LookAtTarget[])]");
					((LookAtTarget)0).RegisterOnClickUp(action2);
				}
			}
			slots = _slots;
			num++;
			obj += 8;
		}
		_subscribed = true;
	}

	public void ResetSequence()
	{
		//IL_00eb: Expected O, but got I4
		//IL_0102: Expected O, but got I4
		//IL_006d: Expected O, but got I4
		//IL_0076: Expected O, but got I4
		//IL_007f: Expected O, but got I4
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		EnsureInitialized();
		SetUnlockedSlotCount(startUnlockedSlotCount);
		if (trackToggleState && clearTogglesOnReset)
		{
			bool[] toggledOn = _toggledOn;
			object obj = 0;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj < toggledOn.Length)
			{
				bool[] toggledOn2 = _toggledOn;
				_ = 0;
				obj2++;
				toggledOn = _toggledOn;
				obj3++;
				obj = obj2;
			}
		}
		bool flag = _unlockedCount == 0;
		object obj4 = 72;
		if (!flag)
		{
			obj4 = 136;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rax_v5+this @ rcx (LookAtTargetUnlockSequence5)]");
		int num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rax_v5+this @ rcx (LookAtTargetUnlockSequence5)]");
		if ((nint)0 >= (nint)1)
		{
			if (num > 5)
			{
				num = 5;
			}
		}
		else
		{
			num = 1;
		}
		debugUnlockedSlotCount = num;
		int toggledOnCountInt = GetToggledOnCountInt();
		debugToggledOnCount = toggledOnCountInt;
	}

	public void LockToFirstOnly()
	{
		//IL_0030: Expected O, but got I4
		//IL_0047: Expected O, but got I4
		EnsureInitialized();
		SetUnlockedSlotCount(1);
		bool flag = _unlockedCount == 0;
		object obj = 72;
		if (!flag)
		{
			obj = 136;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v4+this @ rcx (LookAtTargetUnlockSequence5)]");
		int num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v4+this @ rcx (LookAtTargetUnlockSequence5)]");
		if ((nint)0 >= (nint)1)
		{
			if (num > 5)
			{
				num = 5;
			}
		}
		else
		{
			num = 1;
		}
		debugUnlockedSlotCount = num;
		int toggledOnCountInt = GetToggledOnCountInt();
		debugToggledOnCount = toggledOnCountInt;
	}

	public void UnlockAll()
	{
		//IL_0030: Expected O, but got I4
		//IL_0047: Expected O, but got I4
		EnsureInitialized();
		SetUnlockedSlotCount(5);
		bool flag = _unlockedCount == 0;
		object obj = 72;
		if (!flag)
		{
			obj = 136;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v4+this @ rcx (LookAtTargetUnlockSequence5)]");
		int num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v4+this @ rcx (LookAtTargetUnlockSequence5)]");
		if ((nint)0 >= (nint)1)
		{
			if (num > 5)
			{
				num = 5;
			}
		}
		else
		{
			num = 1;
		}
		debugUnlockedSlotCount = num;
		int toggledOnCountInt = GetToggledOnCountInt();
		debugToggledOnCount = toggledOnCountInt;
	}

	public void SetUnlockedSlotCount(int unlockedSlotCount)
	{
		//IL_03d9: Expected O, but got I4
		//IL_03e2: Expected O, but got I4
		//IL_03eb: Expected O, but got I4
		//IL_0060: Expected O, but got I
		//IL_006d: Expected O, but got I4
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected I4, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected I4, but got Unknown
		//IL_0144: Expected O, but got I
		//IL_014d: Expected O, but got I4
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_012a: Expected O, but got I
		//IL_0260: Expected O, but got I4
		//IL_028a: Expected O, but got I4
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Expected I4, but got Unknown
		//IL_02ef: Expected O, but got I4
		//IL_0355: Expected O, but got I4
		//IL_036c: Expected O, but got I4
		EnsureInitialized();
		int num;
		if (unlockedSlotCount >= 1)
		{
			bool flag = unlockedSlotCount <= 5;
			num = unlockedSlotCount;
			if (!flag)
			{
				num = 5;
			}
		}
		else
		{
			num = 1;
		}
		object obj = 32;
		object obj2 = 0;
		object obj3 = 0;
		do
		{
			LookAtTarget[] slots = _slots;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r14_v2+v132 @ rbx_v2 (LookAtTarget[])]");
			bool flag2 = (UnityEngine.Object)0 != null;
			object obj4 = 0;
			if (flag2)
			{
				object obj5 = obj3 - num;
				int num2 = obj3 ^ num;
				object obj6 = obj3 ^ obj5;
				int num3 = num2 & obj6;
				bool flag3 = num3 < 0;
				bool flag4 = (nint)obj5 < 0;
				bool active = flag4 != flag3;
				if ((nint)obj2 >= num && resetClickStateWhenLocking)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r14_v2+v132 @ rbx_v2 (LookAtTarget[])]");
					((LookAtTarget)0).ResetButton();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r14_v2+v132 @ rbx_v2 (LookAtTarget[])]");
				((LookAtTarget)0).SetActive(active);
				obj4 = 0;
			}
			obj3++;
			obj2++;
			obj += 8;
		}
		while ((nint)obj < 72);
		_unlockedCount = num;
		if (debugLogs)
		{
			string arg = base.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			string message = $"[{arg}] Unlocked slots = {arg2}/5";
			Debug.Log(message);
		}
		if (_unlockedCount != num && onUnlockedChanged != null)
		{
			onUnlockedChanged.Invoke();
		}
		object obj7 = _unlockedCount - num;
		bool flag5 = obj7 == null;
		bool flag6 = !flag5;
		object obj8 = _unlockedCount - 5;
		int num4 = _unlockedCount ^ 5;
		int num5 = _unlockedCount ^ obj8;
		int num6 = num4 & num5;
		bool flag7 = num6 < 0;
		bool flag8 = (nint)obj8 < 0;
		bool flag9 = flag8 == flag7;
		object obj9 = flag9 & flag6;
		if (obj9 != null && onFullyUnlocked != null)
		{
			onFullyUnlocked.Invoke();
		}
		bool flag10 = _unlockedCount == 0;
		object obj10 = 72;
		if (!flag10)
		{
			obj10 = 136;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v528 @ rax_v17+this @ rcx (LookAtTargetUnlockSequence5)]");
		int num7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v528 @ rax_v17+this @ rcx (LookAtTargetUnlockSequence5)]");
		if ((nint)0 >= (nint)1)
		{
			if (num7 > 5)
			{
				num7 = 5;
			}
		}
		else
		{
			num7 = 1;
		}
		debugUnlockedSlotCount = num7;
		int toggledOnCountInt = GetToggledOnCountInt();
		debugToggledOnCount = toggledOnCountInt;
	}

	public int GetUnlockedSlotCount()
	{
		//IL_001a: Expected O, but got I4
		//IL_0031: Expected O, but got I4
		bool flag = _unlockedCount == 0;
		object obj = 72;
		if (!flag)
		{
			obj = 136;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2+this @ rcx (LookAtTargetUnlockSequence5)]");
		int num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2+this @ rcx (LookAtTargetUnlockSequence5)]");
		if ((nint)0 >= (nint)1)
		{
			if (num > 5)
			{
				return 5;
			}
		}
		else
		{
			num = 1;
		}
		return num;
	}

	public int GetToggledOnCountInt()
	{
		//IL_00f9: Expected I4, but got O
		EnsureInitialized();
		if (trackToggleState)
		{
			bool[] toggledOn = _toggledOn;
			if (_toggledOn != null)
			{
				bool[] toggledOn2 = _toggledOn;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				while (num2 < toggledOn.Length)
				{
					num3++;
					int num5 = num4 + 1;
					int num6 = num + 1;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rcx_v3 (System.Int32)+20+v35 @ r10_v2 (System.Boolean[])]");
					if ((nint)0 == 0)
					{
						num5 = num4;
					}
					num = num6;
					num2 = num3;
					num4 = num5;
				}
				return num4;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return 0;
	}

	private void HandleSlotClicked(int slotIndex0Based)
	{
		//IL_0020: Expected O, but got I4
		//IL_0205: Expected O, but got I
		//IL_0073: Expected O, but got I4
		//IL_0037: Expected O, but got I4
		//IL_0065: Expected O, but got I4
		//IL_00dc: Expected O, but got I4
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_00f3: Expected O, but got I4
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected I4, but got Unknown
		EnsureInitialized();
		bool flag = _unlockedCount == 0;
		object obj = 72;
		if (!flag)
		{
			obj = 136;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v25 @ rax_v3+this @ rcx (LookAtTargetUnlockSequence5)]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v25 @ rax_v3+this @ rcx (LookAtTargetUnlockSequence5)]");
		if ((nint)0 >= (nint)1)
		{
			if ((nint)obj2 > 5)
			{
				obj2 = 5;
			}
		}
		else
		{
			obj2 = 1;
		}
		if (slotIndex0Based >= (nint)obj2)
		{
			return;
		}
		if (trackToggleState)
		{
			bool[] toggledOn = _toggledOn;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [slotIndex0Based @ rdx (System.Int32)+20+v149 @ r8_v4 (System.Boolean[])]");
			bool flag2 = (nint)0 == 0;
			bool flag3 = _unlockedCount == 0;
			object obj3 = 72;
			if (!flag3)
			{
				obj3 = 136;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rdx_v11+this @ rcx (LookAtTargetUnlockSequence5)]");
			int num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rdx_v11+this @ rcx (LookAtTargetUnlockSequence5)]");
			if ((nint)0 >= (nint)1)
			{
				if (num > 5)
				{
					num = 5;
				}
			}
			else
			{
				num = 1;
			}
			debugUnlockedSlotCount = num;
			int toggledOnCountInt = GetToggledOnCountInt();
			debugToggledOnCount = toggledOnCountInt;
		}
		if (!unlockNextOnClick)
		{
			return;
		}
		int num2;
		if (!allowBackClicksToUnlockNext)
		{
			object obj4 = obj2 - 1;
			if (slotIndex0Based != (nint)obj4)
			{
				return;
			}
			num2 = obj2 + 1;
		}
		else
		{
			num2 = slotIndex0Based + 2;
		}
		if (num2 >= 1)
		{
			if (num2 > 5)
			{
				num2 = 5;
			}
		}
		else
		{
			num2 = 1;
		}
		if (num2 > (nint)obj2)
		{
			SetUnlockedSlotCount(num2);
		}
	}

	private void RefreshDebugValues()
	{
		//IL_001a: Expected O, but got I4
		//IL_0031: Expected O, but got I4
		bool flag = _unlockedCount == 0;
		object obj = 72;
		if (!flag)
		{
			obj = 136;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rax_v2+this @ rcx (LookAtTargetUnlockSequence5)]");
		int num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rax_v2+this @ rcx (LookAtTargetUnlockSequence5)]");
		if ((nint)0 >= (nint)1)
		{
			if (num > 5)
			{
				num = 5;
			}
		}
		else
		{
			num = 1;
		}
		debugUnlockedSlotCount = num;
		int toggledOnCountInt = GetToggledOnCountInt();
		debugToggledOnCount = toggledOnCountInt;
	}
}
