using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to control Panini Projection distance and crop to fit over time. It requires you have in your scene an object with a Volume with PaniniProjection active, and a MMPaniniProjectionShaker_HDRP component.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.HDRP", null)]
	public class MMF_PaniniProjection_HDRP : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Panini Projection", true, 26, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[MMFInspectorGroup("Distance", true, 27, false, false)]
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeDistance;

		[Tooltip("the curve used to animate the distance value on")]
		public AnimationCurve ShakeDistance;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(0f, 1f)]
		public float RemapDistanceZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(0f, 1f)]
		public float RemapDistanceOne;

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
