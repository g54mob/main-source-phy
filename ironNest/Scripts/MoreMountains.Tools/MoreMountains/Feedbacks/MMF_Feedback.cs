using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public abstract class MMF_Feedback
	{
		[CompilerGenerated]
		private sealed class _003CForceInitialValueDelayedCo_003Ed__157 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Feedback _003C_003E4__this;

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
			public _003CForceInitialValueDelayedCo_003Ed__157(int _003C_003E1__state)
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
		private sealed class _003CInfinitePlay_003Ed__149 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Feedback _003C_003E4__this;

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
			public _003CInfinitePlay_003Ed__149(int _003C_003E1__state)
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
		private sealed class _003CPlayCoroutine_003Ed__146 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Feedback _003C_003E4__this;

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
			public _003CPlayCoroutine_003Ed__146(int _003C_003E1__state)
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
		private sealed class _003CRepeatedPlay_003Ed__150 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Feedback _003C_003E4__this;

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
			public _003CRepeatedPlay_003Ed__150(int _003C_003E1__state)
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
		private sealed class _003CSequenceCoroutine_003Ed__152 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Feedback _003C_003E4__this;

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
			public _003CSequenceCoroutine_003Ed__152(int _003C_003E1__state)
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
		private sealed class _003CTriggerRepeatedPlay_003Ed__151 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Feedback _003C_003E4__this;

			public Vector3 position;

			public float feedbacksIntensity;

			private float _003CrepeatStartTime_003E5__2;

			private float _003CrepeatDuration_003E5__3;

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
			public _003CTriggerRepeatedPlay_003Ed__151(int _003C_003E1__state)
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
		private sealed class _003CWaitFor_003Ed__168 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Feedback _003C_003E4__this;

			public float delay;

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
			public _003CWaitFor_003Ed__168(int _003C_003E1__state)
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

		public const string _randomnessGroupName = "Feedback Randomness";

		public const string _rangeGroupName = "Feedback Range";

		public const string _automaticSetupGroupName = "Automatic Setup";

		[MMFInspectorGroup("Feedback Settings", true, 0, false, true)]
		[Tooltip("whether or not this feedback is active")]
		public bool Active;

		[HideInInspector]
		public int UniqueID;

		[Tooltip("the name of this feedback to display in the inspector")]
		public string Label;

		[Tooltip("whether to broadcast this feedback's message using an int or a scriptable object. Ints are simple to setup but can get messy and make it harder to remember what int corresponds to what. MMChannel scriptable objects require you to create them in advance, but come with a readable name and are more scalable")]
		public MMChannelModes ChannelMode;

		[Tooltip("the ID of the channel on which this feedback will communicate")]
		[MMEnumCondition("ChannelMode", new int[] { 0 })]
		public int Channel;

		[Tooltip("the MMChannel definition asset to use to broadcast this feedback. The shaker will have to reference that same MMChannel definition to receive events - to create a MMChannel, right click anywhere in your project (usually in a Data folder) and go MoreMountains > MMChannel, then name it with some unique name")]
		[MMEnumCondition("ChannelMode", new int[] { 1 })]
		public MMChannel MMChannelDefinition;

		[Tooltip("the chance of this feedback happening (in percent : 100 : happens all the time, 0 : never happens, 50 : happens once every two calls, etc)")]
		[Range(0f, 100f)]
		public float Chance;

		[Tooltip("use this color to customize the background color of the feedback in the MMF_Player's list")]
		public Color DisplayColor;

		[Tooltip("a number of timing-related values (delay, repeat, etc)")]
		public MMFeedbackTiming Timing;

		[Tooltip("a set of settings letting you define automated target acquisition for this feedback, to (for example) automatically grab the target on this game object, or a parent, a child, or on a reference holder")]
		public MMFeedbackTargetAcquisition AutomatedTargetAcquisition;

		[MMFInspectorGroup("Feedback Randomness", true, 58, false, true)]
		[Tooltip("if this is true, intensity will be multiplied by a random value on play, picked between RandomMultiplier.x and RandomMultiplier.y")]
		public bool RandomizeOutput;

		[Tooltip("a random value (randomized between its x and y) by which to multiply the output of this feedback, if RandomizeOutput is true")]
		[MMFCondition("RandomizeOutput", true)]
		[MMFVector(new string[] { "Min", "Max" })]
		public Vector2 RandomMultiplier;

		[Tooltip("if this is true, this feedback's duration will be multiplied by a random value on play, picked between RandomDurationMultiplier.x and RandomDurationMultiplier.y")]
		public bool RandomizeDuration;

		[Tooltip("a random value (randomized between its x and y) by which to multiply the duration of this feedback, if RandomizeDuration is true")]
		[MMFCondition("RandomizeDuration", true)]
		[MMFVector(new string[] { "Min", "Max" })]
		public Vector2 RandomDurationMultiplier;

		[MMFInspectorGroup("Feedback Range", true, 47, false, false)]
		[Tooltip("if this is true, only shakers within the specified range will respond to this feedback")]
		public bool UseRange;

		[Tooltip("when in UseRange mode, only shakers within that distance will respond to this feedback")]
		public float RangeDistance;

		[Tooltip("when in UseRange mode, whether or not to modify the shake intensity based on the RangeFallOff curve")]
		public bool UseRangeFalloff;

		[Tooltip("the animation curve to use to define falloff (on the x 0 represents the range center, 1 represents the max distance to it)")]
		public AnimationCurve RangeFalloff;

		[Tooltip("the values to remap the falloff curve's y axis' 0 and 1")]
		[MMFVector(new string[] { "Zero", "One" })]
		public Vector2 RemapRangeFalloff;

		[MMFInspectorGroup("Automatic Setup", true, 49, false, true)]
		[Tooltip("a button used to attempt an auto shaker setup for this feedback, adding whatever shaker it requires to function to the scene")]
		public MMF_Button AutomaticShakerSetupButton;

		[HideInInspector]
		public MMF_Player Owner;

		[HideInInspector]
		public bool DebugActive;

		protected float _lastPlayTimestamp;

		protected int _playsLeft;

		protected bool _initialized;

		protected Coroutine _playCoroutine;

		protected Coroutine _infinitePlayCoroutine;

		protected Coroutine _sequenceCoroutine;

		protected Coroutine _repeatedPlayCoroutine;

		protected bool _requiresSetup;

		protected string _requiredTarget;

		protected float _randomDurationMultiplier;

		protected int _sequenceTrackID;

		protected float _beatInterval;

		protected bool BeatThisFrame;

		protected int LastBeatIndex;

		protected int CurrentSequenceIndex;

		protected float LastBeatTimestamp;

		protected MMChannelData _channelData;

		protected float _totalDuration;

		protected int _indexInOwnerFeedbackList;

		protected string _requiredTargetTextCached;

		protected string _requiredTargetTextCachedExtra;

		protected float _repeatOffset;

		public virtual IEnumerator Pause => null;

		public virtual bool HoldingPause => false;

		public virtual bool LooperPause => false;

		public virtual bool ScriptDrivenPause { get; set; }

		public virtual float ScriptDrivenPauseAutoResume { get; set; }

		public virtual bool LooperStart => false;

		public virtual bool HasChannel => false;

		public virtual bool HasAutomaticShakerSetup => false;

		public virtual bool HasRandomness => false;

		public virtual bool CanForceInitialValue => false;

		public virtual bool ForceInitialValueDelayed => false;

		public virtual bool HasAutomatedTargetAcquisition => false;

		public virtual MMF_ReferenceHolder ForcedReferenceHolder { get; set; }

		public virtual bool HasRange => false;

		public virtual int PlaysLeft => 0;

		public virtual bool HasCustomInspectors => false;

		public virtual bool InCooldown => false;

		public virtual bool IsPlaying { get; set; }

		public virtual float ComputedRandomMultiplier => 0f;

		public virtual TimescaleModes ComputedTimescaleMode => default(TimescaleModes);

		public virtual bool InScaledTimescaleMode => false;

		public virtual float FeedbackTime => 0f;

		public virtual float FeedbackDeltaTime => 0f;

		public virtual float TotalDuration => 0f;

		public virtual bool IsExpanded { get; set; }

		public virtual bool RequiresSetup => false;

		public virtual string RequiredTarget => null;

		public virtual bool DrawGroupInspectors => false;

		public virtual bool DisplayFullHeaderColor => false;

		public virtual string RequiresSetupText => null;

		public virtual string RequiredTargetText => null;

		public virtual string RequiredTargetTextExtra => null;

		public virtual string RequiredChannelText => null;

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

		public virtual MMChannelData ChannelData => null;

		protected virtual float FinalNormalizedTime => 0f;

		public virtual bool NormalPlayDirection => false;

		public virtual bool ShouldPlayInThisSequenceDirection => false;

		public virtual float ComputeIntensity(float intensity, Vector3 position)
		{
			return 0f;
		}

		public virtual void CacheRequiresSetup()
		{
		}

		public virtual bool EvaluateRequiresSetup()
		{
			return false;
		}

		public virtual void SetFeedbackDuration(float newDuration)
		{
		}

		public virtual void PreInitialization(MMF_Player owner, int index)
		{
		}

		public virtual void Initialization(MMF_Player owner, int index)
		{
		}

		public virtual void SetIndexInFeedbacksList(int index)
		{
		}

		public virtual void AutomaticShakerSetup()
		{
		}

		protected virtual void AutomateTargetAcquisitionInternal()
		{
		}

		public virtual void ForceAutomateTargetAcquisition()
		{
		}

		protected virtual void AutomateTargetAcquisition()
		{
		}

		protected virtual GameObject FindAutomatedTargetGameObject()
		{
			return null;
		}

		protected virtual T FindAutomatedTarget<T>()
		{
			return default(T);
		}

		public virtual void Play(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CPlayCoroutine_003Ed__146))]
		protected virtual IEnumerator PlayCoroutine(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		protected virtual void RegularPlay(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void TriggerCustomPlay(Vector3 position, float intensity)
		{
		}

		[IteratorStateMachine(typeof(_003CInfinitePlay_003Ed__149))]
		protected virtual IEnumerator InfinitePlay(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CRepeatedPlay_003Ed__150))]
		protected virtual IEnumerator RepeatedPlay(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CTriggerRepeatedPlay_003Ed__151))]
		protected virtual IEnumerator TriggerRepeatedPlay(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CSequenceCoroutine_003Ed__152))]
		protected virtual IEnumerator SequenceCoroutine(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		public virtual void SetSequence(MMSequence newSequence)
		{
		}

		public virtual void Stop(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		public virtual void SkipToTheEnd(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		public virtual void ForceInitialValue(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CForceInitialValueDelayedCo_003Ed__157))]
		protected virtual IEnumerator ForceInitialValueDelayedCo(Vector3 position, float feedbacksIntensity = 1f)
		{
			return null;
		}

		public virtual void RestoreInitialValues()
		{
		}

		public virtual void ResetFeedback()
		{
		}

		public virtual void PlayerComplete()
		{
		}

		public virtual void SetDelayBetweenRepeats(float delay)
		{
		}

		public virtual void SetInitialDelay(float delay)
		{
		}

		public virtual void ComputeNewRandomDurationMultiplier()
		{
		}

		public virtual void ResetPlayCount()
		{
		}

		protected virtual float ApplyTimeMultiplier(float duration)
		{
			return 0f;
		}

		[IteratorStateMachine(typeof(_003CWaitFor_003Ed__168))]
		protected virtual IEnumerator WaitFor(float delay)
		{
			return null;
		}

		public virtual void ComputeTotalDuration()
		{
		}

		protected virtual float ApplyDirection(float normalizedTime)
		{
			return 0f;
		}

		protected virtual void CustomInitialization(MMF_Player owner)
		{
		}

		protected abstract void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f);

		protected virtual void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void CustomSkipToTheEnd(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void CustomRestoreInitialValues()
		{
		}

		protected virtual void CustomPlayerComplete()
		{
		}

		protected virtual void CustomReset()
		{
		}

		public virtual void InitializeCustomAttributes()
		{
		}

		public virtual void OnValidate()
		{
		}

		public virtual void OnAddFeedback()
		{
		}

		public virtual void OnDestroy()
		{
		}

		public virtual void OnDisable()
		{
		}

		public virtual void OnDrawGizmosSelectedHandler()
		{
		}
	}
}
