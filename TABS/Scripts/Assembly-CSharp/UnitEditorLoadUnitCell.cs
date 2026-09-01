using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorLoadUnitCell : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public UnitEditorConfirmLoadUI confirmLoadUI;

	public Image icon;

	public TextMeshProUGUI nameText;

	private UnitBlueprint unitBlueprint;

	public void Init(UnitBlueprint unitBlueprint)
	{
		this.unitBlueprint = unitBlueprint;
		nameText.text = unitBlueprint.Name;
		if (unitBlueprint.Entity.SpriteIcon != null)
		{
			icon.sprite = unitBlueprint.Entity.SpriteIcon;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Object.FindObjectOfType<UnitEditorManager>().LoadUnit(unitBlueprint);
		confirmLoadUI.SetupUI(unitBlueprint);
	}
}
