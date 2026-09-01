using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to control bloom intensity and threshold over time. It requires you have in your scene an object with a PostProcessVolume with Bloom active, and a MMBloomShaker component.")]
	[FeedbackPath("PostProcess/Bloom")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.PostProcessing", null)]
	public class MMF_Bloom : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Bloom", true, 41, false, false)]
		[Tooltip("the duration of the feedback, in seconds")]
		public float ShakeDuration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[Tooltip("whether or not to add to the initial intensity")]
		public bool RelativeValues;

		[MMFInspectorGroup("Intensity", true, 42, false, false)]
		[Tooltip("the curve to animate the intensity on")]
		public AnimationCurve ShakeIntensity;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapIntensityOne;

		[MMFInspectorGroup("Threshold", true, 43, false, false)]
		[Tooltip("the curve to animate the threshold on")]
		public AnimationCurve ShakeThreshold;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapThresholdZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapThresholdOne;

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
