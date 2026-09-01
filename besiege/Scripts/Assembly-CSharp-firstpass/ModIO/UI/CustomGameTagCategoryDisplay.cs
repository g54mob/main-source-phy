using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModIO.UI
{
	[AddComponentMenu("ModIO/Explorer/Explorer Custom Tag Category Display")]
	public class CustomGameTagCategoryDisplay : GameTagCategoryDisplay, IExplorerViewElement
	{
		[Serializable]
		public class ItemTypeToTagCategories
		{
			public string ItemTypeName;

			public string[] TagCategories;
		}

		public ItemTypeToTagCategories[] ItemTypeToTags = new ItemTypeToTagCategories[4]
		{
			new ItemTypeToTagCategories
			{
				ItemTypeName = "Machines",
				TagCategories = new string[3] { "Machine Tags", "Machine Categories", "Details" }
			},
			new ItemTypeToTagCategories
			{
				ItemTypeName = "Levels",
				TagCategories = new string[3] { "Level Tags", "Level Categories", "Details" }
			},
			new ItemTypeToTagCategories
			{
				ItemTypeName = "Mods",
				TagCategories = new string[1] { "Mod Tags" }
			},
			new ItemTypeToTagCategories
			{
				ItemTypeName = "Skin Packs",
				TagCategories = new string[2] { "Skin Packs Tags", "Details" }
			}
		};

		private ExplorerView m_view;

		public void SetExplorerView(ExplorerView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onRequestFilterChanged.RemoveListener(DisplayHiddenOption);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onRequestFilterChanged.AddListener(DisplayHiddenOption);
					DisplayHiddenOption(m_view.requestFilter);
				}
				else
				{
					DisplayHiddenOption(null);
				}
			}
		}

		private void DisplayHiddenOption(RequestFilter requestFilter)
		{
			List<IRequestFieldFilter> value = null;
			requestFilter.fieldFilterMap.TryGetValue("tags", out value);
			ClearCategoryFilters();
			if (value == null || value.Count <= 0)
			{
				return;
			}
			foreach (IRequestFieldFilter item in value)
			{
				if (item != null && item.filterMethod == FieldFilterMethod.EquivalentCollection)
				{
					IEnumerable<string> tags = item.filterValue as IEnumerable<string>;
					FilterCategoriesOnItemType(tags);
					break;
				}
			}
		}

		private void ClearCategoryFilters()
		{
			for (int i = 0; i < m_itemInstances.Length; i++)
			{
				if (!m_itemInstances[i].gameObject.activeSelf)
				{
					m_itemInstances[i].gameObject.SetActive(true);
				}
			}
		}

		private void FilterCategoriesOnItemType(IEnumerable<string> tags)
		{
			if (tags == null || tags.Count() == 0)
			{
				return;
			}
			ItemTypeToTagCategories itemTypeToTagCategories = ItemTypeToTags.Where((ItemTypeToTagCategories x) => tags.Contains(x.ItemTypeName)).SingleOrDefault();
			if (itemTypeToTagCategories == null)
			{
				Debug.Log("No item type selected...");
				return;
			}
			for (int num = 0; num < m_itemInstances.Length; num++)
			{
				if (!itemTypeToTagCategories.TagCategories.Contains(m_tagCategories[num].name))
				{
					m_itemInstances[num].gameObject.SetActive(false);
				}
			}
		}
	}
}
