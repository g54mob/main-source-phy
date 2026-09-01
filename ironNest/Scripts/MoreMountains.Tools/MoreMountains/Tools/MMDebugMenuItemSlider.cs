using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMDebugMenuItemSlider : MonoBehaviour
	{
		public enum Modes
		{
			Float = 0,
			Int = 1
		}

		[Header("Bindings")]
		public Modes Mode;

		public Slider TargetSlider;

		public Text SliderText;

		public Text SliderValueText;

		public Image SliderKnob;

		public Image SliderLine;

		public float RemapZero;

		public float RemapOne;

		public string SliderEventName;

		[MMReadOnly]
		public float SliderValue;

		[MMReadOnly]
		public int SliderValueInt;

		protected bool _valueSetThisFrame;

		protected bool _listening;

		protected virtual void Awake()
		{
		}

		public void ValueChangeCheck()
		{
		}

		protected virtual void UpdateValue(float newValue)
		{
		}

		protected virtual void TriggerSliderEvent(float value)
		{
		}

		protected virtual void OnMMDebugMenuSliderEvent(string sliderEventName, float value, MMDebugMenuSliderEvent.EventModes eventMode)
		{
		}

		public virtual void OnEnable()
		{
		}

		public virtual void OnDestroy()
		{
		}
	}
}
