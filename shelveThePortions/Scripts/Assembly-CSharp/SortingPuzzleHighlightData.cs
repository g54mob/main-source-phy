using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SortingPuzzleHighlightData
{
	[HideInInspector]
	public string name;

	public SortingPuzzleType sortingPuzzleType;

	public List<GameObject> highlightableGameObjectsList = new List<GameObject>();

	public void OnValidate()
	{
		name = sortingPuzzleType.ToString();
	}
}
