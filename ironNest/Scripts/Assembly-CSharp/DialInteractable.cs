using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Dial Interactable (InputActions)")]
public class DialInteractable : MonoBehaviour, ICursorDraggable
{
	public enum DialMode
	{
		Unlimited = 0,
		Limited = 1
	}

	[Serializable]
	public class FloatEvent : UnityEvent<float>
	{
	}

	[CompilerGenerated]
	private sealed class _003CAutoFindCursorManagerRoutine_003Ed__121 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DialInteractable _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

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
		public _003CAutoFindCursorManagerRoutine_003Ed__121(int _003C_003E1__state)
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
	private sealed class _003CAutoFindSystemManagerRoutine_003Ed__128 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DialInteractable _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

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
		public _003CAutoFindSystemManagerRoutine_003Ed__128(int _003C_003E1__state)
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

	[Header("Cursor Integration")]
	[Tooltip("If true, subscribes to the singleton DynamicCursorManager (tag = 'CursorManager') to receive aggregated PrimaryClick Down/Up events.\nDrag begins when the press starts on this Dial's Interactable and ends on release (even if pointer leaves).")]
	[SerializeField]
	private bool useCursorManagerIntegration;

	[Tooltip("Optional direct reference to the DynamicCursorManager.\nIf left empty, this component auto-finds it by tag ('CursorManager') with periodic retries at runtime.")]
	[SerializeField]
	private DynamicCursorManager cursorManager;

	[Tooltip("Unity Tag used to locate the DynamicCursorManager at runtime when auto-finding is needed.\nDefault: 'CursorManager'. Ensure your manager GameObject has this tag.")]
	[SerializeField]
	private string cursorManagerTag;

	[Tooltip("Seconds between repeated attempts to find the cursor manager by tag when not yet available (runtime instantiation safe).")]
	[SerializeField]
	[Min(0.05f)]
	private float autoFindRetrySeconds;

	[Tooltip("VirtualCursor providing the single screen-space pointer position (pixels).\nIf not assigned, this component tries to adopt from DynamicCursorManager, then FindObjectOfType<VirtualCursor>().")]
	[SerializeField]
	private VirtualCursor virtualCursor;

	[Tooltip("Camera used to project the VirtualCursor's screen position onto the Dial's plane.\nIf not assigned, Camera.main is used at runtime.")]
	[SerializeField]
	private Camera raycastCamera;

	[Tooltip("Interactable used by DynamicCursorManager to detect hover/press on this Dial.\nIf left empty, searches this GameObject and its children for Interactable (NOT parents).")]
	[SerializeField]
	private Interactable interactable;

	[Tooltip("If true, release (OnEndDialDrag) is sent back to this Dial even if the pointer is no longer over it at release time.\nRecommended: true (mirrors collider-based behavior and keeps drag cycles consistent).")]
	[SerializeField]
	private bool alwaysReleaseToSameTarget;

	[Tooltip("If true, legacy OnMouseDown/Up callbacks on any collider in this object hierarchy are honored (editor-only workflows).\nDefault: false. Enable only if you intentionally require legacy input in parallel with Input Actions.")]
	[SerializeField]
	private bool useLegacyMouseCallbacks;

	[Header("Dial Mode")]
	[Tooltip("Selects dial behavior.\nUnlimited: value accumulates beyond any bounds (continuous rotation).\nLimited: rotation clamped between Min/Max angles and mapped to output value range.")]
	public DialMode dialMode;

	[Header("Universal Settings")]
	[Tooltip("Local axis around which the dial rotates.\nExample: (0,0,1) for Z axis (forward), (0,1,0) for Y axis.")]
	public Vector3 rotationAxis;

	[Header("Limited Mode Settings")]
	[Tooltip("Minimum allowed rotation angle in degrees (applies only in Limited mode).")]
	[SerializeField]
	private float minRotationAngle;

	[Tooltip("Maximum allowed rotation angle in degrees (applies only in Limited mode).")]
	[SerializeField]
	private float maxRotationAngle;

	[Tooltip("Output value when dial is at minimum rotation (Limited mode).")]
	[SerializeField]
	private float minOutputValue;

	[Tooltip("Output value when dial is at maximum rotation (Limited mode).")]
	[SerializeField]
	private float maxOutputValue;

	[Header("Detent Settings")]
	[Tooltip("Enable stepped detent snapping.\nUnlimited: quantizes accumulated rotation.\nLimited: quantizes output value / mapped angle.")]
	[SerializeField]
	private bool useDetents;

