using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class CampaignCreatorCampaignCellUI : MonoBehaviour
	{
		private TextMeshProUGUI m_CampaignLevelText;

		private Button m_Button;

		public TABSCampaignAsset CampaignWrapper { get; private set; }

		public void Init(TABSCampaignAsset wrap, UnityAction OnClickAction)
		{
			m_Button = GetComponent<Button>();
			m_CampaignLevelText = GetComponentInChildren<TextMeshProUGUI>();
			CampaignWrapper = wrap;
			string text = wrap.Entity.Name;
			m_CampaignLevelText.text = text;
			m_Button.onClick.AddListener(OnClickAction);
		}
	}
}
