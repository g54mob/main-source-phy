using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	[RequireComponent(typeof(Selectable))]
	public class SelectionEventListener : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
	{
		public delegate void OnSelectionChangedDelegate(bool isSelected);

		public UnityEvent<bool> OnSelectionChangedEvent;

		public OnSelectionChangedDelegate OnSelectionChanged;

		protected Selectable selectable;

		public Selectable Selectable => null;

		public bool IsSelected => false;

		public void OnSelect(BaseEventData eventData)
		{
		}

		public void OnDeselect(BaseEventData eventData)
		{
		}
	}
}
