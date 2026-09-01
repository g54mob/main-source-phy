using System.Collections.Generic;
using UnityEngine;

public class LeaderboardSlots
{
	public List<LeaderboardSlot> m_Slots = new List<LeaderboardSlot>();

	private Transform m_SlotsParent;

	private GameObject m_SlotPrefab;

	public LeaderboardSlots(GameObject prefab, Transform parent)
	{
		m_SlotsParent = parent;
		m_SlotPrefab = prefab;
	}

	public void AddSlots(GameLeaderboardEntry[] leaderboardEntries, LeaderboardFilterState filter)
	{
		if (leaderboardEntries != null)
		{
			foreach (GameLeaderboardEntry leaderboardEntry in leaderboardEntries)
			{
				AddSlot(leaderboardEntry, filter);
			}
			SetDefaultHighlightColors();
			HighlightPlayerSlot(filter);
		}
	}

	public LeaderboardSlot GetSlotWithOwnerId(string id)
	{
		foreach (LeaderboardSlot slot in m_Slots)
		{
			if (slot.m_OwnerId == id)
			{
				return slot;
			}
		}
		return null;
	}

	public void DestroyAll()
	{
		foreach (LeaderboardSlot slot in m_Slots)
		{
			Object.Destroy(slot.gameObject);
		}
		m_Slots.Clear();
	}

	public void AddFriendSlots(GameLeaderboardEntry[] leaderboardEntries, LeaderboardFilterState filter)
	{
		if (leaderboardEntries != null)
		{
			foreach (GameLeaderboardEntry leaderboardEntry in leaderboardEntries)
			{
				AddSlot(leaderboardEntry, filter);
			}
		}
	}

	public LeaderboardSlot AddSlot(GameLeaderboardEntry leaderboardEntry, LeaderboardFilterState filter)
	{
		LeaderboardSlot leaderboardSlot = InstantiateSlot(m_SlotPrefab, m_SlotsParent);
		if (leaderboardSlot != null)
		{
			leaderboardSlot.gameObject.SetActive(value: true);
			leaderboardSlot.Populate(filter, leaderboardEntry.GetGlobalRank(), leaderboardEntry.GetId(), leaderboardEntry.GetName(), leaderboardEntry.GetScore(), leaderboardEntry.HasBreaks());
		}
		return leaderboardSlot;
	}

	private void SortSlots()
	{
		m_Slots.Sort(SortLeaderboardSlotsByBudget);
		for (int i = 0; i < m_Slots.Count; i++)
		{
			m_Slots[i].transform.SetSiblingIndex(i);
		}
	}

	private int SortLeaderboardSlotsByBudget(LeaderboardSlot a, LeaderboardSlot b)
	{
		return a.m_Score.CompareTo(b.m_Score);
	}

	private int SortByRank(LeaderboardSlot a, LeaderboardSlot b)
	{
		return a.m_Rank.CompareTo(b.m_Rank);
	}

	public void SetDefaultHighlightColors()
	{
		for (int i = 0; i < m_SlotsParent.childCount; i++)
		{
			m_SlotsParent.GetChild(i).GetComponent<LeaderboardSlot>().m_Highlight.color = Color.clear;
		}
	}

	public void HighlightPlayerSlot(LeaderboardFilterState filter)
	{
		string steamId = SteamUtils.GetSteamId();
		foreach (LeaderboardSlot slot in m_Slots)
		{
			if (steamId == slot.m_OwnerId)
			{
				slot.m_Highlight.color = GameUI.m_Instance.m_LeaderboardDefaultHighlightColor;
				slot.m_RankText.text = GameUI.MarkupForBlack(GameLeaderboards.FormatRank(slot.m_Rank));
				slot.m_OwnedBy.text = GameUI.MarkupForBlack(slot.m_OwnedBy.text);
				slot.m_ScoreText.text = GameUI.MarkupForBlack(GameLeaderboards.FormatScore(slot.m_Score, filter));
				slot.m_BreakIcon.color = Color.black;
			}
		}
	}

	public int GetPlayerScore()
	{
		string steamId = SteamUtils.GetSteamId();
		foreach (LeaderboardSlot slot in m_Slots)
		{
			if (steamId == slot.m_OwnerId)
			{
				return slot.m_Score;
			}
		}
		return 0;
	}

	public int GetPlayerScoreIndex()
	{
		string steamId = SteamUtils.GetSteamId();
		for (int i = 0; i < m_Slots.Count; i++)
		{
			if (steamId == m_Slots[i].m_OwnerId)
			{
				return i;
			}
		}
		return -1;
	}

	public bool HasSlotThatBreaks()
	{
		foreach (LeaderboardSlot slot in m_Slots)
		{
			if (slot.m_BreakIcon.gameObject.activeInHierarchy)
			{
				return true;
			}
		}
		return false;
	}

	private LeaderboardSlot InstantiateSlot(GameObject prefab, Transform parent)
	{
		GameObject gameObject = Object.Instantiate(prefab, parent);
		if (gameObject == null)
		{
			return null;
		}
		LeaderboardSlot component = gameObject.GetComponent<LeaderboardSlot>();
		if (component != null)
		{
			component.gameObject.SetActive(value: false);
			m_Slots.Add(component);
		}
		return component;
	}

	private LeaderboardSlot GetFirstInactiveSlot()
	{
		foreach (LeaderboardSlot slot in m_Slots)
		{
			if (!slot.gameObject.activeSelf)
			{
				return slot;
			}
		}
		return null;
	}
}
