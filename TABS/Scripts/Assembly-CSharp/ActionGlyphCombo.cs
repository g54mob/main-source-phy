using System;
using InControl;
using Landfall.TABS_Input;
using UnityEngine;

public class ActionGlyphCombo : ActionGlyphText
{
	[SerializeField]
	protected string additionalActionName;

	public string AdditionalActionName
	{
		get
		{
			return additionalActionName;
		}
		set
		{
			additionalActionName = value;
		}
	}

	protected override void SetGlyphText(InputType inputType, InputDeviceStyle deviceStyle)
	{
		inputType = PrepareInputTypeForConsoleGlyphs(inputType);
		deviceStyle = PrepareInputStyleForConsoleGlyphs(deviceStyle);
		if (HasLocalizeTextComponent())
		{
			UnityEngine.Object.Destroy(textComponent);
		}
		words = originalWords;
		string text = iconService.GetActionGlyph(action, inputType, deviceStyle);
		_ = string.Empty;
		string text2 = (string.IsNullOrEmpty(words) ? string.Empty : Localizer.GetSinglePhrase(words, (string[])null).ToUpper());
		if (!string.IsNullOrEmpty(additionalActionName))
		{
			PlayerAction playerActionByName = playerActions.GetPlayerActionByName(additionalActionName);
			string actionGlyph = iconService.GetActionGlyph(playerActionByName, inputType, deviceStyle);
			text = text + " + " + actionGlyph;
		}
		switch (inputType)
		{
		case InputType.Controller:
		{
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
			text = (rightAlignGlyph ? (text2 + text3 + text) : (text + text3 + text2));
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
}
