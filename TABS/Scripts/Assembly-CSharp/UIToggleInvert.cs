using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleInvert : MonoBehaviour
{
	private TextMeshProUGUI m_textMesh;

	private Toggle m_toggle;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		m_toggle = GetComponent<Toggle>();
		m_textMesh = GetComponentInChildren<TextMeshProUGUI>();
		if (m_toggle != null)
		{
			m_toggle.onValueChanged.AddListener(OnToggleValueChanged);
			OnToggleValueChanged(m_toggle.isOn);
		}
	}

	public void OnToggleValueChanged(bool isOn)
	{
		if (m_toggle == null || m_textMesh == null)
		{
			Init();
		}
		else if (isOn)
		{
			if (m_toggle.graphic != null)
			{
				m_toggle.graphic.color = Color.white;
			}
			m_textMesh.color = Color.black;
		}
		else
		{
			if (m_toggle.graphic != null)
			{
				m_toggle.graphic.color = Color.black;
			}
			m_textMesh.color = Color.white;
		}
	}
}
