using System;
using System.Collections.Generic;
using UnityEngine;

public class PolyTwitchSuggestions
{
	public static List<PolyTwitchSuggestion> m_Suggestions = new List<PolyTwitchSuggestion>();

	public static List<PolyTwitchSuggestion> m_AutoplaySuggestions = new List<PolyTwitchSuggestion>();

	public static readonly int MAX_SUGGESTIONS = 999;

	public static PolyTwitchSuggestion Create(string username, string ownerId, string fileId, BridgeSaveData saveData, string bridgeHash, string layoutHash, PolyTwitchSuggestionTag tag, int numBitsUsed)
	{
		PolyTwitchSuggestion polyTwitchSuggestion = new PolyTwitchSuggestion();
		if (polyTwitchSuggestion == null)
		{
			Debug.LogWarningFormat("Failed to allocate PolyTwitchSuggstion");
			return null;
		}
		polyTwitchSuggestion.m_Username = username;
		polyTwitchSuggestion.m_OwnerId = ownerId;
		polyTwitchSuggestion.m_FileId = fileId;
		polyTwitchSuggestion.m_LayoutHash = layoutHash;
		polyTwitchSuggestion.m_Tag = tag;
		polyTwitchSuggestion.m_DateTime = DateTime.Now;
		polyTwitchSuggestion.m_BridgeSaveData = saveData;
		polyTwitchSuggestion.m_BridgeHash = bridgeHash;
		polyTwitchSuggestion.m_Muted = false;
		polyTwitchSuggestion.m_NumBitsUsed = numBitsUsed;
		polyTwitchSuggestion.m_Slot = GameUI.m_Instance.m_PolyTwitchMain.m_SuggestionsPanel.AddSuggestion(polyTwitchSuggestion);
		m_Suggestions.Insert(0, polyTwitchSuggestion);
		UpdateSuggestionPositionInList(polyTwitchSuggestion);
		return polyTwitchSuggestion;
	}

	public static void Delete(PolyTwitchSuggestion suggestion)
	{
		GameUI.m_Instance.m_PolyTwitchMain.m_SuggestionsPanel.RemoveSuggestion(suggestion.m_Slot);
		if (m_Suggestions.Contains(suggestion))
		{
			m_Suggestions.Remove(suggestion);
		}
		SortAutoplayList();
	}

	public static void DeleteAll()
	{
		foreach (PolyTwitchSuggestion suggestion in m_Suggestions)
		{
			GameUI.m_Instance.m_PolyTwitchMain.m_SuggestionsPanel.RemoveSuggestion(suggestion.m_Slot);
		}
		m_Suggestions.Clear();
		SortAutoplayList();
	}

	public static void SortAutoplayList()
	{
		m_AutoplaySuggestions.Clear();
		int num = -1;
		int index = 0;
		for (int i = 0; i < m_Suggestions.Count; i++)
		{
			if (num != m_Suggestions[i].m_NumBitsUsed)
			{
				index = i;
				num = m_Suggestions[i].m_NumBitsUsed;
			}
			m_AutoplaySuggestions.Insert(index, m_Suggestions[i]);
		}
	}

	public static int GetNumberOfUnseenNotifications()
	{
		int num = 0;
		foreach (PolyTwitchSuggestion suggestion in m_Suggestions)
		{
			if (!suggestion.HasBeenViewed() && !suggestion.m_Muted)
			{
				num++;
			}
		}
		return num;
	}

	public static PolyTwitchSuggestion GetOldestUnViewedSuggestion()
	{
		foreach (PolyTwitchSuggestion suggestion in m_Suggestions)
		{
			if (!suggestion.HasBeenViewed() && !suggestion.m_Muted)
			{
				return suggestion;
			}
		}
		return null;
	}

	public static PolyTwitchSuggestion GetFirstAutoplaySuggestion()
	{
		foreach (PolyTwitchSuggestion autoplaySuggestion in m_AutoplaySuggestions)
		{
			if (!autoplaySuggestion.HasBeenViewed() && !autoplaySuggestion.m_Muted)
			{
				return autoplaySuggestion;
			}
		}
		return null;
	}

	public static PolyTwitchSuggestion GetOldestSuggestionThatHasNotPassedOrFailed()
	{
		foreach (PolyTwitchSuggestion suggestion in m_Suggestions)
		{
			if (!suggestion.HasPassedOrFailed() && !suggestion.m_Muted)
			{
				return suggestion;
			}
		}
		return null;
	}

	public static PolyTwitchSuggestion GetOldestSuggestion()
	{
		foreach (PolyTwitchSuggestion suggestion in m_Suggestions)
		{
			if (!suggestion.m_Muted)
			{
				return suggestion;
			}
		}
		return null;
	}

	public static void RemoveOldestSuggestion()
	{
		PolyTwitchSuggestion oldestSuggestion = GetOldestSuggestion();
		if (oldestSuggestion != null)
		{
			Delete(oldestSuggestion);
		}
	}

