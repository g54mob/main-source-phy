using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
	public enum BackdriveSource
	{
		CurrentAngle = 0,
		DesiredRotation = 1
	}

	[Serializable]
	public class FloatValueProvider_CurrentAngle : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_CurrentAngle(TurretController c)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_DesiredRotation : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_DesiredRotation(TurretController c)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_CurrentRotationSpeed : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_CurrentRotationSpeed(TurretController c)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_DesiredElevation : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_DesiredElevation(TurretController c)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_CurrentElevation : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_CurrentElevation(TurretController c)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_PowderCharge : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_PowderCharge(GunController g)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunCurrentElevation : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunCurrentElevation(GunController g)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunElevationSpeed : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunElevationSpeed(GunController g)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunElevationErrorDeg : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunElevationErrorDeg(GunController g)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunCurrentRange : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunCurrentRange(GunController g)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunPredictedImpactTime : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunPredictedImpactTime(GunController g)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunCanFire : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunCanFire(GunController g)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunIsReloading : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunIsReloading(GunController g)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_RotationErrorDeg : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_RotationErrorDeg(TurretController c)
		{
		}

		public float GetFloatValue()
		{
			return 0f;
		}
	}

	[CompilerGenerated]
	private sealed class _003CInternal_MoveTurret_003Ed__121 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Vector3 worldPos;

		public TurretController _003C_003E4__this;

		private Vector3 _003CdesiredLocation_003E5__2;

		private Vector3 _003CstartingLocation_003E5__3;

		private double _003CstartedAt_003E5__4;

		private double _003CendsAt_003E5__5;

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
		public _003CInternal_MoveTurret_003Ed__121(int _003C_003E1__state)
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

	public static TurretController Instance;

	[Header("Core Components")]
	[Tooltip("UI/2D RectTransform that visually represents the turret base (its Z rotation is driven by this script).")]
	public RectTransform turretBase;

	[Tooltip("Optional 3D mimic component to mirror turret rotation/elevation into a 3D model.")]
	public Turret3DMimic turret3DMimic;

	[Tooltip("All GunController instances under this turret. They may receive desired elevation from this controller (legacy global) or independently via per-gun bindings.")]
	public List<GunController> guns;

	[Header("Rotation Physics")]
	[Tooltip("Maximum steady-state rotation speed in degrees/second that the control system will attempt to reach.")]
	public float rotationSpeed;

	[Tooltip("Time in seconds to accelerate from 0 to max rotation speed (also used for braking). Higher = smoother, smaller = snappier.")]
	public float rotationAccelerationTime;

	[Tooltip("Initial turret rotation angle in degrees (Z axis).")]
	public float startingRotation;

	[Header("Elevation Limits")]
	[Tooltip("Minimum allowed barrel elevation in degrees.")]
	public float minBarrelElevation;

	[Tooltip("Maximum allowed barrel elevation in degrees.")]
	public float maxBarrelElevation;

	[Tooltip("Starting elevation angle applied to all guns (clamped to min/max). Used only if 'Drive Gun Elevations From Controller' is enabled.")]
	public float startingElevation;

	[Header("Input Settings")]
	[Tooltip("Keyboard key to rotate turret left (CCW).")]
	public KeyCode rotateLeftKey;

	[Tooltip("Keyboard key to rotate turret right (CW).")]
	public KeyCode rotateRightKey;

	[Tooltip("Keyboard key to increase elevation (raise barrels).")]
	public KeyCode increaseElevationKey;

	[Tooltip("Keyboard key to decrease elevation (lower barrels).")]
	public KeyCode decreaseElevationKey;

	[Tooltip("Degrees/second added to DesiredRotation while a rotation key is held.")]
	public float desiredRotationSpeed;

	[Tooltip("Degrees/second added or subtracted from DesiredElevation while elevation keys are held (only used if 'Drive Gun Elevations From Controller' is enabled).")]
	public float desiredElevationChangeSpeed;

	[Header("Rotation Dial Integration")]
	[Tooltip("Dial controlling additive turret rotation (fine adjustment when dragged).\nShould be set to Unlimited mode. Ignored while a rotation-speed dial is actively providing input unless overridden by 'Drag Overrides Speed Dial'.")]
	public DialInteractable rotationDial;

	[Tooltip("How many dial degrees correspond to one turret degree of rotation for fine adjustments.\nExample: 4 means 4° of dial = 1° of turret.")]
	public float dialDegreesPerTurretDegree;

	[Tooltip("Constant offset added after mapping dial degrees to turret rotation (allows calibrating neutral dial position).")]
	public float turretRotationOffset;

	[Header("Elevation Dial Integration (Legacy Global)")]
	[Tooltip("Dial controlling additive elevation (fine adjustment when dragged). Ignored if an elevation speed dial is actively providing continuous input unless overridden by 'Drag Overrides Elevation Speed Dial'.\nOnly relevant if 'Drive Gun Elevations From Controller' is enabled.")]
	public DialInteractable elevationDial;

	[Tooltip("How many dial degrees correspond to one elevation degree for fine adjustments (global mode only).")]
	public float dialDegreesPerElevationDegree;

	[Tooltip("Constant offset added after mapping dial degrees to elevation (global mode only).")]
	public float elevationOffset;

	[Header("Compass Output Settings")]
	[Tooltip("Offset (degrees) added before wrapping to [0,360) for compass-style outputs. Ex: 90 means left becomes 90.")]
	public float compassBearingOffset;

	[Tooltip("If true, compass bearing outputs invert sign (mirrors left/right).")]
	public bool invertCompassBearing;

	[Header("Manual Rotation Speed Dial")]
	[Tooltip("Dial providing continuous rotation speed (-1..+1). Negative = left (CCW), positive = right (CW). Overrides rotation drag dial while active unless 'Drag Overrides Speed Dial' is enabled.")]
	public DialInteractable rotationSpeedDial;

	[Tooltip("Maximum degrees/second applied when rotation speed dial is at magnitude 1. Also used as the target-rate ceiling for DesiredRotation when speed dial is active.")]
	public float maxManualRotationSpeed;

	[Header("Manual Rotation Speed Debug Input (New Input System)")]
	[Tooltip("Input Action (Button recommended) that, while held, forces the Manual Rotation Speed Dial to full LEFT (-1).\n- Press/hold: sets rotationSpeedDial value to -1\n- Release/not pressed: returns to 0 (unless the Right action is held)\nThis is intended as a debug control that reuses the existing rotationSpeedDial logic path.\nIf unassigned or disabled, it does nothing.")]
	[SerializeField]
	private InputActionReference forceManualRotateLeftAction;

	[Tooltip("Input Action (Button recommended) that, while held, forces the Manual Rotation Speed Dial to full RIGHT (+1).\n- Press/hold: sets rotationSpeedDial value to +1\n- Release/not pressed: returns to 0 (unless the Left action is held)\nThis is intended as a debug control that reuses the existing rotationSpeedDial logic path.\nIf unassigned or disabled, it does nothing.")]
	[SerializeField]
	private InputActionReference forceManualRotateRightAction;

	[Tooltip("If true, when both Force Left and Force Right debug actions are held at the same time, they cancel out to 0 (neutral).\nIf false, 'Force Right' wins when both are held.")]
	[SerializeField]
	private bool debugForceActionsCancelOut;

	[Header("Manual Elevation Speed Dial (Legacy Global)")]
	[Tooltip("Dial providing continuous elevation speed (-1..+1). Negative = down, positive = up. Overrides elevation drag dial while active unless 'Drag Overrides Elevation Speed Dial' is enabled.\nOnly relevant if 'Drive Gun Elevations From Controller' is enabled.")]
	public DialInteractable elevationSpeedDial;

	[Tooltip("Maximum degrees/second applied when elevation speed dial is at magnitude 1 (global mode only).")]
	public float maxManualElevationSpeed;

	[Header("Desired Rotation Target Dynamics")]
	[Tooltip("Time in seconds for the DesiredRotation's commanded rate to ramp from 0 to |MaxManualRotationSpeed| when the rotation speed dial changes.\n0 = instantaneous (legacy behavior). Applies only to the speed-dial path; rotation drag (absolute) remains instantaneous.\nRecommendation: match 'Rotation Physics > rotationAccelerationTime' for symmetrical feel.")]
	public float desiredRotationAccelerationTime;

	[Header("Rotation Telemetry")]
	[Tooltip("If > 0, enables exponential smoothing of measured rotation speed for display. 0 = no smoothing. Recommended small value like 0.15. Range: 0..0.95.")]
	[Range(0f, 0.95f)]
	public float rotationSpeedSmoothing;

	[Header("Dial Backdrive")]
	[Tooltip("If true, the rotation dial will be programmatically rotated to follow the turret when the turret moves from any source.\nBackdrive never runs while the user is actively dragging the rotation dial (to avoid fighting the user).")]
	public bool backdriveRotationDial;

	[Tooltip("Select whether the backdriven dial follows the physical turret angle (CurrentAngle) or the target angle (DesiredRotation).")]
	public BackdriveSource backdriveSource;

	[Tooltip("If true, the dial uses its own detent smoothing to animate toward the backdriven angle (if detents are enabled). If false, it snaps each frame for exact sync.")]
	public bool backdriveUseDialSmoothing;

	[Tooltip("If true, wraps the source angle used for backdrive to keep the dial angle bounded, preventing unbounded growth.\nTypical: 360 to keep within [-180..+180]. Set false to let the dial spin indefinitely.")]
	public bool wrapBackdriveAngle;

	[Tooltip("Modulo in degrees used when wrapping the backdrive source angle (see 'Wrap Backdrive Angle'). 360 produces [-180..+180].")]
	public float backdriveWrapDegrees;

	[Header("Drag Override Options")]
	[Tooltip("If true, when the rotation speed dial is non-zero and the user begins dragging the rotation dial, dragging takes priority for the duration of the drag.\nIf false, dragging is ignored while the rotation speed dial is active.")]
	public bool dragOverridesSpeedDial;

	[Tooltip("If true, when the elevation speed dial is non-zero and the user begins dragging the elevation dial, dragging takes priority for the duration of the drag.\nIf false, dragging is ignored while the elevation speed dial is active.\nOnly relevant if 'Drive Gun Elevations From Controller' is enabled.")]
	public bool dragOverridesElevationSpeedDial;

	[Header("Per-Gun Elevation Mode")]
	[Tooltip("If true, the turret controller will push its DesiredElevation to all guns each frame (legacy global mode).\nIf false (recommended), elevation is controlled per gun via UI bindings (e.g., GunElevationSliderBinding), and this controller will NOT set gun elevation.")]
	public bool driveGunElevationsFromController;

	[Header("Movement")]
	public float MovementSpeed;

	public Vector2? MovementStartLoc;

	public Vector2? MovementTargetLoc;

	private Coroutine CR_Movement;

	[Header("Events")]
	[Tooltip("Invoked when the user begins dragging the Rotation Dial WHILE: (1) 'Drag Overrides Speed Dial' is enabled AND (2) the Rotation Speed Dial currently has a non-zero value.\nUse this to react precisely when the manual drag takes priority over the active rotation speed dial.\nFires once per drag begin; suppressed if the speed dial value is effectively zero (|value| ≤ 0.001).")]
	public UnityEvent OnRotationDragOverrideSpeedDial;

	[Tooltip("Invoked when the user begins dragging the Elevation Dial WHILE: (1) 'Drag Overrides Elevation Speed Dial' is enabled AND (2) the Elevation Speed Dial currently has a non-zero value.\nUse this to react precisely when the manual drag takes priority over the active elevation speed dial.\nFires once per drag begin; suppressed if the speed dial value is effectively zero (|value| ≤ 0.001).\nOnly relevant if 'Drive Gun Elevations From Controller' is enabled.")]
	public UnityEvent OnElevationDragOverrideSpeedDial;

	public UnityEvent OnTurretStartMove;

	public UnityEvent OnTurretFinishMove;

	private float rotationVelocity;

	private int controlledGunIndex;

	private float desiredRotationVelocity;

	private float desiredRotationVelocityTarget;

	private bool isUsingSpeedDial;

	private float rotationDialBaseAngle;

	private bool rotationDialDragActive;

	private bool isUsingElevationSpeedDial;

	private float elevationDialBaseAngle;

	private bool elevationDialDragActive;

	private float lastAngleForSpeed;

	private float observedRotationSpeed;

	private bool firstSpeedSample;

	private bool debugForceLeftHeld;

	private bool debugForceRightHeld;

	public bool IsMoving => false;

	public float DesiredRotation { get; private set; }

	public float CurrentAngle { get; private set; }

	public float DesiredElevation { get; private set; }

	public float CurrentElevation => 0f;

	public float CurrentRotationSpeed => 0f;

	public float CommandedRotationSpeed => 0f;

	public float DesiredRotationCompass => 0f;

	public float CurrentAngleCompass => 0f;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
	}

	private void BindAndEnableDebugRotationActions()
	{
	}

	private void UnbindAndDisableDebugRotationActions()
	{
	}

	private void OnForceManualRotateLeftPerformed(InputAction.CallbackContext ctx)
	{
	}

	private void OnForceManualRotateLeftCanceled(InputAction.CallbackContext ctx)
	{
	}

	private void OnForceManualRotateRightPerformed(InputAction.CallbackContext ctx)
	{
	}

	private void OnForceManualRotateRightCanceled(InputAction.CallbackContext ctx)
	{
	}

	private void ApplyDebugForcedManualRotationSpeedToDial()
	{
	}

	private void ApplyRotationToTransforms()
	{
	}

	private void UpdateMeasuredRotationSpeed()
	{
	}

	private float GetAverageCurrentElevation()
	{
		return 0f;
	}

	private void HandleInput()
	{
	}

	private void UpdateDesiredRotationTargetDynamics()
	{
	}

	private void OnBeginRotationDialDrag()
	{
	}

	private void OnEndRotationDialDrag()
	{
	}

	private void OnBeginElevationDialDrag()
	{
	}

	private void OnEndElevationDialDrag()
	{
	}

	private void UpdateRotationPhysics()
	{
	}

	private void UpdateElevationForAllGuns()
	{
	}

	private float MapDialToTurretRotation(float dialDegrees)
	{
		return 0f;
	}

	private float MapDialToElevation(float dialDegrees)
	{
		return 0f;
	}

	private float MapTurretToDialDegrees(float turretDegrees)
	{
		return 0f;
	}

	private float NormalizeCompassBearing(float angle)
	{
		return 0f;
	}

	private void BackdriveRotationDial()
	{
	}

	private static float WrapAngle(float angleDeg, float modulo)
	{
		return 0f;
	}

	public void FireControlledGun()
	{
	}

	public void FireGunByIndex(int gunIndex)
	{
	}

	public void SetPowderChargeForAllGuns(int chargeLevel)
	{
	}

	public void SetTurretLocation(Vector3 worldPos)
	{
	}

	public void MoveTurret(Vector3 worldPos)
	{
	}

	[IteratorStateMachine(typeof(_003CInternal_MoveTurret_003Ed__121))]
	public IEnumerator Internal_MoveTurret(Vector3 worldPos)
	{
		return null;
	}

	public void ResetTurretRotation()
	{
	}
}
