using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the text color an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Text Color")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitTextColor : MMF_UIToolkitColorBase
	{
		protected override void ApplyColor(Color newColor)
		{
		}

		protected override Color GetInitialColor()
		{
			return default(Color);
		}
	}
}
