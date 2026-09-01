using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use ExplorerFilterTagsSelector instead.")]
	public class ExplorerTagFilterView : MonoBehaviour, IGameProfileUpdateReceiver
	{
		public ExplorerView view;

		[Header("Settings")]
		public GameObject tagCategoryPrefab;

		[Header("UI Components")]
		public RectTransform tagCategoryContainer;

		private ModTagCategory[] m_tagCategories = new ModTagCategory[0];

		private List<ModTagCategoryDisplay> m_categoryDisplays = new List<ModTagCategoryDisplay>();

		private List<string> m_selectedTags = new List<string>();

		public string[] selectedTags
		{
			get
			{
				return m_selectedTags.ToArray();
			}
			set
			{
				if (value == null)
				{
					value = new string[0];
				}
				bool flag = m_selectedTags.Count == value.Length;
				int num = 0;
				while (flag && num < value.Length)
				{
					flag = m_selectedTags[num] == value[num];
					num++;
				}
				if (!flag)
				{
					m_selectedTags = new List<string>(value);
					UpdateSelectionDisplay();
				}
			}
		}

		private void Start()
		{
			ModTagContainer component = tagCategoryPrefab.GetComponent<ModTagContainer>();
			view.onTagFilterUpdated += delegate(string[] t)
			{
				selectedTags = t;
			};
			string[] tagFilter = view.GetTagFilter();
			if (tagFilter == null)
			{
				m_selectedTags = new List<string>();
			}
			else
			{
				m_selectedTags = new List<string>(tagFilter);
			}
			ModTagCategory[] tagCategories = ModBrowser.instance.gameProfile.tagCategories;
			if (tagCategories != null)
			{
				m_tagCategories = tagCategories;
			}
			Refresh();
		}

		private void OnEnable()
		{
			StartCoroutine(EndOfFrameUpdateCoroutine());
		}

		private IEnumerator EndOfFrameUpdateCoroutine()
		{
			yield return null;
			LayoutRebuilder.MarkLayoutForRebuild(tagCategoryContainer);
		}

		public void Refresh()
		{
			foreach (ModTagCategoryDisplay categoryDisplay in m_categoryDisplays)
			{
				UnityEngine.Object.Destroy(categoryDisplay.gameObject);
			}
			m_categoryDisplays.Clear();
			ModTagCategory[] tagCategories = m_tagCategories;
			foreach (ModTagCategory category in tagCategories)
			{
				GameObject gameObject = CreateCategoryDisplayInstance(category, tagCategoryPrefab, tagCategoryContainer);
				gameObject.GetComponent<ModTagContainer>().tagClicked += TagClickHandler;
				m_categoryDisplays.Add(gameObject.GetComponent<ModTagCategoryDisplay>());
			}
			if (base.isActiveAndEnabled)
			{
				StartCoroutine(EndOfFrameUpdateCoroutine());
			}
		}

		private void UpdateSelectionDisplay()
		{
			foreach (ModTagCategoryDisplay categoryDisplay in m_categoryDisplays)
			{
				ModTagContainer modTagContainer = categoryDisplay.tagDisplay as ModTagContainer;
				modTagContainer.tagClicked -= TagClickHandler;
				foreach (ModTagDisplay tagDisplay in modTagContainer.tagDisplays)
				{
					Toggle component = tagDisplay.GetComponent<Toggle>();
					component.isOn = m_selectedTags.Contains(tagDisplay.data.tagName);
				}
				modTagContainer.tagClicked += TagClickHandler;
			}
		}

		private GameObject CreateCategoryDisplayInstance(ModTagCategory category, GameObject prefab, RectTransform container)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(prefab, container);
			gameObject.name = category.name;
			ModTagCategoryDisplay component = gameObject.GetComponent<ModTagCategoryDisplay>();
			component.Initialize();
			component.DisplayCategory(category);
			ToggleGroup toggleGroup = null;
			if (!category.isMultiTagCategory)
			{
				toggleGroup = component.gameObject.AddComponent<ToggleGroup>();
				toggleGroup.allowSwitchOff = true;
			}
			ModTagContainer component2 = gameObject.GetComponent<ModTagContainer>();
			foreach (ModTagDisplay tagDisplay in component2.tagDisplays)
			{
				Toggle component3 = tagDisplay.GetComponent<Toggle>();
				component3.isOn = m_selectedTags.Contains(tagDisplay.data.tagName);
				component3.group = toggleGroup;
			}
			return gameObject;
		}

		public void OnGameProfileUpdated(GameProfile gameProfile)
		{
			if (Application.isPlaying && this != null && m_tagCategories != gameProfile.tagCategories)
			{
				ModTagCategory[] array = gameProfile.tagCategories;
				if (array == null)
				{
					array = new ModTagCategory[0];
				}
				m_tagCategories = array;
				Refresh();
			}
		}

		private void TagClickHandler(ModTagDisplayComponent display)
		{
			string tagName = display.data.tagName;
			if (m_selectedTags.Contains(tagName))
			{
				m_selectedTags.Remove(tagName);
			}
			else
			{
				m_selectedTags.Add(tagName);
			}
			view.SetTagFilter(m_selectedTags);
		}

		[Obsolete("No longer necessary. Initialization occurs in Start().")]
		public void Initialize()
		{
		}
	}
}
