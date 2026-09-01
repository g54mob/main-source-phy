using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ItemSlot))]
[RequireComponent(typeof(BoxCollider))]
public class RequisitionSlot : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CCR_DestroyConsoleAfterDelay_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public RequisitionSlot _003C_003E4__this;

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
		public _003CCR_DestroyConsoleAfterDelay_003Ed__40(int _003C_003E1__state)
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
	private sealed class _003CCR_RunPunchcardGraph_003Ed__46 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RequisitionSlot _003C_003E4__this;

		private PunchcardGraph _003CnewGraph_003E5__2;

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
		public _003CCR_RunPunchcardGraph_003Ed__46(int _003C_003E1__state)
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
	private sealed class _003CCR_SpendRequisitionPoints_003Ed__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public RequisitionSlot _003C_003E4__this;

		public int cost;

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
		public _003CCR_SpendRequisitionPoints_003Ed__45(int _003C_003E1__state)
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

	[Header("Current Card (Read Only)")]
	[Tooltip("The PunchcardRuntime currently held in the slot. Derived from the ItemSlot's CurrentItem.")]
	public PunchcardRuntime CurrentCard;

	[Tooltip("The instantiated console control prefab for the current card.")]
	public GameObject CurrentCardConsole;

	[Header("External References")]
	[Tooltip("MissionStatsTracker used to read and spend Requisition Points.")]
	public MissionStatsTracker statsTracker;

	[Tooltip("LookAtTarget component for the lever that triggers requisition. Registered as a click callback automatically.")]
	public LookAtTarget lever;

	[Tooltip("Animator on the slot object, driven by HasCard, Scan, Requisition, and RequisitionFail parameters.")]
	public Animator slotAnimator;

	[Tooltip("Transform that console control prefabs are instantiated under when a card is placed.")]
	public Transform Transform_ConsoleAnchor;

	[Header("Animator Parameter Names")]
	[Tooltip("Animator bool parameter name for the 'has card' state.")]
	public string hasCardBoolParam;

	[Tooltip("Animator trigger parameter name fired on card insert (scan animation).")]
	public string scanTriggerParam;

	[Tooltip("Animator trigger parameter name fired when requisition is attempted.")]
	public string requisitionTriggerParam;

	[Tooltip("Animator trigger parameter name fired when requisition fails (gates or insufficient points).")]
	public string requisitionFailTriggerParam;

	[Header("Behavior Flags")]
	[Tooltip("If true, the scan animation trigger fires automatically when a card is placed.")]
	public bool autoScanOnInsert;

	[Header("Redemption Handling")]
	[Tooltip("If true, the slot is cleared after a successful requisition.")]
	public bool clearSlotAfterRedemption;

	[Header("Animation Timings")]
	[Tooltip("Delay (seconds) before the console control prefab is destroyed after the card is removed normally (not during a swap). During a card swap the outgoing console is always destroyed immediately.")]
	public float CardConsoleDestroyDelay;

	[Tooltip("Minimum time (seconds) between requisition attempts. Prevents double-firing.")]
	public float RedemptionCooldown;

	[Tooltip("Delay (seconds) between triggering the requisition animation and actually spending the points.")]
	public float PointsDeductionDelay;

	[Tooltip("Delay (seconds) passed to PunchcardRuntime.OnCardUsed — gives the destroy animation time to play.")]
	public float CardConsumedDelay;

	[Header("Failure Handling")]
	[Tooltip("If true, fires the RequisitionFail animator trigger when a requirement gate blocks the requisition.")]
	public bool fireFailTriggerOnRedemptionFail;

	[Tooltip("If true, fires the Requisition animator trigger even when the player has insufficient points (for visual feedback).")]
	public bool fireRequisitionTriggerOnInsufficientPoints;

	[Header("Events")]
	[Tooltip("Fired ONLY on a fully successful requisition (gates passed, points spent, rewards applied).")]
	public UnityEvent onCardRequisitioned;

	[Tooltip("Fired when a requisition attempt is rejected because the cooldown has not elapsed.")]
	public UnityEvent onCooldownRejection;

	[Tooltip("Fired when a requisition attempt is rejected conditions are not met.")]
	public UnityEvent onRejection;

	[Header("Debug")]
	[Tooltip("Log requisition state changes to the Console.")]
	public bool debugLogs;

	private ItemSlot itemSlot;

	private bool leverCallbackRegistered;

	private int hasCardBoolHash;

	private int scanTriggerHash;

	private int requisitionTriggerHash;

	private int requisitionFailTriggerHash;

	private float lastRedemptionTime;

	private GameObject pendingConsoleDestroy;

	private Coroutine pendingConsoleDestroyCoroutine;

	public bool HasCard => false;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Start()
	{
	}

	private void OnItemAdded(GameObject itemGO)
	{
	}

	private void OnItemRemoved(GameObject itemGO)
	{
	}

	private void ImmediatelyDestroyPendingConsole()
	{
	}

	[IteratorStateMachine(typeof(_003CCR_DestroyConsoleAfterDelay_003Ed__40))]
	private IEnumerator CR_DestroyConsoleAfterDelay(float delay)
	{
		return null;
	}

	public void PlaceCard(PunchcardRuntime card)
	{
	}

	public void RemoveCard(PunchcardRuntime card, bool autoEject = false)
	{
	}

	public void ClearSlot()
	{
	}

	public void AttemptRequisition()
	{
	}

	[IteratorStateMachine(typeof(_003CCR_SpendRequisitionPoints_003Ed__45))]
	private IEnumerator CR_SpendRequisitionPoints(float delay, int cost)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CCR_RunPunchcardGraph_003Ed__46))]
	private IEnumerator CR_RunPunchcardGraph()
	{
		return null;
	}

	private void FireScan()
	{
	}

	private void FireRequisitionTrigger(bool success)
	{
	}

	private void FireFailAnimation()
	{
	}

	private void UpdateVisualAndLeverState()
	{
	}

	private void RegisterLeverCallback()
	{
	}

	private void CacheAnimatorHashes()
	{
	}
}
