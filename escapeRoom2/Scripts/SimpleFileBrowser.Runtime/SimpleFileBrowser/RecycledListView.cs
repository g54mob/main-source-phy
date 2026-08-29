using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	[RequireComponent(typeof(ScrollRect))]
	public class RecycledListView : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		[SerializeField]
		private FileBrowser fileBrowser;

		[SerializeField]
		private RectTransform viewportTransform;

		[SerializeField]
		private RectTransform contentTransform;

		private float itemHeight;

		private float _1OverItemHeight;

		private float viewportHeight;

		private readonly Dictionary<int, ListItem> items = new Dictionary<int, ListItem>();

		private readonly Stack<ListItem> pooledItems = new Stack<ListItem>();

		private IListViewAdapter adapter;

		private int currentTopIndex = -1;

		private int currentBottomIndex = -1;

		private void Start()
		{
			viewportHeight = viewportTransform.rect.height;
			GetComponent<ScrollRect>().onValueChanged.AddListener(delegate
			{
				UpdateItemsInTheList();
			});
		}

		public void SetAdapter(IListViewAdapter adapter)
		{
			this.adapter = adapter;
			itemHeight = adapter.ItemHeight;
			_1OverItemHeight = 1f / itemHeight;
		}

		public void OnSkinRefreshed()
		{
			if (currentTopIndex >= 0)
			{
				DestroyItemsBetweenIndices(currentTopIndex, currentBottomIndex);
				currentTopIndex = (currentBottomIndex = -1);
			}
			itemHeight = adapter.ItemHeight;
			_1OverItemHeight = 1f / itemHeight;
			UpdateList();
		}

		public void UpdateList()
		{
			float y = Mathf.Max(1f, (float)adapter.Count * itemHeight);
			contentTransform.sizeDelta = new Vector2(0f, y);
			viewportHeight = viewportTransform.rect.height;
			UpdateItemsInTheList(updateAllVisibleItems: true);
		}

		public void OnViewportDimensionsChanged()
		{
			viewportHeight = viewportTransform.rect.height;
			UpdateItemsInTheList();
		}

		private void UpdateItemsInTheList(bool updateAllVisibleItems = false)
		{
			if (adapter.Count > 0)
			{
				float num = contentTransform.anchoredPosition.y - 1f;
				int num2 = (int)(num * _1OverItemHeight);
				int num3 = (int)((num + viewportHeight + 2f) * _1OverItemHeight);
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num3 > adapter.Count - 1)
				{
					num3 = adapter.Count - 1;
				}
				if (currentTopIndex == -1)
				{
					updateAllVisibleItems = true;
					currentTopIndex = num2;
					currentBottomIndex = num3;
					CreateItemsBetweenIndices(num2, num3);
				}
				else
				{
					if (num3 < currentTopIndex || num2 > currentBottomIndex)
					{
						updateAllVisibleItems = true;
						DestroyItemsBetweenIndices(currentTopIndex, currentBottomIndex);
						CreateItemsBetweenIndices(num2, num3);
					}
					else
					{
						if (num2 > currentTopIndex)
						{
							DestroyItemsBetweenIndices(currentTopIndex, num2 - 1);
						}
						if (num3 < currentBottomIndex)
						{
							DestroyItemsBetweenIndices(num3 + 1, currentBottomIndex);
						}
						if (num2 < currentTopIndex)
						{
							CreateItemsBetweenIndices(num2, currentTopIndex - 1);
							if (!updateAllVisibleItems)
							{
								UpdateItemContentsBetweenIndices(num2, currentTopIndex - 1);
							}
						}
						if (num3 > currentBottomIndex)
						{
							CreateItemsBetweenIndices(currentBottomIndex + 1, num3);
							if (!updateAllVisibleItems)
							{
								UpdateItemContentsBetweenIndices(currentBottomIndex + 1, num3);
							}
						}
					}
					currentTopIndex = num2;
					currentBottomIndex = num3;
				}
				if (updateAllVisibleItems)
				{
					UpdateItemContentsBetweenIndices(currentTopIndex, currentBottomIndex);
				}
			}
			else if (currentTopIndex != -1)
			{
				DestroyItemsBetweenIndices(currentTopIndex, currentBottomIndex);
				currentTopIndex = -1;
			}
		}

		private void CreateItemsBetweenIndices(int topIndex, int bottomIndex)
		{
			for (int i = topIndex; i <= bottomIndex; i++)
			{
				CreateItemAtIndex(i);
			}
		}

		private void CreateItemAtIndex(int index)
		{
			ListItem listItem;
			if (pooledItems.Count > 0)
			{
				listItem = pooledItems.Pop();
				listItem.gameObject.SetActive(value: true);
			}
			else
			{
				listItem = adapter.CreateItem();
				listItem.SetAdapter(adapter);
			}
			((RectTransform)listItem.transform).anchoredPosition = new Vector2(1f, (float)(-index) * itemHeight);
			items[index] = listItem;
		}

		private void DestroyItemsBetweenIndices(int topIndex, int bottomIndex)
		{
			for (int i = topIndex; i <= bottomIndex; i++)
			{
				ListItem listItem = items[i];
				listItem.gameObject.SetActive(value: false);
				pooledItems.Push(listItem);
			}
			if (topIndex == currentTopIndex && bottomIndex == currentBottomIndex)
			{
				items.Clear();
				return;
			}
			for (int j = topIndex; j <= bottomIndex; j++)
			{
				items.Remove(j);
			}
		}

		private void UpdateItemContentsBetweenIndices(int topIndex, int bottomIndex)
		{
			for (int i = topIndex; i <= bottomIndex; i++)
			{
				ListItem listItem = items[i];
				listItem.Position = i;
				adapter.SetItemContent(listItem);
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				fileBrowser.DeselectAllFiles();
			}
			else if (eventData.button == PointerEventData.InputButton.Right)
			{
				fileBrowser.OnContextMenuTriggered(eventData.position);
			}
		}
	}
}
