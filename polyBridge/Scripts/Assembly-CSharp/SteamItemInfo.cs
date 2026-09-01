using Steamworks.Ugc;

public class SteamItemInfo
{
	public string m_Title;

	public string m_Description;

	public string m_ID;

	public string m_InstallPath;

	public string m_PreviewImageUrl;

	public bool m_IsMod;

	public SteamItemInfo(Item item)
	{
		m_Title = item.Title;
		m_Description = item.Description;
		m_ID = item.Id.ToString();
		m_PreviewImageUrl = item.PreviewImageUrl;
		m_IsMod = item.HasTag(WorkshopTags.MOD_TAG);
		m_InstallPath = item.Directory;
	}
}
