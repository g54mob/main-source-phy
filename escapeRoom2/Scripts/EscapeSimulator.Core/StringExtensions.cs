using System;
using System.Text;
using UnityEngine;

public static class StringExtensions
{
	public static string Colored(this string @string, Color color)
	{
		string text = ColorUtility.ToHtmlStringRGB(color);
		return "<color=#" + text + ">" + @string + "</color>";
	}

	public static string SplitPascalCase(this string @string)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (char c in @string)
		{
			if (char.IsUpper(c))
			{
				stringBuilder.Append(" ");
			}
			stringBuilder.Append(c);
		}
		if (@string.Length > 0 && char.IsUpper(@string[0]))
		{
			stringBuilder.Remove(0, 1);
		}
		return stringBuilder.ToString();
	}

	public static bool IsPascalCased(this string @string)
	{
		string[] array = @string.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		foreach (string text in array)
		{
			if (text.Length == 0)
			{
				continue;
			}
			if (!char.IsUpper(text[0]))
			{
				return false;
			}
			for (int j = 1; j < text.Length; j++)
			{
				if (!char.IsLower(text[j]))
				{
					return false;
				}
			}
		}
		return true;
	}
}
