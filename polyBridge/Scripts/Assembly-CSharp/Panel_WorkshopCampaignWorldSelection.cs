using UnityEngine;

public class Panel_WorkshopCampaignWorldSelection : MonoBehaviour
{
	public WorldMapIsland[] m_Islands;

	public WorldMapIslandToolTip m_IslandToolTip;

	private void Awake()
	{
		m_IslandToolTip.gameObject.SetActive(value: false);
	}

	public void SetWorkshopCampaignWorld(int index, WorkshopCampaignWorld world)
	{
		if (index < m_Islands.Length)
		{
			m_Islands[index].SetWorkshopCampaignWorld(world);
		}
	}

	public void SetIconPosition(int index, Vector2 anchoredPosition)
	{
		if (index < m_Islands.Length)
		{
			m_Islands[index].GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
		}
	}

	public void SetIconDefaultPosition(int index)
	{
		if (index < m_Islands.Length)
		{
			m_Islands[index].GetComponent<RectTransform>().anchoredPosition = m_Islands[index].m_OriginalAnchoredPosition;
		}
	}

	public void EnableWorld(int index, bool active)
	{
		if (index < m_Islands.Length)
		{
			m_Islands[index].gameObject.SetActive(active);
		}
	}

	public void DisableAllWorlds()
	{
		WorldMapIsland[] islands = m_Islands;
		for (int i = 0; i < islands.Length; i++)
		{
			islands[i].gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		m_IslandToolTip.gameObject.SetActive(value: false);
		WorldMapIsland[] islands = m_Islands;
		foreach (WorldMapIsland worldMapIsland in islands)
		{
			if (worldMapIsland.IsUnderPointer())
			{
				m_IslandToolTip.Enable(worldMapIsland);
				break;
			}
		}
	}
}
