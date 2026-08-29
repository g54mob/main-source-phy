using System.Collections.Generic;

public static class SteamLocalization
{
	public static List<Language> availableLanguages = new List<Language>();

	private static bool initedSteamLanguages = false;

	public static void init()
	{
		if (initedSteamLanguages)
		{
			return;
		}
		initedSteamLanguages = true;
		foreach (Localization.LocalizedLanguage allLanguage in Localization.allLanguages)
		{
			availableLanguages.Add(allLanguage.systemLanguage);
		}
	}
}
