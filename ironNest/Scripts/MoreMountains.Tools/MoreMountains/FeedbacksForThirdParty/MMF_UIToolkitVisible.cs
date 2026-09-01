using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you set the visibility of an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Visible")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitVisible : MMF_UIToolkitBoolBase
	{
		public enum Modes
		{
			Set = 0,
			Toggle = 1
		}

		[Header("Visible")]
		[Tooltip("the selected mode (set : sets the object visible or not, toggle : toggles the object's visibility)")]
		public Modes Mode;

		[Tooltip("whether to set the object visible (true) or not")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public bool Visible;

		protected override void SetValue()
		{
		}

		protected override void SetValue(bool newValue)
		{
		}

		protected override bool GetInitialValue()
		{
			return false;
		}
	}
}
