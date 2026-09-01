using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public AnimationCurve m_hoverCurve;

	public float m_ampFactor = 1f;

	public float m_freqFactor = 1f;

	private float m_hoverTimer;

	private bool m_shouldHover;

	private RectTransform m_rectTransform;

	private Vector2 m_startAnchoredPosition;

	private void Start()
	{
		m_rectTransform = GetComponent<RectTransform>();
		m_startAnchoredPosition = m_rectTransform.anchoredPosition;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_shouldHover = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_shouldHover = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		m_shouldHover = false;
	}

	private void Update()
	{
		if (m_shouldHover)
		{
			float time = m_hoverCurve.keys[m_hoverCurve.keys.Length - 1].time;
			m_hoverTimer += Time.unscaledDeltaTime * m_freqFactor;
			if (time < m_hoverTimer)
			{
				m_hoverTimer = time;
			}
		}
		else
		{
			m_hoverTimer -= Time.unscaledDeltaTime * m_freqFactor;
			if (m_hoverTimer < 0f)
			{
				m_hoverTimer = 0f;
			}
		}
		m_rectTransform.anchoredPosition = m_startAnchoredPosition + Vector2.up * m_hoverCurve.Evaluate(m_hoverTimer) * m_ampFactor;
	}
}
