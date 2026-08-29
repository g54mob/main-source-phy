using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InGameTextEditor.Format
{
	public class LuaSyntaxHighlighter : TextFormatter
	{
		public TextStyle textStyleComment = new TextStyle(new Color(0.5f, 0.5f, 0.5f));

		public TextStyle textStyleString = new TextStyle(new Color(0.9f, 0.4f, 0.1f));

		public TextStyle textStyleNumber = new TextStyle(new Color(0.2f, 0.4f, 0.6f));

		public TextStyle textStyleKeyword = new TextStyle(new Color(0.2f, 0.6f, 0.6f));

		public TextStyle textStyleBuiltIn = new TextStyle(new Color(0.8f, 0.2f, 0.8f));

		public static readonly List<string> keywords = new List<string>
		{
			"and", "break", "do", "else", "elseif", "end", "false", "for", "function", "goto",
			"if", "in", "local", "nil", "not", "or", "repeat", "return", "then", "true",
			"until", "while"
		};

		private readonly string[] builtIns = new string[33]
		{
			"assert", "collectgarbage", "dofile", "error", "getmetatable", "ipairs", "load", "loadfile", "next", "pairs",
			"pcall", "print", "rawequal", "rawget", "rawlen", "rawset", "select", "setmetatable", "tonumber", "tostring",
			"type", "xpcall", "_VERSION", "_G", "coroutine", "table", "string", "math", "io", "os",
			"debug", "package", "file"
		};

		private bool initialized;

		private Regex regex;

		public override bool Initialized => initialized;

		public override void Init()
		{
			string text = "";
			text += "(?<multiLineComment>--\\[\\[.*?\\]\\])";
			text += "|(?<comment>--.*$)";
			text += "|(?<longString>\\[\\[.*?\\]\\])";
			text += "|(?<string>\"(?:[^\"\\\\]|\\\\.)*[\"]?)";
			text += "|(?<char>'(?:[^'\\\\]|\\\\.)*[']?)";
			text += "|(?<number>\\b\\d+(\\.\\d*)?([eE][+-]?\\d+)?\\b)";
			text += "|(?<keyword>";
			for (int i = 0; i < keywords.Count; i++)
			{
				text = text + "\\b" + keywords[i] + "\\b" + ((i < keywords.Count - 1) ? "|" : "");
			}
			text += ")";
			text += "|(?<builtIn>";
			for (int j = 0; j < builtIns.Length; j++)
			{
				text = text + "\\b" + builtIns[j] + "\\b" + ((j < builtIns.Length - 1) ? "|" : "");
			}
			text += ")";
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
							case "multiLineComment":
							case "comment":
							case "longString":
								list.Add(new TextFormatGroup(index, endIndex, textStyleComment));
								break;
							case "string":
							case "char":
								list.Add(new TextFormatGroup(index, endIndex, textStyleString));
								break;
							case "number":
								list.Add(new TextFormatGroup(index, endIndex, textStyleNumber));
								break;
							case "keyword":
								list.Add(new TextFormatGroup(index, endIndex, textStyleKeyword));
								break;
							case "builtIn":
								list.Add(new TextFormatGroup(index, endIndex, textStyleBuiltIn));
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
