using UnityEngine;
using UnityEngine.EventSystems;

namespace Landfall.TABS
{
	public class CustomContentTopBarButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		public CustomContentOverlaysManager.Page m_Page;

		public CustomContentOverlaysManager m_Manager;

		public void OnPointerClick(PointerEventData eventData)
		{
		}
	}
}
