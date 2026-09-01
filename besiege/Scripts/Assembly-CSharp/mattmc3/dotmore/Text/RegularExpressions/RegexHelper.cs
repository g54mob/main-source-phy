using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace mattmc3.dotmore.Text.RegularExpressions
{
	public static class RegexHelper
	{
		private static readonly HashSet<char> s_needsEscaped;

		private static readonly Dictionary<char, string> s_translate;

		public static RegexOptions XmsOpts
		{
			get
			{
				return RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace;
			}
		}

		public static RegexOptions XmsiOpts
		{
			get
			{
				return XmsOpts | RegexOptions.IgnoreCase;
			}
		}

		static RegexHelper()
		{
			s_needsEscaped = new HashSet<char>();
			s_needsEscaped.Add(' ');
			s_needsEscaped.Add('{');
			s_needsEscaped.Add('}');
			s_needsEscaped.Add('[');
			s_needsEscaped.Add(']');
			s_needsEscaped.Add('(');
			s_needsEscaped.Add(')');
			s_needsEscaped.Add('<');
			s_needsEscaped.Add('>');
			s_needsEscaped.Add('.');
			s_needsEscaped.Add('/');
			s_needsEscaped.Add('\\');
			s_needsEscaped.Add('^');
			s_needsEscaped.Add('$');
			s_needsEscaped.Add('?');
			s_needsEscaped.Add('+');
			s_needsEscaped.Add('*');
			s_needsEscaped.Add('#');
			s_needsEscaped.Add('|');
			s_translate = new Dictionary<char, string>();
			s_translate.Add('\t', "\\t");
			s_translate.Add('\r', "\\r");
			s_translate.Add('\n', "\\n");
			s_translate.Add('\v', "\\v");
		}

		public static bool IsValidRegexPattern(string pattern)
		{
			return IsValidRegexPattern(pattern, XmsOpts);
		}

		public static bool IsValidRegexPattern(string pattern, RegexOptions opts)
		{
			bool result = true;
			try
			{
				new Regex(pattern, opts);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static string ConvertWildcardPatternToRegex(string wildcardPattern)
		{
			if (string.IsNullOrEmpty(wildcardPattern))
			{
				return string.Empty;
			}
			string[] array = wildcardPattern.Split('|');
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			stringBuilder.Append("^");
			string[] array2 = array;
			foreach (string str in array2)
			{
				string text = Regex.Escape(str);
				text = text.Replace("\\[!", "[^");
				text = text.Replace("\\[", "[");
				text = text.Replace("\\]", "]");
				text = text.Replace("\\?", ".");
				text = text.Replace("\\*", ".*");
				text = text.Replace("\\#", "\\d");
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append("|");
				}
				stringBuilder.Append("(");
				stringBuilder.Append(text);
				stringBuilder.Append(")");
			}
			stringBuilder.Append("$");
			string text2 = stringBuilder.ToString();
			if (!IsValidRegexPattern(text2))
			{
				throw new ArgumentException(string.Format("Invalid pattern: {0}", wildcardPattern));
			}
			return text2;
		}

		public static string ConvertSqlLikePatternToRegex(string sqlLikePattern)
		{
			string text = "^" + Regex.Escape(sqlLikePattern) + "$";
			text = text.Replace("\\[_\\]", "[_]");
			text = text.Replace("\\[\\[\\]", "\\[");
			text = text.Replace("\\[\\^", "[^");
			text = text.Replace("\\[", "[");
			text = text.Replace("\\]", "]");
			text = text.Replace("%", ".*?");
			text = text.Replace("_", ".");
			if (!IsValidRegexPattern(text))
			{
				throw new ArgumentException(string.Format("Invalid pattern: {0}", sqlLikePattern));
			}
			return text;
		}
	}
}
