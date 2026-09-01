using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you translate an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Translate")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitTranslate : MMF_UIToolkitVector2Base
	{
		[Header("Units")]
		[Tooltip("how to interpret the x value")]
		public LengthUnit LengthUnitX;

		[Tooltip("how to interpret the y value")]
		public LengthUnit LengthUnitY;

		protected override void SetValue(Vector2 newValue)
		{
		}

		protected override Vector2 GetInitialValue()
		{
			return default(Vector2);
		}
	}
}
