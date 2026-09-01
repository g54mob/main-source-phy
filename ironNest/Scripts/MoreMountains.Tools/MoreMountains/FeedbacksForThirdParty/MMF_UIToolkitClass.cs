using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the class of an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Class")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitClass : MMF_UIToolkit
	{
		public enum Modes
		{
			AddToClassList = 0,
			EnableInClassList = 1,
			ToggleInClassList = 2,
			RemoveFromClassList = 3,
			ClearClassList = 4
		}

		[Header("Class Manipulation")]
		[Tooltip("whether to add, enable, toggle, remove or clear the class list")]
		public Modes Mode;

		[Tooltip("the name of the class to add, enable, toggle or remove")]
		[MMFEnumCondition("Mode", new int[] { 0, 1, 2, 3 })]
		public string ClassName;

		[Tooltip("in EnableInClassList mode, whether to enable or disable the class")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public bool Enable;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
