using System;
using System.Collections.Generic;

public class PolyTwitchAutoSaves
{
	public static List<PolyTwitchAutoSave> m_Saves = new List<PolyTwitchAutoSave>();

	public static readonly int MAX_SAVES = 999;

	public static PolyTwitchAutoSave Create(BridgeSaveData bridgeSaveData)
	{
		PolyTwitchAutoSave polyTwitchAutoSave = new PolyTwitchAutoSave(bridgeSaveData, DateTime.Now);
		polyTwitchAutoSave.m_Slot = GameUI.m_Instance.m_PolyTwitchMain.m_HistoryPanel.AddSlot(polyTwitchAutoSave);
		m_Saves.Add(polyTwitchAutoSave);
		return polyTwitchAutoSave;
	}

	public static void RemoveOldestAutoSave()
	{
		PolyTwitchAutoSave oldestAutoSave = GetOldestAutoSave();
		if (oldestAutoSave != null)
		{
			GameUI.m_Instance.m_PolyTwitchMain.m_HistoryPanel.DeleteSlot(oldestAutoSave.m_Slot);
			m_Saves.Remove(oldestAutoSave);
		}
	}

	public static PolyTwitchAutoSave GetMostRecentAutoSave()
	{
		if (m_Saves.Count <= 0)
		{
			return null;
		}
		return m_Saves[m_Saves.Count - 1];
	}

	public static PolyTwitchAutoSave GetOldestAutoSave()
	{
		if (m_Saves.Count <= 0)
		{
			return null;
		}
		return m_Saves[0];
	}

	public static void DeleteAll()
	{
		foreach (PolyTwitchAutoSave safe in m_Saves)
		{
			GameUI.m_Instance.m_PolyTwitchMain.m_HistoryPanel.DeleteSlot(safe.m_Slot);
		}
		m_Saves.Clear();
		GameStateBuild.m_LoadAutoSaveOnEnter = null;
	}

	public static void TurnOffPreviews()
	{
		foreach (PolyTwitchAutoSave safe in m_Saves)
		{
			safe.m_Slot.m_RawImage.gameObject.SetActive(value: false);
		}
	}
}
