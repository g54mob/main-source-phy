using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class Duck : MonoBehaviour
	{
		[Header("Cooldown")]
		[Tooltip("a duration, in seconds, between two jumps, during which jumps are prevented")]
		public float CooldownDuration;

		[Header("Feedbacks")]
		[Tooltip("a feedback to call when jumping")]
		public MMFeedbacks JumpFeedback;

		[Tooltip("a feedback to call when landing")]
		public MMFeedbacks LandingFeedback;

		[Tooltip("a feedback to call when trying to jump while in cooldown")]
		public MMFeedbacks DeniedFeedback;

		protected float _lastJumpStartedAt;

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void Jump()
		{
		}

		public virtual void Land()
		{
		}
	}
}
