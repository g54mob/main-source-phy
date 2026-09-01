using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class FactionUnitCellUI : MonoBehaviour
	{
		private TextMeshProUGUI m_UnitNameText;

		private Button m_Button;

		[SerializeField]
		private Button m_UpButton;

		[SerializeField]
		private Button m_DownButton;

		public bool IsSelected { get; private set; }

		public UnitBlueprint UnitBlueprint { get; private set; }

		public string UnitName { get; private set; }

		public GameObject VisualUnit { get; private set; }

		public void Init(UnitBlueprint blueprint, UnityAction OnClickAction, bool isSelected = false, UnityAction OnUpArrowClick = null, UnityAction OnDownArrowClick = null)
		{
			UnitBlueprint = blueprint;
			UnitName = UnitBlueprint.Name;
			IsSelected = isSelected;
		}

		public void AssignVisualUnit(GameObject visualUnit)
		{
			VisualUnit = visualUnit;
		}

		private void OnDestroy()
		{
			if (VisualUnit != null)
			{
				Object.Destroy(VisualUnit);
			}
		}
	}
}
