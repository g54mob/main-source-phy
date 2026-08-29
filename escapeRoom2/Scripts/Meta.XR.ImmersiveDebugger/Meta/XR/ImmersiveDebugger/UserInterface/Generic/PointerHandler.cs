using UnityEngine.EventSystems;

namespace Meta.XR.ImmersiveDebugger.UserInterface.Generic
{
	public class PointerHandler : UIBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public InteractableController Controller { get; set; }

		public void OnPointerClick(PointerEventData eventData)
		{
			if (Controller != null)
			{
				Controller.OnPointerClick();
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Controller != null)
			{
				Controller.OnPointerEnter();
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (Controller != null)
			{
				Controller.OnPointerExit();
			}
		}
	}
}
