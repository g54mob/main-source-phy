using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TweenPosition : MonoBehaviour
{
	public Vector2 m_MoveToPos;

	public float m_Delay;

	public float m_Time;

	public iTween.EaseType m_EaseType;

	public iTween.LoopType m_LoopType;

	public bool m_PlayOnEnable;

	private RectTransform m_RectTransform;

	private Vector2 m_OriginalPos;

	private void Awake()
	{
		m_RectTransform = GetComponent<RectTransform>();
		m_OriginalPos = m_RectTransform.anchoredPosition;
	}

	private void OnEnable()
	{
		if (m_PlayOnEnable)
		{
			Play();
		}
	}

	public void Play()
	{
		iTween.ValueTo(base.gameObject, iTween.Hash("ignoretimescale", true, "from", m_OriginalPos, "to", m_MoveToPos, "time", m_Time, "delay", m_Delay, "easetype", m_EaseType, "looptype", m_LoopType, "onupdatetarget", base.gameObject, "onupdate", "MoveGuiElement"));
	}

	public void PlayReverse()
	{
		iTween.ValueTo(base.gameObject, iTween.Hash("ignoretimescale", true, "from", m_MoveToPos, "to", m_OriginalPos, "time", m_Time, "delay", m_Delay, "easetype", m_EaseType, "looptype", m_LoopType, "onupdatetarget", base.gameObject, "onupdate", "MoveGuiElement"));
	}

	public void MoveGuiElement(Vector2 position)
	{
		m_RectTransform.anchoredPosition = position;
	}

	public void Stop()
	{
		iTween.Stop(base.gameObject, "value");
	}

	public void Reset()
	{
		if ((bool)m_RectTransform)
		{
			m_RectTransform.anchoredPosition = m_OriginalPos;
		}
	}

	public void SetOriginalPosition(Vector3 newOrigPos)
	{
		m_OriginalPos = newOrigPos;
	}
}
