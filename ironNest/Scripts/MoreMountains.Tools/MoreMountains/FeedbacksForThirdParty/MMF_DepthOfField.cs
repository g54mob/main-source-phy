using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to control depth of field focus distance, aperture and focal length over time. It requires you have in your scene an object with a PostProcessVolume with Depth of Field active, and a MMDepthOfFieldShaker component.")]
	[FeedbackPath("PostProcess/Depth Of Field")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.PostProcessing", null)]
	public class MMF_DepthOfField : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Depth Of Field", true, 51, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float ShakeDuration;

		[Tooltip("whether or not to add to the initial values")]
		public bool RelativeValues;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[MMFInspectorGroup("Focus Distance", true, 52, false, false)]
		[Tooltip("the curve used to animate the focus distance value on")]
		public AnimationCurve ShakeFocusDistance;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapFocusDistanceZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapFocusDistanceOne;

		[MMFInspectorGroup("Aperture", true, 53, false, false)]
		[Tooltip("the curve used to animate the aperture value on")]
		public AnimationCurve ShakeAperture;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0.1f, 32f)]
		public float RemapApertureZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0.1f, 32f)]
		public float RemapApertureOne;

		[MMFInspectorGroup("Focal Length", true, 54, false, false)]
		[Tooltip("the curve used to animate the focal length value on")]
		public AnimationCurve ShakeFocalLength;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 300f)]
		public float RemapFocalLengthZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 300f)]
		public float RemapFocalLengthOne;

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
