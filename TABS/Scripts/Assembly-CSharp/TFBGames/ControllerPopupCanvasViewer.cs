using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class ControllerPopupCanvasViewer : MonoBehaviour
	{
		[SerializeField]
		private Image xboxController;

		[SerializeField]
		private Image ps4Controller;

		[SerializeField]
		private Image ps5Controller;

		[SerializeField]
		private Image switchController;

		private InputService inputService;

		private void Awake()
		{
			Object.DontDestroyOnLoad(base.gameObject);
			inputService = ServiceLocator.GetService<InputService>();
			OnInputStyleChanged(GetPlatformDeviceStyle());
		}

		private void OnEnable()
		{
			if (inputService != null)
			{
				inputService.InputDeviceStyleChanged += OnInputStyleChanged;
			}
		}

		private void OnDisable()
		{
			if (inputService != null)
			{
				inputService.InputDeviceStyleChanged -= OnInputStyleChanged;
			}
		}

		private InputDeviceStyle GetPlatformDeviceStyle()
		{
			return PlayerActions.Instance.ActiveDevice.DeviceStyle;
		}

		private void OnInputStyleChanged(InputDeviceStyle deviceStyle)
		{
			xboxController.gameObject.SetActive(deviceStyle == InputDeviceStyle.XboxOne || deviceStyle == InputDeviceStyle.Unknown);
			ps4Controller.gameObject.SetActive(deviceStyle == InputDeviceStyle.PlayStation4);
			ps5Controller.gameObject.SetActive(deviceStyle == InputDeviceStyle.PlayStation5);
			switchController.gameObject.SetActive(deviceStyle == InputDeviceStyle.NintendoSwitch);
		}
	}
}
