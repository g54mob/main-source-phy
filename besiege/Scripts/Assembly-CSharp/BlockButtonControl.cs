using System;
using UnityEngine;
using UnityEngine.Rendering;

public class BlockButtonControl : UIButton
{
	public static BlockButtonControl ActiveButton;

	[HideInInspector]
	public bool initialized;

	public int myIndex;

	public Renderer bg;

	public BlockMenuControl blockMenuControllerCode;

	public BlockViewerController visBoxCode;

	public DynamicText limitsText;

	[HideInInspector]
	public MeshRenderer myRenderer;

	[HideInInspector]
	public MeshRenderer secondaryRen;

	[HideInInspector]
	public MeshFilter myMeshFilter;

	protected BlockSkinLoader.SkinPack.Skin skin;

	protected Material[] defaultMat;

	protected bool splitMats;

	[HideInInspector]
	public float targetMag = 1f;

	public float defaultSize = 1.1f;

	protected int lastCount;

	protected bool assigned;

	protected bool limitDelegateUsed;

	protected MaterialPropertyBlock props;

	private BlockBehaviour blockBehaviour;

	private ServerMachine lastMachine;

	private Vector3 startScale = Vector3.one;

	private bool canChangeSkin = true;

	protected FauxTransform _alignment;

	private bool setup;

	public FauxTransform Alignment
	{
		get
		{
			if (_alignment == null)
			{
				Transform transform = myRenderer.transform;
				_alignment = new FauxTransform(transform.localPosition, transform.localRotation, transform.localScale);
			}
			return _alignment;
		}
	}

	public static event Action<BlockButtonControl> CreatedButton;

	public void InvalidateAlignment()
	{
		_alignment = null;
	}

	public void StartDisregardInactive()
	{
		Setup();
		BlockPrefab value;
		if (PrefabMaster.BlockPrefabs.TryGetValue(myIndex, out value))
		{
			BlockButtonControl buttonIcon = value.GetButtonIcon();
			if (buttonIcon == null)
			{
				value.SetIcons(new BlockButtonControl[1] { this });
			}
			else if (this != buttonIcon)
			{
				value.SetIcons(new BlockButtonControl[2] { buttonIcon, this });
			}
		}
	}

