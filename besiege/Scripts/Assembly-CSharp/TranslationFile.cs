using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class TranslationFile
{
	private const string HeaderText = "//Language File. Any line starting with '//' will be ignored.\r\n//\r\n//At each value replace the text between the quotes to change the in-game text.\r\n//Leave the number in place, as that is how Besiege knows which text to replace.\r\n//\r\n//'----' Denotes a different scene.\r\n//'--' Denotes a section in a scene.\r\n// No dashes denotes the English text.\r\n// Text within '[' and ']' in a comment is a note about that comment.\r\n// - Any comment that specifies a required word or phrase must include that in the final translation. If it is not included, the English text will be used. (See #1 for an example.)\r\n//\r\n// Including '\\n' in a translation will instead write a new line, in game. (See #37 for an example.)\r\n//\r\n// TIP: Load this in Notepad++ (free) and set the 'Language' on the top bar to 'C#'- this will nicely highlight the format of this file. \r\n\r\n//This line is required to detect that it is a language file. Please leave untouched.\r\n[Besiege Language File]\r\n//The details about the language. Both are required. \r\n//The systemLanguage needs to be one of the following list so it can be recognised from the system language: https://docs.unity3d.com/540/Documentation/ScriptReference/SystemLanguage.html\r\nsystemLanguage = \"{0}\"\r\nlanguageName = \"{1}\"\r\n\r\n//This line is required to know when the body of the translations starts. Please leave untouched.\r\n[Begin Translations]";

	private string filePath;

	private string fileName;

	private string languageName = string.Empty;

	private string systemLanguage = string.Empty;

	private Regex langAbbriviationRegex = new Regex("systemLanguage = \"([A-Za-z]+)\"", RegexOptions.Compiled);

	private Regex langNameRegex = new Regex("languageName = \"(.*)\"", RegexOptions.Compiled);

	private Regex localisationRegex = new Regex("\\/\\/\\s*(.*)\r?\n([0-9]+) = \"(.*)\"", RegexOptions.Compiled);

	private List<TranslationEntry> translationEntries;

	private Dictionary<int, int> lookupDictionary = new Dictionary<int, int>();

	private List<TranslationEntry> duplicateEntries;

	public string SystemLanguage
	{
		get
		{
			return systemLanguage;
		}
	}

	public string LanguageName
	{
		get
		{
			return languageName;
		}
	}

	public string FileName
	{
		get
		{
			return fileName;
		}
	}

	public IEnumerable<int> TranslationIds
	{
		get
		{
			return lookupDictionary.Keys;
		}
	}

	public static TranslationFile CreateDummyFile()
	{
		TranslationFile translationFile = new TranslationFile();
		translationFile.languageName = "English";
		translationFile.systemLanguage = "English";
		translationFile.filePath = "EnglishDummy.txt";
		translationFile.fileName = "EnglishDummy";
		return translationFile;
	}

	public void Load(string filePath)
	{
		this.filePath = filePath;
		fileName = Path.GetFileName(filePath);
		string fileContent = File.ReadAllText(filePath).Replace("\r\n", "\n");
		LoadFromString(fileContent);
	}

	public void LoadFromString(string fileContent, string filePath)
	{
		this.filePath = filePath;
		fileName = Path.GetFileName(filePath);
		LoadFromString(fileContent);
	}

	public int GetTranslationID(string translation)
	{
		if (string.IsNullOrEmpty(translation))
		{
			return -1;
		}
		TranslationEntry translationEntry = null;
		try
		{
			translationEntry = translationEntries.Where((TranslationEntry x) => x.Translation.Equals(translation)).FirstOrDefault();
		}
		catch (Exception)
		{
		}
		if (translationEntry == null)
		{
			return -1;
		}
		return translationEntry.TranslationID;
	}

	public bool ContainsTranslation(int id)
	{
		return lookupDictionary.ContainsKey(id);
	}

	public string GetTranslationString(int id)
	{
		return GetTranslation(id).Translation;
	}

	public void SetTranslationString(int id, string translation)
	{
		GetTranslation(id).Translation = translation;
	}

	public TranslationEntry GetTranslation(int id)
	{
		return translationEntries[lookupDictionary[id]];
	}

	public Dictionary<string, List<int>> FindDoubleTranslations()
	{
		Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
		foreach (TranslationEntry translationEntry in translationEntries)
		{
			if (!dictionary.ContainsKey(translationEntry.Translation))
			{
				dictionary.Add(translationEntry.Translation, new List<int>());
			}
			dictionary[translationEntry.Translation].Add(translationEntry.TranslationID);
		}
		return dictionary;
	}

	public void Save(string manualPathOverride = "")
	{
		if (manualPathOverride != string.Empty)
		{
			SaveFile(manualPathOverride);
		}
		else
		{
			SaveFile(filePath);
		}
	}

	public void AddTranslation(TranslationEntry newTranslationEntry)
	{
		if (lookupDictionary.ContainsKey(newTranslationEntry.TranslationID))
		{
			TranslationEntry translation = GetTranslation(newTranslationEntry.TranslationID);
			if (translation.Translation.Equals(newTranslationEntry.Translation))
			{
				duplicateEntries.Add(newTranslationEntry);
			}
			else
			{
				translation.Translation = newTranslationEntry.Translation;
			}
		}
		else
		{
			lookupDictionary.Add(newTranslationEntry.TranslationID, translationEntries.Count);
			translationEntries.Add(newTranslationEntry);
		}
	}

	public void RemoveDuplicateTranslations(List<int> duplicateList)
	{
		int translationId;
		foreach (int duplicate in duplicateList)
		{
			translationId = duplicate;
			if (lookupDictionary.ContainsKey(translationId))
			{
				lookupDictionary.Remove(translationId);
			}
			translationEntries.RemoveAll((TranslationEntry x) => x.TranslationID == translationId);
		}
	}

	private void LoadFromString(string fileContent)
	{
		translationEntries = new List<TranslationEntry>();
		duplicateEntries = new List<TranslationEntry>();
		LoadHeader(fileContent);
		LoadTranslations(fileContent);
	}

	private void LoadTranslations(string fileContent)
	{
		MatchCollection matchCollection = localisationRegex.Matches(fileContent);
		foreach (Match item in matchCollection)
		{
			if (item.Groups.Count != 4)
			{
				Console.WriteLine("Could not parse line, number of groups don't match: " + item.Groups.Count);
				continue;
			}
			int result = 0;
			if (!int.TryParse(item.Groups[2].Value, out result))
			{
				Console.WriteLine("Could not parse translation id: " + item.Groups[2].Value);
				continue;
			}
			TranslationEntry translationEntry = new TranslationEntry();
			translationEntry.TranslationID = result;
			translationEntry.Comment = item.Groups[1].Value.Replace("\r", string.Empty);
			translationEntry.Translation = item.Groups[3].Value;
			TranslationEntry newTranslationEntry = translationEntry;
			AddTranslation(newTranslationEntry);
		}
		if (duplicateEntries.Count > 0)
		{
			Console.WriteLine("Found " + duplicateEntries.Count + " double entries");
		}
	}

	private void LoadHeader(string fileContent)
	{
		string[] array = fileContent.Split('\n');
		string[] array2 = array;
		foreach (string input in array2)
		{
			if (!string.IsNullOrEmpty(systemLanguage) && !string.IsNullOrEmpty(languageName))
			{
				break;
			}
			Match match = langAbbriviationRegex.Match(input);
			if (match.Success)
			{
				systemLanguage = match.Groups[1].Value;
				continue;
			}
			Match match2 = langNameRegex.Match(input);
			if (match2.Success)
			{
				languageName = match2.Groups[1].Value;
			}
		}
	}

	private void SaveFile(string translationFilePath)
	{
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(translationFilePath, FileMode.Truncate, FileAccess.Write), Encoding.UTF8))
		{
			streamWriter.NewLine = "\r\n";
			streamWriter.WriteLine(string.Format("//Language File. Any line starting with '//' will be ignored.\r\n//\r\n//At each value replace the text between the quotes to change the in-game text.\r\n//Leave the number in place, as that is how Besiege knows which text to replace.\r\n//\r\n//'----' Denotes a different scene.\r\n//'--' Denotes a section in a scene.\r\n// No dashes denotes the English text.\r\n// Text within '[' and ']' in a comment is a note about that comment.\r\n// - Any comment that specifies a required word or phrase must include that in the final translation. If it is not included, the English text will be used. (See #1 for an example.)\r\n//\r\n// Including '\\n' in a translation will instead write a new line, in game. (See #37 for an example.)\r\n//\r\n// TIP: Load this in Notepad++ (free) and set the 'Language' on the top bar to 'C#'- this will nicely highlight the format of this file. \r\n\r\n//This line is required to detect that it is a language file. Please leave untouched.\r\n[Besiege Language File]\r\n//The details about the language. Both are required. \r\n//The systemLanguage needs to be one of the following list so it can be recognised from the system language: https://docs.unity3d.com/540/Documentation/ScriptReference/SystemLanguage.html\r\nsystemLanguage = \"{0}\"\r\nlanguageName = \"{1}\"\r\n\r\n//This line is required to know when the body of the translations starts. Please leave untouched.\r\n[Begin Translations]", SystemLanguage, LanguageName));
			streamWriter.WriteLine();
			foreach (TranslationEntry translationEntry in translationEntries)
			{
				streamWriter.WriteLine(string.Format("// {0}", translationEntry.Comment));
				streamWriter.WriteLine(string.Format("{0} = \"{1}\"", translationEntry.TranslationID, translationEntry.Translation));
				streamWriter.WriteLine();
			}
		}
	}
}
