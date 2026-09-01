using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	public class ModfileContainer : MonoBehaviour
	{
		public RectTransform containerTemplate;

		public bool hideIfEmpty;

		[SerializeField]
		private int m_itemLimit = -1;

		private GameObject m_templateClone;

		private RectTransform m_container;

		private ModfileView m_itemTemplate;

		private Modfile[] m_modfiles = new Modfile[0];

		private ModfileView[] m_views = new ModfileView[0];

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
					DisplayModfiles(m_modfiles);
					if (this.onItemLimitChanged != null)
					{
						this.onItemLimitChanged(m_itemLimit);
					}
				}
			}
		}

		public Modfile[] modfiles => m_modfiles;

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
			m_itemTemplate = containerTemplate.GetComponentInChildren<ModfileView>(includeInactive: true);
			bool flag = parent.childCount > num && parent.GetChild(num).gameObject.name == text;
			if (flag)
			{
				m_templateClone = parent.GetChild(num).gameObject;
				ModfileView[] componentsInChildren = m_templateClone.GetComponentsInChildren<ModfileView>(includeInactive: true);
				if (componentsInChildren == null || componentsInChildren.Length == 0)
				{
					flag = false;
					UnityEngine.Object.Destroy(m_templateClone);
				}
				else
				{
					m_container = (RectTransform)componentsInChildren[0].transform.parent;
					ModfileView[] array = componentsInChildren;
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
				ModfileView componentInChildren = m_templateClone.GetComponentInChildren<ModfileView>(includeInactive: true);
				m_container = (RectTransform)componentInChildren.transform.parent;
				UnityEngine.Object.Destroy(componentInChildren.gameObject);
				m_templateClone.SetActive(value: true);
			}
			DisplayModfiles(m_modfiles);
		}

		protected virtual void OnEnable()
		{
			DisplayModfiles(m_modfiles);
		}

		public virtual void DisplayModfiles(IList<Modfile> modfiles)
		{
			if (m_modfiles != modfiles)
			{
				int num = 0;
				if (modfiles != null)
				{
					num = modfiles.Count;
				}
				m_modfiles = new Modfile[num];
				for (int i = 0; i < num; i++)
				{
					m_modfiles[i] = modfiles[i];
				}
			}
			if (m_itemTemplate != null)
			{
				int num2 = m_modfiles.Length;
				if (m_itemLimit >= 0 && m_itemLimit < num2)
				{
					num2 = m_itemLimit;
				}
				UIUtilities.SetInstanceCount(m_container, m_itemTemplate, "Modfile View", num2, ref m_views);
				for (int j = 0; j < num2; j++)
				{
					m_views[j].modfile = m_modfiles[j];
				}
				m_templateClone.SetActive(num2 > 0 || !hideIfEmpty);
			}
		}

		public static bool HasValidTemplate(ModfileContainer container, out string helpMessage)
		{
			helpMessage = null;
			bool result = true;
			ModfileView modfileView = null;
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
			else if ((modfileView = container.containerTemplate.gameObject.GetComponentInChildren<ModfileView>()) == null)
			{
				helpMessage = "Invalid template: No ModfileView component found in the children of the container template.";
				result = false;
			}
			else if (modfileView.transform == container.containerTemplate)
			{
				helpMessage = "Invalid template: The ModfileView component cannot share a GameObject with the container template.";
				result = false;
			}
			return result;
		}
	}
}
