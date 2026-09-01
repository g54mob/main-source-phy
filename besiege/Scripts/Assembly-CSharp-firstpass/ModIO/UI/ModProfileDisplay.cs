using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use ModProfileFieldDisplay components instead")]
	public class ModProfileDisplay : ModProfileDisplayComponent
	{
		private delegate string GetDisplayString(ModProfileDisplayData data);

		[Header("Settings")]
		[Tooltip("If the profile has no description, the description display element(s) can be filled with the summary instead.")]
		public bool replaceMissingDescriptionWithSummary;

		[Header("UI Components")]
		public Text modIdDisplay;

		public Text gameIdDisplay;

		public Text nameDisplay;

		public Text nameIdDisplay;

		public Text statusDisplay;

		public Text visibilityDisplay;

		public Text contentWarningsDisplay;

		public Text dateAddedDisplay;

		public Text dateUpdatedDisplay;

		public Text dateLiveDisplay;

		public Text summaryDisplay;

		public Text descriptionAsHTMLDisplay;

		public Text descriptionAsTextDisplay;

		public Text homepageURLDisplay;

		public Text profileURLDisplay;

		public Text metadataBlobDisplay;

		[Header("Display Data")]
		[SerializeField]
		private ModProfileDisplayData m_data = default(ModProfileDisplayData);

		private List<TextLoadingOverlay> m_loadingOverlays = new List<TextLoadingOverlay>();

		private Dictionary<Text, GetDisplayString> m_displayMapping;

		public override ModProfileDisplayData data
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

		public override event Action<ModProfileDisplayComponent> onClick;

		private void PresentData()
		{
			if (m_displayMapping == null)
			{
				Initialize();
			}
			foreach (KeyValuePair<Text, GetDisplayString> item in m_displayMapping)
			{
				item.Key.text = item.Value(m_data);
			}
			foreach (TextLoadingOverlay loadingOverlay in m_loadingOverlays)
			{
				if (loadingOverlay != null && loadingOverlay.gameObject != null)
				{
					loadingOverlay.gameObject.SetActive(false);
				}
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
			if (modIdDisplay != null)
			{
				m_displayMapping.Add(modIdDisplay, (ModProfileDisplayData d) => d.modId.ToString());
			}
			if (gameIdDisplay != null)
			{
				m_displayMapping.Add(gameIdDisplay, (ModProfileDisplayData d) => d.gameId.ToString());
			}
			if (statusDisplay != null)
			{
				m_displayMapping.Add(statusDisplay, (ModProfileDisplayData d) => d.status.ToString());
			}
			if (visibilityDisplay != null)
			{
				m_displayMapping.Add(visibilityDisplay, (ModProfileDisplayData d) => d.visibility.ToString());
			}
			if (dateAddedDisplay != null)
			{
				m_displayMapping.Add(dateAddedDisplay, (ModProfileDisplayData d) => ServerTimeStamp.ToLocalDateTime(d.dateAdded).ToString());
			}
			if (dateUpdatedDisplay != null)
			{
				m_displayMapping.Add(dateUpdatedDisplay, (ModProfileDisplayData d) => ServerTimeStamp.ToLocalDateTime(d.dateUpdated).ToString());
			}
			if (dateLiveDisplay != null)
			{
				m_displayMapping.Add(dateLiveDisplay, (ModProfileDisplayData d) => ServerTimeStamp.ToLocalDateTime(d.dateLive).ToString());
			}
			if (contentWarningsDisplay != null)
			{
				m_displayMapping.Add(contentWarningsDisplay, (ModProfileDisplayData d) => d.contentWarnings.ToString());
			}
			if (homepageURLDisplay != null)
			{
				m_displayMapping.Add(homepageURLDisplay, (ModProfileDisplayData d) => Utility.SafeTrimString(d.homepageURL));
			}
			if (nameDisplay != null)
			{
				m_displayMapping.Add(nameDisplay, (ModProfileDisplayData d) => Utility.SafeTrimString(d.name));
			}
			if (nameIdDisplay != null)
			{
				m_displayMapping.Add(nameIdDisplay, (ModProfileDisplayData d) => Utility.SafeTrimString(d.nameId));
			}
			if (summaryDisplay != null)
			{
				m_displayMapping.Add(summaryDisplay, (ModProfileDisplayData d) => Utility.SafeTrimString(d.summary));
			}
			if (descriptionAsHTMLDisplay != null)
			{
				m_displayMapping.Add(descriptionAsHTMLDisplay, delegate(ModProfileDisplayData d)
				{
					string text = d.descriptionAsHTML;
					if (replaceMissingDescriptionWithSummary && string.IsNullOrEmpty(text))
					{
						text = d.summary;
					}
					return Utility.SafeTrimString(text);
				});
			}
			if (descriptionAsTextDisplay != null)
			{
				m_displayMapping.Add(descriptionAsTextDisplay, delegate(ModProfileDisplayData d)
				{
					string text = d.descriptionAsText;
					if (replaceMissingDescriptionWithSummary && string.IsNullOrEmpty(text))
					{
						text = d.summary;
					}
					return Utility.SafeTrimString(text);
				});
			}
			if (metadataBlobDisplay != null)
			{
				m_displayMapping.Add(metadataBlobDisplay, (ModProfileDisplayData d) => d.metadataBlob);
			}
			if (profileURLDisplay != null)
			{
				m_displayMapping.Add(profileURLDisplay, (ModProfileDisplayData d) => Utility.SafeTrimString(d.profileURL.Trim()));
			}
		}

		private void CollectLoadingOverlays()
		{
			TextLoadingOverlay[] componentsInChildren = base.gameObject.GetComponentsInChildren<TextLoadingOverlay>(true);
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

		public override void DisplayProfile(ModProfile profile)
		{
			m_data = ModProfileDisplayData.CreateFromProfile(profile);
			PresentData();
		}

		public override void DisplayLoading()
		{
			foreach (TextLoadingOverlay loadingOverlay in m_loadingOverlays)
			{
				loadingOverlay.gameObject.SetActive(true);
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
