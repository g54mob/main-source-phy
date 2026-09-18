using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PotionCategory
{
	[HideInInspector]
	public string name;

	public PotionCategoryType potionCategoryType;

	public Color color;

	public Texture2D colorBlindSprite;

	public Color colorBlindSpriteColor = Color.white;

	public float colorBlindSpriteOpacity = 0.2f;

	public List<SortingPuzzleType> sortingPuzzleTypeList = new List<SortingPuzzleType>();

	public void OnValidate()
	{
		name = potionCategoryType.ToString();
	}
}
