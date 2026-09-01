using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorClothingCell : UnitEditorItemCell
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
			SelectClothingItem();
		}

		private void SelectClothingItem()
		{
			m_manager.EquipNewProp(m_item);
			m_manager.UIManager.NavigateToPage("UNIT");
			m_manager.DestroyTemporary();
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			m_manager.EquipTemporaryProp(m_item);
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			m_manager.DestroyTemporary();
		}

		public override void OnPointerEnter(PointerEventData eventData)
		{
			base.OnPointerEnter(eventData);
			m_manager.EquipTemporaryProp(m_item);
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			base.OnPointerExit(eventData);
			m_manager.DestroyTemporary();
		}

		public override void OnSubmit(BaseEventData eventData)
		{
			base.OnSubmit(eventData);
			SelectClothingItem();
		}
	}
}
