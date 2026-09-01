using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	public class ToggleUGUI : MonoBehaviour
	{
		public delegate void ValueChangedDelegate(bool value);

		public TextMeshProUGUI TextTf;

		public Toggle Toggle;

		[SerializeField]
		public Toggle.ToggleEvent OnValueChangedEvent;

		public ValueChangedDelegate OnValueChanged;

		public bool Value
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public string Text
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public void Start()
		{
		}

		private void onValueChanged(bool isOn)
		{
		}
	}
}
