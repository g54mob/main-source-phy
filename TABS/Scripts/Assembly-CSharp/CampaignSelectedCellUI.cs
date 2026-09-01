using Landfall.TABS.Workshop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CampaignSelectedCellUI : MonoBehaviour
{
	private Button m_Button;

	private int m_campaignIndex;

	public TABSCampaignLevelAsset CampaignLevelReference { get; private set; }

	private float m_ButtonYValue => m_Button.transform.position.y;

	public int CampaignIndex => m_campaignIndex;

	public void Init(int index, TABSCampaignLevelAsset sequence, UnityAction onClickAction, bool unlocked, bool newLevel)
	{
		m_Button = GetComponent<Button>();
		m_campaignIndex = index;
		CampaignLevelReference = sequence;
		if (unlocked)
		{
			m_Button.onClick.AddListener(onClickAction);
			if (newLevel)
			{
				base.gameObject.FetchComponent<ScaleJiggleAnimation>().Play();
			}
		}
		else
		{
			m_Button.interactable = false;
			GetComponent<MapGrid>().Lock();
			base.gameObject.FetchComponent<UIScaleJiggle>().enabled = false;
			base.gameObject.FetchComponent<UISounds>().enabled = false;
		}
	}
}
