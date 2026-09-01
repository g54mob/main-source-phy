using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorSettingsButton : EditorSettingsButton
{
	[SerializeField]
	private Image m_color;

	public Action m_colorCallback;

	protected override void Awake()
	{
		base.Awake();
		Button componentInChildren = GetComponentInChildren<Button>();
		if (componentInChildren != null)
		{
			componentInChildren.onClick.AddListener(delegate
			{
				m_colorCallback();
			});
		}
	}

	public void SetColor(Color c)
	{
		if (!(m_color == null))
		{
			m_color.color = c;
		}
	}

	public void AddButtonCallback(Action callback)
	{
		m_colorCallback = callback;
	}
}
