using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you control the opacity of a canvas group over time.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("UI/CanvasGroup")]
	public class MMF_CanvasGroup : MMF_FeedbackBase
	{
		[MMFInspectorGroup("Canvas Group", true, 12, true, false)]
		[Tooltip("the receiver to write the level to")]
		public CanvasGroup TargetCanvasGroup;

		[Tooltip("the curve to tween the opacity on")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public MMTweenType AlphaCurve;

		[Tooltip("the value to remap the opacity curve's 0 to")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float RemapZero;

		[Tooltip("the value to remap the opacity curve's 1 to")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float RemapOne;

		[Tooltip("the value to move the opacity to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float InstantAlpha;

		public override bool HasAutomatedTargetAcquisition => false;

		public override bool CanForceInitialValue => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		public override void OnAddFeedback()
		{
		}

		protected override void FillTargets()
		{
		}
	}
}
