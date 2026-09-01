using System;
using System.Collections.Generic;
using System.Linq;
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

		private class CategoryItem : MonoBehaviour
		{
			public GenericTextComponent label;

			public RectTransform tagContainer;

			public TagContainerItem[] tagInstances;
		}

		public TemplateData template;

		[Tooltip("Should hidden categories be displayed?")]
		public bool displayHidden;

		private CategoryItem m_itemTemplate;

		private RectTransform m_container;

		private CategoryItem[] m_itemInstances = new CategoryItem[0];

		private ModTagCategory[] m_tagCategories = new ModTagCategory[0];

		public List<string> categoryExclusion = new List<string>();

		public IEnumerable<TagContainerItem> tagItems
		{
			get
			{
				CategoryItem[] itemInstances = m_itemInstances;
				foreach (CategoryItem categoryItem in itemInstances)
				{
					TagContainerItem[] tagInstances = categoryItem.tagInstances;
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
			template.root.gameObject.SetActive(value: false);
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
			List<CategoryItem> list = new List<CategoryItem>(m_container.GetComponentsInChildren<CategoryItem>(includeInactive: true));
			list.Remove(m_itemTemplate);
			foreach (CategoryItem item in list)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
			UpdateTags();
		}

		protected virtual void OnEnable()
		{
			DisplayTagCategories(m_tagCategories);
		}

		public void UpdateTags()
		{
			ModManager.GetGameProfile(delegate(GameProfile profile)
			{
				DisplayTagCategories(profile.tagCategories);
			}, WebRequestError.LogAsWarning);
		}

		public void DisplayTagCategories(IEnumerable<ModTagCategory> tagCategories)
		{
			if (this == null)
			{
				return;
			}
			List<ModTagCategory> list = tagCategories?.ToList();
			tagCategories = ((list == null || list.Count <= 0) ? ((IEnumerable<ModTagCategory>)new ModTagCategory[0]) : ((IEnumerable<ModTagCategory>)list.Where((ModTagCategory x) => !categoryExclusion.Contains(x.name)).ToList()));
			if (m_tagCategories != tagCategories)
			{
				List<ModTagCategory> list2 = new List<ModTagCategory>();
				foreach (ModTagCategory tagCategory in tagCategories)
				{
					if (tagCategory != null && tagCategory.tags != null && tagCategory.tags.Length != 0 && (displayHidden || !tagCategory.isHidden))
					{
						list2.Add(tagCategory);
					}
				}
				m_tagCategories = list2.ToArray();
			}
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			int num = m_tagCategories.Length;
			UIUtilities.SetInstanceCount(m_container, m_itemTemplate, "Tag Category", num, ref m_itemInstances);
			if (template.categoryLabel.displayComponent != null)
			{
				for (int num2 = 0; num2 < num; num2++)
				{
					m_itemInstances[num2].label.text = m_tagCategories[num2].name;
				}
			}
			for (int num3 = 0; num3 < num; num3++)
			{
				ModTagCategory modTagCategory = m_tagCategories[num3];
				CategoryItem categoryItem = m_itemInstances[num3];
				UIUtilities.SetInstanceCount(categoryItem.tagContainer, template.tagTemplate, "Tag", modTagCategory.tags.Length, ref categoryItem.tagInstances);
				if (template.tagTemplate.categoryName.displayComponent != null)
				{
					for (int num4 = 0; num4 < modTagCategory.tags.Length; num4++)
					{
						categoryItem.tagInstances[num4].categoryName.text = modTagCategory.name;
					}
				}
				for (int num5 = 0; num5 < modTagCategory.tags.Length; num5++)
				{
					categoryItem.tagInstances[num5].TagName = modTagCategory.tags[num5];
				}
			}
			if (this.onTagsChanged != null)
			{
				this.onTagsChanged(tagItems);
			}
		}

		public void UpdateTagStates(List<string> tags)
		{
			foreach (TagContainerItem tagItem in tagItems)
			{
				if (tags.Contains(tagItem.TagName))
				{
					tagItem.GetComponent<StateToggle>().isOn = true;
				}
				else
				{
					tagItem.GetComponent<StateToggle>().isOn = false;
				}
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
