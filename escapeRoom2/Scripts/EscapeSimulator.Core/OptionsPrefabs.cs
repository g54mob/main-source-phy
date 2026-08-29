using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "OptionsPrefabs", menuName = "ES/OptionsPrebas")]
public class OptionsPrefabs : ScriptableObject
{
	public OptionsPrefab_TabButton tabButton;

	public OptionsPrefab_Button button;

	public OptionsPrefab_Dropdown dropdown;

	public OptionsPrefab_Slider slider;

	public OptionsPrefab_Toggle toggle;

	public OptionsPrefab_ToggleMulti toggleMulti;

	public OptionsPrefab_InputField inputField;

	public GameObject label;

	public GameObject header;

	public GameObject labelWithTitle;

	public GameObject rebindingButton;

	public GameObject rebindingButtonWithSecondaryButton;

	public GameObject VRControlsImage;

	public ES2FrameUI es2Frame;

	public GameObject spacer;

	public OptionsPrefab_TabButton getTabButton(string label, Transform parent)
	{
		OptionsPrefab_TabButton optionsPrefab_TabButton = UnityEngine.Object.Instantiate(tabButton, parent);
		optionsPrefab_TabButton.Text.text = label;
		Localization.translateObject(optionsPrefab_TabButton.Text.transform);
		PineUI.addButtonListeners(optionsPrefab_TabButton.button);
		return optionsPrefab_TabButton;
	}

