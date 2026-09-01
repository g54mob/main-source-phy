using UnityEngine;

public class Panel_CampaignWorldSelection : MonoBehaviour
{
	public WorldMapIsland[] m_Islands;

	public WorldMapIslandToolTip m_IslandToolTip;

	public GameObject m_NormalWorldLayout;

	public GameObject m_SecretWorldLayout;

	private void Awake()
	{
		m_IslandToolTip.gameObject.SetActive(value: false);
		m_NormalWorldLayout.SetActive(value: true);
		m_SecretWorldLayout.SetActive(value: false);
	}

	private void OnEnable()
	{
		m_SecretWorldLayout.SetActive(GameManager.IsSecretWorldUnlocked());
		m_NormalWorldLayout.SetActive(!m_SecretWorldLayout.activeSelf);
	}

	public void UpdateManual()
	{
		m_SecretWorldLayout.SetActive(GameManager.IsSecretWorldUnlocked());
		m_NormalWorldLayout.SetActive(!m_SecretWorldLayout.activeSelf);
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
