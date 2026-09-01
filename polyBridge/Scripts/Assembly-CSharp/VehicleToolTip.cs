using TMPro;
using UnityEngine;

public class VehicleToolTip : MonoBehaviour
{
	public RectTransform m_RectTransform;

	public LabelValueResizer m_LabelValueResizer;

	public TextMeshProUGUI m_VehicleName;

	public TextMeshProUGUI m_Mass;

	public TextMeshProUGUI m_Speed;

	public TextMeshProUGUI m_Acceleration;

	public TextMeshProUGUI m_HorsePower;

	public void Enable(Vehicle vehicle)
	{
		base.gameObject.SetActive(value: true);
		Vector2 screenPos = Cameras.MainCamera().WorldToScreenPoint(vehicle.m_SandboxItem.m_Label.transform.position + new Vector3(0f, 0.5f, 0f));
		GameUI.SetScreenPosClamped(base.gameObject, screenPos, 0f, 0f);
		m_VehicleName.text = vehicle.GetDisplayName();
		m_Mass.text = vehicle.GetVehicleInfoMass();
		if (vehicle.GetDisplayName() == "UFO")
		{
			string text = "<sprite name=Tooltip_Infinite>";
			m_Speed.text = text;
			m_Acceleration.text = text;
			m_HorsePower.text = text;
		}
		else
		{
			m_Speed.text = vehicle.GetVehicleInfoSpeed();
			m_Acceleration.text = vehicle.GetVehicleInfoAcceleration();
			m_HorsePower.text = vehicle.GetVehicleInfoHorsePower();
		}
		m_RectTransform.localScale = (Game.IsRunningOnSteamDeck() ? new Vector3(1.3f, 1.3f, 1f) : new Vector3(1f, 1f, 1f));
		m_LabelValueResizer.ForceUpdate();
	}

	public void Disable()
	{
		base.gameObject.SetActive(value: false);
	}
}
