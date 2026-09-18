using UnityEngine;
using UnityEngine.UI;

public class ControlCancelUIImage : MonoBehaviour
{
	[SerializeField]
	private Image cancelImage;

	private void OnEnable()
	{
		EventManager.OnControlsChange += SetCancelImage;
		SetCancelImage();
	}

	private void OnDisable()
	{
		EventManager.OnControlsChange -= SetCancelImage;
	}

	public void SetCancelImage()
	{
		string playerInputDeviceInputBinding = Singleton<InputDataStore>.Instance.GetDeviceCancelKeys()[0];
		cancelImage.sprite = Singleton<InputDataStore>.Instance.GetDeviceBindingIcon(playerInputDeviceInputBinding);
	}
}
