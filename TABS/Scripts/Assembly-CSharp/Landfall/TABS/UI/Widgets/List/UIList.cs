using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UI.Widgets.List
{
	public class UIList : MonoBehaviour
	{
		public delegate void OnItemAddedDelegate(UIListItem item);

		[SerializeField]
		private GameObject m_itemTemplate;

		[SerializeField]
		private Image m_itemTemplateBackgroundImage;

		[SerializeField]
		private bool m_AlternateBackgroundColors;

		private RectTransform m_contentElement;

		private List<UIListItem> m_items;

		private Color m_originalBackgroundColor;

		private DisableOnInputDeviceChange m_DisableGameObjectsInputChange;

		public List<UIListItem> Items => m_items;

		public OnItemAddedDelegate OnItemAddedCallback { get; set; }

		private void Awake()
		{
			m_DisableGameObjectsInputChange = GetComponent<DisableOnInputDeviceChange>();
			m_items = new List<UIListItem>();
			m_contentElement = (RectTransform)base.transform.Find("Viewport").Find("Content");
			UpdateContentHeight();
		}

		private void Start()
		{
			if (m_itemTemplateBackgroundImage != null && m_AlternateBackgroundColors)
			{
				m_originalBackgroundColor = m_itemTemplateBackgroundImage.color;
			}
		}

		private UIListItem CreateItem()
		{
			GameObject obj = Object.Instantiate(m_itemTemplate);
			obj.SetActive(value: true);
			UIListItem component = obj.GetComponent<UIListItem>();
			component.RectTransform.SetParent(m_contentElement, worldPositionStays: true);
			component.RectTransform.localScale = m_itemTemplate.transform.localScale;
			if (m_AlternateBackgroundColors)
			{
				Color color = component.BackgroundImage.color;
				if (m_items.Count % 2 == 0)
				{
					component.BackgroundImage.color = new Color(color.r, color.g, color.b, 0.003921569f);
				}
			}
			return component;
		}

		public UIListItem AddItem(string displayText)
		{
			UIListItem uIListItem = CreateItem();
			uIListItem.SetDisplayText(displayText, null);
			m_DisableGameObjectsInputChange.AddToDisableOnGamepad(uIListItem.RowSpacer);
			m_items.Add(uIListItem);
			UpdateContentHeight();
			OnItemAddedCallback?.Invoke(uIListItem);
			return uIListItem;
		}

		public void RemoveItem(UIListItem item)
		{
			m_items.Remove(item);
			m_DisableGameObjectsInputChange.RemoveFromDisableOnGamepad(item.RowSpacer);
			Object.Destroy(item.gameObject);
			UpdateContentHeight();
			UpdateItemColors();
		}

		private void UpdateItemColors()
		{
			if (!m_AlternateBackgroundColors)
			{
				return;
			}
			for (int i = 0; i < m_items.Count; i++)
			{
				UIListItem uIListItem = m_items[i];
				Color originalBackgroundColor = m_originalBackgroundColor;
				if (i % 2 == 0)
				{
					uIListItem.BackgroundImage.color = new Color(originalBackgroundColor.r, originalBackgroundColor.g, originalBackgroundColor.b, 0.003921569f);
				}
				else
				{
					uIListItem.BackgroundImage.color = originalBackgroundColor;
				}
			}
		}

		public void RemoveAllItems()
		{
			for (int i = 0; i < m_items.Count; i++)
			{
				Object.Destroy(m_items[i].gameObject);
			}
			m_items.Clear();
			UpdateContentHeight();
		}

		private void UpdateContentHeight()
		{
			float num = 0f;
			for (int i = 0; i < m_items.Count; i++)
			{
				num += m_items[i].ElementHeight;
			}
			m_contentElement.sizeDelta = new Vector2(m_contentElement.sizeDelta.x, num);
		}
	}
}
