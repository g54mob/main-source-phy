using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class ReparentToTaggedParent : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CReparentEndOfFrame_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ReparentToTaggedParent _003C_003E4__this;

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
		public _003CReparentEndOfFrame_003Ed__10(int _003C_003E1__state)
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

	[Header("Target Parent")]
	[Tooltip("The tag on the target parent (e.g., your World Space Canvas or a transform above it).")]
	[SerializeField]
	private string parentTag;

	[Tooltip("If true, will verify the target has a Canvas in World Space (RenderMode.WorldSpace).")]
	[SerializeField]
	private bool requireWorldSpaceCanvas;

	[Header("Timing")]
	[Tooltip("If true, reparent at the end of the first frame to allow other scripts to finish setup.")]
	[SerializeField]
	private bool reparentAtEndOfFrame;

	[Tooltip("If true, will only attempt reparenting once (useful for pooled objects).")]
	[SerializeField]
	private bool reparentOnlyOnce;

	[Header("Misc")]
	[SerializeField]
	private bool logWarnings;

	[Header("Events")]
	[Tooltip("Fired after reparenting completes successfully.\n\nUse this to notify components on this GameObject that the hierarchy has changed\nand that localScale may have been silently recalculated by Unity.\n\nRecommended wiring:\n  → DraggableItem.ResnapshotBaseScale\n\nThis ensures DraggableItem's base scale reference matches the item's actual\nlocalScale after reparenting, preventing a scale jump on first pickup.")]
	[SerializeField]
	private UnityEvent onReparentComplete;

	private bool _done;

	private void OnEnable()
	{
	}

	private void Start()
	{
	}

	private void TryStartReparent()
	{
	}

	[IteratorStateMachine(typeof(_003CReparentEndOfFrame_003Ed__10))]
	private IEnumerator ReparentEndOfFrame()
	{
		return null;
	}

	private void DoReparent()
	{
	}
}
