using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PotionPuzzlesSprites
{
	[HideInInspector]
	public string name;

	public SortingPuzzleType sortingPuzzleType;

	public List<Sprite> puzzleSpritesList = new List<Sprite>();

	public void OnValidate()
	{
		name = sortingPuzzleType.ToString();
	}
}
