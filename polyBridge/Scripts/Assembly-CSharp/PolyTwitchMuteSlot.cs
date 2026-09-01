using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PolyTwitchMuteSlot : MonoBehaviour
{
	public TextMeshProUGUI m_UserName;

	public Button m_RemoveButton;

	[NonSerialized]
	public PolyTwitchBan m_Ban;

	public void Init(PolyTwitchBan ban)
	{
		GameUI.SetAndEnableText(m_UserName, ban.m_Username);
		m_Ban = ban;
	}

	public void Start()
	{
		m_RemoveButton.onClick.AddListener(OnRemoveButton);
	}

	private void OnRemoveButton()
	{
		PolyTwitchBans.UnBanPlayer(m_Ban.m_OwnerId);
	}
}
