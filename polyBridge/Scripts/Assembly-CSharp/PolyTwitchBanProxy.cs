public class PolyTwitchBanProxy
{
	public string m_Username;

	public string m_HashId;

	public bool m_Muted;

	public PolyTwitchBanProxy(PolyTwitchBan ban)
	{
		m_Username = ban.m_Username;
		m_HashId = ban.m_OwnerId;
		m_Muted = ban.m_Muted;
	}
}
