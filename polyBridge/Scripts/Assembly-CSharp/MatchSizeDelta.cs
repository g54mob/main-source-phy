using UnityEngine;

[ExecuteAlways]
public class MatchSizeDelta : MonoBehaviour
{
	public RectTransform m_Master;

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
			m_Self.sizeDelta = new Vector2(m_Master.sizeDelta.x + (float)m_DeltaX, m_Master.sizeDelta.y + (float)m_DeltaY);
		}
	}
}
