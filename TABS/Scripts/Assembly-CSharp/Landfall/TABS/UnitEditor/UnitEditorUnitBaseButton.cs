using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorUnitBaseButton : Selectable, ISubmitHandler, IEventSystemHandler, IPointerClickHandler
	{
		public Image icon;

		public LocalizeText nameText;

		private int unitBaseIndex = -1;

		private UnitEditorManager UnitEditorManager;

		public void Initlize(UnitEditorManager.UnitBaseWrapper unitBaseWrapper, UnitEditorManager unitEditorManager, int index)
		{
			unitBaseIndex = index;
			icon.sprite = unitBaseWrapper.BaseIcon;
			nameText.LocaleID = unitBaseWrapper.BaseDisplayName;
			base.name = unitBaseWrapper.BaseDisplayName;
			UnitEditorManager = unitEditorManager;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			UnitEditorManager.SwitchUnitBase(unitBaseIndex);
		}

		public void OnSubmit(BaseEventData eventData)
		{
			UnitEditorManager.SwitchUnitBase(unitBaseIndex);
		}
	}
}
