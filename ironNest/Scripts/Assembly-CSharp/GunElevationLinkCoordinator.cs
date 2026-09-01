using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Gameplay/Gun Elevation Link Coordinator")]
public class GunElevationLinkCoordinator : MonoBehaviour
{
	public enum InitialSyncOnLink
	{
		None = 0,
		UseGunA = 1,
		UseGunB = 2,
		Average = 3
	}

	private enum DragLeader
	{
		None = 0,
		SliderA = 1,
		SliderB = 2,
		DialA = 3,
		DialB = 4
	}

	[CompilerGenerated]
	private sealed class _003CInitialSyncCoroutine_003Ed__46 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GunElevationLinkCoordinator _003C_003E4__this;

		public GunElevationSliderBinding followerSliderBinding;

		public GunController followerGun;

		public float duration;

		public float targetDeg;

		private float _003Cstart_003E5__2;

		private float _003Ct_003E5__3;

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
		public _003CInitialSyncCoroutine_003Ed__46(int _003C_003E1__state)
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

	[Header("Guns (Required)")]
	[Tooltip("Primary GunController (Gun A) to be linked.\nRequired for linking logic. Must reference the GunController that owns the elevation state, reload state, and firing logic.")]
	[SerializeField]
	private GunController gunA;

	[Tooltip("Secondary GunController (Gun B) to be linked.\nRequired for linking logic. Must reference the other GunController that will be synchronized.")]
	[SerializeField]
	private GunController gunB;

	[Header("Bindings (Recommended)")]
	[Tooltip("Gun A's GunElevationSliderBinding.\n- Used to detect user dragging.\n- Used to cancel active drags on the follower.\n- Used to update the follower's interactive slider visually WITHOUT firing callbacks.")]
	[SerializeField]
	private GunElevationSliderBinding sliderBindingA;

	[Tooltip("Gun B's GunElevationSliderBinding.\n- Used to detect user dragging.\n- Used to cancel active drags on the follower.\n- Used to update the follower's interactive slider visually WITHOUT firing callbacks.")]
	[SerializeField]
	private GunElevationSliderBinding sliderBindingB;

	[Tooltip("Gun A's DialInteractable used by GunElevationDialBinding.\n- Used only to detect (and optionally end) dial drags while linked, to avoid input fights.")]
	[SerializeField]
	private DialInteractable dialA;

	[Tooltip("Gun B's DialInteractable used by GunElevationDialBinding.\n- Used only to detect (and optionally end) dial drags while linked, to avoid input fights.")]
	[SerializeField]
	private DialInteractable dialB;

	[Header("Link Toggle Inputs")]
	[Tooltip("Optional LookAtTarget button that toggles linking on click down.\nThe coordinator will call ToggleLinked() when this button is pressed.")]
	[SerializeField]
	private LookAtTarget linkToggleButton;

	[Tooltip("Optional Input Action used to toggle link state.\n- Action type: Button or Value.\n- On 'performed', the coordinator toggles linking.\nNote: No hardcoded keys are used; bind this in your Input Actions asset.")]
	[SerializeField]
	private InputActionReference toggleLinkAction;

	[Header("Animator Feedback (Optional)")]
	[Tooltip("Optional Animator that will receive a bool parameter to reflect link state (true = linked).\nUse to drive visuals/indicators for the linked state.")]
	[SerializeField]
	private Animator linkedStateAnimator;

	[Tooltip("Animator bool parameter name used to indicate link state.\nThe coordinator sets this parameter to true when linked, false when unlinked.\nDefault: \"IsLinked\".")]
	[SerializeField]
	private string linkedStateBoolParam;

	[Header("Eligibility & Safety")]
	[Tooltip("If true, attempts to turn ON linking are ignored unless BOTH guns are:\n- Loaded (have a chambered shell) and\n- Not reloading and not depressing to reload (GunController.CanFire == true).\nThis gate is evaluated when toggling link ON. While linked, normal auto-unlink still applies when a gun fires.\nRecommended: true.")]
	[SerializeField]
	private bool requireBothGunsLoadedToLink;

