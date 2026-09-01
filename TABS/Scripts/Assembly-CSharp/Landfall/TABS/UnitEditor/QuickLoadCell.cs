using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class QuickLoadCell : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		public Image iconImage;

		private UnitBlueprint unit;

		public void Setup(UnitBlueprint unit)
		{
			this.unit = unit;
			iconImage.sprite = unit.Entity.SpriteIcon;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			UnitEditorManager unitEditorManager = Object.FindObjectOfType<UnitEditorManager>();
			unitEditorManager.LoadUnit(unit);
			unitEditorManager.UIManager.NavigateToPage("UNIT");
		}
	}
}
