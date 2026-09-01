using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Dropdown))]
	public class ExplorerDateLiveDropdownController : MonoBehaviour, IExplorerViewElement
	{
		[Serializable]
		public class OptionData
		{
			public string displayText = "All-time";

			public int filterPeriodSeconds = -1;

			public int filterRoundingSeconds;
		}

		public OptionData[] options = new OptionData[4]
		{
			new OptionData
			{
				displayText = "All-time",
				filterPeriodSeconds = -1,
				filterRoundingSeconds = 0
			},
			new OptionData
			{
				displayText = "Today",
				filterPeriodSeconds = 86400,
				filterRoundingSeconds = 3600
			},
			new OptionData
			{
				displayText = "This Week",
				filterPeriodSeconds = 604800,
				filterRoundingSeconds = 43200
			},
			new OptionData
			{
				displayText = "This Month",
				filterPeriodSeconds = 2592000,
				filterRoundingSeconds = 86400
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
				SetExplorerViewDateLiveFilter();
			});
			SetExplorerViewDateLiveFilter();
		}

		public void SetExplorerView(ExplorerView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onRequestFilterChanged.RemoveListener(DisplayDateLiveOption);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onRequestFilterChanged.AddListener(DisplayDateLiveOption);
					DisplayDateLiveOption(m_view.requestFilter);
				}
				else
				{
					DisplayDateLiveOption(null);
				}
			}
		}

		public void DisplayDateLiveOption(RequestFilter filter)
		{
			if (filter == null)
			{
				return;
			}
			List<IRequestFieldFilter> value = null;
			if (filter != null)
			{
				filter.fieldFilterMap.TryGetValue("date_live", out value);
			}
			int num = -1;
			if (value != null)
			{
				IRequestFieldFilter<int> requestFieldFilter = null;
				for (int i = 0; i < value.Count; i++)
				{
					if (requestFieldFilter != null)
					{
						break;
					}
					IRequestFieldFilter requestFieldFilter2 = value[i];
					if (requestFieldFilter2.filterMethod == FieldFilterMethod.GreaterThan || requestFieldFilter2.filterMethod == FieldFilterMethod.Minimum)
					{
						requestFieldFilter = requestFieldFilter2 as IRequestFieldFilter<int>;
					}
				}
				if (requestFieldFilter != null)
				{
					num = requestFieldFilter.filterValue;
				}
			}
			int value2 = dropdown.value;
			if (num < 0)
			{
				for (int j = 0; j < options.Length; j++)
				{
					if (options[j].filterPeriodSeconds < 0)
					{
						value2 = j;
						break;
					}
				}
			}
			else
			{
				int num2 = num - ServerTimeStamp.Now;
				int num3 = int.MaxValue;
				for (int k = 0; k < options.Length; k++)
				{
					OptionData optionData = options[k];
					if (optionData.filterPeriodSeconds >= 0)
					{
						int num4 = optionData.filterPeriodSeconds - optionData.filterRoundingSeconds;
						int num5 = num2 - num4;
						if (num5 >= 0 && num5 < num3)
						{
							value2 = k;
							num3 = num5;
						}
					}
				}
			}
			dropdown.value = value2;
		}

		public void SetExplorerViewDateLiveFilter()
		{
			if (m_view == null)
			{
				return;
			}
			OptionData selectedOption = GetSelectedOption();
			int num = -1;
			if (selectedOption != null)
			{
				int now = ServerTimeStamp.Now;
				if (selectedOption.filterPeriodSeconds > 0)
				{
					num = now - selectedOption.filterPeriodSeconds;
					int num2 = num % selectedOption.filterRoundingSeconds;
					num -= num2;
				}
			}
			MinimumFilter<int> minimumFilter = null;
			if (num > 0)
			{
				MinimumFilter<int> minimumFilter2 = new MinimumFilter<int>(0);
				minimumFilter2.minimum = num;
				minimumFilter2.isInclusive = false;
				minimumFilter = minimumFilter2;
			}
			m_view.SetFieldFilters("date_live", minimumFilter);
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
