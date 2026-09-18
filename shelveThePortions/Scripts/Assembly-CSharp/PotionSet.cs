using System;
using UnityEngine;

[Serializable]
public class PotionSet
{
	[HideInInspector]
	public string name;

	public PrefabTypes bottlePrefabType;

	public SortingPuzzleType sortingPuzzleType;

	public void OnValidate()
	{
		name = bottlePrefabType.ToString() + " " + sortingPuzzleType;
	}
}
