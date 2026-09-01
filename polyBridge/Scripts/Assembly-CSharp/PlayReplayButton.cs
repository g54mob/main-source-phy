using UnityEngine;
using UnityEngine.UI;

public class PlayReplayButton : MonoBehaviour
{
	public Button m_Button;

	public Image m_Background;

	public Image m_Icon;

	public Sprite m_PlaySprite;

	public Sprite m_PauseSprite;

	private bool m_IsAnimating;

	private float m_AnimationElapsedSeconds;

	private float ANIMATION_DURATION_SECONDS = 0.35f;

	private Color DEFAULT_BACKGROUND_COLOR = new Color(0f, 0f, 0f, 0.7843f);

	private Color DEFAULT_ICON_COLOR = new Color(1f, 1f, 1f, 1f);

	private Color TARGET_BACKGROUND_COLOR = new Color(0f, 0f, 0f, 0f);

	private Color TARGET_ICON_COLOR = new Color(1f, 1f, 1f, 0f);

	private Vector3 DEFAULT_SCALE = Vector3.one;

	private Vector3 TARGET_SCALE = new Vector3(2f, 2f, 1f);

	private void Update()
	{
		if (m_IsAnimating)
		{
			m_AnimationElapsedSeconds += Time.unscaledDeltaTime;
			float num = Mathf.Clamp01(m_AnimationElapsedSeconds / ANIMATION_DURATION_SECONDS);
			AnimateColorAndScale(num);
			if (Mathf.Approximately(num, 1f))
			{
				HideButton();
				m_IsAnimating = false;
			}
		}
	}

	public void DisplayPlayIconStatic()
	{
		m_Icon.sprite = m_PlaySprite;
		SetDefaultColorAndScale();
	}

	public void DisplayPauseIconStatic()
	{
		m_Icon.sprite = m_PauseSprite;
		SetDefaultColorAndScale();
	}

	public void DoPlayIconAnimation()
	{
		m_Icon.sprite = m_PlaySprite;
		StartAnimation();
	}

	public void DoPauseIconAnimation()
	{
		m_Icon.sprite = m_PauseSprite;
		StartAnimation();
	}

	public void HideButton()
	{
		m_Background.color = new Color(0f, 0f, 0f, 0f);
		m_Icon.color = new Color(1f, 1f, 1f, 0f);
	}

	private void StartAnimation()
	{
		SetDefaultColorAndScale();
		m_IsAnimating = true;
		m_AnimationElapsedSeconds = 0f;
	}

	private void AnimateColorAndScale(float t)
	{
		m_Background.transform.localScale = Vector3.Lerp(DEFAULT_SCALE, TARGET_SCALE, t);
		m_Icon.transform.localScale = Vector3.Lerp(DEFAULT_SCALE, TARGET_SCALE, t);
		m_Background.color = Color.Lerp(DEFAULT_BACKGROUND_COLOR, TARGET_BACKGROUND_COLOR, t);
		m_Icon.color = Color.Lerp(DEFAULT_ICON_COLOR, TARGET_ICON_COLOR, t);
	}

	private void SetDefaultColorAndScale()
	{
		m_Background.transform.localScale = Vector3.one;
		m_Icon.transform.localScale = Vector3.one;
		m_Background.color = DEFAULT_BACKGROUND_COLOR;
		m_Icon.color = DEFAULT_ICON_COLOR;
	}
}
