using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorSaveScreen : UnitEditorSubMenu
	{
		[SerializeField]
		private UnitEditorManager UCManager;

		[Header("Save Checkmarks")]
		[SerializeField]
		private UnitEditorSaveCheckmark NameCheckmark;

		[SerializeField]
		private UnitEditorSaveCheckmark CostCheckmark;

		[SerializeField]
		private UnitEditorSaveCheckmark IconCheckmark;

		[SerializeField]
		private UnitEditorSaveCheckmark DescriptionCheckmark;

		[Header("Input Fields")]
		[SerializeField]
		private NavigableTMPTextInput CostInputField;

		[SerializeField]
		private TMP_Text AutoCostField;

		[SerializeField]
		private NavigableTMPTextInput UnitDescriptionField;

		[SerializeField]
		private NavigableTMPTextInput UnitNameField;

		[SerializeField]
		private ToggleGroup costToggle;

		[SerializeField]
		private Toggle autoCostToggle;

		[SerializeField]
		private Toggle manualCostToggle;

		[SerializeField]
		private GameObject noIconText;

		[Header("Selectables")]
		[SerializeField]
		private UnitEditorSelectableItem costSelectableItem;

		[SerializeField]
		private Button saveButton;

		[SerializeField]
		private Image[] imagesToRecolor;

		[SerializeField]
		private Color inactiveColor;

		[SerializeField]
		private Color activeColor;

		[SerializeField]
		private Color canSaveGamepadColor;

		[SerializeField]
		private Color cannotSaveGamepadColor;

		private bool changedToAutoCost;

		private bool iconSet;

		private const int MinimumUnitCost = 20;

		private const string Save = "BUTTON_SAVE";

		private const string ToggleText = "BUTTON_TOGGLE";

		public bool CanSave { get; private set; }

		public void OnUnitNameChanged(string newName)
		{
			bool state = !string.IsNullOrEmpty(newName);
			NameCheckmark.SetState(state);
			CheckSaveLegality();
		}

		public void OnSetIcon(bool exists)
		{
			iconSet = exists;
			if (noIconText != null)
			{
				noIconText.SetActive(!iconSet);
			}
			IconCheckmark.SetState(exists);
			CheckSaveLegality();
		}

		public void OnEnterSaveScreen()
		{
			if (UCManager.AutoCost)
			{
				SetAutoCost();
			}
			if (noIconText != null)
			{
				noIconText.SetActive(!iconSet);
			}
			SelectCostTab();
		}

		public override void Open()
		{
			base.Open();
			if (costSelectableItem != null)
			{
				costSelectableItem.Selected += OnItemSelected;
				costSelectableItem.Deselected += OnItemDeselected;
			}
			OnEnterSaveScreen();
			UpdateGamepadGlyphs();
		}

		public override void OnGainedFocus()
		{
			base.OnGainedFocus();
			UpdateGamepadGlyphs();
		}

		private void UpdateGamepadGlyphs(string middleAction = "", string middleText = "")
		{
			if (stateManager is UnitEditorUIManager unitEditorUIManager)
			{
				UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
				if (!(gamepadGlyphs == null))
				{
					gamepadGlyphs.UpdateActionNames("Back", "BUTTON_EXIT", UnitEditorGamepadGlyphs.Position.Left);
					gamepadGlyphs.UpdateActionNames(middleAction, middleText, UnitEditorGamepadGlyphs.Position.Middle);
					gamepadGlyphs.UpdateActionNames("Menu", "BUTTON_SAVE", UnitEditorGamepadGlyphs.Position.Right, GetSaveColor());
				}
			}
		}

		private void UpdateSaveButtonColor()
		{
			if (stateManager is UnitEditorUIManager unitEditorUIManager)
			{
				UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
				if (!(gamepadGlyphs == null))
				{
					gamepadGlyphs.UpdateTextColor(GetSaveColor(), UnitEditorGamepadGlyphs.Position.Right);
				}
			}
		}

		private Color GetSaveColor()
		{
			if (!CanSave)
			{
				return cannotSaveGamepadColor;
			}
			return canSaveGamepadColor;
		}

		public override void Close()
		{
			base.Close();
			if (costSelectableItem != null)
			{
				costSelectableItem.Selected -= OnItemSelected;
				costSelectableItem.Deselected -= OnItemDeselected;
			}
		}

		protected override void UpdateGamepads()
		{
			base.UpdateGamepads();
			if (playerActions != null && !UnitEditorManager.isTestingUnit)
			{
				if (playerActions.m_toggleUnitCost.WasPressed && base.SelectedItem == costSelectableItem && !CostInputField.IsTextInputEnabled)
				{
					ToggleUnitCostMode();
				}
				if (playerActions.m_menu.WasPressed && CanSave)
				{
					saveButton.onClick.Invoke();
				}
			}
		}

		protected override void OnItemSelected(UnitEditorSelectableItem item)
		{
			base.OnItemSelected(item);
			if (item == costSelectableItem)
			{
				UpdateGamepadGlyphs("Toggle Unit Cost", "BUTTON_TOGGLE");
			}
		}

		protected override void OnItemDeselected(UnitEditorSelectableItem item)
		{
			base.OnItemDeselected(item);
			UpdateGamepadGlyphs();
		}

		private void SetAutoCost()
		{
			CostCheckmark.SetState(newState: true);
			CheckSaveLegality();
			AutoCostField.text = UCManager.GetAutoCost().ToString();
		}

		public void SetCustomCost(string customCost)
		{
			if (string.IsNullOrEmpty(customCost))
			{
				return;
			}
			if (!int.TryParse(customCost, out var result))
			{
				if (!float.TryParse(customCost, out var result2))
				{
					return;
				}
				result = Mathf.FloorToInt(result2);
			}
			ushort num = (ushort)((result >= 0) ? ((result <= 65535) ? ((ushort)result) : ushort.MaxValue) : 0);
			CostInputField.text = num.ToString();
			CostCheckmark.SetState(num > 20);
			CheckSaveLegality();
			UCManager.CustomCost = num;
			Debug.Log($"Setting Custom Cost: {num}");
			bool autoCost = num == UCManager.GetAutoCost();
			SetAutoCost(autoCost);
			SelectCostTab();
		}

		public void SelectCostTab()
		{
			int num = 0;
			if (!UCManager.AutoCost)
			{
				num = 1;
			}
			costToggle.GetComponentsInChildren<Toggle>()[num].isOn = true;
		}

		private void ToggleUnitCostMode()
		{
			if (!(UCManager == null))
			{
				if (UCManager.AutoCost)
				{
					manualCostToggle.isOn = true;
				}
				else
				{
					autoCostToggle.isOn = true;
				}
			}
		}

		public void SwapToManualCost()
		{
			SetAutoCost(auto: false);
			SetCustomCost(CostInputField.text);
		}

		public void SetAutoCost(bool auto)
		{
			changedToAutoCost = auto;
			if (!auto)
			{
				UCManager.AutoCost = false;
			}
		}

		private void LateUpdate()
		{
			if (changedToAutoCost)
			{
				UCManager.AutoCost = true;
				SetAutoCost();
				changedToAutoCost = false;
			}
		}

		public void Clear()
		{
			OnEnterSaveScreen();
			costToggle.GetComponentsInChildren<Toggle>()[0].isOn = true;
			UCManager.AutoCost = false;
			UnitDescriptionField.text = "";
		}

		public void OnEnterDescription(string value)
		{
			CheckSaveLegality();
		}

		public string GetUnitName()
		{
			return UnitNameField.text;
		}

		public string GetUnitDescrption()
		{
			return UnitDescriptionField.text;
		}

		public void CheckSaveLegality()
		{
			bool state = NameCheckmark.GetState();
			bool state2 = CostCheckmark.GetState();
			bool state3 = IconCheckmark.GetState();
			if (state && state2 && state3)
			{
				CanSave = true;
				saveButton.interactable = true;
				for (int i = 0; i < imagesToRecolor.Length; i++)
				{
					imagesToRecolor[i].color = activeColor;
				}
				saveButton.GetComponent<UISounds>().enabled = true;
				saveButton.GetComponent<UIScaleJiggle>().isEnabled = true;
			}
			else
			{
				CanSave = false;
				saveButton.interactable = false;
				for (int j = 0; j < imagesToRecolor.Length; j++)
				{
					imagesToRecolor[j].color = inactiveColor;
				}
				saveButton.GetComponent<UISounds>().enabled = false;
				saveButton.GetComponent<UIScaleJiggle>().isEnabled = false;
			}
			UpdateSaveButtonColor();
		}

		public void SetName(string name)
		{
			UnitNameField.text = name;
		}

		public void SetDescription(string description)
		{
			UnitDescriptionField.text = description;
		}
	}
}
