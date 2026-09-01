using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	public class MMF_UIToolkitBoolBase : MMF_UIToolkit
	{
		protected bool _initialValue;

		public override float FeedbackDuration => 0f;

		public override bool HasCustomInspectors => false;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void SetValue()
		{
		}

		protected virtual void SetValue(bool newValue)
		{
		}

		protected virtual bool GetInitialValue()
		{
			return false;
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
