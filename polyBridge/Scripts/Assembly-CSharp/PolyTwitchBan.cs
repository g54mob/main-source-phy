public class PolyTwitchBan
{
	public string m_Username;

	public string m_OwnerId;

	public bool m_Muted;

	public PolyTwitchBan(string username, string ownerId, bool muted)
	{
		m_Username = username;
		m_OwnerId = ownerId;
		m_Muted = muted;
	}

	public void Mute()
	{
		m_Muted = true;
		PolyTwitchSuggestions.HideSuggestionsWithOwnerId(m_OwnerId, hide: true);
	}

	public void UnMute()
	{
		m_Muted = false;
		PolyTwitchSuggestions.HideSuggestionsWithOwnerId(m_OwnerId, hide: false);
	}
}
