using System.Collections.Generic;

public class PolyTwitchBans
{
	public static Dictionary<string, PolyTwitchBan> m_Bans = new Dictionary<string, PolyTwitchBan>();

	public static PolyTwitchBan Create(string username, string ownerId, bool muted)
	{
		if (m_Bans.ContainsKey(ownerId))
		{
			m_Bans[ownerId].m_Muted = muted;
			m_Bans[ownerId].m_Username = username;
			m_Bans[ownerId].m_OwnerId = ownerId;
			return m_Bans[ownerId];
		}
		PolyTwitchBan polyTwitchBan = new PolyTwitchBan(username, ownerId, muted);
		m_Bans.Add(ownerId, polyTwitchBan);
		return polyTwitchBan;
	}

	public static void MutePlayer(string username, string ownerId)
	{
		PolyTwitchBan banForOwnerId = GetBanForOwnerId(ownerId);
		if (banForOwnerId != null)
		{
			banForOwnerId.Mute();
			return;
		}
		PolyTwitchBan polyTwitchBan = Create(username, ownerId, muted: true);
		polyTwitchBan.Mute();
		GameUI.m_Instance.m_PolyTwitchMain.m_BanListPanel.AddBan(polyTwitchBan);
	}

	public static void BanPlayer(string username, string ownerId)
	{
		PolyTwitchBan banForOwnerId = GetBanForOwnerId(ownerId);
		if (banForOwnerId != null)
		{
			banForOwnerId.Mute();
			return;
		}
		PolyTwitch.BanPlayer(ownerId);
		PolyTwitchBan polyTwitchBan = Create(username, ownerId, muted: true);
		polyTwitchBan.Mute();
		GameUI.m_Instance.m_PolyTwitchMain.m_BanListPanel.AddBan(polyTwitchBan);
	}

	public static void UnBanPlayer(string ownerId)
	{
		PolyTwitchBan banForOwnerId = GetBanForOwnerId(ownerId);
		if (banForOwnerId != null)
		{
			banForOwnerId.UnMute();
			PolyTwitch.UnBanPlayer(ownerId);
			GameUI.m_Instance.m_PolyTwitchMain.m_BanListPanel.RemoveBan(banForOwnerId);
			m_Bans.Remove(ownerId);
		}
	}

	public static void RemoveAllBans()
	{
		PolyTwitch.UnBanAllPlayers(UnBanAllPlayersComplete);
		GameUI.m_Instance.m_Status.Open(Localize.Get("UI_WORKSHOPUPDATEITEM_UPDATING"));
	}

	private static void UnBanAllPlayersComplete(string errorMessage)
	{
		if (string.IsNullOrEmpty(errorMessage))
		{
			GameUI.m_Instance.m_Status.Close();
			foreach (KeyValuePair<string, PolyTwitchBan> ban in m_Bans)
			{
				PolyTwitchBan value = ban.Value;
				if (value != null)
				{
					value.UnMute();
					GameUI.m_Instance.m_PolyTwitchMain.m_BanListPanel.RemoveBan(value);
				}
			}
			m_Bans.Clear();
		}
		else
		{
			GameUI.m_Instance.m_Status.Complete(errorMessage);
		}
	}

	private static PolyTwitchBan GetBanForOwnerId(string ownerId)
	{
		if (m_Bans.ContainsKey(ownerId))
		{
			return m_Bans[ownerId];
		}
		return null;
	}
}
