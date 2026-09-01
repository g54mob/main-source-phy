using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public enum CommandSource
	{
		Unknown = 0,
		Slider = 1,
		Dial = 2,
		API = 3
	}

	[CompilerGenerated]
	private sealed class _003CFireShellDelayed_003Ed__108 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GunController _003C_003E4__this;

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
		public _003CFireShellDelayed_003Ed__108(int _003C_003E1__state)
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

	[Header("Identification")]
	[Tooltip("Human-friendly identifier for logs and debugging.")]
	public string gunName;

	[Tooltip("Index of this barrel for any 3D mimic/animation logic.")]
	public int barrelIndex3D;

	[Header("Core Components")]
	[Tooltip("Fire point RectTransform used as the projectile origin in world-space canvas coordinates.")]
	public RectTransform firePoint;

	[Tooltip("Reload controller responsible for shell state and reload workflow. Will be auto-wired back to this GunController on Awake.")]
	public ArtilleryReloadController artilleryReloadController;

	[Tooltip("Animator for firing/recoil visuals. Optional.")]
	public Animator gunAnimator;

	[Header("Button Integration")]
	[Tooltip("Optional LookAtTarget button used to trigger firing and update visual active state.")]
	public LookAtTarget fireButton;

	[Tooltip("Optional LookAtTarget button that is ONLY activated/deactivated based on whether this gun can currently fire.\nThis button does not register any click handlers here and is not used to request firing.\nUse it as a 'ready to fire' indicator or as a separate UI element that should only be interactable/visible when firing is possible.")]
	public LookAtTarget buttonToActivate;

	[Header("Firing Logic")]
	[Tooltip("Delay between requesting fire and actually spawning visuals/projectiles (seconds). Input is handled elsewhere via Input Actions.")]
	public float fireDelay;

	[Header("Elevation Physics (Base)")]
	[Tooltip("Base slew speed for elevation motion (degrees per second) before any attenuation.")]
	public float elevationChangeSpeed;

	[Tooltip("Time (seconds) to ramp toward target slew speed (smooth acceleration/deceleration).")]
	public float elevationAccelerationTime;

	[Header("Gun Dispersion (UI/local units)")]
	[Tooltip("Max horizontal dispersion in UI/local units (visual spread).")]
	public float gunHorizontalDispersion;

	[Tooltip("Max vertical dispersion in UI/local units (visual spread).")]
	public float gunVerticalDispersion;

	[Header("System Pressure Attenuation (Slider Only)")]
	[Tooltip("If true, the elevation motor is pressure-limited ONLY when the last command source was the SLIDER.\n- Accelerating toward target speed uses pressure-scaled acceleration and max speed.\n- Braking (reducing current speed) uses FULL base acceleration (unscaled), so motion can stop when pressure is lost.\n- Dial commands remain immediate and unaffected.\n- Reload motion is never attenuated.\nRecommended: true.")]
	[SerializeField]
	private bool attenuateSliderBySystemPressure;

	[Tooltip("Optional reference to a HighPressureSystemManager providing Health01 (0..1).\nIf empty and Auto-Find is enabled, this Gun will look up a manager by System Id via HighPressureSystemManager.FindBySystemId().")]
	[SerializeField]
	private HighPressureSystemManager highPressureSystemManager;

	[Tooltip("If true and no direct HighPressureSystemManager is assigned, auto-find one by System Id at runtime (safe across multi-scene).")]
	[SerializeField]
	private bool autoFindSystemManagerById;

	[Tooltip("System Id used to auto-find the HighPressureSystemManager when no direct reference is assigned.\nRules: Case-sensitive, non-empty.\nExamples: \"Default\", \"ReactorA\", \"UpperDeck\"")]
	[SerializeField]
	private string systemIdForAutoFind;

	[Tooltip("Curve mapping Health01 (X: 0..1) to speed scale (Y: 0..1) applied to BOTH max speed and acceleration when last command source was the slider.\nY=1 => no attenuation; Y=0 => target speed is 0 and acceleration toward target is 0, but braking remains at full base acceleration.\nDefault: Linear(0->0, 1->1).")]
	[SerializeField]
	private AnimationCurve healthToSpeedScale;

	[Tooltip("If true, logs effective speed scale for diagnostics (Play Mode only). Disable in production.")]
	[SerializeField]
	private bool logSliderAttenuation;

	[Header("External Reload Coordination (Optional)")]
	[Tooltip("If true, auto-lowering to the reload elevation is delayed while reloading is pending. Managed by an external coordinator to synchronize multi-gun reload behavior.\nWhen false (default): this gun lowers to its reload elevation as soon as reloading starts per normal logic.")]
	[SerializeField]
	private bool externalReloadLoweringLocked;

	[Tooltip("If true, after reload completes, the gun remains held at the reload elevation until explicitly released by an external coordinator.\nWhen false (default): this gun immediately resumes toward Desired elevation upon reload completion.")]
	[SerializeField]
	private bool externalHoldAtReloadAfterComplete;

	private float elevationChangeVelocity;

	private float minRange;

	private float maxRange;

	private bool isReloading;

	private bool pendingReload;

	private bool hasFired;

	private Turret3DMimic turret3DMimic;

	private TurretController parentTurret;

	private float reloadElevation;

	private bool isTargetingReloadElevation;

	private float internalDesiredElevation;

	private CommandSource lastCommandSource;

	private bool? cachedFireButtonActive;

	private bool? cachedButtonToActivateActive;

	public ShellBlueprint ChamberedShellBlueprint => null;

	public float CurrentRange { get; private set; }

	public float CurrentElevation { get; private set; }

	public float DesiredElevationAngle { get; private set; }

	public float MinElevationAngle { get; private set; }

	public int PowderCharges => 0;

	public float CurrentElevationSpeed { get; private set; }

	public bool IsReloading => false;

	public bool CanFire => false;

	public float PredictedImpactTime { get; private set; }

	public float ElevationErrorDeg => 0f;

	[Tooltip("The last input source that set Desired Elevation.\n- Unknown: no source yet\n- Slider: interactive Desired Elevation slider\n- Dial: Elevation dial\n- API: other callers (e.g., range mapping)\nUseful for detecting when Dial overrides the Slider.")]
	public CommandSource LastCommandSource => default(CommandSource);

	public bool ExternalReloadLoweringLocked => false;

	public bool ExternalHoldAtReloadAfterComplete => false;

	public event Action<float> OnPredictedImpactTimeChanged
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

	public event Action OnGunFired
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

	public event Action OnShellLaunched
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

	public event Action<int> OnPowderChargeChanged
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

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Initialize(TurretController controller, float minElevation, float maxElevation)
	{
	}

	[Tooltip("Set desired elevation as commanded by a Dial (immediate, unaffected by pressure).")]
	public void SetDesiredElevationFromDial(float elevationAngle)
	{
	}

	[Tooltip("Set desired elevation as commanded by the Slider (pressure-limited motor).")]
	public void SetDesiredElevationFromSlider(float elevationAngle)
	{
	}

	[Tooltip("Generic desired elevation setter (unattenuated). Prefer source-specific setters.")]
	public void SetDesiredElevation(float elevationAngle)
	{
	}

	public void SetExternalReloadLoweringLocked(bool locked)
	{
	}

	public void SetExternalHoldAtReloadAfterComplete(bool hold)
	{
	}

	public void ReleaseReloadHoldAndRestore()
	{
	}

	private void SetDesiredElevationInternal(float elevationAngle, CommandSource source)
	{
	}

	public void ResetElevation()
	{
	}

	public void SetDesiredRange(float targetRange)
	{
	}

	public bool SetPowderCharge(int chargeLevel)
	{
		return false;
	}

	public void UpdateRangeLimitsFromCharge()
	{
	}

	public float MapElevationToRange(float elevation)
	{
		return 0f;
	}

	private void UpdateElevationPhysics()
	{
	}

	private void HandleReloading()
	{
	}

	public void OnReloadingComplete()
	{
	}

	public void RequestFire()
	{
	}

	[IteratorStateMachine(typeof(_003CFireShellDelayed_003Ed__108))]
	private IEnumerator FireShellDelayed()
	{
		return null;
	}

	private void FireShell()
	{
	}

	private void NotifyPredictedImpactTime()
	{
	}

	private void UpdateFireButtonActiveState()
	{
	}

	public void OnShellLoaded()
	{
	}

	public void ForcePendingReload()
	{
	}
}
