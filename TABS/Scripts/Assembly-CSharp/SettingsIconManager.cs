using System;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;

public class SettingsIconManager : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI applyButtonText;

	[SerializeField]
	private TextMeshProUGUI okBackButtonText;

	[SerializeField]
	private TextMeshProUGUI resetButtonText;

	[SerializeField]
	private TextMeshProUGUI leftBumperText;

	[SerializeField]
	private TextMeshProUGUI rightBumperText;

	private InputService inputService;

	private GlyphService iconService;

	private PlayerActions playerActions;

	private string backGlyphText;

	private string applySettingsGlyphText;

	private string resetGlyphText;

	private string tabDownGlyphText;

	private string tabUpGlyphText;

	private const string applyScreenText = "APPLY SCREEN";

	private const string okBackText = "OK";

	private const string resetDefaultText = "RESET TO DEFAULT";

	private const string backText = "BACK";

	private const string resetText = "RESET";

	private void Awake()
	{
		inputService = ServiceLocator.GetService<InputService>();
		iconService = ServiceLocator.GetService<GlyphService>();
		playerActions = PlayerActions.Instance;
		if (inputService != null)
		{
			inputService.InputChanged += OnInputChange;
			inputService.InputDeviceStyleChanged += OnInputDeviceStyleChanged;
		}
		UpdateGlyphs(PlayerActions.Instance.InputType, PlayerActions.Instance.LastDeviceStyle);
	}

	private void OnDestroy()
	{
		if (inputService != null)
		{
			inputService.InputChanged -= OnInputChange;
			inputService.InputDeviceStyleChanged -= OnInputDeviceStyleChanged;
		}
	}

	private void OnInputChange(InputType inputType)
	{
		UpdateGlyphs(inputType, PlayerActions.Instance.LastDeviceStyle);
	}

	private void OnInputDeviceStyleChanged(InputDeviceStyle style)
	{
		UpdateGlyphs(PlayerActions.Instance.InputType, style);
	}

	public void UpdateGlyphs(InputType inputType, InputDeviceStyle deviceStyle)
	{
		applySettingsGlyphText = iconService.GetActionGlyph(playerActions.m_applySettings, inputType, deviceStyle);
		backGlyphText = iconService.GetActionGlyph(playerActions.m_back, inputType, deviceStyle);
		resetGlyphText = iconService.GetActionGlyph(playerActions.m_resetSettings, inputType, deviceStyle);
		tabDownGlyphText = iconService.GetActionGlyph(playerActions.m_cycleTabsDown, inputType, deviceStyle);
		tabUpGlyphText = iconService.GetActionGlyph(playerActions.m_cycleTabsUp, inputType, deviceStyle);
		switch (inputType)
		{
		case InputType.Controller:
			okBackButtonText.text = backGlyphText + " BACK";
			applyButtonText.text = applySettingsGlyphText + " APPLY SCREEN";
			resetButtonText.text = resetGlyphText + " RESET";
			leftBumperText.text = tabDownGlyphText ?? "";
			rightBumperText.text = tabUpGlyphText ?? "";
			break;
		case InputType.Keyboard:
		case InputType.Any:
			applyButtonText.text = "APPLY SCREEN";
			okBackButtonText.text = "OK";
			resetButtonText.text = "RESET TO DEFAULT";
			leftBumperText.text = "";
			rightBumperText.text = "";
			break;
		default:
			throw new ArgumentOutOfRangeException("inputType", inputType, null);
		}
	}
}
