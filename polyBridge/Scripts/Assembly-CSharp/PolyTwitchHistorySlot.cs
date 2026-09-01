using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PolyTwitchHistorySlot : MonoBehaviour
{
	public TextMeshProUGUI m_SlotName;

	public Image m_Underline;

	public PointerEvents m_InfoPointerEvents;

	public RawImage m_RawImage;

	[NonSerialized]
	public PolyTwitchAutoSave m_AutoSave;

	public void Init(PolyTwitchAutoSave autoSave)
	{
		m_AutoSave = autoSave;
		m_Underline.gameObject.SetActive(value: false);
		m_SlotName.text = string.Format("Autosave at {0}", autoSave.m_SaveDateTime.ToString("h:mm tt ", CultureInfo.InvariantCulture));
		m_InfoPointerEvents.RegisterOnHoverChangeDelegate(OnHoverChange);
	}

	public void OnLoadAutoSave()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			if (PolyTwitchAutoPlay.m_Running)
			{
				PolyTwitchAutoPlay.TurnOff();
			}
			GameStateBuild.m_LoadAutoSaveOnEnter = m_AutoSave;
			GameUI.m_Instance.m_TopBar.OnExitSim();
		}
		else
		{
			GameUI.m_Instance.m_PolyTwitchMain.m_HistoryPanel.SelectAutoSave(m_AutoSave);
			Bridge.ClearAndLoad(m_AutoSave.m_BridgeSaveData);
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	public void OnHoverChange(bool hover)
	{
		GameUI.m_Instance.m_PolyTwitchMain.m_HistoryPanel.SetHoverHistorySlot(hover ? this : null);
	}
}
