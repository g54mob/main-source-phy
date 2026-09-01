using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(TagContainer))]
	[AddComponentMenu("ModIO/Inspector/Tags Container")]
	public class InspectorTagsContainer : MonoBehaviour, IModViewElement
	{
		private ModView m_view;

		public TagContainer container
		{
			get
			{
				return base.gameObject.GetComponent<TagContainer>();
			}
		}

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayInArrayFilterTags);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayInArrayFilterTags);
					DisplayInArrayFilterTags(m_view.profile);
				}
				else
				{
					DisplayInArrayFilterTags(null);
				}
			}
		}

		public void DisplayInArrayFilterTags(ModProfile modProfile)
		{
			IEnumerable<string> tags = null;
			if (modProfile != null)
			{
				tags = modProfile.tagNames;
			}
			container.DisplayTags(tags);
		}
	}
}
