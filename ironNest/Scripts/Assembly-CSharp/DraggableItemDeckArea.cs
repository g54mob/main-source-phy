using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DraggableItemDeckArea : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CLerpToTargetRoutine_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DraggableItem item;

		public DraggableItemDeckArea _003C_003E4__this;

		public Vector3 targetLocalPos;

		public Quaternion targetLocalRot;

		private float _003Cdur_003E5__2;

		private float _003Ct_003E5__3;

		private Vector3 _003CstartPos_003E5__4;

		private Quaternion _003CstartRot_003E5__5;

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
		public _003CLerpToTargetRoutine_003Ed__34(int _003C_003E1__state)
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

	public static readonly List<DraggableItemDeckArea> AllDecks;

	[Header("Layout (Fan)")]
	[Tooltip("Total angular spread of the fan layout in degrees.\nCards are evenly distributed across this arc.\n\nSafe default: 60.")]
	public float fanAngle;

	[Tooltip("Radius of the arc used for the fan layout.\nControls how far cards bow outward (along local Y) at the edges of the fan.\n\nSafe default: 0.35.")]
	public float radius;

	[Tooltip("Local Z position of cards within the deck.\nNegative values lift cards toward the viewer (off the XY table surface).\nPositive values push cards into the table.\n\nSafe default: 0.")]
	public float cardZ;

	[Tooltip("Additional local Z offset applied to all card positions in the deck.\nStacks on top of cardZ. Use to nudge the whole deck toward or away from the viewer.\n\nSafe default: 0.")]
	public float verticalOffset;

	[Header("Linear Width Distribution")]
	[Tooltip("If greater than 0, cards are distributed linearly across this width (local X)\ninstead of a pure fan arc.\n\nSet to 0 to use legacy fan-only layout.\n\nSafe default: 0.")]
	public float deckWidth;

	[Tooltip("Blend factor (0–1) between a flat linear layout and the arc depth from the fan.\n\n0 = perfectly flat row (no Y arc depth).\n1 = full arc depth applied along local Y.\n\nSafe default: 1.")]
	[Range(0f, 1f)]
	public float depthArcBlend;

	[Tooltip("Maximum allowed gap (local units) between adjacent cards when using linear\nwidth distribution.\n\nIf the natural gap would exceed this value, the effective width is reduced\nso cards stay closer together.\n\nSet to 0 to disable the limit.\n\nSafe default: 0.")]
	public float maxGapBetweenCards;

	[Header("Deck Z Separation (Anti Z-Clipping)")]
	[Tooltip("Z offset (local units) applied per card index to prevent Z-clipping where\nadjacent cards overlap in the fan or linear layout.\n\nApplied as: localPosition.z += index * deckZSeparationStep\n\nBecause local -Z is toward the viewer, a negative value lifts each successive\ncard slightly closer to the camera, so higher-index cards always sit visually\nin front of lower-index cards.\n\nThe total Z spread across the full deck =\n    abs(deckZSeparationStep) * (cardCount - 1)\n\nRespects DraggableItem.enableStackingOffset: items with that flag disabled\nreceive no per-index Z step and are positioned at the base cardZ.\n\nSafe default: -0.0005.")]
	public float deckZSeparationStep;

	[Header("Layout Animation")]
	[Tooltip("Duration in seconds for cards to animate to their new layout positions\nwhen the deck is reordered (item added, removed, or inserted).\n\nApplies to both the shuffling cards making room AND the dropped card\nanimating into its insertion slot.\n\nSet to 0 to snap instantly with no animation.\n\nSafe default: 0.25.")]
	public float layoutAnimationDuration;

	[Header("Spawn")]
	[Tooltip("Fallback DraggableItem prefab used when a spawned entry has no per-definition\nprefab override.\n\nRequired if any DeckSpawnEntry.PrefabOverride is null.")]
	public DraggableItem Prefab_DraggableItemFallback;

	[Header("Debug")]
	[Tooltip("If true, draws layout debug lines in the Scene view.\n\nSafe default: false.")]
	public bool debugLayout;

	[Tooltip("If true, logs overlap check results to the Console.\n\nSafe default: false.")]
	public bool debugOverlap;

	[Tooltip("If true, logs spacing calculations when the gap limit reduces effective width.\n\nSafe default: false.")]
	public bool debugSpacing;

	[Tooltip("If true, logs spawn and insertion events to the Console.\n\nSafe default: false.")]
	public bool debugSpawn;

	[Header("Runtime")]
	[Tooltip("All DraggableItems currently registered in this deck. Read-only at runtime.")]
	public List<DraggableItem> items;

	private BoxCollider boxCol;

	private readonly Dictionary<DraggableItem, Coroutine> _layoutRoutines;

	public Bounds DeckBounds => default(Bounds);

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Awake()
	{
	}

	public bool Overlaps(DraggableItem item)
	{
		return false;
	}

	public float GetOverlapVolume(DraggableItem item)
	{
		return 0f;
	}

	public void AddItems(List<DeckSpawnEntry> entries, DragSurface surface, ItemSlot slot)
	{
	}

	public void AddItems(List<DeckSpawnEntry> entries, DragSurface surface, List<ItemSlot> slots)
	{
	}

	public void AddBack(DraggableItem item)
	{
	}

	public void RemoveItem(DraggableItem item)
	{
	}

	private int ComputeInsertionIndex(DraggableItem item)
	{
		return 0;
	}

	private List<float> ComputeLayoutXPositions(int count)
	{
		return null;
	}

	public void LayoutFan(bool animate = true)
	{
	}

	private void AnimateItemToTarget(DraggableItem item, Vector3 targetLocalPos, Quaternion targetLocalRot)
	{
	}

	private void StopLayoutRoutine(DraggableItem item)
	{
	}

	[IteratorStateMachine(typeof(_003CLerpToTargetRoutine_003Ed__34))]
	private IEnumerator LerpToTargetRoutine(DraggableItem item, Vector3 targetLocalPos, Quaternion targetLocalRot)
	{
		return null;
	}

	private static float SmoothStep01(float t)
	{
		return 0f;
	}
}
