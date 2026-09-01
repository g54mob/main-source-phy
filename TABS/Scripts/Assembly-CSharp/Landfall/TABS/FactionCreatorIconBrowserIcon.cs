using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class FactionCreatorIconBrowserIcon : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
	{
		public Image iconIamge;

		private Image backgroundImage;

		public Color darkColor;

		private FactionIcon icon;

		public GameObject Setup(FactionIcon icon)
		{
			this.icon = icon;
			backgroundImage = GetComponent<Image>();
			base.gameObject.SetActive(value: true);
			icon.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && iconIamge != null)
				{
					iconIamge.sprite = sprite;
				}
			});
			return base.gameObject;
		}

		private void OnEnter()
		{
			iconIamge.color = darkColor;
			backgroundImage.color = Color.white;
		}

		private void OnExit()
		{
			iconIamge.color = Color.white;
			backgroundImage.color = darkColor;
		}

		private void OnClick()
		{
			Object.FindObjectOfType<FactionCreatorIconBrowser>().SelectIcon(icon);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			OnEnter();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			OnExit();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnClick();
		}

		public void OnSelect(BaseEventData eventData)
		{
			OnEnter();
		}

		public void OnDeselect(BaseEventData eventData)
		{
			OnExit();
		}

		public void OnSubmit(BaseEventData eventData)
		{
			OnClick();
		}
	}
}
