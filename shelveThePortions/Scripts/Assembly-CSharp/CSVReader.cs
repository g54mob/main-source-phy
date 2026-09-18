using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVReader : Singleton<CSVReader>
{
	[SerializeField]
	private TextAsset csvFile;

	public Dictionary<string, string> LoadData(int currentLanguageID)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		string[] array = csvFile.text.Split(new string[2] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
		int num = 0;
		string[] array2 = array;
		foreach (string text in array2)
		{
			string[] array3 = (from Match m in new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)").Matches(text)
				select m.Value).ToArray();
			int num2 = Enum.GetNames(typeof(Languages)).Length - 1;
			if (array3.Length <= currentLanguageID || array3.Length != num2 + 1)
			{
				Debug.LogError(num + ": parsing failure, more content than number of languages currentLanguageID:" + currentLanguageID + " languageCount:" + num2 + "  matches.Length: " + array3.Length + "\n" + text);
			}
			else if (array3[0] != "")
			{
				if (!dictionary.ContainsKey(array3[0]))
				{
					string value = array3[currentLanguageID].Trim('"');
					dictionary.Add(array3[0].ToLower(), value);
				}
				else
				{
					Debug.LogError(array3[0] + " ID in Localization already exists before");
				}
			}
			num++;
		}
		return dictionary;
	}
}