	[Tooltip("Step size for detents (e.g., 1 for whole-number increments). Must be > 0 to have effect.")]
	[SerializeField]
	private float detentStepSize;

	[Tooltip("Smoothing time (seconds) for snapping between detent steps. Lower = snappier.")]
	[Range(0.01f, 1f)]
	[SerializeField]
	private float detentSmoothTime;

	[Header("Custom Value Mapping")]
	[Tooltip("Optional curve remapping dial position to output value sensitivity (Limited mode only).\nX: normalized position (0=min angle, 1=max angle).\nY: remapped interpolation factor (0..1) used between Min/Max output values.")]
	[SerializeField]
	private AnimationCurve valueCurve;

	[Header("Dead Zone (Limited Mode Only)")]
	[Tooltip("Enable an angular dead zone that only triggers events when the dial enters or exits it (Limited mode only). Has no effect in Unlimited mode.\nThis dead zone DOES NOT clamp or alter the dial's value/rotation; it only raises events.")]
	[SerializeField]
	private bool deadZoneEnabled;

	[Tooltip("Lower (inclusive) bound of the dead zone, in degrees, relative to this dial's local rotation space.\nMust lie within [Min Rotation Angle .. Max Rotation Angle].")]
	[SerializeField]
	private float deadZoneMinAngle;

	[Tooltip("Upper (inclusive) bound of the dead zone, in degrees, relative to this dial's local rotation space.\nMust lie within [Min Rotation Angle .. Max Rotation Angle] and be >= Dead Zone Min Angle.")]
	[SerializeField]
	private float deadZoneMaxAngle;

	[Header("Dial Value (Runtime)")]
	[Tooltip("Accumulated value (Unlimited mode) OR mapped value within min/max (Limited mode). Updated during interaction.")]
	[SerializeField]
	private float accumulatedValue;

	[Tooltip("Current raw rotation angle in degrees (Limited mode uses clamped angle; Unlimited mode uses accumulated rotation).")]
	[SerializeField]
	private float currentRotationAngle;

	[Header("Broker-based Drag Lock (Optional)")]
	[Tooltip("If true, when a drag begins while DynamicCursorManager is in FPSLocked mode, this component acquires a lock from InteractionLockBroker.\n\nThe acquired lock request is:\n- FreezePlayerController = true\n- UseFreeMouse = true\n- UseUIActionMap = false\n\nOn drag end/disable, this component releases ONLY its own handle.\n\nNested lock safety:\n- Releasing this handle will not override other locks (e.g., console zone) because the broker resolves final state across all handles.\n\nImportant:\n- This does NOT change action maps.\n- This ONLY acquires when starting in FPSLocked (so dragging inside an already-unlocked console does not request an extra lock).")]
	[SerializeField]
	private bool useBrokerLockWhileDragging;

	[Tooltip("Unity Tag used to locate the InteractionLockBroker.\n\nDefault: 'LockBroker'.\n\nSetup:\n- Place one InteractionLockBroker in your master scene.\n- Tag that GameObject with this tag.\n\nRules:\n- Tag must exist in Project Settings > Tags and Layers.\n\nNo fallback:\n- If the broker is missing, a warning is logged and the dial will still drag, but without auto FreeMouse/freeze.")]
	[SerializeField]
	private string lockBrokerTag;

	[Tooltip("Debug label sent to the broker for this dial's drag lock request.\n\nFormat rules:\n- Any string; used for logging only.\n\nSafe examples:\n- 'DialDrag:ReactorPressure'\n- 'DialDrag:ConsoleKnobA'")]
	[SerializeField]
	private string brokerDebugLabel;

	[Header("Health-Constrained Output (Limited Mode)")]
	[Tooltip("If true, the dial's OUTPUT VALUES are dynamically clamped toward a center value based on a HighPressureSystemManager's health.\n- Applies ONLY in Limited mode. Rotation angles are NOT reduced; only output values are compressed toward Clamp Center.\n- Health01 (0..1) from the manager is mapped through 'Health -> Range Scale' to compute a scale S in [0..1].\n- EffectiveMin = Lerp(ClampCenter, MinOutputValue, S). EffectiveMax = Lerp(ClampCenter, MaxOutputValue, S).\nExamples (Min/Max = -1..+1, ClampCenter=0): Health=1 => range [-1,+1]; Health=0.25 with S=0.5 => range [-0.5,+0.5].")]
	[SerializeField]
	private bool constrainOutputBySystemHealth;

