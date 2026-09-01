using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelBrass : MonoBehaviour
	{
		[Header("Bindings")]
		public MMAudioAnalyzer TargetAnalyzer;

		public Light TargetLight;

		[Header("Cooldown")]
		[Tooltip("a duration, in seconds, between two special dance moves, during which moves are prevented")]
		public float CooldownDuration;

		[Header("Feedbacks")]
		[Tooltip("a feedback to play when doing a special dance move")]
		public MMFeedbacks SpecialDanceMoveFeedbacks;

		protected float _lastMoveStartedAt;

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void ControlLightIntensity()
		{
		}

		protected virtual void SpecialDanceMove()
		{
		}
	}
}
