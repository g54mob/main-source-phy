using GamepadUI.StateManager.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public abstract class UnitEditorPopupSubMenu : UISubMenu
	{
		protected void InvokeButton(Button button)
		{
			if (button != null)
			{
				button.onClick.Invoke();
			}
			else
			{
				Debug.LogError(base.gameObject.name + ", is try to invoke a null button!", this);
			}
		}
	}
}