	[Tooltip("Optional direct reference to a HighPressureSystemManager to read health from.\nIf left empty and Auto-Find is enabled, this component will try to locate a manager by 'System Id' using HighPressureSystemManager.FindBySystemId().")]
	[SerializeField]
	private HighPressureSystemManager highPressureSystemManager;

	[Tooltip("If true and no direct HighPressureSystemManager is assigned, tries to auto-find one by 'System Id' at runtime and keeps a live subscription.\nSafe for multi-scene/runtime instantiation.")]
	[SerializeField]
	private bool autoFindSystemManagerById;

	[Tooltip("Manager System Id used for auto-finding when no direct reference is assigned.\nMust match HighPressureSystemManager.SystemId exactly (case-sensitive). Examples: \"Default\", \"ReactorA\".")]
	[SerializeField]
	private string systemIdForAutoFind;

	[Tooltip("Seconds between repeated attempts to auto-find the HighPressureSystemManager by System Id when not yet available (runtime instantiation safe).")]
	[SerializeField]
	[Min(0.05f)]
	private float autoFindHpsRetrySeconds;

	[Tooltip("AnimationCurve mapping Health01 (X: 0..1) to Range Scale S (Y: 0..1) used to compress the OUTPUT VALUE range toward Clamp Center.\n- Y=1 => full Min/Max range (no clamp). Y=0 => both Effective Min and Max collapse to Clamp Center.\n- Typical: Linear (0,0)->(1,1). To match the example (Health=0.25 => S=0.5), set the curve point (0.25, 0.5).")]
	[SerializeField]
	private AnimationCurve healthToRangeScale;

	[Tooltip("Value about which the output range is symmetrically compressed when health falls.\nRecommended: 0 for ranges like [-1,+1]. MUST lie within [MinOutputValue..MaxOutputValue] for correct behavior.\nExamples: 0 (bidirectional control), 50 (compress toward midpoint of 0..100 scale).")]
	[SerializeField]
	private float clampCenterValue;

	[Tooltip("If true, logs Effective Min/Max output limits whenever health-driven clamping changes them (Play Mode only).\nUseful for wiring/debug; disable in production.")]
	[SerializeField]
	private bool logHealthClamping;

	[Header("Rotation Speed Measurement")]
	[Tooltip("Maximum degrees/sec used to normalise MeasuredRotationSpeed into the 0–1 NormalizedRotationSpeed property.\n\nTune this to the fastest spin you expect the player to make.\nExample: 180 means a half-rotation per second = NormalizedRotationSpeed of 1.0.\n\nMust be > 0. Values below 1 are clamped to 1 to avoid divide-by-zero.")]
	[SerializeField]
	[Min(1f)]
	private float maxExpectedDegreesPerSecond;

	[Tooltip("Raw angular speed in degrees/sec (always positive). Updated every frame from transform delta.\nNo smoothing or decay applied — use FMODParameterSetter's Output Smoothing for that.\n\nReference this property by name via FMODParameterSetter's Provider Property Name field:\n  Property name: MeasuredRotationSpeed")]
	[SerializeField]
	private float inspectorMeasuredRotationSpeed;

	[Tooltip("MeasuredRotationSpeed normalised to 0–1 using Max Expected Degrees/Sec. Clamped, no smoothing.\n\nReference this property by name via FMODParameterSetter's Provider Property Name field:\n  Property name: NormalizedRotationSpeed\n\nRecommended: use this as the FMODParameterSetter input, set FMOD param range to 0–1,\nand set FMODParameterSetter mapping range (fmodParamMin=0, fmodParamMax=1).")]
	[SerializeField]
	private float inspectorNormalizedRotationSpeed;

	[SerializeField]
	private bool ClampGamepadCursorToValve;

	[Tooltip("This valve will be reset to default value when player stops providing input on gamepad")]
	[SerializeField]
	private bool ResetToDefaultValueWithoutNoInput;

	[Tooltip("This value will be used as multiplier for screen height to calculate actual distance")]
	[SerializeField]
	private float CursorDistanceMultiplierFromCenter;

	[Header("Limited Mode Settings")]
	[Tooltip("Minimum allowed virtual cursor movement for gamepad. Angle is calculated from the right")]
	[SerializeField]
	private float ClampedMinRotationAngle;

	[Tooltip("Maximum allowed virtual cursor movement for gamepad. Angle is calculated from the right")]
	[SerializeField]
	private float ClampedMaxRotationAngle;

	private Quaternion _speedPrevRotation;

	private bool _speedPrevRotationValid;

