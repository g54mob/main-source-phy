using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class PowderChargeController : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass21_0
	{
		public int idx;

		public PowderChargeController _003C_003E4__this;

		internal void _003CAwake_003Eb__0()
		{
			_003C_003E4__this.OnChargeButtonPressed(idx);
		}
	}

	private sealed class _003CEnableLoadChargesAfterDelay_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PowderChargeController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CEnableLoadChargesAfterDelay_003Ed__28(int _003C_003E1__state)
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
			//IL_0097: Expected I4, but got I8
			//IL_018b: Expected I4, but got O
			PowderChargeController powderChargeController = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(powderChargeController.dispensingAnimationTime);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_017d;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_017d;
				}
				if (powderChargeController.isActive && powderChargeController.loadChargesButton != null && powderChargeController.currentSelectedCharges > 0)
				{
					if ((object)powderChargeController.loadChargesButton == null)
					{
						goto IL_017d;
					}
					powderChargeController.loadChargesButton.SetActive(active: true);
				}
				powderChargeController.loadChargeEnableCoroutine = null;
			}
			return false;
			IL_017d:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
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

	private sealed class _003CResetDispensersReverseWithDelay_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PowderChargeController _003C_003E4__this;

		private int _003Ci_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CResetDispensersReverseWithDelay_003Ed__31(int _003C_003E1__state)
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
			//IL_007c: Expected I4, but got I8
			//IL_0259: Expected I4, but got O
			PowderChargeController powderChargeController = _003C_003E4__this;
			int num;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				num = (_003Ci_003E5__2 = powderChargeController.currentSelectedCharges - 1);
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0214;
				}
				num = _003Ci_003E5__2 - 1;
				_003C_003E1__state = -1;
				_003Ci_003E5__2 = num;
			}
			if (num >= 0)
			{
				List<GameObject> chargeDispensers = powderChargeController.chargeDispensers;
				if (_003Ci_003E5__2 < chargeDispensers._size)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					object obj = default(object);
					UnityEngine.Object obj2;
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
						UnityEngine.Object obj3 = default(UnityEngine.Object);
						obj2 = obj3;
					}
					else
					{
						obj2 = null;
					}
					if (obj2 != null && !string.IsNullOrEmpty(powderChargeController.resetTrigger))
					{
						if ((object)obj2 == null)
						{
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						}
						((Animator)obj2).SetTrigger(powderChargeController.resetTrigger);
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string message = $"[DEBUG] Reset trigger sent to dispenser {arg}";
						Debug.Log(message);
					}
				}
				WaitForSeconds waitForSeconds = new WaitForSeconds(powderChargeController.resetDispensersDelay);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
			powderChargeController.resetDispensersCoroutine = null;
			goto IL_0214;
			IL_0214:
			return false;
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

	public List<LookAtTarget> chargeButtons;

	public List<GameObject> chargeDispensers;

	public string dispenseTrigger;

	public string resetTrigger;

	public LookAtTarget loadChargesButton;

	public ArtilleryReloadController reloadController;

	public string chargeSelectionStateKey;

	public string resetDispensersStateKey;

	public Transform chamberSlot;

	public ShellBlueprint debugShellBlueprint;

	public int maxCharges;

	public float dispensingAnimationTime;

	public float resetDispensersDelay;

	public bool autoReactivateOnInventoryRefill;

	public bool disableButtonsWhenInventoryEmpty;

	private int currentSelectedCharges;

	private bool isActive;

	private bool resetTriggeredThisState;

	private Coroutine loadChargeEnableCoroutine;

	private Coroutine resetDispensersCoroutine;

	private bool inventorySubscribed;

	public float DispensedChargesFloat
	{
		get
		{
			//IL_0007: Expected F4, but got I4
			return currentSelectedCharges;
		}
	}

	private void Awake()
	{
		SetAllButtonsActive(active: false);
		if (loadChargesButton != null)
		{
			loadChargesButton.SetActive(active: false);
		}
		List<LookAtTarget> list = chargeButtons;
		int num = 0;
		int num2 = 0;
		UnityEngine.Object obj = default(UnityEngine.Object);
		LookAtTarget lookAtTarget = default(LookAtTarget);
		while (num2 < list._size && num < maxCharges)
		{
			_003C_003Ec__DisplayClass21_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass21_0();
			CS_0024_003C_003E8__locals4._003C_003E4__this = this;
			CS_0024_003C_003E8__locals4.idx = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				UnityAction action = delegate
				{
					CS_0024_003C_003E8__locals4._003C_003E4__this.OnChargeButtonPressed(CS_0024_003C_003E8__locals4.idx);
				};
				lookAtTarget.RegisterOnClickDown(action);
			}
			list = chargeButtons;
			num++;
			num2 = num;
		}
		if (loadChargesButton != null)
		{
			UnityAction action2 = OnLoadChargesPressed;
			loadChargesButton.RegisterOnClickDown(action2);
		}
		TrySubscribeInventory();
	}

	private void OnEnable()
	{
		TrySubscribeInventory();
	}

	private void OnDisable()
	{
		//IL_00fb: Expected O, but got I
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_0060: Expected I4, but got O
		if (!inventorySubscribed)
		{
			return;
		}
		UnityEngine.Object obj = PowderChargeInventory._003CInstance_003Ek__BackingField;
		if (PowderChargeInventory._003CInstance_003Ek__BackingField != null)
		{
			Action<int> value = OnInventoryChargesChanged;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rdi_v5 (UnityEngine.Object)+50]");
			Delegate obj2 = (Delegate)0;
			object obj3 = PowderChargeInventory._003CInstance_003Ek__BackingField + 80;
			IntPtr intPtr = default(IntPtr);
			bool flag;
			Delegate obj5 = default(Delegate);
			do
			{
				Delegate obj4 = Delegate.Remove(obj2, value);
				if ((object)obj4 != null)
				{
					((PowderChargeController)(object)obj4).OnInventoryChargesChanged((int)typeof(Action<int>));
					if ((object)obj4 == null)
					{
						((PowderChargeController)(object)PowderChargeInventory._003CInstance_003Ek__BackingField).OnInventoryChargesChanged((int)(nint)intPtr);
						return;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				flag = (object)obj5 != obj2;
				obj2 = obj5;
			}
			while (flag);
		}
		inventorySubscribed = false;
	}

	private void Update()
	{
		TrySubscribeInventory();
		if (reloadController != null && chamberSlot == null)
		{
			ArtilleryReloadController artilleryReloadController = reloadController;
			chamberSlot = artilleryReloadController.chamberSlot;
		}
		if (!(reloadController != null))
		{
			return;
		}
		ReloadStateDef currentState = reloadController.CurrentState;
		if (currentState == null)
		{
			return;
		}
		ReloadStateDef currentState2 = reloadController.CurrentState;
		bool flag = currentState2.stateKey == chargeSelectionStateKey;
		if (flag != isActive)
		{
			isActive = flag;
			if (!flag)
			{
				EndChargeSelection();
			}
			else
			{
				SetAllButtonsActive(active: false);
				if (loadChargesButton != null)
				{
					loadChargesButton.SetActive(active: false);
				}
				currentSelectedCharges = 0;
				if (loadChargeEnableCoroutine != null)
				{
					StopCoroutine(loadChargeEnableCoroutine);
					loadChargeEnableCoroutine = null;
				}
				if (resetDispensersCoroutine != null)
				{
					StopCoroutine(resetDispensersCoroutine);
					resetDispensersCoroutine = null;
				}
				ApplyInventoryAvailabilityToUI();
				if (loadChargesButton != null)
				{
					loadChargesButton.SetActive(active: false);
				}
			}
		}
		ReloadStateDef currentState3 = reloadController.CurrentState;
		if (currentState3.stateKey == resetDispensersStateKey && !resetTriggeredThisState)
		{
			if (resetDispensersCoroutine != null)
			{
				StopCoroutine(resetDispensersCoroutine);
				resetDispensersCoroutine = null;
			}
			_003CResetDispensersReverseWithDelay_003Ed__31 obj = new _003CResetDispensersReverseWithDelay_003Ed__31(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
			resetDispensersCoroutine = coroutine;
			resetTriggeredThisState = true;
		}
		else
		{
			ReloadStateDef currentState4 = reloadController.CurrentState;
			if (currentState4.stateKey != resetDispensersStateKey)
			{
				resetTriggeredThisState = false;
			}
		}
	}

	private void BeginChargeSelection()
	{
		SetAllButtonsActive(active: false);
		if (loadChargesButton != null)
		{
			loadChargesButton.SetActive(active: false);
		}
		currentSelectedCharges = 0;
		if (loadChargeEnableCoroutine != null)
		{
			StopCoroutine(loadChargeEnableCoroutine);
			loadChargeEnableCoroutine = null;
		}
		if (resetDispensersCoroutine != null)
		{
			StopCoroutine(resetDispensersCoroutine);
			resetDispensersCoroutine = null;
		}
		ApplyInventoryAvailabilityToUI();
		if (loadChargesButton != null)
		{
			loadChargesButton.SetActive(active: false);
		}
	}

	private void EndChargeSelection()
	{
		SetAllButtonsActive(active: false);
		if (loadChargesButton != null)
		{
			loadChargesButton.SetActive(active: false);
		}
		if (loadChargeEnableCoroutine != null)
		{
			StopCoroutine(loadChargeEnableCoroutine);
			loadChargeEnableCoroutine = null;
		}
	}

	private void OnChargeButtonPressed(int index)
	{
		//IL_0114: Expected O, but got I4
		//IL_0166: Expected O, but got I4
		//IL_0184: Expected O, but got I
		//IL_0196: Expected O, but got I4
		//IL_0299: Expected O, but got I4
		//IL_0397: Expected O, but got I4
		if (!isActive || index != currentSelectedCharges)
		{
			return;
		}
		if (PowderChargeInventory._003CInstance_003Ek__BackingField != null)
		{
			PowderChargeInventory powderChargeInventory = PowderChargeInventory._003CInstance_003Ek__BackingField;
			if (powderChargeInventory._currentCharges > 0)
			{
				int num = powderChargeInventory._currentCharges - 1;
				bool flag = powderChargeInventory._currentCharges == num;
				object obj = 0;
				if (!flag)
				{
					Action<int> onChargesChanged = powderChargeInventory.OnChargesChanged;
					powderChargeInventory._currentCharges = num;
					powderChargeInventory.currentChargesForInspector = num;
					bool flag2 = powderChargeInventory.OnChargesChanged == null;
					obj = 0;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v547 @ rcx_v53 (System.Action`1<System.Int32>)+28]");
						obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v547 @ rcx_v53 (System.Action`1<System.Int32>)+18] (should have been resolved before IL gen)");
						UnityEngine.Object obj2 = (UnityEngine.Object)num;
					}
					((powderChargeInventory._currentCharges <= 0) ? powderChargeInventory.onInventoryEmpty : ((powderChargeInventory._currentCharges <= 6) ? powderChargeInventory.onSixOrLessRemaining : powderChargeInventory.onMoreThanSixRemaining))?.Invoke();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"Powder charge used. Remaining: {arg}";
				Debug.Log(message);
				if (chargeDispensers != null)
				{
					List<GameObject> list = chargeDispensers;
					if (index < list._size)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if ((UnityEngine.Object)num != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							UnityEngine.Object obj3 = default(UnityEngine.Object);
							if (obj3 != null && !string.IsNullOrEmpty(dispenseTrigger))
							{
								((Animator)obj3).SetTrigger(dispenseTrigger);
							}
						}
					}
				}
				if (index >= 0)
				{
					List<LookAtTarget> list2 = chargeButtons;
					if (index < list2._size)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if ((UnityEngine.Object)num != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							LookAtTarget lookAtTarget = default(LookAtTarget);
							lookAtTarget.SetActive(active: false);
						}
					}
				}
				int num2 = currentSelectedCharges + 1;
				currentSelectedCharges = num2;
				ApplyInventoryAvailabilityToUI();
				if (loadChargesButton != null)
				{
					loadChargesButton.SetActive(active: false);
					if (loadChargeEnableCoroutine != null)
					{
						StopCoroutine(loadChargeEnableCoroutine);
						loadChargeEnableCoroutine = null;
					}
					if (currentSelectedCharges > 0)
					{
						_003CEnableLoadChargesAfterDelay_003Ed__28 obj4 = new _003CEnableLoadChargesAfterDelay_003Ed__28(0);
						obj4._003C_003E1__state = 0;
						obj4._003C_003E4__this = this;
						Coroutine coroutine = StartCoroutine(obj4);
						loadChargeEnableCoroutine = coroutine;
					}
				}
				return;
			}
			Debug.LogWarning("Attempted to use a powder charge, but none are left!");
		}
		Debug.LogWarning("OnChargeButtonPressed: Failed to use charge. Inventory is likely empty.");
		ApplyInventoryAvailabilityToUI();
		if (currentSelectedCharges > 0 && loadChargesButton != null)
		{
			loadChargesButton.SetActive(active: true);
		}
	}

	private IEnumerator EnableLoadChargesAfterDelay()
	{
		_003CEnableLoadChargesAfterDelay_003Ed__28 obj = new _003CEnableLoadChargesAfterDelay_003Ed__28(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void OnLoadChargesPressed()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		string message = $"PowderChargeController: Attempting to set powder charge to {arg}";
		Debug.Log(message);
		object message2;
		if (!(debugShellBlueprint != null))
		{
			int num = currentSelectedCharges;
			message2 = "[DEBUG] debugShellBlueprint is not assigned in inspector.";
		}
		else
		{
			bool flag = debugShellBlueprint.SetPowderCharge(currentSelectedCharges);
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = "SUCCESS";
			if (!flag)
			{
				arg2 = "UNCHANGED";
			}
			ShellBlueprint shellBlueprint = debugShellBlueprint;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg3 = default(object);
			object arg4 = default(object);
			string text = $"[DEBUG] SetPowderCharge({arg3}) on debugShellBlueprint: {arg2} (Current: {arg4})";
			int currentPowderCharge = shellBlueprint.currentPowderCharge;
			int num = currentSelectedCharges;
			message2 = text;
		}
		Debug.Log(message2);
		ShellBlueprint currentChamberedShellBlueprint = GetCurrentChamberedShellBlueprint();
		if (!(currentChamberedShellBlueprint != null))
		{
			Debug.LogWarning("PowderChargeController: No shell with ShellBlueprint in chamber slot to load charges into!");
		}
		else
		{
			bool flag2 = currentChamberedShellBlueprint.SetPowderCharge(currentSelectedCharges);
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg5 = "SUCCESS";
			if (!flag2)
			{
				arg5 = "UNCHANGED";
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg6 = default(object);
			object arg7 = default(object);
			string message3 = $"SetPowderCharge({arg6}) on shell in chamber: {arg5} (Current: {arg7})";
			Debug.Log(message3);
		}
		EndChargeSelection();
	}

	public void ResetAllUsedDispensers()
	{
		if (resetDispensersCoroutine != null)
		{
			StopCoroutine(resetDispensersCoroutine);
			resetDispensersCoroutine = null;
		}
		_003CResetDispensersReverseWithDelay_003Ed__31 obj = new _003CResetDispensersReverseWithDelay_003Ed__31(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
		resetDispensersCoroutine = coroutine;
	}

	private IEnumerator ResetDispensersReverseWithDelay()
	{
		_003CResetDispensersReverseWithDelay_003Ed__31 obj = new _003CResetDispensersReverseWithDelay_003Ed__31(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private ShellBlueprint GetCurrentChamberedShellBlueprint()
	{
		object message;
		if (chamberSlot != null)
		{
			if ((object)chamberSlot != null)
			{
				if (chamberSlot.childCount == 0)
				{
					message = "[DEBUG] chamberSlot has no children.";
					goto IL_017a;
				}
				if ((object)chamberSlot != null)
				{
					Transform child = chamberSlot.GetChild(0);
					if (!(child != null))
					{
						message = "[DEBUG] chamberSlot.GetChild(0) returned null.";
						goto IL_017a;
					}
					if ((object)child != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						UnityEngine.Object obj = default(UnityEngine.Object);
						if (obj == null)
						{
							Debug.LogWarning("[DEBUG] No ShellBlueprint found on child of chamberSlot.");
						}
						return (ShellBlueprint)obj;
					}
				}
			}
			return (ShellBlueprint)(object)new NullReferenceException();
		}
		message = "[DEBUG] chamberSlot is null.";
		goto IL_017a;
		IL_017a:
		Debug.LogWarning(message);
		return null;
	}

	private void SetAllButtonsActive(bool active)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<LookAtTarget>.Enumerator enumerator = default(List<LookAtTarget>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if ((object)obj == null)
					{
						break;
					}
					((LookAtTarget)obj).SetActive(active);
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public void ResetAll()
	{
		SetAllButtonsActive(active: false);
		if (loadChargesButton != null)
		{
			loadChargesButton.SetActive(active: false);
		}
		currentSelectedCharges = 0;
		if (loadChargeEnableCoroutine != null)
		{
			StopCoroutine(loadChargeEnableCoroutine);
			loadChargeEnableCoroutine = null;
		}
		if (resetDispensersCoroutine != null)
		{
			StopCoroutine(resetDispensersCoroutine);
			resetDispensersCoroutine = null;
		}
	}

	private void TrySubscribeInventory()
	{
		//IL_0140: Expected O, but got I
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_00a1: Expected I4, but got O
		if ((!autoReactivateOnInventoryRefill && !disableButtonsWhenInventoryEmpty) || inventorySubscribed)
		{
			return;
		}
		UnityEngine.Object obj = PowderChargeInventory._003CInstance_003Ek__BackingField;
		if (!(PowderChargeInventory._003CInstance_003Ek__BackingField != null))
		{
			return;
		}
		Action<int> b = OnInventoryChargesChanged;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rdi_v5 (UnityEngine.Object)+50]");
		Delegate obj2 = (Delegate)0;
		object obj3 = PowderChargeInventory._003CInstance_003Ek__BackingField + 80;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj4 = Delegate.Combine(obj2, b);
			if ((object)obj4 != null)
			{
				((PowderChargeController)(object)obj4).OnInventoryChargesChanged((int)typeof(Action<int>));
				if ((object)obj4 == null)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag = (object)obj5 != obj2;
			obj2 = obj5;
			if (!flag)
			{
				inventorySubscribed = true;
				return;
			}
		}
		IntPtr intPtr = default(IntPtr);
		((PowderChargeController)(object)PowderChargeInventory._003CInstance_003Ek__BackingField).OnInventoryChargesChanged((int)(nint)intPtr);
	}

	private void UnsubscribeInventory()
	{
		//IL_00fb: Expected O, but got I
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_0060: Expected I4, but got O
		if (!inventorySubscribed)
		{
			return;
		}
		UnityEngine.Object obj = PowderChargeInventory._003CInstance_003Ek__BackingField;
		if (PowderChargeInventory._003CInstance_003Ek__BackingField != null)
		{
			Action<int> value = OnInventoryChargesChanged;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rdi_v5 (UnityEngine.Object)+50]");
			Delegate obj2 = (Delegate)0;
			object obj3 = PowderChargeInventory._003CInstance_003Ek__BackingField + 80;
			IntPtr intPtr = default(IntPtr);
			bool flag;
			Delegate obj5 = default(Delegate);
			do
			{
				Delegate obj4 = Delegate.Remove(obj2, value);
				if ((object)obj4 != null)
				{
					((PowderChargeController)(object)obj4).OnInventoryChargesChanged((int)typeof(Action<int>));
					if ((object)obj4 == null)
					{
						((PowderChargeController)(object)PowderChargeInventory._003CInstance_003Ek__BackingField).OnInventoryChargesChanged((int)(nint)intPtr);
						return;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				flag = (object)obj5 != obj2;
				obj2 = obj5;
			}
			while (flag);
		}
		inventorySubscribed = false;
	}

	private void OnInventoryChargesChanged(int newCount)
	{
		if (!isActive)
		{
			return;
		}
		if (newCount > 0)
		{
			if (autoReactivateOnInventoryRefill)
			{
				ApplyInventoryAvailabilityToUI();
			}
		}
		else if (disableButtonsWhenInventoryEmpty)
		{
			SetAllButtonsActive(active: false);
			if (loadChargesButton != null && currentSelectedCharges == 0)
			{
				loadChargesButton.SetActive(active: false);
			}
		}
	}

	private void ApplyInventoryAvailabilityToUI()
	{
		if (!isActive)
		{
			return;
		}
		UnityEngine.Object obj = PowderChargeInventory._003CInstance_003Ek__BackingField;
		if (PowderChargeInventory._003CInstance_003Ek__BackingField != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ rdi_v2 (UnityEngine.Object)+58]");
			if ((nint)0 > (nint)0)
			{
				if (currentSelectedCharges < 0 || currentSelectedCharges >= maxCharges)
				{
					return;
				}
				List<LookAtTarget> list = chargeButtons;
				if (currentSelectedCharges < list._size)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UnityEngine.Object obj2 = default(UnityEngine.Object);
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						LookAtTarget lookAtTarget = default(LookAtTarget);
						lookAtTarget.SetActive(active: true);
					}
				}
				return;
			}
		}
		if (disableButtonsWhenInventoryEmpty)
		{
			SetAllButtonsActive(active: false);
			if (loadChargesButton != null && currentSelectedCharges == 0)
			{
				loadChargesButton.SetActive(active: false);
			}
		}
	}

	private void ActivateNextChargeButtonIfValid()
	{
		if (currentSelectedCharges < 0 || currentSelectedCharges >= maxCharges)
		{
			return;
		}
		List<LookAtTarget> list = chargeButtons;
		if (currentSelectedCharges < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				LookAtTarget lookAtTarget = default(LookAtTarget);
				lookAtTarget.SetActive(active: true);
			}
		}
	}

	public PowderChargeController()
	{
		List<LookAtTarget> list = new List<LookAtTarget>(6);
		chargeButtons = list;
		chargeDispensers = new List<GameObject>(6);
		dispenseTrigger = "Dispense";
		resetTrigger = "Reset";
		chargeSelectionStateKey = "SelectPowderCharge";
		resetDispensersStateKey = "DispensersReset";
		maxCharges = 6;
		dispensingAnimationTime = 1f;
		resetDispensersDelay = 0.3f;
		autoReactivateOnInventoryRefill = true;
		base._002Ector();
	}
}
