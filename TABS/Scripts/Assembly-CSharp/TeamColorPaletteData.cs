using System;
using Landfall.TABS;
using UnityEngine;

[Serializable]
public struct TeamColorPaletteData
{
	public Color m_colorRed;

	public Material m_materialRed;

	public Color m_colorBlue;

	public Material m_materialBlue;

	public int ColorIndex;

	public Color GetColor(Team team)
	{
		switch (team)
		{
		case Team.Red:
			return m_colorRed;
		case Team.Blue:
			return m_colorBlue;
		default:
			return Color.magenta;
		}
	}

	public Material GetMaterial(Team team)
	{
		switch (team)
		{
		case Team.Red:
			return m_materialRed;
		case Team.Blue:
			return m_materialBlue;
		default:
			return null;
		}
	}
}
