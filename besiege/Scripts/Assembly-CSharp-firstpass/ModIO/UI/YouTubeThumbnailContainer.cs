using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	public class YouTubeThumbnailContainer : MonoBehaviour, IModViewElement
	{
		public RectTransform containerTemplate;

		public bool hideIfEmpty;

		private ModView m_view;

		private GameObject m_templateClone;

		private RectTransform m_container;

		private YouTubeThumbnailDisplay m_itemTemplate;

		private int m_modId;

		private string[] m_youTubeIds = new string[0];

		private YouTubeThumbnailDisplay[] m_displays = new YouTubeThumbnailDisplay[0];

		private GalleryImageContainer sibling;

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		protected virtual void Awake()
		{
			sibling = base.transform.parent.GetComponentInChildren<GalleryImageContainer>();
			containerTemplate.gameObject.SetActive(false);
		}

		protected virtual void Start()
		{
			Transform parent = containerTemplate.parent;
			string text = containerTemplate.gameObject.name + " (Instance)";
			int num = containerTemplate.GetSiblingIndex() + 1;
			m_itemTemplate = containerTemplate.GetComponentInChildren<YouTubeThumbnailDisplay>(true);
			bool flag = parent.childCount > num && parent.GetChild(num).gameObject.name == text;
			if (flag)
			{
				m_templateClone = parent.GetChild(num).gameObject;
				YouTubeThumbnailDisplay[] componentsInChildren = m_templateClone.GetComponentsInChildren<YouTubeThumbnailDisplay>(true);
				if (componentsInChildren == null || componentsInChildren.Length == 0)
				{
					flag = false;
					Object.Destroy(m_templateClone);
				}
				else
				{
					m_container = (RectTransform)componentsInChildren[0].transform.parent;
					YouTubeThumbnailDisplay[] array = componentsInChildren;
					foreach (YouTubeThumbnailDisplay youTubeThumbnailDisplay in array)
					{
						Object.Destroy(youTubeThumbnailDisplay.gameObject);
					}
				}
			}
			if (!flag)
			{
				m_templateClone = (GameObject)Object.Instantiate(containerTemplate.gameObject, parent);
				m_templateClone.transform.SetSiblingIndex(num);
				m_templateClone.name = text;
				YouTubeThumbnailDisplay componentInChildren = m_templateClone.GetComponentInChildren<YouTubeThumbnailDisplay>(true);
				m_container = (RectTransform)componentInChildren.transform.parent;
				Object.Destroy(componentInChildren.gameObject);
				m_templateClone.SetActive(true);
			}
			DisplayThumbnails(m_modId, m_youTubeIds);
		}

		protected virtual void OnEnable()
		{
			DisplayThumbnails(m_modId, m_youTubeIds);
		}

		public virtual void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayProfile);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayProfile);
					DisplayProfile(m_view.profile);
				}
				else
				{
					DisplayProfile(null);
				}
			}
		}

		public virtual void DisplayProfile(ModProfile profile)
		{
			int modId = 0;
			string[] array = null;
			if (profile != null && profile.media != null && profile.media.youTubeURLs != null)
			{
				modId = profile.id;
				string[] youTubeURLs = profile.media.youTubeURLs;
				array = new string[youTubeURLs.Length];
				for (int i = 0; i < youTubeURLs.Length; i++)
				{
					array[i] = Utility.ExtractYouTubeIdFromURL(youTubeURLs[i]);
				}
			}
			DisplayThumbnails(modId, array);
		}

		public virtual void DisplayThumbnails(int modId, IList<string> youTubeIds)
		{
			m_modId = modId;
			if (m_youTubeIds != youTubeIds)
			{
				int num = 0;
				if (youTubeIds != null)
				{
					num = youTubeIds.Count;
				}
				m_youTubeIds = new string[num];
				for (int i = 0; i < num; i++)
				{
					m_youTubeIds[i] = youTubeIds[i];
				}
			}
			if (m_itemTemplate != null)
			{
				UIUtilities.SetInstanceCount(m_container, m_itemTemplate, "YouTube Thumbnail", m_youTubeIds.Length, ref m_displays);
				for (int j = 0; j < m_youTubeIds.Length; j++)
				{
					m_displays[j].DisplayThumbnail(m_modId, m_youTubeIds[j]);
				}
				m_templateClone.SetActive(GalleryCount() > 1 || !hideIfEmpty);
			}
		}

		public int LocalCount()
		{
			return m_youTubeIds.Length;
		}

		private int GalleryCount()
		{
			return LocalCount() + sibling.LocalCount();
		}

		public static bool HasValidTemplate(YouTubeThumbnailContainer container, out string helpMessage)
		{
			helpMessage = null;
			bool result = true;
			YouTubeThumbnailDisplay youTubeThumbnailDisplay = null;
			if (container.containerTemplate == null)
			{
				helpMessage = "Invalid template: The container template is unassigned.";
				result = false;
			}
			else if (!container.containerTemplate.IsChildOf(container.transform) || container.containerTemplate == container.transform)
			{
				helpMessage = "Invalid template: The container template must be a child of this object.";
				result = false;
			}
			else if ((youTubeThumbnailDisplay = container.containerTemplate.gameObject.GetComponentInChildren<YouTubeThumbnailDisplay>()) == null)
			{
				helpMessage = "Invalid template: No YouTubeThumbnailDisplay component found in the children of the container template.";
				result = false;
			}
			else if (youTubeThumbnailDisplay.transform == container.containerTemplate)
			{
				helpMessage = "Invalid template: The YouTubeThumbnailDisplay component cannot share a GameObject with the container template.";
				result = false;
			}
			return result;
		}
	}
}
