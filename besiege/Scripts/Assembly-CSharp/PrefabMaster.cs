using System;
using System.Collections.Generic;
using System.Linq;
using BesiegeDlc;
using InternalModding;
using InternalModding.Blocks;
using Ordered;
using UnityEngine;

public class PrefabMaster : SingleInstance<PrefabMaster>
{
	public enum PrefabType
	{
		Normal = 0,
		Network = 1,
		Stripped = 2
	}

	[Serializable]
	public class PreloadedSkins
	{
		public string name;

		public string id;

		public List<PreloadedSkin> blockSkins = new List<PreloadedSkin>();
	}

	[Serializable]
	public class PreloadedSkin
	{
		public BlockType type;

		public Mesh mesh;

		public Texture texture;

		public Material material;

		public Mesh[] extra = new Mesh[0];
	}

	[SerializeField]
	protected Transform blockPrefabContainer;

	[SerializeField]
	protected Transform networkBlockContainer;

	[SerializeField]
	protected Transform strippedBlockPrefabContainer;

	[SerializeField]
	protected Transform levelPrefabContainer;

	[SerializeField]
	private List<PreloadedSkins> officialBlockSkins = new List<PreloadedSkins>();

	protected System.Collections.Generic.Dictionary<int, BlockPrefab> _blockPrefabs = new System.Collections.Generic.Dictionary<int, BlockPrefab>();

	protected System.Collections.Generic.Dictionary<int, BlockBehaviour> _networkBlocks = new System.Collections.Generic.Dictionary<int, BlockBehaviour>();

	protected System.Collections.Generic.Dictionary<int, BlockBehaviour> _strippedBlockPrefabs = new System.Collections.Generic.Dictionary<int, BlockBehaviour>();

	protected List<GameObject> _blockGhosts = new List<GameObject>();

	private static int enumLength;

	protected System.Collections.Generic.Dictionary<int, Ordered.Dictionary<int, LevelPrefab>> _levelPrefabs = new System.Collections.Generic.Dictionary<int, Ordered.Dictionary<int, LevelPrefab>>();

	private DlcManager dlcManager;

	private static int numberOfLevelObjects;

	public override string Name
	{
		get
		{
			return "PrefabMaster";
		}
	}

	public static System.Collections.Generic.Dictionary<int, Ordered.Dictionary<int, LevelPrefab>> LevelPrefabs
	{
		get
		{
			return SingleInstance<PrefabMaster>.Instance._levelPrefabs;
		}
	}

	public static System.Collections.Generic.Dictionary<int, BlockPrefab> BlockPrefabs
	{
		get
		{
			return SingleInstance<PrefabMaster>.Instance._blockPrefabs;
		}
	}

	public static List<GameObject> BlockGhosts
	{
		get
		{
			return SingleInstance<PrefabMaster>.Instance._blockGhosts;
		}
	}

	public static List<PreloadedSkins> OfficialBlockSkins
	{
		get
		{
			return SingleInstance<PrefabMaster>.Instance.officialBlockSkins;
		}
	}

	public void Awake()
	{
		if (!setUp)
		{
			SingleInstance<PrefabMaster>.Initialize(this);
		}
	}

	public override void SetUp()
	{
		BlockSkinLoader.CreateDefaultPack();
		dlcManager = ((dlcManager == null) ? new DlcManager() : DlcManager.Instance);
		DlcManager obj = dlcManager;
		obj.DlcManagerInitialized = (Action)Delegate.Combine(obj.DlcManagerInitialized, new Action(OnDlcManagerInitialized));
		dlcManager.Initialize();
		NotifyDlcManagerSignOn();
	}

	private void OnDlcManagerInitialized()
	{
		GenerateBlockPrefabs();
		GenerateLevelPrefabs();
	}

