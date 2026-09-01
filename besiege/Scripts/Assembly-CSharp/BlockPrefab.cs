using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Blocks;
using Newtonsoft.Json.Utilities;
using UnityEngine;

[Serializable]
public class BlockPrefab
{
	public BlockType Type;

	public int ID;

	public string name;

	public int locID = -1;

	public string[] nameKeywords;

	public PlaceMode placeMode;

	public Vector3 placementOffset = Vector3.zero;

	public float rotationOffset;

	public GameObject gameObject;

	public GameObject ghost;

	public BlockBehaviour blockBehaviour;

	public BlockBehaviour strippedBlock;

	public BlockVisualController VisualController;

	public bool RegisterBuildUpdate;

	public bool RegisterBuildFixedUpdate;

	public bool RegisterBuildLateUpdate;

	public bool RegisterSimUpdate;

	public bool RegisterSimFixedUpdate;

	public bool RegisterSimLateUpdate;

	public bool RegisterEmulationUpdate;

	public bool EmulatesAnyKeys;

	public bool SetBreakForce = true;

	public bool SetColliderIterations;

	public bool SetVelocityIterations = true;

	public bool SetMaxAngularVelocity;

	public int IterationCount = 10;

	public int VelocityIterationCount = 10;

	public int MaxAngularVelocity;

	public bool hasBVC;

	public bool hasMyBounds = true;

	public DamageIgnoreSetting blockDamageSetting = DamageIgnoreSetting.Include;

	public MachineDamageType[] blockDamageTypes;

	public PhysFinishSetting physFinishSetting;

	public bool mechanicalJoint;

	public bool hasDamageType;

	public DamageType myDamageType;

	public bool isArmor;

	public bool endBlock;

	public bool canBurn;

	public bool canFreeze;

	public bool hasHealthBar;

	public float blockHealth;

	public bool reduceBreakforce;

	public bool bleedOnTexture;

	public Vector3 rayPosition;

	public bool hasArrow;

	public bool SkinCanBeChanged = true;

	public bool canBeHeated;

	public bool Unused;

	public bool hasMeshFilter;

	public bool skinnedRenderer;

	public bool hasFragment;

	public bool hasShortVis;

	public bool clusterBaseCandidate;

	public float heatLerpSpeed = 2f;

	public Color heatGlowColor = new Color(1f, 0.26f, 0f);

	public string heatColorName = "_EmissCol";

	public Color burnColor = new Color(0.2f, 0.2f, 0.2f, 1f);

	[Tooltip("Defaults to locID. Automatically word-wraps if one localisation is used, otherwise each loc is on its own line.")]
	[Header("Tooltip")]
	public int[] nameLocalisations;

	public int[] upperDescLocalisations;

	[Tooltip("Defaults to 'Control With'")]
	public int keyUpperLocalisation = 563;

	public ControlScheme.BlockControls keyGroup = ControlScheme.BlockControls.None;

	public int firstKeyLocalisation;

	public int secondKeyLocalisation;

	public int[] lowerDescLocalisations;

	[Tooltip("Will always word-wrap unless newlines are present.")]
	public int[] flipLocalisations;

	public int warningLocalisation;

	[Tooltip("If this block doesn't actually exist as a physical object (e.g. fixed camera block).")]
	public bool isVirtualBlock;

	public string massOverride;

	[Tooltip("0 = disabled, ≤0.5 = ↓↓, ≤0.95 = ↓, >0.95 & <1.05 = ~, ≥1.05 = ↑, ≥2 = ↑↑")]
	public float buoyancyOverride;

	[NonSerialized]
	[HideInInspector]
	public BlockButtonControl[] buttonIcons = new BlockButtonControl[1];

	[HideInInspector]
	public Guid modId = Guid.Empty;

	[HideInInspector]
	public int localId;

	[HideInInspector]
	public List<BlockSkinLoader.SkinPack.Skin> officialSkins;

	[HideInInspector]
	public List<BlockSkinLoader.SkinPack.Skin> downloadedSkins;

