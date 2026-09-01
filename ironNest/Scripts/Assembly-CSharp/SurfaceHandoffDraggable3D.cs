using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class SurfaceHandoffDraggable3D : MonoBehaviour, ICursorDraggable
{
	public enum HomeSurface
	{
		None = 0,
		Clipboard = 1,
		Map = 2
	}

	public enum DragAnchorMode
	{
		PivotUnderCursor = 0,
		PreserveGrabOffset = 1
	}

	[CompilerGenerated]
	private sealed class _003CLerpLocalScaleRoutine_003Ed__74 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public SurfaceHandoffDraggable3D _003C_003E4__this;

		public Vector3 from;

		public Vector3 to;

		private float _003Ct_003E5__2;

		private float _003Cdur_003E5__3;

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
		public _003CLerpLocalScaleRoutine_003Ed__74(int _003C_003E1__state)
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

	[Header("References (Required)")]
	[SerializeField]
	[Tooltip("Collider used ONLY to confirm the initial press hit belongs to this object.\n\nRequired.\n\nNotes:\n- Should be the collider representing THIS object.\n- Used to verify that BeginDragFromManager screenPos actually hits this object.\n- Drag motion can be either pivot-under-cursor OR preserve-grab-offset.\n- If left unassigned, this component will try GetComponent<Collider>() in Awake().")]
	private Collider interactionCollider;

	[SerializeField]
	[Tooltip("Camera used for ray-to-plane intersection and surface pointer-over checks.\n\nResolution order:\n1) BeginDragFromManager(camera,...) overrides this if non-null.\n2) If assigned here, it is used.\n3) If autoResolveByTag is enabled, resolved via raycastCameraTag.\n4) Fallback: Camera.main.")]
	private Camera cam;

	[Header("Tag Resolution (Prefab-Friendly Defaults)")]
	[SerializeField]
	[Tooltip("If true, missing references are resolved at runtime using Unity Tags (recommended for runtime-instanced prefabs).\n\nSafe default: true.")]
	private bool autoResolveByTag;

	[SerializeField]
	[Tooltip("Unity Tag used to find the raycast camera if not assigned.\n\nSafe default: \"MainCamera\".\n\nTip:\n- Make sure exactly one Camera in scene carries this tag.")]
	private string raycastCameraTag;

	[SerializeField]
	[Tooltip("Unity Tag used to find the VirtualCursor.\n\nSafe default: \"VirtualCursor\".\n\nIf VirtualCursor is not found:\n- Dragging falls back to screen center.\nRecommended:\n- Ensure a VirtualCursor exists for consistent behavior.")]
	private string virtualCursorTag;

	[SerializeField]
	[Tooltip("Unity Tag used to find the clipboard surface (BoundedDragSurface3D).\n\nSafe default: \"ClipboardSurface\".\n\nRequired for clipboard handoff behavior if you do not assign clipboardSurface manually.")]
	private string clipboardSurfaceTag;

	[SerializeField]
	[Tooltip("Unity Tag used to find the map surface (BoundedDragSurface3D).\n\nSafe default: \"MapSurface\".\n\nRequired for map handoff behavior if you do not assign mapSurface manually.")]
	private string mapSurfaceTag;

	[Header("Resolved/Assigned Systems")]
	[SerializeField]
	[Tooltip("VirtualCursor used as the single pointer source.\n\nIf not assigned and autoResolveByTag is true, resolved via virtualCursorTag.\n\nFallback:\n- Screen center if missing.")]
	private VirtualCursor virtualCursor;

	[SerializeField]
	[Tooltip("Clipboard surface (BoundedDragSurface3D).\n\nIf not assigned, resolved by tag when autoResolveByTag is true.")]
	private BoundedDragSurface3D clipboardSurface;

	[SerializeField]
	[Tooltip("Map surface (BoundedDragSurface3D).\n\nIf not assigned, resolved by tag when autoResolveByTag is true.")]
	private BoundedDragSurface3D mapSurface;

	[Header("Drag Settings")]
	[SerializeField]
	[Tooltip("Drag anchor mode.\n\nPivotUnderCursor:\n- On drag start the object pivot is placed under the cursor (snaps).\n- During drag the object pivot follows the cursor.\n- This eliminates cursor drift during surface handoff.\n\nPreserveGrabOffset:\n- On drag start we remember the current offset between the object pivot and the cursor plane-hit point.\n- During drag we keep that offset, so the object does NOT snap/center under the cursor.\n\nSafe default: PivotUnderCursor.")]
	private DragAnchorMode dragAnchorMode;

	[SerializeField]
	[Tooltip("Lift (world units) applied along active surface normal while dragging.\n\nIf useSurfaceDefaultLift is true and this is 0, uses surface.DefaultDragLift.\n\nNote:\n- While dragging, the final applied lift is (resolvedLift + perObjectRandomLiftOffset).\n- On release, the drag-lift portion is removed, but random lift remains.\n\nSafe default: 0.02")]
	private float dragLift;

	[SerializeField]
	[Tooltip("If true and dragLift is 0, uses surface.DefaultDragLift.\n\nSafe default: true.")]
	private bool useSurfaceDefaultLift;

	[SerializeField]
	[Tooltip("Follow smoothing speed while dragging.\n\nImplementation:\ntransform.position = Lerp(current, target, dt * dragFollowSpeed)\n\nSafe default: 18-24")]
	private float dragFollowSpeed;

	[SerializeField]
	[Tooltip("If true, clamps movement within the active surface bounds.\n\nImportant:\n- This implementation clamps ONLY the in-plane axes and preserves normal-axis lift.\n- This allows drag lift and per-object random lift to remain visible.\n\nSafe default: true.")]
	private bool clampToSurfaceBounds;

	[Header("Per-Object Random Lift Offset (Anti Z-Fighting)")]
	[SerializeField]
	[Tooltip("If true, this draggable samples and stores a random lift offset (world units) and adds it to the resolved lift.\n\nWhy:\n- Provides tiny per-object height variance to reduce z-fighting when multiple draggables overlap.\n\nHow it works:\n- On Awake (and again if re-sampled), a random value in [randomLiftMin, randomLiftMax] is chosen.\n- While dragging: final lift = (dragLift or surface.DefaultDragLift) + sampledRandomLiftOffset.\n- On release: only the sampledRandomLiftOffset remains (drag lift is removed).\n\nSafe default: enabled.")]
	private bool useRandomLiftOffset;

	[SerializeField]
	[Tooltip("Minimum random lift offset (world units) sampled per object.\n\nNotes:\n- Typically negative to pull the object slightly closer to the surface.\n- Must be <= randomLiftMax.\n\nSafe default: -0.003")]
	private float randomLiftMin;

	[SerializeField]
	[Tooltip("Maximum random lift offset (world units) sampled per object.\n\nNotes:\n- Typically negative to pull the object slightly closer to the surface.\n- Must be >= randomLiftMin.\n\nSafe default: -0.001")]
	private float randomLiftMax;

	[SerializeField]
	[Tooltip("If true, re-samples the random lift offset each time a drag starts.\n\nIf false:\n- The object keeps the same sampled lift offset for its whole lifetime (per-instance consistency).\n\nSafe default: false.")]
	private bool resampleRandomLiftEachDrag;

	[Header("Rotation (Match Surface)")]
	[SerializeField]
	[Tooltip("If true, while on a surface the object rotates to match surface.transform.rotation.\n\nSafe default: true.")]
	private bool matchSurfaceRotation;

	[SerializeField]
	[Tooltip("If true, surface rotation is smoothly slerped (both during drag and during surface transitions).\n\nRecommended: true for camera-parented clipboard surfaces to reduce jitter.\n\nIf false, rotation snaps immediately on surface entry.")]
	private bool smoothSurfaceRotation;

	[SerializeField]
	[Tooltip("Rotation smoothing speed when smoothSurfaceRotation is true.\n\nSafe default: 18.")]
	private float surfaceRotationLerpSpeed;

	[Header("Surface Handoff (Robust)")]
	[SerializeField]
	[Tooltip("If true, can handoff between clipboard and map while dragging.\n\nSafe default: true.")]
	private bool enableSurfaceHandoff;

	[SerializeField]
	[Tooltip("Clipboard exit margin (pixels) used to allow leaving clipboard while clamped.\n\nSafe default: 24.")]
	private float exitClipboardMarginPixels;

	[SerializeField]
	[Tooltip("Max ray distance used for surface collider tests.\n\nSafe default: 1000.")]
	private float raycastMaxDistance;

	[SerializeField]
	[Tooltip("Cooldown (seconds) after a handoff before another handoff is allowed.\n\nWhy:\n- Prevents flip-flopping when clipboard and map are both ray-hittable at once.\n\nSafe default: 0.10.")]
	private float handoffCooldownSeconds;

	[SerializeField]
	[Tooltip("When the pointer ray hits BOTH clipboard and map in the same frame, this decides which surface wins.\n\nIf true:\n- Choose the surface with the closer raycast hit distance (more physically 'in front').\n\nIf false:\n- Keep current surface unless explicit exit rules trigger.\n\nSafe default: true.")]
	private bool preferCloserSurfaceOnOverlap;

	[Header("Surface Scale (Per-Surface Multiplier + Smooth Transition)")]
	[SerializeField]
	[Tooltip("If true, applies surface.SurfaceScaleMultiplier.\n\nEffective scale = basePrefabScale * surface.SurfaceScaleMultiplier")]
	private bool useSurfaceScaleMultiplier;

	[SerializeField]
	[Tooltip("If true, scale transitions are smoothed when switching surfaces.\n\nSafe default: true.")]
	private bool smoothSurfaceScale;

	[SerializeField]
	[Tooltip("Duration (seconds) for smoothing scale transitions.\n\nSafe default: 0.18.")]
	private float surfaceScaleTransitionDuration;

	[Header("Events (UnityEvent)")]
	[SerializeField]
	[Tooltip("Invoked when a drag successfully starts (after a surface is chosen and initial placement is done).\n\nNotes:\n- This is in addition to the C# event DragStarted.\n- Use this to hook up simple inspector-driven reactions (SFX, VFX, etc.).")]
	private UnityEvent onDragStartedUnityEvent;

	[SerializeField]
	[Tooltip("Invoked when a drag ends.\n\nNotes:\n- This is in addition to the C# event DragEnded.\n- Fired even if the object is disabled mid-drag (see OnDisable()).")]
	private UnityEvent onDragEndedUnityEvent;

	[Header("Debug")]
	[SerializeField]
	[Tooltip("If true, logs drag lifecycle and handoffs.\n\nSafe default: false.")]
	private bool debug;

	private bool _dragging;

	private bool _externallyControlled;

	private HomeSurface _currentSurface;

	private Plane _activePlane;

	private float _handoffCooldownRemaining;

	private Vector3 _baseLocalScale;

	private Coroutine _surfaceScaleRoutine;

	private Vector3 _grabOffsetWorld;

	private float _randomLiftOffset;

	public bool IsDragging => false;

	public event Action DragStarted
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

	public event Action DragEnded
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

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	public void BeginDragFromManager(Camera raycastCamera, Vector2 screenPos)
	{
	}

	public void EndDragFromManager()
	{
	}

	private void StartDragInternal(Vector2 pressScreenPos)
	{
	}

	private void UpdateDrag(Vector2 screenPos)
	{
	}

	private void EndDragInternal()
	{
	}

	private void ApplyRestingLiftOnly(BoundedDragSurface3D surf)
	{
	}

	private static Vector3 ProjectPointOnPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
	{
		return default(Vector3);
	}

	private void SampleRandomLiftOffsetIfNeeded(bool force)
	{
	}

	private float ResolveLift(BoundedDragSurface3D surf)
	{
		return 0f;
	}

	private void CaptureGrabOffsetIfNeeded(BoundedDragSurface3D surf, Vector2 screenPos)
	{
	}

	private void PlaceAccordingToAnchorMode(BoundedDragSurface3D surf, Vector2 screenPos, bool snap)
	{
	}

	private void PlaceWithPreservedGrabOffset(BoundedDragSurface3D surf, Vector2 screenPos, bool snap)
	{
	}

	private void ForcePlacePivotUnderCursor(BoundedDragSurface3D surf, Vector2 screenPos, bool snap)
	{
	}

	private void TrySurfaceHandoff(Vector2 screenPos)
	{
	}

	private void HandoffTo(HomeSurface newSurface, Vector2 screenPos, string reason)
	{
	}

	private HomeSurface ChooseSurfaceFromPointer(Vector2 screenPos)
	{
		return default(HomeSurface);
	}

	private void ApplySurfaceRotation(BoundedDragSurface3D surface, bool smooth)
	{
	}

	private void ApplySurfaceScale(BoundedDragSurface3D surface, bool smooth)
	{
	}

	[IteratorStateMachine(typeof(_003CLerpLocalScaleRoutine_003Ed__74))]
	private IEnumerator LerpLocalScaleRoutine(Vector3 from, Vector3 to, float duration)
	{
		return null;
	}

	private Vector2 GetPointerScreenPosition()
	{
		return default(Vector2);
	}

	private bool ResolveReferencesByTag(bool logWarnings)
	{
		return false;
	}

	private BoundedDragSurface3D GetSurface(HomeSurface s)
	{
		return null;
	}

	private bool IsScreenPosInsideSurfaceScreenRect(BoundedDragSurface3D surface, Vector2 screenPos, float marginPixels)
	{
		return false;
	}

	private void StopAllLocalCoroutines()
	{
	}

	private static float SmoothStep01(float t)
	{
		return 0f;
	}
}
