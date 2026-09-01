using UnityEngine;
using UnityEngine.EventSystems;

public class FactionButtonMover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public AnimationCurve m_HoverCurve;

	public float m_Factor;

	public RectTransform m_RectTransform;

	[SerializeField]
	protected bool m_UseUnscaledTime;

	private float m_curveTime;

	private Vector3 m_startPos;

	private float m_timer;

	private bool m_countDown = true;

	private void Start()
	{
		m_curveTime = m_HoverCurve.keys[m_HoverCurve.keys.Length - 1].time;
		m_startPos = m_RectTransform.anchoredPosition;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		MoveUp();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		MoveDown();
	}

	public void MoveUp()
	{
		m_countDown = false;
	}

	public void MoveDown()
	{
		m_countDown = true;
	}

	private void Update()
	{
		float num = ((!m_UseUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
		if (m_countDown)
		{
			if (m_timer > 0f)
			{
				m_timer -= num;
			}
		}
		else if (m_timer < m_curveTime)
		{
			m_timer += num;
		}
		m_RectTransform.anchoredPosition = m_startPos + Vector3.up * m_HoverCurve.Evaluate(m_timer) * m_Factor;
	}
}
