using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Landfall.TABS
{
	public class UIButton : SimpleButton, IPointerClickHandler, IEventSystemHandler
	{
		public UnityEvent m_OnClick;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				m_OnClick.Invoke();
			}
		}
	}
}
