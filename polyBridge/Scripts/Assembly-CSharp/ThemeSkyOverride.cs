using UnityEngine;

public class ThemeSkyOverride
{
	public string m_ThemeID;

	public Color m_Top;

	public Color m_Middle;

	public Color m_Bottom;

	public float m_MiddleOffset;

	public ThemeSkyOverride(string id, Color top, Color middle, Color bottom, float middleOffset)
	{
		Set(id, top, middle, bottom, middleOffset);
	}

	public void Set(string id, Color top, Color middle, Color bottom, float middleOffset)
	{
		m_ThemeID = id;
		m_Top = top;
		m_Middle = middle;
		m_Bottom = bottom;
		m_MiddleOffset = middleOffset;
	}
}
