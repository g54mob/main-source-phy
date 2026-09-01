using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class ImpactVisualCorrections : MonoBehaviour
{
	public enum TargetSelectionMode
	{
		NearestActiveTarget = 0,
		SpecificID = 1
	}

	public enum RotationAxis
	{
		LocalX = 0,
		LocalY = 1,
		LocalZ = 2
	}

	public enum PointerAxis
	{
		LocalUp = 0,
		LocalRight = 1,
		LocalForward = 2,
		LocalDown = 3,
		LocalLeft = 4,
		LocalBack = 5
	}

	[CompilerGenerated]
	private sealed class _003CDeferredInitialEvaluation_003Ed__43 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ImpactVisualCorrections _003C_003E4__this;

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
		public _003CDeferredInitialEvaluation_003Ed__43(int _003C_003E1__state)
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

	[Header("Core References")]
	[Tooltip("Reference to the ImpactLocation component that records whether targets/allies/enemies were hit and impact radius. Auto-resolved if null and 'Auto Resolve References' is enabled.")]
	public ImpactLocation impactLocation;

	[Tooltip("If true, also suppresses arrow/range corrections when ImpactLocation's entity filter blocked scouting strips (DisableIfAnyEntityMatches). If false, corrections behave as before and ignore that flag.")]
	public bool suppressWhenScoutingStripsBlocked;

	[Tooltip("Override transform whose local space defines positions (usually 'MissionParent'). If null and auto-resolve is enabled, searches for tag 'MissionParent'.")]
	public Transform missionParentOverride;

	[Header("UI Elements")]
	[Tooltip("Root GameObject for the directional pointer visuals (child contains pointer variants or a single pointer). Used ONLY for TARGET/ENEMY corrections. On target/enemy hit this object is destroyed to suppress corrections.")]
	public GameObject arrowRoot;

	[Tooltip("TMP Text component used to display distance/bracket text for a missed impact (relative to TARGETS/ENEMIES only). On target/enemy hit, its GameObject is destroyed to suppress corrections.")]
	public TMP_Text rangeText;

	[Header("Display Settings")]
	[Tooltip("Fallback distance format string when no Distance Tier Controller is active (e.g. '{0:0}' or '{0:0.00}'). Ignored if a distance correction tier is active.")]
	public string rangeFormat;

	[Tooltip("Enable periodic re-evaluation of miss direction & distance after initial calculation.")]
	public bool liveUpdate;

	[Tooltip("Seconds between live updates. <= 0 means update every frame if Live Update is enabled.")]
	public float liveUpdateInterval;

	[Header("Target Selection")]
	[Tooltip("Mode for choosing the TARGET/ENEMY used for direction and distance calculations. Allies are never considered for correction.")]
	public TargetSelectionMode targetSelection;

	[Tooltip("Target ID used if TargetSelectionMode is set to SpecificIndex (Targets only).")]
	public string specificTargetID;

	[Header("Timing")]
	[Tooltip("Number of frames to delay before first evaluation (lets other systems register targets).")]
	public int evaluationFrameDelay;

	[Tooltip("If true, attempts to auto-assign ImpactLocation & MissionParent references on Awake.")]
	public bool autoResolveReferences;

	[Header("Arrow Rotation Configuration")]
	[Tooltip("Axis (local) around which the pointer should rotate to face the target direction.")]
	public RotationAxis rotationAxis;

	[Tooltip("Local axis of the arrow model that should point toward the target (e.g. LocalUp = arrow points with its up vector).")]
	public PointerAxis pointerAxis;

	[Tooltip("If true, preserves the initial local rotation of arrowRoot and rotates from that baseline.")]
	public bool preserveInitialRotation;

	[Tooltip("If true, prevents pointer updates when direction magnitude is below a small threshold (avoids jitter at near-zero distances).")]
	public bool useMinDirectionMagnitude;

	[Tooltip("Minimum squared magnitude threshold for direction vector when Use Min Direction Magnitude is enabled.")]
	public float minDirectionSqrMagnitude;

	[Header("Tier Integration (Runtime Auto-Lookup)")]
	[Tooltip("If true, tries to automatically locate the ImpactCorrectionTierController singleton if not explicitly assigned.")]
	public bool attemptAutoLocateTierController;

	[Tooltip("If true, subscribes to tier change events so this impact's visuals update if tiers unlock while it is visible.")]
	public bool listenForTierChanges;

	[Tooltip("Optional explicit reference to a tier controller (rarely needed for runtime prefabs). If null and auto-locate enabled, uses ImpactCorrectionTierController.Instance.")]
	public ImpactCorrectionTierController explicitTierController;

	[Header("Debug")]
	[Tooltip("If true, logs extra information about tier lookups and direction error recalculations.")]
	public bool debugLogs;

	private Transform _missionParent;

	private float _nextUpdateTime;

	private bool _initialEvaluated;

	private bool _isHit;

	private bool _purgedSuppressedUIElements;

	private Vector2 _impactLocalPos;

	private EntityLocation _currentTarget;

	private Vector2 _currentTargetLocalPos;

	private Quaternion _initialArrowLocalRotation;

	private float _directionErrorOffsetDeg;

	private EntityLocation _lastTargetRef;

	private bool _errorOffsetValid;

	private bool _subscribedTierEvents;

	private bool _pendingTierRetry;

	private float _tierRetryTime;

	private const float TierLookupRetryDelay = 0.5f;

	private ImpactCorrectionTierController ActiveTierController => null;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	[IteratorStateMachine(typeof(_003CDeferredInitialEvaluation_003Ed__43))]
	private IEnumerator DeferredInitialEvaluation()
	{
		return null;
	}

	private void Update()
	{
	}

	private void AttemptTierSubscription(bool initial = false)
	{
	}

	private void UnsubscribeTierEvents()
	{
	}

	private void HandleTiersChanged()
	{
	}

	private void ResetDirectionError()
	{
	}

	private void PerformEvaluation()
	{
	}

	private void UpdateMissVisuals(bool liveUpdateTrigger = false)
	{
	}

	private void SuppressCorrectionsOnHit()
	{
	}

	private void DisableAll()
	{
	}

	private bool AnyTargetOrEnemyInsideRadius(Vector2 impactLocalPos, float radius)
	{
		return false;
	}

	private EntityLocation SelectTargetData(Vector2 fromLocal)
	{
		return null;
	}

	private void ApplyArrowRotationWithTiers(float missDistance, bool targetChanged, bool liveUpdateTrigger)
	{
	}

	private Vector3 GetAxisVector(RotationAxis axis)
	{
		return default(Vector3);
	}

	private Vector3 GetPointerAxisVector(PointerAxis axis)
	{
		return default(Vector3);
	}

	private void ApplyDistanceDisplay(float missDistance)
	{
	}

	private void DestroyAndNull(ref GameObject go)
	{
	}

	private void DestroyAndNullTMP(ref TMP_Text tmp)
	{
	}
}