	[HideInInspector]
	public List<BlockVisualController> blockVisControllers;

	protected List<BlockSkinLoader.SkinPack.Skin> allAvailableSkins;

	protected GhostMaterialController _ghostController;

	protected BlockSkinLoader.SkinPack.Skin _defaultSkin;

	private static readonly string[] BlockTypeNames = Enum.GetNames(typeof(BlockType));

	public bool AutoCompletePlacement
	{
		get
		{
			return !Machine.IsDraggedBlock(Type);
		}
	}

	public bool CanGetNewVisuals
	{
		get
		{
			return SkinCanBeChanged && (CanChangeMesh || CanChangeTexture);
		}
	}

	public bool LoadNewVisuals
	{
		get
		{
			return CanGetNewVisuals && !Unused;
		}
	}

	public bool CanChangeMesh
	{
		get
		{
			return (hasMeshFilter && Type != BlockType.BuildSurface) || Type == BlockType.Sail;
		}
	}

	public bool CanChangeTexture
	{
		get
		{
			return VisualController.CanChangeTexture;
		}
	}

	public bool HasAnyAlternateVisuals
	{
		get
		{
			return AvailableSkins.Count > 0;
		}
	}

	public BlockSkinLoader.SkinPack.Skin SelectedSkin
	{
		get
		{
			return VisualController.selectedSkin;
		}
	}

	public BlockSkinLoader.SkinPack.Skin DefaultSkin
	{
		get
		{
			if (_defaultSkin == null)
			{
				_defaultSkin = ((!officialSkins.Any()) ? FindDefaultSkin() : officialSkins[0]);
			}
			return _defaultSkin;
		}
	}

	public List<BlockSkinLoader.SkinPack.Skin> AvailableSkins
	{
		get
		{
			if (allAvailableSkins == null || allAvailableSkins.Count != officialSkins.Count + downloadedSkins.Count)
			{
				return allAvailableSkins = new List<BlockSkinLoader.SkinPack.Skin>(officialSkins).Concat(downloadedSkins).ToList();
			}
			return allAvailableSkins;
		}
	}

	public GhostMaterialController ghostController
	{
		get
		{
			if (_ghostController == null)
			{
				_ghostController = ghost.GetComponent<GhostMaterialController>();
				_ghostController.Awake();
			}
			return _ghostController;
		}
	}

	public BlockPrefab()
	{
		officialSkins = new List<BlockSkinLoader.SkinPack.Skin>();
		downloadedSkins = new List<BlockSkinLoader.SkinPack.Skin>();
		blockVisControllers = new List<BlockVisualController>();
	}

	public void SetIcons(BlockButtonControl[] icons)
	{
		BlockType type = Type;
		if (type == BlockType.Unused3)
		{
			Debug.Log(icons[0]);
		}
		buttonIcons = icons;
		if (SkinCanBeChanged)
		{
			VisualController.SetPrefabIcons();
		}
	}

	public BlockButtonControl GetButtonIcon(int i = 0)
	{
		BlockType type = Type;
		if (type == BlockType.Unused3)
		{
			if (HasButtonIcons() && buttonIcons[0] != null)
			{
				return buttonIcons[i];
			}
			if (PrefabMaster.BlockPrefabs.Count < 56)
			{
				return PrefabMaster.BlockPrefabs[26].buttonIcons[i];
			}
			return PrefabMaster.BlockPrefabs[55].buttonIcons[i];
		}
		return buttonIcons[i];
	}

	public bool HasButtonIcons()
	{
		return buttonIcons != null && buttonIcons.Length > 0;
	}

	public int ButtonIconCount()
	{
		return buttonIcons.Length;
	}

	public BlockSkinLoader.SkinPack.Skin FindDefaultSkin()
	{
		if (ID < BlockSkinLoader.defaultPack.skins.Count)
		{
			return BlockSkinLoader.defaultPack.skins[ID];
		}
		return null;
	}

