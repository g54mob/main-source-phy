using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	public class MMFeedbacksEnabler : MonoBehaviour
	{
		public MMFeedbacks TargetMMFeedbacks { get; set; }

		protected virtual void OnEnable()
		{
		}
	}
}
