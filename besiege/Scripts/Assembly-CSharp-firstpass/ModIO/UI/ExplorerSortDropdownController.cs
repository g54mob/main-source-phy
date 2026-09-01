using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Dropdown))]
	public class ExplorerSortDropdownController : MonoBehaviour, IExplorerViewElement
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

			public bool isAscending;
		}

		public OptionData[] options = new OptionData[1]
		{
			new OptionData
			{
				displayText = "Newest",
				fieldName = "date_live",
				isAscending = false
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
					m_view.onRequestFilterChanged.RemoveListener(DisplaySortOption);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onRequestFilterChanged.AddListener(DisplaySortOption);
					DisplaySortOption(m_view.requestFilter);
				}
				else
				{
					DisplaySortOption(null);
				}
			}
		}

		public void DisplaySortOption(RequestFilter filter)
		{
			if (filter != null)
			{
				DisplaySortOption(filter.sortFieldName, filter.isSortAscending);
			}
		}

		public void DisplaySortOption(string fieldName, bool isAscending)
		{
			OptionData selectedOption = GetSelectedOption();
			if (selectedOption != null && selectedOption.fieldName == fieldName && selectedOption.isAscending == isAscending)
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
				if (optionData.fieldName == fieldName && optionData.isAscending == isAscending)
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
				OptionData selectedOption = GetSelectedOption();
				if (selectedOption != null)
				{
					m_view.SetSortMethod(selectedOption.isAscending, selectedOption.fieldName);
				}
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
