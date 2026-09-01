using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Gameplay/Move To Surface Bridge")]
public class MoveToSurfaceBridge : MonoBehaviour
{
	public enum TriggerOnceConsumeMode
	{
		[Tooltip("Consumed the moment a move trigger successfully starts. Most robust.")]
		OnTriggerStart = 0,
		[Tooltip("Consumed only after the move fully completes.\nAllows retry if the move is aborted mid-animation.")]
		OnMoveCompleted = 1
	}

	[CompilerGenerated]
	private sealed class _003CAnimateRoutine_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoveToSurfaceBridge _003C_003E4__this;

		private Vector3 _003CstartLocalPos_003E5__2;

		private Quaternion _003CstartRot_003E5__3;

		private Vector3 _003CstartScale_003E5__4;

		private Quaternion _003CtargetRot_003E5__5;

		private Vector3 _003CtargetScale_003E5__6;

		private float _003Cdur_003E5__7;

		private float _003Ct_003E5__8;

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
		public _003CAnimateRoutine_003Ed__36(int _003C_003E1__state)
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
	private sealed class _003CAutoMoveRoutine_003Ed__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoveToSurfaceBridge _003C_003E4__this;

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
		public _003CAutoMoveRoutine_003Ed__35(int _003C_003E1__state)
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
	[Tooltip("Optional reference to a draggable behaviour on this object that implements\nICursorDraggable.\n\nUsed to skip firing while the object is actively being dragged.\n\nIf left unassigned, searched automatically on this GameObject in Awake.\n\nRecommendation: assign your DraggableItem here for reliability.")]
	[SerializeField]
	private MonoBehaviour draggableBehaviour;

	[Tooltip("The destination DragSurface items are moved to.\n\nIf not assigned and autoResolveByTag is true, resolved via destinationSurfaceTag.\n\nRequired for movement.")]
	[SerializeField]
	private DragSurface destinationSurface;

	[Header("Tag Resolution (Prefab-Friendly Defaults)")]
	[Tooltip("If true, unassigned references are resolved at runtime using Unity Tags.\n\nSafe default: true.")]
	[SerializeField]
	private bool autoResolveByTag;

	[Tooltip("Unity Tag used to find the destination DragSurface if not manually assigned.\n\nEnsure exactly one GameObject in the scene carries this tag.\n\nSafe default: \"ClipboardSurface\".")]
	[SerializeField]
	private string destinationSurfaceTag;

	[Header("Slot Selection (Shared Cycler)")]
	[Tooltip("If true, uses a DragSurfaceSlotCycler found on the destination surface's\nGameObject to choose a round-robin destination position.\n\nCritical (anti-skip):\n- The slot world position is allocated exactly ONCE when\n  MoveToDestinationNow() is called.\n- That same position is reused for the full animation, so the cycler\n  index is not advanced more than once per trigger.\n\nFallback:\n- If no cycler exists or it has no slots, destination is the surface\n  transform origin.\n\nSafe default: true.")]
	[SerializeField]
	private bool useSlotCyclerIfPresent;

	[Header("One-Shot Trigger (Per Instantiation)")]
	[Tooltip("If true, only ONE successful move trigger is allowed for the lifetime of\nthis instantiated object.\n\nNote: disabling/enabling does NOT reset the guard.\n\nSafe default: false.")]
	[SerializeField]
	private bool triggerOnlyOncePerInstantiation;

	[Tooltip("When the one-shot guard is consumed (only used when\ntriggerOnlyOncePerInstantiation is true).\n\nOnTriggerStart:  consumed immediately when a move starts.\nOnMoveCompleted: consumed after the move finishes.\n\nSafe default: OnTriggerStart.")]
	[SerializeField]
	private TriggerOnceConsumeMode triggerOnceMode;

	[Header("Timing (Optional Auto-Fire)")]
	[Tooltip("If true, automatically triggers a move after autoMoveDelaySeconds when\nthis component is enabled.\n\nSafe default: false.")]
	[SerializeField]
	private bool autoMoveAfterDelay;

	[Tooltip("Delay in seconds before the auto-move fires.\n\nSafe default: 0.75.")]
	[SerializeField]
	private float autoMoveDelaySeconds;

