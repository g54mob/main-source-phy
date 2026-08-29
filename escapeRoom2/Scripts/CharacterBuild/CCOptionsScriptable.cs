using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CCOptions", menuName = "ES/CCOptions")]
public class CCOptionsScriptable : ScriptableObject
{
	[Serializable]
	public class CategoryData
	{
		public bool shouldDisplayName = true;

		public string id;

		public Vector2 cellSize = new Vector2(60f, 60f);

		public List<VariantData> variants;
	}

	[Serializable]
	public class VariantData
	{
		public int id;

		public Sprite sprite;

		public Sprite spriteFemale;

		public Color color;

		public int awardNeeded = -1;
	}

	[Serializable]
	public class SkinType
	{
		public CharacterBuild.SkinColor texture;

		public Color tint;

		public Color pickerColor;

		public float hardLight;
	}

	public Color[] hairColors;

	public SkinType[] skinTypes;

	public List<CategoryData> options;

	public VariantData getVariant(string groupId, string categoryId, int variantId)
	{
		return options.Find((CategoryData x) => x.id == categoryId)?.variants.Find((VariantData x) => x.id == variantId);
	}
}
