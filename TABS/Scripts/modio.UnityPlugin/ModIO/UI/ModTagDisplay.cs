using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("No longer supported.")]
	public class ModTagDisplay : ModTagDisplayComponent
	{
		[Header("Settings")]
		public bool capitalizeName;

		public bool capitalizeCategory;

		[Header("UI Components")]
		public Text nameDisplay;

		public Text categoryDisplay;

		public GameObject loadingOverlay;

		[Header("Display Data")]
		[SerializeField]
		private ModTagDisplayData m_data;

		public override ModTagDisplayData data
		{
			get
			{
				return m_data;
			}
			set
			{
				m_data = value;
				PresentData();
			}
		}

		public override event Action<ModTagDisplayComponent> onClick;

		public override void Initialize()
		{
		}

		private void PresentData()
		{
			if (nameDisplay != null)
			{
				nameDisplay.text = (capitalizeName ? m_data.tagName.ToUpper() : m_data.tagName);
			}
			if (categoryDisplay != null)
			{
				categoryDisplay.text = (capitalizeCategory ? m_data.categoryName.ToUpper() : m_data.categoryName);
			}
			if (loadingOverlay != null)
			{
				loadingOverlay.SetActive(value: false);
			}
		}

		public void DisplayTag(ModTag tag, string category)
		{
			DisplayModTag(tag.name, category);
		}

		public void DisplayTag(string tag, string category)
		{
			DisplayModTag(tag, category);
		}

		public override void DisplayModTag(ModTag tag, string categoryName)
		{
			DisplayModTag(tag.name, categoryName);
		}

		public override void DisplayModTag(string tagName, string categoryName)
		{
			ModTagDisplayData modTagDisplayData = new ModTagDisplayData
			{
				tagName = tagName,
				categoryName = ((categoryName == null) ? string.Empty : categoryName)
			};
			m_data = modTagDisplayData;
			PresentData();
		}

		public override void DisplayLoading()
		{
			nameDisplay.text = string.Empty;
			if (categoryDisplay != null)
			{
				categoryDisplay.text = string.Empty;
			}
			if (loadingOverlay != null)
			{
				loadingOverlay.SetActive(value: true);
			}
		}

		public void NotifyClicked()
		{
			if (onClick != null)
			{
				onClick(this);
			}
		}
	}
}
