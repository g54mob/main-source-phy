using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorProjectileSlot : UnitEditorSelectableItem
{
	public LocalizeText text;

	public Image icon;

	public void Setup(UnitEditorManager.EquipedWeaponWrapper weapon)
	{
		ProjectileEntity projectile = Object.FindObjectOfType<UnitEditorManager>().GetProjectile(weapon);
		if ((bool)projectile)
		{
			text.LocaleID = projectile.DisplayName;
			icon.sprite = projectile.Entity.SpriteIcon;
		}
	}
}
