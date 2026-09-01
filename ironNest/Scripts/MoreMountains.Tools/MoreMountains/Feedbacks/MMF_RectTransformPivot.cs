using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you control the position of a RectTransform's pivot over time")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("UI/RectTransform Pivot")]
	public class MMF_RectTransformPivot : MMF_FeedbackBase
	{
		[MMFInspectorGroup("Target RectTransform", true, 37, true, false)]
		[Tooltip("the RectTransform whose position you want to control over time")]
		public RectTransform TargetRectTransform;

		[MMFInspectorGroup("Pivot", true, 39, false, false)]
		[Tooltip("The curve along which to evaluate the position of the RectTransform's pivot")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public MMTweenType SpeedCurve;

		[Tooltip("the position to remap the curve's 0 to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		[MMFVector(new string[] { "Min", "Max" })]
		public Vector2 RemapZero;

		[Tooltip("the position to remap the curve's 1 to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
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
