using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVImporter
{
	private static string PATTERN = "([\\t,])";

	private static string SEPARATOR = "\n";

	public static Dictionary<string, string> Parse(string text)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		new Regex(PATTERN, RegexOptions.IgnoreCase);
		string[] array = Regex.Split(text, "\n\\s*");
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = Regex.Split(array[i], "\t\\s*");
			if (array2.Length > 1 && !array2[0].Contains("#"))
			{
				string text2 = array2[0].ToLower();
				text2 = text2.Trim();
				dictionary.Add(text2, array2[1]);
			}
		}
		return dictionary;
	}
}
