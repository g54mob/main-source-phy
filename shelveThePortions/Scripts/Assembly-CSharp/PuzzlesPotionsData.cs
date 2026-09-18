using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PuzzlesPotionsData
{
	[HideInInspector]
	public string name;

	public SortingPuzzleType sortingPuzzleType;

	public List<PrefabTypes> potionsPrefabTypes;

	public int bottlesInSet;

	public void OnValidate()
	{
		name = sortingPuzzleType.ToString();
	}
}
