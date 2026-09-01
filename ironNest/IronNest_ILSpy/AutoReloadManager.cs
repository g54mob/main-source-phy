using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoReloadManager : MonoBehaviour
{
	private sealed class _003CAutoReloadFlow_003Ed__37 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AutoReloadManager _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoReloadFlow_003Ed__37(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0012: Expected O, but got I8
			//IL_002c: Expected O, but got I8
			while (true)
			{
				int num = _003C_003E1__state;
				if (_003C_003E1__state > 7)
				{
					break;
				}
				object obj = 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdx_v1+56A118+v31 @ rax_v2 (System.Int32)*4]");
				object obj2 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v54 @ rcx_v3 (should have been resolved before IL gen)");
			}
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

	private sealed class _003CPerformPowderSelection_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AutoReloadManager _003C_003E4__this;

		private float _003CstartTime_003E5__2;

		private int _003CtargetCharges_003E5__3;

		private float _003CnextChargePressTime_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CPerformPowderSelection_003Ed__38(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0012: Expected O, but got I8
			//IL_002c: Expected O, but got I8
			while (true)
			{
				int num = _003C_003E1__state;
				if (_003C_003E1__state > 6)
				{
					break;
				}
				object obj = 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rdx_v1+56AEA4+v31 @ rax_v2 (System.Int32)*4]");
				object obj2 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v58 @ rcx_v3 (should have been resolved before IL gen)");
			}
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

	private sealed class _003CPressButton_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LookAtTarget button;

		public AutoReloadManager _003C_003E4__this;

		public float waitAfter;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CPressButton_003Ed__41(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_00f1: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_0066: Expected I4, but got I8
			//IL_0220: Expected I4, but got O
			//IL_009b: Invalid comparison between F4 and I4
			//IL_0052: Expected I4, but got I8
			AutoReloadManager autoReloadManager = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						_003C_003E1__state = -1;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)button == null)
					{
						goto IL_0212;
					}
					button.OnClickUp();
					if (waitAfter > 0f)
					{
						WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(waitAfter);
						_003C_003E2__current = waitForSecondsRealtime;
						_003C_003E1__state = 2;
						return true;
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if (button != null)
				{
					if ((object)button != null)
					{
						int instanceID = button.GetInstanceID();
						if ((object)_003C_003E4__this != null && autoReloadManager.pressedButtonIdsThisState != null)
						{
							object obj2 = default(object);
							autoReloadManager.pressedButtonIdsThisState.Add((int)(&obj2));
							float unscaledTime = Time.unscaledTime;
							autoReloadManager.lastButtonPressTime = unscaledTime;
							if ((object)button != null)
							{
								button.OnClickDown();
								WaitForSecondsRealtime waitForSecondsRealtime2 = new WaitForSecondsRealtime(autoReloadManager.clickCycleDelay);
								_003C_003E2__current = waitForSecondsRealtime2;
								_003C_003E1__state = 1;
								return true;
							}
						}
					}
					goto IL_0212;
				}
			}
			return false;
			IL_0212:
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

	private ArtilleryReloadController reloadController;

	private CylinderShellSelector cylinderSelector;

	private PowderChargeController powderController;

	private GunController gunController;

	private InputActionReference toggleAutoReloadAction;

	private bool autoReloadEnabled;

	private int desiredPowderCharges;

	private float clickCycleDelay;

	private float postActionDelay;

	private float rotationSettleDelay;

	private float autoAdvanceBridgeDelay;

	private float powderSelectionTimeout;

	private bool startOnGunFired;

	private float chargeButtonCadenceSeconds;

	private bool isAutoReloading;

	private string currentAction;

	private int observedStateIndex;

	private List<int> pressedButtonIdsThisState;

	private bool cylinderRotatedThisCycle;

	private float lastButtonPressTime;

	private bool powderSelectionCompleted;

	private Coroutine autoReloadRoutine;

	private bool lastAutoReloadEnabled;

	private void Awake()
	{
		lastAutoReloadEnabled = autoReloadEnabled;
		HookInputAction(subscribe: true);
	}

	private void OnEnable()
	{
		HookInputAction(subscribe: true);
		if (autoReloadEnabled && !isAutoReloading)
		{
			StartAutoReload();
		}
	}

	private void OnDisable()
	{
		HookInputAction(subscribe: false);
		StopAutoReloadInternal();
	}

	private void OnDestroy()
	{
		HookInputAction(subscribe: false);
	}

	private void Start()
	{
		if (autoReloadEnabled && !isAutoReloading)
		{
			StartAutoReload();
		}
	}

	private void Update()
	{
		if (autoReloadEnabled != lastAutoReloadEnabled)
		{
			if (!autoReloadEnabled)
			{
				StopAutoReload();
			}
			else
			{
				StartAutoReload();
			}
			lastAutoReloadEnabled = autoReloadEnabled;
		}
		if (!(reloadController != null))
		{
			return;
		}
		ReloadStateDef currentState = reloadController.CurrentState;
		if (currentState != null)
		{
			ArtilleryReloadController artilleryReloadController = reloadController;
			if (artilleryReloadController.currentStateIndex != observedStateIndex)
			{
				observedStateIndex = artilleryReloadController.currentStateIndex;
				pressedButtonIdsThisState.Clear();
				cylinderRotatedThisCycle = false;
				powderSelectionCompleted = false;
				ReloadStateDef currentState2 = reloadController.CurrentState;
				string text = "Entered state '" + currentState2.stateKey + "'";
				currentAction = text;
			}
		}
	}

	private void HookInputAction(bool subscribe)
	{
		if (!(toggleAutoReloadAction != null))
		{
			return;
		}
		InputAction action = toggleAutoReloadAction.action;
		if (!subscribe)
		{
			Action<InputAction.CallbackContext> value = OnToggleAutoReloadAction;
			action.performed -= value;
			return;
		}
		Action<InputAction.CallbackContext> value2 = OnToggleAutoReloadAction;
		action.performed += value2;
		InputAction action2 = toggleAutoReloadAction.action;
		if (!action2.enabled)
		{
			InputAction action3 = toggleAutoReloadAction.action;
			action3.Enable();
		}
	}

	private void OnToggleAutoReloadAction(InputAction.CallbackContext ctx)
	{
		bool flag = !autoReloadEnabled;
		if (autoReloadEnabled != flag)
		{
			autoReloadEnabled = flag;
			lastAutoReloadEnabled = flag;
			if (~(autoReloadEnabled ? 1u : 0u) == 0)
			{
				StopAutoReload();
			}
			else
			{
				StartAutoReload();
			}
		}
	}

	public void ToggleAutoReload()
	{
		bool flag = !autoReloadEnabled;
		if (autoReloadEnabled != flag)
		{
			autoReloadEnabled = flag;
			lastAutoReloadEnabled = flag;
			if (~(autoReloadEnabled ? 1u : 0u) == 0)
			{
				StopAutoReload();
			}
			else
			{
				StartAutoReload();
			}
		}
	}

	public void SetAutoReload(bool enabled)
	{
		if (autoReloadEnabled != enabled)
		{
			autoReloadEnabled = enabled;
			lastAutoReloadEnabled = enabled;
			if (!enabled)
			{
				StopAutoReload();
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 29 Invalid \"Jump target not found in method: 0x180558DE0\"");
			}
		}
	}

	public void StartAutoReload()
	{
		if (!isAutoReloading && ValidateReferences())
		{
			currentAction = "Starting auto reload";
			pressedButtonIdsThisState.Clear();
			cylinderRotatedThisCycle = false;
			powderSelectionCompleted = false;
			_003CAutoReloadFlow_003Ed__37 obj = new _003CAutoReloadFlow_003Ed__37(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
			autoReloadRoutine = coroutine;
			isAutoReloading = true;
			Debug.Log("[AutoReloadManager] Auto-reload started.");
		}
	}

	public void StopAutoReload()
	{
		if (isAutoReloading)
		{
			StopAutoReloadInternal();
			Debug.Log("[AutoReloadManager] Auto-reload stopped.");
		}
	}

	private void StopAutoReloadInternal()
	{
		//IL_00c7: Expected O, but got I
		if (autoReloadRoutine != null)
		{
			StopCoroutine(autoReloadRoutine);
			autoReloadRoutine = null;
		}
		isAutoReloading = false;
		currentAction = "Idle";
		List<int> list = pressedButtonIdsThisState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdi_v1 (System.Collections.Generic.List`1<System.Int32>)+1C]");
		_ = (nint)0 + (nint)1;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<int>())
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdi_v1 (System.Collections.Generic.List`1<System.Int32>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdi_v1 (System.Collections.Generic.List`1<System.Int32>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rdi_v1 (System.Collections.Generic.List`1<System.Int32>)+18]");
				Array.Clear((Array)num, 0, 0);
			}
		}
		cylinderRotatedThisCycle = false;
		powderSelectionCompleted = false;
	}

	public void OnGunFired()
	{
		if (startOnGunFired && autoReloadEnabled && !isAutoReloading)
		{
			Debug.Log("[AutoReloadManager] Gun fired event received, initiating auto reload.");
			StartAutoReload();
		}
	}

	private IEnumerator AutoReloadFlow()
	{
		_003CAutoReloadFlow_003Ed__37 obj = new _003CAutoReloadFlow_003Ed__37(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator PerformPowderSelection()
	{
		_003CPerformPowderSelection_003Ed__38 obj = new _003CPerformPowderSelection_003Ed__38(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private int CountSelectedCharges()
	{
		//IL_0206: Expected I4, but got O
		if (!(powderController != null))
		{
			goto IL_01ea;
		}
		PowderChargeController powderChargeController = powderController;
		if ((object)powderController != null)
		{
			if (powderChargeController.chargeButtons == null)
			{
				goto IL_01ea;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (true)
			{
				List<LookAtTarget> chargeButtons = powderChargeController.chargeButtons;
				if (powderChargeController.chargeButtons == null)
				{
					break;
				}
				if (num3 < chargeButtons._size)
				{
					PowderChargeController powderChargeController2 = powderController;
					if ((object)powderController == null || powderChargeController2.chargeButtons == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj != null)
					{
						if ((object)obj == null)
						{
							break;
						}
						GameObject gameObject = ((Component)obj).gameObject;
						if ((object)gameObject == null)
						{
							break;
						}
						if (gameObject.activeSelf)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ stack_8_v5 (UnityEngine.Object)+A0]");
							if ((nint)0 != 0)
							{
								goto IL_01dd;
							}
						}
						num2++;
					}
					powderChargeController = powderController;
					num++;
					if ((object)powderController == null)
					{
						break;
					}
					num3 = num;
					continue;
				}
				goto IL_01dd;
				IL_01dd:
				return num2;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
		IL_01ea:
		return 0;
	}

	private LookAtTarget GetChargeButtonAtIndex(int index)
	{
		if (!(powderController != null) || index < 0)
		{
			goto IL_00cb;
		}
		PowderChargeController powderChargeController = powderController;
		if ((object)powderController != null)
		{
			List<LookAtTarget> chargeButtons = powderChargeController.chargeButtons;
			if (powderChargeController.chargeButtons != null)
			{
				if (index < chargeButtons._size)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					LookAtTarget result = default(LookAtTarget);
					return result;
				}
				goto IL_00cb;
			}
		}
		return (LookAtTarget)(object)new NullReferenceException();
		IL_00cb:
		return null;
	}

	private IEnumerator PressButton(LookAtTarget button, float waitAfter)
	{
		_003CPressButton_003Ed__41 obj = new _003CPressButton_003Ed__41(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.button = button;
		obj.waitAfter = waitAfter;
		return obj;
	}

	private unsafe bool CanPress(LookAtTarget button)
	{
		//IL_0153: Expected I4, but got O
		if (!isAutoReloading || !(button != null))
		{
			goto IL_013f;
		}
		if ((object)button != null)
		{
			GameObject gameObject = button.gameObject;
			if ((object)gameObject != null)
			{
				if (gameObject.activeSelf && button.isActive)
				{
					int instanceID = button.GetInstanceID();
					if (pressedButtonIdsThisState == null)
					{
						goto IL_0145;
					}
					object obj = default(object);
					if (!pressedButtonIdsThisState.Contains((int)(&obj)))
					{
						float unscaledTime = Time.unscaledTime;
						float num = unscaledTime - lastButtonPressTime;
						if (!(0.05f > num))
						{
							return true;
						}
					}
				}
				goto IL_013f;
			}
		}
		goto IL_0145;
		IL_0145:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_013f:
		return false;
	}

	private bool CylinderSlotAHasShell()
	{
		//IL_00d3: Expected I4, but got O
		if (cylinderSelector != null)
		{
			CylinderShellSelector cylinderShellSelector = cylinderSelector;
			if ((object)cylinderSelector == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			if (cylinderShellSelector.bullets != null)
			{
				List<GameObject> bullets = cylinderShellSelector.bullets;
				if (bullets._size != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UnityEngine.Object obj = default(UnityEngine.Object);
					return obj != null;
				}
			}
		}
		return false;
	}

	private bool ValidateReferences()
	{
		object message;
		if (reloadController != null)
		{
			if (!(cylinderSelector == null))
			{
				return true;
			}
			message = "[AutoReloadManager] Missing CylinderShellSelector reference.";
		}
		else
		{
			message = "[AutoReloadManager] Missing ArtilleryReloadController reference.";
		}
		Debug.LogError(message);
		return false;
	}

	public AutoReloadManager()
	{
		//IL_008d: Expected I4, but got I8
		desiredPowderCharges = 3;
		clickCycleDelay = 0.1f;
		postActionDelay = 0.35f;
		rotationSettleDelay = 1.5f;
		autoAdvanceBridgeDelay = 0.2f;
		powderSelectionTimeout = 15f;
		startOnGunFired = true;
		chargeButtonCadenceSeconds = 0.35f;
		currentAction = "Idle";
		observedStateIndex = -1;
		List<int> list = new List<int>();
		pressedButtonIdsThisState = list;
		base._002Ector();
	}
}
