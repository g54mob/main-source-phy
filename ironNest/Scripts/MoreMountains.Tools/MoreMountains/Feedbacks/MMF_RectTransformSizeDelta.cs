using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you control the size delta property (the size of this RectTransform relative to the distances between the anchors) of a RectTransform, over time")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("UI/RectTransformSizeDelta")]
	public class MMF_RectTransformSizeDelta : MMF_FeedbackBase
	{
		[MMFInspectorGroup("Target RectTransform", true, 37, true, false)]
		[Tooltip("the rect transform we want to impact")]
		public RectTransform TargetRectTransform;

		[MMFInspectorGroup("Size Delta", true, 38, false, false)]
		[Tooltip("the speed at which we should animate the size delta")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public MMTweenType SpeedCurve;

		[Tooltip("the value to remap the curve's 0 to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		[MMFVector(new string[] { "Min", "Max" })]
		public Vector2 RemapZero;

		[Tooltip("the value to remap the curve's 1 to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		[MMFVector(new string[] { "Min", "Max" })]
		public Vector2 RemapOne;

		public override bool HasAutomatedTargetAcquisition => false;

		public override bool CanForceInitialValue => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void FillTargets()
		{
		}
	}
}
