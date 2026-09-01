using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Dropdown))]
	[AddComponentMenu("ModIO/Explorer/Explorer Hidden Tag Dropdown Controller")]
	public class ExplorerHiddenTagDropdownController : MonoBehaviour, IExplorerViewElement
	{
		public class FieldSelectAttribute : PropertyAttribute
		{
		}

		[Serializable]
		public class OptionData
		{
			public string displayText;

			[FieldSelect]
			public string fieldName;

			public bool isNoFilterOption;
		}

		public OptionData[] options = new OptionData[1]
		{
			new OptionData
			{
				displayText = "All Types",
				fieldName = string.Empty,
				isNoFilterOption = true
			}
		};

		private ExplorerView m_view;

		public Dropdown dropdown
		{
			get
			{
				return base.gameObject.GetComponent<Dropdown>();
			}
		}

		private void Start()
		{
			dropdown.onValueChanged.AddListener(delegate
			{
				SetExplorerViewSortMethod();
			});
			SetExplorerViewSortMethod();
		}

		public void SetExplorerView(ExplorerView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onRequestFilterChanged.RemoveListener(DisplayHiddenOption);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onRequestFilterChanged.AddListener(DisplayHiddenOption);
					DisplayHiddenOption(m_view.requestFilter);
				}
				else
				{
					DisplayHiddenOption(null);
				}
			}
		}

		private void ToggleFilterRequestChangedListener(bool enable)
		{
			if (!(m_view == null))
			{
				if (enable)
				{
					m_view.onRequestFilterChanged.AddListener(DisplayHiddenOption);
				}
				else
				{
					m_view.onRequestFilterChanged.RemoveListener(DisplayHiddenOption);
				}
			}
		}

		public void DisplayHiddenOption(RequestFilter requestFilter)
		{
			List<IRequestFieldFilter> value = null;
			requestFilter.fieldFilterMap.TryGetValue("tags", out value);
			if (value != null && value.Count > 0)
			{
				foreach (IRequestFieldFilter item in value)
				{
					if (item != null && item.filterMethod == FieldFilterMethod.EquivalentCollection)
					{
						IEnumerable<string> filterTags = item.filterValue as IEnumerable<string>;
						UpdateFilterOptions(filterTags);
						break;
					}
				}
				return;
			}
			DisplaySortOption(options[0].fieldName);
		}

		private void UpdateFilterOptions(IEnumerable<string> filterTags)
		{
			bool flag = false;
			string filterTag;
			foreach (string filterTag2 in filterTags)
			{
				filterTag = filterTag2;
				OptionData optionData = options.Where((OptionData x) => x.fieldName == filterTag).FirstOrDefault();
				if (optionData != null)
				{
					DisplaySortOption(optionData.fieldName);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				DisplaySortOption(options[0].fieldName);
			}
		}

		public void DisplaySortOption(string hiddenTagName)
		{
			OptionData selectedOption = GetSelectedOption();
			if (selectedOption != null && selectedOption.fieldName == hiddenTagName)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < options.Length; i++)
			{
				if (num >= 0)
				{
					break;
				}
				OptionData optionData = options[i];
				if (optionData.fieldName == hiddenTagName)
				{
					num = i;
				}
			}
			if (num < 0)
			{
				num = 0;
			}
			dropdown.value = num;
		}

		public void SetExplorerViewSortMethod()
		{
			if (!(m_view == null))
			{
				List<string> list = new List<string>(1);
				OptionData selectedOption = GetSelectedOption();
				if (selectedOption != null && !selectedOption.isNoFilterOption)
				{
					list.Add(selectedOption.fieldName);
				}
				m_view.SetTagFilter(list);
			}
		}

		public OptionData GetSelectedOption()
		{
			if (options != null && options.Length > 0 && dropdown.options != null && dropdown.value < dropdown.options.Count)
			{
				Dropdown.OptionData optionData = dropdown.options[dropdown.value];
				OptionData[] array = options;
				foreach (OptionData optionData2 in array)
				{
					if (optionData2.displayText == optionData.text)
					{
						return optionData2;
					}
				}
			}
			return null;
		}
	}
}
