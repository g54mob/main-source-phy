using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("Define zoom properties : For will set the zoom to the specified parameters for a certain duration, Set will leave them like that forever. Zoom properties include the field of view, the duration of the zoom transition (in seconds) and the zoom duration (the time the camera should remain zoomed in, in seconds). For this to work, you'll need to add a MMCameraZoom component to your Camera, or a MMCinemachineZoom if you're using virtual cameras.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Camera/Camera Zoom")]
	public class MMF_CameraZoom : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Camera Zoom", true, 72, false, false)]
		[Tooltip("the zoom mode (for : forward for TransitionDuration, static for Duration, backwards for TransitionDuration)")]
		public MMCameraZoomModes ZoomMode;

		[Tooltip("the target field of view")]
		public float ZoomFieldOfView;

		[Tooltip("the zoom transition duration")]
		public float ZoomTransitionDuration;

		[Tooltip("the duration for which the zoom is at max zoom")]
		public float ZoomDuration;

		[Tooltip("whether or not ZoomFieldOfView should add itself to the current camera's field of view value")]
		public bool RelativeFieldOfView;

		[Header("Transition Speed")]
		[Tooltip("the animation curve to apply to the zoom transition")]
		public MMTweenType ZoomTween;

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

		public override bool CanForceInitialValue => false;

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
