using TMPro;
using UnityEngine;

public class WeeklyChallengeSeasonDropdownItem : MonoBehaviour
{
	public TextMeshProUGUI m_SeasonProgressText;

	private void Start()
	{
		if (m_SeasonProgressText != null && m_SeasonProgressText.transform.parent != null && (bool)m_SeasonProgressText.transform.parent.parent)
		{
			int siblingIndex = m_SeasonProgressText.transform.transform.parent.parent.GetSiblingIndex();
			int numberPassedWeeksInSeason = WeeklyChallenges.GetNumberPassedWeeksInSeason(siblingIndex);
			int numberWeeksInSeason = WeeklyChallenges.GetNumberWeeksInSeason(siblingIndex);
			m_SeasonProgressText.text = $"{numberPassedWeeksInSeason} / {numberWeeksInSeason}";
		}
	}
}
