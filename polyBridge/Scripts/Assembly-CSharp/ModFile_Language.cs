using System.IO;
using System.Text;
using I2.Loc;

public class ModFile_Language
{
	public static void CreateTemplateCSV(string dirPath)
	{
		string languageName = Localize.GetLanguageName(Profiles.m_ActiveProfile.m_LanguageCode);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("Keys,Notes," + languageName + ",LANGUAGE_NAME_HERE");
		foreach (TermData mTerm in LocalizationManager.Sources[0].mTerms)
		{
			string text = LocalizationManager.Sources[0].GetTranslation(mTerm.Term, "Notes").Replace("\"", "\"\"");
			string text2 = Localize.Get(mTerm.Term).Replace("\"", "\"\"");
			stringBuilder.AppendLine(mTerm.Term + ",\"" + text + "\",\"" + text2 + "\",");
		}
		File.WriteAllText(Path.Combine(dirPath, "templateCSV.csv"), stringBuilder.ToString());
	}
}
