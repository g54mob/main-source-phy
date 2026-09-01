using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to control one or more target MMF Players")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Feedbacks/MMF Player Control")]
	public class MMF_PlayerControl : MMF_Feedback
	{
		public enum Modes
		{
			PlayFeedbacks = 0,
			StopFeedbacks = 1,
			PauseFeedbacks = 2,
			ResumeFeedbacks = 3,
			Initialization = 4,
			PlayFeedbacksInReverse = 5,
			PlayFeedbacksOnlyIfReversed = 6,
			PlayFeedbacksOnlyIfNormalDirection = 7,
			ResetFeedbacks = 8,
			Revert = 9,
			SetDirectionTopToBottom = 10,
			SetDirectionBottomToTop = 11,
			RestoreInitialValues = 12,
			SkipToTheEnd = 13,
			RefreshCache = 14
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("MMF Player", true, 79, false, false)]
		[Tooltip("a specific MMFeedbacks / MMF_Player to play")]
		public List<MMF_Player> TargetPlayers;

		[Tooltip("if this is true, this feedback will be considered as Playing while any of the target players are still Playing")]
		public bool WaitForTargetPlayersToFinish;

		public Modes Mode;

		public override bool HasChannel => false;

		public override float FeedbackDuration => 0f;

		public override bool IsPlaying => false;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
