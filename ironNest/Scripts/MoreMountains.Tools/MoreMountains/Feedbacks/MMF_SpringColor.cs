using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("A feedback used to pilot color springs")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Springs/Spring Color")]
	public class MMF_SpringColor : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Spring", true, 72, false, false)]
		[Tooltip("the Color spring we want to pilot using this feedback. If you set one, only that spring will be targeted. If you don't, an event will be sent out to all springs matching the channel data info")]
		public MMSpringComponentBase TargetSpring;

		[Tooltip("the duration for the player to consider. This won't impact your particle system, but is a way to communicate to the MMF Player the duration of this feedback. Usually you'll want it to match your actual particle system, and setting it can be useful to have this feedback work with holding pauses.")]
		public float DeclaredDuration;

		[Tooltip("the command to use on that spring")]
		public SpringCommands Command;

		[MMEnumCondition("Command", new int[] { 0, 1, 2, 4 })]
		[Tooltip("the new color this spring should move towards")]
		public Color MoveToColor;

		[Tooltip("the color to add to the spring's current velocity to disturb it and make it bump")]
		[MMEnumCondition("Command", new int[] { 5 })]
		public Color BumpColor;

		[Tooltip("the min color from which to pick a random color in MoveToRandom mode")]
		[MMEnumCondition("Command", new int[] { 3 })]
		public Color MoveToRandomColorMin;

		[Tooltip("the max color from which to pick a random color in MoveToRandom mode")]
		[MMEnumCondition("Command", new int[] { 3 })]
		public Color MoveToRandomColorMax;

		[Tooltip("the min color from which to pick a random color in BumpRandom mode")]
		[MMEnumCondition("Command", new int[] { 6 })]
		public Color BumpRandomColorMin;

		[Tooltip("the max color from which to pick a random color in BumpRandom mode")]
		[MMEnumCondition("Command", new int[] { 6 })]
		public Color BumpRandomColorMax;

		[Header("Overrides")]
		[Tooltip("whether or not to override the current Damping value of the target spring(s) with the one specified below (NewDamping)")]
		public bool OverrideDamping;

		[Tooltip("the new damping value to apply to the target spring(s) if OverrideDamping is true")]
		[MMFCondition("OverrideDamping", true)]
		public float NewDamping;

		[Tooltip("whether or not to override the current Frequency value of the target spring(s) with the one specified below (NewFrequency)")]
		public bool OverrideFrequency;

		[Tooltip("the new frequency value to apply to the target spring(s) if OverrideFrequency is true")]
		[MMFCondition("OverrideFrequency", true)]
		public float NewFrequency;

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
