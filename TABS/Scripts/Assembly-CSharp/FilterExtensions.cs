using System;
using System.Collections.Generic;

public static class FilterExtensions
{
	private static char[] split = new char[1] { ' ' };

	public static List<T> ApplyFilter<T>(this List<T> filteredItems, string filter) where T : IFilterableItem
	{
		if (string.IsNullOrWhiteSpace(filter))
		{
			foreach (T filteredItem in filteredItems)
			{
				filteredItem.ItemCellGameObject.SetActive(value: true);
			}
		}
		else
		{
			string[] array = filter.ToLower().Split(split, StringSplitOptions.RemoveEmptyEntries);
			int num = array.Length;
			int count = filteredItems.Count;
			for (int i = 0; i < count; i++)
			{
				bool active = true;
				string filteringName = filteredItems[i].FilteringName;
				for (int j = 0; j < num; j++)
				{
					if (!filteringName.Contains(array[j]))
					{
						active = false;
						break;
					}
				}
				filteredItems[i].ItemCellGameObject.SetActive(active);
			}
		}
		return filteredItems;
	}
}
