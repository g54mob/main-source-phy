using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class GettingStartedTutorialHeroReference : MonoBehaviour
	{
		[Header("Hero Settings")]
		public KeyCode ActionKey;

		public float JumpForce;

		[Header("Feedbacks")]
		public MMFeedbacks JumpFeedback;

		public MMFeedbacks LandingFeedback;

		[Header("Events")]
		public UnityEvent OnJump;

		public UnityEvent OnLand;

		private const float _lowVelocity = 0.1f;

		private Rigidbody _rigidbody;

		private float _velocityLastFrame;

		private bool _jumping;

		private void Awake()
		{
		}

		private void Update()
		{
		}

		private void Jump()
		{
		}
	}
}
