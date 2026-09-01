using System;
using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using UnityEngine;

namespace TFBGames
{
	public class MultiActionGlyphText : ActionGlyphText
	{
		[Serializable]
		public class GlyphTextContainer
		{
			public string actionName;

			public string words;
		}

		[Header("Additional Glyph Text")]
		[SerializeField]
		protected List<GlyphTextContainer> glyphTexts;

		[SerializeField]
		protected string leftGlyphBlock = "[";

		[SerializeField]
		protected string rightGlyphBlock = "]";

		[SerializeField]
		protected string separator = " ";

		[SerializeField]
		protected string sentenceEnd = "";

		protected override void Init()
		{
			if (!base.HasInit)
			{
				if (!string.IsNullOrEmpty(actionName) && !string.IsNullOrEmpty(words))
				{
					glyphTexts.Insert(0, new GlyphTextContainer
					{
						actionName = actionName,
						words = words
					});
				}
				base.Init();
			}
		}

		protected override void SetGlyphText(InputType inputType, InputDeviceStyle deviceStyle)
		{
			inputType = PrepareInputTypeForConsoleGlyphs(inputType);
			deviceStyle = PrepareInputStyleForConsoleGlyphs(deviceStyle);
			if (HasLocalizeTextComponent())
			{
				words = string.Empty;
				int count = glyphTexts.Count;
				for (int i = 0; i < count; i++)
				{
					GlyphTextContainer glyphTextContainer = glyphTexts[i];
					string text = ((i == count - 1) ? sentenceEnd : separator);
					words = words + GetGlyphTextCombo(glyphTextContainer.actionName, glyphTextContainer.words, inputType, deviceStyle) + text;
				}
				textComponent.LocaleID = words;
			}
		}

		protected string GetGlyphTextCombo(string glyph, string rawText, InputType inputType, InputDeviceStyle deviceStyle)
		{
			inputType = PrepareInputTypeForConsoleGlyphs(inputType);
			deviceStyle = PrepareInputStyleForConsoleGlyphs(deviceStyle);
			PlayerAction playerActionByName = playerActions.GetPlayerActionByName(glyph);
			string text = iconService.GetActionGlyph(playerActionByName, inputType, deviceStyle);
			string text2 = rawText;
			string result = string.Empty;
			switch (inputType)
			{
			case InputType.Controller:
				if (overrideTextSize > 0)
				{
					text2 = $"<size={overrideTextSize}%>{text2}</size>";
				}
				if (overrideGlyphSize > 0)
				{
					text = $"<size={overrideGlyphSize}%>{text}</size>";
				}
				if (overrideVerticalAlign > 0)
				{
					text2 = $"<voffset={overrideVerticalAlign}>{text2}</voffset>";
				}
				result = (rightAlignGlyph ? (text2 + " " + leftGlyphBlock + text + rightGlyphBlock) : (leftGlyphBlock + text + rightGlyphBlock + " " + text2));
				break;
			case InputType.Keyboard:
			case InputType.Any:
				if (!hideText)
				{
					if (overrideTextSize > 0)
					{
						text2 = $"<size={overrideTextSize}%>{text2}</size>";
					}
					if (overrideVerticalAlign > 0)
					{
						text2 = "<voffset=15>" + text2 + "</voffset>";
					}
					result = (rightAlignGlyph ? (text2 + " " + leftGlyphBlock + text + rightGlyphBlock) : (leftGlyphBlock + text + rightGlyphBlock + " " + text2));
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("inputType", inputType, null);
			}
			return result;
		}
	}
}