	public static bool SuggestionFromSameOwnerExists(string ownerId, string bridgeHash)
	{
		foreach (PolyTwitchSuggestion suggestion in m_Suggestions)
		{
			if (suggestion.m_OwnerId == ownerId && Utils.MD5HashesMatch(suggestion.m_BridgeHash, bridgeHash))
			{
				return true;
			}
		}
		return false;
	}

	public static void UpdateSuggestionTimeAndBits(string ownerId, string bridgeHash, int numBitsUsed)
	{
		foreach (PolyTwitchSuggestion suggestion in m_Suggestions)
		{
			if (suggestion.m_OwnerId == ownerId && Utils.MD5HashesMatch(suggestion.m_BridgeHash, bridgeHash))
			{
				if (numBitsUsed > suggestion.m_NumBitsUsed)
				{
					suggestion.SetStatus(PolyTwitchSuggestionStatus.UNVIEWED);
					suggestion.m_NumBitsUsed = numBitsUsed;
				}
				suggestion.m_DateTime = DateTime.Now;
				suggestion.UpdateSlotDisplay();
				UpdateSuggestionPositionInList(suggestion);
				break;
			}
		}
	}

	public static PolyTwitchSuggestion GetNextAutoplayThatHasNotBeenSimulated(PolyTwitchSuggestion suggestion)
	{
		for (int i = 0; i < m_AutoplaySuggestions.Count; i++)
		{
			if (!m_AutoplaySuggestions[i].HasBeenSimulated() && !m_AutoplaySuggestions[i].m_Muted)
			{
				return m_AutoplaySuggestions[i];
			}
		}
		return null;
	}

	public static int GetNumberOfAutoplaySuggestionsFollowing(PolyTwitchSuggestion suggestion)
	{
		int num = m_AutoplaySuggestions.IndexOf(suggestion);
		if (num == -1)
		{
			return 0;
		}
		int num2 = 0;
		for (int i = num + 1; i < m_AutoplaySuggestions.Count; i++)
		{
			if (!m_AutoplaySuggestions[i].HasPassedOrFailed() && !m_AutoplaySuggestions[i].m_Muted)
			{
				num2++;
			}
		}
		return num2;
	}

	public static PolyTwitchSuggestion GetPrevSuggestion(PolyTwitchSuggestion suggestion)
	{
		int num = m_Suggestions.IndexOf(suggestion) - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (!m_Suggestions[num].m_Muted)
			{
				return m_Suggestions[num];
			}
		}
		return null;
	}

	public static PolyTwitchSuggestion GetNextSuggestion(PolyTwitchSuggestion suggestion)
	{
		int num = m_Suggestions.IndexOf(suggestion) + 1;
		for (int i = num; i < m_Suggestions.Count; i++)
		{
			if (!m_Suggestions[num].m_Muted)
			{
				return m_Suggestions[num];
			}
		}
		return null;
	}

	public static void HideSuggestionsWithOwnerId(string hashId, bool hide)
	{
		foreach (PolyTwitchSuggestion suggestion in m_Suggestions)
		{
			if (suggestion.m_OwnerId == hashId)
			{
				suggestion.m_Muted = hide;
				suggestion.m_Slot.gameObject.SetActive(!hide);
			}
		}
	}

	public static void UpdateSuggestionPositionInList(PolyTwitchSuggestion suggestion)
	{
		m_Suggestions.Remove(suggestion);
		int num = m_Suggestions.Count;
		for (int i = 0; i < m_Suggestions.Count; i++)
		{
			if (m_Suggestions[i].HasBeenViewed())
			{
				num = i;
				break;
			}
		}
		int num2 = (suggestion.HasBeenViewed() ? m_Suggestions.Count : num);
		for (int j = 0; j < m_Suggestions.Count; j++)
		{
			if (suggestion.HasBeenViewed() != m_Suggestions[j].HasBeenViewed())
			{
				continue;
			}
			if (suggestion.HasBeenViewed())
			{
				if (suggestion.m_DateTime >= m_Suggestions[j].m_DateTime)
				{
					num2 = j;
					break;
				}
			}
			else if (suggestion.m_NumBitsUsed >= m_Suggestions[j].m_NumBitsUsed)
			{
				num2 = j;
				break;
			}
		}
		m_Suggestions.Insert(num2, suggestion);
		suggestion.m_Slot.transform.SetSiblingIndex(num2);
		SortAutoplayList();
	}

	public static int GetHighestUnviewedBitCount()
	{
		int num = 0;
		for (int i = 0; i < m_Suggestions.Count; i++)
		{
			if (!m_Suggestions[i].HasBeenViewed())
			{
				num = Mathf.Max(num, m_Suggestions[i].m_NumBitsUsed);
			}
		}
		return num;
	}
}