	private void OnDestroy()
	{
		if (dlcManager != null)
		{
			DlcManager obj = dlcManager;
			obj.DlcManagerInitialized = (Action)Delegate.Remove(obj.DlcManagerInitialized, new Action(OnDlcManagerInitialized));
			dlcManager.CleanUp();
		}
		dlcManager = null;
	}

	private void Update()
	{
		if (dlcManager != null)
		{
			dlcManager.OnUpdate();
		}
	}

	private void NotifyDlcManagerSignOn()
	{
		dlcManager.OnUserSignin();
	}

	public static LevelPrefab GetPrefab(StatMaster.Category category, int index)
	{
		Ordered.Dictionary<int, LevelPrefab> value;
		if (LevelPrefabs.TryGetValue((int)category, out value) && index < value.Count)
		{
			return value.ElementAt(index).Value;
		}
		return null;
	}

	public void GenerateLevelPrefabs()
	{
		if (levelPrefabContainer == null)
		{
			levelPrefabContainer = base.transform.FindChild("OBJECTS/Prefabs");
		}
		if (levelPrefabContainer == null)
		{
			Debug.LogError("Could not find OBJECTS/Prefabs object. Level prefabs won't be properly set up", base.transform);
			return;
		}
		_levelPrefabs.Add(10, new Ordered.Dictionary<int, LevelPrefab>());
		foreach (Transform item in levelPrefabContainer)
		{
			if (item.name == "Unused")
			{
				continue;
			}
			foreach (Transform item2 in item)
			{
				CreateLevelPrefab(item2);
			}
		}
	}

	public static void CreateLevelPrefab(Transform prefab)
	{
		LevelPrefab component = prefab.GetComponent<LevelPrefab>();
		if (!SingleInstance<PrefabMaster>.Instance.dlcManager.AddLevelPrefab(component))
		{
			UnityEngine.Object.Destroy(component.gameObject);
			return;
		}
		BasicInfo component2 = prefab.GetComponent<BasicInfo>();
		if (component2 != null)
		{
			component2.CalculateDensity();
		}
		if (component.category == StatMaster.Category.Weather)
		{
			Debug.LogError(component.transform.parent.name + "/" + component.transform.name + ": " + component.ID + " is assigned to the Weather category, this is an outdated category");
			component.category = StatMaster.Category.Virtual;
		}
		else if (component.category == StatMaster.Category.All)
		{
			Debug.LogError(component.transform.parent.name + "/" + component.transform.name + ": " + component.ID + " is assigned to the All category, this is not a valid category for level objects to be in");
			component.category = StatMaster.Category.Buildings;
		}
		if (!SingleInstance<PrefabMaster>.Instance._levelPrefabs.ContainsKey((int)component.category))
		{
			SingleInstance<PrefabMaster>.Instance._levelPrefabs.Add((int)component.category, new Ordered.Dictionary<int, LevelPrefab>());
		}
		if (SingleInstance<PrefabMaster>.Instance._levelPrefabs[(int)component.category].ContainsKey(component.ID))
		{
			LevelPrefab value = SingleInstance<PrefabMaster>.Instance._levelPrefabs[(int)component.category].GetValue(component.ID);
			Debug.LogError(component.transform.parent.name + "/" + component.transform.name + ": " + component.ID + " already exists as: " + value.transform.parent.name + "/" + value.transform.name);
			return;
		}
		SingleInstance<PrefabMaster>.Instance._levelPrefabs[(int)component.category].Add(component.ID, component);
		if (SingleInstance<PrefabMaster>.Instance._levelPrefabs[10].ContainsKey(component.ID))
		{
			LevelPrefab value2;
			if (SingleInstance<PrefabMaster>.Instance._levelPrefabs[10].TryGetValue(component.ID, out value2))
			{
				Debug.LogError(component.transform.parent.name + "/" + component.transform.name + ": " + component.ID + " already exists as: " + value2.transform.parent.name + "/" + value2.transform.name);
			}
			else
			{
				Debug.LogError(component.transform.parent.name + "/" + component.transform.name + ": " + component.ID + " already present in dictionary but no item found");
			}
		}
		else
		{
			SingleInstance<PrefabMaster>.Instance._levelPrefabs[10].Add(component.ID, component);
		}
	}

