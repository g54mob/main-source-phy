using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you broadcast a float value to the MMRadio system.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("GameObject/Broadcast")]
	public class MMF_Broadcast : MMF_FeedbackBase
	{
		[Header("Level")]
		[Tooltip("the curve to tween the intensity on")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public MMTweenType Curve;

		[Tooltip("the value to remap the intensity curve's 0 to")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float RemapZero;

		[Tooltip("the value to remap the intensity curve's 1 to")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float RemapOne;

		[Tooltip("the value to move the intensity to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float InstantChange;

		protected MMF_BroadcastProxy _proxy;

		public override bool HasChannel => false;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void FillTargets()
		{
		}
	}
}
