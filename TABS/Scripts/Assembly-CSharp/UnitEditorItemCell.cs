using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UnitEditorItemCell : Selectable, ISubmitHandler, IEventSystemHandler, IPointerClickHandler, IFilterableItem
{
	protected UnitEditorManager m_manager;

	protected CharacterItem m_item;

	public LocalizeText nameText;

	public string FilteringName => nameText.Text.text.ToLower();

	public GameObject ItemCellGameObject => base.gameObject;

	public CharacterItem Item => m_item;

	public virtual void Initialize(CharacterItem item, UnitEditorManager manager)
	{
		m_item = item;
		m_manager = manager;
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
	}

	public virtual void OnSubmit(BaseEventData eventData)
	{
	}

	public override void OnSelect(BaseEventData eventData)
	{
	}

	public override void OnDeselect(BaseEventData eventData)
	{
	}
}
