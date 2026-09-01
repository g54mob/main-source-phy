using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileCard : MonoBehaviour
{
	public Button m_Button;

	public Image m_VehicleIcon;

	public TextMeshProUGUI m_ProfileName;

	public TextMeshProUGUI m_ProgressText;

	public TextMeshProUGUI m_SlotNumberText;

	private int m_SlotIndex;

	private Action<int> m_Callback;

	private void Awake()
	{
		m_Button.onClick.AddListener(OnClicked);
	}

	public void Init(int slotIndex, Sprite vehicleIcon, string name, int numCompletedLevels, Action<int> callback)
	{
		SetSlotNumber(slotIndex);
		SetVehicleIcon(vehicleIcon);
		SetProfileName(name);
		SetProgress(numCompletedLevels);
		m_Callback = callback;
		m_SlotIndex = slotIndex;
	}

	private void SetSlotNumber(int slotIndex)
	{
		if (m_SlotNumberText != null)
		{
			m_SlotNumberText.text = string.Format(Localize.Get("UI_SLOT_NUMBER"), slotIndex + 1);
		}
	}

	private void SetVehicleIcon(Sprite sprite)
	{
		if (m_VehicleIcon != null)
		{
			m_VehicleIcon.sprite = sprite;
			m_VehicleIcon.gameObject.SetActive(sprite != null);
		}
	}

	private void SetProfileName(string name)
	{
		if (m_ProfileName != null)
		{
			m_ProfileName.text = name;
		}
	}

	private void SetProgress(int numCompletedLevels)
	{
		if (m_ProgressText != null)
		{
			m_ProgressText.text = numCompletedLevels.ToString();
		}
	}

	private void OnClicked()
	{
		m_Callback?.Invoke(m_SlotIndex);
	}
}
