using System;

public class PolyTwitchAutoSave
{
	public PolyTwitchHistorySlot m_Slot;

	public DateTime m_SaveDateTime;

	public BridgeSaveData m_BridgeSaveData;

	public string m_BridgeSaveDataHash;

	public PolyTwitchAutoSave(BridgeSaveData bridgeSaveData, DateTime saveDateTime)
	{
		m_BridgeSaveData = bridgeSaveData;
		m_SaveDateTime = saveDateTime;
		m_BridgeSaveDataHash = Utils.MD5HashFor(m_BridgeSaveData.SerializeBinary());
	}
}