	public static void RemoveLevelPrefab(StatMaster.Category category, int id)
	{
		if (id < SingleInstanceFindOnly<ModManager>.Instance.EntityIdStart)
		{
			throw new Exception("Tried to remove vanilla level prefab!");
		}
		SingleInstance<PrefabMaster>.Instance._levelPrefabs[(int)category].Remove(id);
		SingleInstance<PrefabMaster>.Instance._levelPrefabs[10].Remove(id);
	}

	private static BlockPrefab InitPrefab(Transform blockPrefab)
	{
		BlockPrefabContainer component = blockPrefab.GetComponent<BlockPrefabContainer>();
		BlockBehaviour component2 = blockPrefab.GetComponent<BlockBehaviour>();
		BlockPrefab info = component.Info;
		PrefabTransform[] componentsInChildren = blockPrefab.GetComponentsInChildren<PrefabTransform>(true);
		foreach (PrefabTransform prefabTransform in componentsInChildren)
		{
			prefabTransform.Apply(info);
			UnityEngine.Object.Destroy(prefabTransform.gameObject);
		}
		UnityEngine.Object.Destroy(component);
		component2.Prefab = (info.strippedBlock.Prefab = info);
		component2.SetUpPrefab();
		return info;
	}

	public static void GenerateBlockPrefabs()
	{
		if (SingleInstance<PrefabMaster>.Instance.blockPrefabContainer == null)
		{
			SingleInstance<PrefabMaster>.Instance.blockPrefabContainer = SingleInstance<PrefabMaster>.Instance.transform.FindChild("BLOCKS/Prefabs");
		}
		if (SingleInstance<PrefabMaster>.Instance.networkBlockContainer == null)
		{
			Transform transform = new GameObject("NetworkBlocks").transform;
			transform.SetParent(SingleInstance<PrefabMaster>.Instance.blockPrefabContainer.parent, true);
			SingleInstance<PrefabMaster>.Instance.networkBlockContainer = transform;
		}
		enumLength = Enum.GetNames(typeof(BlockType)).Length;
		SingleInstance<PrefabMaster>.Instance._blockPrefabs = new System.Collections.Generic.Dictionary<int, BlockPrefab>();
		if (!SingleInstance<PrefabMaster>.Instance.blockPrefabContainer)
		{
			return;
		}
		foreach (Transform item in SingleInstance<PrefabMaster>.Instance.blockPrefabContainer)
		{
			BlockPrefab blockPrefab = InitPrefab(item);
			if (!SingleInstance<PrefabMaster>.Instance._blockPrefabs.ContainsKey(blockPrefab.ID))
			{
				blockPrefab.SetGameObject(item.gameObject);
				SingleInstance<PrefabMaster>.Instance.dlcManager.AddBlock(item.GetComponent<BlockPrefabContainer>());
				AddBlockPrefab(blockPrefab.Type, blockPrefab);
			}
		}
	}

	public static void AddBlockPrefab(BlockType blockType, BlockPrefab prefab)
	{
		if (!SingleInstance<PrefabMaster>.Instance._blockPrefabs.ContainsKey((int)blockType))
		{
			SingleInstance<PrefabMaster>.Instance._blockPrefabs.Add((int)blockType, prefab);
			CreateNetworkPrefab((int)blockType, prefab);
			SingleInstance<PrefabMaster>.Instance._blockGhosts.Add(prefab.ghost);
			SingleInstance<PrefabMaster>.Instance._strippedBlockPrefabs.Add((int)blockType, prefab.strippedBlock);
			prefab.ConcludePrefab();
		}
	}

