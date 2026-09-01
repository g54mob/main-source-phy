using System;
using UnityEngine;

[Serializable]
public class UnitColorInstance
{
	public string colorName;

	public Color color;

	[Range(0f, 1f)]
	public float currentValue;

	public bool acceptWhenDead;
}
