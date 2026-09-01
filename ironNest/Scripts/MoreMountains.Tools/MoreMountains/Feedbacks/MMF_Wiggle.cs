using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you trigger position, rotation and/or scale wiggles on an object equipped with a MMWiggle component, for the specified durations.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/Wiggle")]
	public class MMF_Wiggle : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Target", true, 54, true, false)]
		[Tooltip("the Wiggle component to target")]
		public MMWiggle TargetWiggle;

		[MMFInspectorGroup("Position", true, 55, false, false)]
		[Tooltip("whether or not to wiggle position")]
		public bool WigglePosition;

		[Tooltip("the duration (in seconds) of the position wiggle")]
		public float WigglePositionDuration;

		[MMFInspectorGroup("Rotation", true, 56, false, false)]
		[Tooltip("whether or not to wiggle rotation")]
		public bool WiggleRotation;

		[Tooltip("the duration (in seconds) of the rotation wiggle")]
		public float WiggleRotationDuration;

		[MMFInspectorGroup("Scale", true, 57, false, false)]
		[Tooltip("whether or not to wiggle scale")]
		public bool WiggleScale;

		[Tooltip("the duration (in seconds) of the scale wiggle")]
		public float WiggleScaleDuration;

		public override bool HasAutomatedTargetAcquisition => false;

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

		protected override void AutomateTargetAcquisition()
		{
		}

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
