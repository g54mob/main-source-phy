using System;
using Localisation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScoreboardItem : MonoBehaviour, IEventSystemHandler, IPointerClickHandler
{
	public Action<ScoreboardItem> ItemClicked;

	[SerializeField]
	private Text playerIdText;

	[SerializeField]
	private Image playerIconImage;

	[SerializeField]
	private Text nameText;

	[SerializeField]
	private Text teamText;

	[SerializeField]
	private Text statusText;

	[SerializeField]
	private Text pingText;

	[SerializeField]
	private Sprite spectatorSprite;

	[SerializeField]
	private Sprite playerSprite;

	[SerializeField]
	private Image selectedBackgroundImage;

	private PlayerData playerData;

	public PlayerData PlayerData
	{
		get
		{
			return playerData;
		}
	}

	public void UpdateData(PlayerData player)
	{
		playerData = player;
		playerIdText.text = player.networkId.ToString();
		if (player.isSpectator)
		{
			playerIconImage.sprite = spectatorSprite;
			playerIconImage.color = Color.white;
			statusText.text = string.Empty;
		}
		else
		{
			playerIconImage.sprite = playerSprite;
			playerIconImage.color = ReferenceMaster.Instance.teamColors[(int)player.team];
			if (player.machine != null && player.machine.isSimulating)
			{
				statusText.text = LocalisationManager.GetTranslation(1938);
			}
			else if (OptionsMaster.votingEnabled && player.voteState)
			{
				statusText.text = LocalisationManager.GetTranslation(1939);
			}
			else
			{
				statusText.text = LocalisationManager.GetTranslation(1940);
			}
		}
		nameText.text = player.name;
		teamText.text = GetTeamText(player);
		pingText.text = Mathf.Min(player.ping, OptionsMaster.maxScoreboardPing).ToString();
	}

	public void Select()
	{
		selectedBackgroundImage.enabled = true;
	}

	public void Deselect()
	{
		selectedBackgroundImage.enabled = false;
	}

	private void Awake()
	{
		Deselect();
	}

	private string GetTeamText(PlayerData player)
	{
		string empty = string.Empty;
		if (player.isSpectator)
		{
			return LocalisationManager.GetTranslation(1941);
		}
		switch (player.team)
		{
		case MPTeam.Blue:
			return LocalisationManager.GetTranslation(1942);
		case MPTeam.Green:
			return LocalisationManager.GetTranslation(1943);
		case MPTeam.Orange:
			return LocalisationManager.GetTranslation(1945);
		case MPTeam.Red:
			return LocalisationManager.GetTranslation(1944);
		default:
			return LocalisationManager.GetTranslation(1946);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (ItemClicked != null)
		{
			ItemClicked(this);
		}
	}
}
