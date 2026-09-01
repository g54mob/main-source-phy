using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you trigger a blink on an MMBlink object.")]
	[FeedbackPath("Renderer/MMBlink")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	public class MMF_Blink : MMF_Feedback
	{
		public enum BlinkModes
		{
			Toggle = 0,
			Start = 1,
			Stop = 2
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Blink", true, 61, true, false)]
		[Tooltip("the target object to blink")]
		public MMBlink TargetBlink;

		[Tooltip("an optional list of extra target objects to blink")]
		public List<MMBlink> ExtraTargetBlinks;

		[Tooltip("the selected mode for this feedback")]
		public BlinkModes BlinkMode;

		[Tooltip("the duration of the blink. You can set it manually, or you can press the GrabDurationFromBlink button to automatically compute it. For performance reasons, this isn't updated unless you press the button, make sure you do so if you change the blink's duration.")]
		public float Duration;

		public MMF_Button GrabDurationFromBlinkButton;

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

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		public override void InitializeCustomAttributes()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void HandleBlink(MMBlink target)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}

		public virtual void GrabDurationFromBlink()
		{
		}
	}
}
