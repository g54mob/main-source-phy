using System;
using UnityEngine;
using UnityEngine.UI;

public class ProfileAvatarChoice : MonoBehaviour
{
	public Image m_Icon;

	public Button m_Button;

	public Image m_Outline;

	[NonSerialized]
	public string m_VehicleAddressable;

	[NonSerialized]
	public string m_VehicleSkinLocID;

	private Action<int> m_Callback;

	private int m_ChoiceIndex;

	private Color m_DefaultOutlineColor;

	private void Awake()
	{
		m_Button.onClick.AddListener(OnClicked);
		m_DefaultOutlineColor = m_Outline.color;
	}

	public void Init(int choiceIndex, string addressableName, string skinLocID, Action<int> callback)
	{
		m_VehicleAddressable = addressableName;
		m_VehicleSkinLocID = skinLocID;
		m_Callback = callback;
		m_ChoiceIndex = choiceIndex;
		m_Icon.sprite = GetSprite();
	}

	public Sprite GetSprite()
	{
		return Profiles.GetSpriteForVehicle(m_VehicleAddressable, m_VehicleSkinLocID);
	}

	public void Highlight(bool on)
	{
		m_Outline.color = (on ? GameUI.m_Instance.m_GoldColor : m_DefaultOutlineColor);
	}

	private void OnClicked()
	{
		m_Callback?.Invoke(m_ChoiceIndex);
	}
}
