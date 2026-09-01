using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class BounceManager : MonoBehaviour
	{
		[Header("Cooldown")]
		[Tooltip("a duration, in seconds, between two jumps, during which jumps are prevented")]
		public float CooldownDuration;

		[Header("Bindings")]
		[Tooltip("the animator of the 'no feedback' version")]
		public Animator NoFeedbackAnimator;

		[Tooltip("the animator of the 'feedback' version")]
		public Animator FeedbackAnimator;

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
	}
}
