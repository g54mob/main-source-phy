using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/MMF Player")]
	[DisallowMultipleComponent]
	public class MMF_Player : MMFeedbacks
	{
		[Serializable]
		private class MMF_FeedbackListCopy
		{
			[SerializeReference]
			public List<MMF_Feedback> FeedbackList;

			public static List<MMF_Feedback> CopyFrom(MMF_Player source)
			{
				return null;
			}
		}

		public enum AccessMethods
		{
			First = 0,
			Previous = 1,
			Closest = 2,
			Next = 3,
			Last = 4
		}

		[CompilerGenerated]
		private sealed class _003CFrameOnePlayCo_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Player _003C_003E4__this;

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
			public _003CFrameOnePlayCo_003Ed__34(int _003C_003E1__state)
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
		private sealed class _003CHandleInitialDelayCo_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Player _003C_003E4__this;

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
			public _003CHandleInitialDelayCo_003Ed__38(int _003C_003E1__state)
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
		private sealed class _003CPausedFeedbacksCo_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Player _003C_003E4__this;

			public Vector3 position;

			public float feedbacksIntensity;

			private int _003Ci_003E5__2;

			private int _003Ccount_003E5__3;

			private float _003CunscaledTimeAtEnd_003E5__4;

			private bool _003CinAutoResume_003E5__5;

			private float _003CscriptDrivenPauseStartedAt_003E5__6;

			private float _003CautoResumeDuration_003E5__7;

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
			public _003CPausedFeedbacksCo_003Ed__40(int _003C_003E1__state)
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
		private sealed class _003CPlayFeedbacksAfterFrames_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public int framesAmount;

			public MMF_Player _003C_003E4__this;

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
			public _003CPlayFeedbacksAfterFrames_003Ed__19(int _003C_003E1__state)
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
		private sealed class _003CPlayFeedbacksCoroutine_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Player _003C_003E4__this;

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
			public _003CPlayFeedbacksCoroutine_003Ed__31(int _003C_003E1__state)
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
		private sealed class _003CSkipToTheEndCo_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Player _003C_003E4__this;

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
			public _003CSkipToTheEndCo_003Ed__41(int _003C_003E1__state)
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

		[SerializeReference]
		public List<MMF_Feedback> FeedbacksList;

		public bool KeepPlayModeChanges;

		[Tooltip("if this is true, the inspector won't refresh while the feedback plays, this saves on performance but feedback inspectors' progress bars for example won't look as smooth")]
		public bool PerformanceMode;

		[Tooltip("if this is true, StopFeedbacks will be called on all feedbacks on Disable")]
		public bool StopFeedbacksOnDisable;

		[Tooltip("how many times this player has started playing")]
		[MMReadOnly]
		public int PlayCount;

		protected Type _t;

		protected float _cachedTotalDuration;

		protected bool _initialized;

		public override float TotalDuration => 0f;

		public virtual bool SkippingToTheEnd { get; protected set; }

		public virtual bool HasAutomaticShakerSetup => false;

		protected override void Awake()
		{
		}

		protected override void Start()
		{
		}

		protected virtual void InitializeFeedbackList()
		{
		}

		protected virtual void ExtraInitializationChecks()
		{
		}

		protected override void OnEnable()
		{
		}

		[IteratorStateMachine(typeof(_003CPlayFeedbacksAfterFrames_003Ed__19))]
		public virtual IEnumerator PlayFeedbacksAfterFrames(int framesAmount)
		{
			return null;
		}

		public virtual void PreInitialization()
		{
		}

		public override void Initialization(bool forceInitIfPlaying = false)
		{
		}

		public override void Initialization(GameObject owner)
		{
		}

		public override void PlayFeedbacks()
		{
		}

		public override void PlayFeedbacks(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
		}

		public override void PlayFeedbacksInReverse()
		{
		}

		public override void PlayFeedbacksInReverse(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
		}

		public override void PlayFeedbacksOnlyIfReversed()
		{
		}

		public override void PlayFeedbacksOnlyIfReversed(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
		}

		public override void PlayFeedbacksOnlyIfNormalDirection()
		{
		}

		public override void PlayFeedbacksOnlyIfNormalDirection(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
		}

		[IteratorStateMachine(typeof(_003CPlayFeedbacksCoroutine_003Ed__31))]
		public override IEnumerator PlayFeedbacksCoroutine(Vector3 position, float feedbacksIntensity = 1f, bool forceRevert = false)
		{
			return null;
		}

		protected override void PlayFeedbacksInternal(Vector3 position, float feedbacksIntensity, bool forceRevert = false)
		{
		}

		public virtual bool IsAllowedToPlay(Vector3 position)
		{
			return false;
		}

		[IteratorStateMachine(typeof(_003CFrameOnePlayCo_003Ed__34))]
		protected virtual IEnumerator FrameOnePlayCo(Vector3 position, float feedbacksIntensity, bool forceRevert = false)
		{
			return null;
		}

		protected override void PreparePlay(Vector3 position, float feedbacksIntensity, bool forceRevert = false)
		{
		}

		protected override void CheckForPauses()
		{
		}

		protected override void PlayAllFeedbacks(Vector3 position, float feedbacksIntensity, bool forceRevert = false)
		{
		}

		[IteratorStateMachine(typeof(_003CHandleInitialDelayCo_003Ed__38))]
		protected override IEnumerator HandleInitialDelayCo(Vector3 position, float feedbacksIntensity, bool forceRevert = false)
		{
			return null;
		}

		protected override void Update()
		{
		}

		[IteratorStateMachine(typeof(_003CPausedFeedbacksCo_003Ed__40))]
		protected override IEnumerator PausedFeedbacksCo(Vector3 position, float feedbacksIntensity)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CSkipToTheEndCo_003Ed__41))]
		protected virtual IEnumerator SkipToTheEndCo()
		{
			return null;
		}

		public override void StopFeedbacks()
		{
		}

		public override void StopFeedbacks(bool stopAllFeedbacks = true)
		{
		}

		public override void StopFeedbacks(Vector3 position, float feedbacksIntensity = 1f, bool stopAllFeedbacks = true)
		{
		}

		public override void ResetFeedbacks()
		{
		}

		public override void Revert()
		{
		}

		public virtual void SetDirection(Directions newDirection)
		{
		}

		public void SetDirectionTopToBottom()
		{
		}

		public void SetDirectionBottomToTop()
		{
		}

		public virtual void PlayerCompleteFeedbacks()
		{
		}

		public override void PauseFeedbacks()
		{
		}

		public virtual void RestoreInitialValues()
		{
		}

		public virtual void ForceInitialValues()
		{
		}

		public virtual void SkipToTheEnd()
		{
		}

		public override void ResumeFeedbacks()
		{
		}

		public virtual void AddFeedback(MMF_Feedback newFeedback)
		{
		}

		public new MMF_Feedback AddFeedback(Type feedbackType, bool add = true)
		{
			return null;
		}

		public override void RemoveFeedback(int id)
		{
		}

		public virtual void CopyPlayerFrom(MMF_Player source)
		{
		}

		public virtual void CopyFeedbackListFrom(MMF_Player source)
		{
		}

		public virtual void AddFeedbackListFrom(MMF_Player source)
		{
		}

		public virtual void AutomaticShakerSetup()
		{
		}

		public override bool HasFeedbackStillPlaying()
		{
			return false;
		}

		protected override void CheckForLoops()
		{
		}

		protected virtual void ComputeNewRandomDurationMultipliers()
		{
		}

		public virtual float ComputeRangeIntensityMultiplier(Vector3 position)
		{
			return 0f;
		}

		protected bool FeedbackCanPlay(MMF_Feedback feedback)
		{
			return false;
		}

		protected override void ApplyAutoRevert()
		{
		}

		public override float ApplyTimeMultiplier(float duration)
		{
			return 0f;
		}

		public virtual void ProxyDestroy(GameObject gameObjectToDestroy)
		{
		}

		public virtual void ProxyDestroy(GameObject gameObjectToDestroy, float delay)
		{
		}

		public virtual void ProxyDestroyImmediate(GameObject gameObjectToDestroy)
		{
		}

		public virtual T GetFeedbackOfType<T>(AccessMethods method, int referenceIndex) where T : MMF_Feedback
		{
			return null;
		}

		public virtual T GetFeedbackOfType<T>() where T : MMF_Feedback
		{
			return null;
		}

		public virtual List<T> GetFeedbacksOfType<T>() where T : MMF_Feedback
		{
			return null;
		}

		public virtual T GetFeedbackOfType<T>(string searchedLabel) where T : MMF_Feedback
		{
			return null;
		}

		public virtual List<T> GetFeedbacksOfType<T>(string searchedLabel) where T : MMF_Feedback
		{
			return null;
		}

		protected virtual void OnMMSetFeedbackRangeCenterEvent(Transform newTransform)
		{
		}

		protected override void OnDisable()
		{
		}

		protected override void OnValidate()
		{
		}

		public virtual void RefreshCache()
		{
		}

		public virtual void ComputeCachedTotalDuration()
		{
		}

		protected override void OnDestroy()
		{
		}

		protected void OnDrawGizmosSelected()
		{
		}
	}
}
