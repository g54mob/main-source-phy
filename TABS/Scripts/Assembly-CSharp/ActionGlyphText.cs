using System;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class ActionGlyphText : ActionGlyph
{
	[SerializeField]
	protected string words;

	[SerializeField]
	[Tooltip("Hide Text When Mouse/Keyboard is in use")]
	protected bool hideText;

	[SerializeField]
	[Range(0f, 100f)]
	protected int overrideTextSize;

	[SerializeField]
	[Range(0f, 200f)]
	protected int overrideGlyphSize;

	[SerializeField]
	protected bool rightAlignGlyph;

	[SerializeField]
	protected int overrideVerticalAlign;

	[SerializeField]
	protected int overrideGlyphVerticalAlign;

	[SerializeField]
	protected int spaceBetweenGlyphAndText;

	[SerializeField]
	protected bool forceTextToNewLine;

	protected string originalWords;

	protected ActionGlyphTextPlatformSpecificOverride overrideSettings;

	protected int overrideIndent;

	protected override void Init()
	{
		if (!base.HasInit)
		{
			overrideSettings = GetComponent<ActionGlyphTextPlatformSpecificOverride>();
			TryApplyOverrideSettings();
			originalWords = words;
			base.Init();
			RefreshGlyph();
		}
	}

	private void OnDestroy()
	{
		Localizer.UnregisterCallback(this);
	}

	protected override void SetGlyphText(InputType inputType, InputDeviceStyle deviceStyle)
	{
		inputType = PrepareInputTypeForConsoleGlyphs(inputType);
		deviceStyle = PrepareInputStyleForConsoleGlyphs(deviceStyle);
		if (HasLocalizeTextComponent())
		{
			UnityEngine.Object.Destroy(textComponent);
		}
		string text = iconService.GetActionGlyph(action, inputType, deviceStyle);
		words = originalWords;
		string text2 = (string.IsNullOrEmpty(words) ? string.Empty : Localizer.GetSinglePhrase(words, (string[])null).ToUpper());
		TryApplyOverrideSettings();
		switch (inputType)
		{
		case InputType.Controller:
		{
			if (Mathf.Abs(overrideIndent) > 0)
			{
				text = $"<indent={overrideIndent}%>{text}";
			}
			if (overrideGlyphSize > 0)
			{
				text = $"<size={overrideGlyphSize}%>{text}</size>";
			}
			if (overrideGlyphVerticalAlign != 0)
			{
				text = $"<voffset={overrideGlyphVerticalAlign}>{text}</voffset>";
			}
			if (overrideTextSize > 0)
			{
				text2 = $"<size={overrideTextSize}%>{text2}</size>";
			}
			if (overrideVerticalAlign != 0)
			{
				text2 = $"<voffset={overrideVerticalAlign}>{text2}</voffset>";
			}
			string text3 = ((spaceBetweenGlyphAndText != 0) ? $"<space={spaceBetweenGlyphAndText}/>" : " ");
			string text4 = (forceTextToNewLine ? Environment.NewLine : string.Empty);
			text = (rightAlignGlyph ? (text2 + text4 + text3 + text) : (text + text4 + text3 + text2));
			textMesh.text = text;
			break;
		}
		case InputType.Keyboard:
		case InputType.Any:
			if (hideText)
			{
				textMesh.text = string.Empty;
				break;
			}
			textMesh.text = text2;
			if (overrideTextSize > 0)
			{
				textMesh.text = $"<size={overrideTextSize}%>{textMesh.text}";
			}
			if (overrideVerticalAlign > 0)
			{
				textMesh.text = $"<voffset={overrideVerticalAlign}>{textMesh.text}</voffset>";
			}
			break;
		default:
			throw new ArgumentOutOfRangeException("inputType", inputType, null);
		}
	}

	public void UpdateActionNames(string newAction, string newWords)
	{
		if (playerActions == null)
		{
			Init();
		}
		if (newAction == null || newWords == null)
		{
			Debug.LogError("Unable to update action names", this);
			return;
		}
		actionName = newAction;
		action = playerActions.GetPlayerActionByName(actionName);
		originalWords = newWords;
		SetGlyphText(playerActions.InputType, playerActions.LastDeviceStyle);
	}

	public void UpdateTextColor(Color color)
	{
		textMesh.color = color;
	}

	public void RefreshGlyph()
	{
		InputDevice activeDevice = PlayerActions.Instance.ActiveDevice;
		InputType inputType = PlayerActions.Instance.InputType;
		InputDeviceStyle deviceStyle = activeDevice?.DeviceStyle ?? InputDeviceStyle.XboxOne;
		Localizer.RegisterCallback(this, delegate
		{
			SetGlyphText(inputType, deviceStyle);
		});
		RefreshLocalizer();
	}

	private void RefreshLocalizer()
	{
		IPlayerPrefsPlatform service = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		if (service != null)
		{
			Localizer.SetLanguage((Localizer.Language)service.GetInt("VIDEO_LANGUAGE"));
		}
	}

	private void TryApplyOverrideSettings()
	{
		if (overrideSettings != null)
		{
			overrideSettings.OverrideSettings(ref overrideGlyphSize, ref overrideVerticalAlign, ref overrideGlyphVerticalAlign, ref spaceBetweenGlyphAndText, ref overrideIndent, ref overrideTextSize);
		}
	}
}
