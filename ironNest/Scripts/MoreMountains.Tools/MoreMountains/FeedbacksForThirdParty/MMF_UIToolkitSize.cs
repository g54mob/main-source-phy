using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the size an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Size")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitSize : MMF_UIToolkitVector2Base
	{
		protected override void SetValue(Vector2 newValue)
		{
		}

		protected override Vector2 GetInitialValue()
		{
			return default(Vector2);
		}
	}
}
