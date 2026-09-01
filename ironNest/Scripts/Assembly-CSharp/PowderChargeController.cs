using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowderChargeController : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CEnableLoadChargesAfterDelay_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PowderChargeController _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CEnableLoadChargesAfterDelay_003Ed__28(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CResetDispensersReverseWithDelay_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PowderChargeController _003C_003E4__this;

		private int _003Ci_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CResetDispensersReverseWithDelay_003Ed__31(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[Header("Charge Dispenser Buttons (Order Matters: 1-6)")]
	[Tooltip("Charge selection buttons in order of dispensing (1..6). Only the next valid button is enabled at a time.\nThese are typically LookAtTarget components that drive gaze/click interaction.")]
	public List<LookAtTarget> chargeButtons;

	[Header("Charge Dispensers (Order Matters: 1-6)")]
	[Tooltip("Physical dispenser objects aligned with the charge buttons (1..6).\nEach should have an Animator with 'Dispense'/'Reset' triggers.")]
	public List<GameObject> chargeDispensers;

	[Header("Animation Trigger Names")]
	[Tooltip("Animator trigger name for dispensing a charge.\nExample: \"Dispense\"")]
	public string dispenseTrigger;

	[Tooltip("Animator trigger name for resetting to idle.\nExample: \"Reset\"")]
	public string resetTrigger;

	[Header("Load Charges Button")]
	[Tooltip("Button to confirm and load the currently selected number of charges into the chambered shell.\nOnly enabled after at least one charge is dispensed.")]
	public LookAtTarget loadChargesButton;

	[Header("Reload Integration")]
	[Tooltip("Reload controller that drives when the charge selection UI is enabled.")]
	public ArtilleryReloadController reloadController;

	[Tooltip("Reload state key that enables charge selection.\nExample: \"SelectPowderCharge\"")]
	public string chargeSelectionStateKey;

	[Tooltip("Reload state key that triggers dispenser reset in reverse order.\nExample: \"DispensersReset\"")]
	public string resetDispensersStateKey;

	[Header("Chamber Slot (Assign from ArtilleryReloadController)")]
	[Tooltip("Transform that holds the current shell in the chamber.\nThe first child is expected to carry a ShellBlueprint component.")]
	public Transform chamberSlot;

	[Header("Debug/Testing")]
	[Tooltip("Optional: A ShellBlueprint instance for testing without a chambered shell.\nIf assigned, it will be updated alongside the chambered shell.")]
	public ShellBlueprint debugShellBlueprint;

	[Header("Designer Tuning")]
	[Tooltip("Maximum number of powder charges selectable for a single shell.\nValid range: 1–6.")]
	[Range(1f, 6f)]
	public int maxCharges;

	[Header("Dispensing Animation Timer")]
	[Tooltip("Seconds to wait after dispensing a charge before re-enabling the Load Charges button.\nExample: 1.0")]
	public float dispensingAnimationTime;

	[Header("Reset Animation Delay")]
	[Tooltip("Delay in seconds between triggering reset on each dispenser (reverse order).\nExample: 0.3")]
	public float resetDispensersDelay;

	[Header("Inventory Integration")]
	[Tooltip("When true, the controller listens for PowderChargeInventory changes and:\n- If selection is active and charges become available (> 0), automatically re-enables the next valid dispense button.\nThis fixes the case where selection got stuck after temporarily running out.")]
	public bool autoReactivateOnInventoryRefill;

	[Tooltip("When true, and while selection is active, the controller will forcibly deactivate ALL dispense buttons and the Load button\nas soon as the shared PowderChargeInventory reaches 0.\nThis prevents the player from interacting with a still-active LookAtTarget on a second gun after the first gun consumed the last charge.\nSafe default: enabled.")]
	public bool disableButtonsWhenInventoryEmpty;

	private int currentSelectedCharges;

	private bool isActive;

	private bool resetTriggeredThisState;

	private Coroutine loadChargeEnableCoroutine;

	private Coroutine resetDispensersCoroutine;

	private bool inventorySubscribed;

	public float DispensedChargesFloat => 0f;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void BeginChargeSelection()
	{
	}

	private void EndChargeSelection()
	{
	}

	private void OnChargeButtonPressed(int index)
	{
	}

	[IteratorStateMachine(typeof(_003CEnableLoadChargesAfterDelay_003Ed__28))]
	private IEnumerator EnableLoadChargesAfterDelay()
	{
		return null;
	}

	private void OnLoadChargesPressed()
	{
	}

	public void ResetAllUsedDispensers()
	{
	}

	[IteratorStateMachine(typeof(_003CResetDispensersReverseWithDelay_003Ed__31))]
	private IEnumerator ResetDispensersReverseWithDelay()
	{
		return null;
	}

	private ShellBlueprint GetCurrentChamberedShellBlueprint()
	{
		return null;
	}

	private void SetAllButtonsActive(bool active)
	{
	}

	public void ResetAll()
	{
	}

	private void TrySubscribeInventory()
	{
	}

	private void UnsubscribeInventory()
	{
	}

	private void OnInventoryChargesChanged(int newCount)
	{
	}

	private void ApplyInventoryAvailabilityToUI()
	{
	}

	private void ActivateNextChargeButtonIfValid()
	{
	}
}
