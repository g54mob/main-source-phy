using UnityEngine.UI;

namespace ModIO.UI
{
	public static class UIComponentExtensions
	{
		public static void SetIsOnWithoutNotify(this Toggle toggle, bool value)
		{
			Toggle.ToggleEvent onValueChanged = toggle.onValueChanged;
			toggle.onValueChanged = new Toggle.ToggleEvent();
			toggle.isOn = value;
			toggle.onValueChanged = onValueChanged;
		}
	}
}
