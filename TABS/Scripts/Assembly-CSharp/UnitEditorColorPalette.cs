using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Unit Editor/Color Palette", order = 2)]
public class UnitEditorColorPalette : ScriptableObject
{
	[Serializable]
	public struct ParentCatagories
	{
		public string name;

		public ColorPaletteCatagory[] colorPaletteCatagories;

		public Sprite colorWheelSprite;
	}

	[Serializable]
	public struct ColorPaletteCatagory
	{
		public enum CatagoryType
		{
			NormalColors = 0,
			TeamColors = 1
		}

		public string name;

		public CatagoryType Cataogry;

		public ColorPaletteData[] Colors;

		public TeamColorPaletteData[] TeamColors;

		public Sprite shardImage;

		public bool ShowNormalColors()
		{
			return Cataogry == CatagoryType.NormalColors;
		}

		public bool ShowTeamColors()
		{
			return Cataogry == CatagoryType.TeamColors;
		}
	}

	[SerializeField]
	private ParentCatagories[] m_ColorPalleteParentCatagories;

	private ColorPaletteData[] m_colors;

	[SerializeField]
	private TeamColorPaletteData[] m_teamColors;

	[SerializeField]
	private Material m_highlightMaterial;

	private bool m_initialized;

	public ColorPaletteData[] Colors
	{
		get
		{
			if (m_colors.Length == 0 || !Application.isPlaying)
			{
				Initialize();
			}
			return m_colors;
		}
	}

	public TeamColorPaletteData[] TeamColors
	{
		get
		{
			if (m_colors == null || m_colors.Length == 0)
			{
				Initialize();
			}
			return m_teamColors;
		}
	}

	public ParentCatagories[] ColorPaletteParentCatagories
	{
		get
		{
			if (m_colors == null || m_colors.Length == 0)
			{
				Initialize();
			}
			return m_ColorPalleteParentCatagories;
		}
	}

	public Material HighlightMaterial => m_highlightMaterial;

	public void SetNewColors(Color[] colors)
	{
		m_colors = new ColorPaletteData[colors.Length];
		for (int i = 0; i < colors.Length; i++)
		{
			m_colors[i] = new ColorPaletteData
			{
				ColorIndex = i,
				m_color = colors[i]
			};
		}
	}

	public void Initialize()
	{
		List<ColorPaletteData> list = new List<ColorPaletteData>();
		List<TeamColorPaletteData> list2 = new List<TeamColorPaletteData>();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < m_ColorPalleteParentCatagories.Length; i++)
		{
			for (int j = 0; j < m_ColorPalleteParentCatagories[i].colorPaletteCatagories.Length; j++)
			{
				if (m_ColorPalleteParentCatagories[i].colorPaletteCatagories[j].Cataogry == ColorPaletteCatagory.CatagoryType.NormalColors)
				{
					for (int k = 0; k < m_ColorPalleteParentCatagories[i].colorPaletteCatagories[j].Colors.Length; k++)
					{
						m_ColorPalleteParentCatagories[i].colorPaletteCatagories[j].Colors[k].ColorIndex = num;
						list.Add(m_ColorPalleteParentCatagories[i].colorPaletteCatagories[j].Colors[k]);
						num++;
					}
				}
				else
				{
					for (int l = 0; l < m_ColorPalleteParentCatagories[i].colorPaletteCatagories[j].TeamColors.Length; l++)
					{
						m_ColorPalleteParentCatagories[i].colorPaletteCatagories[j].TeamColors[l].ColorIndex = num2;
						list2.Add(m_ColorPalleteParentCatagories[i].colorPaletteCatagories[j].TeamColors[l]);
						num2++;
					}
				}
			}
		}
		m_colors = list.ToArray();
		m_teamColors = list2.ToArray();
		Debug.Log(base.name + " initialized. Colors: " + m_colors.Length);
		m_initialized = true;
	}
}
