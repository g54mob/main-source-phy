using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIColorSetter : MonoBehaviour
{
	public UIColorPreset m_Preset;

	private Image m_Element;

	public void ApplyColor()
	{
		if ((bool)m_Element && (bool)m_Preset)
		{
			m_Element.color = m_Preset.m_Color;
		}
	}

	public void OverridePreset()
	{
		m_Preset.m_Color = m_Element.color;
	}

	private void OnEnable()
	{
		ApplyColor();
		if (m_Element == null)
		{
			m_Element = GetComponent<Image>();
		}
	}
}
