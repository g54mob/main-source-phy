using System;
using System.Collections;
using System.Collections.Generic;
using Besiege.Tooltips;
using Steamworks;
using UnityEngine;

public class PrefabVisualUI : SingleInstanceFindOnly<PrefabVisualUI>
{
	[NonSerialized]
	public int ID;

	public UIButton openButton;

	public UIButton openCloseButton;

	public UIButton prevPageButton;

	public UIButton nextPageButton;

	public UIButton refreshButton;

	public UIButton openWorkshopButton;

	public OpenFileBrowserButton settingsButton;

	public Transform downloadedBG;

	protected MeshRenderer downloadedBGren;

	public Transform optionsContainer;

	public DynamicText downloadedText;

	public DynamicText pageNumText;

	public bool bottomRightPageNum;

	public Material iconInactiveMaterial;

	public List<SelectSkinButton> officialIcons = new List<SelectSkinButton>();

	public List<SelectSkinButton> downloadedIcons = new List<SelectSkinButton>();

	public GameObject[] toggledObjects = new GameObject[0];

	public UIHoverArea[] mouseOverArea = new UIHoverArea[0];

	public MeshRenderer[] renderersToFade = new MeshRenderer[0];

	public MeshRenderer[] blurs = new MeshRenderer[0];

	public DynamicText[] textsToFade = new DynamicText[0];

	public bool collapsed = true;

	protected GameObject selectedTooltip;

	protected Collider selectedTrigger;

	protected int iconColumns;

	protected int numOfPages = 1;

	protected int currentPage;

	private float ySettingsButton;

	private float reverse = 1f;

	private bool awoken;

	public static int MAX_COLUMNS = 3;

	private static int ROWS = 3;

	private static Dictionary<int, List<BlockSkinLoader.SkinPack.Skin>> mostRecentSkins = new Dictionary<int, List<BlockSkinLoader.SkinPack.Skin>>();

	private int lastID = -1;

	private int lastDownloadCount = -1;

	public override string Name
	{
		get
		{
			return "PrefabVisualUI";
		}
	}

