using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you control the offset of the lower left corner of the rectangle relative to the lower left anchor, and the offset of the upper right corner of the rectangle relative to the upper right anchor.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("UI/RectTransform Offset")]
	public class MMF_RectTransformOffset : MMF_FeedbackBase
	{
		[MMFInspectorGroup("Target RectTransform", true, 37, true, false)]
		public RectTransform TargetRectTransform;

		[MMFInspectorGroup("Offset Min", true, 40, false, false)]
		[Tooltip("whether we should modify the offset min or not")]
		public bool ModifyOffsetMin;

		[Tooltip("the curve to animate the min offset on")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public MMTweenType OffsetMinCurve;

		[Tooltip("the value to remap the min curve's 0 on")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public Vector2 OffsetMinRemapZero;

		[Tooltip("the value to remap the min curve's 1 on")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public Vector2 OffsetMinRemapOne;

		[MMFInspectorGroup("Offset Max", true, 41, false, false)]
		[Tooltip("whether we should modify the offset max or not")]
		public bool ModifyOffsetMax;

		[Tooltip("the curve to animate the max offset on")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public MMTweenType OffsetMaxCurve;

		[Tooltip("the value to remap the max curve's 0 on")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public Vector2 OffsetMaxRemapZero;

		[Tooltip("the value to remap the max curve's 1 on")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public Vector2 OffsetMaxRemapOne;

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
