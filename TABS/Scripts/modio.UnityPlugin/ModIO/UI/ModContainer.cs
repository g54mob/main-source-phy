using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ModContainer : MonoBehaviour
	{
		public RectTransform containerTemplate;

		public static Action<ModProfile> overrideItemAction;

		public bool hideIfEmpty;

		[SerializeField]
		private int m_itemLimit = -1;

		public GameObject loadingScreen;

		[SerializeField]
		[Tooltip("If enabled, fills the container with hidden mod views to match the item limit.")]
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

		public ModProfile[] modProfiles => m_modProfiles;

		public event Action<int> onItemLimitChanged;

		protected virtual void Awake()
		{
			containerTemplate.gameObject.SetActive(value: false);
		}

		protected virtual void Start()
		{
			Transform parent = containerTemplate.parent;
			string text = containerTemplate.gameObject.name + " (Instance)";
			int num = containerTemplate.GetSiblingIndex() + 1;
			m_itemTemplate = containerTemplate.GetComponentInChildren<ModView>(includeInactive: true);
			if (m_itemTemplate.gameObject.GetComponent<CanvasGroup>() == null)
			{
				m_itemTemplate.gameObject.AddComponent<CanvasGroup>();
			}
			bool flag = parent.childCount > num && parent.GetChild(num).gameObject.name == text;
			if (flag)
			{
				m_templateClone = parent.GetChild(num).gameObject;
				ModView[] componentsInChildren = m_templateClone.GetComponentsInChildren<ModView>(includeInactive: true);
				if (componentsInChildren == null || componentsInChildren.Length == 0)
				{
					flag = false;
					UnityEngine.Object.Destroy(m_templateClone);
				}
				else
				{
					m_container = (RectTransform)componentsInChildren[0].transform.parent;
					ModView[] array = componentsInChildren;
					for (int i = 0; i < array.Length; i++)
					{
						UnityEngine.Object.Destroy(array[i].gameObject);
					}
				}
			}
			if (!flag)
			{
				m_templateClone = UnityEngine.Object.Instantiate(containerTemplate.gameObject, parent);
				m_templateClone.transform.SetSiblingIndex(num);
				m_templateClone.name = text;
				ModView componentInChildren = m_templateClone.GetComponentInChildren<ModView>(includeInactive: true);
				m_container = (RectTransform)componentInChildren.transform.parent;
				UnityEngine.Object.Destroy(componentInChildren.gameObject);
				m_templateClone.SetActive(value: true);
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
			int itemCount = 0;
			if (profiles != null)
			{
				profiles.ForEach(delegate(ModProfile x)
				{
					if (x != null && x.status == ModStatus.Accepted)
					{
						itemCount++;
					}
				});
			}
			else
			{
				statistics?.ForEach(delegate(ModStatistics x)
				{
					if (x != null)
					{
						itemCount++;
					}
				});
			}
			if (loadingScreen != null)
			{
				loadingScreen.SetActive(itemCount == 0 && (profiles == null || profiles.Count != 0));
			}
			if (m_modProfiles != profiles)
			{
				m_modProfiles = new ModProfile[itemCount];
				for (int num = 0; num < itemCount; num++)
				{
					m_modProfiles[num] = profiles[num];
				}
			}
			if (m_modStatistics != statistics)
			{
				m_modStatistics = new ModStatistics[itemCount];
				for (int num2 = 0; num2 < itemCount; num2++)
				{
					m_modStatistics[num2] = statistics[num2];
				}
			}
			if (!(m_itemTemplate != null))
			{
				return;
			}
			int num3 = itemCount;
			if (m_fillToLimit && m_itemLimit >= 0)
			{
				num3 = m_itemLimit;
				if (num3 < itemCount)
				{
					itemCount = num3;
				}
			}
			UIUtilities.SetInstanceCount(m_container, m_itemTemplate, "Mod View", num3, ref m_views, reactivateAll: true);
			if (m_fillToLimit && m_itemLimit >= 0)
			{
				int num4 = itemCount;
				for (int num5 = 0; num5 < num4; num5++)
				{
					CanvasGroup component = m_views[num5].GetComponent<CanvasGroup>();
					component.alpha = 1f;
					component.interactable = true;
					component.blocksRaycasts = true;
				}
				for (int num6 = num4; num6 < num3; num6++)
				{
					CanvasGroup component2 = m_views[num6].GetComponent<CanvasGroup>();
					component2.alpha = 0f;
					component2.interactable = false;
					component2.blocksRaycasts = false;
				}
			}
			if (m_modProfiles != null)
			{
				for (int num7 = 0; num7 < m_modProfiles.Length && num7 < num3; num7++)
				{
					ModView view = m_views[num7];
					view.profile = m_modProfiles[num7];
					Button componentInChildren = view.GetComponentInChildren<Button>();
					if (overrideItemAction != null)
					{
						componentInChildren.onClick.RemoveAllListeners();
						componentInChildren.onClick.AddListener(delegate
						{
							overrideItemAction(view.profile);
						});
					}
					else
					{
						componentInChildren.onClick.RemoveAllListeners();
						componentInChildren.onClick.AddListener(delegate
						{
							view.InspectMod();
						});
					}
					m_views[num7].profile = m_modProfiles[num7];
				}
			}
			if (m_modStatistics != null)
			{
				for (int num8 = 0; num8 < m_modStatistics.Length && num8 < num3; num8++)
				{
					m_views[num8].statistics = m_modStatistics[num8];
				}
			}
			m_templateClone.SetActive(itemCount > 0 || !hideIfEmpty);
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
