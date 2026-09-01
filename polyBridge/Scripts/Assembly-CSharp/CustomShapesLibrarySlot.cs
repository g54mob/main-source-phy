using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomShapesLibrarySlot
{
	public CustomShapesLibrarySlotType m_SlotType;

	public string m_DisplayNamLocID;

	public Sprite m_Sprite;

	public string m_FullyQualifiedPath;

	public List<string> m_Filenames = new List<string>();

	public CustomShapesLibrarySlot(CustomShapesLibrarySlotType slotType, string displayNameLocID, Sprite sprite, string path, FileInfo[] fileInfos)
	{
		m_SlotType = slotType;
		m_DisplayNamLocID = displayNameLocID;
		m_Sprite = sprite;
		m_FullyQualifiedPath = path;
		foreach (FileInfo fileInfo in fileInfos)
		{
			m_Filenames.Add(fileInfo.Name);
		}
	}
}
