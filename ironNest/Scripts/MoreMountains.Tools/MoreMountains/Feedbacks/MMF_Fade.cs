using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you trigger a fade event.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Camera/Fade")]
	public class MMF_Fade : MMF_Feedback
	{
		public enum FadeTypes
		{
			FadeIn = 0,
			FadeOut = 1,
			Custom = 2
		}

		public enum PositionModes
		{
			FeedbackPosition = 0,
			Transform = 1,
			WorldPosition = 2,
			Script = 3
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Fade", true, 43, false, false)]
		[Tooltip("the type of fade we want to use when this feedback gets played")]
		public FadeTypes FadeType;

		[Tooltip("the ID of the fader(s) to pilot")]
		public int ID;

		[Tooltip("the duration (in seconds) of the fade")]
		public float Duration;

		[Tooltip("the curve to use for this fade")]
		public MMTweenType Curve;

		[Tooltip("whether or not this fade should ignore timescale")]
		public bool IgnoreTimeScale;

		[Header("Custom")]
		[Tooltip("the target alpha we're aiming for with this fade")]
		public float TargetAlpha;

		[Header("Position")]
		[Tooltip("the chosen way to position the fade")]
		public PositionModes PositionMode;

		[Tooltip("the transform on which to center the fade")]
		[MMFEnumCondition("PositionMode", new int[] { 1 })]
		public Transform TargetTransform;

		[Tooltip("the coordinates on which to center the fade")]
		[MMFEnumCondition("PositionMode", new int[] { 2 })]
		public Vector3 TargetPosition;

		[Tooltip("the position offset to apply when centering the fade")]
		public Vector3 PositionOffset;

		[Header("Optional Target")]
		[Tooltip("this field lets you bind a specific MMFader to this feedback. If left empty, the feedback will trigger a MMFadeEvent instead, targeting all matching faders. If you fill it, only that specific fader will be targeted.")]
		public MMFader TargetFader;

		protected Vector3 _position;

		protected FadeTypes _fadeType;

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

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual Vector3 GetPosition(Vector3 position)
		{
			return default(Vector3);
		}

		protected override void CustomRestoreInitialValues()
		{
		}

		public override void AutomaticShakerSetup()
		{
		}
	}
}
