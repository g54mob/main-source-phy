using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
	[Header("Icon and Text")]
	public Image m_IconImage;

	public TextMeshProUGUI m_NameText;

	public TextMeshProUGUI m_DescText;

	[Header("Progress")]
	public GameObject m_ProgressParent;

	public Image m_ProgressFG;

	public TextMeshProUGUI m_ProgressText;

	[Header("Completed")]
	public GameObject m_CompletedParent;

	public TextMeshProUGUI m_CompletedDate;

	public TextMeshProUGUI m_CompletedTime;

	public TextMeshProUGUI m_CompletedLabel;

	private int m_AchID;

	private bool m_Unlocked;

	private readonly int PROGRESS_BAR_HEIGHT = 15;

	private readonly int PROGRESS_BAR_WIDTH = 375;

	public void Init(int achID)
	{
		m_AchID = achID;
	}

	public bool IsUnlocked()
	{
		return m_Unlocked;
	}

	public void UpdateWithAchivementData()
	{
		m_Unlocked = GameAchievements.HasUnlocked((GameAchievement)m_AchID);
		UpdateIconLocal();
		UpdateNameAndDescription();
		UpdateProgressBar();
		UpdateCompletedDate();
	}

	public GameAchievement GetGameAchivement()
	{
		return (GameAchievement)m_AchID;
	}

	private void UpdateIconLocal()
	{
		if (m_Unlocked && m_AchID < GameUI.m_Instance.m_Achievements.m_UnlockedSprites.Length)
		{
			m_IconImage.sprite = GameUI.m_Instance.m_Achievements.m_UnlockedSprites[m_AchID];
		}
		else if (m_AchID < GameUI.m_Instance.m_Achievements.m_LockedSprites.Length)
		{
			m_IconImage.sprite = GameUI.m_Instance.m_Achievements.m_LockedSprites[m_AchID];
		}
	}

	private void UpdateNameAndDescription()
	{
		int num = m_AchID + 1;
		if (num >= 10)
		{
			m_NameText.text = Localize.Get("ACH_NAME_" + num);
			m_DescText.text = Localize.Get("ACH_DESC_" + num);
		}
		else
		{
			m_NameText.text = Localize.Get("ACH_NAME_0" + num);
			m_DescText.text = Localize.Get("ACH_DESC_0" + num);
		}
	}

	private void UpdateProgressBar()
	{
		int totalValue = 0;
		GameAchievements.GetProgressValues(m_AchID, out var progressValue, out totalValue);
		m_ProgressText.text = $"{progressValue}/{totalValue}";
		float num = (float)progressValue / (float)totalValue;
		m_ProgressFG.GetComponent<RectTransform>().sizeDelta = new Vector2(num * (float)PROGRESS_BAR_WIDTH, PROGRESS_BAR_HEIGHT);
		m_ProgressParent.SetActive(totalValue > 1);
	}

	private void UpdateCompletedDate()
	{
		m_CompletedParent.SetActive(m_Unlocked);
		if (m_Unlocked)
		{
			DateTime unlockTime = GameAchievements.GetUnlockTime(m_AchID);
			m_CompletedDate.text = Utils.FormatShortDate(unlockTime);
			m_CompletedTime.text = unlockTime.ToLocalTime().ToString("h:mm tt") ?? "";
		}
	}
}
