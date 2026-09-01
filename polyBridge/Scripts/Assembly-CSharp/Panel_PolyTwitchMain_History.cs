using UnityEngine;
using UnityEngine.UI;

public class Panel_PolyTwitchMain_History : MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject m_SlotPrefab;

	[Header("Scrolling")]
	public RectTransform m_ContentRectTransform;

	public ScrollRect m_ScrollRect;

	private float m_ContentLastY;

	private bool m_IsDraggingScrollbar;

	private string m_LastPreviewHash;

	private PolyTwitchHistorySlot m_HoverHistorySlot;

	private BridgeSaveData m_RestoreBridgeSaveData;

	public void OnEnable()
	{
		m_ContentLastY = m_ContentRectTransform.anchoredPosition.y;
	}

	public void OnDisable()
	{
	}

	public void Update()
	{
		UpdateScrollbarState();
		UpdateBridgePreview();
		m_ScrollRect.enabled = !GameUI.m_Instance.m_PolyTwitchMain.IsMoving();
	}

	public bool IsDraggingScrollbar()
	{
		return m_IsDraggingScrollbar;
	}

	public PolyTwitchHistorySlot AddSlot(PolyTwitchAutoSave autoSave)
	{
		PolyTwitchHistorySlot component = Object.Instantiate(m_SlotPrefab, m_ContentRectTransform.transform).GetComponent<PolyTwitchHistorySlot>();
		component.transform.SetSiblingIndex(0);
		component.Init(autoSave);
		return component;
	}

	public void DeleteSlot(PolyTwitchHistorySlot slot)
	{
		if (slot != null)
		{
			Object.Destroy(slot.gameObject);
		}
	}

	public void SelectAutoSave(PolyTwitchAutoSave autoSave)
	{
		foreach (Transform item in m_ContentRectTransform.transform)
		{
			PolyTwitchHistorySlot component = item.GetComponent<PolyTwitchHistorySlot>();
			if ((bool)component)
			{
				component.m_Underline.gameObject.SetActive(component.m_AutoSave == autoSave);
			}
		}
	}

	public PolyTwitchHistorySlot GetMostRecentSaveSlot()
	{
		foreach (Transform item in m_ContentRectTransform.transform)
		{
			PolyTwitchHistorySlot component = item.GetComponent<PolyTwitchHistorySlot>();
			if ((bool)component)
			{
				return component;
			}
		}
		return null;
	}

	public void SetHoverHistorySlot(PolyTwitchHistorySlot slot)
	{
		if (m_HoverHistorySlot != slot)
		{
			m_HoverHistorySlot = slot;
		}
	}

	private void UpdateScrollbarState()
	{
		if (Mathf.Abs(m_ContentRectTransform.anchoredPosition.y - m_ContentLastY) > 0.001f)
		{
			m_IsDraggingScrollbar = true;
		}
		m_ContentLastY = m_ContentRectTransform.anchoredPosition.y;
		if (m_IsDraggingScrollbar && GameInput.GetMouseButtonJustReleased(0))
		{
			m_IsDraggingScrollbar = false;
		}
	}

	private void UpdateBridgePreview()
	{
		PolyTwitchHistorySlot hoverHistorySlot = m_HoverHistorySlot;
		if (hoverHistorySlot == null)
		{
			PolyTwitchAutoSaves.TurnOffPreviews();
		}
		else
		{
			DisplayBridgePreviewSlot(hoverHistorySlot);
		}
	}

	private void DisplayBridgePreviewSlot(PolyTwitchHistorySlot historySlot)
	{
		if (m_LastPreviewHash != historySlot.m_AutoSave.m_BridgeSaveDataHash)
		{
			if (!PolyTwitch.m_IsTakingScreenshot)
			{
				m_RestoreBridgeSaveData = BridgeSave.Serialize();
				PolyTwitch.m_IsTakingScreenshot = true;
				WorkshopPreview.Create(showBridge: true, showPrebuilds: true, PointOfViewType.SIM_CENTER_PITCHED_DOWN, GameStateManager.GetState(), historySlot.m_AutoSave.m_BridgeSaveData, OnOverlayCaptured, OnCreatePreviewComplete);
				m_LastPreviewHash = historySlot.m_AutoSave.m_BridgeSaveDataHash;
			}
		}
		else if (historySlot.m_RawImage.texture != null)
		{
			historySlot.m_RawImage.gameObject.SetActive(value: true);
		}
	}

	private void OnOverlayCaptured(BridgeSaveData suggestion)
	{
		Bridge.ClearAndLoad(suggestion);
	}

	public void OnCreatePreviewComplete()
	{
		PolyTwitch.m_IsTakingScreenshot = false;
		if ((bool)m_HoverHistorySlot)
		{
			m_HoverHistorySlot.m_RawImage.texture = WorkshopPreview.m_PreviewTexture2D;
			m_HoverHistorySlot.m_RawImage.gameObject.SetActive(value: true);
			Bridge.ClearAndLoad(m_RestoreBridgeSaveData);
		}
	}
}
