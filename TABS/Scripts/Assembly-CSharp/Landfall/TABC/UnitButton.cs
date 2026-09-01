using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABC
{
	public class UnitButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		public bool isSellButton;

		private TextMeshProUGUI unitNameText;

		private TextMeshProUGUI unitCostText;

		private Image unitImage;

		public UnitData data;

		private void Awake()
		{
			data = GetComponent<UnitData>();
			if (!isSellButton)
			{
				unitNameText = base.transform.Find("Name").GetComponentInChildren<TextMeshProUGUI>();
				unitCostText = base.transform.Find("Cost").GetComponentInChildren<TextMeshProUGUI>();
				unitImage = base.transform.Find("Image").GetComponentInChildren<Image>();
			}
		}

		private void Start()
		{
		}

		public void Clear()
		{
			unitNameText.text = "";
			unitCostText.text = "";
			unitImage.enabled = false;
			data.dataInstance.unit = null;
			data.dataInstance.level = 1;
		}

		public void SetUnit(UnitDataInstance unit, bool isOWned)
		{
			data.dataInstance.ownedByPlayer = isOWned;
			if (unit == null)
			{
				Clear();
				return;
			}
			if ((bool)unit.unit.unitBlueprint)
			{
				unitImage.sprite = unit.unit.unitBlueprint.Entity.SpriteIcon;
			}
			unitCostText.text = unit.unit.cost.ToString();
			if ((bool)unit.unit.unitBlueprint)
			{
				unitNameText.text = unit.unit.unitBlueprint.Entity.Name;
			}
			unitImage.enabled = true;
			data.dataInstance.unit = unit.unit;
			data.dataInstance.level = unit.level;
		}

		public void SetUnitBlueprint(SimulatedUnitBlueprint unit, bool isOWned)
		{
			data.dataInstance.ownedByPlayer = isOWned;
			if (unit == null)
			{
				Clear();
				return;
			}
			unitImage.sprite = unit.unitBlueprint.Entity.SpriteIcon;
			unitCostText.text = unit.cost.ToString();
			unitNameText.text = unit.unitBlueprint.Entity.Name;
			unitImage.enabled = true;
			data.dataInstance.unit = unit;
			data.dataInstance.level = 1;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			BoardManagerUI.instance.EnterUnitButton(GetComponent<UnitButton>());
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			BoardManagerUI.instance.ExitUnitButton();
		}
	}
}
