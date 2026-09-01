using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Linear Slider Interactable (InputActions)")]
public class LinearSliderInteractable : MonoBehaviour, ICursorDraggable
{
	[CompilerGenerated]
	private sealed class _003CAutoFindCursorManagerRoutine_003Ed__94 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LinearSliderInteractable _003C_003E4__this;

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
		public _003CAutoFindCursorManagerRoutine_003Ed__94(int _003C_003E1__state)
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
	[Tooltip("If true, subscribes to the singleton DynamicCursorManager (tag = 'CursorManager') to receive aggregated PrimaryClick Down/Up events.\nDrag begins when the press starts on this Slider's Interactable and ends on release (even if pointer leaves).")]
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

	[Tooltip("Camera used to raycast the VirtualCursor's screen position into world space for the drag plane.\nIf not assigned, Camera.main is used at runtime.")]
	[SerializeField]
	private Camera raycastCamera;

	[Tooltip("Interactable used by DynamicCursorManager to detect hover/press on this Slider.\nIf left empty, searches this GameObject and its children for Interactable (NOT parents).")]
	[SerializeField]
	private Interactable interactable;

	[Tooltip("If true, release (OnEndSliderDrag) is sent back to this Slider even if the pointer is no longer over it at release time.")]
	[SerializeField]
	private bool alwaysReleaseToSameTarget;

	[Tooltip("If true, legacy OnMouseDown/Up callbacks on any collider in this object hierarchy are honored (editor-only workflows).")]
	[SerializeField]
	private bool useLegacyMouseCallbacks;

	[Header("Linear Motion")]
	[Tooltip("Local axis along which the object moves while dragging.\nExample: (1,0,0)=X, (0,1,0)=Y, (0,0,1)=Z. Magnitude is ignored; normalized at runtime.")]
	[SerializeField]
	private Vector3 movementAxis;

	[Tooltip("Minimum distance along the local movement axis relative to the captured base localPosition (at Awake).")]
	[SerializeField]
	private float minDistance;

	[Tooltip("Maximum distance along the local movement axis relative to the captured base localPosition (at Awake).")]
	[SerializeField]
	private float maxDistance;

	[Header("Value Mapping (Limited Range)")]
	[Tooltip("Output value when slider is at minimum distance.")]
	[SerializeField]
	private float minOutputValue;

	[Tooltip("Output value when slider is at maximum distance.")]
	[SerializeField]
	private float maxOutputValue;

	[Tooltip("Optional curve remapping slider position to output value sensitivity.\nX: normalized position (0=min distance, 1=max distance).\nY: remapped interpolation factor (0..1) used between min/max output values.")]
	[SerializeField]
	private AnimationCurve valueCurve;

	[Header("Detent Settings")]
	[Tooltip("Enable stepped detent snapping that quantizes output to fixed steps and smooths motion to target.")]
	[SerializeField]
	private bool useDetents;

	[Tooltip("Step size for detents (e.g., 1 for whole-number increments). Must be > 0 to have effect.")]
	[SerializeField]
	private float detentStepSize;

	[Tooltip("Smoothing time (seconds) for snapping between detent steps. Lower = snappier.")]
	[Range(0.01f, 1f)]
	[SerializeField]
	private float detentSmoothTime;

	[Header("Drag Sensitivity")]
	[Tooltip("If true, distance is computed RELATIVE to press start: StartDistance + (ProjectedPointerDeltaAlongAxis * DragSensitivity).\nIf false, uses absolute projection from line origin (camera-distance dependent).")]
	[SerializeField]
	private bool useRelativeDrag;

	[Tooltip("Multiplier for projected pointer delta along axis when Use Relative Drag is enabled.\n1 = 1:1, 0.5 = half speed, 2 = twice as fast. Negative inverts.")]
	[SerializeField]
	private float dragSensitivity;

	[Header("Slider Value (Runtime)")]
	[Tooltip("Mapped value within min/max output range. Updated during interaction and via API.")]
	[SerializeField]
	private float accumulatedValue;

	[Tooltip("Current distance along the local movement axis (in local space units).")]
	[SerializeField]
	private float currentDistance;

	[Header("Broker-based Drag Lock (Optional)")]
	[Tooltip("If true, when a drag begins while DynamicCursorManager is in FPSLocked mode, this component acquires a lock from InteractionLockBroker.\n\nThe acquired lock request is:\n- FreezePlayerController = true\n- UseFreeMouse = true\n- UseUIActionMap = false\n\nOn drag end/disable, this component releases ONLY its own handle.\n\nNested lock safety:\n- Releasing this handle will not override other locks (e.g., console zone) because the broker resolves final state across all handles.\n\nImportant:\n- This does NOT change action maps.\n- This ONLY acquires when starting in FPSLocked (so dragging inside an already-unlocked console does not request an extra lock).")]
	[SerializeField]
	private bool useBrokerLockWhileDragging;

