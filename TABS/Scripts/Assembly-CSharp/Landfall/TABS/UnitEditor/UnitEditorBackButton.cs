using UnityEngine;
using UnityEngine.EventSystems;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorBackButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		private UnitEditorUIManager UIManager;

		private void Awake()
		{
			UIManager = GetComponentInParent<UnitEditorUIManager>();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
		}
	}
}
