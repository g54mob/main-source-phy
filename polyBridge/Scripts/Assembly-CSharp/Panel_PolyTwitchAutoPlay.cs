using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PolyTwitchAutoPlay : MonoBehaviour
{
	[Header("Header")]
	public TextMeshProUGUI m_Title;

	[Header("Body")]
	public GameObject m_Body;

	public Button m_NextButton;

	private void OnEnable()
	{
		m_NextButton.onClick.AddListener(OnNext);
		Update();
	}

	private void OnDisable()
	{
		m_NextButton.onClick.RemoveAllListeners();
	}

	private void Update()
	{
		m_Title.text = PolyTwitchAutoPlay.GetTitleText();
		PositionNextIcon();
	}

	private void PositionNextIcon()
	{
		Vector2 preferredValues = m_Title.GetPreferredValues();
		if (preferredValues.x > 0f)
		{
			float x = m_Title.rectTransform.anchoredPosition.x + preferredValues.x / 2f + 5f;
			m_NextButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, m_NextButton.GetComponent<RectTransform>().anchoredPosition.y);
		}
	}

	public void SkipToNextBridge()
	{
		OnNext();
	}

	private void OnMute()
	{
		PolyTwitchAutoPlay.MuteCurrentSlot();
	}

	private void OnNext()
	{
		if (PolyTwitchAutoPlay.m_CycleToNextLevel)
		{
			PolyTwitchAutoPlay.MoveToNextLevel();
		}
		else
		{
			PolyTwitchAutoPlay.MoveToNextSuggestion();
		}
	}
}
