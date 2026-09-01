using System;
using UnityEngine;

public class ProfileSlot : MonoBehaviour
{
	public ProfileCard m_ActiveCard;

	public ProfileCard m_DisabledCard;

	public ProfileCard m_CreateNewCard;

	public ProfileCard m_EmptySlotCard;

	[NonSerialized]
	public string m_ProfileName;

	private Action<int> m_Callback;

	public void SetCallback(Action<int> callback)
	{
		m_Callback = callback;
	}

	public void MakeActiveCard(int slotIndex, Sprite vehicleIcon, string profileName, int numCompletedLevels)
	{
		m_ActiveCard.gameObject.SetActive(value: true);
		m_DisabledCard.gameObject.SetActive(value: false);
		m_CreateNewCard.gameObject.SetActive(value: false);
		m_EmptySlotCard.gameObject.SetActive(value: false);
		m_ProfileName = profileName;
		m_ActiveCard.Init(slotIndex, vehicleIcon, profileName, numCompletedLevels, ProfileClicked);
	}

	public void MakeDisabledCard(int slotIndex, Sprite vehicleIcon, string profileName, int numCompletedLevels)
	{
		m_ActiveCard.gameObject.SetActive(value: false);
		m_DisabledCard.gameObject.SetActive(value: true);
		m_CreateNewCard.gameObject.SetActive(value: false);
		m_EmptySlotCard.gameObject.SetActive(value: false);
		m_ProfileName = profileName;
		m_DisabledCard.Init(slotIndex, vehicleIcon, profileName, numCompletedLevels, ProfileClicked);
	}

	public void MakeCreateNewCard(int slotIndex)
	{
		m_ActiveCard.gameObject.SetActive(value: false);
		m_DisabledCard.gameObject.SetActive(value: false);
		m_CreateNewCard.gameObject.SetActive(value: true);
		m_EmptySlotCard.gameObject.SetActive(value: false);
		m_ProfileName = string.Empty;
		m_CreateNewCard.Init(slotIndex, null, null, 0, CreateNewCardClicked);
	}

	public void MakeEmptyCard(int slotIndex)
	{
		m_ActiveCard.gameObject.SetActive(value: false);
		m_DisabledCard.gameObject.SetActive(value: false);
		m_CreateNewCard.gameObject.SetActive(value: false);
		m_EmptySlotCard.gameObject.SetActive(value: true);
		m_ProfileName = string.Empty;
		m_EmptySlotCard.Init(slotIndex, null, null, 0, null);
	}

	public bool IsCreateNewCard()
	{
		return m_CreateNewCard.gameObject.activeSelf;
	}

	public bool IsEmptyCard()
	{
		return m_EmptySlotCard.gameObject.activeSelf;
	}

	public bool IsDisabledCard()
	{
		return m_DisabledCard.gameObject.activeSelf;
	}

	private void CreateNewCardClicked(int slotIndex)
	{
		m_Callback?.Invoke(slotIndex);
	}

	private void ProfileClicked(int slotIndex)
	{
		m_Callback?.Invoke(slotIndex);
	}
}
