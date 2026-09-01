using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	public class ModContainer : MonoBehaviour
	{
		public RectTransform containerTemplate;

		public bool hideIfEmpty;

		[SerializeField]
		private int m_itemLimit = -1;

		[Tooltip("If enabled, fills the container with hidden mod views to match the item limit.")]
		[SerializeField]
		private bool m_fillToLimit;

		private GameObject m_templateClone;

		private RectTransform m_container;

		private ModView m_itemTemplate;

		private ModView[] m_views = new ModView[0];

		private ModProfile[] m_modProfiles = new ModProfile[0];

		private ModStatistics[] m_modStatistics = new ModStatistics[0];

		public int itemLimit
		{
			get
			{
				return m_itemLimit;
			}
			set
			{
				if (m_itemLimit != value)
				{
					m_itemLimit = value;
					DisplayMods(m_modProfiles, m_modStatistics);
					if (this.onItemLimitChanged != null)
					{
						this.onItemLimitChanged(m_itemLimit);
					}
				}
			}
		}

		public ModProfile[] modProfiles
		{
			get
			{
				return m_modProfiles;
			}
		}

		public event Action<int> onItemLimitChanged;

		protected virtual void Awake()
		{
			containerTemplate.gameObject.SetActive(false);
		}

		protected virtual void Start()
		{
			Transform parent = containerTemplate.parent;
			string text = containerTemplate.gameObject.name + " (Instance)";
			int num = containerTemplate.GetSiblingIndex() + 1;
			m_itemTemplate = containerTemplate.GetComponentInChildren<ModView>(true);
			if (m_itemTemplate.gameObject.GetComponent<CanvasGroup>() == null)
			{
				m_itemTemplate.gameObject.AddComponent<CanvasGroup>();
			}
			bool flag = parent.childCount > num && parent.GetChild(num).gameObject.name == text;
			if (flag)
			{
				m_templateClone = parent.GetChild(num).gameObject;
				ModView[] componentsInChildren = m_templateClone.GetComponentsInChildren<ModView>(true);
				if (componentsInChildren == null || componentsInChildren.Length == 0)
				{
					flag = false;
					UnityEngine.Object.Destroy(m_templateClone);
				}
				else
				{
					m_container = (RectTransform)componentsInChildren[0].transform.parent;
					ModView[] array = componentsInChildren;
					foreach (ModView modView in array)
					{
						UnityEngine.Object.Destroy(modView.gameObject);
					}
				}
			}
			if (!flag)
			{
				m_templateClone = (GameObject)UnityEngine.Object.Instantiate(containerTemplate.gameObject, parent);
				m_templateClone.transform.SetSiblingIndex(num);
				m_templateClone.name = text;
				ModView componentInChildren = m_templateClone.GetComponentInChildren<ModView>(true);
				m_container = (RectTransform)componentInChildren.transform.parent;
				UnityEngine.Object.Destroy(componentInChildren.gameObject);
				m_templateClone.SetActive(true);
			}
			DisplayMods(m_modProfiles, m_modStatistics);
		}

		protected virtual void OnEnable()
		{
			DisplayMods(m_modProfiles, m_modStatistics);
		}

		public virtual void DisplayMods(IList<ModProfile> profiles, IList<ModStatistics> statistics)
		{
			if (profiles != null && statistics != null && profiles.Count != statistics.Count)
			{
				Debug.LogWarning("[mod.io] Cannot display a collection of profiles and statistics where the counts are not equal.\n profiles.Count = " + profiles.Count + "\n statistics.Count = " + statistics.Count, this);
				statistics = null;
			}
			int num = 0;
			if (profiles != null)
			{
				num = profiles.Count;
			}
			else if (statistics != null)
			{
				num = statistics.Count;
			}
			if (m_modProfiles != profiles)
			{
				m_modProfiles = new ModProfile[num];
				for (int i = 0; i < num; i++)
				{
					m_modProfiles[i] = profiles[i];
				}
			}
			if (m_modStatistics != statistics)
			{
				m_modStatistics = new ModStatistics[num];
				for (int j = 0; j < num; j++)
				{
					m_modStatistics[j] = statistics[j];
				}
			}
			if (!(m_itemTemplate != null))
			{
				return;
			}
			int num2 = num;
			if (m_fillToLimit && m_itemLimit >= 0)
			{
				num2 = m_itemLimit;
				if (num2 < num)
				{
					num = num2;
				}
			}
			UIUtilities.SetInstanceCount(m_container, m_itemTemplate, "Mod View", num2, ref m_views, true);
			if (m_fillToLimit && m_itemLimit >= 0)
			{
				int num3 = num;
				for (int k = 0; k < num3; k++)
				{
					CanvasGroup component = m_views[k].GetComponent<CanvasGroup>();
					component.alpha = 1f;
					component.interactable = true;
					component.blocksRaycasts = true;
				}
				for (int l = num3; l < num2; l++)
				{
					CanvasGroup component2 = m_views[l].GetComponent<CanvasGroup>();
					component2.alpha = 0f;
					component2.interactable = false;
					component2.blocksRaycasts = false;
				}
			}
			if (m_modProfiles != null)
			{
				for (int m = 0; m < m_modProfiles.Length && m < num2; m++)
				{
					m_views[m].profile = m_modProfiles[m];
				}
			}
			if (m_modStatistics != null)
			{
				for (int n = 0; n < m_modStatistics.Length && n < num2; n++)
				{
					m_views[n].statistics = m_modStatistics[n];
				}
			}
			m_templateClone.SetActive(num > 0 || !hideIfEmpty);
		}

		public List<ModView> GetModViews()
		{
			List<ModView> list = null;
			if (m_fillToLimit && m_views != null)
			{
				list = new List<ModView>();
				ModView[] views = m_views;
				foreach (ModView modView in views)
				{
					if (modView.gameObject.GetComponent<CanvasGroup>().alpha == 1f)
					{
						list.Add(modView);
					}
				}
			}
			else if (m_views != null)
			{
				list = new List<ModView>(m_views);
			}
			return list;
		}

		public static bool HasValidTemplate(ModContainer container, out string helpMessage)
		{
			helpMessage = null;
			bool result = true;
			ModView modView = null;
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
			else if ((modView = container.containerTemplate.gameObject.GetComponentInChildren<ModView>()) == null)
			{
				helpMessage = "Invalid template: No ModView component found in the children of the container template.";
				result = false;
			}
			else if (modView.transform == container.containerTemplate)
			{
				helpMessage = "Invalid template: The ModView component cannot share a GameObject with the container template.";
				result = false;
			}
			return result;
		}
	}
}
