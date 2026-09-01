using System;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationSettings", menuName = "TABS/LocalizationSettings", order = 2)]
public class LocalizationSettings : ScriptableObject
{
	[Serializable]
	private class LanguageSettings
	{
		public Localizer.Language m_language;

		public TMP_FontAsset[] m_fonts;
	}

	[SerializeField]
	private TMP_FontAsset m_defaultFont;

	[SerializeField]
	private LanguageSettings[] m_languageSettings;

	public TMP_FontAsset GetFont(Localizer.Language lang, int fontIndex = 0)
	{
		if (m_languageSettings == null)
		{
			return m_defaultFont;
		}
		int num = m_languageSettings.Length;
		for (int i = 0; i < num; i++)
		{
			if (m_languageSettings[i].m_language == lang)
			{
				TMP_FontAsset[] fonts = m_languageSettings[i].m_fonts;
				if (fonts == null || fonts.Length < 1)
				{
					return m_defaultFont;
				}
				fontIndex = Mathf.Min(fontIndex, fonts.Length - 1);
				return fonts[fontIndex];
			}
		}
		return m_defaultFont;
	}
}
