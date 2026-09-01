using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelSpringsComparisonDemo : MonoBehaviour
	{
		[Header("Spring")]
		public List<MMSpringFloat> Springs;

		public List<Transform> MovingObjects;

		public FeelSpringsDemoSlider BumpAmountSlider;

		protected Vector3 _newPosition;

		protected float _range;

		protected virtual void OnEnable()
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
