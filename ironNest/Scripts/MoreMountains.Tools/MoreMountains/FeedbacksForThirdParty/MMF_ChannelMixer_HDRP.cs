using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.HDRP", null)]
	[FeedbackHelp("This feedback allows you to control channel mixer's red, green and blue over time.It requires you have in your scene an object with a Volumewith Channel Mixer active, and a MM Channel Mixer HDRP component.")]
	public class MMF_ChannelMixer_HDRP : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Color Grading", true, 10, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float ShakeDuration;

		[Tooltip("whether or not to add to the initial intensity")]
		public bool RelativeIntensity;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[MMFInspectorGroup("Red", true, 13, false, false)]
		[Tooltip("the curve used to animate the red value on")]
		public AnimationCurve ShakeRed;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-200f, 200f)]
		public float RemapRedZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-200f, 200f)]
		public float RemapRedOne;

		[MMFInspectorGroup("Green", true, 12, false, false)]
		[Tooltip("the curve used to animate the green value on")]
		public AnimationCurve ShakeGreen;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-200f, 200f)]
		public float RemapGreenZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-200f, 200f)]
		public float RemapGreenOne;

		[MMFInspectorGroup("Blue", true, 11, false, false)]
		[Tooltip("the curve used to animate the blue value on")]
		public AnimationCurve ShakeBlue;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-200f, 200f)]
		public float RemapBlueZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-200f, 200f)]
		public float RemapBlueOne;

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
