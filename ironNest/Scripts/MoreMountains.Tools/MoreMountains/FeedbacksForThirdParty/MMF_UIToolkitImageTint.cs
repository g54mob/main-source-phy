using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the image tint of an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Image Tint")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitImageTint : MMF_UIToolkitColorBase
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
