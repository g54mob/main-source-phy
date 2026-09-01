using System;
using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public class MMFeedbacksEvents
	{
		[Tooltip("whether or not this MMFeedbacks should fire MMFeedbacksEvents")]
		public bool TriggerMMFeedbacksEvents;

		[Tooltip("whether or not this MMFeedbacks should fire Unity Events")]
		public bool TriggerUnityEvents;

		[Tooltip("This event will fire every time this MMFeedbacks gets played")]
		public UnityEvent OnPlay;

		[Tooltip("This event will fire every time this MMFeedbacks starts a holding pause")]
		public UnityEvent OnPause;

		[Tooltip("This event will fire every time this MMFeedbacks resumes after a holding pause")]
		public UnityEvent OnResume;

		[Tooltip("This event will fire every time this MMFeedbacks reverts its play direction")]
		public UnityEvent OnRevert;

		[Tooltip("This event will fire every time this MMFeedbacks plays its last MMFeedback")]
		public UnityEvent OnComplete;

		[Tooltip("This event will fire every time this MMFeedbacks gets restored to its initial values")]
		public UnityEvent OnRestoreInitialValues;

		[Tooltip("This event will fire every time this MMFeedbacks gets skipped to the end")]
		public UnityEvent OnSkipToTheEnd;

		[Tooltip("This event will fire after the MMF Player is done initializing")]
		public UnityEvent OnInitializationComplete;

		[Tooltip("This event will fire every time this MMFeedbacks' game object gets enabled")]
		public UnityEvent OnEnable;

		[Tooltip("This event will fire every time this MMFeedbacks' game object gets disabled")]
		public UnityEvent OnDisable;

		public virtual bool OnPlayIsNull { get; protected set; }

		public virtual bool OnPauseIsNull { get; protected set; }

		public virtual bool OnResumeIsNull { get; protected set; }

		public virtual bool OnRevertIsNull { get; protected set; }

		public virtual bool OnCompleteIsNull { get; protected set; }

		public virtual bool OnRestoreInitialValuesIsNull { get; protected set; }

		public virtual bool OnSkipToTheEndIsNull { get; protected set; }

		public virtual bool OnInitializationCompleteIsNull { get; protected set; }

		public virtual bool OnEnableIsNull { get; protected set; }

		public virtual bool OnDisableIsNull { get; protected set; }

		public virtual void Initialization()
		{
		}

		public virtual void TriggerOnPlay(MMFeedbacks source)
		{
		}

		public virtual void TriggerOnPause(MMFeedbacks source)
		{
		}

		public virtual void TriggerOnResume(MMFeedbacks source)
		{
		}

		public virtual void TriggerOnRevert(MMFeedbacks source)
		{
		}

		public virtual void TriggerOnComplete(MMFeedbacks source)
		{
		}

		public virtual void TriggerOnSkipToTheEnd(MMFeedbacks source)
		{
		}

		public virtual void TriggerOnInitializationComplete(MMFeedbacks source)
		{
		}

		public virtual void TriggerOnRestoreInitialValues(MMFeedbacks source)
		{
		}

		public virtual void TriggerOnEnable(MMF_Player source)
		{
		}

		public virtual void TriggerOnDisable(MMF_Player source)
		{
		}
	}
}
