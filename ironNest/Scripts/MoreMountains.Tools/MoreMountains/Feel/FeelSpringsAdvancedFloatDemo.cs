using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelSpringsAdvancedFloatDemo : MonoBehaviour
	{
		[Header("Bindings")]
		public MMSpringPosition PositionSpring;

		public MMSpringRotation RotationSpring;

		public MMSpringScale ScaleSpring;

		public FeelSpringsDemoSlider PositionDampingSlider;

		public FeelSpringsDemoSlider PositionFrequencySlider;

		public FeelSpringsDemoSlider RotationDampingSlider;

		public FeelSpringsDemoSlider RotationFrequencySlider;

		public FeelSpringsDemoSlider ScaleDampingSlider;

		public FeelSpringsDemoSlider ScaleFrequencySlider;

		public FeelSpringsDemoSlider BumpAmountSlider;

		public Transform MovingObject;

		protected Vector3 _newPosition;

		protected Vector3 _newBump;

		protected float _range;

		protected virtual void Awake()
		{
		}

		public virtual void RandomMove()
		{
		}

		public virtual void RandomBump()
		{
		}

		protected virtual void Update()
		{
		}
	}
}
