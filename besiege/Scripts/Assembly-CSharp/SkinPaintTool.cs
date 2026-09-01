using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Localisation;
using Selectors;
using UnityEngine;

public class SkinPaintTool : MonoBehaviour
{
	public enum SortType
	{
		DownloadDate = 0,
		LastUsedDate = 1,
		Name = 2
	}

	public static BlockSkinLoader.SkinPack.Skin Skin;

	public static Func<string, BlockSkinLoader.SkinPack, bool> searchFunc = (string SearchField, BlockSkinLoader.SkinPack pack) => CultureInfo.InvariantCulture.CompareInfo.IndexOf(pack.name, SearchField, CompareOptions.IgnoreCase) >= 0;

	public List<BlockSkinLoader.SkinPack> skinPacks = new List<BlockSkinLoader.SkinPack>();

	public PaintButton button;

	public SelectSkinButton icon;

	public UIButton openButton;

	public UIButton prevPageButton;

	public UIButton nextPageButton;

	public TextHolderAutocomplete searchField;

	public UIButton machineSkinsButton;

	public GameObject machineSkinsButtonBg;

	public UIButton sortButton;

	public LocalisationChild sortTooltipSubtitle;

	public GameObject sortIconName;

	public GameObject sortIconDate;

	public GameObject sortIconLastUsed;

	public Transform BG;

	protected MeshRenderer BGren;

	public DynamicText pageNumText;

	[NonSerialized]
	public List<SelectSkinPaintButton> icons;

	[NonSerialized]
	public UIHoverArea[] mouseOverArea;

	public bool collapsed = true;

	public static bool PaintingSelection = false;

	[NonSerialized]
	public SortType CurrentType = SortType.Name;

	private string lastSearch = string.Empty;

	private Coroutine searchCoroutine;

	private bool machineSkinsOnly;

	private List<BlockSkinLoader.SkinPack> machineSkins;

	private List<BlockSkinLoader.SkinPack> recentSkins;

	protected int currentPage;

	private bool awoken;

	private bool delegated;

	private bool externalOpen;

	private bool wasDone;

	private bool turnOffButton;

	protected int numOfPages
	{
		get
		{
			return Mathf.CeilToInt((skinPacks.Count - 1) / icons.Count) + 1;
		}
	}

	protected virtual void Awake()
	{
		if (!awoken)
		{
			Skin = BlockSkinLoader.defaultPack.FindAvailableSkin();
			icons = GetComponentsInChildren<SelectSkinPaintButton>().ToList();
			List<UIHoverArea> list = GetComponentsInChildren<UIHoverArea>(true).ToList();
			list.Add(openButton.GetComponent<UIHoverArea>());
			mouseOverArea = list.ToArray();
			awoken = true;
			collapsed = true;
			openButton.Click += Open;
			prevPageButton.Click += PrevPage;
			nextPageButton.Click += NextPage;
			machineSkinsButton.Click += ToggleMachineSkins;
			searchField.TextChanged += RunSearch;
			searchField.GetItems = null;
			sortButton.Click += delegate
			{
				Sort(true);
			};
			if (button == null)
			{
				button = base.gameObject.GetComponentInParent<PaintButton>();
			}
		}
	}

	private void OnEnable()
	{
		if (button.SelectionPaintMode())
		{
			externalOpen = true;
			Open();
		}
		if (!delegated)
		{
			delegated = true;
			BlockSkinLoader.SkinModified += UpdateDisplay;
			BlockSkinLoader.SkinPacksAdded += delegate
			{
				RunSearch(lastSearch);
			};
			StatMaster.LevelEditingToggled += UpdateUIFromLevelEditor;
			ReferenceMaster.OnConnect += ResetUI;
		}
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		Close();
		PaintingSelection = false;
		if (delegated)
		{
			delegated = false;
			BlockSkinLoader.SkinModified -= UpdateDisplay;
			BlockSkinLoader.SkinPacksAdded -= delegate
			{
				RunSearch(lastSearch);
			};
			StatMaster.LevelEditingToggled -= UpdateUIFromLevelEditor;
			ReferenceMaster.OnConnect -= ResetUI;
		}
	}

	private void OnDestroy()
	{
		OnDisable();
	}

	private void SetPosition(Vector3 pos)
	{
		if (base.transform.localPosition != pos)
		{
			base.transform.localPosition = pos;
		}
	}

