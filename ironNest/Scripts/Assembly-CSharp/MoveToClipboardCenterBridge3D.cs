using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public class MoveToClipboardCenterBridge3D : MonoBehaviour
{
	public enum TriggerOnceConsumeMode
	{
		OnTriggerStart = 0,
		OnMoveCompleted = 1
	}

	[CompilerGenerated]
	private sealed class _003CAnimateToClipboardDestinationRoutine_003Ed__37 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoveToClipboardCenterBridge3D _003C_003E4__this;

		private Vector3 _003CstartPos_003E5__2;

		private Quaternion _003CstartRot_003E5__3;

		private Vector3 _003CstartScale_003E5__4;

		private Vector3 _003CtargetPos_003E5__5;

		private Quaternion _003CtargetRot_003E5__6;

		private Vector3 _003CtargetScale_003E5__7;

		private float _003Cdur_003E5__8;

		private float _003Ct_003E5__9;

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
		public _003CAnimateToClipboardDestinationRoutine_003Ed__37(int _003C_003E1__state)
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
	private sealed class _003CAutoMoveRoutine_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoveToClipboardCenterBridge3D _003C_003E4__this;

		private float _003Cremaining_003E5__2;

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
		public _003CAutoMoveRoutine_003Ed__36(int _003C_003E1__state)
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

	[Header("References (Optional / Prefab-Friendly)")]
	[SerializeField]
	[Tooltip("Optional reference to a draggable behaviour on this object that implements ICursorDraggable.\n\nUsed only to avoid firing while the object is actively being dragged.\n\nIf left unassigned:\n- The script will search this GameObject for a MonoBehaviour that implements ICursorDraggable.\n\nSafe usage:\n- Assign your SurfaceHandoffDraggable3D here (recommended).")]
	private MonoBehaviour draggableBehaviour;

	[SerializeField]
	[Tooltip("Clipboard surface (BoundedDragSurface3D) used as the destination.\n\nIf not assigned and autoResolveByTag is true, resolved by clipboardSurfaceTag.\n\nRequired for movement.")]
	private BoundedDragSurface3D clipboardSurface;

	[Header("Tag Resolution (Prefab-Friendly Defaults)")]
	[SerializeField]
	[Tooltip("If true, missing references are resolved at runtime using Unity Tags.\n\nSafe default: true.")]
	private bool autoResolveByTag;

	[SerializeField]
	[Tooltip("Unity Tag used to find the clipboard surface if clipboardSurface is not assigned.\n\nSafe default: \"ClipboardSurface\".\n\nTip:\n- Ensure exactly one object in the scene carries this tag.")]
	private string clipboardSurfaceTag;

	[Header("Slot Selection (Shared Cycler)")]
	[SerializeField]
	[Tooltip("If true, uses a ClipboardSlotCycler3D found on the clipboard surface GameObject (if present) to choose\na round-robin destination offset.\n\nCritical behavior (anti-skip):\n- The slot is allocated ONCE when MoveToClipboardCenteredNow() is called.\n- That allocated offset is reused for the full move (including final snap), so the cycler index is not advanced multiple times.\n\nFallback:\n- If no cycler exists or it has no offsets, destination is the clipboard center.\n\nSafe default: true.")]
	private bool useClipboardSlotCyclerIfPresent;

	[Header("One-Shot Trigger (Per Instantiation)")]
	[SerializeField]
	[Tooltip("If true, this component will allow only ONE successful move trigger for the lifetime of this instantiated object.\n\nApplies to:\n- Auto-move (if enabled)\n- Manual calls to MoveToClipboardCenteredNow()\n\nImportant:\n- This is 'per instantiated object lifetime', not 'per enable'. Disabling/enabling the object does not reset it.\n\nSafe default: false.")]
	private bool triggerOnlyOncePerInstantiation;

	[SerializeField]
	[Tooltip("When the one-shot is considered 'consumed' (only used if Trigger Only Once Per Instantiation is enabled).\n\nOnTriggerStart:\n- Consumed immediately once a move starts (snap or animation).\n- Most robust: prevents double-starts and repeated triggers.\n\nOnMoveCompleted:\n- Consumed only after the move finishes (after final snap).\n- Allows retry if the move is aborted (e.g., dragging starts mid-animation).\n\nSafe default: OnTriggerStart.")]
	private TriggerOnceConsumeMode triggerOnceMode;

	[Header("Timing (Optional Auto-Fire)")]
	[SerializeField]
	[Tooltip("If true, automatically triggers a move to clipboard destination after autoMoveDelaySeconds.\n\nSafe default: false.")]
	private bool autoMoveAfterDelay;

	[SerializeField]
	[Tooltip("Delay (seconds) before auto move triggers.\n\nSafe default: 0.75.")]
	private float autoMoveDelaySeconds;

	[SerializeField]
	[Tooltip("If true, auto move will only fire if the object is not being dragged.\n\nThis uses ICursorDraggable.IsDragging if a draggableBehaviour is assigned/resolved.\n\nSafe default: true.")]
	private bool autoMoveOnlyIfNotDragging;

	[Header("Movement Style)")]
	[SerializeField]
	[Tooltip("If true, animates position/rotation/scale to the clipboard destination.\n\nIf false, snaps instantly.\n\nSafe default: true.")]
	private bool animate;

	[SerializeField]
	[Tooltip("Duration (seconds) for the move animation.\n\nSafe default: 0.22.")]
	private float durationSeconds;

	[SerializeField]
	[Tooltip("If true, parents the object to the clipboard surface before moving.\n\nRecommended: true.\n\nWhy:\n- If the clipboard surface is camera-parented / moving, parenting first ensures the object follows correctly during the animation.")]
	private bool parentToClipboardBeforeMove;

	[Header("Clipboard Rotation Matching)")]
	[SerializeField]
	[Tooltip("If true, aligns the object rotation to clipboardSurface.transform.rotation during the move.\n\nSafe default: true.")]
	private bool matchSurfaceRotation;

	[SerializeField]
	[Tooltip("If true and matchSurfaceRotation is enabled, smoothly slerps rotation during the move.\n\nIf false, rotation snaps at the end (or immediately if snapping).\n\nSafe default: true.")]
	private bool smoothRotation;

	[Header("Clipboard Scale Matching (SurfaceScaleMultiplier)")]
	[SerializeField]
	[Tooltip("If true, applies clipboardSurface.SurfaceScaleMultiplier to the object.\n\nEffective scale:\n- baseLocalScale * clipboardSurface.SurfaceScaleMultiplier\n\nImportant:\n- baseLocalScale is captured at Awake from transform.localScale unless you provide baseScaleOverride.\n\nSafe default: true.")]
	private bool applySurfaceScaleMultiplier;

	[SerializeField]
	[Tooltip("Optional override for the 'base' local scale used when applying SurfaceScaleMultiplier.\n\nIf left as (0,0,0):\n- base scale is captured from transform.localScale in Awake.\n\nUse this if:\n- The object is instantiated with a runtime scale that is already modified, but you still want a consistent prefab-authored base.\n\nSafe example:\n- Prefab scale is (1,1,1) but runtime spawner sets to (2,2,2); set override to (1,1,1) to keep surface scaling consistent.")]
	private Vector3 baseScaleOverride;

	[SerializeField]
	[Tooltip("If true and applySurfaceScaleMultiplier is enabled, smoothly lerps localScale during the move.\n\nIf false:\n- localScale snaps to the target scale (either immediately if snapping, or at the end of the move).\n\nSafe default: true.")]
	private bool smoothScale;

	[Header("Destination Placement)")]
	[SerializeField]
	[Tooltip("If true, clamps the final position to clipboard surface bounds using clipboardSurface.ClampToSurfaceBounds().\n\nSafe default: true.")]
	private bool clampToSurfaceBounds;

	[SerializeField]
	[Tooltip("Lift (world units) applied along clipboard surface normal at the destination.\n\nIf 0, uses clipboardSurface.DefaultDragLift.\n\nSafe default: 0.0 (use surface default).")]
	private float destinationLift;

	[Header("Debug)")]
	[SerializeField]
	[Tooltip("If true, logs slot allocation and movement execution.\n\nSafe default: false.")]
	private bool debug;

	private ICursorDraggable _draggable;

	private Coroutine _routine;

	private Vector3 _capturedBaseLocalScale;

	private ClipboardSlotCycler3D _cycler;

	private bool _hasAllocatedSlot;

	private Vector2 _allocatedNormalizedOffset;

	private int _allocatedSlotIndex;

	private bool _hasConsumedOneShot;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void MoveToClipboardCenteredNow()
	{
	}

	private void AllocateSlotOnceForThisMove()
	{
	}

	private void ClearAllocatedSlot()
	{
	}

	[IteratorStateMachine(typeof(_003CAutoMoveRoutine_003Ed__36))]
	private IEnumerator AutoMoveRoutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CAnimateToClipboardDestinationRoutine_003Ed__37))]
	private IEnumerator AnimateToClipboardDestinationRoutine()
	{
		return null;
	}

	private void SnapToClipboardDestination(bool applyScaleNow, bool applyRotationNow)
	{
	}

	private void ResolveCyclerReference()
	{
	}

	private Vector3 ComputeClipboardTargetLocalScale()
	{
		return default(Vector3);
	}

	private Vector3 GetClipboardDestinationTargetWorld()
	{
		return default(Vector3);
	}

	private Vector3 GetClipboardDestinationBasePointOnPlaneWorld()
	{
		return default(Vector3);
	}

	private void ResolveDraggableReference()
	{
	}

	private bool ResolveClipboardByTag(bool logWarnings)
	{
		return false;
	}

	private static Vector3 ProjectPointOnPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
	{
		return default(Vector3);
	}

	private static float SmoothStep01(float t)
	{
		return 0f;
	}
}
