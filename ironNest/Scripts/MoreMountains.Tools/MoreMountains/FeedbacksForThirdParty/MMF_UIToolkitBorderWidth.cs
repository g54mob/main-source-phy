using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the border width of an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Border Width")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitBorderWidth : MMF_UIToolkitFloatBase
	{
		[Tooltip("whether to modify the left border width or not")]
		public bool Left;

		[Tooltip("whether to modify the right border width or not")]
		public bool Right;

		[Tooltip("whether to modify the top border width or not")]
		public bool Top;

		[Tooltip("whether to modify the bottom border width or not")]
		public bool Bottom;

		protected override void SetValue(float newValue)
		{
		}

		protected override float GetInitialValue()
		{
			return 0f;
		}
	}
}