	[Tooltip("Unity Tag used to locate the InteractionLockBroker.\n\nDefault: 'LockBroker'.\n\nSetup:\n- Place one InteractionLockBroker in your master scene.\n- Tag that GameObject with this tag.\n\nRules:\n- Tag must exist in Project Settings > Tags and Layers.\n\nNo fallback:\n- If the broker is missing, a warning is logged and the slider will still drag, but without auto FreeMouse/freeze.")]
	[SerializeField]
	private string lockBrokerTag;

	[Tooltip("Debug label sent to the broker for this slider's drag lock request.\n\nFormat rules:\n- Any string; used for logging only.\n\nSafe examples:\n- 'SliderDrag:Throttle'\n- 'SliderDrag:ConsoleLeverA'")]
	[SerializeField]
	private string brokerDebugLabel;

	[Header("Linear Speed Measurement")]
	[Tooltip("Maximum local-space units/sec used to normalise MeasuredLinearSpeed into the 0-1 NormalizedLinearSpeed property.\n\nTune this to the fastest slide speed you expect the player to make.\nExample: 0.5 means half a unit per second = NormalizedLinearSpeed of 1.0.\n\nMust be > 0. Values below 0.001 are clamped to 0.001 to avoid divide-by-zero.")]
	[SerializeField]
	[Min(0.001f)]
	private float maxExpectedUnitsPerSecond;

	[Tooltip("Raw linear speed in local-space units/sec (always positive). Updated every frame from localPosition delta.\nNo smoothing or decay applied -- use FMODParameterSetter's Output Smoothing for that.\n\nReference this property by name via FMODParameterSetter's Provider Property Name field:\n  Property name: MeasuredLinearSpeed")]
	[SerializeField]
	private float inspectorMeasuredLinearSpeed;

	[Tooltip("MeasuredLinearSpeed normalised to 0-1 using Max Expected Units/Sec. Clamped, no smoothing.\n\nReference this property by name via FMODParameterSetter's Provider Property Name field:\n  Property name: NormalizedLinearSpeed\n\nRecommended: use this as the FMODParameterSetter input, set FMOD param range to 0-1,\nand set FMODParameterSetter mapping range (fmodParamMin=0, fmodParamMax=1).")]
	[SerializeField]
	private float inspectorNormalizedLinearSpeed;

	private Vector3 _speedPrevLocalPosition;

	private bool _speedPrevLocalPositionValid;

	[Header("Slider Events")]
	[Tooltip("Invoked whenever the slider's public value changes. Parameter = new value.")]
	public UnityEvent<float> OnValueChanged;

	[Tooltip("Invoked exactly when a drag begins on this slider (\"Grab\").\nFires after the broker lock is acquired (if enabled) and before any drag processing occurs.\nUse this to trigger audio, animations, or state changes that should happen the moment the slider is grabbed.")]
	public UnityEvent OnGrab;

	[Tooltip("Invoked exactly when a drag ends on this slider (\"Release\").\nFires after the broker lock is released. Also fires if the slider is disabled mid-drag.\nUse this to trigger audio, animations, or state changes that should happen the moment the slider is released.")]
	public UnityEvent OnRelease;

	private bool isDragging;

	private bool _subscribedToManager;

	private bool _pressBeganHere;

	private Coroutine _findRoutine;

	private Vector3 baseLocalPosition;

	private float lastQuantizedValue;

	private float detentTargetDistance;

	private float detentCurrentDistance;

	private float detentVelocity;

	private Vector3 dragPlaneOriginWorld;

	private Vector3 dragStartHitWorld;

	private float dragStartDistance;

	private InteractionLockBroker _broker;

	private InteractionLockBroker.LockHandle _dragHandle;

	public float MeasuredLinearSpeed { get; private set; }

	public float NormalizedLinearSpeed { get; private set; }

	public float Value => 0f;

	public float CurrentDistance => 0f;

	public bool IsDragging => false;

	public bool UseLegacyMouseCallbacks => false;

	public event Action OnBeginSliderDrag
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

	public event Action OnEndSliderDrag
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

	private void ApplyLocalPosition(float distance)
	{
	}

	private float MapDistanceToValue(float distance)
	{
		return 0f;
	}

	public void BeginSliderDrag()
	{
	}

	public void EndSliderDrag()
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

	private Vector3 GetAxisWorld()
	{
		return default(Vector3);
	}

	private Vector3 GetLineOriginWorld()
	{
		return default(Vector3);
	}

	private Vector3 GetPointerWorldPointOnDragPlane(Vector3 planePoint)
	{
		return default(Vector3);
	}

	public void ResetToMinimum()
	{
	}

	public void SetSliderValue(float value)
	{
	}

	private float InverseCurveEvaluate(float normalizedValue)
	{
		return 0f;
	}

	private void MeasureLinearSpeed()
	{
	}

	private void EnsureCursorManagerSubscription()
	{
	}

	[IteratorStateMachine(typeof(_003CAutoFindCursorManagerRoutine_003Ed__94))]
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
}