	private void LateUpdate()
	{
		if (turnOffButton)
		{
			Close();
			if (button.SelectionPaintMode())
			{
				button.OffExternal();
			}
			turnOffButton = false;
		}
		else if (!collapsed && !externalOpen)
		{
			if ((!MouseOver() && (InputManager.LeftMouseButton() || InputManager.RotateCameraKey() || InputManager.FocusCameraKey())) || ReferenceMaster.activeMachineSimulating)
			{
				Close();
				if (button.SelectionPaintMode())
				{
					button.OffExternal();
				}
			}
		}
		else
		{
			externalOpen = false;
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
			return;
		}
		if (StatMaster.SelectedLevelPrefab != null || StatMaster.Mode.LevelEditor.selectedTool != StatMaster.Tool.None)
		{
			ReferenceMaster.ResetLevelEditor();
		}
		Close();
	}

	public IEnumerator Start()
	{
		if (!button.SelectionPaintMode())
		{
			SetPosition(new Vector3(base.transform.localPosition.x, 69f, base.transform.localPosition.z));
			BGren = BG.GetComponentInChildren<MeshRenderer>();
		}
		yield return null;
		SetIconDisplay(Skin, true);
		RunSearch(lastSearch);
	}

	public void Open()
	{
		if (collapsed)
		{
			if (button.SelectionPaintMode())
			{
				PaintingSelection = true;
			}
			SetPosition(new Vector3(base.transform.localPosition.x, 0f, base.transform.localPosition.z));
			int page = currentPage;
			if (machineSkinsOnly)
			{
				machineSkins = GetMachineSkinPacks();
				if (string.IsNullOrEmpty(lastSearch))
				{
					skinPacks.Clear();
					skinPacks.AddRange(machineSkins);
				}
			}
			GotoPage(page);
			collapsed = false;
		}
		else if (!externalOpen && button.SelectionPaintMode())
		{
			turnOffButton = true;
			BlockSkinLoader.SetSelectionToPack(Skin.pack, Machine.Active());
		}
	}

	public void Close()
	{
		if (!collapsed)
		{
			collapsed = true;
			SetPosition(new Vector3(base.transform.localPosition.x, 69f, base.transform.localPosition.z));
			PaintingSelection = false;
		}
	}

	private void Hide()
	{
		base.transform.parent.localPosition = new Vector3(-1000f, base.transform.parent.localPosition.y, base.transform.parent.localPosition.z);
	}

	protected void UpdateUIFromLevelEditor(bool visible)
	{
		if (!visible)
		{
			Hide();
		}
		else
		{
			SetUI(true);
		}
	}

