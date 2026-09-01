using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackPath("PostProcess/Motion Blur URP")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.URP", null)]
	[FeedbackHelp("This feedback allows you to control motion blur intensity over time. It requires you have in your scene an object with a Volume with MotionBlur active, and a MMMotionBlurShaker_URP component.")]
	public class MMF_MotionBlur_URP : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Motion Blur", true, 25, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[MMFInspectorGroup("Intensity", true, 24, false, false)]
		[Tooltip("the curve to animate the intensity on")]
		public AnimationCurve Intensity;

		[Tooltip("the value to which to remap the curve's zero to")]
		[Range(0f, 1f)]
		public float RemapIntensityZero;

		[Tooltip("the value to which to remap the curve's one to")]
		[Range(0f, 1f)]
		public float RemapIntensityOne;

		[Tooltip("whether or not to add to the initial intensity")]
		public bool RelativeIntensity;

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
