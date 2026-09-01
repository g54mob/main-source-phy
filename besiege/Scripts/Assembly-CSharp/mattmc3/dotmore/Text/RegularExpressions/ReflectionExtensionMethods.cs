using System.Text.RegularExpressions;

namespace mattmc3.dotmore.Text.RegularExpressions
{
	public static class ReflectionExtensionMethods
	{
		public static RegexOptions ToRegexOptions(this string reOpts)
		{
			RegexOptions regexOptions = RegexOptions.None;
			char[] array = reOpts.ToLower().ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				switch (array[i])
				{
				case 'x':
					regexOptions |= RegexOptions.IgnorePatternWhitespace;
					break;
				case 'm':
					regexOptions |= RegexOptions.Multiline;
					break;
				case 's':
					regexOptions |= RegexOptions.Singleline;
					break;
				case 'i':
					regexOptions |= RegexOptions.IgnoreCase;
					break;
				case 'c':
					regexOptions |= RegexOptions.Compiled;
					break;
				}
			}
			return regexOptions;
		}

		public static string GetGroupMatch(this Regex re, string searchString, int matchGroupNumber)
		{
			Match match = re.Match(searchString);
			if (!match.Success)
			{
				return null;
			}
			return match.Groups[matchGroupNumber].Value;
		}

		public static string GetGroupMatch(this Regex re, string searchString, string matchGroupName)
		{
			Match match = re.Match(searchString);
			if (!match.Success)
			{
				return null;
			}
			return match.Groups[matchGroupName].Value;
		}

		public static string ReplaceSubstringMatch(this Regex re, string searchString, string replacementString, int matchGroupNumber)
		{
			Match match = re.Match(searchString);
			if (!match.Success)
			{
				return searchString;
			}
			Group obj = match.Groups[matchGroupNumber];
			return searchString.Substring(0, obj.Index) + replacementString + searchString.Substring(obj.Index + obj.Length);
		}
	}
}
