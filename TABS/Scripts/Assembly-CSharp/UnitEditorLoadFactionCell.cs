using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorLoadFactionCell : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public Image icon;

	public TextMeshProUGUI nameText;

	private Faction faction;

	private UnitEditorLoadGrid loadGrid;

	public void Init(Faction faction, UnitEditorLoadGrid grid)
	{
		this.faction = faction;
		nameText.text = faction.Entity.Name;
		icon.sprite = faction.Entity.SpriteIcon;
		loadGrid = grid;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		loadGrid.SelectFaction(faction);
	}
}
