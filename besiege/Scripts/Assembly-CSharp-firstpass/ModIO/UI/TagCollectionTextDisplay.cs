using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ModIO.UI
{
	public class TagCollectionTextDisplay : MonoBehaviour, IGameProfileUpdateReceiver, IModViewElement
	{
		public bool includeCategory;

		public string tagSeparator = ", ";

		private GenericTextComponent m_textComponent = default(GenericTextComponent);

		private ModView m_view;

		private string[] m_tags = new string[0];

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
			Component textDisplayComponent = GenericTextComponent.FindCompatibleTextComponent(base.gameObject);
			m_textComponent.SetTextDisplayComponent(textDisplayComponent);
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
			string text = string.Empty;
			if (m_tags.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				List<string> list2 = new List<string>(m_tags);
				if (includeCategory && m_tagCategoryMap.Count > 0)
				{
					for (int i = 0; i < list2.Count; i++)
					{
						string text2 = list2[i];
						string value;
						if (m_tagCategoryMap.TryGetValue(text2, out value))
						{
							list2[i] = value + ":" + text2;
						}
					}
				}
				foreach (string item in list2)
				{
					switch (item)
					{
					case "Fits in Bounding Box":
					case "Requires Mod":
					case "Basic":
					case "Advanced":
					case "Built With Mods":
					case "Other":
					case "WIP":
					case "Machines":
					case "Levels":
					case "Skin Packs":
					case "Mods":
						continue;
					}
					stringBuilder.Append(item + tagSeparator);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length -= tagSeparator.Length;
				}
				text = stringBuilder.ToString();
			}
			m_textComponent.text = text;
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