	private static void CreateNetworkPrefab(int ID, BlockPrefab prefab)
	{
		if (!SingleInstanceFindOnly<BlockLoader>.Instance.IsModBlock(ID))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(prefab.gameObject, SingleInstance<PrefabMaster>.Instance.networkBlockContainer) as GameObject;
			gameObject.name = prefab.gameObject.name;
			UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<BlockPrefabContainer>());
			BlockBehaviour component = gameObject.GetComponent<BlockBehaviour>();
			component.Prefab = prefab;
			AddNetworkBlock(gameObject);
			SingleInstance<PrefabMaster>.Instance._networkBlocks.Add(ID, component);
		}
	}

	public static bool GetBlock(BlockType blockType, PrefabType prefabType, out BlockBehaviour block)
	{
		if (SingleInstanceFindOnly<BlockLoader>.Instance.IsModBlock((int)blockType))
		{
			prefabType = PrefabType.Normal;
		}
		switch (prefabType)
		{
		case PrefabType.Normal:
		{
			BlockPrefab value;
			if (SingleInstance<PrefabMaster>.Instance._blockPrefabs.TryGetValue((int)blockType, out value))
			{
				block = value.blockBehaviour;
				return true;
			}
			break;
		}
		case PrefabType.Stripped:
			if (SingleInstance<PrefabMaster>.Instance._strippedBlockPrefabs.TryGetValue((int)blockType, out block))
			{
				return true;
			}
			break;
		case PrefabType.Network:
			if (SingleInstance<PrefabMaster>.Instance._networkBlocks.TryGetValue((int)blockType, out block))
			{
				return true;
			}
			break;
		}
		return GetBlock(blockType, out block);
	}

	public static void AddNetworkBlock(GameObject go)
	{
		NetworkBlock networkBlock = go.AddComponent<NetworkBlock>();
		networkBlock.blockBehaviour = go.GetComponent<BlockBehaviour>();
		networkBlock.blockBehaviour.SetNetworkBlock(networkBlock);
		networkBlock.isBlock = true;
		networkBlock.FetchComponents();
	}

	public static BlockPrefab RemoveBlockPrefab(int id)
	{
		if (id < enumLength)
		{
			throw new Exception("Tried to remove vanilla block!");
		}
		if (!SingleInstance<PrefabMaster>.Instance._blockPrefabs.ContainsKey(id))
		{
			return null;
		}
		BlockPrefab result = SingleInstance<PrefabMaster>.Instance._blockPrefabs[id];
		SingleInstance<PrefabMaster>.Instance._blockPrefabs.Remove(id);
		SingleInstance<PrefabMaster>.Instance._strippedBlockPrefabs.Remove(id);
		return result;
	}

	public static bool GetNetworkBlock(BlockType blockType, out BlockBehaviour block)
	{
		block = null;
		if (!SingleInstance<PrefabMaster>.Instance._networkBlocks.TryGetValue((int)blockType, out block))
		{
			return GetBlock(blockType, out block);
		}
		return true;
	}

	public static bool GetPrefab(BlockType blockType, out BlockPrefab prefab)
	{
		if (SingleInstance<PrefabMaster>.Instance._blockPrefabs.TryGetValue((int)blockType, out prefab))
		{
			return true;
		}
		return false;
	}

	public static bool GetBlock(BlockType blockType, out BlockBehaviour block)
	{
		BlockPrefab value;
		if (!SingleInstance<PrefabMaster>.Instance._blockPrefabs.TryGetValue((int)blockType, out value))
		{
			block = null;
			return false;
		}
		block = value.blockBehaviour;
		return true;
	}

	public static Vector3 GetDefaultScale(BlockType blockID)
	{
		return Vector3.one;
	}

	public static bool GetStrippedBlock(BlockType blockType, out BlockBehaviour block)
	{
		block = null;
		if (!SingleInstance<PrefabMaster>.Instance._strippedBlockPrefabs.TryGetValue((int)blockType, out block))
		{
			return GetBlock(blockType, out block);
		}
		return true;
	}
}
