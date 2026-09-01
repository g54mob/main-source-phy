using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace ModIO.UI
{
	public class TagCollectionTextDisplay : MonoBehaviour, IModViewElement, IGameProfileUpdateReceiver
	{
		public bool includeCategory;

		public int maxTags = 20;

		public string tagSeparator = ", ";

		private ModView m_view;

		public GenericTextComponent textTemplate;

		private GenericTextComponent m_textComponent;

		public Transform textTemplateContainer;

		public List<string> categoryExclusion;

		private string[] m_tags = new string[0];

		private Dictionary<string, string> m_tagCategoryMap = new Dictionary<string, string>();

		GameObject IModViewElement.gameObject => base.gameObject;

		private void GenerateTextObject()
		{
			Component component = Object.Instantiate(textTemplate.displayComponent, textTemplateContainer);
			component.gameObject.SetActive(value: true);
			m_textComponent.SetTextDisplayComponent(component);
		}

		protected virtual void OnEnable()
		{
			DisplayTags(m_tags);
		}

		public void SetModView(ModView view)
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
			if (textTemplateContainer == null)
			{
				return;
			}
			foreach (Transform item in textTemplateContainer)
			{
				Object.Destroy(item.gameObject);
			}
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
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			_ = string.Empty;
			if (m_tags.Length != 0 && m_tags.Length != 0)
			{
				List<string> list2 = new List<string>(m_tags);
				int num = Mathf.Min(m_tags.Length, maxTags);
				list2.SetLength(num);
				if (includeCategory && m_tagCategoryMap.Count > 0)
				{
					for (int i = 0; i < list2.Count; i++)
					{
						string text = list2[i];
						if (m_tagCategoryMap.TryGetValue(text, out var value) && !categoryExclusion.Contains(value))
						{
							list2[i] = value + ":" + text;
						}
					}
				}
				int num2 = 0;
				string[] tags2 = m_tags;
				foreach (string text2 in tags2)
				{
					if (m_tagCategoryMap.TryGetValue(text2, out var value2) && !categoryExclusion.Contains(value2))
					{
						if (num2 >= num)
						{
							break;
						}
						GenerateTextObject();
						m_textComponent.text = text2;
						GenerateTextObject();
						m_textComponent.text = tagSeparator;
						num2++;
					}
				}
				if (textTemplateContainer.childCount > 1)
				{
					Object.Destroy(textTemplateContainer.GetChild(textTemplateContainer.childCount - 1).gameObject);
				}
			}
			Canvas.ForceUpdateCanvases();
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
	}
}
