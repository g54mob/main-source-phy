using System;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine.UI;

public class UnitEditorConfirmLoadUI : UIComponentMainMenu
{
	public Image IconImage;

	public TextMeshProUGUI unitName;

	public TextMeshProUGUI renameButtonText;

	public TMP_InputField RenameInputField;

	public Image confirmRenameImage;

	public TextMeshProUGUI infoBoxUnitName;

	public TextMeshProUGUI infoBoxUnitInfo;

	public TextMeshProUGUI infoBoxUnitDescription;

	private UnitBlueprint currentUnit;

	private bool isRenaming;

	public void SetupUI(UnitBlueprint unit)
	{
		currentUnit = unit;
		if ((bool)unit.Entity.SpriteIcon)
		{
			IconImage.sprite = unit.Entity.SpriteIcon;
		}
		unitName.text = unit.Name;
		isRenaming = false;
		unitName.enabled = true;
		unitName.text = unit.Entity.Name;
		RenameInputField.gameObject.SetActive(value: false);
		confirmRenameImage.enabled = false;
		renameButtonText.enabled = true;
		string text = "By: Landfall";
		if (unit.IsCustomUnit)
		{
			text = "By: You";
		}
		infoBoxUnitName.text = unit.Entity.Name;
		infoBoxUnitDescription.text = unit.UnitDescription;
		infoBoxUnitInfo.text = text + Environment.NewLine + "Local Unit";
	}

	public void RenameUnit()
	{
		if (!isRenaming)
		{
			isRenaming = true;
			unitName.enabled = false;
			RenameInputField.text = currentUnit.Entity.Name;
			RenameInputField.gameObject.SetActive(value: true);
			confirmRenameImage.enabled = true;
			renameButtonText.enabled = false;
		}
		else if (!string.IsNullOrEmpty(RenameInputField.text))
		{
			string text = RenameInputField.text;
			isRenaming = false;
			unitName.enabled = true;
			unitName.text = text;
			RenameInputField.gameObject.SetActive(value: false);
			BattleCreatorSharedCommands.RenameUnit(currentUnit, text);
			confirmRenameImage.enabled = false;
			renameButtonText.enabled = true;
		}
	}
}
