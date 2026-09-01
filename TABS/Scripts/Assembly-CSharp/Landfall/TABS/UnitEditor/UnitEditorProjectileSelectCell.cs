using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorProjectileSelectCell : UnitEditorSelectableListItem
	{
		public Image m_image;

		public LocalizeText m_text;

		private ProjectileEntity projectileEntity;

		private UnitEditorManager.EquipedWeaponWrapper weaponToGiveProjectile;

		public GameObject Init(ProjectileEntity projectileEntity, UnitEditorManager.EquipedWeaponWrapper weaponToGiveProjectile)
		{
			this.projectileEntity = projectileEntity;
			this.weaponToGiveProjectile = weaponToGiveProjectile;
			m_text.LocaleID = projectileEntity.DisplayName;
			m_image.sprite = projectileEntity.Entity.SpriteIcon;
			base.gameObject.SetActive(value: true);
			return base.gameObject;
		}

		public void SelectProjectile()
		{
			UnitEditorManager unitEditorManager = Object.FindObjectOfType<UnitEditorManager>();
			if (unitEditorManager != null)
			{
				unitEditorManager.EquipProjectile(projectileEntity, weaponToGiveProjectile.isRightHanded);
				UnitEditorUIManager unitEditorUIManager = unitEditorManager.unitEditorUIManager;
				unitEditorManager.RespawnWeapons();
				if (unitEditorUIManager != null)
				{
					unitEditorUIManager.NavigateToPage("EQUIPEDCLOTHING");
					unitEditorUIManager.SetupEquipedWeapon(weaponToGiveProjectile);
				}
			}
		}

		public override bool ValidInFilter(string filter)
		{
			if (m_text.Text.text.ToLower().Contains(filter.ToLower()))
			{
				return true;
			}
			return false;
		}
	}
}
