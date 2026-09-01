using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.HDRP", null)]
	[FeedbackHelp("This feedback allows you to control Exposure intensity over time. It requires you have in your scene an object with a Volume with Exposure active, and a MMExposureShaker_HDRP component.")]
	public class MMF_Exposure_HDRP : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Exposure", true, 17, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[MMFInspectorGroup("Intensity", true, 18, false, false)]
		[Tooltip("the curve to animate the intensity on")]
		public AnimationCurve FixedExposure;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapFixedExposureZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapFixedExposureOne;

		[Tooltip("whether or not to add to the initial intensity")]
		public bool RelativeFixedExposure;

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

		public override bool HasRandomness => false;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}

		public override void AutomaticShakerSetup()
		{
		}
	}
}
