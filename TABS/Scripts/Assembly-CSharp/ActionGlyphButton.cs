using System;
using InControl;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;

public class ActionGlyphButton : ActionGlyphText
{
	[SerializeField]
	private RectTransform wordRect;

	[SerializeField]
	private TextMeshProUGUI wordText;

	[SerializeField]
	private TextMeshProUGUI glyphText;

	[SerializeField]
	private float controllerFontSize = 28f;

	[SerializeField]
	private float keyboardFontSize = 33f;

	private const float WordRectControllerSize = 83f;

	private const float WordRectControllerPosition = 41.5f;

	protected override void SetGlyphText(InputType inputType, InputDeviceStyle deviceStyle)
	{
		inputType = PrepareInputTypeForConsoleGlyphs(inputType);
		deviceStyle = PrepareInputStyleForConsoleGlyphs(deviceStyle);
		string actionGlyph = iconService.GetActionGlyph(action, InputType.Controller, deviceStyle);
		switch (inputType)
		{
		case InputType.Controller:
			if (glyphText != null)
			{
				glyphText.enabled = true;
				glyphText.text = actionGlyph;
			}
			wordRect.anchoredPosition = Vector2.right * 41.5f;
			wordRect.sizeDelta = Vector2.left * 83f;
			wordText.fontSize = controllerFontSize;
			break;
		case InputType.Keyboard:
		case InputType.Any:
			if (glyphText != null)
			{
				glyphText.enabled = false;
			}
			wordRect.anchoredPosition = Vector2.zero;
			wordRect.sizeDelta = Vector2.zero;
			wordText.fontSize = keyboardFontSize;
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}
}
