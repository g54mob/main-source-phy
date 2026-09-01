using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_AchievementPopup : MonoBehaviour
{
	private enum PopupState
	{
		Idle = 0,
		FetchingData = 1,
		ForwardAnim = 2,
		ReverseAnim = 3
	}

	public Image m_IconImage;

	public TextMeshProUGUI m_NameText;

	private List<int> m_AchQueue = new List<int>();

	private int m_AchID;

	private string m_AchStrID;

	private int m_ImageFetchID;

	private TweenPosition m_TweenPosition;

	private PopupState m_State;

	private float m_TimeTracker;

	private void Awake()
	{
		m_TweenPosition = GetComponent<TweenPosition>();
	}

	private void Update()
	{
		switch (m_State)
		{
		case PopupState.FetchingData:
			UpdateFetchData();
			break;
		case PopupState.ForwardAnim:
			UpdateForwardAnim();
			break;
		case PopupState.ReverseAnim:
			UpdateReverseAnim();
			break;
		default:
			UpdateIdle();
			break;
		}
	}

	public void ShowAchievementPopup(int achID)
	{
		m_AchQueue.Add(achID);
		base.gameObject.SetActive(value: true);
		InterfaceAudio.Play("ui_tutorial_achievement_unlocked");
	}

	private void UpdateIdle()
	{
		if (m_AchQueue.Count > 0)
		{
			StartDataFetch(m_AchQueue[0]);
			m_AchQueue.RemoveAt(0);
			m_State = PopupState.FetchingData;
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}

	private void UpdateFetchData()
	{
		UpdateIcon();
		m_TweenPosition.Play();
		m_TimeTracker = 3.5f;
		m_State = PopupState.ForwardAnim;
	}

	private void UpdateForwardAnim()
	{
		m_TimeTracker -= Time.unscaledDeltaTime;
		if (m_TimeTracker <= 0f)
		{
			m_TweenPosition.PlayReverse();
			m_TimeTracker = 1f;
			m_State = PopupState.ReverseAnim;
		}
	}

	private void UpdateReverseAnim()
	{
		m_TimeTracker -= Time.unscaledDeltaTime;
		if (m_TimeTracker <= 0f)
		{
			m_State = PopupState.Idle;
		}
	}

	private void StartDataFetch(int achID)
	{
		m_AchID = achID;
		int num = m_AchID + 1;
		if (num >= 10)
		{
			m_NameText.text = Localize.Get("ACH_NAME_" + num);
		}
		else
		{
			m_NameText.text = Localize.Get("ACH_NAME_0" + num);
		}
	}

	private void UpdateIcon()
	{
		if (m_AchID < GameUI.m_Instance.m_Achievements.m_UnlockedSprites.Length)
		{
			m_IconImage.sprite = GameUI.m_Instance.m_Achievements.m_UnlockedSprites[m_AchID];
		}
	}
}
