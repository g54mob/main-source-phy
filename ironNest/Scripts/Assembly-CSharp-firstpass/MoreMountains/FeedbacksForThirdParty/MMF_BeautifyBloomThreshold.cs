using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to control Beautify's bloom threshold over time. It requires you have in your scene an object with a Volume with Beautify active, and a MMBeautifyShaker component.")]
	[FeedbackPath("PostProcess/Beautify Bloom Threshold")]
	public class MMF_BeautifyBloomThreshold : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Bloom Threshold", true, 41, false, false)]
		[Tooltip("the duration of the feedback, in seconds")]
		public float ShakeDuration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[Tooltip("whether or not to add to the initial threshold")]
		public bool RelativeValues;

		[MMFInspectorGroup("Threshold", true, 42, false, false)]
		[Tooltip("the curve to animate the threshold on")]
		public AnimationCurve ShakeThreshold;

		[Tooltip("the value to remap the curve's 0 to. Beautify's default threshold is 0.75")]
		public float RemapThresholdZero;

		[Tooltip("the value to remap the curve's 1 to. Set below the resting value (e.g. 0.1) so more surfaces bloom at the shake peak")]
		public float RemapThresholdOne;

		private static readonly AnimationCurve _flatCurve;

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
	}
}
