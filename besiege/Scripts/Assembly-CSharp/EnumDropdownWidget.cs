using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;

public class EnumDropdownWidget : BaseOptionWidget
{
	private class DropdownItem
	{
		public UIButton btn;

		public GameObject go;

		public DynamicText text;

		public DynamicText ext;
	}

	[SerializeField]
	private DynamicText optionText;

	[SerializeField]
	private DynamicText extText;

	[SerializeField]
	private UIButton dropdownBtn;

	[SerializeField]
	private GameObject dropdownTemplate;

	[SerializeField]
	private Transform openToggle;

	[SerializeField]
	private GameObject optionBg;

	private MainOptionsMenu.OptionsCategory.EnumOption enumOption;

	private List<DropdownItem> dropdownItems = new List<DropdownItem>();

	private bool isOpen;

	private Vector3 dropdownPos;

	private Vector3 templateSpacer = new Vector3(0f, 0.15f, 0f);

	private bool isStringEnum;

	protected void Awake()
	{
		dropdownBtn.Click += OnDropdown;
		dropdownPos = dropdownTemplate.transform.localPosition;
		dropdownTemplate.SetActive(false);
	}

	private void InitEntry(int index)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(dropdownTemplate, base.transform, true) as GameObject;
		gameObject.transform.localPosition = dropdownPos - templateSpacer * index;
		gameObject.SetActive(true);
		DropdownItem dropdownItem = new DropdownItem();
		dropdownItem.go = gameObject;
		dropdownItem.btn = gameObject.GetComponent<UIButton>();
		dropdownItem.btn.Click += delegate
		{
			OnItemClicked(index);
		};
		dropdownItem.text = gameObject.transform.FindChild("EnumName").GetComponent<DynamicText>();
		dropdownItem.ext = gameObject.transform.FindChild("ExtensionName").GetComponent<DynamicText>();
		string text = ((!isStringEnum) ? LocalisationManager.GetTranslation(enumOption.optionLocIDs[index]) : (enumOption as MainOptionsMenu.OptionsCategory.StringEnumOption).options[index]);
		string[] array = text.Split(new string[1] { "ext:" }, StringSplitOptions.None);
		string text2 = string.Empty;
		if (array.Length > 1)
		{
			text = array[0];
			text2 = array[1];
		}
		ReferenceMaster.SetDynamicText(dropdownItem.text, text);
		ReferenceMaster.SetDynamicText(dropdownItem.ext, text2);
		dropdownItems.Add(dropdownItem);
	}

	private void OnDropdown()
	{
		if (isOpen)
		{
			Clear();
			return;
		}
		int num = ((!isStringEnum) ? enumOption.optionLocIDs.Length : (enumOption as MainOptionsMenu.OptionsCategory.StringEnumOption).options.Length);
		for (int i = 0; i < num; i++)
		{
			InitEntry(i);
		}
		ToggleOpen(true);
	}

	private void OnItemClicked(int index)
	{
		enumOption.setFunc(index);
		UpdateVisual();
	}

	private void Clear()
	{
		if (isOpen)
		{
			while (dropdownItems.Count > 0)
			{
				DropdownItem dropdownItem = dropdownItems[0];
				dropdownItems.RemoveAt(0);
				UnityEngine.Object.Destroy(dropdownItem.go);
			}
			ToggleOpen(false);
		}
	}

	public override void Set(MainOptionsMenu.OptionsCategory.MenuOption option)
	{
		enumOption = option as MainOptionsMenu.OptionsCategory.EnumOption;
		isStringEnum = option is MainOptionsMenu.OptionsCategory.StringEnumOption;
		UpdateVisual();
		ToggleOpen(false);
	}

	private void ToggleOpen(bool toggle)
	{
		isOpen = toggle;
		openToggle.localRotation = Quaternion.Euler(0f, 0f, (!toggle) ? 180 : 0);
		optionBg.SetActive(toggle);
	}

	public override void UpdateVisual()
	{
		Clear();
		int num = enumOption.getFunc();
		string text = ((!isStringEnum || num == -1) ? LocalisationManager.GetTranslation((num != -1) ? enumOption.optionLocIDs[num] : 784) : (enumOption as MainOptionsMenu.OptionsCategory.StringEnumOption).options[num]);
		string[] array = text.Split(new string[1] { "ext:" }, StringSplitOptions.None);
		string text2 = string.Empty;
		if (array.Length > 1)
		{
			text = array[0];
			text2 = array[1];
		}
		ReferenceMaster.SetDynamicText(optionText, text);
		ReferenceMaster.SetDynamicText(extText, text2);
	}
}
