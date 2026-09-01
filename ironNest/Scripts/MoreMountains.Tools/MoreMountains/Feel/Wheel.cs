using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class Wheel : MonoBehaviour
	{
		[Header("Binding")]
		[Tooltip("the part of the wheel that rotates")]
		public Transform RotatingPart;

		[Header("Settings")]
		[Tooltip("the speed at which the wheel should rotate")]
		public float RotationSpeed;

		[Header("Feedbacks")]
		[Tooltip("a feedback to call when the wheel starts turning")]
		public MMFeedbacks TurnFeedback;

		[Tooltip("a feedback to call when the wheel stops turning")]
		public MMFeedbacks TurnStopFeedback;

		protected bool _turning;

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void HandleWheel()
		{
		}

		protected virtual void Turn()
		{
		}

		protected virtual void TurnStop()
		{
		}
	}
}
