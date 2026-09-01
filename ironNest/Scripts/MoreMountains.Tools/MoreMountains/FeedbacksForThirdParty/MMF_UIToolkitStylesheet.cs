using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the stylesheet on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Stylesheet")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitStylesheet : MMF_UIToolkit
	{
		[Header("Stylesheet")]
		[Tooltip("the new stylesheet to apply to the document")]
		public StyleSheet NewStylesheet;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
