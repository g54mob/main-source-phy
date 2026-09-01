using TMPro;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorRiderSelectCell : UnitEditorSelectableListItem
	{
		public UnitButtonBase m_UnitButton;

		public TextMeshProUGUI m_unitName;

		private UnitBlueprint m_unit;

		public void Setup(UnitBlueprint unit)
		{
			m_unit = unit;
			base.gameObject.SetActive(value: true);
			string text = (string.IsNullOrEmpty(m_unit.Entity.Name) ? m_unit.name : m_unit.Entity.Name);
			m_unitName.text = text;
			m_UnitButton.Setup(unit);
		}

		public void SelectUnit()
		{
			UnitEditorManager unitEditorManager = Object.FindObjectOfType<UnitEditorManager>();
			if (unitEditorManager != null)
			{
				unitEditorManager.SetRider(m_unit);
				UnitEditorUIManager unitEditorUIManager = unitEditorManager.unitEditorUIManager;
				if (unitEditorUIManager != null)
				{
					unitEditorUIManager.NavigateToPage("UNIT");
				}
			}
		}

		public override bool ValidInFilter(string filter)
		{
			if (m_unitName.text.ToLower().Contains(filter.ToLower()))
			{
				return true;
			}
			return false;
		}
	}
}
