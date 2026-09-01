using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using mattmc3.dotmore.Text.RegularExpressions;

namespace mattmc3.dotmore.Extensions
{
	public static class StringExtensions
	{
		private static Regex s_regexNormalizeSpace;

		public static string Slice(this string s, int startIndex, int? endIndex = null)
		{
			if (s == null)
			{
				return null;
			}
			int num = startIndex;
			int num2 = ((!endIndex.HasValue) ? s.Length : endIndex.Value);
			if (num < 0)
			{
				num = s.Length + startIndex;
			}
			if (num2 < 0)
			{
				num2 = s.Length + num2;
			}
			if (num < 0)
			{
				num = 0;
			}
			if (num > s.Length)
			{
				num = s.Length;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > s.Length)
			{
				num2 = s.Length;
			}
			int num3 = num2 - num;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num + num3 > s.Length)
			{
				return s.Substring(num);
			}
			return s.Substring(num, num3);
		}

		public static string SubstringBefore(this string s, string innerString)
		{
			if (s == null)
			{
				return null;
			}
			int num = s.IndexOf(innerString);
			return (num >= 0) ? s.Substring(0, num) : null;
		}

		public static string SubstringAfter(this string s, string innerString)
		{
			if (s == null)
			{
				return null;
			}
			int num = s.IndexOf(innerString);
			return (num >= 0) ? s.Substring(num + 1) : null;
		}

		public static string Repeat(this string s, int repetitions)
		{
			if (s == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < repetitions; i++)
			{
				stringBuilder.Append(s);
			}
			return stringBuilder.ToString();
		}

		public static string FormatWith(this string s, params object[] args)
		{
			if (s == null)
			{
				return null;
			}
			return string.Format(s, args);
		}

		public static string FormatWith(this string s, IFormatProvider provider, params object[] args)
		{
			if (s == null)
			{
				return null;
			}
			return string.Format(provider, s, args);
		}

		public static string ToCSharpEscapedString(this string s)
		{
			string text = s;
			text = text.Replace("\\", "\\\\");
			text = text.Replace("\"", "\\\"");
			text = text.Replace("\0", "\\0");
			text = text.Replace("\a", "\\a");
			text = text.Replace("\b", "\\b");
			text = text.Replace("\f", "\\f");
			text = text.Replace("\n", "\\n");
			text = text.Replace("\r", "\\r");
			text = text.Replace("\t", "\\t");
			return text.Replace("\v", "\\v");
		}

		public static string Reverse(this string input)
		{
			char[] array = input.ToCharArray();
			Array.Reverse(array);
			return new string(array);
		}

		public static bool IsNullOrWhitespace(this string input)
		{
			return IsNullOrWhiteSpace(input);
		}

