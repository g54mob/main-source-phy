using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	public class GalleryImageContainer : MonoBehaviour, IModViewElement
	{
		public RectTransform containerTemplate;

		public bool hideIfEmpty;

		private ModView m_view;

		private GameObject m_templateClone;

		private RectTransform m_container;

		private GalleryImageDisplay m_itemTemplate;

		private int m_modId;

		private GalleryImageLocator[] m_locators = new GalleryImageLocator[0];

		private GalleryImageDisplay[] m_displays = new GalleryImageDisplay[0];

		public Action<GalleryImageDisplay> OnNewImages;

		private YouTubeThumbnailContainer sibling;

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		protected virtual void Awake()
		{
			sibling = base.transform.parent.GetComponentInChildren<YouTubeThumbnailContainer>();
			containerTemplate.gameObject.SetActive(false);
		}

		protected virtual void Start()
		{
			Transform parent = containerTemplate.parent;
			string text = containerTemplate.gameObject.name + " (Instance)";
			int num = containerTemplate.GetSiblingIndex() + 1;
			m_itemTemplate = containerTemplate.GetComponentInChildren<GalleryImageDisplay>(true);
			bool flag = parent.childCount > num && parent.GetChild(num).gameObject.name == text;
			if (flag)
			{
				m_templateClone = parent.GetChild(num).gameObject;
				GalleryImageDisplay[] componentsInChildren = m_templateClone.GetComponentsInChildren<GalleryImageDisplay>(true);
				if (componentsInChildren == null || componentsInChildren.Length == 0)
				{
					flag = false;
					UnityEngine.Object.Destroy(m_templateClone);
				}
				else
				{
					m_container = (RectTransform)componentsInChildren[0].transform.parent;
					GalleryImageDisplay[] array = componentsInChildren;
					foreach (GalleryImageDisplay galleryImageDisplay in array)
					{
						UnityEngine.Object.Destroy(galleryImageDisplay.gameObject);
					}
				}
			}
			if (!flag)
			{
				m_templateClone = (GameObject)UnityEngine.Object.Instantiate(containerTemplate.gameObject, parent);
				m_templateClone.transform.SetSiblingIndex(num);
				m_templateClone.name = text;
				GalleryImageDisplay componentInChildren = m_templateClone.GetComponentInChildren<GalleryImageDisplay>(true);
				m_container = (RectTransform)componentInChildren.transform.parent;
				UnityEngine.Object.Destroy(componentInChildren.gameObject);
				m_templateClone.SetActive(true);
			}
			DisplayImages(m_modId, m_locators);
		}

		protected virtual void OnEnable()
		{
			DisplayImages(m_modId, m_locators);
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
			GalleryImageLocator[] locators = null;
			if (profile != null && profile.media != null)
			{
				modId = profile.id;
				locators = profile.media.galleryImageLocators;
			}
			DisplayImages(modId, locators);
		}

		public virtual void DisplayImages(int modId, IList<GalleryImageLocator> locators)
		{
			m_modId = modId;
			if (m_locators != locators)
			{
				int num = 0;
				if (locators != null)
				{
					num = locators.Count;
				}
				m_locators = new GalleryImageLocator[num];
				for (int i = 0; i < num; i++)
				{
					m_locators[i] = locators[i];
				}
			}
			if (m_itemTemplate != null)
			{
				UIUtilities.SetInstanceCount(m_container, m_itemTemplate, "Gallery Image", m_locators.Length, ref m_displays);
				for (int j = 0; j < m_locators.Length; j++)
				{
					m_displays[j].DisplayGalleryImage(modId, m_locators[j]);
				}
				if (m_locators.Length > 0 && OnNewImages != null)
				{
					OnNewImages(m_displays[0]);
				}
				m_templateClone.SetActive(GalleryCount() > 1 || !hideIfEmpty);
			}
		}

		public int LocalCount()
		{
			return m_locators.Length;
		}

		private int GalleryCount()
		{
			return LocalCount() + sibling.LocalCount();
		}

		public static bool HasValidTemplate(GalleryImageContainer container, out string helpMessage)
		{
			helpMessage = null;
			bool result = true;
			GalleryImageDisplay galleryImageDisplay = null;
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
			else if ((galleryImageDisplay = container.containerTemplate.gameObject.GetComponentInChildren<GalleryImageDisplay>()) == null)
			{
				helpMessage = "Invalid template: No GalleryImageDisplay component found in the children of the container template.";
				result = false;
			}
			else if (galleryImageDisplay.transform == container.containerTemplate)
			{
				helpMessage = "Invalid template: The GalleryImageDisplay component cannot share a GameObject with the container template.";
				result = false;
			}
			return result;
		}
	}
}
