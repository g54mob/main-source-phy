using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the border radius of an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Border Radius")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitBorderRadius : MMF_UIToolkitFloatBase
	{
		[Tooltip("whether to modify the bottom left border radius or not")]
		public bool BottomLeft;

		[Tooltip("whether to modify the bottom right border radius or not")]
		public bool BottomRight;

		[Tooltip("whether to modify the top left border radius or not")]
		public bool TopLeft;

		[Tooltip("whether to modify the top right border radius or not")]
		public bool TopRight;

		protected override void SetValue(float newValue)
		{
		}

		protected override float GetInitialValue()
		{
			return 0f;
		}
	}
}
