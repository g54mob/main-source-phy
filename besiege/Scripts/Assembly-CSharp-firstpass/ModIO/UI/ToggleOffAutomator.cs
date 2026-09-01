using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Toggle))]
	public class ToggleOffAutomator : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IDeselectHandler
	{
		public bool offWhenDisabled = true;

		public bool offWhenDeselected = true;

		public bool offWhenMouseExits = true;

		private void OnDisable()
		{
			if (offWhenDisabled)
			{
				GetComponent<Toggle>().isOn = false;
			}
		}

		public void OnDeselect(BaseEventData eventData)
		{
			if (offWhenDeselected)
			{
				GetComponent<Toggle>().isOn = false;
			}
		}

		public void OnPointerExit(PointerEventData pointerEventData)
		{
			if (offWhenMouseExits)
			{
				GetComponent<Toggle>().isOn = false;
			}
		}
	}
}