	public void OverrideLists(BlockPrefab prefab)
	{
		officialSkins = prefab.officialSkins;
		downloadedSkins = prefab.downloadedSkins;
		blockVisControllers = prefab.blockVisControllers;
		allAvailableSkins = prefab.allAvailableSkins;
	}

	public void InitSimComponents(BlockBehaviour block)
	{
		Machine parentMachine = block.ParentMachine;
		if (hasHealthBar && block.ShouldInitHealth)
		{
			block.BlockHealth.Init(parentMachine, block);
		}
		if (hasDamageType && block.Prefab.Type != BlockType.MetalJaw)
		{
			BlockDamageType component = block.GetComponent<BlockDamageType>();
			if (block.SimPhysics)
			{
				component.Init(parentMachine, block);
			}
			else
			{
				UnityEngine.Object.Destroy(component);
			}
		}
	}

	public int GetCount()
	{
		return blockVisControllers.Count;
	}

	public static int GetCount(BlockType t)
	{
		return PrefabMaster.BlockPrefabs[(int)t].blockVisControllers.Count;
	}

	public static int GetCount(int t)
	{
		return PrefabMaster.BlockPrefabs[t].blockVisControllers.Count;
	}

	public void SkinModified(BlockSkinLoader.SModifier m)
	{
		BlockSkinLoader.SkinPack.Skin skin = null;
		if (m == null)
		{
			return;
		}
		if (m is BlockSkinLoader.SkinPack.Skin)
		{
			skin = m as BlockSkinLoader.SkinPack.Skin;
			if (BlockSkinLoader.defaultPack == null || skin == null || skin.pack == BlockSkinLoader.defaultPack)
			{
				return;
			}
			if (VisualController != null && (VisualController.selectedSkin == null || VisualController.selectedSkin == skin || VisualController.selectedSkin.shortSkin == skin))
			{
				VisualController.UpdateVis();
			}
			{
				foreach (BlockVisualController blockVisController in blockVisControllers)
				{
					if ((blockVisController != null && blockVisController.selectedSkin == null) || blockVisController.selectedSkin == skin || blockVisController.selectedSkin.shortSkin == skin)
					{
						blockVisController.UpdateVis();
					}
				}
				return;
			}
		}
		if (m is BlockSkinLoader.SkinPack)
		{
			BlockSkinLoader.SkinPack skinPack = m as BlockSkinLoader.SkinPack;
			if (skinPack == null || !skinPack.deleted)
			{
				return;
			}
			if (VisualController != null && (VisualController.selectedSkin == null || VisualController.selectedSkin.pack == skinPack))
			{
				VisualController.UpdateVis();
			}
			{
				foreach (BlockVisualController blockVisController2 in blockVisControllers)
				{
					if ((blockVisController2 != null && blockVisController2.selectedSkin == null) || blockVisController2.selectedSkin.pack == skinPack)
					{
						blockVisController2.UpdateVis();
					}
				}
				return;
			}
		}
		if (m != BlockSkinLoader.UpdateAll || SingleInstance<BlockSkinLoader>.Instance.UseAnimation)
		{
			return;
		}
		if (VisualController != null)
		{
			VisualController.UpdateVis();
		}
		foreach (BlockVisualController blockVisController3 in blockVisControllers)
		{
			if (blockVisController3 != null)
			{
				blockVisController3.UpdateVis();
			}
		}
	}

	public BlockPrefab SetNameFromEnum()
	{
		name = Enum.GetName(typeof(BlockType), ID);
		if (gameObject != null)
		{
			gameObject.name = name;
		}
		if (ghost != null)
		{
			ghost.name = name + "Ghost";
		}
		return this;
	}

	public BlockPrefab SetNameFromGameObject()
	{
		if (gameObject != null)
		{
			name = gameObject.name;
		}
		if (ghost != null)
		{
			ghost.name = name + "Ghost";
		}
		return this;
	}

	public void ConcludePrefab()
	{
		if (VisualController != null)
		{
			VisualController.InitPrefab(ID);
		}
	}

