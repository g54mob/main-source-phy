using TMPro;
using UnityEngine;

public class WorldMapIslandToolTip : MonoBehaviour
{
	public TextMeshProUGUI m_WorldName;

	public PanelResizeHorizontal m_PanelResizeHorizontal;

	[Header("Roots")]
	public GameObject m_UnLockedRoot;

	public GameObject m_LockedRoot;

	[Header("UnLocked")]
	public TextMeshProUGUI m_Difficulty;

	public TextMeshProUGUI m_WorldSubtitle;

	public GameObject[] m_Buoys;

	[Header("Locked")]
	public TextMeshProUGUI m_LockedInfo;

	public void Enable(WorldMapIsland island)
	{
		base.gameObject.SetActive(value: true);
		Vector2 screenPos = island.transform.position;
		GameUI.SetScreenPosClamped(base.gameObject, screenPos, 0f, 0f);
		InitFields(island);
		m_PanelResizeHorizontal.ForceUpdate();
	}

	public void Disable()
	{
		base.gameObject.SetActive(value: false);
	}

	private void InitFields(WorldMapIsland island)
	{
		m_UnLockedRoot.SetActive(island.IsUnLocked());
		m_LockedRoot.SetActive(!island.IsUnLocked());
		m_WorldName.text = island.GetDisplayName();
		m_WorldSubtitle.text = island.GetSubTitle();
		m_Difficulty.text = Campaign.FormatDifficultyLabel(island.GetNumStars());
		if (island.IsLocked() && island.GetNumStars() < 5)
		{
			m_LockedInfo.text = Campaign.FormatUnlockHelp(island.GetNumStars());
		}
		else
		{
			m_LockedInfo.text = string.Empty;
		}
	}
}
