using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("Utility/AutoDisableAfter")]
public class AutoDisableAfter : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDisableAfterCoroutine_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float startSeconds;

		public AutoDisableAfter _003C_003E4__this;

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
		public _003CDisableAfterCoroutine_003Ed__12(int _003C_003E1__state)
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

	[SerializeField]
	[Tooltip("Time in seconds before the target GameObject will be disabled or destroyed. Set to 0 to act immediately. Examples: 0 (immediate), 2.5 (two and a half seconds). Default: 5.")]
	private float seconds;

	[SerializeField]
	[Tooltip("If true, uses unscaled time for the countdown (ignores Time.timeScale). Use when you want the timer to continue while the game is paused via timeScale. Default: false.")]
	private bool useUnscaledTime;

	[SerializeField]
	[Tooltip("Optional explicit target GameObject to disable (or destroy) when the timer ends. If left empty (None) the script will act on the GameObject this component is attached to. This same field is used for both the disable and destroy behaviours.")]
	private GameObject target;

	[SerializeField]
	[Tooltip("If true, each time the GameObject is enabled the timer restarts from the full duration. If false, re-enabling will continue an existing countdown if one is in progress. Default: true.")]
	private bool restartOnEnable;

	[SerializeField]
	[Tooltip("If true, the target GameObject is permanently destroyed (Destroy) instead of being deactivated (SetActive(false)) when the timer ends. This is irreversible at runtime - the object and all its components are removed from the scene. If the target is this GameObject, this component is destroyed along with it. Default: false (disables the target instead).")]
	private bool destroyInsteadOfDisable;

	private Coroutine runningCoroutine;

	private GameObject RuntimeTarget => null;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void StartTimerFromEnable()
	{
	}

	private void StopTimer()
	{
	}

	[IteratorStateMachine(typeof(_003CDisableAfterCoroutine_003Ed__12))]
	private IEnumerator DisableAfterCoroutine(float startSeconds)
	{
		return null;
	}

	private void ApplyEndOfTimerAction()
	{
	}
}
