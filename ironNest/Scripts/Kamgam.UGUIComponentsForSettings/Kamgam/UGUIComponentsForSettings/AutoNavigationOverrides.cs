using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	[RequireComponent(typeof(Selectable))]
	public class AutoNavigationOverrides : MonoBehaviour, ISelectHandler, IEventSystemHandler, IUpdateSelectedHandler
	{
		protected Selectable selectable;

		public bool DisableOnAwakeIfNotNeeded;

		[Tooltip("Defines which element to navigate to.<br />If left empty then the default navigation will be used.")]
		public Selectable SelectOnUpOverride;

		[Tooltip("Defines which element to navigate to.<br />If left empty then the default navigation will be used.")]
		public Selectable SelectOnDownOverride;

		[Tooltip("Defines which element to navigate to.<br />If left empty then the default navigation will be used.")]
		public Selectable SelectOnLeftOverride;

		[Tooltip("Defines which element to navigate to.<br />If left empty then the default navigation will be used.")]
		public Selectable SelectOnRightOverride;

		public bool BlockUp;

		public bool BlockDown;

		public bool BlockLeft;

		public bool BlockRight;

		public Selectable Selectable => null;

		public bool IsBlockingAnyDirection => false;

		public void Awake()
		{
		}

		public bool HasOverrides()
		{
			return false;
		}

		public bool HasActiveOverrides()
		{
			return false;
		}

		public void OnUpdateSelected(BaseEventData eventData)
		{
		}

		public void ApplyOverrides()
		{
		}

		public void SetSelectableDown(Selectable selectable)
		{
		}

		public void SetSelectableUp(Selectable selectable)
		{
		}

		public void SetSelectableRight(Selectable selectable)
		{
		}

		public void SetSelectableLeft(Selectable selectable)
		{
		}

		public Selectable FindSelectableOnUp()
		{
			return null;
		}

		public Selectable FindSelectableOnDown()
		{
			return null;
		}

		public Selectable FindSelectableOnLeft()
		{
			return null;
		}

		public Selectable FindSelectableOnRight()
		{
			return null;
		}

		public void OnSelect(BaseEventData eventData)
		{
		}
	}
}
