using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(ModTagCollectionDisplayComponent))]
	[Obsolete("No longer supported. Use TagContainer instead.")]
	public class ModTagCategoryDisplay : MonoBehaviour
	{
		[Header("Settings")]
		public bool capitalizeCategory;

		[Header("UI Components")]
		public Text nameDisplay;

		public ModTagCollectionDisplayComponent tagDisplay
		{
			get
			{
				return base.gameObject.GetComponent<ModTagCollectionDisplayComponent>();
			}
		}

		public void Initialize()
		{
			tagDisplay.Initialize();
		}

		public void DisplayCategory(string categoryName, IEnumerable<string> tags)
		{
			ModTagCategory modTagCategory = new ModTagCategory();
			modTagCategory.name = categoryName;
			modTagCategory.tags = tags.ToArray();
			ModTagCategory category = modTagCategory;
			DisplayCategory(category);
		}

		public void DisplayCategory(ModTagCategory category)
		{
			nameDisplay.text = ((!capitalizeCategory) ? category.name : category.name.ToUpper());
			tagDisplay.DisplayTags(category.tags, new ModTagCategory[1] { category });
		}
	}
}
