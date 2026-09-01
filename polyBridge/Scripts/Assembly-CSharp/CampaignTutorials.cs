using UnityEngine;

public class CampaignTutorials : MonoBehaviour
{
	public string m_UiTutorialLevelId;

	public string m_HydraulicsTutorialLevelId;

	public string m_HydraulicControllerTutorialLevelId;

	public static CampaignTutorials m_Instance;

	private void Awake()
	{
		m_Instance = this;
	}

	public CampaignTutorialType GetTutorialTypeForLevelId(string levelId)
	{
		if (levelId == m_UiTutorialLevelId)
		{
			return CampaignTutorialType.UI;
		}
		if (levelId == m_HydraulicsTutorialLevelId)
		{
			return CampaignTutorialType.Hydraulics;
		}
		if (levelId == m_HydraulicControllerTutorialLevelId)
		{
			return CampaignTutorialType.HydraulicController;
		}
		return CampaignTutorialType.None;
	}
}
