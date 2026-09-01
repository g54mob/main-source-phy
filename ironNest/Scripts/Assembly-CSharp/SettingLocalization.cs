using System;
using Localisation;
using UnityEngine;

[Serializable]
public class SettingLocalization
{
	[SerializeField]
	private string _id;

	[SerializeField]
	private TextIdentifier _langKey;

	public string Id => null;

	public TextIdentifier LangKey => null;
}
