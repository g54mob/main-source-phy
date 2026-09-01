using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ArtilleryReloadController : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CAutoAdvanceToNextStateCoroutine_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ArtilleryReloadController _003C_003E4__this;

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
		public _003CAutoAdvanceToNextStateCoroutine_003Ed__27(int _003C_003E1__state)
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

	[Header("Animators")]
	[Tooltip("Assign Animator components that should receive triggers for reload state transitions.\nAll listed animators will receive the state's 'triggers' when SetState is called.")]
	public List<Animator> animators;

	[Header("Reload States (Editable in Inspector)")]
	[Tooltip("Ordered list of reload states defining the reload flow. Each state must provide a unique 'stateKey'.\nEnsure 'advanceButton' is assigned if the state is advanced via a UI button.")]
	public List<ReloadStateDef> reloadStates;

	[Header("Chambered Shell Logic")]
	[Tooltip("Transform used as the parent for the chambered shell instance.")]
	public Transform chamberSlot;

	[Tooltip("Runtime reference to the currently chambered shell instance (null if none).")]
	public GameObject chamberedShell;

	[Header("Shell Source")]
	[Tooltip("Prefab used to instantiate a shell into the chamber when TryLoadShell is called.")]
	public GameObject shellPrefab;

	[Header("State Info (Debug)")]
	[SerializeField]
	private int currentStateIndex;

	[Header("Cylinder Integration")]
	[Tooltip("Reference to the CylinderShellSelector for shell transfer integration.\nUsed by animation events to move shells between cylinder and transfer/chamber slots.")]
	public CylinderShellSelector cylinderShellSelector;

	[Tooltip("Transform used as a temporary parent when moving a shell from the cylinder to the chamber.")]
	public Transform transferSlot;

	[Tooltip("Runtime reference to the shell currently residing in the transfer slot (null if none).")]
	public GameObject transferShell;

	[Header("Gun Controller Link")]
	[Tooltip("Reference to the gun controller that should be notified when a reload is completed.\nIf the selected state has 'isReloadCompleteState' true, OnReloadingComplete() will be invoked.")]
	public GunController gunController;

	private bool working;

	private readonly Dictionary<LookAtTarget, bool> buttonListenerRegistered;

	public ReloadStateDef CurrentState => null;

	public int CurrentStateIndex => 0;

	[Tooltip("State change event emitted whenever SetState activates a new state.\nSubscribe from UI controllers to avoid per-frame polling.\nSignature: Action<ReloadStateDef>")]
	public event Action<ReloadStateDef> OnStateChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Start()
	{
	}

	private void RegisterAdvanceButtonListeners()
	{
	}

	public void AdvanceState()
	{
	}

	public void RegressState()
	{
	}

	public void SetState(int newIndex, bool force = false)
	{
	}

	public void ForceResetStateToInitial()
	{
	}

	public void ResetAnimators()
	{
	}

	private void UpdateAllAdvanceButtons()
	{
	}

	[IteratorStateMachine(typeof(_003CAutoAdvanceToNextStateCoroutine_003Ed__27))]
	private IEnumerator AutoAdvanceToNextStateCoroutine()
	{
		return null;
	}

	public void GoToState(string stateKey)
	{
	}

	public void OnAnimationEvent_AdvanceState()
	{
	}

	public void OnAnimationEvent_RegressState()
	{
	}

	public void OnUserInput_Advance()
	{
	}

	public void OnUserInput_Regress()
	{
	}

	public void TryLoadShell()
	{
	}

	public bool CanLoadBullet()
	{
		return false;
	}

	public void ReceiveChamberedBullet(GameObject bullet)
	{
	}

	public void EjectChamberedShell()
	{
	}

	public void ClearTransferredShell()
	{
	}

	public void AnimationEvent_MoveShellToTransferSlot()
	{
	}

	public void AnimationEvent_TransferShellToChamber()
	{
	}
}
