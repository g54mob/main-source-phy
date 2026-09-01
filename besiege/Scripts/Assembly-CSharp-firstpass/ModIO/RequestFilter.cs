using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ModIO
{
	public class RequestFilter
	{
		public static readonly RequestFilter None = new RequestFilter();

		public string sortFieldName = string.Empty;

		public bool isSortAscending = true;

		public Dictionary<string, List<IRequestFieldFilter>> fieldFilterMap = new Dictionary<string, List<IRequestFieldFilter>>();

		[Obsolete("Use RequestFilter.fieldFilterMap instead.", true)]
		public Dictionary<string, IRequestFieldFilter> fieldFilters;

		public string GenerateFilterString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(sortFieldName))
			{
				stringBuilder.Append("_sort=" + ((!isSortAscending) ? "-" : string.Empty) + sortFieldName + "&");
			}
			foreach (KeyValuePair<string, List<IRequestFieldFilter>> item in fieldFilterMap)
			{
				if (item.Value == null)
				{
					continue;
				}
				foreach (IRequestFieldFilter item2 in item.Value)
				{
					if (item2 != null)
					{
						stringBuilder.Append(item2.GenerateFilterString(item.Key) + "&");
					}
				}
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Length--;
			}
			return stringBuilder.ToString();
		}

		public void AddFieldFilter(string fieldName, IRequestFieldFilter filter)
		{
			if (string.IsNullOrEmpty(fieldName) || filter == null || filter.filterValue == null)
			{
				Debug.LogWarning("[mod.io] Attempted to add an invalid field filter to the request filter.\nfieldName=\"" + ((fieldName != null) ? fieldName : "NULL") + "\"\nfilter=" + ((filter != null) ? filter.GetType().ToString() : "NULL") + ((filter != null) ? ("\nfilterValue=" + ((filter.filterValue != null) ? filter.filterValue.ToString() : "NULL")) : string.Empty));
				return;
			}
			List<IRequestFieldFilter> value = null;
			fieldFilterMap.TryGetValue(fieldName, out value);
			if (value == null)
			{
				value = new List<IRequestFieldFilter>();
				fieldFilterMap[fieldName] = value;
			}
			for (int i = 0; i < value.Count; i++)
			{
				if (value[i] != null && value[i].filterMethod == filter.filterMethod)
				{
					value.RemoveAt(i);
					break;
				}
			}
			value.Add(filter);
		}

		public void AddFieldFilter<T>(string fieldName, RangeFilter<T> filter) where T : IComparable<T>
		{
			if (filter != null)
			{
				MinimumFilter<T> minimumFilter = new MinimumFilter<T>();
				minimumFilter.minimum = filter.min;
				minimumFilter.isInclusive = filter.isMinInclusive;
				MinimumFilter<T> filter2 = minimumFilter;
				MaximumFilter<T> maximumFilter = new MaximumFilter<T>();
				maximumFilter.maximum = filter.max;
				maximumFilter.isInclusive = filter.isMaxInclusive;
				MaximumFilter<T> filter3 = maximumFilter;
				AddFieldFilter(fieldName, filter2);
				AddFieldFilter(fieldName, filter3);
			}
		}
	}
}
