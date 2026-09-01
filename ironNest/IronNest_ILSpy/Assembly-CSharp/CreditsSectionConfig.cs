using System;
using Localisation;
using UnityEngine;

[Serializable]
public class CreditsSectionConfig
{
	private TextIdentifier _titleLangKey;

	private string _titleOverride;

	private TextAsset _contentFile;

	public TextIdentifier TitleLangKey => _titleLangKey;

	public string TitleOverride => _titleOverride;

	public string ContentText
	{
		get
		{
			if ((object)_contentFile != null)
			{
				return _contentFile.text;
			}
			return (string)(object)new NullReferenceException();
		}
	}
}
