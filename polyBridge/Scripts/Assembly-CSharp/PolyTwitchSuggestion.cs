using System;

public class PolyTwitchSuggestion
{
	public PolyTwitchSuggestionSlot m_Slot;

	public string m_Username;

	public string m_OwnerId;

	public string m_FileId;

	public string m_LayoutHash;

	public DateTime m_DateTime;

	public BridgeSaveData m_BridgeSaveData;

	public string m_BridgeHash;

	public bool m_Muted;

	public PolyTwitchSuggestionStatus m_Status;

	public PolyTwitchSuggestionTag m_Tag;

	public int m_NumBitsUsed;

	public bool IsOwnerMuted()
	{
		if (PolyTwitchBans.m_Bans.ContainsKey(m_OwnerId))
		{
			PolyTwitchBan polyTwitchBan = PolyTwitchBans.m_Bans[m_OwnerId];
			if (polyTwitchBan != null && polyTwitchBan.m_Muted)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasBeenViewed()
	{
		return m_Status != PolyTwitchSuggestionStatus.UNVIEWED;
	}

	public bool HasBeenSimulated()
	{
		if (m_Status != PolyTwitchSuggestionStatus.SIMULATED)
		{
			return HasPassedOrFailed();
		}
		return true;
	}

	public bool HasPassedOrFailed()
	{
		if (m_Status != PolyTwitchSuggestionStatus.FAILED)
		{
			return m_Status == PolyTwitchSuggestionStatus.PASSED;
		}
		return true;
	}

	public void SetStatus(PolyTwitchSuggestionStatus status)
	{
		m_Status = status;
	}

	public string GetDisplayName()
	{
		return m_Username;
	}

	public void UpdateSlotDisplay()
	{
		GameUI.SetAndEnableText(m_Slot.m_UserName, GetDisplayName());
		m_Slot.m_Time.text = m_Slot.ElapsedTimeFormatted(m_DateTime);
	}
}
