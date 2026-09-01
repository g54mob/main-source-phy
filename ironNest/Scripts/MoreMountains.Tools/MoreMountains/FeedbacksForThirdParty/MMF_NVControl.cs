using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackPath("Haptics/Haptic Control")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.NiceVibrations", null)]
	[FeedbackHelp("Add this feedback to interact with haptics at a global level, stopping them all, enabling or disabling them, adjusting their global level or initializing/release the haptic engine.")]
	public class MMF_NVControl : MMF_Feedback
	{
		public enum ControlTypes
		{
			Stop = 0,
			EnableHaptics = 1,
			DisableHaptics = 2,
			AdjustHapticsLevel = 3,
			Initialize = 4,
			Release = 5
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Haptic Control", true, 24, false, false)]
		[Tooltip("the type of control order to trigger when playing this feedback - check Nice Vibrations' documentation for the exact behaviour of these")]
		public ControlTypes ControlType;

		[Tooltip("the output level when in AdjustHapticsLevel mode")]
		[MMFEnumCondition("ControlType", new int[] { 3 })]
		public float OutputLevel;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
