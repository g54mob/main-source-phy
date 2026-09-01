using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class BounceFeedbacks : MonoBehaviour
	{
		public MMFeedbacks ChargeFeedbacks;

		public MMFeedbacks JumpFeedbacks;

		public MMFeedbacks LandingFeedbacks;

		public virtual void PlayCharge()
		{
		}

		public virtual void PlayJump()
		{
		}

		public virtual void PlayLanding()
		{
		}
	}
}
