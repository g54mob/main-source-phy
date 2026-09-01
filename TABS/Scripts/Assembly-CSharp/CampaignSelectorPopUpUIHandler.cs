using TMPro;
using UnityEngine;

public class CampaignSelectorPopUpUIHandler : MonoBehaviour
{
	private CodeAnimation m_Animation;

	private TextMeshProUGUI m_PopUpText;

	private bool m_PoppedIn;

	private CampaignInfo m_CampaignInfo;

	private RectTransform m_Transform;

	private void Awake()
	{
		m_Animation = GetComponent<CodeAnimation>();
		m_PopUpText = GetComponent<TextMeshProUGUI>();
		m_Transform = GetComponent<RectTransform>();
		m_PoppedIn = false;
	}

	public void PopIn(CampaignInfo campaignInfo, float yValue)
	{
		m_CampaignInfo = campaignInfo;
		Vector3 position = m_Transform.position;
		position.y = yValue;
		m_Transform.position = position;
		ChangeValues();
		if (m_PoppedIn)
		{
			m_Animation.PlayBoop();
		}
		else
		{
			m_Animation.PlayIn();
		}
		m_PoppedIn = true;
	}

	public void PopOut()
	{
		m_PoppedIn = false;
		m_CampaignInfo.Description = string.Empty;
		m_Animation.PlayOut();
	}

	public void Hide()
	{
		m_Transform.localScale = Vector3.zero;
	}

	private void ChangeValues()
	{
		m_PopUpText.text = m_CampaignInfo.Description;
	}
}
