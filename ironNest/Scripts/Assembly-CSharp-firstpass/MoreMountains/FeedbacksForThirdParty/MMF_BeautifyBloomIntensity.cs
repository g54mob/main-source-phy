using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to control Beautify's bloom intensity over time. It requires you have in your scene an object with a Volume with Beautify active, and a MMBeautifyShaker component.")]
	[FeedbackPath("PostProcess/Beautify Bloom Intensity")]
	public class MMF_BeautifyBloomIntensity : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Bloom Intensity", true, 41, false, false)]
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

		[Tooltip("the value to remap the curve's 1 to. No hard cap; 1-5 is a practical burst range")]
		public float RemapIntensityOne;

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
