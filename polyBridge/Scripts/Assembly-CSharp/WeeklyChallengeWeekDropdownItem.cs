using UnityEngine;
using UnityEngine.UI;

public class WeeklyChallengeWeekDropdownItem : MonoBehaviour
{
	public Image m_LevelProgressCompletedIcon;

	public Image m_LevelProgressUnderBudgetIcon;

	public Image m_LevelProgressUnderBudgetNoBreaksIcon;

	public void Start()
	{
		if (m_LevelProgressCompletedIcon != null && m_LevelProgressCompletedIcon.transform.parent != null)
		{
			int num = GameUI.m_Instance.m_WeeklyChallenges.GetSelectedSeason() - 1;
			int num2 = ((num >= 0) ? (num * WeeklyChallenges.NUM_CHALLENGES_PER_SEASON) : 0);
			WeeklyChallengeStub weeklyChallengeStub = WeeklyChallenges.GetWeeklyChallengeStub(m_LevelProgressCompletedIcon.transform.parent.GetSiblingIndex() + num2);
			m_LevelProgressCompletedIcon.gameObject.SetActive(weeklyChallengeStub != null && WeeklyChallengesProgress.HasCompletedLevel(weeklyChallengeStub.m_ItemID));
			m_LevelProgressUnderBudgetIcon.gameObject.SetActive(weeklyChallengeStub != null && WeeklyChallengesProgress.HasCompletedLevelUnderBudget(weeklyChallengeStub.m_ItemID, WeeklyChallenges.GetBudget(weeklyChallengeStub.m_ItemID)));
			m_LevelProgressUnderBudgetNoBreaksIcon.gameObject.SetActive(weeklyChallengeStub != null && WeeklyChallengesProgress.HasCompletedLevelUnderBudgetNoBreaks(weeklyChallengeStub.m_ItemID, WeeklyChallenges.GetBudget(weeklyChallengeStub.m_ItemID)));
		}
	}
}
