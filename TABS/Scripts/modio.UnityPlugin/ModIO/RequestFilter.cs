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
				stringBuilder.Append("_sort=" + (isSortAscending ? "" : "-") + sortFieldName + "&");
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
			stringBuilder.Append($"tags-not-in={ModManager.ModVersion + 1}");
			return stringBuilder.ToString();
		}

		public void AddFieldFilter(string fieldName, IRequestFieldFilter filter)
		{
			if (string.IsNullOrEmpty(fieldName) || filter == null || filter.filterValue == null)
			{
				Debug.LogWarning("[mod.io] Attempted to add an invalid field filter to the request filter.\nfieldName=\"" + ((fieldName == null) ? "NULL" : fieldName) + "\"\nfilter=" + ((filter == null) ? "NULL" : filter.GetType().ToString()) + ((filter == null) ? string.Empty : ("\nfilterValue=" + ((filter.filterValue == null) ? "NULL" : filter.filterValue.ToString()))));
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
				MinimumFilter<T> filter2 = new MinimumFilter<T>
				{
					minimum = filter.min,
					isInclusive = filter.isMinInclusive
				};
				MaximumFilter<T> filter3 = new MaximumFilter<T>
				{
					maximum = filter.max,
					isInclusive = filter.isMaxInclusive
				};
				AddFieldFilter(fieldName, filter2);
				AddFieldFilter(fieldName, filter3);
			}
		}
	}
}
