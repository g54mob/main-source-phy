using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(TagContainer))]
	public class ExplorerFilterTagsContainer : MonoBehaviour, IExplorerViewElement
	{
		private ExplorerView m_view;

		public TagContainer container => base.gameObject.GetComponent<TagContainer>();

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

		public void DisplayInArrayFilterTags(RequestFilter filter)
		{
			List<IRequestFieldFilter> value = null;
			filter?.fieldFilterMap.TryGetValue("tags", out value);
			string[] tags = null;
			if (value != null)
			{
				MatchesArrayFilter<string> matchesArrayFilter = null;
				for (int i = 0; i < value.Count; i++)
				{
					if (matchesArrayFilter != null)
					{
						break;
					}
					IRequestFieldFilter requestFieldFilter = value[i];
					if (requestFieldFilter.filterMethod == FieldFilterMethod.EquivalentCollection)
					{
						matchesArrayFilter = requestFieldFilter as MatchesArrayFilter<string>;
					}
				}
				if (matchesArrayFilter != null)
				{
					tags = matchesArrayFilter.filterValue;
				}
			}
			container.DisplayTags(tags);
		}

		public void RemoveTagFromExplorerFilter(TagContainerItem tagItem)
		{
			if (m_view != null && tagItem != null)
			{
				m_view.RemoveTagFromFilter(tagItem.tagName.text);
			}
		}
	}
}
