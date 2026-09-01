using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use ModStatisticsFieldDisplay components instead.")]
	public class ModStatisticsDisplay : ModStatisticsDisplayComponent
	{
		private delegate string GetDisplayString(ModStatisticsDisplayData data);

		[Header("UI Components")]
		public Text popularityRankDisplay;

		public Text popularityModCountDisplay;

		public Text downloadCountDisplay;

		public Text subscriberCountDisplay;

		public Text ratingCountDisplay;

		public Text ratingPositiveCountDisplay;

		public Text ratingPositivePercentageDisplay;

		public Text ratingNegativeCountDisplay;

		public Text ratingNegativePercentageDisplay;

		public Text ratingWeightedAggregateDisplay;

		public Text ratingAsTextDisplay;

		[Header("Display Data")]
		[SerializeField]
		private ModStatisticsDisplayData m_data;

		private List<TextLoadingOverlay> m_loadingOverlays;

		private Dictionary<Text, GetDisplayString> m_displayMapping;

		public override ModStatisticsDisplayData data
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

		public override event Action<ModStatisticsDisplayComponent> onClick;

		private void PresentData()
		{
			if (m_displayMapping == null)
			{
				Initialize();
			}
			foreach (TextLoadingOverlay loadingOverlay in m_loadingOverlays)
			{
				loadingOverlay.gameObject.SetActive(value: false);
			}
			foreach (KeyValuePair<Text, GetDisplayString> item in m_displayMapping)
			{
				item.Key.text = item.Value(m_data);
			}
		}

		public override void Initialize()
		{
			if (m_displayMapping == null)
			{
				BuildDisplayMap();
				CollectLoadingOverlays();
			}
		}

		private void BuildDisplayMap()
		{
			m_displayMapping = new Dictionary<Text, GetDisplayString>();
			if (popularityRankDisplay != null)
			{
				m_displayMapping.Add(popularityRankDisplay, (ModStatisticsDisplayData s) => ValueFormatting.AbbreviateInteger(s.popularityRankPosition, "0.0"));
			}
			if (popularityModCountDisplay != null)
			{
				m_displayMapping.Add(popularityModCountDisplay, (ModStatisticsDisplayData s) => ValueFormatting.AbbreviateInteger(s.popularityRankModCount, "0.0"));
			}
			if (downloadCountDisplay != null)
			{
				m_displayMapping.Add(downloadCountDisplay, (ModStatisticsDisplayData s) => ValueFormatting.AbbreviateInteger(s.downloadCount, "0.0"));
			}
			if (subscriberCountDisplay != null)
			{
				m_displayMapping.Add(subscriberCountDisplay, (ModStatisticsDisplayData s) => ValueFormatting.AbbreviateInteger(s.subscriberCount, "0.0"));
			}
			if (ratingCountDisplay != null)
			{
				m_displayMapping.Add(ratingCountDisplay, (ModStatisticsDisplayData s) => ValueFormatting.AbbreviateInteger(s.ratingCount, "0.0"));
			}
			if (ratingPositiveCountDisplay != null)
			{
				m_displayMapping.Add(ratingPositiveCountDisplay, (ModStatisticsDisplayData s) => ValueFormatting.AbbreviateInteger(s.ratingPositiveCount, "0.0"));
			}
			if (ratingPositivePercentageDisplay != null)
			{
				m_displayMapping.Add(ratingPositivePercentageDisplay, (ModStatisticsDisplayData s) => (s.ratingCount <= 0) ? "--" : ((100f * (float)s.ratingPositiveCount / (float)s.ratingCount).ToString("0") + "%"));
			}
			if (ratingNegativeCountDisplay != null)
			{
				m_displayMapping.Add(ratingNegativeCountDisplay, (ModStatisticsDisplayData s) => ValueFormatting.AbbreviateInteger(s.ratingNegativeCount, "0.0"));
			}
			if (ratingNegativePercentageDisplay != null)
			{
				m_displayMapping.Add(ratingNegativePercentageDisplay, (ModStatisticsDisplayData s) => (s.ratingCount <= 0) ? "--" : ((100f * (float)s.ratingNegativeCount / (float)s.ratingCount).ToString("0") + "%"));
			}
			if (ratingWeightedAggregateDisplay != null)
			{
				m_displayMapping.Add(ratingWeightedAggregateDisplay, (ModStatisticsDisplayData s) => (100f * s.ratingWeightedAggregate).ToString("0") + "%");
			}
			if (ratingAsTextDisplay != null)
			{
				m_displayMapping.Add(ratingAsTextDisplay, (ModStatisticsDisplayData s) => s.ratingDisplayText);
			}
		}

		private void CollectLoadingOverlays()
		{
			TextLoadingOverlay[] componentsInChildren = base.gameObject.GetComponentsInChildren<TextLoadingOverlay>(includeInactive: true);
			List<Text> list = new List<Text>(m_displayMapping.Keys);
			m_loadingOverlays = new List<TextLoadingOverlay>();
			TextLoadingOverlay[] array = componentsInChildren;
			foreach (TextLoadingOverlay textLoadingOverlay in array)
			{
				if (list.Contains(textLoadingOverlay.textDisplayComponent))
				{
					m_loadingOverlays.Add(textLoadingOverlay);
				}
			}
		}

		public override void DisplayStatistics(ModStatistics statistics)
		{
			ModStatisticsDisplayData modStatisticsDisplayData = ModStatisticsDisplayData.CreateFromStatistics(statistics);
			m_data = modStatisticsDisplayData;
			PresentData();
		}

		public override void DisplayLoading()
		{
			foreach (TextLoadingOverlay loadingOverlay in m_loadingOverlays)
			{
				loadingOverlay.gameObject.SetActive(value: true);
			}
			foreach (Text key in m_displayMapping.Keys)
			{
				key.text = string.Empty;
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
