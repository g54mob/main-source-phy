using System;
using System.Collections.Generic;
using UnityEngine;

public class Panel_FileLoader : MonoBehaviour
{
	public GameObject m_Content;

	public GameObject m_FileSlotPrefab;

	public bool m_HighlightOnHover;

	public Color m_ZebraColor = new Color(0.19215687f, 0.23137255f, 0.23921569f);

	private Color m_NoColor = new Color(0f, 0f, 0f, 0f);

	[NonSerialized]
	public List<FileSlot> m_Slots = new List<FileSlot>();

	public FileSlot AddSlot(string filename, long lastWriteTimeTicks, string displayName, FileSlot.OnClickedDelegate clickCallback, FileSlot.OnHoverChangeDelegate hoverCallback)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(m_FileSlotPrefab, m_Content.transform);
		if (!gameObject)
		{
			return null;
		}
		FileSlot component = gameObject.GetComponent<FileSlot>();
		if ((bool)component)
		{
			component.name = m_FileSlotPrefab.name;
			component.m_FileName = filename;
			component.m_LastWriteTimeTicks = lastWriteTimeTicks;
			GameUI.SetAndEnableText(component.m_DisplayName, displayName);
			component.m_SelectedHighlight.gameObject.SetActive(value: false);
			component.SetOnClickedCallback(clickCallback);
			if (m_HighlightOnHover)
			{
				component.EnableHighlightOnHover();
				component.SetOnHoverChangeCallback(hoverCallback);
			}
			m_Slots.Add(component);
			if (component.m_Background != null)
			{
				component.m_Background.color = ((m_Slots.IndexOf(component) % 2 == 1) ? m_ZebraColor : m_NoColor);
			}
		}
		return component;
	}

	public FileSlot GetNewestSlot()
	{
		if (m_Slots.Count == 0)
		{
			return null;
		}
		FileSlot fileSlot = m_Slots[0];
		foreach (FileSlot slot in m_Slots)
		{
			if (slot.m_LastWriteTimeTicks > fileSlot.m_LastWriteTimeTicks)
			{
				fileSlot = slot;
			}
		}
		return fileSlot;
	}

	public FileSlot GetSlotWhenPointerOverInfo()
	{
		foreach (FileSlot slot in m_Slots)
		{
			if (slot.m_InfoButton.gameObject.activeInHierarchy && slot.m_InfoPointerEvents.m_IsHovering)
			{
				return slot;
			}
		}
		return null;
	}

	public void DeleteSlot(FileSlot slot)
	{
		if (m_Slots.Contains(slot))
		{
			UnityEngine.Object.Destroy(slot.gameObject);
			m_Slots.Remove(slot);
		}
		MatchLayoutWithSlots();
	}

	public FileSlot FindSlotByIndex(int index)
	{
		if (index < 0 || index >= m_Slots.Count)
		{
			return null;
		}
		return m_Slots[index];
	}

	public FileSlot FindSlotByFilename(string filename)
	{
		foreach (FileSlot slot in m_Slots)
		{
			if (slot.m_FileName == filename)
			{
				return slot;
			}
		}
		return null;
	}

	public FileSlot FindSlotByDisplayName(string name)
	{
		foreach (FileSlot slot in m_Slots)
		{
			if (slot.m_DisplayName.text == name)
			{
				return slot;
			}
		}
		return null;
	}

	public FileSlot GetFirstSlot()
	{
		if (m_Slots.Count <= 0)
		{
			return null;
		}
		return m_Slots[0];
	}

	public int GetSlotIndex(FileSlot slot)
	{
		return m_Slots.IndexOf(slot);
	}

	public void SelectSlotIndex(int index)
	{
		if (index < m_Slots.Count)
		{
			SelectSlot(m_Slots[index]);
		}
	}

	public void SelectSlot(FileSlot slot)
	{
		if (slot != null)
		{
			UnSelectAllSlots();
			slot.m_SelectedHighlight.gameObject.SetActive(value: true);
		}
	}

	public void DestroySlots()
	{
		for (int i = 0; i < m_Content.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(m_Content.transform.GetChild(i).gameObject);
		}
		m_Slots.Clear();
	}

	public void UnSelectAllSlots()
	{
		foreach (FileSlot slot in m_Slots)
		{
			slot.m_SelectedHighlight.gameObject.SetActive(value: false);
		}
	}

	public int NumSlots()
	{
		return m_Slots.Count;
	}

	public void SortByDate()
	{
		m_Slots.Sort(SortByDate);
		MatchLayoutWithSlots();
	}

	public void SortAlphabetically()
	{
		m_Slots.Sort(SortByName);
		MatchLayoutWithSlots();
	}

	public void MatchLayoutWithSlots()
	{
		for (int i = 0; i < m_Slots.Count; i++)
		{
			m_Slots[i].transform.SetSiblingIndex(i);
			if (m_Slots[i].m_Background != null)
			{
				m_Slots[i].m_Background.color = ((i % 2 == 1) ? m_ZebraColor : m_NoColor);
			}
		}
	}

	private int SortByDate(FileSlot a, FileSlot b)
	{
		if (a.m_IsDirectory == b.m_IsDirectory)
		{
			return b.m_LastWriteTimeTicks.CompareTo(a.m_LastWriteTimeTicks);
		}
		return b.m_IsDirectory.CompareTo(a.m_IsDirectory);
	}

	private int SortByName(FileSlot a, FileSlot b)
	{
		if (a.m_IsDirectory == b.m_IsDirectory)
		{
			return a.m_DisplayName.text.CompareTo(b.m_DisplayName.text);
		}
		return b.m_IsDirectory.CompareTo(a.m_IsDirectory);
	}
}
