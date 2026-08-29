using System;
using UnityEngine;

namespace InGameTextEditor.Format
{
	[Serializable]
	public class TextStyle
	{
		public FontStyle fontStyle;

		public bool overrideFontStyle;

		public Color fontColor = new Color(0f, 0f, 0f, 1f);

		public bool overrideColor;

		public string RichtTextOpenTag
		{
			get
			{
				string text = (overrideColor ? ("<color=#" + ColorUtility.ToHtmlStringRGBA(fontColor) + ">") : "");
				if (overrideFontStyle)
				{
					switch (fontStyle)
					{
					case FontStyle.Bold:
						text += "<b>";
						break;
					case FontStyle.Italic:
						text += "<i>";
						break;
					case FontStyle.BoldAndItalic:
						text += "<b><i>";
						break;
					}
				}
				return text;
			}
		}

		public string RichtTextCloseTag
		{
			get
			{
				string text = "";
				if (overrideFontStyle)
				{
					switch (fontStyle)
					{
					case FontStyle.Bold:
						text += "</b>";
						break;
					case FontStyle.Italic:
						text += "</i>";
						break;
					case FontStyle.BoldAndItalic:
						text += "</i></b>";
						break;
					}
				}
				if (overrideColor)
				{
					text += "</color>";
				}
				return text;
			}
		}

		public TextStyle(FontStyle fontStyle)
		{
			this.fontStyle = fontStyle;
			overrideFontStyle = true;
		}

		public TextStyle(Color fontColor)
		{
			this.fontColor = fontColor;
			overrideColor = true;
		}

		public TextStyle(FontStyle fontStyle, Color fontColor)
		{
			this.fontStyle = fontStyle;
			this.fontColor = fontColor;
			overrideFontStyle = true;
			overrideColor = true;
		}
	}
}