	[Tooltip("If true, the move (auto or manual) is skipped while the object is actively\nbeing dragged.\n\nSafe default: true.")]
	[SerializeField]
	private bool skipMoveWhileDragging;

	[Header("Movement Style")]
	[Tooltip("If true, animates position/rotation/scale to the destination.\nIf false, snaps instantly.\n\nSafe default: true.")]
	[SerializeField]
	private bool animate;

	[Tooltip("Duration in seconds for the move animation.\n\nSafe default: 0.22.")]
	[SerializeField]
	private float durationSeconds;

	[Tooltip("If true, re-parents this object to the destination surface transform\nbefore moving.\n\nRecommended: true.\nWhy: if the destination surface is camera-parented or moving, parenting\nfirst keeps the animation correct.\n\nSafe default: true.")]
	[SerializeField]
	private bool parentToDestinationBeforeMove;

	[Header("Rotation Matching")]
	[Tooltip("If true, aligns this object's rotation to\ndestinationSurface.transform.rotation during the move.\n\nSafe default: true.")]
	[SerializeField]
	private bool matchSurfaceRotation;

	[Tooltip("If true and matchSurfaceRotation is enabled, rotation is smoothly slerped\nduring animation. If false, rotation snaps at the end.\n\nSafe default: true.")]
	[SerializeField]
	private bool smoothRotation;

	[Header("Scale Matching (SurfaceScaleMultiplier)")]
	[Tooltip("If true, applies destinationSurface.surfaceScaleMultiplier to this object.\n\nEffective scale = baseLocalScale * destinationSurface.surfaceScaleMultiplier.\n\nSafe default: true.")]
	[SerializeField]
	private bool applySurfaceScaleMultiplier;

	[Tooltip("Optional override for the base local scale used with SurfaceScaleMultiplier.\n\nIf left as (0, 0, 0), base scale is captured from transform.localScale in Awake.\n\nUse when the object is spawned with a modified scale but you want consistent\nprefab-authored scaling.")]
	[SerializeField]
	private Vector3 baseScaleOverride;

	[Tooltip("If true and applySurfaceScaleMultiplier is enabled, scale is smoothly lerped\nduring animation. If false, scale snaps at the end.\n\nSafe default: true.")]
	[SerializeField]
	private bool smoothScale;

	[Header("Destination Placement")]
	[Tooltip("If true, clamps the final position to the destination surface bounds.\n\nSafe default: true.")]
	[SerializeField]
	private bool clampToSurfaceBounds;

	[Tooltip("Lift (world units) applied along the destination surface normal at arrival.\n\nIf 0, uses destinationSurface.defaultDragLift.\n\nSafe default: 0 (use surface default).")]
	[SerializeField]
	private float destinationLift;

	[Header("Debug")]
	[Tooltip("If true, logs slot allocation and move execution to the Console.\n\nSafe default: false.")]
	[SerializeField]
	private bool debug;

	private ICursorDraggable _draggable;

	private Coroutine _routine;

	private Vector3 _capturedBaseLocalScale;

	private DragSurfaceSlotCycler _cycler;

	private bool _hasAllocatedSlot;

	private Vector3 _allocatedLocalPosition;

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

	public void MoveToDestinationNow()
	{
	}

	private void AllocateSlotOnceForThisMove()
	{
	}

	private void ClearAllocatedSlot()
	{
	}

	[IteratorStateMachine(typeof(_003CAutoMoveRoutine_003Ed__35))]
	private IEnumerator AutoMoveRoutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CAnimateRoutine_003Ed__36))]
	private IEnumerator AnimateRoutine()
	{
		return null;
	}

	private void SnapToDestination(bool applyScale, bool applyRotation)
	{
	}

	private Vector3 GetDestinationWorldPosition()
	{
		return default(Vector3);
	}

	private Vector3 ComputeTargetLocalScale()
	{
		return default(Vector3);
	}

	private void ResolveCyclerReference()
	{
	}

	private void ResolveDraggableReference()
	{
	}

	private bool ResolveDestinationByTag(bool logWarnings)
	{
		return false;
	}

	private static float SmoothStep01(float t)
	{
		return 0f;
	}
}
