using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Audio/AudioSource Volume")]
	[FeedbackHelp("This feedback lets you control the volume of a target AudioSource over time.")]
	public class MMF_AudioSourceVolume : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("AudioSource Volume", true, 87, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeVolume;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve VolumeTween;

		[Range(-1f, 1f)]
		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapVolumeZero;

		[Range(-1f, 1f)]
		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapVolumeOne;

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
