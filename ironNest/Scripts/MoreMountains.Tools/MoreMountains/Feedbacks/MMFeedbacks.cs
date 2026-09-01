using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	public class MMFeedbacks : MonoBehaviour
	{
		public enum Directions
		{
			TopToBottom = 0,
			BottomToTop = 1
		}

		public enum SafeModes
		{
			Nope = 0,
			EditorOnly = 1,
			RuntimeOnly = 2,
			Full = 3
		}

		public enum InitializationModes
		{
			Script = 0,
			Awake = 1,
			Start = 2
		}

		[CompilerGenerated]
		private sealed class _003CHandleInitialDelayCo_003Ed__95 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMFeedbacks _003C_003E4__this;

			public Vector3 position;

			public float feedbacksIntensity;

			public bool forceRevert;

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
			public _003CHandleInitialDelayCo_003Ed__95(int _003C_003E1__state)
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
		private sealed class _003CPausedFeedbacksCo_003Ed__98 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

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
			public _003CPausedFeedbacksCo_003Ed__98(int _003C_003E1__state)
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
		private sealed class _003CPlayFeedbacksCoroutine_003Ed__90 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMFeedbacks _003C_003E4__this;

			public Vector3 position;

			public float feedbacksIntensity;

			public bool forceRevert;

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
			public _003CPlayFeedbacksCoroutine_003Ed__90(int _003C_003E1__state)
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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayFeedbacksTask_003Ed__81 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public MMFeedbacks _003C_003E4__this;

			public Vector3 position;

			public float feedbacksIntensity;

			public bool forceRevert;

			private YieldAwaitable.YieldAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayFeedbacksTask_003Ed__82 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public MMFeedbacks _003C_003E4__this;

			private YieldAwaitable.YieldAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		public List<MMFeedback> Feedbacks;

		[Tooltip("the chosen initialization modes. If you use Script, you'll have to initialize manually by calling the Initialization method and passing it an owner. Otherwise, you can have this component initialize itself at Awake or Start, and in this case the owner will be the MMFeedbacks itself")]
		public InitializationModes InitializationMode;

		[Tooltip("if you set this to true, the system will make changes to ensure that initialization always happens before play")]
		public bool AutoInitialization;

		[Tooltip("the selected safe mode")]
		public SafeModes SafeMode;

		[Tooltip("the selected direction these feedbacks should play in")]
		public Directions Direction;

		[Tooltip("whether or not this MMFeedbacks should invert its direction when all feedbacks have played")]
		public bool AutoChangeDirectionOnEnd;

		[Tooltip("whether or not to play this feedbacks automatically on Start")]
		public bool AutoPlayOnStart;

		[Tooltip("whether or not to play this feedbacks automatically on Enable")]
		public bool AutoPlayOnEnable;

		[Tooltip("if this is true, all feedbacks within that player will work on the specified ForcedTimescaleMode, regardless of their individual settings")]
		public bool ForceTimescaleMode;

		[Tooltip("the time scale mode all feedbacks on this player should work on, if ForceTimescaleMode is true")]
		[MMFCondition("ForceTimescaleMode", true)]
		public TimescaleModes ForcedTimescaleMode;

		[Tooltip("a time multiplier that will be applied to all feedback durations (initial delay, duration, delay between repeats...)")]
		public float DurationMultiplier;

		[Tooltip("a multiplier to apply to all timescale operations (1: normal, less than 1: slower operations, higher than 1: faster operations)")]
		public float TimescaleMultiplier;

		[Tooltip("if this is true, will expose a RandomDurationMultiplier. The final duration of each feedback will be : their base duration * DurationMultiplier * a random value between RandomDurationMultiplier.x and RandomDurationMultiplier.y")]
		public bool RandomizeDuration;

		[Tooltip("if RandomizeDuration is true, the min (x) and max (y) values for the random duration multiplier")]
		[MMCondition("RandomizeDuration", true)]
		public Vector2 RandomDurationMultiplier;

		[Tooltip("if this is true, more editor-only, detailed info will be displayed per feedback in the duration slot")]
		public bool DisplayFullDurationDetails;

		[Tooltip("the timescale at which the player itself will operate. This notably impacts sequencing and pauses duration evaluation.")]
		public TimescaleModes PlayerTimescaleMode;

		[Tooltip("if this is true, this feedback will only play if its distance to RangeCenter is lower or equal to RangeDistance")]
		public bool OnlyPlayIfWithinRange;

		[Tooltip("when in OnlyPlayIfWithinRange mode, the transform to consider as the center of the range")]
		public Transform RangeCenter;

		[Tooltip("when in OnlyPlayIfWithinRange mode, the distance to the center within which the feedback will play")]
		public float RangeDistance;

		[Tooltip("when in OnlyPlayIfWithinRange mode, whether or not to modify the intensity of feedbacks based on the RangeFallOff curve")]
		public bool UseRangeFalloff;

		[Tooltip("the animation curve to use to define falloff (on the x 0 represents the range center, 1 represents the max distance to it)")]
		[MMFCondition("UseRangeFalloff", true)]
		public AnimationCurve RangeFalloff;

		[Tooltip("the values to remap the falloff curve's y axis' 0 and 1")]
		[MMFVector(new string[] { "Zero", "One" })]
		public Vector2 RemapRangeFalloff;

		[Tooltip("whether or not to ignore MMSetFeedbackRangeCenterEvent, used to set the RangeCenter from anywhere")]
		public bool IgnoreRangeEvents;

		[Tooltip("a duration, in seconds, during which triggering a new play of this MMFeedbacks after it's been played once will be impossible")]
		public float CooldownDuration;

		[Tooltip("a duration, in seconds, to delay the start of this MMFeedbacks' contents play")]
		public float InitialDelay;

		[Tooltip("whether this player can be played or not, useful to temporarily prevent play from another class, for example")]
		public bool CanPlay;

		[Tooltip("if this is true, you'll be able to trigger a new Play while this feedback is already playing, otherwise you won't be able to")]
		public bool CanPlayWhileAlreadyPlaying;

		[Tooltip("the chance of this sequence happening (in percent : 100 : happens all the time, 0 : never happens, 50 : happens once every two calls, etc)")]
		[Range(0f, 100f)]
		public float ChanceToPlay;

		[Tooltip("the intensity at which to play this feedback. That value will be used by most feedbacks to tune their amplitude. 1 is normal, 0.5 is half power, 0 is no effect.Note that what this value controls depends from feedback to feedback, don't hesitate to check the code to see what it does exactly.")]
		public float FeedbacksIntensity;

		[Tooltip("a number of UnityEvents that can be triggered at the various stages of this MMFeedbacks")]
		public MMFeedbacksEvents Events;

		[Tooltip("a global switch used to turn all feedbacks on or off globally")]
		public static bool GlobalMMFeedbacksActive;

		[HideInInspector]
		public bool DebugActive;

		protected float _startTime;

		protected float _holdingMax;

		protected float _lastStartAt;

		protected int _lastStartFrame;

		protected bool _pauseFound;

		protected float _totalDuration;

		protected bool _shouldStop;

		protected const float _smallValue = 0.001f;

		protected float _randomDurationMultiplier;

		protected float _lastOnEnableFrame;

		public bool IsPlaying { get; protected set; }

		public virtual float ElapsedTime => 0f;

		public int TimesPlayed { get; protected set; }

		public bool InScriptDrivenPause { get; set; }

		public bool ContainsLoop { get; set; }

		public bool ShouldRevertOnNextPlay { get; set; }

		public bool ForcingUnscaledTimescaleMode => false;

		public virtual float TotalDuration => 0f;

		public virtual float ComputedInitialDelay => 0f;

		public virtual float GetTime()
		{
			return 0f;
		}

		public virtual float GetDeltaTime()
		{
			return 0f;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnEnable()
		{
		}

		public virtual void Initialization(bool forceInitIfPlaying = false)
		{
		}

		public virtual void Initialization(GameObject owner)
		{
		}

		public virtual void PlayFeedbacks()
		{
		}

		[AsyncStateMachine(typeof(_003CPlayFeedbacksTask_003Ed__81))]
		public virtual Task PlayFeedbacksTask(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
			return null;
		}

		[AsyncStateMachine(typeof(_003CPlayFeedbacksTask_003Ed__82))]
		public virtual Task PlayFeedbacksTask()
		{
			return null;
		}

		public virtual void PlayFeedbacks(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
		}

		public virtual void PlayFeedbacksInReverse()
		{
		}

		public virtual void PlayFeedbacksInReverse(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
		}

		public virtual void PlayFeedbacksOnlyIfReversed()
		{
		}

		public virtual void PlayFeedbacksOnlyIfReversed(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
		}

		public virtual void PlayFeedbacksOnlyIfNormalDirection()
		{
		}

		public virtual void PlayFeedbacksOnlyIfNormalDirection(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
		}

		[IteratorStateMachine(typeof(_003CPlayFeedbacksCoroutine_003Ed__90))]
		public virtual IEnumerator PlayFeedbacksCoroutine(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
			return null;
		}

		protected virtual void PlayFeedbacksInternal(Vector3 position, float feedbacksIntensity, bool forceRevert = false)
		{
		}

		protected virtual void PreparePlay(Vector3 position, float feedbacksIntensity, bool forceRevert = false)
		{
		}

		protected virtual void CheckForPauses()
		{
		}

		protected virtual void PlayAllFeedbacks(Vector3 position, float feedbacksIntensity, bool forceRevert = false)
		{
		}

		[IteratorStateMachine(typeof(_003CHandleInitialDelayCo_003Ed__95))]
		protected virtual IEnumerator HandleInitialDelayCo(Vector3 position, float feedbacksIntensity, bool forceRevert = false)
		{
			return null;
		}

		protected virtual void Update()
		{
		}

		public virtual bool HasFeedbackStillPlaying()
		{
			return false;
		}

		[IteratorStateMachine(typeof(_003CPausedFeedbacksCo_003Ed__98))]
		protected virtual IEnumerator PausedFeedbacksCo(Vector3 position, float feedbacksIntensity)
		{
			return null;
		}

		public virtual void StopFeedbacks()
		{
		}

		public virtual void StopFeedbacks(bool stopAllFeedbacks = true)
		{
		}

		public virtual void StopFeedbacks(Vector3 position, float feedbacksIntensity = 1f, bool stopAllFeedbacks = true)
		{
		}

		public virtual void ResetFeedbacks()
		{
		}

		public virtual void Revert()
		{
		}

		public virtual void SetCanPlay(bool newState)
		{
		}

		public virtual void PauseFeedbacks()
		{
		}

		public virtual void ResumeFeedbacks()
		{
		}

		public virtual MMFeedback AddFeedback(Type feedbackType, bool add = true)
		{
			return null;
		}

		public virtual void RemoveFeedback(int id)
		{
		}

		protected virtual bool EvaluateChance()
		{
			return false;
		}

		protected virtual void CheckForLoops()
		{
		}

		protected bool FeedbackCanPlay(MMFeedback feedback)
		{
			return false;
		}

		protected virtual void ApplyAutoRevert()
		{
		}

		public virtual float ApplyTimeMultiplier(float duration)
		{
			return 0f;
		}

		public virtual void AutoRepair()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnValidate()
		{
		}

		protected virtual void OnDestroy()
		{
		}
	}
}
