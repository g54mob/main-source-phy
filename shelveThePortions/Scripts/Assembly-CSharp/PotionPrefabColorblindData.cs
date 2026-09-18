using System;
using UnityEngine;

[Serializable]
public class PotionPrefabColorblindData
{
	[HideInInspector]
	public string name;

	public PrefabTypes bottlePrefabType;

	public float patternCount;

	public float patternSpacing;

	public Vector4 offset;

	public void OnValidate()
	{
		name = bottlePrefabType.ToString();
	}
}
