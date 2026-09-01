using System;
using System.Collections.Generic;

namespace ModIO.UI
{
	[Serializable]
	[Obsolete("No longer supported.")]
	public struct ModTagDisplayData
	{
		public string tagName;

		public string categoryName;

		public static ModTagDisplayData[] GenerateArray(IEnumerable<string> tagNames, IEnumerable<ModTagCategory> categories)
		{
			List<string> list = new List<string>(tagNames);
			if (list.Count == 0)
			{
				return new ModTagDisplayData[0];
			}
			if (categories == null)
			{
				categories = new List<ModTagCategory>(0);
			}
			List<ModTagDisplayData> list2 = new List<ModTagDisplayData>(list.Count);
			foreach (ModTagCategory category in categories)
			{
				string[] tags = category.tags;
				foreach (string item in tags)
				{
					if (list.Contains(item))
					{
						ModTagDisplayData item2 = new ModTagDisplayData
						{
							tagName = item,
							categoryName = category.name
						};
						list2.Add(item2);
						while (list.Remove(item))
						{
						}
						if (list.Count == 0)
						{
							return list2.ToArray();
						}
					}
				}
			}
			foreach (string item4 in list)
			{
				ModTagDisplayData item3 = new ModTagDisplayData
				{
					tagName = item4,
					categoryName = null
				};
				list2.Add(item3);
			}
			return list2.ToArray();
		}
	}
}
