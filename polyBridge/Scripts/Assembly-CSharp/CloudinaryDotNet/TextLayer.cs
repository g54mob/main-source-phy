using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CloudinaryDotNet
{
	public class TextLayer : BaseLayer<TextLayer>
	{
		protected string m_text;

		protected object m_textStyle;

		protected string m_fontFamily;

		protected int m_fontSize;

		protected string m_fontWeight;

		protected string m_fontStyle;

		protected string m_fontAntialiasing;

		protected string m_fontHinting;

		protected string m_textDecoration;

		protected string m_textAlign;

		protected string m_stroke;

		protected string m_letterSpacing;

		protected string m_lineSpacing;

		public TextLayer()
		{
			m_resourceType = "text";
			FontSize(12);
		}

		public TextLayer(string text)
			: this()
		{
			Text(text);
		}

		public new TextLayer ResourceType(string resourceType)
		{
			throw new InvalidOperationException("Cannot modify resourceType " + resourceType + " for text layers");
		}

		public new TextLayer Type(string type)
		{
			throw new InvalidOperationException("Cannot modify type " + type + " for text layers");
		}

		public new TextLayer Format(string format)
		{
			throw new InvalidOperationException("Cannot modify format " + format + " for text layers");
		}

		public TextLayer Text(string text)
		{
			m_text = OverlayTextEncode(text);
			return this;
		}

		public TextLayer TextStyle(string textStyleIdentifier)
		{
			m_textStyle = textStyleIdentifier;
			return this;
		}

		public TextLayer TextStyle(Expression textStyleIdentifier)
		{
			m_textStyle = textStyleIdentifier;
			return this;
		}

		public TextLayer FontAntialiasing(FontAntialiasing value)
		{
			m_fontAntialiasing = ApiShared.GetCloudinaryParam(value);
			return this;
		}

		public TextLayer FontHinting(FontHinting value)
		{
			m_fontHinting = ApiShared.GetCloudinaryParam(value);
			return this;
		}

		public TextLayer FontFamily(string fontFamily)
		{
			m_fontFamily = fontFamily;
			return this;
		}

		public TextLayer FontSize(int fontSize)
		{
			m_fontSize = fontSize;
			return this;
		}

		public TextLayer FontWeight(string fontWeight)
		{
			m_fontWeight = fontWeight;
			return this;
		}

		public TextLayer FontStyle(string fontStyle)
		{
			m_fontStyle = fontStyle;
			return this;
		}

		public TextLayer TextDecoration(string textDecoration)
		{
			m_textDecoration = textDecoration;
			return this;
		}

		public TextLayer TextAlign(string textAlign)
		{
			m_textAlign = textAlign;
			return this;
		}

		public TextLayer Stroke(string stroke)
		{
			m_stroke = stroke;
			return this;
		}

		public TextLayer LetterSpacing(string letterSpacing)
		{
			m_letterSpacing = letterSpacing;
			return this;
		}

		public TextLayer LineSpacing(string lineSpacing)
		{
			m_lineSpacing = lineSpacing;
			return this;
		}

		public override string AdditionalParams()
		{
			if (string.IsNullOrEmpty(m_publicId) && string.IsNullOrEmpty(m_text))
			{
				throw new ArgumentException("Must supply either text or publicId.");
			}
			List<string> list = new List<string>();
			string text = TextStyleIdentifier();
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(text);
			}
			if (!string.IsNullOrEmpty(m_text))
			{
				list.Add(m_text);
			}
			return string.Join(":", list);
		}

		private static string Encode(string text)
		{
			return Utils.Encode(text).Replace("%2f", "%252f").Replace("/", "%252f")
				.Replace("%3a", ":")
				.Replace("+", "%20")
				.Replace("%2c", "%252c")
				.Replace(",", "%252c")
				.Replace("(", "%28")
				.Replace(")", "%29")
				.Replace("$", "%24");
		}

		private string OverlayTextEncode(string text)
		{
			StringBuilder stringBuilder = new StringBuilder();
			MatchCollection matchCollection = Regex.Matches(text, "\\$\\([a-zA-Z]\\w+\\)");
			int num = 0;
			foreach (Match item in matchCollection)
			{
				string text2 = text.Substring(num, item.Index - num);
				text2 = Encode(text2);
				stringBuilder.Append(text2);
				stringBuilder.Append(item.Value);
				num = item.Index + item.Length;
			}
			stringBuilder.Append(Encode(text.Substring(num)));
			return stringBuilder.ToString();
		}

		private string TextStyleIdentifier()
		{
			string text = m_textStyle?.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(m_fontWeight) && !m_fontWeight.Equals("normal", StringComparison.Ordinal))
			{
				list.Add(m_fontWeight);
			}
			if (!string.IsNullOrEmpty(m_fontStyle) && !m_fontStyle.Equals("normal", StringComparison.Ordinal))
			{
				list.Add(m_fontStyle);
			}
			if (!string.IsNullOrEmpty(m_fontAntialiasing))
			{
				list.Add("antialias_" + m_fontAntialiasing);
			}
			if (!string.IsNullOrEmpty(m_fontHinting))
			{
				list.Add("hinting_" + m_fontHinting);
			}
			if (!string.IsNullOrEmpty(m_textDecoration) && !m_textDecoration.Equals("none", StringComparison.Ordinal))
			{
				list.Add(m_textDecoration);
			}
			if (!string.IsNullOrEmpty(m_textAlign))
			{
				list.Add(m_textAlign);
			}
			if (!string.IsNullOrEmpty(m_stroke) && !m_stroke.Equals("none", StringComparison.Ordinal))
			{
				list.Add(m_stroke);
			}
			if (!string.IsNullOrEmpty(m_letterSpacing))
			{
				list.Add("letter_spacing_" + m_letterSpacing);
			}
			if (!string.IsNullOrEmpty(m_lineSpacing))
			{
				list.Add("line_spacing_" + m_lineSpacing);
			}
			if (string.IsNullOrEmpty(m_fontFamily) && list.Count == 0)
			{
				return null;
			}
			if (string.IsNullOrEmpty(m_fontFamily))
			{
				throw new ArgumentException("Must supply fontFamily.");
			}
			list.Insert(0, m_fontSize.ToString(CultureInfo.InvariantCulture));
			list.Insert(0, m_fontFamily);
			return string.Join("_", list);
		}
	}
}
