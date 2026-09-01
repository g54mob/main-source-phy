using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorPopupSubMenuSaveSub : UnitEditorPopupSubMenu
	{
		[SerializeField]
		private Button saveButton;

		[SerializeField]
		private Button discardButton;

		protected override void UpdateGamepads()
		{
			base.UpdateGamepads();
			if (playerActions.m_accept.WasPressed)
			{
				InvokeButton(saveButton);
			}
			else if (playerActions.m_discardChanges.WasPressed)
			{
				InvokeButton(discardButton);
			}
		}
	}
}