	[Header("Dial Events")]
	[Tooltip("Invoked whenever the dial's public value changes. Parameter = new value (accumulated or mapped).")]
	public FloatEvent OnValueChanged;

	[Tooltip("Invoked once when a Limited-mode dial first enters the configured Dead Zone (inclusive).")]
	public UnityEvent OnEnterDeadZone;

	[Tooltip("Invoked once when a Limited-mode dial exits the configured Dead Zone.")]
	public UnityEvent OnExitDeadZone;

	[Tooltip("Invoked exactly when a drag begins on this dial (\"Grab\").")]
	public UnityEvent OnGrab;

	[Tooltip("Invoked exactly when a drag ends on this dial (\"Release\").")]
	public UnityEvent OnRelease;

	private bool isDragging;

	private Vector3 dragStart;

	private float lastAngle;

	private float lastRawAngle;

	private bool _subscribedToManager;

	private bool _pressBeganHere;

	private Coroutine _findRoutine;

	private float lastQuantizedValue;

	private float detentTargetAngle;

	private float detentCurrentAngle;

	private float detentVelocity;

	private bool _wasInDeadZone;

	private InteractionLockBroker _broker;

	private InteractionLockBroker.LockHandle _dragHandle;

	private Coroutine _findHpsRoutine;

	private bool _subscribedToHps;

	private float effectiveMinOutputValue;

	private float effectiveMaxOutputValue;

	private float _lastAnnouncedEffectiveMin;

	private float _lastAnnouncedEffectiveMax;

	public float MeasuredRotationSpeed { get; private set; }

	public float NormalizedRotationSpeed { get; private set; }

	public float AccumulatedValue => 0f;

	public bool IsDragging => false;

	public bool UseLegacyMouseCallbacks => false;

	public bool IsInDeadZone => false;

	public event Action OnBeginDialDrag
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

	public event Action OnEndDialDrag
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

	public event Action DragStarted
	{
		add
		{
		}
		remove
		{
		}
	}

	public event Action DragEnded
	{
		add
		{
		}
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

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private float MapRotationToValue(float rotationAngle)
	{
		return 0f;
	}

	public void BeginDialDrag()
	{
	}

	public void EndDialDrag()
	{
	}

	private void TryAcquireBrokerDragLockIfNeeded()
	{
	}

	private void ReleaseBrokerDragLockIfHeld()
	{
	}

	private void TryFindBroker()
	{
	}

	private Vector3 GetPointerWorldPointOnDialPlane()
	{
		return default(Vector3);
	}

	public void ResetToMinimum()
	{
	}

	public void SetDialValue(float value)
	{
	}

	public void SetAccumulatedValueUnlimited(float angleDegrees, bool fireValueChangedEvent = false, bool smoothToTarget = true)
	{
	}

	private float InverseCurveEvaluate(float normalizedValue)
	{
		return 0f;
	}

	public void InvalidateRotationSpeed()
	{
	}

	private void MeasureRotationSpeed()
	{
	}

	private bool IsAngleWithinDeadZone(float angle)
	{
		return false;
	}

	private void EvaluateDeadZoneTransition(bool fireEvents)
	{
	}

	private void OnValidate()
	{
	}

	private void EnsureCursorManagerSubscription()
	{
	}

	[IteratorStateMachine(typeof(_003CAutoFindCursorManagerRoutine_003Ed__121))]
	private IEnumerator AutoFindCursorManagerRoutine()
	{
		return null;
	}

	private void SubscribeToCursorManager()
	{
	}

	private void UnsubscribeFromCursorManager()
	{
	}

	private void HandlePrimaryClickDown(Interactable pressTarget)
	{
	}

	private void HandlePrimaryClickUp(Interactable pressSourceTarget)
	{
	}

	private void TryEnsureCursorManager()
	{
	}

	private void EnsureSystemManagerSubscription()
	{
	}

	[IteratorStateMachine(typeof(_003CAutoFindSystemManagerRoutine_003Ed__128))]
	private IEnumerator AutoFindSystemManagerRoutine()
	{
		return null;
	}

	private void SubscribeToSystemManager()
	{
	}

	private void UnsubscribeFromSystemManager()
	{
	}

	private void HandleSystemHealthChanged01(float health01)
	{
	}

	private void RecomputeEffectiveRangeFromHealth(float health01, bool forceNotify)
	{
	}

	private void SetEffectiveRange(float newMin, float newMax, bool forceNotify)
	{
	}

	private void GetActiveOutputRange(out float minV, out float maxV)
	{
		minV = default(float);
		maxV = default(float);
	}
}