	protected override void Awake()
	{
		if (!awoken)
		{
			base.Awake();
			awoken = true;
			ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnSimulationToggle));
			BlockSkinLoader.SkinModified += UpdateDisplay;
			StatMaster.SelectedBlockChanged += SetUIBasedOnID;
			StatMaster.LevelEditingToggled += UpdateUIFromLevelEditor;
			ReferenceMaster.OnConnect += ResetUI;
			openButton.Click += Open;
			openCloseButton.Down += Toggle;
			openCloseButton.MouseEnter += TooltipMouseEnter;
			openCloseButton.MouseExit += TooltipMouseExit;
			CursorHoverHook cursorHoverHook = openCloseButton.gameObject.AddComponent<CursorHoverHook>();
			cursorHoverHook.onCursorOver = (Action)Delegate.Combine(cursorHoverHook.onCursorOver, new Action(TooltipMouseOver));
			prevPageButton.Click += PrevPage;
			nextPageButton.Click += NextPage;
			refreshButton.Click += RefreshSkins;
			if (openWorkshopButton != null)
			{
				openWorkshopButton.Click += OpenWorkshopSkinsOverlay;
			}
			else
			{
				Debug.LogError("openWorkshopButton doesn't have a reference on PrefabVisualUI!", base.gameObject);
			}
			Setup();
		}
	}

	private void OnSimulationToggle(bool isSim)
	{
		base.gameObject.SetActive(!isSim);
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnSimulationToggle));
		BlockSkinLoader.SkinModified -= UpdateDisplay;
		StatMaster.SelectedBlockChanged -= SetUIBasedOnID;
		ReferenceMaster.OnConnect -= ResetUI;
		StatMaster.LevelEditingToggled -= UpdateUIFromLevelEditor;
		refreshButton.Click -= RefreshSkins;
	}

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (!collapsed && ((!StatMaster.hudOccluding && !MouseOver() && InputManager.LeftMouseButton()) || ReferenceMaster.activeMachineSimulating))
		{
			_Close();
		}
	}

	public bool MouseOver()
	{
		UIHoverArea[] array = mouseOverArea;
		foreach (UIHoverArea uIHoverArea in array)
		{
			if (uIHoverArea.isMouseOver)
			{
				return true;
			}
		}
		return false;
	}

	public void Toggle()
	{
		if (collapsed)
		{
			if (StatMaster.SelectedLevelPrefab != null || StatMaster.Mode.LevelEditor.selectedTool != StatMaster.Tool.None)
			{
				ReferenceMaster.ResetLevelEditor();
			}
			else
			{
				Open();
			}
		}
		else
		{
			if (StatMaster.SelectedLevelPrefab != null || StatMaster.Mode.LevelEditor.selectedTool != StatMaster.Tool.None)
			{
				ReferenceMaster.ResetLevelEditor();
			}
			Close();
		}
		if (collapsed)
		{
			BlockTooltipHolder tooltip = SingleInstance<BlockTooltipController>.Instance.GetTooltip((BlockType)ID);
			if ((bool)tooltip)
			{
				tooltip.tooltipCode.OnClicked();
			}
		}
	}

	public void Setup()
	{
		if (ySettingsButton == 0f)
		{
			ySettingsButton = settingsButton.transform.localPosition.y;
		}
		collapsed = true;
		downloadedBGren = downloadedBG.GetComponentInChildren<MeshRenderer>();
		SetOpenRens(false);
	}

	protected void SetOpenRens(bool enabled)
	{
		if (enabled)
		{
			if (base.transform.localPosition != new Vector3(base.transform.localPosition.x, 2.1f, base.transform.localPosition.z))
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, 2.1f, base.transform.localPosition.z);
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, 0f, base.transform.localPosition.z);
		}
		MeshRenderer[] array = renderersToFade;
		foreach (MeshRenderer meshRenderer in array)
		{
			Color color = meshRenderer.material.GetColor("_TintColor");
			meshRenderer.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, (!enabled) ? 0f : 0.15685f));
		}
		MeshRenderer[] array2 = blurs;
		foreach (MeshRenderer meshRenderer2 in array2)
		{
			meshRenderer2.gameObject.SetActive(enabled);
		}
		GameObject[] array3 = toggledObjects;
		foreach (GameObject gameObject in array3)
		{
			gameObject.SetActive(enabled);
		}
		downloadedText.color = new Color(downloadedText.color.r, downloadedText.color.g, downloadedText.color.b, (!enabled || iconColumns <= 0) ? 0f : 1f);
		prevPageButton.gameObject.SetActive(enabled && numOfPages > 1);
		nextPageButton.gameObject.SetActive(enabled && numOfPages > 1);
	}

	public void Open()
	{
		if (!ReferenceMaster.DisableAllTools() && collapsed && base.gameObject.activeInHierarchy)
		{
			StartCoroutine(IEOpen());
		}
	}

	public IEnumerator IEOpen()
	{
		GotoPage(currentPage);
		collapsed = false;
		if ((bool)selectedTooltip)
		{
			selectedTooltip.SendMessage("OnMouseExit", null, SendMessageOptions.DontRequireReceiver);
		}
		SetOpenRens(true);
		yield break;
	}

	public void Close()
	{
		ReferenceMaster.DisableAllTools();
		_Close();
	}

	protected void _Close()
	{
		if (!collapsed && base.gameObject.activeInHierarchy)
		{
			StartCoroutine(IEClose());
		}
	}

	public IEnumerator IEClose()
	{
		collapsed = true;
		if ((bool)selectedTooltip && mouseOverArea[mouseOverArea.Length - 1].isMouseOver)
		{
			selectedTooltip.SendMessage("OnMouseEnter", null, SendMessageOptions.DontRequireReceiver);
		}
		SetOpenRens(false);
		yield break;
	}

	public void RefreshSkins()
	{
		BlockSkinLoader.LoadNewSkins();
	}

	private void Hide()
	{
		Transform parent = base.transform.parent;
		Vector3 position = parent.position;
		parent.position = new Vector3(-1000f, position.y, position.z);
	}

	protected void UpdateUIFromLevelEditor(bool visible)
	{
		if (!visible)
		{
			Hide();
			if ((bool)selectedTrigger)
			{
				selectedTrigger.enabled = true;
			}
		}
		else
		{
			SetUIBasedOnID(StatMaster.SelectedBlockId, true);
		}
	}

	public void SetUIBasedOnID(BlockType id)
	{
		SetUIBasedOnID(id, true);
	}

	private bool IsPlatformInitialized()
	{
		return SteamManager.Initialized;
	}

	public virtual void SetUIBasedOnID(BlockType blockType, bool close)
	{
		if (blockType == BlockType.BuildNode)
		{
			blockType = BlockType.BuildSurface;
		}
		int num = (int)blockType;
		if (num == 0 && OptionsMaster.skinsEnabled)
		{
			return;
		}
		Transform parent = base.transform.parent;
		Vector3 position = parent.position;
		if (ID != num)
		{
			currentPage = 0;
		}
		ID = num;
		if (bottomRightPageNum)
		{
			Transform transform = settingsButton.transform;
			Vector3 localPosition = transform.localPosition;
			if (openWorkshopButton != null)
			{
				if (SingleInstance<StatMaster>.Instance.LowViolence || !IsPlatformInitialized())
				{
					transform.localPosition = new Vector3(localPosition.x, openWorkshopButton.transform.localPosition.y, localPosition.z);
					openWorkshopButton.gameObject.SetActive(false);
				}
				else
				{
					transform.localPosition = new Vector3(localPosition.x, ySettingsButton, localPosition.z);
					openWorkshopButton.gameObject.SetActive(true);
				}
			}
			else
			{
				Debug.LogError("openWorkshop reference isn't assigned on PrefabVisualUI!", base.gameObject);
			}
		}
		if ((close || ReferenceMaster.activeMachineSimulating) && !collapsed)
		{
			Close();
		}
		if ((bool)selectedTrigger)
		{
			selectedTrigger.enabled = true;
		}
		BlockPrefab blockPrefab = PrefabMaster.BlockPrefabs[ID];
		if (OptionsMaster.skinsEnabled && blockPrefab.CanGetNewVisuals)
		{
			List<BlockSkinLoader.SkinPack.Skin> list = PrefabMaster.BlockPrefabs[ID].VisualController.CustomOptions();
			for (int i = 0; i < blockPrefab.ButtonIconCount(); i++)
			{
				BlockButtonControl buttonIcon = blockPrefab.GetButtonIcon(i);
				if (buttonIcon == null)
				{
					Hide();
				}
				else
				{
					if (!buttonIcon.blockMenuControllerCode.gameObject.activeSelf)
					{
						continue;
					}
					parent.position = new Vector3(buttonIcon.transform.position.x, position.y, position.z);
					iconColumns = Mathf.CeilToInt((float)list.Count / (1f * (float)ROWS));
					numOfPages = Mathf.CeilToInt((float)iconColumns / (1f * (float)MAX_COLUMNS));
					Color color = pageNumText.color;
					if (numOfPages > 1)
					{
						pageNumText.color = new Color(color.r, color.g, color.b, 1f);
						pageNumText.SetText("1/" + numOfPages);
						if (!collapsed)
						{
							prevPageButton.gameObject.SetActive(true);
							nextPageButton.gameObject.SetActive(true);
						}
					}
					else
					{
						pageNumText.color = new Color(color.r, color.g, color.b, 0f);
						prevPageButton.gameObject.SetActive(false);
						nextPageButton.gameObject.SetActive(false);
					}
					GotoPage(currentPage, close);
					selectedTooltip = buttonIcon.gameObject;
					Collider[] components = buttonIcon.GetComponents<Collider>();
					for (int j = 0; j < components.Length; j++)
					{
						if (components[j].enabled && components[j].isTrigger)
						{
							selectedTrigger = components[j];
							selectedTrigger.enabled = false;
							break;
						}
					}
					return;
				}
			}
		}
		Hide();
	}

	public void NextPage()
	{
		int num = currentPage + 1;
		if (num >= numOfPages)
		{
			num = 0;
		}
		if (num < 0)
		{
			num = numOfPages - 1;
		}
		GotoPage(num);
	}

	public void PrevPage()
	{
		int num = currentPage - 1;
		if (num >= numOfPages)
		{
			num = 0;
		}
		if (num < 0)
		{
			num = numOfPages - 1;
		}
		GotoPage(num);
	}

	public void GotoPage(int page, bool settingUp = false)
	{
		if (page > numOfPages || page < 0)
		{
			return;
		}
		List<BlockSkinLoader.SkinPack.Skin> list = PrefabMaster.BlockPrefabs[ID].VisualController.CustomOptions();
		int count = list.Count;
		if (ID != lastID || lastDownloadCount != count)
		{
			settingUp = true;
			lastID = ID;
			lastDownloadCount = count;
		}
		currentPage = page;
		if (!settingUp)
		{
			pageNumText.SetText(page + 1 + "/" + numOfPages);
		}
		else
		{
			float num = Mathf.Clamp(iconColumns, 0, MAX_COLUMNS);
			Transform parent = base.transform.parent;
			Vector3 position = parent.position;
			float num2 = 7.3f - (num - 1f) * 0.7f;
			bool flag = position.x >= num2;
			reverse = ((!flag) ? 1f : (-1f));
			downloadedBG.localScale = new Vector3(num, downloadedBG.localScale.y, downloadedBG.localScale.z);
			for (int i = 0; i < downloadedBG.childCount; i++)
			{
				Transform child = downloadedBG.GetChild(i);
				child.localPosition = new Vector3(reverse * 0.35f, child.localPosition.y, child.localPosition.z);
			}
			float num3;
			if (flag)
			{
				downloadedBG.localPosition = new Vector3(-0.375f, downloadedBG.localPosition.y, downloadedBG.localPosition.z);
				num3 = downloadedBGren.bounds.min.x - 0.6f;
				if (bottomRightPageNum)
				{
					pageNumText.transform.position = new Vector3(-0.445f, pageNumText.transform.position.y, pageNumText.transform.position.z);
				}
			}
			else
			{
				downloadedBG.localPosition = new Vector3(0.375f, downloadedBG.localPosition.y, downloadedBG.localPosition.z);
				num3 = downloadedBGren.bounds.max.x;
				if (bottomRightPageNum)
				{
					pageNumText.transform.position = new Vector3(num3 - 0.07f, pageNumText.transform.position.y, pageNumText.transform.position.z);
				}
			}
			optionsContainer.position = new Vector3(num3, optionsContainer.position.y, optionsContainer.position.z);
			float x = downloadedBG.GetChild(0).position.x;
			downloadedText.transform.parent.position = new Vector3(x, downloadedText.transform.parent.position.y, downloadedText.transform.parent.position.z);
			if (!bottomRightPageNum)
			{
				if (flag)
				{
					num3 = downloadedBGren.bounds.min.x;
					settingsButton.transform.position = new Vector3(num3 + 0.11f, settingsButton.transform.position.y, settingsButton.transform.position.z);
				}
				else
				{
					num3 = downloadedBGren.bounds.max.x;
					settingsButton.transform.position = new Vector3(num3 - 0.11f, settingsButton.transform.position.y, settingsButton.transform.position.z);
				}
			}
		}
		SetOfficial();
		for (int j = 0; j < downloadedIcons.Count; j++)
		{
			if (j < iconColumns * ROWS && j < MAX_COLUMNS * ROWS)
			{
				if (settingUp)
				{
					downloadedIcons[j].transform.localPosition = new Vector3(Mathf.Abs(downloadedIcons[j].transform.localPosition.x) * reverse, downloadedIcons[j].transform.localPosition.y, downloadedIcons[j].transform.localPosition.z);
					continue;
				}
				int num4 = page * ROWS * MAX_COLUMNS + j;
				if (num4 < count)
				{
					downloadedIcons[j].Setup(ID, list[num4]);
				}
				else
				{
					downloadedIcons[j].Setup(ID, null);
				}
			}
			else if (settingUp)
			{
				downloadedIcons[j].Disable(ID);
			}
		}
	}

	public void SetRecent(BlockSkinLoader.SkinPack.Skin skin)
	{
		if (!mostRecentSkins.ContainsKey(ID))
		{
			mostRecentSkins.Add(ID, new List<BlockSkinLoader.SkinPack.Skin>());
		}
		List<BlockSkinLoader.SkinPack.Skin> list = mostRecentSkins[ID];
		if (list.Contains(skin))
		{
			list.Remove(skin);
		}
		int num = 3;
		if (!skin.isDefault)
		{
			list.Add(skin);
			if (list.Count > num)
			{
				list.RemoveAt(0);
			}
		}
		else
		{
			BlockSkinLoader.SkinPack.Skin skin2 = null;
			if (list.Count >= num)
			{
				skin2 = list[0];
				list.RemoveAt(0);
				list.Add(skin2);
			}
		}
		SetOfficial();
	}

	private void SetOfficial()
	{
		int count = PrefabMaster.BlockPrefabs[ID].officialSkins.Count;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < officialIcons.Count; i++)
		{
			if (num < count)
			{
				BlockSkinLoader.SkinPack.Skin skin = PrefabMaster.BlockPrefabs[ID].officialSkins[num];
				if (skin.pack.id != "3dprint")
				{
					officialIcons[i].Setup(ID, skin);
					num2++;
				}
				else
				{
					i--;
				}
				num++;
				continue;
			}
			int num3 = i - num2;
			if (mostRecentSkins.ContainsKey(ID) && StatMaster.advancedBuilding)
			{
				List<BlockSkinLoader.SkinPack.Skin> list = mostRecentSkins[ID];
				if (list == null || num3 >= list.Count)
				{
					officialIcons[i].Setup(ID, null);
				}
				else
				{
					officialIcons[i].Setup(ID, mostRecentSkins[ID][num3]);
				}
			}
			else
			{
				officialIcons[i].Setup(ID, null);
			}
		}
	}

	public void ResetUI()
	{
		ReferenceMaster.Instance.StartCoroutine(IEResetUI());
	}

	public IEnumerator IEResetUI()
	{
		yield return null;
		SingleInstanceFindOnly<PrefabVisualUI>.Instance.SetUIBasedOnID((BlockType)SingleInstanceFindOnly<PrefabVisualUI>.Instance.ID, true);
	}

	public void UpdateDisplay(BlockSkinLoader.SModifier m)
	{
		BlockSkinLoader.SkinPack.Skin skin = null;
		if (m != null)
		{
			if (!(m is BlockSkinLoader.SkinPack.Skin))
			{
				SingleInstanceFindOnly<PrefabVisualUI>.Instance.SetUIBasedOnID((BlockType)SingleInstanceFindOnly<PrefabVisualUI>.Instance.ID, false);
				return;
			}
			skin = m as BlockSkinLoader.SkinPack.Skin;
		}
		if (ID == skin.prefab.ID)
		{
			SingleInstanceFindOnly<PrefabVisualUI>.Instance.SetUIBasedOnID((BlockType)skin.prefab.ID, false);
			return;
		}
		BlockVisualController visualController = PrefabMaster.BlockPrefabs[ID].VisualController;
		if (!(visualController is BlockVisualControllerExtended))
		{
			return;
		}
		BlockVisualControllerExtended blockVisualControllerExtended = visualController as BlockVisualControllerExtended;
		for (int i = 0; i < blockVisualControllerExtended.otherBlocksSkinsAllowed.Length; i++)
		{
			if (blockVisualControllerExtended.otherBlocksSkinsAllowed[i] == skin.prefab.ID)
			{
				SingleInstanceFindOnly<PrefabVisualUI>.Instance.SetUIBasedOnID((BlockType)skin.prefab.ID, false);
				break;
			}
		}
	}

	private void SkinUIEvent(Action<BlockTooltipHolder> action)
	{
		BlockTooltipHolder tooltip = SingleInstance<BlockTooltipController>.Instance.GetTooltip((BlockType)ID);
		if (collapsed && (bool)tooltip)
		{
			action(tooltip);
		}
	}

	protected virtual void OpenWorkshopSkinsOverlay()
	{
		if (SteamManager.Initialized)
		{
			SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/workshop/browse/?appid=346010&requiredtags[]=Skin+Packs");
		}
	}

	protected virtual void OpenSkinSettings()
	{
		if (!collapsed)
		{
			Close();
		}
	}

	public virtual void TooltipMouseEnter()
	{
		if (collapsed)
		{
			BlockTooltipHolder tooltip = SingleInstance<BlockTooltipController>.Instance.GetTooltip((BlockType)ID);
			if ((bool)tooltip)
			{
				SingleInstance<BlockTooltipController>.Instance.UpdatePosition(tooltip, openCloseButton.transform, Vector3.zero.WithY(0.25f));
			}
		}
	}

	public virtual void TooltipMouseOver()
	{
		if (collapsed)
		{
			BlockTooltipHolder tooltip = SingleInstance<BlockTooltipController>.Instance.GetTooltip((BlockType)ID);
			if ((bool)tooltip)
			{
				tooltip.tooltipCode.OnCursorOver();
			}
		}
	}

	public virtual void TooltipMouseExit()
	{
		if (collapsed)
		{
			BlockTooltipHolder tooltip = SingleInstance<BlockTooltipController>.Instance.GetTooltip((BlockType)ID);
			if ((bool)tooltip)
			{
				tooltip.tooltipCode.OnMouseExit();
			}
		}
	}
}
