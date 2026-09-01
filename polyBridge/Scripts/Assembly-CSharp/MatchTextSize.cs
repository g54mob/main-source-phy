using TMPro;
using UnityEngine;

[ExecuteAlways]
public class MatchTextSize : MonoBehaviour
{
	public TMP_Text m_Master;

	public RectTransform m_Self;

	public int m_DeltaX;

	public int m_DeltaY;

	private void OnEnable()
	{
		Update();
	}

	private void Update()
	{
		if (m_Master != null && m_Self != null)
		{
			m_Self.sizeDelta = new Vector2(m_Master.renderedWidth + (float)m_DeltaX, m_Master.renderedHeight + (float)m_DeltaY);
		}
	}
}
