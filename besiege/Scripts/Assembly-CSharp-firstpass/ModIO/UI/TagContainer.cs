using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	public class TagContainer : MonoBehaviour, IGameProfileUpdateReceiver, IModViewElement
	{
		public RectTransform containerTemplate;

		public bool hideIfEmpty;

		public bool IgnoreRedundantTags;

		private ModView m_view;

		private GameObject m_templateClone;

		private TagContainerItem m_itemTemplate;

		private RectTransform m_container;

		private string[] m_tags = new string[0];

		private TagContainerItem[] m_displays = new TagContainerItem[0];

		private Dictionary<string, string> m_tagCategoryMap = new Dictionary<string, string>();

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		protected virtual void Awake()
		{
			containerTemplate.gameObject.SetActive(false);
			Transform parent = containerTemplate.parent;
			string text = containerTemplate.gameObject.name + " (Instance)";
			int num = containerTemplate.GetSiblingIndex() + 1;
			m_itemTemplate = containerTemplate.GetComponentInChildren<TagContainerItem>(true);
			bool flag = parent.childCount > num && parent.GetChild(num).gameObject.name == text;
			if (flag)
			{
				m_templateClone = parent.GetChild(num).gameObject;
				TagContainerItem[] componentsInChildren = m_templateClone.GetComponentsInChildren<TagContainerItem>(true);
				if (componentsInChildren == null || componentsInChildren.Length == 0)
				{
					flag = false;
					Object.Destroy(m_templateClone);
				}
				else
				{
					m_container = (RectTransform)componentsInChildren[0].transform.parent;
					TagContainerItem[] array = componentsInChildren;
					foreach (TagContainerItem tagContainerItem in array)
					{
						Object.Destroy(tagContainerItem.gameObject);
					}
				}
			}
			if (!flag)
			{
				m_templateClone = (GameObject)Object.Instantiate(containerTemplate.gameObject, parent);
				m_templateClone.transform.SetSiblingIndex(num);
				m_templateClone.name = text;
				TagContainerItem componentInChildren = m_templateClone.GetComponentInChildren<TagContainerItem>(true);
				m_container = (RectTransform)componentInChildren.transform.parent;
				Object.Destroy(componentInChildren.gameObject);
				m_templateClone.SetActive(true);
			}
			DisplayTags(m_tags);
		}

		protected virtual void OnEnable()
		{
			DisplayTags(m_tags);
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayProfileTags);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayProfileTags);
					DisplayProfileTags(m_view.profile);
				}
				else
				{
					DisplayProfileTags(null);
				}
			}
		}

		public void DisplayProfileTags(ModProfile profile)
		{
			IEnumerable<string> tags = null;
			if (profile != null)
			{
				tags = profile.tagNames;
			}
			DisplayTags(tags);
		}

		public void DisplayTags(IEnumerable<string> tags)
		{
			if (m_tags != tags)
			{
				if (tags == null)
				{
					tags = new string[0];
				}
				List<string> list = new List<string>();
				foreach (string tag in tags)
				{
					list.Add(tag);
				}
				m_tags = list.ToArray();
			}
			if (!(m_itemTemplate != null))
			{
				return;
			}
			int num = ((!IgnoreRedundantTags) ? m_tags.Length : 0);
			string[] array = m_tags;
			if (IgnoreRedundantTags)
			{
				List<string> list2 = new List<string>();
				for (int i = 0; i < m_tags.Length; i++)
				{
					string text = m_tags[i];
					switch (text)
					{
					case "Machine Blocks":
						text = "+ Blocks";
						break;
					case "Multiplayer Supported":
						text = "Multiplayer ✓";
						break;
					case "Level Editor Objects":
						text = "+ Level Objects";
						break;
					case "Level Editor Logic":
					case "Level Editor Extensions":
						text = "Level Editor";
						break;
					case "Machine Control Utility":
						text = "Machine Controls";
						break;
					case "Fits in Bounding Box":
					case "Requires Mod":
					case "Other":
					case "Machines":
					case "Levels":
					case "Skin Packs":
					case "Mods":
					case "Miscellaneous":
						continue;
					}
					list2.Add(text.Replace(" Packs", string.Empty));
					num++;
				}
				array = list2.ToArray();
			}
			UIUtilities.SetInstanceCount(m_container, m_itemTemplate, "Tag", num, ref m_displays);
			if (m_itemTemplate.categoryName.displayComponent != null && m_tagCategoryMap.Count > 0)
			{
				for (int j = 0; j < num; j++)
				{
					string value;
					if (m_tagCategoryMap.TryGetValue(array[j], out value))
					{
						m_displays[j].categoryName.text = value.ToUpper();
					}
				}
			}
			for (int k = 0; k < num; k++)
			{
				string text2 = array[k];
				m_displays[k].tagName.text = text2.ToUpper();
			}
			m_templateClone.SetActive(num > 0 || !hideIfEmpty);
		}

		public void OnGameProfileUpdated(GameProfile gameProfile)
		{
			int num = 0;
			if (gameProfile != null && gameProfile.tagCategories != null)
			{
				num = gameProfile.tagCategories.Length;
			}
			m_tagCategoryMap = new Dictionary<string, string>(num);
			for (int i = 0; i < num; i++)
			{
				ModTagCategory modTagCategory = gameProfile.tagCategories[i];
				if (modTagCategory.tags != null)
				{
					string[] tags = modTagCategory.tags;
					foreach (string key in tags)
					{
						m_tagCategoryMap[key] = modTagCategory.name;
					}
				}
			}
			DisplayTags(m_tags);
		}

		public static bool HasValidTemplate(TagContainer container, out string helpMessage)
		{
			helpMessage = null;
			bool result = true;
			if (container.containerTemplate.gameObject == container.gameObject || container.transform.IsChildOf(container.containerTemplate))
			{
				helpMessage = "This Tag Container has an invalid template.\nThe container template cannot share the same GameObject as this Tag Container component, and cannot be a parent of this object.";
				result = false;
			}
			TagContainerItem componentInChildren = container.containerTemplate.GetComponentInChildren<TagContainerItem>(true);
			if (componentInChildren == null || container.containerTemplate.gameObject == componentInChildren.gameObject)
			{
				helpMessage = "This Tag Container has an invalid template.\nThe container template needs a child with the TagContainerItem component attached to use as the item template.";
				result = false;
			}
			return result;
		}
	}
}
