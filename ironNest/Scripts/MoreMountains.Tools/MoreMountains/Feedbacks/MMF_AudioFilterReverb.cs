using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Audio/Audio Filter Reverb")]
	[FeedbackHelp("This feedback lets you control a low pass audio filter over time. You'll need a MMAudioFilterReverbShaker on your filter.")]
	public class MMF_AudioFilterReverb : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Reverb Filter", true, 28, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeReverb;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeReverb;

		[Range(-10000f, 2000f)]
		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapReverbZero;

		[Range(-10000f, 2000f)]
		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapReverbOne;

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