	public BlockPrefab SetupSkinFromGameObject(GameObject go)
	{
		Material[] array = null;
		BlockPrefab prefab = VisualController.Prefab;
		Mesh mesh;
		if (prefab.skinnedRenderer)
		{
			SkinnedMeshRenderer meshRenderer = (VisualController as BlockSkinnedVisualController).meshRenderer;
			mesh = meshRenderer.sharedMesh;
			array = meshRenderer.materials;
		}
		else
		{
			if (VisualController.renderers.Length > 1)
			{
				List<Material> list = new List<Material>();
				bool flag = true;
				for (int i = 0; i < VisualController.renderers.Length; i++)
				{
					if (i > 0 && VisualController.renderers[i].material != VisualController.renderers[i - 1].material)
					{
						flag = false;
					}
					list.Add(VisualController.renderers[i].material);
				}
				array = ((!flag) ? list.ToArray() : VisualController.renderers[0].materials);
			}
			else if (VisualController.renderers.Length > 0)
			{
				array = ((!(VisualController.renderers[0] == null)) ? VisualController.renderers[0].materials : null);
			}
			mesh = ((!prefab.hasMeshFilter) ? null : VisualController.MeshFilter.sharedMesh);
		}
		Texture texture = ((array != null && array.Length > 0) ? array[0].mainTexture : null);
		_defaultSkin = BlockSkinLoader.defaultPack.CreateSkinFor(this);
		_defaultSkin.SetPreloaded(mesh, texture, array);
		_defaultSkin.SetActive(true);
		string text = prefab.Type.ToString();
		string text2 = "\\Skins\\Template\\";
		if (prefab.CanChangeMesh)
		{
			_defaultSkin.objPath = StaticSettings.DataPath + text2 + text + "\\" + text + ".obj";
		}
		if (prefab.CanChangeTexture)
		{
			_defaultSkin.texPath = StaticSettings.DataPath + text2 + text + "\\" + text + ".png";
		}
		BlockType iD = (BlockType)ID;
		if (iD == BlockType.SingleWoodenBlock)
		{
			BlockSkinLoader.defaultPack.skins[1].shortSkin = DefaultSkin;
		}
		else if (hasShortVis)
		{
			BlockSkinLoader.SkinPack.Skin skin = BlockSkinLoader.SkinPack.Skin.Incomplete(ID, BlockSkinLoader.defaultPack);
			skin.SetPreloaded((VisualController as FragmentVisualController).shortVis.filter.sharedMesh, null, null);
			skin.SetActive(true);
			_defaultSkin.shortSkin = skin;
		}
		BlockSkinLoader.SkinModified += SkinModified;
		return this;
	}

	public BlockPrefab SetGameObject(GameObject go)
	{
		gameObject = go;
		VisualController = gameObject.GetComponent<BlockVisualController>();
		bool flag = false;
		BlockBehaviour[] components = go.GetComponents<BlockBehaviour>();
		foreach (BlockBehaviour blockBehaviour in components)
		{
			if (flag)
			{
				Debug.LogWarning(go.name + " has multiple BlockBehaviour components.");
				break;
			}
			flag = true;
			this.blockBehaviour = blockBehaviour;
			this.blockBehaviour.isPrefab = true;
			this.blockBehaviour.objectIsPrefab = true;
		}
		if (this.blockBehaviour is ModBlockBehaviourHandler)
		{
			ModdedBlock moddedBlock = ((ModBlockBehaviourHandler)this.blockBehaviour).moddedBlock;
			modId = moddedBlock.Info.Mod.Info.Id;
			localId = moddedBlock.LocalId;
		}
		return SetupSkinFromGameObject(go);
	}

	public static int TryParseBlockId(string prefabName)
	{
		if (string.IsNullOrEmpty(prefabName))
		{
			return -1;
		}
		int num = BlockTypeNames.IndexOf((string x) => x.Equals(prefabName));
		if (num == -1)
		{
			return -1;
		}
		return num;
	}
}
