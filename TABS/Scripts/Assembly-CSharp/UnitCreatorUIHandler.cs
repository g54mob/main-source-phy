using System.Collections.Generic;
using UnityEngine;

public class UnitCreatorUIHandler : MonoBehaviour
{
	[SerializeField]
	private GameObject m_LoadObject;

	[SerializeField]
	private GameObject m_SaveObject;

	[SerializeField]
	private GameObject m_FactionObject;

	[SerializeField]
	private GameObject m_BlurObject;

	private Dictionary<UnitCreatorScreen, ICampaignMenu> m_CampaignMenues;

	private ICampaignMenu m_CurrentScreen;

	public static UnitCreatorUIHandler Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		InitReferences();
		InitListeners();
	}

	private void InitReferences()
	{
		m_CampaignMenues = new Dictionary<UnitCreatorScreen, ICampaignMenu>();
		m_CampaignMenues.Add(UnitCreatorScreen.Load, m_LoadObject.GetComponent<ICampaignMenu>());
		m_CampaignMenues.Add(UnitCreatorScreen.Save, m_SaveObject.GetComponent<ICampaignMenu>());
		m_CampaignMenues.Add(UnitCreatorScreen.Faction, m_FactionObject.GetComponent<ICampaignMenu>());
	}

	private void InitListeners()
	{
	}

	public void Toggle(UnitCreatorScreen screen)
	{
		foreach (KeyValuePair<UnitCreatorScreen, ICampaignMenu> campaignMenue in m_CampaignMenues)
		{
			if (campaignMenue.Key != screen)
			{
				campaignMenue.Value.Close();
			}
		}
		ICampaignMenu campaignMenu = m_CampaignMenues[screen];
		campaignMenu.Toggle();
		m_BlurObject.SetActive(campaignMenu.IsOpen());
	}
}
