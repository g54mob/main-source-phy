using System;
using System.Collections.Generic;
using GamepadUI.StateManager.Core;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorEquipedClothing : TabbedUIComponent
	{
		[SerializeField]
		private GameObject TABSParent;

		[SerializeField]
		private GameObject TABSContentParent;

		[SerializeField]
		private UnitEditorColorWheel colorWheel;

		[SerializeField]
		private GameObject ColorCell;

		[SerializeField]
		private Image ClothingImage;

		[SerializeField]
		private LocalizeText ItemName;

		[SerializeField]
		private GameObject StatsCell;

		[SerializeField]
		private Transform StatsParent;

		[SerializeField]
		private GameObject ProjectileStat;

		[SerializeField]
		private ToggleGroup tabs;

		[SerializeField]
		private UnitEditorProjectileSlot projectileSlot;

		private const string Back = "BUTTON_BACK";

		private const string Remove = "BUTTON_REMOVE";

		private UnitEditorManager.EquipedWrapper wrapper;

		private List<GameObject> CellsToRemove = new List<GameObject>();

		private UnitEditorEquipedColorCell[] colorCells;

		private InputService inputService;

		private bool usingController;

		public override void OpenSubMenu(UISubMenu menu)
		{
			if (menu is UnitEditorSubMenu unitEditorSubMenu)
			{
				unitEditorSubMenu.SetUpdateGlyphs(shouldUpdate: false);
			}
			base.OpenSubMenu(menu);
		}

		protected override void Awake()
		{
			base.Awake();
			inputService = ServiceLocator.GetService<InputService>();
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			if (inputService != null)
			{
				inputService.InputChanged += OnInputChanged;
				usingController = inputService.CurrentInputType == InputType.Controller;
			}
			if (colorWheel != null)
			{
				colorWheel.ColorWheelStateChanged += OnColorWheelModeChanged;
				colorWheel.LastFrameExit = false;
			}
			if (stateManager is UnitEditorUIManager unitEditorUIManager)
			{
				UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
				if (!(gamepadGlyphs == null))
				{
					gamepadGlyphs.UpdateActionNames("Back", "BUTTON_BACK", UnitEditorGamepadGlyphs.Position.Left);
					gamepadGlyphs.UpdateActionNames("Remove Equipped", "BUTTON_REMOVE", UnitEditorGamepadGlyphs.Position.Middle);
					gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Right);
				}
			}
		}

		protected override void OnClose()
		{
			base.OnClose();
			if (inputService != null)
			{
				inputService.InputChanged -= OnInputChanged;
			}
			if (colorWheel != null)
			{
				colorWheel.LastFrameExit = true;
				colorWheel.OnColorPreviewExit();
				colorWheel.ColorWheelStateChanged -= OnColorWheelModeChanged;
			}
		}

		protected override bool IsTabValid(int index)
		{
			if (base.IsTabValid(index))
			{
				return wrapper.GetWrapperType() == UnitEditorManager.EquipedWrapper.WrapperType.Weapon;
			}
			return false;
		}

		private void OnInputChanged(InputType inputType)
		{
			switch (inputType)
			{
			case InputType.Controller:
				usingController = true;
				break;
			case InputType.Keyboard:
			case InputType.Any:
				usingController = false;
				break;
			default:
				throw new ArgumentOutOfRangeException("inputType", inputType, null);
			}
		}

		private void OnColorWheelModeChanged(ColorWheelMode mode)
		{
			switch (mode)
			{
			case ColorWheelMode.EquipmentColors:
			case ColorWheelMode.ColorParentCategories:
			case ColorWheelMode.ColorParentCategory:
			case ColorWheelMode.ColorCategory:
				return;
			}
			throw new ArgumentOutOfRangeException("mode", mode, null);
		}

		protected override void Update()
		{
			base.Update();
			if (playerActions.m_removeEquippedItem.WasPressed)
			{
				colorWheel.RemoveClothes();
			}
			if (playerActions.m_back.WasPressed)
			{
				if (colorWheel != null && colorWheel.gameObject.activeInHierarchy && colorWheel.WheelMode == ColorWheelMode.EquipmentColors)
				{
					colorWheel.OnColorPreviewExit();
					BackToUnitPage();
				}
				if (projectileSlot.gameObject.activeInHierarchy)
				{
					BackToUnitPage();
				}
				if (wrapper != null && wrapper.GetWrapperType() == UnitEditorManager.EquipedWrapper.WrapperType.Ability)
				{
					BackToUnitPage();
				}
			}
			colorWheel.UpdateColorWheelInput(usingController);
		}

		public void BackToUnitPage()
		{
			UnitEditorUIManager unitEditorUIManager = stateManager as UnitEditorUIManager;
			if (unitEditorUIManager != null)
			{
				unitEditorUIManager.NavigateToPage("UNIT");
			}
		}

		public void Setup(UnitEditorManager.EquipedWrapper equiped)
		{
			wrapper = equiped;
			bool active = false;
			if (equiped.GetWrapperType() == UnitEditorManager.EquipedWrapper.WrapperType.Ability)
			{
				TABSParent.SetActive(value: false);
				TABSContentParent.SetActive(value: false);
			}
			else if (equiped.GetWrapperType() == UnitEditorManager.EquipedWrapper.WrapperType.Weapon)
			{
				UnitEditorManager.EquipedWeaponWrapper weapon = (UnitEditorManager.EquipedWeaponWrapper)equiped;
				if (((UnitEditorManager.EquipedWeaponWrapper)equiped).isRangedWeapon)
				{
					UnityEngine.Object.FindObjectOfType<UnitEditorListSelectScreen>().SetWeapon(weapon);
					projectileSlot.Setup(weapon);
					active = true;
				}
				TABSContentParent.SetActive(value: true);
				TABSParent.SetActive(value: true);
				UnityEngine.Object.FindObjectOfType<UnitEditorListSelectScreen>().SetWeapon(weapon);
				projectileSlot.Setup(weapon);
			}
			else
			{
				SwitchTab(0);
				TABSContentParent.SetActive(value: true);
				TABSParent.SetActive(value: false);
			}
			ProjectileStat.SetActive(active);
			ItemName.LocaleID = equiped.prop.DisplayName;
			if ((bool)equiped.prop.Entity.SpriteIcon)
			{
				ClothingImage.enabled = true;
				ClothingImage.sprite = equiped.prop.Entity.SpriteIcon;
			}
			else
			{
				ClothingImage.enabled = false;
			}
			for (int i = 0; i < CellsToRemove.Count; i++)
			{
				UnityEngine.Object.Destroy(CellsToRemove[i]);
			}
			CellsToRemove.Clear();
			colorWheel.Setup(equiped, this);
		}

		private void SpawnStatCell(UnitEditorManager.StatsWrapper stat)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(StatsCell, StatsParent);
			gameObject.GetComponent<UnitEditorStatCell>().Init(stat);
			CellsToRemove.Add(gameObject);
			ProjectileStat.transform.SetAsLastSibling();
		}
	}
}
