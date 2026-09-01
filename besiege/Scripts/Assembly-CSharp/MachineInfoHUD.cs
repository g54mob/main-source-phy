using System.Collections.Generic;
using System.Linq;
using System.Text;
using InternalModding.Blocks;
using InternalModding.Loading;
using Localisation;
using UnityEngine;
using UnityEngine.UI;

public class MachineInfoHUD : MonoBehaviour, ILocalisationAware
{
	public class MachineInfoHudItemDataComparer : IComparer<MachineInfoHudItemData>
	{
		public int Compare(MachineInfoHudItemData x, MachineInfoHudItemData y)
		{
			bool flag = x.Limit == -1;
			bool flag2 = y.Limit == -1;
			bool flag3 = x.Count > x.Limit;
			bool flag4 = y.Count > y.Limit;
			int num = x.Limit - x.Count;
			int num2 = y.Limit - y.Count;
			if (flag && !flag2)
			{
				return -1;
			}
			if (!flag && flag2)
			{
				return 1;
			}
			if (flag && flag2)
			{
				return (x.Count != y.Count) ? ((x.Count > y.Count) ? 1 : (-1)) : 0;
			}
			if (flag3 && !flag4)
			{
				return 1;
			}
			if (!flag3 && flag4)
			{
				return -1;
			}
			return (num != num2) ? ((num <= num2) ? 1 : (-1)) : 0;
		}
	}

	public bool KeepOldItems;

	[SerializeField]
	private MachineInfoHUDItem templateItem;

	[SerializeField]
	private Transform contentTransform;

	[SerializeField]
	private Text machineBlocksCountText;

	[SerializeField]
	private Text machineClustersText;

	[SerializeField]
	private Text machineSizeText;

	[SerializeField]
	private Text keysMappedText;

	[SerializeField]
	private Text machineTitleText;

	[SerializeField]
	private LayoutElement viewportElement;

	[SerializeField]
	private GameObject noBlocksMessageGameObject;

	[SerializeField]
	private GameObject scrollViewGameObject;

	[SerializeField]
	private Color limitExceededColor = Color.red;

	private Dictionary<int, MachineInfoHUDItem> items = new Dictionary<int, MachineInfoHUDItem>();

	private bool isShown;

	public MachineInfoHudItemData GetDataFromPrefab(ServerMachine serverMachine, BlockPrefab prefab)
	{
		MachineInfoHudItemData machineInfoHudItemData = new MachineInfoHudItemData();
		machineInfoHudItemData.BlockID = prefab.ID;
		BlockType iD = (BlockType)prefab.ID;
		machineInfoHudItemData.Limit = LevelEditor.Instance.Settings.GetBlockLimit(iD);
		machineInfoHudItemData.Name = ReferenceMaster.TranslateBlockName(iD);
		machineInfoHudItemData.IconSprite = GetBlockIcon(prefab);
		if (serverMachine.BlockTypeCount.ContainsKey(prefab.ID))
		{
			machineInfoHudItemData.Count = serverMachine.BlockTypeCount[prefab.ID];
		}
		else
		{
			machineInfoHudItemData.Count = Random.Range(0, 254);
		}
		return machineInfoHudItemData;
	}

	public void AddItem(MachineInfoHudItemData itemData)
	{
		MachineInfoHUDItem machineInfoHUDItem = (MachineInfoHUDItem)Object.Instantiate(templateItem, contentTransform);
		machineInfoHUDItem.gameObject.SetActive(true);
		machineInfoHUDItem.Setup(itemData);
		items.Add(itemData.BlockID, machineInfoHUDItem);
	}

	private string PrepareMachineName(string name)
	{
		string text = name;
		if (string.IsNullOrEmpty(text))
		{
			text = "Unnamed machine";
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < text.Length; i++)
		{
			stringBuilder.Append(text[i]);
			stringBuilder.Append(' ');
		}
		return stringBuilder.ToString().ToUpper();
	}

	private bool MachineContainsBlocks(ServerMachine serverMachine)
	{
		if (serverMachine.BlockTypeCount.Count == 0)
		{
			return false;
		}
		foreach (KeyValuePair<int, int> item in serverMachine.BlockTypeCount)
		{
			if (item.Key == 0 || item.Value == 0)
			{
				continue;
			}
			return true;
		}
		return false;
	}

	private List<MachineInfoHudItemData> GenerateItemDataList(ServerMachine serverMachine)
	{
		List<MachineInfoHudItemData> list = new List<MachineInfoHudItemData>();
		foreach (KeyValuePair<int, int> item in serverMachine.BlockTypeCount)
		{
			int key = item.Key;
			BlockType blockType = (BlockType)key;
			BlockType blockType2 = blockType;
			if (blockType2 != BlockType.BuildNode && blockType2 != BlockType.BuildEdge && blockType2 != BlockType.StartingBlock && blockType2 != BlockType.Unused && blockType2 != BlockType.Unused3 && blockType2 != BlockType.CameraBlock && blockType2 != BlockType.Magnet && serverMachine.BlockTypeCount[key] != 0)
			{
				MachineInfoHudItemData dataFromPrefab = GetDataFromPrefab(serverMachine, PrefabMaster.BlockPrefabs[key]);
				list.Add(dataFromPrefab);
			}
		}
		return list;
	}

