using System.Collections.Generic;

public class CampaignLayoutData
{
	public int m_Version;

	public string m_Filename;

	public string m_Id;

	public string m_Title;

	public string m_Description;

	public string m_WinMessage;

	public List<string> m_ItemIds;

	public CampaignLayoutData(int version, string filename, string id, string title, string description, string winMessage, List<string> itemIds)
	{
		m_Version = version;
		m_Filename = filename;
		m_Id = id;
		m_Title = title;
		m_Description = description;
		m_WinMessage = winMessage;
		m_ItemIds = new List<string>(itemIds);
	}
}
