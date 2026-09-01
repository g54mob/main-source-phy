using System.IO;

public class WorkshopCampaignLevel
{
	public WorkshopItem m_WorkshopItem;

	public bool m_UnlimitedBudget;

	public bool m_UnlimitedMaterial;

	public WorkshopCampaignLevel(WorkshopItem workshopItem)
	{
		m_WorkshopItem = workshopItem;
		m_UnlimitedBudget = false;
		m_UnlimitedMaterial = false;
	}

	public string GetId()
	{
		if (m_WorkshopItem == null)
		{
			return string.Empty;
		}
		return m_WorkshopItem.GetId();
	}

	public string GetTitle()
	{
		if (m_WorkshopItem == null)
		{
			return string.Empty;
		}
		return m_WorkshopItem.GetTitle();
	}

	public string GetDescription()
	{
		if (m_WorkshopItem == null)
		{
			return string.Empty;
		}
		return m_WorkshopItem.GetDescription();
	}

	public string GetMetaData()
	{
		if (m_WorkshopItem == null)
		{
			return string.Empty;
		}
		return m_WorkshopItem.GetMetadata();
	}

	public string GetDirectory()
	{
		if (m_WorkshopItem == null)
		{
			return string.Empty;
		}
		return m_WorkshopItem.GetDirectory();
	}

	public string GetPathAndFilename()
	{
		if (m_WorkshopItem == null)
		{
			return string.Empty;
		}
		return Path.Combine(m_WorkshopItem.GetDirectory(), Workshop.LEVEL_LAYOUT_FILENAME);
	}
}
