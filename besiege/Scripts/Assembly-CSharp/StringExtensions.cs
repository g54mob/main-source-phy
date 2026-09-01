using System.Collections.Generic;
using System.Text;

public static class StringExtensions
{
	public static string Truncate(this string value, int maxChars)
	{
		return (value.Length > maxChars) ? (value.Substring(0, maxChars) + "...") : value;
	}

	public static string TruncateFolderPath(this string folderPath, int maxFolderPathCharacters)
	{
		string text = folderPath;
		while (text.Length > maxFolderPathCharacters)
		{
			int num = text.Split(FileSystemPath.DirectorySeparator).Length - 1;
			if (num < 1)
			{
				break;
			}
			int num2 = text.IndexOf(FileSystemPath.DirectorySeparator);
			text = text.Substring(num2 + 1, text.Length - num2 - 1);
		}
		if (text.Length > maxFolderPathCharacters)
		{
			text = text.Truncate(maxFolderPathCharacters);
		}
		return text;
	}

	public static List<string> WordWrap(this string str, char splitChar, int width)
	{
		string[] array = Explode(str, new char[1] { splitChar });
		List<string> list = new List<string>();
		int num = 0;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			if (num + text.Length > width)
			{
				if (num > 0)
				{
					list.Add(stringBuilder.ToString());
					stringBuilder = new StringBuilder();
					num = 0;
				}
				text = text.TrimStart();
			}
			stringBuilder.Append(text);
			num += text.Length;
		}
		list.Add(stringBuilder.ToString());
		return list;
	}

	private static string[] Explode(string str, char[] splitChars)
	{
		List<string> list = new List<string>();
		int num = 0;
		while (true)
		{
			int num2 = str.IndexOfAny(splitChars, num);
			if (num2 == -1)
			{
				break;
			}
			string text = str.Substring(num, num2 - num);
			char c = str.Substring(num2, 1)[0];
			if (char.IsWhiteSpace(c))
			{
				list.Add(text);
				list.Add(c.ToString());
			}
			else
			{
				list.Add(text + c);
			}
			num = num2 + 1;
		}
		list.Add(str.Substring(num));
		return list.ToArray();
	}
}
