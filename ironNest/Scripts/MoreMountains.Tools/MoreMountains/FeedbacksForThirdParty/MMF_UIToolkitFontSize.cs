using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the font size of an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Font Size")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitFontSize : MMF_UIToolkitFloatBase
	{
		protected override void SetValue(float newValue)
		{
		}

		protected override float GetInitialValue()
		{
			return 0f;
		}
	}
}
