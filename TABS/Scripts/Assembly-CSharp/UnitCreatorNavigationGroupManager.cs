using Landfall.TABS.Workshop;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitCreatorNavigationGroupManager : UINavigationGroupManager
{
	[Space]
	[SerializeField]
	[Tooltip("Team buttons. A navigation group should also be attached to this.")]
	protected UnitEditorTeamButtons teamButtons;

	[SerializeField]
	protected UINavigationGroup mainMenuGroup;

	[SerializeField]
	protected UINavigationGroup saveLoadButtonGroup;

	[SerializeField]
	protected UINavigationGroup testButtonGroup;

	[SerializeField]
	[Tooltip("Item grid animation. A navigation group should also be attached to this.")]
	protected CodeAnimation itemGridAnimation;

	[SerializeField]
	[Tooltip("Equipped items animation. A navigation group should also be attached to this.")]
	protected CodeAnimation equippedItemsAnimation;

	[SerializeField]
	[Tooltip("Context menu animation. A navigation group should also be attached to this.")]
	protected CodeAnimation contextMenuAnimation;

	[SerializeField]
	[Tooltip("Color picker. A navigation group should also be attached to this.")]
	protected UIColorPicker colorPicker;

	[SerializeField]
	[Tooltip("Save unit UI. A navigation group should also be attached to this.")]
	private UnitCreatorSaveUnitUI saveUnit;

	[SerializeField]
	[Tooltip("Load unit UI. A navigation group should also be attached to this.")]
	private UnitCreatorLoadUnitUI loadUnit;

	private UINavigationGroup teamButtonsGroup;

	private UINavigationGroup itemGridGroup;

	private UINavigationGroup equippedItemsGroup;

	private UINavigationGroup contextMenuGroup;

	private UINavigationGroup colorPickerGroup;

	private UINavigationGroup saveUnitGroup;

	private UINavigationGroup loadUnitGroup;

	private bool colorPickerDidOpen;

	private GameObject preColorPickerObject;

	protected override void Awake()
	{
		base.Awake();
		teamButtonsGroup = teamButtons.gameObject.GetComponent<UINavigationGroup>();
		itemGridGroup = itemGridAnimation.gameObject.GetComponent<UINavigationGroup>();
		equippedItemsGroup = equippedItemsAnimation.gameObject.GetComponent<UINavigationGroup>();
		contextMenuGroup = contextMenuAnimation.gameObject.GetComponent<UINavigationGroup>();
		colorPickerGroup = colorPicker.gameObject.GetComponent<UINavigationGroup>();
		saveUnitGroup = saveUnit.gameObject.GetComponent<UINavigationGroup>();
		loadUnitGroup = loadUnit.gameObject.GetComponent<UINavigationGroup>();
	}

	private void OnEnable()
	{
		if (itemGridAnimation != null)
		{
			itemGridAnimation.InPlayed += OnItemGridOpened;
			itemGridAnimation.OutPlayed += OnItemGridClosed;
		}
		if (equippedItemsAnimation != null)
		{
			equippedItemsAnimation.InPlayed += OnEquippedItemsOpened;
			equippedItemsAnimation.OutPlayed += OnEquippedItemsClosed;
		}
		if (contextMenuAnimation != null)
		{
			contextMenuAnimation.InPlayed += OnContextMenuOpened;
			contextMenuAnimation.OutPlayed += OnContextMenuClosed;
		}
		if (colorPicker != null)
		{
			colorPicker.Opened += OnColorPickerOpened;
			colorPicker.Closed += OnColorPickerClosed;
		}
		if (saveUnit != null)
		{
			saveUnit.Opened += OnSaveUnitOpened;
			saveUnit.Closed += OnSaveUnitClosed;
		}
		if (loadUnit != null)
		{
			loadUnit.Opened += OnLoadUnitOpened;
			loadUnit.Closed += OnLoadUnitClosed;
		}
	}

	private void OnDisable()
	{
		if (itemGridAnimation != null)
		{
			itemGridAnimation.InPlayed -= OnItemGridOpened;
			itemGridAnimation.OutPlayed -= OnItemGridClosed;
		}
		if (equippedItemsAnimation != null)
		{
			equippedItemsAnimation.InPlayed -= OnEquippedItemsOpened;
			equippedItemsAnimation.OutPlayed -= OnEquippedItemsClosed;
		}
		if (contextMenuAnimation != null)
		{
			contextMenuAnimation.InPlayed -= OnContextMenuOpened;
			contextMenuAnimation.OutPlayed -= OnContextMenuClosed;
		}
		if (colorPicker != null)
		{
			colorPicker.Opened -= OnColorPickerOpened;
			colorPicker.Closed -= OnColorPickerClosed;
		}
		if (saveUnit != null)
		{
			saveUnit.Opened -= OnSaveUnitOpened;
			saveUnit.Closed -= OnSaveUnitClosed;
		}
		if (loadUnit != null)
		{
			loadUnit.Opened -= OnLoadUnitOpened;
			loadUnit.Closed -= OnLoadUnitClosed;
		}
	}

	private void EnableVisibleGroups()
	{
		EnableGroup(mainMenuGroup);
		EnableGroup(teamButtonsGroup);
		EnableGroup(testButtonGroup);
		EnableGroup(saveLoadButtonGroup);
		if (itemGridAnimation.IsVisible)
		{
			EnableGroup(itemGridGroup);
		}
		if (equippedItemsAnimation.IsVisible)
		{
			EnableGroup(equippedItemsGroup);
		}
		if (contextMenuAnimation.IsVisible)
		{
			EnableGroup(contextMenuGroup);
		}
		if (saveUnit.IsOpen())
		{
			EnableGroup(saveUnitGroup);
		}
		if (loadUnit.IsOpen())
		{
			EnableGroup(loadUnitGroup);
		}
	}

	private void OnItemGridOpened()
	{
		EnableGroup(itemGridGroup);
	}

	private void OnItemGridClosed()
	{
		DisableGroup(itemGridGroup);
	}

	private void OnEquippedItemsOpened()
	{
		EnableGroup(equippedItemsGroup);
	}

	private void OnEquippedItemsClosed()
	{
		DisableGroup(equippedItemsGroup);
	}

	private void OnContextMenuOpened()
	{
		EnableGroup(contextMenuGroup);
	}

	private void OnContextMenuClosed()
	{
		DisableGroup(contextMenuGroup);
	}

	private void OnColorPickerOpened(UIColorButton defaultButton)
	{
		if (!colorPickerDidOpen)
		{
			colorPickerDidOpen = true;
			DisableAllGroups();
			EventSystem current = EventSystem.current;
			preColorPickerObject = ((current != null) ? current.currentSelectedGameObject : null);
			EnableGroup(colorPickerGroup);
			if (current != null && defaultButton != null)
			{
				current.SetSelectedGameObject(defaultButton.gameObject);
			}
		}
	}

	private void OnColorPickerClosed()
	{
		if (colorPickerDidOpen)
		{
			colorPickerDidOpen = false;
			DisableGroup(colorPickerGroup);
			EnableVisibleGroups();
			EventSystem current = EventSystem.current;
			if (current != null && preColorPickerObject != null)
			{
				current.SetSelectedGameObject(preColorPickerObject);
			}
			preColorPickerObject = null;
		}
	}

	private void OnSaveUnitOpened()
	{
		DisableAllGroups();
		EnableGroup(saveUnitGroup);
	}

	private void OnSaveUnitClosed()
	{
		DisableGroup(saveUnitGroup);
		EnableVisibleGroups();
	}

	private void OnLoadUnitOpened()
	{
		DisableAllGroups();
		EnableGroup(loadUnitGroup);
	}

	private void OnLoadUnitClosed()
	{
		DisableGroup(loadUnitGroup);
		EnableVisibleGroups();
	}
}
