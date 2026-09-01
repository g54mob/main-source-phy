using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorAbilityCell : UnitEditorItemCell
	{
		public Image icon;

		public override void Initialize(CharacterItem item, UnitEditorManager manager)
		{
			base.Initialize(item, manager);
			icon.sprite = item.Entity.SpriteIcon;
			nameText.LocaleID = item.DisplayName;
		}

		public override void OnPointerClick(PointerEventData eventData)
		{
			base.OnPointerClick(eventData);
			SelectAbilityItem();
		}

		private void SelectAbilityItem()
		{
			m_manager.EquipNewAbility(m_item);
			m_manager.UIManager.NavigateToPage("UNIT");
			m_manager.DestroyTemporary();
		}

		public override void OnSubmit(BaseEventData eventData)
		{
			base.OnSubmit(eventData);
			SelectAbilityItem();
		}
	}
}
