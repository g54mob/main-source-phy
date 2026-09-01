using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(GameTagCategoryDisplay))]
	public class ExplorerFilterTagsDropdown : MonoBehaviour, ICancelHandler, IEventSystemHandler, IExplorerViewElement
	{
		public GameObject popup;

		private ExplorerView m_view;

		private string[] m_selectedTags = new string[0];

		private bool m_isUpdating;

		public GameTagCategoryDisplay categoryDisplay
		{
			get
			{
				return base.gameObject.GetComponent<GameTagCategoryDisplay>();
			}
		}

		private void Awake()
		{
			categoryDisplay.onTagsChanged += delegate
			{
				UpdateSelectedTagsDisplay(m_selectedTags);
			};
		}

		private void OnEnable()
		{
			UpdateSelectedTagsDisplay(m_selectedTags);
		}

		public void SetExplorerView(ExplorerView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onRequestFilterChanged.RemoveListener(DisplayInArrayFilterTags);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onRequestFilterChanged.AddListener(DisplayInArrayFilterTags);
					DisplayInArrayFilterTags(m_view.requestFilter);
				}
				else
				{
					DisplayInArrayFilterTags(null);
				}
			}
		}

		public void DisplayInArrayFilterTags(RequestFilter requestFilter)
		{
			List<IRequestFieldFilter> value = null;
			requestFilter.fieldFilterMap.TryGetValue("tags", out value);
			if (value != null && value.Count > 0)
			{
				foreach (IRequestFieldFilter item in value)
				{
					if (item != null && item.filterMethod == FieldFilterMethod.EquivalentCollection)
					{
						UpdateSelectedTagsDisplay(item.filterValue as IEnumerable<string>);
						break;
					}
				}
				return;
			}
			UpdateSelectedTagsDisplay(null);
		}

		public void UpdateSelectedTagsDisplay(IEnumerable<string> selectedTags)
		{
			if (m_selectedTags != selectedTags)
			{
				if (selectedTags == null)
				{
					selectedTags = new string[0];
				}
				List<string> list = new List<string>();
				foreach (string selectedTag in selectedTags)
				{
					if (!string.IsNullOrEmpty(selectedTag))
					{
						list.Add(selectedTag);
					}
				}
				m_selectedTags = list.ToArray();
			}
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			m_isUpdating = true;
			foreach (TagContainerItem tagItem in categoryDisplay.tagItems)
			{
				bool flag = false;
				for (int i = 0; i < m_selectedTags.Length; i++)
				{
					if (flag)
					{
						break;
					}
					flag = m_selectedTags[i] == tagItem.tagName.text;
				}
				tagItem.GetComponentInChildren<StateToggleDisplay>(true).isOn = flag;
			}
			m_isUpdating = false;
		}

		public void AddTagToExplorerFilter(TagContainerItem tagItem)
		{
			if (m_view != null && !m_isUpdating)
			{
				m_view.AddTagToFilter(tagItem.tagName.text);
			}
		}

		public void RemoveTagFromExplorerFilter(TagContainerItem tagItem)
		{
			if (m_view != null && !m_isUpdating)
			{
				m_view.RemoveTagFromFilter(tagItem.tagName.text);
			}
		}

		public void ToggleTagInExplorerFilter(TagContainerItem tagItem)
		{
			if (m_view != null && !m_isUpdating)
			{
				string text = tagItem.tagName.text;
				StateToggleDisplay componentInChildren = tagItem.GetComponentInChildren<StateToggleDisplay>(true);
				if (componentInChildren.isOn)
				{
					m_view.AddTagToFilter(text);
				}
				else
				{
					m_view.RemoveTagFromFilter(text);
				}
			}
		}

		public static bool HasValidTemplate(ExplorerFilterTagsDropdown selector, out string helpMessage)
		{
			helpMessage = null;
			if (selector.categoryDisplay == null || !GameTagCategoryDisplay.HasValidTemplate(selector.categoryDisplay, out helpMessage))
			{
				helpMessage = "The required GameTagCategoryDisplay is missing or has an invalid template.";
				return false;
			}
			bool result = true;
			TagContainerItem tagTemplate = selector.categoryDisplay.template.tagTemplate;
			if (tagTemplate.gameObject.GetComponentInChildren<StateToggleDisplay>(true) == null)
			{
				helpMessage = "This ExplorerFilterTagsDropdown has an invalid template.\nThe tag template of the GameTagCategoryDisplay must also have a StateToggleDisplay derived component as a child, or on the same GameObject.\n(EG. GameObjectToggle, StateToggle, or SlideToggle.)";
				result = false;
			}
			return result;
		}

		public void OnCancel(BaseEventData eventData)
		{
			if (popup != null && popup.activeInHierarchy)
			{
				popup.SetActive(false);
				if (NavigationManager.allowSelectionChange)
				{
					base.gameObject.GetComponent<Selectable>().Select();
				}
			}
		}
	}
}