	private int GetKeysMappedCount(ServerMachine serverMachine)
	{
		List<int> list = new List<int>();
		List<BlockBehaviour> buildingBlocks = ReferenceMaster.GetBuildingBlocks(serverMachine.PlayerID);
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			foreach (MKey key2 in buildingBlocks[i].Keys)
			{
				for (int j = 0; j < key2.KeysCount; j++)
				{
					int key = (int)key2.GetKey(j);
					if (!list.Contains(key))
					{
						list.Add(key);
					}
				}
			}
		}
		return list.Count;
	}

	private void SetMachineBlockColor(Color color)
	{
		Graphic[] componentsInChildren = machineBlocksCountText.transform.parent.GetComponentsInChildren<Graphic>();
		foreach (Graphic graphic in componentsInChildren)
		{
			graphic.color = color;
		}
	}

	public void Setup(ServerMachine serverMachine)
	{
		int num = Mathf.Max(serverMachine.DisplayBlockCount, 0);
		int blockCountLimiter = LevelEditor.Instance.Settings.BlockCountLimiter;
		if (LevelEditor.Instance.Settings.BlockCountLimiter == -1)
		{
			machineBlocksCountText.text = num.ToString();
		}
		else
		{
			machineBlocksCountText.text = string.Format("{0} / {1}", num - 1, blockCountLimiter);
			if (num > blockCountLimiter)
			{
				SetMachineBlockColor(limitExceededColor);
			}
			else
			{
				SetMachineBlockColor(Color.white);
			}
		}
		machineTitleText.text = PrepareMachineName(serverMachine.Name);
		machineClustersText.text = serverMachine.ClusterCount.ToString();
		machineSizeText.text = string.Format("{0:0.#}x{1:0.#}x{2:0.#}", serverMachine.Size.x, serverMachine.Size.y, serverMachine.Size.z);
		int keysMappedCount = GetKeysMappedCount(serverMachine);
		keysMappedText.text = keysMappedCount.ToString();
		if (!KeepOldItems)
		{
			ClearItems();
		}
		if (MachineContainsBlocks(serverMachine))
		{
			scrollViewGameObject.SetActive(true);
			noBlocksMessageGameObject.SetActive(false);
		}
		else
		{
			scrollViewGameObject.SetActive(false);
			noBlocksMessageGameObject.SetActive(true);
		}
		List<MachineInfoHudItemData> source = GenerateItemDataList(serverMachine);
		IEnumerable<MachineInfoHudItemData> enumerable = source.OrderByDescending((MachineInfoHudItemData item) => item, new MachineInfoHudItemDataComparer());
		foreach (MachineInfoHudItemData item in enumerable)
		{
			AddItem(item);
		}
	}

	private void Awake()
	{
		templateItem.gameObject.SetActive(false);
		Hide();
	}

	private Sprite GetBlockIcon(BlockPrefab blockPrefab)
	{
		Sprite sprite;
		if (blockPrefab.ID < ReferenceMaster.Instance.blockTypeSprites.Length)
		{
			sprite = ReferenceMaster.Instance.blockTypeSprites[blockPrefab.ID];
		}
		else
		{
			ModdedBlock blockByEffectiveId = ModIds.GetBlockByEffectiveId(blockPrefab.ID);
			sprite = ((blockByEffectiveId != null) ? blockByEffectiveId.BlockTypeSprite : null);
		}
		if (sprite == null)
		{
			Debug.LogError("Could not find ID '" + blockPrefab.ID + "' (" + blockPrefab.name + ") in block icons");
			return ReferenceMaster.Instance.blockTypeSprites[7];
		}
		return sprite;
	}

	private void Update()
	{
		if (!StatMaster.isMP || !InputManager.ToggleMachineInfoHUD())
		{
			return;
		}
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

	private void UpdateInfo()
	{
		Machine machine = Machine.Active();
		if (machine != null)
		{
			ServerMachine serverMachine = (ServerMachine)Machine.Active();
			Setup(serverMachine);
		}
	}

	private void ClearItems()
	{
		List<int> list = new List<int>();
		foreach (int key in items.Keys)
		{
			list.Add(key);
		}
		foreach (int item in list)
		{
			RemoveItem(item);
		}
		items.Clear();
	}

	private void RemoveItem(int blockID)
	{
		MachineInfoHUDItem machineInfoHUDItem = items[blockID];
		items.Remove(blockID);
		Object.Destroy(machineInfoHUDItem.gameObject);
	}

	public void Hide()
	{
		if (isShown)
		{
			StatMaster.SetInMenu(false);
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
		UpdateInfo();
		base.transform.GetChild(0).gameObject.SetActive(true);
		viewportElement.enabled = true;
		isShown = true;
	}

	public void OnLocalisationChange()
	{
		if (isShown)
		{
			Show();
		}
	}
}
