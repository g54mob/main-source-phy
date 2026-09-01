using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class BarbarianEnemy : MonoBehaviour
	{
		public MMFeedbacks DamageFeedback;

		public float DamageCooldown;

		protected float _lastDamageTakenAt;

		public virtual void TakeDamage(int damage)
		{
		}
	}
}