	public void OnDestroyDisregardInactive()
	{
		UnassignDelegate();
		if (assigned)
		{
			ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineChanged, new Action<Machine>(AssignDelegate));
			assigned = false;
		}
	}

	public void Setup()
	{
		if (setup)
		{
			return;
		}
		Start();
		setup = true;
		myRenderer = base.transform.FindChild("IconPivot/Icon").GetComponent<MeshRenderer>();
		Transform transform = ((myRenderer.transform.childCount <= 0) ? null : myRenderer.transform.GetChild(0));
		secondaryRen = ((!(transform == null)) ? transform.GetComponent<MeshRenderer>() : null);
		myMeshFilter = myRenderer.GetComponent<MeshFilter>();
		myRenderer.shadowCastingMode = ShadowCastingMode.Off;
		defaultMat = myRenderer.materials;
		targetMag = myRenderer.bounds.size.magnitude;
		startScale = myRenderer.transform.localScale;
		if (BlockButtonControl.CreatedButton != null)
		{
			BlockButtonControl.CreatedButton(this);
		}
		BlockType blockType = (BlockType)myIndex;
		BlockPrefab prefab;
		if (PrefabMaster.GetPrefab(blockType, out prefab))
		{
			canChangeSkin = prefab.SkinCanBeChanged;
		}
		if (assigned)
		{
			return;
		}
		ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineChanged, new Action<Machine>(AssignDelegate));
		if (StatMaster.isMP)
		{
			if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
			{
				AssignDelegate(PlayerData.localPlayer.machine);
			}
		}
		else
		{
			Machine machine = Machine.Active();
			if (machine != null)
			{
				AssignDelegate(machine);
			}
		}
		assigned = true;
	}

	private void Start()
	{
		if (props == null)
		{
			props = new MaterialPropertyBlock();
		}
		props.SetFloat("_GridSpacing", 0.75f);
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (myRenderer != null && myRenderer.material.HasProperty("_GridSpacing"))
		{
			myRenderer.SetPropertyBlock(props);
		}
	}

	private void Initialize()
	{
		if (!initialized)
		{
			PrefabMaster.GetBlock((BlockType)myIndex, out blockBehaviour);
			initialized = true;
		}
	}

	protected override bool _InvokeOnDown()
	{
		if (base._InvokeOnDown())
		{
			Set();
		}
		return false;
	}

	private void ActivateButton()
	{
		ActiveButton = this;
		LevelEditor instance = LevelEditor.Instance;
		if (instance != null)
		{
			instance.ResetWindow();
		}
	}

	public void Set()
	{
		BlockType blockType = (BlockType)myIndex;
		if (!canChangeSkin)
		{
			base.transform.root.GetComponent<AudioSource>().Play();
		}
		SingleInstanceFindOnly<AddPiece>.Instance.SetBlockType(blockType);
		if (SingleInstanceFindOnly<AddPiece>.Instance.CurrentType == blockType || (SingleInstanceFindOnly<AddPiece>.Instance.CurrentType == BlockType.BuildNode && (blockType == BlockType.BuildEdge || blockType == BlockType.BuildSurface)))
		{
			UpdateLimitText();
			bg.gameObject.SetActive(true);
			AddPiece.usingCopiedBlock = false;
			blockMenuControllerCode.CheckIfActive(true);
			ActivateButton();
			if (StatMaster.isSearching)
			{
				BlockTabController tabController = blockMenuControllerCode.TabController;
				tabController.OpenTabWithBlock(myIndex);
			}
			BlockTabController.toolControllerCode.DisableAll();
		}
	}

	public void AssignDelegate(Machine m)
	{
		Setup();
		if (!StatMaster.isMP)
		{
			if (limitsText != null)
			{
				limitsText.gameObject.SetActive(false);
			}
			if (myRenderer != null)
			{
				BlockPrefab value;
				if (skin == null && PrefabMaster.BlockPrefabs.TryGetValue(myIndex, out value))
				{
					skin = value.DefaultSkin;
				}
				SetMaterialFromSkin();
			}
			return;
		}
		UnassignDelegate();
		if (m != null)
		{
			lastMachine = m as ServerMachine;
			ServerMachine serverMachine = lastMachine;
			serverMachine.BannedBlocksUpdated = (BannedBlocksUpdated)Delegate.Combine(serverMachine.BannedBlocksUpdated, new BannedBlocksUpdated(UpdateLimitText));
			limitDelegateUsed = true;
		}
		if (base.transform.parent.gameObject.activeSelf)
		{
			UpdateLimitText();
		}
	}

	public void UnassignDelegate()
	{
		if (limitDelegateUsed)
		{
			if (lastMachine != null)
			{
				ServerMachine serverMachine = lastMachine;
				serverMachine.BannedBlocksUpdated = (BannedBlocksUpdated)Delegate.Remove(serverMachine.BannedBlocksUpdated, new BannedBlocksUpdated(UpdateLimitText));
			}
			limitDelegateUsed = false;
		}
	}

	public void OnDestroy()
	{
		if (assigned)
		{
			ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineChanged, new Action<Machine>(AssignDelegate));
			assigned = false;
		}
	}

	public void UpdateLimitText()
	{
		if (limitsText == null)
		{
			SetMaterial();
			return;
		}
		limitsText.gameObject.SetActive(StatMaster.isMP && LevelEditor.Instance != null && LevelEditor.Instance.Settings != null && LevelEditor.Instance.Settings.GetBlockLimit((BlockType)myIndex) != -1);
		if (limitsText.gameObject.activeInHierarchy)
		{
			int blockLimit = LevelEditor.Instance.Settings.GetBlockLimit((BlockType)myIndex);
			if (limitDelegateUsed && !PlayerData.localPlayer.isSpectator)
			{
				lastCount = (lastMachine.BlockTypeCount.ContainsKey(myIndex) ? lastMachine.BlockTypeCount[myIndex] : 0);
				if (lastCount <= blockLimit)
				{
					limitsText.color.a = 0.65f;
				}
				else
				{
					limitsText.color.a = 1f;
				}
				ReferenceMaster.SetDynamicText(limitsText, lastCount + "/" + blockLimit);
			}
			else
			{
				limitsText.color.a = 0.65f;
				ReferenceMaster.SetDynamicText(limitsText, "0/" + blockLimit);
			}
		}
		SetMaterial();
	}

	public void SetMesh(Mesh m)
	{
		Setup();
		if (myMeshFilter == null)
		{
			myRenderer = base.transform.FindChild("IconPivot/Icon").GetComponent<MeshRenderer>();
			myMeshFilter = myRenderer.GetComponent<MeshFilter>();
		}
		myMeshFilter.mesh = m;
		if (myIndex == 78)
		{
			secondaryRen.gameObject.SetActive(m != PrefabMaster.BlockPrefabs[myIndex].DefaultSkin.mesh);
		}
		if (targetMag == 0f && myRenderer != null)
		{
			targetMag = myRenderer.bounds.size.magnitude;
		}
		SetIconToMatch(Alignment);
		CorrectScaleForOutlierSkinSizes();
	}

	protected void SetIconToMatch(FauxTransform trans)
	{
		Vector3 localPosition = trans.localPosition;
		localPosition.z = myRenderer.transform.localPosition.z;
		myRenderer.transform.localPosition = localPosition;
		myRenderer.transform.localRotation = trans.localRotation;
		myRenderer.transform.localScale = trans.localScale;
	}

	protected void CorrectScaleForOutlierSkinSizes()
	{
		MeshRenderer meshRenderer = myRenderer;
		Vector3 size = meshRenderer.bounds.size;
		float magnitude = new Vector3(size.x, size.y, 0f).magnitude;
		if (magnitude != 0f)
		{
			if (Mathf.Abs(targetMag - magnitude) > 0.6f * targetMag)
			{
				float num = targetMag / magnitude;
				meshRenderer.transform.localScale *= num;
			}
			else
			{
				meshRenderer.transform.localScale = startScale * defaultSize;
			}
		}
	}

	public void SetMaterial(BlockSkinLoader.SkinPack.Skin s = null, bool? sm = null)
	{
		Setup();
		if (myRenderer == null)
		{
			Debug.LogWarning("Block Button doesn't have renderer");
			return;
		}
		if (sm.HasValue)
		{
			splitMats = sm.Value;
		}
		if (s != null)
		{
			skin = s;
		}
		if (skin == null)
		{
			BlockPrefab value;
			if (!PrefabMaster.BlockPrefabs.TryGetValue(myIndex, out value))
			{
				return;
			}
			skin = value.DefaultSkin;
		}
		if (limitDelegateUsed)
		{
			int blockLimit = LevelEditor.Instance.Settings.GetBlockLimit((BlockType)myIndex);
			if (blockLimit != -1 && !PlayerData.localPlayer.isSpectator)
			{
				lastCount = (lastMachine.BlockTypeCount.ContainsKey(myIndex) ? lastMachine.BlockTypeCount[myIndex] : 0);
				if (lastCount >= blockLimit)
				{
					myRenderer.materials = new Material[1] { skin.BannedIconMat };
					return;
				}
			}
		}
		SetMaterialFromSkin();
	}

	private void SetMaterialFromSkin()
	{
		if (skin == null || !skin.prefab.CanGetNewVisuals)
		{
			myRenderer.materials = defaultMat;
		}
		else if (skin.prefab.Type == BlockType.Sail)
		{
			Material[] materials = skin.materials;
			if (skin.materials.Length < 2)
			{
				materials = new Material[2]
				{
					skin.material,
					(PrefabMaster.BlockPrefabs[myIndex].blockBehaviour as SailBlock).sailMat
				};
			}
			secondaryRen.materials = materials;
			props.SetTexture("_SailTex", skin.material.mainTexture);
			myRenderer.SetPropertyBlock(props);
			secondaryRen.SetPropertyBlock(props);
		}
		else if (splitMats)
		{
			myRenderer.materials = new Material[1] { skin.material };
		}
		else
		{
			myRenderer.materials = skin.materials;
		}
	}

	public void Activate()
	{
		bg.gameObject.SetActive(true);
		ActivateButton();
		if ((bool)visBoxCode)
		{
			visBoxCode.Set(myIndex);
		}
		else
		{
			Debug.LogError("Missing weird visBoxCode");
		}
		if (base.transform.parent.gameObject.activeSelf)
		{
			UpdateLimitText();
		}
	}

	public void Deactivate()
	{
		bg.gameObject.SetActive(false);
	}

	public bool MatchesFilter(string filter)
	{
		Initialize();
		if (blockBehaviour == null)
		{
			return false;
		}
		BlockPrefab prefab = blockBehaviour.Prefab;
		string text = ReferenceMaster.TranslateBlockName(prefab.Type).ToLower();
		if (string.IsNullOrEmpty(text))
		{
			text = prefab.name.ToLower();
		}
		if (filter.Length > 1 && text.Contains(filter))
		{
			return true;
		}
		if (text.StartsWith(filter, StringComparison.Ordinal))
		{
			return true;
		}
		string[] array = filter.Split(' ');
		string[] array2 = text.Split(' ');
		string[] nameKeywords = prefab.nameKeywords;
		string[] array3 = array;
		foreach (string text2 in array3)
		{
			bool flag = false;
			string[] array4 = array2;
			foreach (string text3 in array4)
			{
				if (text3.Contains(text2))
				{
					flag = true;
					break;
				}
			}
			if (!flag && nameKeywords != null && text2.Length >= 2)
			{
				string[] array5 = nameKeywords;
				foreach (string text4 in array5)
				{
					if (text4.ToLower().StartsWith(text2, StringComparison.Ordinal))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}
}