		public static bool IsNullOrWhiteSpace(string value)
		{
			if (value != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					if (!char.IsWhiteSpace(value[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		public static string[] SplitLines(this string that)
		{
			return that.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
		}

		public static string Translate(this string str, string translationFromString, string translationToString)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}
			if (translationFromString == null)
			{
				throw new ArgumentNullException("translationFromString");
			}
			if (translationToString == null)
			{
				throw new ArgumentNullException("translationToString");
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char value in str)
			{
				int num = translationFromString.IndexOf(value);
				if (num < 0)
				{
					stringBuilder.Append(value);
				}
				else if (num < translationToString.Length)
				{
					stringBuilder.Append(translationToString[num]);
				}
			}
			return stringBuilder.ToString();
		}

		public static int[] AllIndexesOf(this string baseString, string searchString)
		{
			List<int> list = new List<int>();
			int startIndex = 0;
			while (true)
			{
				int num = baseString.IndexOf(searchString, startIndex);
				if (num < 0)
				{
					break;
				}
				list.Add(num);
				startIndex = num + 1;
			}
			return list.ToArray();
		}

		public static int IndexOfAny(this string baseString, string[] anyOf)
		{
			int num = -1;
			foreach (string value in anyOf)
			{
				num = baseString.IndexOf(value);
				if (num >= 0)
				{
					return num;
				}
			}
			return num;
		}

		public static string Replace(this string s, string oldValue, string newValue, StringComparison comparisonType)
		{
			if (s == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(oldValue) || newValue == null)
			{
				return s;
			}
			int num = s.IndexOf(oldValue, comparisonType);
			if (num < 0)
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = oldValue.Length;
			int num2 = 0;
			while (num >= 0)
			{
				stringBuilder.Append(s, num2, num - num2);
				stringBuilder.Append(newValue);
				num2 = num + length;
				num = s.IndexOf(oldValue, num2, comparisonType);
			}
			stringBuilder.Append(s, num2, s.Length - num2);
			return stringBuilder.ToString();
		}

		public static string Left(this string s, int length)
		{
			if (s == null)
			{
				return null;
			}
			if (length > s.Length)
			{
				length = s.Length;
			}
			else if (length < 0)
			{
				length = 0;
			}
			return s.Substring(0, length);
		}

		public static string Right(this string s, int length)
		{
			if (s == null)
			{
				return null;
			}
			if (length > s.Length)
			{
				length = s.Length;
			}
			else if (length < 0)
			{
				length = 0;
			}
			return s.Substring(s.Length - length, length);
		}

		public static string Substring(this string s, int startIndex, int length, bool neverFail)
		{
			if (!neverFail)
			{
				return s.Substring(startIndex, length);
			}
			startIndex = startIndex.ConstrainBetween(0, s.Length);
			if (length < 0)
			{
				length = 0;
			}
			if (startIndex + length > s.Length)
			{
				length = s.Length - startIndex;
			}
			return s.Substring(startIndex, length);
		}

		public static string Truncate(this string s, int maxLength, string truncationSuffix = "...")
		{
			if (s == null)
			{
				return null;
			}
			if (maxLength < 0)
			{
				return string.Empty;
			}
			if (s.Length <= maxLength)
			{
				return s;
			}
			if (maxLength < truncationSuffix.Length)
			{
				return s.Left(maxLength);
			}
			return s.Left(maxLength - truncationSuffix.Length) + truncationSuffix;
		}

		public static bool Contains(this string s, string value, StringComparison comparisonType)
		{
			return s.IndexOf(value, comparisonType) >= 0;
		}

		public static bool Contains(this string s, char[] values, StringComparison comparisonType)
		{
			return s.Contains(values.Select((char c) => new string(c, 1)).ToArray(), comparisonType);
		}

		public static bool Contains(this string s, string[] values)
		{
			return s.Contains(values, StringComparison.Ordinal);
		}

		public static bool Contains(this string s, string[] values, StringComparison comparisonType)
		{
			if (s == null)
			{
				return false;
			}
			bool flag = false;
			foreach (string text in values)
			{
				if (text != null && s.IndexOf(text, comparisonType) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		public static string NormalizeWhiteSpace(this string s)
		{
			if (s == null)
			{
				return null;
			}
			if (s_regexNormalizeSpace == null)
			{
				s_regexNormalizeSpace = new Regex("\\s+", RegexHelper.XmsOpts | RegexOptions.Compiled);
			}
			return s_regexNormalizeSpace.Replace(s.Trim(), " ");
		}

		public static string NormalizeNewlines(this string s)
		{
			if (s == null)
			{
				return null;
			}
			return s.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
		}

		public static string TrimEnd(this string s)
		{
			if (s == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(s);
			while (stringBuilder.Length > 0 && char.IsWhiteSpace(stringBuilder[stringBuilder.Length - 1]))
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		public static string TrimStart(this string s)
		{
			if (s == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(s);
			while (stringBuilder.Length > 0 && char.IsWhiteSpace(stringBuilder[0]))
			{
				stringBuilder.Remove(0, 1);
			}
			return stringBuilder.ToString();
		}

		public static bool IsWildcardMatch(this string s, string wildcardPattern)
		{
			if (string.IsNullOrEmpty(wildcardPattern))
			{
				return string.IsNullOrEmpty(s);
			}
			string pattern = RegexHelper.ConvertWildcardPatternToRegex(wildcardPattern);
			return Regex.IsMatch(s, pattern, RegexHelper.XmsiOpts);
		}

		public static string[] Chunk(this string s, int chunkSize)
		{
			List<string> list = new List<string>();
			int num = 0;
			while (s != null && num < s.Length)
			{
				int num2 = ((chunkSize >= s.Length - num) ? (s.Length - num) : chunkSize);
				list.Add(s.Substring(num, num2));
				num += num2;
			}
			return list.ToArray();
		}

		public static string MakeTitleCase(this string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			string input = textInfo.ToTitleCase(value.ToLower());
			MatchEvaluator evaluator = (Match m) => m.Groups[1].Value + m.Groups[2].Value.ToLower();
			return Regex.Replace(input, "([0-9])([A-Z])", evaluator);
		}
	}
}
