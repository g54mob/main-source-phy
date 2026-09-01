using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CampaignPlayCellUI : MonoBehaviour
{
	private TextMeshProUGUI m_CampaignNameText;

	private Button m_Button;

	private TABSCampaignAsset m_currentCampaign;

	private float m_ButtonYValue => m_Button.transform.position.y;

	public TABSCampaignAsset CurrentCampaign => m_currentCampaign;

	public void Init(TABSCampaignAsset campaign, UnityAction onClickAction)
	{
		m_CampaignNameText = GetComponentInChildren<TextMeshProUGUI>();
		m_Button = GetComponent<Button>();
		m_CampaignNameText.text = campaign.Entity.Name;
		m_Button.onClick.AddListener(onClickAction);
		m_currentCampaign = campaign;
	}
}
