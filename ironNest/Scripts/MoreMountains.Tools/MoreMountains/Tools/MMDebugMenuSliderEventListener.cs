using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMDebugMenuSliderEventListener : MonoBehaviour
	{
		[Header("Events")]
		public string SliderEventName;

		public MMDSliderValueChangedEvent MMDValueChangedEvent;

		[Header("Test")]
		[Range(0f, 1f)]
		public float TestValue;

		[MMInspectorButton("TestSetValue")]
		public bool TestSetValueButton;

		protected virtual void TestSetValue()
		{
		}

		protected virtual void OnMMDebugMenuSliderEvent(string sliderEventName, float value, MMDebugMenuSliderEvent.EventModes eventMode)
		{
		}

		public virtual void OnEnable()
		{
		}

		public virtual void OnDisable()
		{
		}
	}
}
