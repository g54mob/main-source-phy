using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(Canvas))]
	public class WindowBacking : MonoBehaviour
	{
		public Canvas canvas
		{
			get
			{
				return base.gameObject.GetComponent<Canvas>();
			}
		}

		private void Start()
		{
		}

		public void UpdateSortingOrder(IBrowserView view)
		{
			bool active = false;
			if (view != null && !view.isRootView && view.gameObject != null)
			{
				Canvas component = view.gameObject.GetComponent<Canvas>();
				if (component != null)
				{
					int sortingOrder = component.sortingOrder - 1;
					canvas.sortingOrder = sortingOrder;
					active = true;
				}
			}
			base.gameObject.SetActive(active);
		}
	}
}
