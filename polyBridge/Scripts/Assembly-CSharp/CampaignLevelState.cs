public class CampaignLevelState
{
	public float m_ElapsedSeconds;

	public CampaignLevelStatus m_Status;

	public CampaignLevelState(float elapsedSeconds, CampaignLevelStatus status)
	{
		m_ElapsedSeconds = elapsedSeconds;
		m_Status = status;
	}

	public static bool StatusIsUpgrade(CampaignLevelStatus oldStatus, CampaignLevelStatus newStatus)
	{
		switch (oldStatus)
		{
		case CampaignLevelStatus.PASS:
			if (newStatus != CampaignLevelStatus.UNDER_BUDGET)
			{
				return newStatus == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS;
			}
			return true;
		case CampaignLevelStatus.UNDER_BUDGET:
			return newStatus == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS;
		case CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS:
			return false;
		default:
			return true;
		}
	}
}
