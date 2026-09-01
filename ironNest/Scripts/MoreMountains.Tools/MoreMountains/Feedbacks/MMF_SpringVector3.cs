using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("A feedback used to pilot Vector3 springs")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Springs/Spring Vector3")]
	public class MMF_SpringVector3 : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Spring", true, 72, false, false)]
		[Tooltip("the Vector3 spring we want to pilot using this feedback. If you set one, only that spring will be targeted. If you don't, an event will be sent out to all springs matching the channel data info")]
		public MMSpringComponentBase TargetSpring;

		[Tooltip("the duration for the player to consider. This won't impact your particle system, but is a way to communicate to the MMF Player the duration of this feedback. Usually you'll want it to match your actual particle system, and setting it can be useful to have this feedback work with holding pauses.")]
		public float DeclaredDuration;

		[Tooltip("the command to use on that spring")]
		public SpringCommands Command;

		[MMEnumCondition("Command", new int[] { 0, 1, 2, 4 })]
		[Tooltip("the new value this spring should move towards")]
		public Vector3 MoveToValue;

		[Tooltip("the amount to add to the spring's current velocity to disturb it and make it bump")]
		[MMEnumCondition("Command", new int[] { 5 })]
		public Vector3 BumpAmount;

		[Tooltip("the min values between which a random target x value will be picked when calling MoveToRandom")]
		[MMEnumCondition("Command", new int[] { 3 })]
		public Vector3 MoveToRandomValueMin;

		[Tooltip("the min (x) and max (y) values between which a random target y value will be picked when calling MoveToRandom")]
		[MMEnumCondition("Command", new int[] { 3 })]
		public Vector3 MoveToRandomValueMax;

		[Tooltip("the min (x) and max (y) values between which a random bump x value will be picked when calling BumpRandom")]
		[MMEnumCondition("Command", new int[] { 6 })]
		public Vector3 BumpAmountRandomValueMin;

		[Tooltip("the min (x) and max (y) values between which a random bump y value will be picked when calling BumpRandom")]
		[MMEnumCondition("Command", new int[] { 6 })]
		public Vector3 BumpAmountRandomValueMax;

		[Header("Overrides")]
		[Tooltip("whether or not to override the current Damping value of the target spring(s) with the one specified below (NewDamping)")]
		public bool OverrideDamping;

		[Tooltip("the new damping value to apply to the target spring(s) if OverrideDamping is true")]
		[MMFCondition("OverrideDamping", true)]
		public Vector3 NewDamping;

		[Tooltip("whether or not to override the current Frequency value of the target spring(s) with the one specified below (NewFrequency)")]
		public bool OverrideFrequency;

		[Tooltip("the new frequency value to apply to the target spring(s) if OverrideFrequency is true")]
		[MMFCondition("OverrideFrequency", true)]
		public Vector3 NewFrequency;

		protected MMChannelData _eventChannelData;

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

		public override bool HasChannel => false;

		public override bool CanForceInitialValue => false;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
