using System;
using Localisation;
using UnityEngine;

[Serializable]
public class CreditsSectionConfig
{
	[SerializeField]
	private TextIdentifier _titleLangKey;

	[SerializeField]
	[Tooltip("If not empty, it will be used instead of translation key.")]
	private string _titleOverride;

	[SerializeField]
	private TextAsset _contentFile;

	public TextIdentifier TitleLangKey => null;

	public string TitleOverride => null;

	public string ContentText => null;
}
