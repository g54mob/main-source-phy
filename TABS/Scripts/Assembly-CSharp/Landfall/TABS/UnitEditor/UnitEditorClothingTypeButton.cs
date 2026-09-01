using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorClothingTypeButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
	{
		public Image icon;

		public new LocalizeText name;

		private UnitEditorManager.ClothingTypeWrapper gearType;

		private UnitEditorManager UnitEditorManager;

		public void Initlize(UnitEditorManager.ClothingTypeWrapper clothinbgTypeWrapper, UnitEditorManager unitEditorManager)
		{
			icon.sprite = clothinbgTypeWrapper.BaseIcon;
			name.LocaleID = clothinbgTypeWrapper.BaseDisplayName;
			gearType = clothinbgTypeWrapper;
			UnitEditorManager = unitEditorManager;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnButtonPressed();
		}

		private void OnButtonPressed()
		{
			UnitEditorManager.UIManager.ShowClothesByType(gearType);
			UnitEditorManager.UIManager.NavigateToPage("CLOTHINGLIST");
		}

		public void OnSubmit(BaseEventData eventData)
		{
			OnButtonPressed();
		}
	}
}
