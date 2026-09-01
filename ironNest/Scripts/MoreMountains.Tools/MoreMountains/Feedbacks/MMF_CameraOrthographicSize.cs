using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Camera/Orthographic Size")]
	[FeedbackHelp("This feedback lets you control a camera's orthographic size over time. You'll need a MMCameraOrthographicSizeShaker on your camera.")]
	public class MMF_CameraOrthographicSize : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Orthographic Size", true, 41, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeOrthographicSize;

		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeOrthographicSize;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapOrthographicSizeZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapOrthographicSizeOne;

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
