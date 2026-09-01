using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use TagCollectionTextDisplay instead.")]
	[RequireComponent(typeof(Text))]
	public class ModTagCollectionTextDisplay : ModTagCollectionDisplayComponent
	{
		[Header("Settings")]
		public bool includeCategory;

		public string tagSeparator = ", ";

		[Header("UI Components")]
		public GameObject loadingOverlay;

		[Header("Display Data")]
		[SerializeField]
		private ModTagDisplayData[] m_data = new ModTagDisplayData[0];

		public Text text => base.gameObject.GetComponent<Text>();

		public override IEnumerable<ModTagDisplayData> data
		{
			get
			{
				return m_data;
			}
			set
			{
				if (value == null)
				{
					m_data = new ModTagDisplayData[0];
				}
				else
				{
					m_data = value.ToArray();
				}
				PresentData(m_data);
			}
		}

		public event Action<ModTagCollectionDisplayComponent> onClick;

		private void PresentData(ModTagDisplayData[] displayData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < displayData.Length; i++)
			{
				ModTagDisplayData modTagDisplayData = displayData[i];
				if (includeCategory && !string.IsNullOrEmpty(modTagDisplayData.categoryName))
				{
					stringBuilder.Append(modTagDisplayData.categoryName + ": ");
				}
				stringBuilder.Append(modTagDisplayData.tagName + tagSeparator);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length -= tagSeparator.Length;
			}
			text.text = stringBuilder.ToString();
			text.enabled = true;
			if (loadingOverlay != null)
			{
				loadingOverlay.SetActive(value: false);
			}
		}

		public override void Initialize()
		{
			_ = Application.isPlaying;
		}

		public override void DisplayTags(ModProfile profile, IEnumerable<ModTagCategory> tagCategories)
		{
			DisplayTags(profile.tagNames, tagCategories);
		}

		public override void DisplayTags(IEnumerable<string> tags, IEnumerable<ModTagCategory> tagCategories)
		{
			if (tags == null)
			{
				tags = new string[0];
			}
			m_data = ModTagDisplayData.GenerateArray(tags, tagCategories);
			PresentData(m_data);
		}

		public override void DisplayLoading()
		{
			text.text = string.Empty;
			if (loadingOverlay != null)
			{
				loadingOverlay.SetActive(value: true);
			}
		}

		public void NotifyClicked()
		{
			if (this.onClick != null)
			{
				this.onClick(this);
			}
		}
	}
}
