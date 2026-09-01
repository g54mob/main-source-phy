using System.Collections.Generic;
using System.Linq;
using Localisation;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
	[SerializeField]
	private ScoreboardItem templateItem;

	[SerializeField]
	private Transform contentTransform;

	[SerializeField]
	private Text serverBlocksText;

	[SerializeField]
	private Text levelBlocksText;

	[SerializeField]
	private Text serverHealthText;

	[SerializeField]
	private Text playerMachineBlocksText;

	[SerializeField]
	private Text playerClusterCountText;

	[SerializeField]
	private Text playerNameText;

	[SerializeField]
	private LayoutElement viewportElement;

	[SerializeField]
	private Text cpuLoadText;

	private Dictionary<int, ScoreboardItem> items = new Dictionary<int, ScoreboardItem>();

	private bool isShown;

	private ScoreboardItem selectedItem;

	private float lastScoreboardUpdate;

	private void Start()
	{
		UpdateServerInfo();
		Hide();
	}

	private Dictionary<BlockType, int> GetBlockTypeCount(PlayerData playerData)
	{
		Dictionary<BlockType, int> dictionary = new Dictionary<BlockType, int>();
		List<BlockBehaviour> buildingBlocks = ReferenceMaster.GetBuildingBlocks(playerData.networkId);
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockType type = buildingBlocks[i].Prefab.Type;
			if (dictionary.ContainsKey(type))
			{
				Dictionary<BlockType, int> dictionary3;
				Dictionary<BlockType, int> dictionary2 = (dictionary3 = dictionary);
				BlockType key2;
				BlockType key = (key2 = type);
				int num = dictionary3[key2];
				dictionary2[key] = num + 1;
			}
			else
			{
				dictionary.Add(type, 1);
			}
		}
		return dictionary;
	}

	private void UpdateServerInfo()
	{
		if (CustomLevel.Instance == null)
		{
			serverBlocksText.text = "0";
			levelBlocksText.text = "0";
			serverHealthText.text = LocalisationManager.GetTranslation(1934);
			return;
		}
		levelBlocksText.text = CustomLevel.Instance.TotalEntityCount.ToString();
		ServerHealth instance = ServerHealth.Instance;
		if (instance == null)
		{
			serverHealthText.text = LocalisationManager.GetTranslation(1934);
			serverBlocksText.text = "0";
			cpuLoadText.text = "-";
			return;
		}
		if (StatMaster.levelSimulating)
		{
			cpuLoadText.text = instance.CPULoad.ToString();
		}
		else
		{
			cpuLoadText.text = "-";
		}
		serverBlocksText.text = instance.ActiveBlockCount.ToString();
		if (instance.Health >= instance.okayThreshold)
		{
			serverHealthText.text = LocalisationManager.GetTranslation(1935);
		}
		else if (instance.Health >= instance.badThreshold)
		{
			serverHealthText.text = LocalisationManager.GetTranslation(1936);
		}
		else
		{
			serverHealthText.text = LocalisationManager.GetTranslation(1937);
		}
	}

	private void UpdatePlayerPings()
	{
		if (isShown)
		{
			UpdateScoreboard(Playerlist.Players);
		}
	}

	private void Update()
	{
		if (StatMaster.isMP && !StatMaster.IsLevelEditorOnly && InputManager.TogglePlayerList())
		{
			if (!isShown)
			{
				if (!StatMaster.inMenu)
				{
					Show();
				}
			}
			else
			{
				Hide();
			}
		}
		if (lastScoreboardUpdate + 0.2f < Time.time)
		{
			UpdatePlayerPings();
			lastScoreboardUpdate = Time.time;
		}
	}

	public void Hide()
	{
		if (isShown)
		{
			StatMaster.SetInMenu(false);
			CancelInvoke("PollScoreboardData");
			viewportElement.enabled = false;
			base.transform.GetChild(0).gameObject.SetActive(false);
			isShown = false;
		}
	}

	private void Show()
	{
		if (!isShown)
		{
			StatMaster.SetInMenu(true);
		}
		if (BesiegeNetworkManager.Instance != null)
		{
			UpdateScoreboard(Playerlist.Players);
			InvokeRepeating("PollScoreboardData", 0f, 1f);
		}
		UpdateServerInfo();
		SelectDefaultItem();
		base.transform.GetChild(0).gameObject.SetActive(true);
		viewportElement.enabled = true;
		isShown = true;
		if (selectedItem != null)
		{
			selectedItem.Select();
		}
	}

	private void SelectDefaultItem()
	{
		if (!(selectedItem != null))
		{
			if (items.Count > 0)
			{
				int current = items.Keys.GetEnumerator().Current;
				SelectScoreboardItem(items[current]);
			}
			else
			{
				SelectScoreboardItem(null);
			}
		}
	}

	private void PollScoreboardData()
	{
		UpdateServerInfo();
		NetworkAuxAddPiece.Instance.SendServerMessage(RPCMessageType.RequestPlayerPings);
	}

	public void UpdateScoreboard(List<PlayerData> players)
	{
		AddOrUpdatePlayers(players);
		RemoveOldPlayers(players);
		SelectDefaultItem();
	}

	private void OnScoreboardItemClicked(ScoreboardItem item)
	{
		SelectScoreboardItem(item);
	}

	private void SelectScoreboardItem(ScoreboardItem item)
	{
		if (selectedItem != null)
		{
			selectedItem.Deselect();
		}
		if (item != null)
		{
			item.Select();
		}
		selectedItem = item;
		UpdatePlayerInfo();
	}

	private void UpdatePlayerInfo()
	{
		if (selectedItem == null)
		{
			playerMachineBlocksText.text = string.Empty;
			playerClusterCountText.text = string.Empty;
			playerNameText.text = "( )";
			return;
		}
		PlayerData playerData = selectedItem.PlayerData;
		playerNameText.text = "( " + playerData.name + " )";
		if (playerData.isSpectator || playerData.machine == null)
		{
			playerMachineBlocksText.text = string.Empty;
			playerClusterCountText.text = string.Empty;
		}
		else
		{
			playerMachineBlocksText.text = playerData.machine.DisplayBlockCount.ToString();
			playerClusterCountText.text = playerData.machine.ClusterCount.ToString();
		}
	}

	private void RemoveOldPlayers(List<PlayerData> players)
	{
		List<int> list = new List<int>();
		int playerId;
		foreach (int key in items.Keys)
		{
			playerId = key;
			PlayerData playerData = players.FirstOrDefault((PlayerData item) => item.networkId == playerId);
			if (playerData == null)
			{
				list.Add(playerId);
			}
		}
		foreach (int item in list)
		{
			RemoveItem(item);
		}
	}

	private void AddOrUpdatePlayers(List<PlayerData> players)
	{
		foreach (PlayerData player in players)
		{
			if (!player.initReady)
			{
				continue;
			}
			if (items.ContainsKey(player.networkId))
			{
				if (player.isLocalPlayer)
				{
					player.ping = BesiegeNetworkManager.Instance.Ping;
				}
				items[player.networkId].UpdateData(player);
				if (items[player.networkId] == selectedItem)
				{
					UpdatePlayerInfo();
				}
			}
			else
			{
				AddItem(player);
			}
		}
	}

	private void AddItem(PlayerData player)
	{
		ScoreboardItem scoreboardItem = (ScoreboardItem)Object.Instantiate(templateItem, contentTransform);
		scoreboardItem.UpdateData(player);
		scoreboardItem.GetComponent<RectTransform>().SetAsLastSibling();
		scoreboardItem.gameObject.SetActive(true);
		scoreboardItem.ItemClicked = OnScoreboardItemClicked;
		items.Add(player.networkId, scoreboardItem);
	}

	private void RemoveItem(int playerId)
	{
		ScoreboardItem scoreboardItem = items[playerId];
		items.Remove(playerId);
		scoreboardItem.ItemClicked = null;
		if (scoreboardItem == selectedItem)
		{
			selectedItem = null;
			SelectDefaultItem();
		}
		Object.Destroy(scoreboardItem.gameObject);
	}
}
