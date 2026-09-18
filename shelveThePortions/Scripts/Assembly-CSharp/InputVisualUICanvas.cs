using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputVisualUICanvas : MonoBehaviour
{
	[SerializeField]
	private GameObject displayPanel;

	[SerializeField]
	private Image displayPanelImage;

	[SerializeField]
	private TextMeshProUGUI displayPanelDeviceText;

	[SerializeField]
	private TextMeshProUGUI displayPanelDeviceStatueText;

	private void Start()
	{
		InputDataStore instance = Singleton<InputDataStore>.Instance;
		instance.OnDeviceRegainedAction = (Action<DeviceDisplaySettings>)Delegate.Combine(instance.OnDeviceRegainedAction, new Action<DeviceDisplaySettings>(OnDeviceChanged));
		InputDataStore instance2 = Singleton<InputDataStore>.Instance;
		instance2.OnDeviceLostAction = (Action<DeviceDisplaySettings>)Delegate.Combine(instance2.OnDeviceLostAction, new Action<DeviceDisplaySettings>(OnDeviceLost));
	}

	private void OnDisable()
	{
		InputDataStore instance = Singleton<InputDataStore>.Instance;
		instance.OnDeviceRegainedAction = (Action<DeviceDisplaySettings>)Delegate.Remove(instance.OnDeviceRegainedAction, new Action<DeviceDisplaySettings>(OnDeviceChanged));
		InputDataStore instance2 = Singleton<InputDataStore>.Instance;
		instance2.OnDeviceLostAction = (Action<DeviceDisplaySettings>)Delegate.Remove(instance2.OnDeviceLostAction, new Action<DeviceDisplaySettings>(OnDeviceLost));
	}

	public void OnDeviceChanged(DeviceDisplaySettings device)
	{
		PlayDevice(device, isActive: true);
	}

	public void OnDeviceLost(DeviceDisplaySettings device)
	{
		PlayDevice(device, isActive: false);
	}

	private void PlayDevice(DeviceDisplaySettings currentDevice, bool isActive)
	{
		SetDeviceUI(currentDevice);
		if (isActive)
		{
			displayPanelDeviceStatueText.text = Singleton<LocalizationSystem>.Instance.GetIDString("Controls_Connected");
		}
		else
		{
			displayPanelDeviceStatueText.text = Singleton<LocalizationSystem>.Instance.GetIDString("Controls_Disconnected");
		}
		displayPanel.SetActive(value: false);
		displayPanel.SetActive(value: true);
	}

	private void SetDeviceUI(DeviceDisplaySettings device)
	{
		if (device != null)
		{
			displayPanelImage.sprite = device.deviceDisplayIcon;
			displayPanelDeviceText.text = device.deviceDisplayName;
		}
		else
		{
			displayPanelImage.sprite = null;
			displayPanelDeviceText.text = Singleton<LocalizationSystem>.Instance.GetIDString("Controls_DeviceUnknown");
		}
	}
}
