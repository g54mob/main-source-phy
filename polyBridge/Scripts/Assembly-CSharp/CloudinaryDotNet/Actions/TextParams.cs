using System;
using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class TextParams : BaseParams
	{
		public string Text { get; set; }

		public string PublicId { get; set; }

		public string FontFamily { get; set; }

		public int FontSize { get; set; }

		public string FontColor { get; set; }

		[Obsolete("Property FontWeitgh is deprecated, please use FontWeight instead")]
		public string FontWeitgh
		{
			get
			{
				return FontWeight;
			}
			set
			{
				FontWeight = value;
			}
		}

		public string FontWeight { get; set; }

		public string FontStyle { get; set; }

		public string Background { get; set; }

		public string Opacity { get; set; }

		public string TextDecoration { get; set; }

		public string TextAlign { get; set; }

		public TextParams()
		{
			FontSize = 12;
		}

		public TextParams(string text)
			: this()
		{
			Text = text;
		}

		public override void Check()
		{
			if (string.IsNullOrEmpty(Text))
			{
				throw new ArgumentException("Text must be specified in TextParams!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "text", Text);
			BaseParams.AddParam(sortedDictionary, "public_id", PublicId);
			BaseParams.AddParam(sortedDictionary, "font_family", FontFamily);
			BaseParams.AddParam(sortedDictionary, "font_size", FontSize.ToString(CultureInfo.InvariantCulture));
			BaseParams.AddParam(sortedDictionary, "font_color", FontColor);
			BaseParams.AddParam(sortedDictionary, "font_weight", FontWeight);
			BaseParams.AddParam(sortedDictionary, "font_style", FontStyle);
			BaseParams.AddParam(sortedDictionary, "background", Background);
			BaseParams.AddParam(sortedDictionary, "opacity", Opacity);
			BaseParams.AddParam(sortedDictionary, "text_decoration", TextDecoration);
			BaseParams.AddParam(sortedDictionary, "text_align", TextAlign);
			return sortedDictionary;
		}
	}
}
