using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class UnitCellUI : MonoBehaviour
	{
		private TextMeshProUGUI m_UnitNameText;

		private Button m_Button;

		[SerializeField]
		private Button m_UpButton;

		[SerializeField]
		private Button m_DownButton;

		public UnitBlueprint UnitWrapper { get; private set; }

		public string UnitName { get; private set; }

		public void Init(UnitBlueprint wrapper, UnityAction OnClickAction)
		{
			m_Button = GetComponent<Button>();
			m_UnitNameText = GetComponentInChildren<TextMeshProUGUI>();
			UnitWrapper = wrapper;
			UnitName = wrapper.Entity.Name;
			m_UnitNameText.text = (wrapper.IsCustomUnit ? UnitName : ("[LOCAL] " + UnitName));
			m_Button.onClick.AddListener(OnClickAction);
		}
	}
}
