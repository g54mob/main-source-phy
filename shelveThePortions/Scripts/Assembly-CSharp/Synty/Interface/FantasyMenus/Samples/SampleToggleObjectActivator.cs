using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleToggleObjectActivator : MonoBehaviour
	{
		[Header("References")]
		public Toggle toggle;

		public GameObject isOnObject;

		public GameObject isOffObject;

		private void Awake()
		{
			if (toggle != null)
			{
				toggle.onValueChanged.AddListener(OnValueChanged);
				OnValueChanged(toggle.isOn);
			}
		}

		private void OnDestroy()
		{
			if (toggle != null)
			{
				toggle.onValueChanged.RemoveListener(OnValueChanged);
			}
		}

		private void OnValueChanged(bool value)
		{
			if (isOnObject != null)
			{
				isOnObject.SetActive(value);
			}
			if (isOffObject != null)
			{
				isOffObject.SetActive(!value);
			}
		}
	}
}
