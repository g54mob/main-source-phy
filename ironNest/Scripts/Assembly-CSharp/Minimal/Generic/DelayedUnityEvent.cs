using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Minimal.Generic
{
	public sealed class DelayedUnityEvent : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CTimerRoutine_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public DelayedUnityEvent _003C_003E4__this;

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
			public _003CTimerRoutine_003Ed__12(int _003C_003E1__state)
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

		[Header("Trigger")]
		[SerializeField]
		[Tooltip("When enabled, the timer will (re)start in OnEnable and invoke the Action after the delay. Use this for pooled objects or components that may be disabled/enabled multiple times.")]
		private bool startOnEnable;

		[SerializeField]
		[Tooltip("When enabled, the timer will start once in Start() and invoke the Action after the delay. Note: Start() only runs once per component lifetime; if you disable/enable later, Start() will not run again.")]
		private bool startOnStart;

		[Header("Timer")]
		[SerializeField]
		[Min(0f)]
		[Tooltip("Delay in seconds before invoking the Action. 0 invokes on the next frame (after one yield), keeping behaviour consistent with the coroutine timing.")]
		private float delaySeconds;

		[SerializeField]
		[Tooltip("If true, the delay uses scaled time (affected by Time.timeScale). If false, the delay uses unscaled time (ignores Time.timeScale), e.g., still counts down during pause menus.")]
		private bool useUnscaledTime;

		[SerializeField]
		[Tooltip("If true, restarting the timer while one is already running will stop the previous timer and start a new one. If false, additional start attempts while running are ignored.")]
		private bool restartIfAlreadyRunning;

		[Header("Action")]
		[SerializeField]
		[Tooltip("Invoked after the timer completes. Assign any method(s) from the Inspector. This component is intentionally generic and does not require any specific receiver type.")]
		private UnityEvent onTimerComplete;

		private Coroutine running;

		private void OnEnable()
		{
		}

		private void Start()
		{
		}

		private void OnDisable()
		{
		}

		public void StartTimer()
		{
		}

		public void StopTimer()
		{
		}

		[IteratorStateMachine(typeof(_003CTimerRoutine_003Ed__12))]
		private IEnumerator TimerRoutine()
		{
			return null;
		}
	}
}
