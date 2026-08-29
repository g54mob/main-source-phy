using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InGameTextEditor.Format
{
	public class SimpleSyntaxHighlighter : TextFormatter
	{
		public TextStyle textStyleRed = new TextStyle(new Color(0.8f, 0f, 0f));

		public TextStyle textStyleGreen = new TextStyle(new Color(0f, 0.8f, 0f));

		public TextStyle textStyleBlue = new TextStyle(new Color(0f, 0f, 0.8f));

		public TextStyle textStyleNumber = new TextStyle(FontStyle.Bold);

		private bool initialized;

		private Regex regex;

		public override bool Initialized => initialized;

		public override void Init()
		{
			string text = "";
			text += "(?<red>\\b[Rr]ed)";
			text += "|(?<green>\\b[Gg]reen)";
			text += "|(?<blue>\\b[Bb]lue)";
			text += "|(?<number>\\b[0-9]+(\\.[0-9]+)?)";
			regex = new Regex(text);
			initialized = true;
		}

		public override void OnLineChanged(Line line)
		{
			List<TextFormatGroup> list = new List<TextFormatGroup>();
			if (line.Text.Length > 0)
			{
				foreach (Match item in regex.Matches(line.Text))
				{
					int num = 0;
					foreach (Group group in item.Groups)
					{
						if (group.Success && num > 0)
						{
							string text = regex.GroupNameFromNumber(num);
							int index = group.Index;
							int endIndex = index + group.Value.Length - 1;
							switch (text)
							{
							case "red":
								list.Add(new TextFormatGroup(index, endIndex, textStyleRed));
								break;
							case "green":
								list.Add(new TextFormatGroup(index, endIndex, textStyleGreen));
								break;
							case "blue":
								list.Add(new TextFormatGroup(index, endIndex, textStyleBlue));
								break;
							case "number":
								list.Add(new TextFormatGroup(index, endIndex, textStyleNumber));
								break;
							}
						}
						num++;
					}
				}
			}
			line.ApplyTextFormat(list);
		}
	}
}
