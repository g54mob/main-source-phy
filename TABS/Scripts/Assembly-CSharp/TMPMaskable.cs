using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TMPMaskable : MonoBehaviour
{
	[SerializeField]
	private bool m_maskable;

	private TMP_Text m_text;

	private void Start()
	{
		SetMaskable(m_maskable);
	}

	public void SetMaskable(bool maskable)
	{
		m_maskable = maskable;
		UpdateMasking();
	}

	public void UpdateMasking()
	{
		if (m_text == null)
		{
			m_text = GetComponent<TMP_Text>();
		}
		m_text.maskable = m_maskable;
		m_text.RecalculateClipping();
	}
}
