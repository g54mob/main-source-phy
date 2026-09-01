using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelSpringsCellMovementDemo : MonoBehaviour
	{
		protected enum Directions
		{
			Left = 0,
			Right = 1,
			Up = 2,
			Down = 3
		}

		[Header("Spring")]
		public MMSpringPosition MovementSpring;

		public MMSpringRotation RotationSpring;

		public MMSpringScale ScaleSpring;

		[Header("Bindings")]
		public FeelSpringsDemoSlider DampingSlider;

		public FeelSpringsDemoSlider FrequencySlider;

		public MMFeedbacks MoveFeedback;

		protected Vector3 _newPosition;

		protected float _cellWidth;

		protected Vector2 _currentPosition;

		protected Vector3 _movementPosition;

		protected virtual void Update()
		{
		}

		public virtual void MoveRandomly()
		{
		}

		protected virtual void Move(Directions direction)
		{
		}

		protected virtual void Bump(Directions direction)
		{
		}

		protected virtual void ComputeNewGridPosition(Directions direction)
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void UpdateSliderValues()
		{
		}
	}
}
