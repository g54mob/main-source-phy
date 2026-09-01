using UnityEngine;

public class DMDimensionImitator : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_target;

	[SerializeField]
	private bool m_followWidth = true;

	[SerializeField]
	private bool m_followHeight = true;

	private RectTransform m_rectTransform;

	private void Start()
	{
		m_rectTransform = GetComponent<RectTransform>();
	}

	private void Update()
	{
		Vector2 sizeDelta = m_target.sizeDelta;
		if (!m_followWidth)
		{
			sizeDelta.x = m_rectTransform.sizeDelta.x;
		}
		if (!m_followHeight)
		{
			sizeDelta.y = m_rectTransform.sizeDelta.y;
		}
		m_rectTransform.sizeDelta = sizeDelta;
	}
}
