using System;
using Localisation;

[Serializable]
public class SettingLocalization
{
	private string _id;

	private TextIdentifier _langKey;

	public string Id => _id;

	public TextIdentifier LangKey => _langKey;
}
