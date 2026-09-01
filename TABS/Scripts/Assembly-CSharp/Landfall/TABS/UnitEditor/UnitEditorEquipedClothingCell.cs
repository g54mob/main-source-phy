using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorEquipedClothingCell : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
	{
		[SerializeField]
		private Image icon;

		[SerializeField]
		private LocalizeText nameText;

		private UnitEditorManager.EquipedClothingWrapper wrapper;

		private UnitEditorManager manager;

		public void Initialize(UnitEditorManager.EquipedClothingWrapper clothingWrapper, UnitEditorManager manager)
		{
			wrapper = clothingWrapper;
			icon.sprite = wrapper.spawnedProp.Entity.SpriteIcon;
			nameText.LocaleID = wrapper.prop.DisplayName;
			this.manager = manager;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			CustomizeItem();
		}

		public void OnSubmit(BaseEventData eventData)
		{
			if (!UnitEditorManager.isTestingUnit)
			{
				CustomizeItem();
			}
		}

		private void CustomizeItem()
		{
			manager.UIManager.NavigateToPage("EQUIPEDCLOTHING");
			manager.UIManager.SetupEquipedClothing(wrapper);
		}
	}
}
