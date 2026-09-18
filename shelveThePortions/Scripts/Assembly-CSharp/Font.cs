using System;
using TMPro;
using UnityEngine;

[Serializable]
public class Font
{
	[HideInInspector]
	public string name;

	public Languages language;

	public TMP_FontAsset languageFont;

	public void OnValidate()
	{
		name = language.ToString();
	}
}
