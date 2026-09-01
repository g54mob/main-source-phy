using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PolyTwitchSuggestionSlot : MonoBehaviour
{
	public Button m_Button;

	public TextMeshProUGUI m_UserName;

	public TextMeshProUGUI m_Time;

	public Image m_StatusIcon;

	public Image m_TagIcon;

	[Header("Status Sprites")]
	public Sprite m_ViewedSprite;

	public Sprite m_FailedSprite;

	public Sprite m_PassedSprite;

	[Header("Tag Sprites")]
	public Sprite m_NewDesignSprite;

	public Sprite m_CorrectionSprite;

	public Sprite m_FunSprite;

	[NonSerialized]
	public PolyTwitchSuggestion m_Suggestion;

	private readonly float REFRESH_SECONDS = 5f;

	private float m_NextRefreshTime;

	private void Update()
	{
		if (m_Suggestion != null)
		{
			if (Time.realtimeSinceStartup > m_NextRefreshTime)
			{
				m_Time.text = ElapsedTimeFormatted(m_Suggestion.m_DateTime);
				m_NextRefreshTime = Time.realtimeSinceStartup + REFRESH_SECONDS;
			}
			SetStatusIconSprite(m_Suggestion.m_Status);
		}
	}

	public void Init(PolyTwitchSuggestion suggestion)
	{
		m_Suggestion = suggestion;
		GameUI.SetAndEnableText(m_UserName, suggestion.GetDisplayName());
		m_Time.text = ElapsedTimeFormatted(suggestion.m_DateTime);
		m_NextRefreshTime = 0f;
		SetTagIconSprite(m_Suggestion.m_Tag);
		SetStatusIconSprite(PolyTwitchSuggestionStatus.UNVIEWED);
	}

	public string ElapsedTimeFormatted(DateTime createDateTime)
	{
		int num = Mathf.RoundToInt((float)(DateTime.Now - createDateTime).TotalSeconds);
		int num2 = Mathf.RoundToInt((float)num / 60f);
		int num3 = num % 60;
		if (num2 <= 99)
		{
			if (num2 <= 0)
			{
				return $"{num3}s ago";
			}
			return $"{num2}m ago";
		}
		return "99+m ago";
	}

	public void OnButton()
	{
		if (!WorkshopPreview.m_IsTakingScreenshot)
		{
			GameUI.m_Instance.m_PolyTwitchBridge.ViewSuggestion(m_Suggestion);
			GameUI.m_Instance.m_PolyTwitchMain.DisplayBridgeAfterImageLoads();
		}
	}

	public void SetStatusIconSprite(PolyTwitchSuggestionStatus status)
	{
		switch (status)
		{
		case PolyTwitchSuggestionStatus.UNVIEWED:
			m_StatusIcon.gameObject.SetActive(value: false);
			break;
		case PolyTwitchSuggestionStatus.VIEWED:
		case PolyTwitchSuggestionStatus.SIMULATED:
			m_StatusIcon.gameObject.SetActive(value: true);
			m_StatusIcon.GetComponent<ToolTipText>().m_Text = "Viewed";
			m_StatusIcon.sprite = m_ViewedSprite;
			break;
		case PolyTwitchSuggestionStatus.FAILED:
			m_StatusIcon.gameObject.SetActive(value: true);
			m_StatusIcon.GetComponent<ToolTipText>().m_Text = "Failed";
			m_StatusIcon.sprite = m_FailedSprite;
			break;
		case PolyTwitchSuggestionStatus.PASSED:
			m_StatusIcon.gameObject.SetActive(value: true);
			m_StatusIcon.GetComponent<ToolTipText>().m_Text = "Passed";
			m_StatusIcon.sprite = m_PassedSprite;
			break;
		default:
			Debug.LogWarningFormat("Unsupported tag {0} in SetStatusIconSprite", status.ToString());
			break;
		}
	}

	private void SetTagIconSprite(PolyTwitchSuggestionTag tag)
	{
		switch (tag)
		{
		case PolyTwitchSuggestionTag.NONE:
			m_TagIcon.gameObject.SetActive(value: false);
			break;
		case PolyTwitchSuggestionTag.CORRECTION:
			m_TagIcon.gameObject.SetActive(value: true);
			m_TagIcon.GetComponent<ToolTipText>().m_Text = "Correction";
			m_TagIcon.sprite = m_CorrectionSprite;
			break;
		case PolyTwitchSuggestionTag.JUST_FOR_FUN:
			m_TagIcon.gameObject.SetActive(value: true);
			m_TagIcon.GetComponent<ToolTipText>().m_Text = "For Fun";
			m_TagIcon.sprite = m_FunSprite;
			break;
		case PolyTwitchSuggestionTag.NEW_DESIGN:
			m_TagIcon.gameObject.SetActive(value: true);
			m_TagIcon.GetComponent<ToolTipText>().m_Text = "New Design";
			m_TagIcon.sprite = m_NewDesignSprite;
			break;
		default:
			Debug.LogWarningFormat("Unsupported tag {0} in SetTagIconSprite", tag.ToString());
			break;
		}
	}
}
