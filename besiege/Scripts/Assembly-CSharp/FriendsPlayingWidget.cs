using System;
using System.Collections.Generic;
using Localisation;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class FriendsPlayingWidget : MonoBehaviour
{
	[SerializeField]
	private Text friendsPlayingText;

	[SerializeField]
	private GameObject friendServerEntryTemplate;

	[SerializeField]
	private Transform contentTransform;

	[SerializeField]
	private Texture2D defaultAvatarTexture;

	[SerializeField]
	private Button closeButton;

	[SerializeField]
	private Button expandButton;

	[SerializeField]
	private GameObject scrollViewGameObject;

	[SerializeField]
	private Button refreshButton;

	[SerializeField]
	private GameObject steamIcon;

	[SerializeField]
	private GameObject wegameIcon;

	[SerializeField]
	private GameObject xboxGameIcon;

	private AppId_t besiegeAppID;

	private List<FriendServerEntry> friendServerEntries = new List<FriendServerEntry>();

	private void Awake()
	{
		if (!ReferenceMaster.IsPlatformReady())
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		besiegeAppID = SteamUtils.GetAppID();
		friendServerEntryTemplate.SetActive(false);
		closeButton.onClick.AddListener(OnCloseButtonClicked);
		expandButton.onClick.AddListener(OnExpandButtonClicked);
		refreshButton.onClick.AddListener(OnRefreshButtonClicked);
		ExpandWidget(false);
		TogglePlatformIcon();
	}

	private void TogglePlatformIcon()
	{
		steamIcon.SetActive(false);
		wegameIcon.SetActive(false);
		xboxGameIcon.SetActive(false);
		if (ReferenceMaster.IsPlatformReady())
		{
			steamIcon.SetActive(true);
		}
	}

	private void OnRefreshButtonClicked()
	{
		Rebuild();
	}

	private void OnExpandButtonClicked()
	{
		ExpandWidget(true);
	}

	private void OnCloseButtonClicked()
	{
		ExpandWidget(false);
	}

	private void ExpandWidget(bool expand)
	{
		closeButton.gameObject.SetActive(expand);
		expandButton.gameObject.SetActive(!expand);
		scrollViewGameObject.SetActive(expand);
	}

	private void Start()
	{
		Rebuild();
	}

	private void GetFriendServers()
	{
		int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagAll);
		for (int i = 0; i < friendCount; i++)
		{
			CSteamID friendByIndex = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagAll);
			FriendGameInfo_t pFriendGameInfo;
			if (SteamFriends.GetFriendGamePlayed(friendByIndex, out pFriendGameInfo) && !(pFriendGameInfo.m_gameID.AppID() != besiegeAppID))
			{
				string friendPersonaName = SteamFriends.GetFriendPersonaName(friendByIndex);
				Texture2D friendAvatar = GetFriendAvatar(friendByIndex);
				string pfNetworkId;
				if (pFriendGameInfo.m_steamIDLobby.IsValid())
				{
					FriendServerEntry friendServerEntry = AddFriendServerEntry();
					friendServerEntry.Setup(friendPersonaName, friendAvatar, pFriendGameInfo.m_steamIDLobby.m_SteamID);
				}
				else if (SteamFriends.GetFriendRichPresence(friendByIndex, "connect").Contains("pf_join") && SingleInstance<WorkshopManager>.hasInstance() && (SingleInstance<WorkshopManager>.Instance as SteamWorkshopManager).GetPlayfabNetworkId(friendByIndex, out pfNetworkId))
				{
					FriendServerEntry friendServerEntry2 = AddFriendServerEntry();
					friendServerEntry2.Setup(friendPersonaName, friendAvatar, pfNetworkId);
				}
			}
		}
	}

	private Texture2D GetFriendAvatar(CSteamID friendID)
	{
		int mediumFriendAvatar = SteamFriends.GetMediumFriendAvatar(friendID);
		if (mediumFriendAvatar == 0)
		{
			return defaultAvatarTexture;
		}
		Texture2D steamImageAsTexture2D = SteamHelper.GetSteamImageAsTexture2D(mediumFriendAvatar);
		if (steamImageAsTexture2D == null)
		{
			return defaultAvatarTexture;
		}
		return steamImageAsTexture2D;
	}

	private FriendServerEntry AddFriendServerEntry()
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(friendServerEntryTemplate, friendServerEntryTemplate.transform.parent);
		FriendServerEntry component = gameObject.GetComponent<FriendServerEntry>();
		component.JoinButtonClicked = (Action<ulong>)Delegate.Combine(component.JoinButtonClicked, new Action<ulong>(OnJoinButtonClicked));
		component.JoinPlayfabClicked = (Action<string>)Delegate.Combine(component.JoinPlayfabClicked, new Action<string>(OnPlayfabJoinButtonClicked));
		gameObject.gameObject.SetActive(true);
		friendServerEntries.Add(component);
		return component;
	}

	private void OnPlayfabJoinButtonClicked(string pfNetworkId)
	{
		BesiegeEntryPointHelper.JoinPlayfabNetwork(pfNetworkId);
	}

	private void OnJoinButtonClicked(ulong friendLobbyId)
	{
		OnSteamJoinButtonClicked((CSteamID)friendLobbyId);
	}

	private void OnSteamJoinButtonClicked(CSteamID lobbyID)
	{
		BesiegeEntryPointHelper.JoinGameLobby(lobbyID.m_SteamID);
	}

	private void Rebuild()
	{
		ClearEntries();
		GetFriendServers();
		SetFriendsPlayingText();
		if (friendServerEntries.Count != 0)
		{
			ExpandWidget(true);
		}
	}

	private void ClearEntries()
	{
		foreach (FriendServerEntry friendServerEntry in friendServerEntries)
		{
			UnityEngine.Object.Destroy(friendServerEntry.gameObject);
		}
		friendServerEntries.Clear();
	}

	private void SetFriendsPlayingText()
	{
		string translation = LocalisationManager.GetTranslation(3388);
		friendsPlayingText.text = string.Format(translation, friendServerEntries.Count);
	}
}
