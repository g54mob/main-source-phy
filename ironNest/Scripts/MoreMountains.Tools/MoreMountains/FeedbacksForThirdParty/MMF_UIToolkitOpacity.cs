using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the opacity of an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Opacity")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitOpacity : MMF_UIToolkitFloatBase
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
