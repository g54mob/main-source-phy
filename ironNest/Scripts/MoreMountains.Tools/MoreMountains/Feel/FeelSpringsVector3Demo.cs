using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelSpringsVector3Demo : MonoBehaviour
	{
		[Header("Spring")]
		public MMSpringFloat SpringX;

		public MMSpringFloat SpringY;

		public MMSpringFloat SpringZ;

		[Header("Bindings")]
		public FeelSpringsDemoSlider DampingXSlider;

		public FeelSpringsDemoSlider FrequencyXSlider;

		public FeelSpringsDemoSlider DampingYSlider;

		public FeelSpringsDemoSlider FrequencyYSlider;

		public FeelSpringsDemoSlider DampingZSlider;

		public FeelSpringsDemoSlider FrequencyZSlider;

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