	[Tooltip("If true, any gun firing (GunController.OnGunFired) will immediately unlink the pair.\nRecommended: true.")]
	[SerializeField]
	private bool unlinkOnAnyGunFired;

	[Tooltip("If true, when a leader drag is detected while linked, any active follower slider or dial drags are cleanly ended to avoid input fights.\nRecommended: true.")]
	[SerializeField]
	private bool endFollowerDragsWhenLinked;

	[Header("Link Start State")]
	[Tooltip("If true, the two guns start in the linked state when this component is enabled.\nIf 'Require Both Guns Loaded To Link' is true, and the guns are not both eligible at startup, linking will be skipped.")]
	[SerializeField]
	private bool startLinked;

	[Tooltip("When linking turns ON, how to synchronize Desired Elevation initially:\n- None: Keep both as-is (they sync only when you start dragging).\n- UseGunA: Copy Gun A's Desired to Gun B.\n- UseGunB: Copy Gun B's Desired to Gun A.\n- Average: Set both to the average of their current Desired.\nRecommended: UseGunA.")]
	[SerializeField]
	private InitialSyncOnLink initialSyncOnLink;

	[Header("Smoothing")]
	[Tooltip("Live follow smoothing time (seconds) while linked and the leader is being dragged.\n- 0 = instant follow (no smoothing).\n- Uses SmoothDamp toward the leader's current Desired each frame.\n- Time source respects 'Use Unscaled Time For Smoothing'.\nExample: 0.5 yields a responsive yet smooth follower.")]
	[SerializeField]
	[Min(0f)]
	private float liveFollowSmoothTimeSeconds;

	[Tooltip("Initial sync smoothing time (seconds) applied when linking turns ON according to 'Initial Sync On Link'.\n- 0 = instant snap to the initial target.\n- The follower(s) will animate to the target using the easing curve below.\n- Time source respects 'Use Unscaled Time For Smoothing'.\nExample: 0.5 for a quick ease to alignment.")]
	[SerializeField]
	[Min(0f)]
	private float initialSyncSmoothTimeSeconds;

	[Tooltip("Easing curve for the initial sync smoothing when link turns ON.\nX: normalized time (0..1). Y: eased interpolation factor (0..1). Default: Ease-In-Out.")]
	[SerializeField]
	private AnimationCurve initialSyncEaseCurve;

	[Tooltip("If true, uses Time.unscaledDeltaTime for all smoothing (recommended for UI-driven animation).\nIf false, uses Time.deltaTime.")]
	[SerializeField]
	private bool useUnscaledTimeForSmoothing;

	[SerializeField]
	[Tooltip("Current link state. True while guns are locked together. Can be toggled at runtime via ToggleLinked().")]
	private bool isLinked;

	private bool prevSliderADrag;

	private bool prevSliderBDrag;

	private bool prevDialADrag;

	private bool prevDialBDrag;

	private float tSliderAStart;

	private float tSliderBStart;

	private float tDialAStart;

	private float tDialBStart;

	private float followerVelAtoB;

	private float followerVelBtoA;

	private Coroutine initialSyncRoutineAtoB;

	private Coroutine initialSyncRoutineBtoA;

	public bool IsLinked => false;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private DragLeader ChooseLeader(bool sliderADrag, bool sliderBDrag, bool dialADrag, bool dialBDrag)
	{
		return default(DragLeader);
	}

	private void OnToggleActionPerformed(InputAction.CallbackContext ctx)
	{
	}

	private void HandleAnyGunFired()
	{
	}

	public void ToggleLinked()
	{
	}

	public void SetLinked(bool linked, bool doInitialSync)
	{
	}

	private bool AreBothGunsEligibleToLink()
	{
		return false;
	}

	private void StartInitialSyncRoutine(GunController followerGun, GunElevationSliderBinding followerSliderBinding, float targetDeg)
	{
	}

	[IteratorStateMachine(typeof(_003CInitialSyncCoroutine_003Ed__46))]
	private IEnumerator InitialSyncCoroutine(GunController followerGun, GunElevationSliderBinding followerSliderBinding, float targetDeg, float duration)
	{
		return null;
	}

	private void StopInitialSyncRoutines()
	{
	}

	private void UpdateAnimator(bool linked)
	{
	}
}
