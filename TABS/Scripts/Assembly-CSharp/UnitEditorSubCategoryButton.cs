using UnityEngine;

public class UnitEditorSubCategoryButton : MonoBehaviour
{
	public UnitEditorMenuButton.GetButtonsCallback m_getButtonsCallbackE;

	private UnitEditorItemList itemListMenu;

	public void OpenItemMenu()
	{
		if (!itemListMenu)
		{
			itemListMenu = GetComponentInParent<UnitEditorItemList>();
		}
		if (m_getButtonsCallbackE != null)
		{
			itemListMenu.UpdateItemButtons(m_getButtonsCallbackE());
		}
		itemListMenu.OpenItemList();
	}
}
