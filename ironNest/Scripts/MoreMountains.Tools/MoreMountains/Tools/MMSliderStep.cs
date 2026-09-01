using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/GUI/MMSliderStep")]
	[RequireComponent(typeof(Slider))]
	public class MMSliderStep : MonoBehaviour
	{
		[Header("Slider Step")]
		public float StepThreshold;

		public UnityEvent OnStep;

		protected Slider _slider;

		protected float _lastStep;

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		public virtual void ValueChangeCheck(float value)
		{
		}
	}
}
