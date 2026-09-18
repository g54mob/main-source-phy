using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleScrollRectSelectableListItem : MonoBehaviour
	{
		private Selectable selectable;

		private bool wasSelected;

		private ScrollRect parentScrollRect;

		private void Start()
		{
			parentScrollRect = GetComponentInParent<ScrollRect>();
			selectable = GetComponentInChildren<Selectable>();
		}

		private void Update()
		{
			if (parentScrollRect != null && selectable != null)
			{
				bool flag = EventSystem.current.currentSelectedGameObject == selectable.gameObject;
				if (!wasSelected && flag)
				{
					parentScrollRect.SnapChildIntoView(GetComponent<RectTransform>());
				}
				wasSelected = flag;
			}
		}
	}
}
