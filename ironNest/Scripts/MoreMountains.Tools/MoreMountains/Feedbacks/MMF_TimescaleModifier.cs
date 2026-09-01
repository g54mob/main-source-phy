using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback triggers a MMTimeScaleEvent, which, if you have a MMTimeManager object in your scene, will be caught and used to modify the timescale according to the specified settings. These settings are the new timescale (0.5 will be twice slower than normal, 2 twice faster, etc), the duration of the timescale modification, and the optional speed at which to transition between normal and altered time scale.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Time/Timescale Modifier")]
	public class MMF_TimescaleModifier : MMF_Feedback
	{
		public enum Modes
		{
			Shake = 0,
			Change = 1,
			Reset = 2,
			Unfreeze = 3
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Timescale Modifier", true, 63, false, false)]
		[Tooltip("the selected mode : shake : changes the timescale for a certain duration- change : sets the timescale to a new value, forever (until you change it again)- reset : resets the timescale to its previous value")]
		public Modes Mode;

		[Tooltip("the new timescale to apply")]
		public float TimeScale;

		[Tooltip("the duration of the timescale modification")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float TimeScaleDuration;

		[Tooltip("whether to reset the timescale on Stop or not")]
		public bool ResetTimescaleOnStop;

		[MMFInspectorGroup("Interpolation", true, 63, false, false)]
		[Tooltip("whether or not we should lerp the timescale")]
		public bool TimeScaleLerp;

		[Tooltip("whether to lerp over a set duration, or at a certain speed")]
		public MMTimeScaleLerpModes TimescaleLerpMode;

		[Tooltip("in Speed mode, the speed at which to lerp the timescale")]
		[MMFEnumCondition("TimescaleLerpMode", new int[] { 0 })]
		public float TimeScaleLerpSpeed;

		[Tooltip("in Duration mode, the curve to use to lerp the timescale")]
		[MMFEnumCondition("TimescaleLerpMode", new int[] { 1 })]
		public MMTweenType TimescaleLerpCurve;

		[Tooltip("in Duration mode, the duration of the timescale interpolation, in unscaled time seconds")]
		[MMFEnumCondition("TimescaleLerpMode", new int[] { 1 })]
		public float TimescaleLerpDuration;

		[Tooltip("whether or not we should lerp the timescale as it goes back to normal afterwards")]
		[MMFEnumCondition("TimescaleLerpMode", new int[] { 1 })]
		public bool TimeScaleLerpOnReset;

		[Tooltip("in Duration mode, the curve to use to lerp the timescale")]
		[MMFEnumCondition("TimescaleLerpMode", new int[] { 1 })]
		public MMTweenType TimescaleLerpCurveOnReset;

		[Tooltip("in Duration mode, the duration of the timescale interpolation, in unscaled time seconds")]
		[MMFEnumCondition("TimescaleLerpMode", new int[] { 1 })]
		public float TimescaleLerpDurationOnReset;

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

		protected override void CustomRestoreInitialValues()
		{
		}

		public override void AutomaticShakerSetup()
		{
		}
	}
}
