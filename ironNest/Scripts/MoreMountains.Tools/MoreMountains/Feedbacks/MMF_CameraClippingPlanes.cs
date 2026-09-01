using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Camera/Clipping Planes")]
	[FeedbackHelp("This feedback lets you control a camera's clipping planes over time. You'll need a MMCameraClippingPlanesShaker on your camera.")]
	public class MMF_CameraClippingPlanes : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Clipping Planes", true, 52, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeClippingPlanes;

		[MMFInspectorGroup("Near Plane", true, 53, false, false)]
		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeNear;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapNearZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapNearOne;

		[MMFInspectorGroup("Far Plane", true, 54, false, false)]
		[Tooltip("the curve used to animate the intensity value on")]
		public AnimationCurve ShakeFar;

		[Tooltip("the value to remap the curve's 0 to")]
		public float RemapFarZero;

		[Tooltip("the value to remap the curve's 1 to")]
		public float RemapFarOne;

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
