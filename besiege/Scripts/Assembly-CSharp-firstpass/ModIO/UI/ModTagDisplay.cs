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
		private ModTagDisplayData m_data = default(ModTagDisplayData);

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
				nameDisplay.text = ((!capitalizeName) ? m_data.tagName : m_data.tagName.ToUpper());
			}
			if (categoryDisplay != null)
			{
				categoryDisplay.text = ((!capitalizeCategory) ? m_data.categoryName : m_data.categoryName.ToUpper());
			}
			if (loadingOverlay != null)
			{
				loadingOverlay.SetActive(false);
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
				categoryName = ((categoryName != null) ? categoryName : string.Empty)
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
				loadingOverlay.SetActive(true);
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
