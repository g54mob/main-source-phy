using System;
using UnityEngine;

[Serializable]
public class CreditInstance
{
	public enum CreditType
	{
		Person = 0,
		Header = 1,
		Spacing = 2,
		Logo = 3
	}

	[HideInInspector]
	public float itemSize = 200f;

	public CreditType creditType;

	public string text;

	public string subText;

	public float spacingSize = 500f;

	public bool localize;

	public Sprite logo;
}
