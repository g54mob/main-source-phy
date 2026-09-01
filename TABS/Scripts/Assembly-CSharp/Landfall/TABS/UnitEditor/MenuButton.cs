using UnityEngine;
using UnityEngine.EventSystems;

namespace Landfall.TABS.UnitEditor
{
	public class MenuButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
	{
		public string pageName;

		private UnitEditorUIManager UIManager;

		private void Awake()
		{
			UIManager = GetComponentInParent<UnitEditorUIManager>();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			UIManager.NavigateToPage(pageName);
		}

		public void OnSubmit(BaseEventData eventData)
		{
			if (!UnitEditorManager.isTestingUnit)
			{
				UIManager.NavigateToPage(pageName);
			}
		}

		public void NavigateToPage()
		{
			if (UIManager != null)
			{
				UIManager.NavigateToPage(pageName);
			}
		}
	}
}
