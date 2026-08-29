using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InGameTextEditor.Format
{
	public class CSharpSyntaxHighlighter : TextFormatter
	{
		public TextStyle textStyleComment = new TextStyle(new Color(0.5f, 0.5f, 0.5f));

		public TextStyle textStyleString = new TextStyle(new Color(0.9f, 0.4f, 0.1f));

		public TextStyle textStyleNumber = new TextStyle(new Color(0.2f, 0.4f, 0.6f));

		public TextStyle textStyleKeyword = new TextStyle(new Color(0.2f, 0.6f, 0.6f));

		private readonly string[] keywords = new string[88]
		{
			"abstract", "add", "alias", "as", "base", "bool", "break", "byte", "case", "catch",
			"char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double",
			"else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
			"foreach", "get", "global", "goto", "if", "implicit", "in", "int", "interface", "internal",
			"is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override",
			"params", "partial", "private", "protected", "public", "readonly", "ref", "remove", "return", "sbyte",
			"sealed", "set", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this",
			"throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using",
			"value", "var", "virtual", "void", "volatile", "where", "while", "yield"
		};

		private bool initialized;

		private Regex regex;

		public override bool Initialized => initialized;

		public override void Init()
		{
			string text = "";
			text += "(?<multiLineCommentEnding>^\\u001B0.*?\\*/)";
			text += "|(?<multiLineCommentStarting>/\\*((?!\\*/).)*$)";
			text += "|(?<comment>/\\*((?!\\*/).)*\\*/)";
			text += "|(?<comment>//.*$?)";
			text += "|(?<string>\"(?:[^\"\\\\]|\\\\.)*[\"]?)";
			text += "|(?<verbatimStringStarting>@\"(?:[^\"\"]|\"\")*$)";
			text += "|(?<verbatimStringEnding>^\\u001B1(?:[^\"\"]|\"\")*[\"])";
			text += "|(?<verbatimString>@\"(?:[^\"\"]|\"\")*[\"])";
			text += "|(?<char>'(?:[^'\\\\]|\\\\.)*[']?)";
			text += "|(?<intBinary>\\b0b[01]+[lL]?)";
			text += "|(?<intHex>\\b0x[0-9a-fA-F]+[lL]?)";
			text += "|(?<float>\\b[0-9]+(\\.[0-9]+)?[dfDF])";
			text += "|(?<floatImplicit>\\b[0-9]*\\.[0-9]+[dfDF]?)";
			text += "|(?<intImplicit>\\b[0-9]+[lL]?)";
			text += "|(?<keyword>";
			for (int i = 0; i < keywords.Length; i++)
			{
				text = text + "\\b" + keywords[i] + "\\b" + ((i < keywords.Length - 1) ? "|" : "");
			}
			text += ")";
			regex = new Regex(text);
			initialized = true;
		}

		public override void OnLineChanged(Line line)
		{
			List<TextFormatGroup> list = new List<TextFormatGroup>();
			bool flag = line.PreviousLine != null && line.PreviousLine.GetProperty("endsWithMultiLineComment", defaultValue: false);
			bool flag2 = false;
			bool value = false;
			bool flag3 = line.PreviousLine != null && line.PreviousLine.GetProperty("endsWithVerbatimString", defaultValue: false);
			bool flag4 = false;
			bool value2 = false;
			if (line.Text.Length > 0)
			{
				MatchCollection matchCollection = null;
				matchCollection = (flag ? regex.Matches("\u001b0" + line.Text) : ((!flag3) ? regex.Matches(line.Text) : regex.Matches("\u001b1" + line.Text)));
				foreach (Match item in matchCollection)
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
							case "multiLineCommentStarting":
								if (!flag3 || flag4)
								{
									value = true;
									list.Add(new TextFormatGroup(index, endIndex, textStyleComment));
								}
								break;
							case "multiLineCommentEnding":
								flag2 = true;
								list.Add(new TextFormatGroup(index + 2, endIndex, textStyleComment));
								break;
							case "comment":
								list.Add(new TextFormatGroup(index, endIndex, textStyleComment));
								break;
							case "verbatimStringStarting":
								if (!flag || flag2)
								{
									value2 = true;
									list.Add(new TextFormatGroup(index, endIndex, textStyleString));
								}
								break;
							case "verbatimStringEnding":
								flag4 = true;
								list.Add(new TextFormatGroup(index + 2, endIndex, textStyleString));
								break;
							case "verbatimString":
								list.Add(new TextFormatGroup(index, endIndex, textStyleString));
								break;
							case "string":
							case "char":
								list.Add(new TextFormatGroup(index, endIndex, textStyleString));
								break;
							case "intBinary":
							case "intHex":
							case "float":
							case "floatImplicit":
							case "intImplicit":
								list.Add(new TextFormatGroup(index, endIndex, textStyleNumber));
								break;
							case "keyword":
								list.Add(new TextFormatGroup(index, endIndex, textStyleKeyword));
								break;
							}
						}
						num++;
					}
				}
				if (flag || flag3)
				{
					foreach (TextFormatGroup item2 in list)
					{
						item2.startIndex -= 2;
						item2.endIndex -= 2;
					}
				}
			}
			if (flag && !flag2)
			{
				list.Clear();
				if (line.Text.Length > 0)
				{
					list.Add(new TextFormatGroup(0, line.Text.Length - 1, textStyleComment));
				}
				line.SetProperty("endsWithMultiLineComment", value: true);
			}
			else
			{
				line.SetProperty("endsWithMultiLineComment", value);
			}
			if (flag3 && !flag4)
			{
				list.Clear();
				if (line.Text.Length > 0)
				{
					list.Add(new TextFormatGroup(0, line.Text.Length - 1, textStyleString));
				}
				line.SetProperty("endsWithVerbatimString", value: true);
			}
			else
			{
				line.SetProperty("endsWithVerbatimString", value2);
			}
			line.ApplyTextFormat(list);
		}
	}
}
