using UnityEngine;

namespace Landfall.TABS
{
	public class PropItem : CharacterItem
	{
		public bool disableGooglyEyesOfParent;

		[SerializeField]
		private bool m_neverHide;

		private SettingsInstance m_hideClothesOption;

		protected override void Start()
		{
			base.Start();
			if (!m_neverHide)
			{
				m_hideClothesOption = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_HIDE_CLOTHES");
				m_hideClothesOption.OnValueChanged += SetVisibility;
				SetVisibility(m_hideClothesOption.currentValue);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (m_hideClothesOption != null && !m_neverHide)
			{
				m_hideClothesOption.OnValueChanged -= SetVisibility;
			}
		}

		private void SetVisibility(int value)
		{
			SetVisibility(value == 0, forceHidden: true);
		}
	}
}
