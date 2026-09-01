using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorPopupSubMenuDiscard : UnitEditorPopupSubMenu
	{
		[SerializeField]
		private Button discardButton;

		protected override void UpdateGamepads()
		{
			base.UpdateGamepads();
			if (playerActions.m_discardChanges.WasPressed)
			{
				InvokeButton(discardButton);
			}
		}
	}
}
