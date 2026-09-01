using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorPopupSubMenuOverwrite : UnitEditorPopupSubMenu
	{
		[SerializeField]
		private Button saveButton;

		[SerializeField]
		private Button saveAsNewButton;

		protected override void UpdateGamepads()
		{
			base.UpdateGamepads();
			if (playerActions.m_accept.WasPressed)
			{
				InvokeButton(saveButton);
			}
			else if (playerActions.m_saveCustomContent.WasPressed)
			{
				InvokeButton(saveAsNewButton);
			}
		}
	}
}
