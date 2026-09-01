using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ZorrosColorDatabase", menuName = "Landfall/Zorro/ColorDatabase", order = 9999999)]
public class ZorrosColorDatabase : ScriptableObject
{
	[Serializable]
	public struct ColorCatagory
	{
		public string Name;

		public ColorWrapper[] Colors;
	}

	[Serializable]
	public struct ColorWrapper
	{
		public Color color;

		public string rating;
	}

	public ColorCatagory[] ColorCatagories;
}
