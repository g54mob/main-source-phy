using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorEquipedAbilityCell : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
	{
		public Image icon;

		public LocalizeText nameText;

		private UnitEditorManager.EquipedSpecialAbility wrapper;

		private UnitEditorManager manager;

		public void Initialize(UnitEditorManager.EquipedSpecialAbility wrapper, UnitEditorManager manager)
		{
			this.wrapper = wrapper;
			icon.sprite = wrapper.spawnedProp.Entity.SpriteIcon;
			nameText.LocaleID = wrapper.prop.DisplayName;
			this.manager = manager;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			ShowAbility();
		}

		public void OnSubmit(BaseEventData eventData)
		{
			if (!UnitEditorManager.isTestingUnit)
			{
				ShowAbility();
			}
		}

		private void ShowAbility()
		{
			manager.UIManager.NavigateToPage("EQUIPEDCLOTHING");
			manager.UIManager.SetupEquipedAbility(wrapper);
		}
	}
}
