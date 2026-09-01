using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("Use ExplorerFilterTagsContainer instead.")]
	[RequireComponent(typeof(TagContainer))]
	public class ExplorerTagFilterBar : MonoBehaviour
	{
		public ExplorerView view;

		private List<string> m_selectedTags = new List<string>();

		[Obsolete("Use TagContainer.hideIfEmpty instead.")]
		[HideInInspector]
		public bool hideIfEmpty;

		public TagContainer container => base.gameObject.GetComponent<TagContainer>();

		private void Start()
		{
			view.onTagFilterUpdated += delegate(string[] t)
			{
				m_selectedTags = new List<string>(t);
				Refresh();
			};
			m_selectedTags = new List<string>(view.GetTagFilter());
			Refresh();
		}

		public void Refresh()
		{
			container.DisplayTags(m_selectedTags);
		}
	}
}
