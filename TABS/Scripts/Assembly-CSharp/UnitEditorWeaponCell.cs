using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorWeaponCell : UnitEditorItemCell
{
	private bool m_isRight = true;

	public Image icon;

	public Image weaponTypeIcon;

	public Sprite rangedIcon;

	public override void Initialize(CharacterItem item, UnitEditorManager manager)
	{
		base.Initialize(item, manager);
		icon.sprite = item.Entity.SpriteIcon;
		nameText.LocaleID = item.DisplayName;
		if (item.GetComponent<RangeWeapon>() != null)
		{
			weaponTypeIcon.sprite = rangedIcon;
		}
	}

	public void SetRight(bool isRight)
	{
		m_isRight = isRight;
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
		SetWeapon();
	}

	private void SetWeapon()
	{
		m_manager.SpawnWeapon(m_item, m_isRight);
		m_manager.UIManager.NavigateToPage("UNIT");
	}

	public override void OnSubmit(BaseEventData eventData)
	{
		base.OnSubmit(eventData);
		SetWeapon();
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
	}
}
