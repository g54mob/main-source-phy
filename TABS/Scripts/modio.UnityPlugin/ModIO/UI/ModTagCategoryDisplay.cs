using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("No longer supported. Use TagContainer instead.")]
	[RequireComponent(typeof(ModTagCollectionDisplayComponent))]
	public class ModTagCategoryDisplay : MonoBehaviour
	{
		[Header("Settings")]
		public bool capitalizeCategory;

		[Header("UI Components")]
		public Text nameDisplay;

		public ModTagCollectionDisplayComponent tagDisplay => base.gameObject.GetComponent<ModTagCollectionDisplayComponent>();

		public void Initialize()
		{
			tagDisplay.Initialize();
		}

		public void DisplayCategory(string categoryName, IEnumerable<string> tags)
		{
			ModTagCategory category = new ModTagCategory
			{
				name = categoryName,
				tags = tags.ToArray()
			};
			DisplayCategory(category);
		}

		public void DisplayCategory(ModTagCategory category)
		{
			nameDisplay.text = (capitalizeCategory ? category.name.ToUpper() : category.name);
			tagDisplay.DisplayTags(category.tags, new ModTagCategory[1] { category });
		}
	}
}
