using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will allow you to cross fade a target Animator to the specified state.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Animation/Animation Crossfade")]
	public class MMF_AnimationCrossfade : MMF_Feedback
	{
		public enum TriggerModes
		{
			SetTrigger = 0,
			ResetTrigger = 1
		}

		public enum ValueModes
		{
			None = 0,
			Constant = 1,
			Random = 2,
			Incremental = 3
		}

		public enum Modes
		{
			Seconds = 0,
			Normalized = 1
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Animation", true, 12, true, false)]
		[Tooltip("the animator whose parameters you want to update")]
		public Animator BoundAnimator;

		[Tooltip("the list of extra animators whose parameters you want to update")]
		public List<Animator> ExtraBoundAnimators;

		[Tooltip("the duration for the player to consider. This won't impact your animation, but is a way to communicate to the MMF Player the duration of this feedback. Usually you'll want it to match your actual animation, and setting it can be useful to have this feedback work with holding pauses.")]
		public float DeclaredDuration;

		[MMFInspectorGroup("CrossFade", true, 16, false, false)]
		[Tooltip("the name of the state towards which to transition. That's the name of the yellow or gray box in your Animator")]
		public string StateName;

		[Tooltip("the ID of the Animator layer you want the crossfade to occur on")]
		public int Layer;

		[Tooltip("whether to specify timing data for the crossfade in seconds or in normalized (0-1) values")]
		public Modes Mode;

		[Tooltip("in Seconds mode, the duration of the transition, in seconds")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float TransitionDuration;

		[Tooltip("in Seconds mode, the offset at which to transition to, in seconds")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float TimeOffset;

		[Tooltip("in Normalized mode, the duration of the transition, normalized between 0 and 1")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float NormalizedTransitionDuration;

		[Tooltip("in Normalized mode, the offset at which to transition to, normalized between 0 and 1")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float NormalizedTimeOffset;

		[Tooltip("according to Unity's docs, 'the time of the transition, normalized'. Really nobody's sure what this does. It's optional.")]
		public float NormalizedTransitionTime;

		protected int _stateHashName;

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

		public override bool HasRandomness => false;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void CrossFade(Animator targetAnimator)
		{
		}
	}
}
