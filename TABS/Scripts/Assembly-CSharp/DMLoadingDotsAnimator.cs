using TMPro;
using UnityEngine;

public class DMLoadingDotsAnimator : MonoBehaviour
{
	[SerializeField]
	private float m_interval = 1f;

	private float m_timer;

	private TMP_Text m_tmpText;

	private void Start()
	{
		m_tmpText = GetComponent<TMP_Text>();
	}

	private void Update()
	{
		m_timer += Time.unscaledDeltaTime;
		m_timer %= m_interval;
		string text = "";
		float num = m_timer / m_interval;
		if (num > 0.75f)
		{
			text = "...";
		}
		else if (num > 0.5f)
		{
			text = "..";
		}
		else if (num > 0.25f)
		{
			text = ".";
		}
		if (m_tmpText != null)
		{
			m_tmpText.text = text;
		}
	}
}
