using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will move the current 'head' of an MMFeedbacks sequence back to another feedback above in the list. What feedback the head lands on depends on your settings : you can decide to have it loop at last pause, or at the last LoopStart feedback in the list (or both). Furthermore, you can decide to have it loop multiple times and cause a pause when met.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Loop/Looper")]
	public class MMF_Looper : MMF_Pause
	{
		[MMFInspectorGroup("Loop", true, 34, false, false)]
		[Header("Loop conditions")]
		[Tooltip("if this is true, this feedback, when met, will cause the MMFeedbacks to reposition its 'head' to the first pause found above it (going from this feedback to the top), or to the start if none is found")]
		public bool LoopAtLastPause;

		[Tooltip("if this is true, this feedback, when met, will cause the MMFeedbacks to reposition its 'head' to the first LoopStart feedback found above it (going from this feedback to the top), or to the start if none is found")]
		public bool LoopAtLastLoopStart;

		[Header("Loop")]
		[Tooltip("if this is true, the looper will loop forever")]
		public bool InfiniteLoop;

		[Tooltip("how many times this loop should run")]
		[MMCondition("InfiniteLoop", true, true)]
		public int NumberOfLoops;

		[Tooltip("the amount of loops left (updated at runtime)")]
		[MMFReadOnly]
		public int NumberOfLoopsLeft;

		[Tooltip("whether we are in an infinite loop at this time or not")]
		[MMFReadOnly]
		public bool InInfiniteLoop;

		[Tooltip("whether or not to trigger a Loop MMFeedbacksEvent when this looper is reached")]
		public bool TriggerMMFeedbacksEvents;

		[Header("Events")]
		[Tooltip("a Unity Event to invoke when the looper is reached")]
		public UnityEvent OnLoop;

		public override bool LooperPause => false;

		public override float FeedbackDuration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		public virtual void TriggerOnLoop(MMFeedbacks source)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomReset()
		{
		}
	}
}
