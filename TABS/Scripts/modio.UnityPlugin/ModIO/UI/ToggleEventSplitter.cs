using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Toggle))]
	public class ToggleEventSplitter : MonoBehaviour
	{
		public Toggle.ToggleEvent toggledOn = new Toggle.ToggleEvent();

		public Toggle.ToggleEvent toggledOff = new Toggle.ToggleEvent();

		private void Start()
		{
			GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged);
		}

		private void OnValueChanged(bool isOn)
		{
			if (isOn)
			{
				toggledOn.Invoke(arg0: true);
			}
			else
			{
				toggledOff.Invoke(arg0: false);
			}
		}
	}
}