	public virtual void SetUI(bool close)
	{
		if ((close || ReferenceMaster.activeMachineSimulating) && !collapsed)
		{
			Close();
		}
		if (StatMaster.advancedBuilding && OptionsMaster.skinsEnabled)
		{
			base.transform.parent.localPosition = new Vector3(0f, base.transform.parent.localPosition.y, base.transform.parent.localPosition.z);
			GotoPage(currentPage, close);
		}
		else
		{
			Hide();
		}
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

	public void GotoPage(int page, bool close = false)
	{
		if (page >= numOfPages || page < 0)
		{
			return;
		}
		currentPage = page;
		if (!close)
		{
			pageNumText.SetText(page + 1 + "/" + numOfPages);
		}
		for (int i = 0; i < icons.Count; i++)
		{
			int num = page * icons.Count + i;
			if (num < skinPacks.Count)
			{
				BlockSkinLoader.SkinPack.Skin skin = skinPacks[num].FindAvailableSkin();
				int iD = skin.prefab.ID;
				icons[i].Setup(iD, skin, this);
			}
			else
			{
				icons[i].Setup(1, null, this);
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
		SetUI(true);
	}

	public void SetIconDisplay(BlockSkinLoader.SkinPack.Skin skin, bool ignoreTurningOff = false)
	{
		if (!ignoreTurningOff && button.SelectionPaintMode())
		{
			turnOffButton = true;
			BlockSkinLoader.SetSelectionToPack(skin.pack, Machine.Active());
		}
		if (Skin != skin || skin.doneLoading != wasDone)
		{
			Skin = skin;
			wasDone = skin.doneLoading;
			icon.Setup(skin.prefab.ID, skin);
		}
	}

	public void UpdateDisplay(BlockSkinLoader.SModifier m)
	{
		BlockSkinLoader.SkinPack.Skin skin = null;
		if (m == null || !(m is BlockSkinLoader.SkinPack.Skin))
		{
			return;
		}
		skin = m as BlockSkinLoader.SkinPack.Skin;
		if (skin == Skin)
		{
			SetIconDisplay(skin);
		}
		if (skinPacks.IndexOf(skin.pack) >= currentPage * icons.Count && skinPacks.IndexOf(skin.pack) < (currentPage + 1) * icons.Count)
		{
			SetUI(false);
			if (StatMaster.advancedBuilding && OptionsMaster.skinsEnabled)
			{
				GotoPage(currentPage);
			}
		}
	}

	public void RunSearch(string search)
	{
		if (searchCoroutine != null)
		{
			StopCoroutine(searchCoroutine);
		}
		searchCoroutine = StartCoroutine(IERunSearch(search));
	}

	public IEnumerator IERunSearch(string search)
	{
		yield return new WaitForSecondsRealtime(0.3f);
		search = search.Trim();
		skinPacks.Clear();
		List<BlockSkinLoader.SkinPack> availableSkinPacks = ((!machineSkinsOnly) ? BlockSkinLoader.SkinPacks : machineSkins);
		if (string.IsNullOrEmpty(search))
		{
			skinPacks.AddRange(availableSkinPacks);
			GotoPage(currentPage);
		}
		else
		{
			for (int i = 0; i < availableSkinPacks.Count; i++)
			{
				BlockSkinLoader.SkinPack s = availableSkinPacks[i];
				if (searchFunc(search, s))
				{
					skinPacks.Add(s);
				}
			}
		}
		skinPacks.RemoveAll(IsInvalidSkin);
		Sort(false, true);
		lastSearch = search;
		searchCoroutine = null;
	}

	private void ToggleMachineSkins()
	{
		machineSkinsOnly = !machineSkinsOnly;
		machineSkinsButtonBg.SetActive(machineSkinsOnly);
		if (machineSkinsOnly)
		{
			machineSkins = GetMachineSkinPacks();
		}
		searchField.SetText(string.Empty);
		RunSearch(string.Empty);
	}

	private List<BlockSkinLoader.SkinPack> GetMachineSkinPacks()
	{
		HashSet<BlockSkinLoader.SkinPack> hashSet = new HashSet<BlockSkinLoader.SkinPack>();
		List<BlockBehaviour> buildingBlocks = Machine.Active().BuildingBlocks;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			if ((bool)buildingBlocks[i].VisualController)
			{
				BlockSkinLoader.SkinPack.Skin selectedSkin = buildingBlocks[i].VisualController.selectedSkin;
				if (selectedSkin != null)
				{
					hashSet.Add(selectedSkin.pack);
				}
			}
		}
		return hashSet.ToList();
	}

	private bool IsInvalidSkin(BlockSkinLoader.SkinPack s)
	{
		return s.id == "3dprint" || s.FindAvailableSkin() == null;
	}

	public void Sort(bool next, bool fromSearch = false)
	{
		if (next)
		{
			CurrentType = ((CurrentType != SortType.Name) ? (CurrentType + 1) : SortType.DownloadDate);
		}
		switch (CurrentType)
		{
		case SortType.DownloadDate:
			if (!fromSearch)
			{
				RunSearch(lastSearch);
			}
			sortTooltipSubtitle.translationID = 5099;
			break;
		case SortType.LastUsedDate:
			skinPacks = skinPacks.OrderByDescending((BlockSkinLoader.SkinPack x) => OptionsMaster.BesiegeConfig.SkinsLastUsedTimes.GetValueOrDefault(x.id)).ToList();
			sortTooltipSubtitle.translationID = 5100;
			break;
		case SortType.Name:
			skinPacks.Sort((BlockSkinLoader.SkinPack x, BlockSkinLoader.SkinPack y) => x.name.CompareTo(y.name));
			sortTooltipSubtitle.translationID = 5097;
			break;
		}
		sortTooltipSubtitle.Recaption();
		if (StatMaster.advancedBuilding && OptionsMaster.skinsEnabled)
		{
			GotoPage((!next && !fromSearch) ? currentPage : 0);
		}
		sortIconName.SetActive(CurrentType == SortType.Name);
		sortIconDate.SetActive(CurrentType == SortType.DownloadDate);
		sortIconLastUsed.SetActive(CurrentType == SortType.LastUsedDate);
	}
}
