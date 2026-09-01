using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelSpringsFloatDemo : MonoBehaviour
	{
		[Header("Spring")]
		public MMSpringFloat FloatSpring;

		[Header("Bindings")]
		public FeelSpringsDemoSlider DampingSlider;

		public FeelSpringsDemoSlider FrequencySlider;

		public FeelSpringsDemoSlider BumpAmountSlider;

		public Transform MovingObject;

		protected Vector3 _newPosition;

		protected float _range;

		protected virtual void OnEnable()
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
