using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the border color of an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Border Color")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitBorderColor : MMF_UIToolkitColorBase
	{
		[MMFInspectorGroup("Borders", true, 55, true, false)]
		[Tooltip("whether or not the feedback should modify the color of the left border")]
		public bool BorderLeft;

		[Tooltip("whether or not the feedback should modify the color of the right border")]
		public bool BorderRight;

		[Tooltip("whether or not the feedback should modify the color of the bottom border")]
		public bool BorderBottom;

		[Tooltip("whether or not the feedback should modify the color of the top border")]
		public bool BorderTop;

		protected override void ApplyColor(Color newColor)
		{
		}

		protected override Color GetInitialColor()
		{
			return default(Color);
		}
	}
}
