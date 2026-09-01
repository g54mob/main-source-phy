using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to control Beautify's chromatic aberration intensity over time. It requires you have in your scene an object with a Volume with Beautify active, and a MMBeautifyShaker component. Beautify's chromaticAberrationIntensity is clamped to [0, 0.1].")]
	[FeedbackPath("PostProcess/Beautify Chromatic Aberration")]
	public class MMF_BeautifyChromaticAberration : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Chromatic Aberration", true, 41, false, false)]
		[Tooltip("the duration of the feedback, in seconds")]
		public float ShakeDuration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[Tooltip("whether or not to add to the initial intensity")]
		public bool RelativeValues;

		[Tooltip("the value to remap the curve's 0 to. Beautify clamps chromaticAberrationIntensity to [0, 0.1]")]
		[Range(0f, 0.1f)]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the curve's 1 to. Beautify clamps this to [0, 0.1]. 0.05 is strongly visible")]
		[Range(0f, 0.1f)]
		public float RemapIntensityOne;

		[MMFInspectorGroup("Intensity", true, 42, false, false)]
		[Tooltip("the curve to animate the intensity on")]
		public AnimationCurve ShakeIntensity;

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
