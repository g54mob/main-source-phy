using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FriendServerEntry : MonoBehaviour
{
	public Action<ulong> JoinButtonClicked;

	public Action<string> JoinPlayfabClicked;

	[SerializeField]
	private RawImage avatarImage;

	[SerializeField]
	private Text playerNameText;

	[SerializeField]
	private Button joinButton;

	private ulong friendsLobbyID;

	private string pfNetworkId;

	private bool isPlayfabNetwork;

	private string avatarUrl;

	public void Setup(string playerName, Texture2D avatarTexture, ulong lobbyID)
	{
		playerNameText.text = FormatPlayerName(playerName);
		avatarImage.texture = avatarTexture;
		friendsLobbyID = lobbyID;
	}

	public void Setup(string playerName, Texture2D avatarTexture, string playfabNetworkId)
	{
		playerNameText.text = FormatPlayerName(playerName);
		avatarImage.texture = avatarTexture;
		isPlayfabNetwork = true;
		pfNetworkId = playfabNetworkId;
	}

	public void Setup(string playerName, Texture2D defaultAvatar, string avatarUrl, ulong lobbyID)
	{
		playerNameText.text = FormatPlayerName(playerName);
		avatarImage.texture = defaultAvatar;
		friendsLobbyID = lobbyID;
		this.avatarUrl = avatarUrl;
	}

	private void ResolveThumbnail(string filePath)
	{
		StartCoroutine(GetPreviewImage(filePath));
	}

	private IEnumerator GetPreviewImage(string previewUrl)
	{
		WWW wwwRequest = new WWW(previewUrl);
		yield return wwwRequest;
		avatarImage.texture = wwwRequest.textureNonReadable;
		wwwRequest.Dispose();
	}

	private string FormatPlayerName(string playerName)
	{
		string text = ReferenceMaster.CamelCaseToSpaces(playerName).ToUpper();
		if (text.Length > 18)
		{
			text = text.Substring(0, 15);
			text += "...";
		}
		return text;
	}

	private void Awake()
	{
		joinButton.onClick.AddListener(OnJoinButtonClicked);
	}

	private void Start()
	{
		if (!string.IsNullOrEmpty(avatarUrl))
		{
			ResolveThumbnail(avatarUrl);
		}
	}

	private void OnJoinButtonClicked()
	{
		if (isPlayfabNetwork)
		{
			if (JoinPlayfabClicked != null)
			{
				JoinPlayfabClicked(pfNetworkId);
			}
		}
		else if (JoinButtonClicked != null)
		{
			JoinButtonClicked(friendsLobbyID);
		}
	}
}
