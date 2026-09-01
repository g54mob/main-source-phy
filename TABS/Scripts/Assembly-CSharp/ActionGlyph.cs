using System;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;

public class ActionGlyph : MonoBehaviour
{
	[SerializeField]
	protected string actionName;

	protected LocalizeText textComponent;

	protected TextMeshProUGUI textMesh;

	protected InputService inputService;

	protected GlyphService iconService;

	protected PlayerAction action;

	protected PlayerActions playerActions;

	protected bool HasInit { get; private set; }

	public PlayerAction Action => action;

	public string ActionName => actionName;

	private void Awake()
	{
		textComponent = GetComponentInChildren<LocalizeText>();
		textMesh = GetComponentInChildren<TextMeshProUGUI>();
		Init();
	}

	protected virtual void Init()
	{
		if (!HasInit)
		{
			textMesh = GetComponentInChildren<TextMeshProUGUI>();
			inputService = ServiceLocator.GetService<InputService>();
			iconService = ServiceLocator.GetService<GlyphService>();
			playerActions = PlayerActions.Instance;
			action = playerActions.GetPlayerActionByName(actionName);
			if (inputService != null)
			{
				inputService.InputChanged += OnInputSourceChanged;
				inputService.InputDeviceStyleChanged += OnInputDeviceStyleChanged;
			}
			OnInputSourceChanged(playerActions.InputType);
			HasInit = true;
		}
	}

	public void OverrideAutomaticUpdate(PlayerActions actions)
	{
		playerActions = actions;
		if (inputService != null)
		{
			inputService.InputChanged -= OnInputSourceChanged;
			inputService.InputDeviceStyleChanged -= OnInputDeviceStyleChanged;
		}
		SetGlyphText(playerActions.InputType, playerActions.LastDeviceStyle);
	}

	protected bool HasLocalizeTextComponent()
	{
		if (textComponent == null)
		{
			return false;
		}
		return true;
	}

	private void OnInputSourceChanged(InputType inputType)
	{
		if (!(textMesh == null))
		{
			SetGlyphText(inputType, PlayerActions.Instance.LastDeviceStyle);
		}
	}

	private void OnInputDeviceStyleChanged(InputDeviceStyle deviceStyle)
	{
		if (!(textMesh == null))
		{
			SetGlyphText(PlayerActions.Instance.InputType, deviceStyle);
		}
	}

	protected InputType PrepareInputTypeForConsoleGlyphs(InputType inputType)
	{
		return inputType;
	}

	protected InputDeviceStyle PrepareInputStyleForConsoleGlyphs(InputDeviceStyle deviceStyle)
	{
		return deviceStyle;
	}

	protected virtual void SetGlyphText(InputType inputType, InputDeviceStyle deviceStyle)
	{
		inputType = PrepareInputTypeForConsoleGlyphs(inputType);
		deviceStyle = PrepareInputStyleForConsoleGlyphs(deviceStyle);
		switch (inputType)
		{
		case InputType.Controller:
		{
			string actionGlyph = iconService.GetActionGlyph(action, InputType.Controller, deviceStyle);
			textMesh.text = actionGlyph ?? "";
			break;
		}
		case InputType.Keyboard:
		case InputType.Any:
			textMesh.text = string.Empty;
			break;
		default:
			throw new ArgumentOutOfRangeException("inputType", inputType, null);
		}
	}

	private void OnDestroy()
	{
		if (inputService != null)
		{
			inputService.InputChanged -= OnInputSourceChanged;
			inputService.InputDeviceStyleChanged -= OnInputDeviceStyleChanged;
		}
	}
}
