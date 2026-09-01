using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PolyTwitchMain_Suggestions : MonoBehaviour
{
	public Button m_RemoveAllButton;

	public GameObject m_BackedUpSuggestions;

	[Header("Prefabs")]
	public GameObject m_SlotPrefab;

	public GameObject m_SuggestionsParentPrefab;

	[Header("Scrolling")]
	public RectTransform m_ContentRectTransform;

	public ScrollRect m_ScrollRect;

	private float m_ContentLastY;

	private bool m_IsDraggingScrollbar;

	private List<string> m_BackupLayoutHashes = new List<string>();

	private const int NUM_LEVELS_TO_BACKUP = 2;

	public void Start()
	{
		m_RemoveAllButton.onClick.AddListener(OnRemoveAll);
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

	public bool IsDraggingScrollbar()
	{
		return m_IsDraggingScrollbar;
	}

	public PolyTwitchSuggestionSlot AddSuggestion(PolyTwitchSuggestion suggestion)
	{
		GameObject obj = Object.Instantiate(m_SlotPrefab, m_ContentRectTransform.transform);
		obj.name = $"{m_SlotPrefab.name}_{suggestion.m_Username}";
		PolyTwitchSuggestionSlot component = obj.GetComponent<PolyTwitchSuggestionSlot>();
		component.transform.SetSiblingIndex(0);
		component.Init(suggestion);
		if (suggestion.IsOwnerMuted())
		{
			suggestion.m_Muted = true;
			component.gameObject.SetActive(value: false);
		}
		return component;
	}

	public void RemoveSuggestion(PolyTwitchSuggestionSlot slot)
	{
		if (slot == null)
		{
			Debug.LogWarningFormat("Trying to remove a null PolyTwitchSuggestionSlot");
			return;
		}
		slot.gameObject.SetActive(value: false);
		Object.Destroy(slot.gameObject);
	}

	public void MoveAllSuggestionsToBackUp()
	{
		if (m_ContentRectTransform.transform.childCount == 0)
		{
			return;
		}
		string layoutHash = m_ContentRectTransform.transform.GetChild(0).transform.GetComponent<PolyTwitchSuggestionSlot>().m_Suggestion.m_LayoutHash;
		PolyTwitchSuggestionsParent polyTwitchSuggestionsParent = FindExistingSuggestionsParent(layoutHash);
		if (!polyTwitchSuggestionsParent)
		{
			polyTwitchSuggestionsParent = CreateSuggestionsParent(layoutHash, m_BackedUpSuggestions.transform);
			m_BackupLayoutHashes.Add(layoutHash);
		}
		if (!(polyTwitchSuggestionsParent == null))
		{
			for (int num = m_ContentRectTransform.transform.childCount - 1; num >= 0; num--)
			{
				m_ContentRectTransform.transform.GetChild(num).transform.SetParent(polyTwitchSuggestionsParent.transform);
			}
			polyTwitchSuggestionsParent.RemoveSuggestions();
			if (m_BackupLayoutHashes.Count > 2)
			{
				ClearBackupForLayoutHash(m_BackupLayoutHashes[0]);
				m_BackupLayoutHashes.RemoveAt(0);
			}
		}
	}

	public void RestoreSuggestionsFromBackUp(string layoutHash)
	{
		PolyTwitchSuggestionsParent polyTwitchSuggestionsParent = FindExistingSuggestionsParent(layoutHash);
		if (polyTwitchSuggestionsParent == null)
		{
			return;
		}
		for (int num = polyTwitchSuggestionsParent.transform.childCount - 1; num >= 0; num--)
		{
			polyTwitchSuggestionsParent.transform.GetChild(num).transform.SetParent(m_ContentRectTransform.transform);
		}
		foreach (Transform item in m_ContentRectTransform.transform)
		{
			PolyTwitchSuggestionSlot component = item.GetComponent<PolyTwitchSuggestionSlot>();
			if ((bool)component && !PolyTwitchSuggestions.m_Suggestions.Contains(component.m_Suggestion))
			{
				PolyTwitchSuggestions.m_Suggestions.Add(component.m_Suggestion);
			}
		}
		PolyTwitchSuggestions.SortAutoplayList();
	}

	public void ClearBackupForLayoutHash(string layoutHash)
	{
		for (int num = m_BackedUpSuggestions.transform.childCount - 1; num >= 0; num--)
		{
			PolyTwitchSuggestionsParent component = m_BackedUpSuggestions.transform.GetChild(num).GetComponent<PolyTwitchSuggestionsParent>();
			if (component != null && component.m_LayoutHash == layoutHash)
			{
				Object.Destroy(component.gameObject);
			}
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

	private PolyTwitchSuggestionsParent CreateSuggestionsParent(string layoutHash, Transform parent)
	{
		GameObject obj = Object.Instantiate(m_SuggestionsParentPrefab, parent);
		obj.name = $"{m_SuggestionsParentPrefab.name}_{layoutHash}";
		PolyTwitchSuggestionsParent component = obj.GetComponent<PolyTwitchSuggestionsParent>();
		component.m_LayoutHash = layoutHash;
		return component;
	}

	private PolyTwitchSuggestionsParent FindExistingSuggestionsParent(string layoutHash)
	{
		foreach (Transform item in m_BackedUpSuggestions.transform)
		{
			PolyTwitchSuggestionsParent component = item.GetComponent<PolyTwitchSuggestionsParent>();
			if (component != null && component.m_LayoutHash == layoutHash)
			{
				return component;
			}
		}
		return null;
	}

	private void OnRemoveAll()
	{
		PopUpMessage.DisplayConfirmation(Localize.Get("CONFIRM_REMOVE_ALL_SUGGESTIONS"), useYesNoLabels: true, ConfirmRemoveAll);
	}

	private void ConfirmRemoveAll()
	{
		PolyTwitchSuggestions.DeleteAll();
	}
}
