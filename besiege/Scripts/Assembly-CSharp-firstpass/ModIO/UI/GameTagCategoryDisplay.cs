using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	public class GameTagCategoryDisplay : MonoBehaviour, IGameProfileUpdateReceiver
	{
		[Serializable]
		public struct TemplateData
		{
			public RectTransform root;

			public GenericTextComponent categoryLabel;

			public TagContainerItem tagTemplate;
		}

		protected class CategoryItem : MonoBehaviour
		{
			public GenericTextComponent label;

			public RectTransform tagContainer;

			public TagContainerItem[] tagInstances;
		}

		public TemplateData template = default(TemplateData);

		[Tooltip("Should hidden categories be displayed?")]
		public bool displayHidden;

		private CategoryItem m_itemTemplate;

		private RectTransform m_container;

		protected CategoryItem[] m_itemInstances = new CategoryItem[0];

		protected ModTagCategory[] m_tagCategories = new ModTagCategory[0];

		public IEnumerable<TagContainerItem> tagItems
		{
			get
			{
				CategoryItem[] itemInstances = m_itemInstances;
				foreach (CategoryItem c in itemInstances)
				{
					TagContainerItem[] tagInstances = c.tagInstances;
					for (int j = 0; j < tagInstances.Length; j++)
					{
						yield return tagInstances[j];
					}
				}
			}
		}

		public event Action<IEnumerable<TagContainerItem>> onTagsChanged;

		protected virtual void Awake()
		{
			template.root.gameObject.SetActive(false);
			if (template.tagTemplate.categoryName.displayComponent == template.categoryLabel.displayComponent)
			{
				template.tagTemplate.categoryName.SetTextDisplayComponent(null);
			}
			m_itemTemplate = template.root.gameObject.GetComponent<CategoryItem>();
			if (m_itemTemplate == null)
			{
				m_itemTemplate = template.root.gameObject.AddComponent<CategoryItem>();
				m_itemTemplate.label = template.categoryLabel;
				m_itemTemplate.tagContainer = template.tagTemplate.transform.parent as RectTransform;
				m_itemTemplate.tagInstances = new TagContainerItem[1] { template.tagTemplate };
			}
			m_container = template.root.parent as RectTransform;
			List<CategoryItem> list = new List<CategoryItem>(m_container.GetComponentsInChildren<CategoryItem>(true));
			list.Remove(m_itemTemplate);
			foreach (CategoryItem item in list)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}

		protected virtual void OnEnable()
		{
			DisplayTagCategories(m_tagCategories);
		}

		public virtual void DisplayTagCategories(IEnumerable<ModTagCategory> tagCategories)
		{
			if (m_tagCategories != tagCategories)
			{
				if (tagCategories == null)
				{
					tagCategories = new ModTagCategory[0];
				}
				List<ModTagCategory> list = new List<ModTagCategory>();
				foreach (ModTagCategory tagCategory in tagCategories)
				{
					if (tagCategory != null && tagCategory.tags != null && tagCategory.tags.Length > 0 && (displayHidden || !tagCategory.isHidden))
					{
						list.Add(tagCategory);
					}
				}
				m_tagCategories = list.ToArray();
			}
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			int num = m_tagCategories.Length;
			UIUtilities.SetInstanceCount(m_container, m_itemTemplate, "Tag Category", num, ref m_itemInstances);
			if (template.categoryLabel.displayComponent != null)
			{
				for (int i = 0; i < num; i++)
				{
					m_itemInstances[i].label.text = m_tagCategories[i].name.ToUpper();
				}
			}
			for (int j = 0; j < num; j++)
			{
				ModTagCategory modTagCategory = m_tagCategories[j];
				CategoryItem categoryItem = m_itemInstances[j];
				UIUtilities.SetInstanceCount(categoryItem.tagContainer, template.tagTemplate, "Tag", modTagCategory.tags.Length, ref categoryItem.tagInstances);
				if (template.tagTemplate.categoryName.displayComponent != null)
				{
					for (int k = 0; k < modTagCategory.tags.Length; k++)
					{
						categoryItem.tagInstances[k].categoryName.text = modTagCategory.name;
					}
				}
				for (int l = 0; l < modTagCategory.tags.Length; l++)
				{
					categoryItem.tagInstances[l].tagName.text = modTagCategory.tags[l];
				}
			}
			if (this.onTagsChanged != null)
			{
				this.onTagsChanged(tagItems);
			}
		}

		public void OnGameProfileUpdated(GameProfile gameProfile)
		{
			DisplayTagCategories(gameProfile.tagCategories);
		}

		public static bool HasValidTemplate(GameTagCategoryDisplay display, out string helpMessage)
		{
			helpMessage = null;
			bool result = true;
			if (display.template.root == null)
			{
				helpMessage = "This Game Tag Category Display has an invalid template.\nThe template root is not assigned.";
				result = false;
			}
			else if (display.template.tagTemplate == null)
			{
				helpMessage = "This Game Tag Category Display has an invalid template.\nThe tag template is not assigned.";
				result = false;
			}
			else if (!display.template.root.IsChildOf(display.transform) || display.template.root == display.transform)
			{
				helpMessage = "This Game Tag Category Display has an invalid template.\nThe template root must be a child of this object.";
				result = false;
			}
			else if (display.template.categoryLabel.displayComponent != null && !display.template.categoryLabel.displayComponent.transform.IsChildOf(display.template.root) && display.template.categoryLabel.displayComponent.transform != display.template.root)
			{
				helpMessage = "This Game Tag Category Display has an invalid template.\nThe category label must be a child of, or attached to the template root.";
				result = false;
			}
			else if (!display.template.tagTemplate.transform.IsChildOf(display.template.root) || display.template.tagTemplate.transform == display.template.root)
			{
				helpMessage = "This Game Tag Category Display has an invalid template.\nThe tag template must be a child of the template root.";
				result = false;
			}
			else if (display.template.categoryLabel.displayComponent != null && (display.template.categoryLabel.displayComponent.transform.IsChildOf(display.template.tagTemplate.transform) || display.template.categoryLabel.displayComponent.transform == display.template.tagTemplate.transform))
			{
				helpMessage = "This Game Tag Category Display has an invalid template.\nThe category label cannot be a child of, or attached to the same transform as the tag template.";
				result = false;
			}
			return result;
		}
	}
}