	public SelectableOption getHeader(string label, Transform parent)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(header, parent);
		Text componentInChildren = gameObject.GetComponentInChildren<Text>();
		componentInChildren.fontSize = 40;
		componentInChildren.GetComponentInChildren<Text>().text = label;
		if (label != null)
		{
			Localization.translateObject(gameObject.transform);
		}
		return new SelectableOption(gameObject, null, SelectableOptionType.Header, 1);
	}

	public SelectableOption getDropdown(string label, Transform parent, string[] choices, int initial, Action<int> onChange, int direction = 1, Sprite[] images = null)
	{
		OptionsPrefab_Dropdown optionsPrefab_Dropdown = UnityEngine.Object.Instantiate(dropdown, parent);
		optionsPrefab_Dropdown.gameObject.SetActive(value: true);
		optionsPrefab_Dropdown.label.text = label;
		Dropdown realDropdown = optionsPrefab_Dropdown.dropdown;
		realDropdown.options = new List<Dropdown.OptionData>();
		for (int i = 0; i < choices.Length; i++)
		{
			string text = choices[i];
			Sprite image = ((images != null) ? images[i] : null);
			realDropdown.options.Add(new Dropdown.OptionData
			{
				text = text,
				image = image
			});
		}
		realDropdown.value = initial;
		realDropdown.onValueChanged.AddListener(delegate
		{
			onChange(realDropdown.value);
			PineFmod.playOneShotSound("event:/SFX/GENERAL/uiToggle");
		});
		Localization.translateObject(optionsPrefab_Dropdown.transform);
		return new SelectableOption(optionsPrefab_Dropdown.dropdown.gameObject, optionsPrefab_Dropdown, SelectableOptionType.Dropdown, direction);
	}

	public SelectableOption getSlider(string label, Transform parent, float initial, float min, float max, Action<float> onChange, float interval)
	{
		OptionsPrefab_Slider optionsPrefab_Slider = UnityEngine.Object.Instantiate(slider, parent);
		optionsPrefab_Slider.gameObject.SetActive(value: true);
		optionsPrefab_Slider.GetComponentInChildren<Text>().text = label;
		Slider realSlider = optionsPrefab_Slider.GetComponentInChildren<Slider>();
		realSlider.minValue = min;
		realSlider.maxValue = max;
		realSlider.value = initial;
		bool flag = Math.Abs(interval - Mathf.Floor(interval)) < 1E-06f;
		string valueStringFormat = (flag ? "0" : "0.00");
		InputField inputField = optionsPrefab_Slider.GetComponentInChildren<InputField>();
		inputField.text = realSlider.value.ToString(valueStringFormat);
		inputField.onEndEdit.AddListener(delegate
		{
			if (float.TryParse(inputField.text, out var result))
			{
				realSlider.value = Mathf.Clamp(result, min, max);
				inputField.text = realSlider.value.ToString(valueStringFormat);
				onChange(realSlider.value);
			}
		});
		realSlider.onValueChanged.AddListener(delegate
		{
			if (!Controller.isActive())
			{
				float value = realSlider.value;
				value = Mathf.Round(value / interval) * interval;
				realSlider.value = value;
			}
			onChange(realSlider.value);
			inputField.text = realSlider.value.ToString(valueStringFormat);
			PineFmod.playOneShotSound("event:/Sound Effects/00 General/Menu Sound Effects/Small/UI_Small_01");
		});
		Localization.translateObject(optionsPrefab_Slider.transform);
		return new SelectableOption(optionsPrefab_Slider.slider.gameObject, optionsPrefab_Slider, SelectableOptionType.Slider, 1);
	}

	public SelectableOption getToggle(string label, Transform parent, bool initial, Action<bool> onChange, string onText = null, string offText = null)
	{
		OptionsPrefab_Toggle optionsPrefab_Toggle = UnityEngine.Object.Instantiate(toggle, parent);
		optionsPrefab_Toggle.gameObject.SetActive(value: true);
		optionsPrefab_Toggle.GetComponentInChildren<Text>().text = label;
		Toggle realToggle = optionsPrefab_Toggle.GetComponentInChildren<Toggle>();
		realToggle.isOn = initial;
		realToggle.onValueChanged.AddListener(delegate
		{
			onChange(realToggle.isOn);
			PineFmod.playOneShotSound("event:/Sound Effects/00 General/Menu Sound Effects/Small/UI_Small_07");
		});
		Localization.translateObject(optionsPrefab_Toggle.transform);
		return new SelectableOption(optionsPrefab_Toggle.toggle.gameObject, optionsPrefab_Toggle, SelectableOptionType.Toggle, 1);
	}

	public SelectableOption getToggleMulti(string label, Transform parent, string[] choices, int initial, Action<int> onChange)
	{
		OptionsPrefab_ToggleMulti optionsPrefab_ToggleMulti = UnityEngine.Object.Instantiate(toggleMulti, parent);
		optionsPrefab_ToggleMulti.gameObject.SetActive(value: true);
		optionsPrefab_ToggleMulti.GetComponentInChildren<Text>().text = label;
		RingToggle componentInChildren = optionsPrefab_ToggleMulti.GetComponentInChildren<RingToggle>();
		componentInChildren.values = choices;
		componentInChildren.selected = initial;
		componentInChildren.syncValues();
		componentInChildren.onChange = delegate(int selected)
		{
			onChange(selected);
			PineFmod.playOneShotSound("event:/SFX/GENERAL/uiToggle");
		};
		Localization.translateObject(optionsPrefab_ToggleMulti.transform);
		return new SelectableOption(optionsPrefab_ToggleMulti.toggle.gameObject, optionsPrefab_ToggleMulti, SelectableOptionType.Toggle, 1);
	}

	public SelectableOption getButton(string label, Transform parent, string buttonLabel, Action onPress)
	{
		OptionsPrefab_Button optionsPrefab_Button = UnityEngine.Object.Instantiate(button, parent);
		optionsPrefab_Button.gameObject.SetActive(value: true);
		optionsPrefab_Button.label.text = label;
		optionsPrefab_Button.buttonText.text = buttonLabel;
		optionsPrefab_Button.button.onClick.AddListener(delegate
		{
			onPress();
			PineFmod.playOneShotSound("event:/SFX/GENERAL/uiToggle");
		});
		Localization.translateObject(optionsPrefab_Button.transform);
		return new SelectableOption(optionsPrefab_Button.gameObject, optionsPrefab_Button, SelectableOptionType.Button, 1);
	}

	public SelectableOption getLabelWithTitle(string label, string title, Transform parent)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(labelWithTitle, parent);
		gameObject.SetActive(value: true);
		Text[] componentsInChildren = gameObject.GetComponentsInChildren<Text>();
		Text text = componentsInChildren[0];
		Text obj = componentsInChildren[1];
		obj.text = label;
		text.text = title;
		Localization.translateObject(obj.transform);
		Localization.translateObject(text.transform);
		return new SelectableOption(labelWithTitle, null, SelectableOptionType.LabelWithText, 1);
	}

	public SelectableOption getInputField(string label, Transform parent, string initial, Action<string> onChange)
	{
		OptionsPrefab_InputField inputObject = UnityEngine.Object.Instantiate(inputField, parent);
		inputObject.gameObject.SetActive(value: true);
		inputObject.label.text = label;
		inputObject.inputField.text = initial;
		inputObject.inputField.onEndEdit.AddListener(delegate
		{
			onChange(inputObject.inputField.text);
		});
		Localization.translateObject(inputObject.transform);
		return new SelectableOption(inputObject.inputField.gameObject, inputObject, SelectableOptionType.InputField, 1);
	}

	public void addHoverable(OptionsPrefab_Base baseObject, Action onHover, Action onUnhover)
	{
		baseObject.usingHover = true;
		baseObject.onHover = onHover;
		baseObject.onUnhover = onUnhover;
	}

	public (GameObject gameObject, MenuOptions.KeyBinding keybinding) getRebindingKey(string label, Transform parent, KeyBindingAction action, Action onPressPrimary, Action onPressSecondary)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(rebindingButtonWithSecondaryButton, parent);
		gameObject.SetActive(value: true);
		Text componentInChildren = gameObject.GetComponentInChildren<Text>();
		componentInChildren.text = label;
		Localization.translateObject(componentInChildren.transform);
		Button component = gameObject.transform.Find("PrimaryKeyCodeButton").GetComponent<Button>();
		component.onClick.AddListener(delegate
		{
			onPressPrimary();
			PineFmod.playOneShotSound("event:/SFX/GENERAL/uiToggle");
		});
		Button component2 = gameObject.transform.Find("SecondaryKeyCodeButton").GetComponent<Button>();
		component2.onClick.AddListener(delegate
		{
			onPressSecondary();
			PineFmod.playOneShotSound("event:/SFX/GENERAL/uiToggle");
		});
		MenuOptions.KeyBinding item = new MenuOptions.KeyBinding
		{
			title = label,
			action = action,
			text = component.GetComponentInChildren<Text>(),
			textSecondary = component2.GetComponentInChildren<Text>()
		};
		return (gameObject: gameObject, keybinding: item);
	}

	public ES2FrameUI getES2Frame(Transform parent)
	{
		ES2FrameUI eS2FrameUI = UnityEngine.Object.Instantiate(es2Frame, parent);
		eS2FrameUI.gameObject.SetActive(value: true);
		return eS2FrameUI;
	}

	public static OptionsPrefabs get()
	{
		return AssetBundleLoader.getAsset<OptionsPrefabs>(AssetBundleType.Misc, "Assets/_Misc/Options/OptionsPrefabs.asset");
	}
}
