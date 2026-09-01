using TMPro;
using UnityEngine;

namespace Landfall.TABS
{
	public class PreviewUnitInformation : MonoBehaviour
	{
		[Header("Text Refrences")]
		public TextMeshProUGUI m_UnitNameText;

		public TextMeshProUGUI m_CostText;

		public TextMeshProUGUI m_HealthText;

		public TextMeshProUGUI m_FactionText;

		public TextMeshProUGUI m_WeaponText;

		public TextMeshProUGUI m_ArmorText;

		public void SetUnitInformation(UnitBlueprint unit)
		{
			m_UnitNameText.text = unit.Name;
			m_CostText.text = "Cost: " + unit.GetUnitCost();
			m_HealthText.text = "Health: " + unit.health;
		}
	}
}
