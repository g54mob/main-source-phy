using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class Blob : MonoBehaviour
	{
		[Header("Cooldown")]
		[Tooltip("a duration, in seconds, between two moves, during which moves are prevented")]
		public float CooldownDuration;

		[Header("Feedbacks")]
		[Tooltip("a feedback to call when moving")]
		public MMFeedbacks MoveFeedback;

		[Tooltip("a feedback to call when trying to move while in cooldown")]
		public MMFeedbacks DeniedFeedback;

		protected float _lastMoveStartedAt;

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void Move()
		{
		}
	}
}
