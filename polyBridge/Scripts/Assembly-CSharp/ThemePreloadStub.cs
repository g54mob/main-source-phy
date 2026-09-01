using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ThemePreloadStub", menuName = "Game/ThemePreloadStub", order = 2)]
public class ThemePreloadStub : ScriptableObject
{
	public string m_ID;

	public string m_DisplayNameLocID;

	public string m_StubPrefabAddress;

	public bool m_ExcludeInRelease;

	[Header("World Selection")]
	public Sprite m_Icon;

	public Sprite m_IconSelected;

	public Sprite m_IconSilouette;

	[NonSerialized]
	public ThemeSkyOverride m_ThemeSkyOverride;

	public void SetSkyOverride(string top, string middle, string bottom, float middleOffset)
	{
		ColorUtility.TryParseHtmlString(top, out var color);
		ColorUtility.TryParseHtmlString(middle, out var color2);
		ColorUtility.TryParseHtmlString(bottom, out var color3);
		middleOffset = Mathf.Clamp(middleOffset, 0.001f, 0.999f);
		if (m_ThemeSkyOverride == null)
		{
			m_ThemeSkyOverride = new ThemeSkyOverride(m_ID, color, color2, color3, middleOffset);
		}
		else
		{
			m_ThemeSkyOverride.Set(m_ID, color, color2, color3, middleOffset);
		}
	}
}
