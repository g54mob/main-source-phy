using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the transform origin an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Transform Origin")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitTransformOrigin : MMF_UIToolkitVector2Base
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
