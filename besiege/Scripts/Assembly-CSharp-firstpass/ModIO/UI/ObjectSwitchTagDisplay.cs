using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	public class ObjectSwitchTagDisplay : MonoBehaviour, IModViewElement
	{
		[Serializable]
		public struct TagObjectPair
		{
			public string tagName;

			public GameObject gameObject;
		}

		public List<TagObjectPair> tagObjectPairs = new List<TagObjectPair>();

		private ModView m_view;

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
					m_view.onProfileChanged.RemoveListener(DisplayProfile);
				}
				if (view != null)
				{
					view.onProfileChanged.AddListener(DisplayProfile);
				}
				m_view = view;
				DisplayProfile(m_view.profile);
			}
		}

		public void DisplayProfile(ModProfile profile)
		{
			if (profile == null)
			{
				HideAll();
				return;
			}
			List<string> tagNames = new List<string>(profile.tagNames);
			DisplayTags(tagNames);
		}

		public void DisplayTags(IList<string> tagNames)
		{
			if (tagNames == null)
			{
				HideAll();
				return;
			}
			foreach (TagObjectPair tagObjectPair in tagObjectPairs)
			{
				if (tagObjectPair.gameObject != null)
				{
					bool active = tagNames.Contains(tagObjectPair.tagName);
					tagObjectPair.gameObject.SetActive(active);
				}
			}
		}

		public void HideAll()
		{
			if (tagObjectPairs.Count <= 0)
			{
				return;
			}
			foreach (TagObjectPair tagObjectPair in tagObjectPairs)
			{
				if (tagObjectPair.gameObject != null)
				{
					tagObjectPair.gameObject.SetActive(false);
				}
			}
		}
	}
}
