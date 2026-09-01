using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the text an element on a target UI Document")]
	[FeedbackPath("UI Toolkit/UITK Text")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.UIToolkit", null)]
	public class MMF_UIToolkitText : MMF_UIToolkit
	{
		[Header("Text")]
		[Tooltip("the new text to set on the target object(s)")]
		public string NewText;

		protected string _initialText;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void SetValue(string newValue)
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected virtual string GetInitialValue()
		{
			return null;
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
