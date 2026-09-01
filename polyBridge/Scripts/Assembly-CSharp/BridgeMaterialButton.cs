using UnityEngine;

public class BridgeMaterialButton : MonoBehaviour
{
	public TwoStateButton m_TwoStateButton;

	public RectTransform m_TwoStateButtonRectTransform;

	public MaterialLimit m_MaterialLimit;

	public GameObject m_SelectedIcon;

	public GameObject m_TutorialArrow;

	private float m_StartY;

	private float m_TargetY;

	private float m_StartTime;

	private bool m_Animating;

	private readonly int SELECTED_Y_OFFSET = 8;

	private readonly float TRANSITION_TIME_SECONDS = 0.1f;

	public void Awake()
	{
		if (m_TutorialArrow != null)
		{
			m_TutorialArrow.SetActive(value: false);
		}
	}

	public void Update()
	{
		if (m_Animating)
		{
			float num = Mathf.Clamp01((Time.realtimeSinceStartup - m_StartTime) / TRANSITION_TIME_SECONDS);
			m_TwoStateButtonRectTransform.anchoredPosition = new Vector2(m_TwoStateButtonRectTransform.anchoredPosition.x, Mathf.SmoothStep(m_StartY, m_TargetY, num));
			if (Mathf.Approximately(num, 1f))
			{
				m_Animating = false;
			}
		}
	}

	public void Select(bool on)
	{
		m_TwoStateButton.TurnOn(on);
		m_SelectedIcon.SetActive(on);
		m_StartY = m_TwoStateButtonRectTransform.anchoredPosition.y;
		m_StartTime = Time.realtimeSinceStartup;
		m_TargetY = (on ? SELECTED_Y_OFFSET : 0);
		m_Animating = true;
	}

	public void SelectNoAnimation(bool on)
	{
		Select(on);
		m_TwoStateButtonRectTransform.anchoredPosition = new Vector2(m_TwoStateButtonRectTransform.anchoredPosition.x, on ? ((float)SELECTED_Y_OFFSET) : 0f);
		m_Animating = false;
	}

	public void ShowTutorialArrow(bool show)
	{
		if (m_TutorialArrow != null)
		{
			m_TutorialArrow.SetActive(show);
		}
	}
}
