using System.Text.RegularExpressions;

namespace Kamgam.SettingsGenerator;

public static class StringUtils
{
	public static string InsertSpaceBeforeUpperCase(string input)
	{
		if (!string.IsNullOrEmpty(input))
		{
			return Regex.Replace(input, "(?<![\\dA-Z])([A-Z])", " $1");
		}
		return input;
	}
}
