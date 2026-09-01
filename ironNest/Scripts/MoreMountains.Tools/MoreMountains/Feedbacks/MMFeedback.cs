using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	[AddComponentMenu(null)]
	[ExecuteAlways]
	public abstract class MMFeedback : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CInfinitePlay_003Ed__66 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMFeedback _003C_003E4__this;

			public Vector3 position;

			public float feedbacksIntensity;

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
			public _003CInfinitePlay_003Ed__66(int _003C_003E1__state)
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
		private sealed class _003CPlayCoroutine_003Ed__64 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMFeedback _003C_003E4__this;

			public Vector3 position;

			public float feedbacksIntensity;

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
			public _003CPlayCoroutine_003Ed__64(int _003C_003E1__state)
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
		private sealed class _003CRepeatedPlay_003Ed__67 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMFeedback _003C_003E4__this;

			public Vector3 position;

			public float feedbacksIntensity;

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
			public _003CRepeatedPlay_003Ed__67(int _003C_003E1__state)
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
		private sealed class _003CSequenceCoroutine_003Ed__68 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMFeedback _003C_003E4__this;

			public Vector3 position;

			public float feedbacksIntensity;

			private float _003CtimeStartedAt_003E5__2;

			private float _003ClastFrame_003E5__3;

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
			public _003CSequenceCoroutine_003Ed__68(int _003C_003E1__state)
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

		[Tooltip("whether or not this feedback is active")]
		public bool Active;

		[Tooltip("the name of this feedback to display in the inspector")]
		public string Label;

		[Tooltip("the chance of this feedback happening (in percent : 100 : happens all the time, 0 : never happens, 50 : happens once every two calls, etc)")]
		[Range(0f, 100f)]
		public float Chance;

		[Tooltip("a number of timing-related values (delay, repeat, etc)")]
		public MMFeedbackTiming Timing;

		[HideInInspector]
		public bool DebugActive;

		protected float _lastPlayTimestamp;

		protected int _playsLeft;

		protected bool _initialized;

		protected Coroutine _playCoroutine;

		protected Coroutine _infinitePlayCoroutine;

		protected Coroutine _sequenceCoroutine;

		protected Coroutine _repeatedPlayCoroutine;

		protected int _sequenceTrackID;

		protected MMFeedbacks _hostMMFeedbacks;

		protected float _beatInterval;

		protected bool BeatThisFrame;

		protected int LastBeatIndex;

		protected int CurrentSequenceIndex;

		protected float LastBeatTimestamp;

		protected bool _isHostMMFeedbacksNotNull;

		protected MMChannelData _channelData;

		public GameObject Owner { get; set; }

		public virtual IEnumerator Pause => null;

		public virtual bool HoldingPause => false;

		public virtual bool LooperPause => false;

		public virtual bool ScriptDrivenPause { get; set; }

		public virtual float ScriptDrivenPauseAutoResume { get; set; }

		public virtual bool LooperStart => false;

		public virtual bool InCooldown => false;

		public virtual bool IsPlaying { get; set; }

		public float FeedbackTime => 0f;

		public float FeedbackDeltaTime => 0f;

		public float TotalDuration => 0f;

		public virtual float FeedbackStartedAt => 0f;

		public virtual float FeedbackDuration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public virtual bool FeedbackPlaying => false;

		public virtual bool NormalPlayDirection => false;

		public virtual bool ShouldPlayInThisSequenceDirection => false;

		protected virtual float FinalNormalizedTime => 0f;

		public virtual MMChannelData ChannelData(int channel)
		{
			return null;
		}

		protected virtual void OnEnable()
		{
		}

		public virtual void Initialization(GameObject owner)
		{
		}

		public virtual void Play(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CPlayCoroutine_003Ed__64))]
		protected virtual IEnumerator PlayCoroutine(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		protected virtual void RegularPlay(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CInfinitePlay_003Ed__66))]
		protected virtual IEnumerator InfinitePlay(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CRepeatedPlay_003Ed__67))]
		protected virtual IEnumerator RepeatedPlay(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CSequenceCoroutine_003Ed__68))]
		protected virtual IEnumerator SequenceCoroutine(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		public virtual void Stop(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		public virtual void ResetFeedback()
		{
		}

		public virtual void SetSequence(MMSequence newSequence)
		{
		}

		public virtual void SetDelayBetweenRepeats(float delay)
		{
		}

		public virtual void SetInitialDelay(float delay)
		{
		}

		protected virtual float ApplyDirection(float normalizedTime)
		{
			return 0f;
		}

		protected virtual float ApplyTimeMultiplier(float duration)
		{
			return 0f;
		}

		protected virtual void CustomInitialization(GameObject owner)
		{
		}

		protected abstract void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f);

		protected virtual void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void CustomReset()
		{
		}
	}
}
