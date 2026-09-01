using UnityEngine;
using UnityEngine.UI;

public class Panel_PolyTwitchMain_BanList : MonoBehaviour
{
	public Button m_UnBanAllButton;

	[Header("Prefabs")]
	public GameObject m_SlotPrefab;

	[Header("Scrolling")]
	public RectTransform m_ContentRectTransform;

	public ScrollRect m_ScrollRect;

	private float m_ContentLastY;

	private bool m_IsDraggingScrollbar;

	public void Start()
	{
		m_UnBanAllButton.onClick.AddListener(OnUnBanAll);
	}

	public void OnEnable()
	{
		m_ContentLastY = m_ContentRectTransform.anchoredPosition.y;
	}

	public void Update()
	{
		UpdateScrollbarState();
		m_ScrollRect.enabled = !GameUI.m_Instance.m_PolyTwitchMain.IsMoving();
	}

	public void AddBan(PolyTwitchBan ban)
	{
		PolyTwitchMuteSlot component = Object.Instantiate(m_SlotPrefab, m_ContentRectTransform.transform).GetComponent<PolyTwitchMuteSlot>();
		component.transform.SetSiblingIndex(0);
		component.Init(ban);
	}

	public void RemoveBan(PolyTwitchBan ban)
	{
		foreach (Transform item in m_ContentRectTransform.transform)
		{
			PolyTwitchMuteSlot component = item.GetComponent<PolyTwitchMuteSlot>();
			if ((bool)component && component.m_Ban == ban)
			{
				Object.Destroy(component.gameObject);
				break;
			}
		}
	}

	public bool IsDraggingScrollbar()
	{
		return m_IsDraggingScrollbar;
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

	private void OnUnBanAll()
	{
		PopUpMessage.Display(Localize.Get("CONFIRM_UNMUTE_ALL"), ConfirmUnBanAll);
	}

	private void ConfirmUnBanAll()
	{
		PolyTwitchBans.RemoveAllBans();
	}
}
