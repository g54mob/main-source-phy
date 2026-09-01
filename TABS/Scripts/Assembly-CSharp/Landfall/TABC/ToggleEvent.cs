using UnityEngine;
using UnityEngine.Events;

namespace Landfall.TABC
{
	public class ToggleEvent : MonoBehaviour
	{
		public bool isOn;

		public UnityEvent turnOnEvent;

		public UnityEvent turnOffEvent;

		public void Toggle()
		{
			isOn = !isOn;
			TriggerEvents();
		}

		public void SetState(bool isOn)
		{
			this.isOn = isOn;
			TriggerEvents();
		}

		private void TriggerEvents()
		{
			if (isOn)
			{
				turnOnEvent.Invoke();
			}
			else
			{
				turnOffEvent.Invoke();
			}
		}
	}
}
