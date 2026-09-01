using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class ToggleObjectOnOffOnEnable : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CToggleLoop_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ToggleObjectOnOffOnEnable _003C_003E4__this;

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
		public _003CToggleLoop_003Ed__9(int _003C_003E1__state)
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

	[Header("Target")]
	[Tooltip("The GameObject that will be enabled/disabled (SetActive) repeatedly while this component is enabled.\n\nNotes:\n- If Target is null, nothing happens.\n- Toggling uses GameObject.SetActive, so it affects the entire hierarchy under Target.\n- Target can be this same GameObject, but disabling it will also disable this component and stop toggling.")]
	[SerializeField]
	private GameObject _target;

	[Header("Timing (seconds)")]
	[Tooltip("How long Target stays ON (active) each cycle, in seconds.\n\nSafe defaults: 0.1–1.0.\nValues <= 0 will be treated as 0 (immediate switch to OFF).")]
	[SerializeField]
	private float _onDuration;

	[Tooltip("How long Target stays OFF (inactive) each cycle, in seconds.\n\nSafe defaults: 0.1–1.0.\nValues <= 0 will be treated as 0 (immediate switch to ON).")]
	[SerializeField]
	private float _offDuration;

	[Header("Behaviour")]
	[Tooltip("If true, when this component is enabled it will immediately force Target ON before waiting On Duration.\nIf false, it will immediately force Target OFF before waiting Off Duration.")]
	[SerializeField]
	private bool _startOn;

	[Tooltip("If true, when this component is disabled it restores Target to the exact active state it had when this component was enabled.\n\nIf false, Target will be left in whatever state it was last toggled to.")]
	[SerializeField]
	private bool _restoreOriginalStateOnDisable;

	private bool _originalTargetActive;

	private Coroutine _routine;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	[IteratorStateMachine(typeof(_003CToggleLoop_003Ed__9))]
	private IEnumerator ToggleLoop()
	{
		return null;
	}
}
