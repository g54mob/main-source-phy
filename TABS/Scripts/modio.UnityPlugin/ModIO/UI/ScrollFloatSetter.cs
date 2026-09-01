using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ScrollFloatSetter : MonoBehaviour
	{
		public float inputMultiplier = 500f;

		[HideInInspector]
		public ScrollRect scrollRect;

		private void Awake()
		{
			scrollRect = GetComponent<ScrollRect>();
		}

		public void SetHorizontalVelocity(float velocity)
		{
			Vector2 velocity2 = scrollRect.velocity;
			velocity2.x = velocity * inputMultiplier;
			scrollRect.velocity = velocity2;
		}

		public void SetVerticalVelocity(float velocity)
		{
			Vector2 velocity2 = scrollRect.velocity;
			velocity2.y = velocity * inputMultiplier;
			scrollRect.velocity = velocity2;
		}
	}
}
