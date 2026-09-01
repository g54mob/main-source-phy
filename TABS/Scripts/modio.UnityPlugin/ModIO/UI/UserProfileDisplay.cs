using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use UserProfileFieldDisplayComponents instead.")]
	public class UserProfileDisplay : UserProfileDisplayComponent
	{
		private delegate string GetDisplayString(UserProfileDisplayData data);

		[Header("UI Components")]
		public Text userIdDisplay;

		public Text nameIdDisplay;

		public Text usernameDisplay;

		public Text lastOnlineDisplay;

		public Text timezoneDisplay;

		public Text languageDisplay;

		public Text profileURLDisplay;

		[Header("Display Data")]
		[SerializeField]
		private UserProfileDisplayData m_data;

		private List<TextLoadingOverlay> m_loadingOverlays = new List<TextLoadingOverlay>();

		private Dictionary<Text, GetDisplayString> m_displayMapping;

		public override UserProfileDisplayData data
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

		public override event Action<UserProfileDisplayComponent> onClick;

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
				loadingOverlay.gameObject.SetActive(value: false);
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
			if (userIdDisplay != null)
			{
				m_displayMapping.Add(userIdDisplay, (UserProfileDisplayData d) => d.userId.ToString());
			}
			if (nameIdDisplay != null)
			{
				m_displayMapping.Add(nameIdDisplay, (UserProfileDisplayData d) => d.nameId);
			}
			if (usernameDisplay != null)
			{
				m_displayMapping.Add(usernameDisplay, (UserProfileDisplayData d) => d.username);
			}
			if (lastOnlineDisplay != null)
			{
				m_displayMapping.Add(lastOnlineDisplay, (UserProfileDisplayData d) => ServerTimeStamp.ToLocalDateTime(d.lastOnline).ToString());
			}
			if (timezoneDisplay != null)
			{
				m_displayMapping.Add(timezoneDisplay, (UserProfileDisplayData d) => d.timezone);
			}
			if (languageDisplay != null)
			{
				m_displayMapping.Add(languageDisplay, (UserProfileDisplayData d) => d.language);
			}
			if (profileURLDisplay != null)
			{
				m_displayMapping.Add(profileURLDisplay, (UserProfileDisplayData d) => d.profileURL);
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

		public override void DisplayProfile(UserProfile profile)
		{
			UserProfileDisplayData userProfileDisplayData = UserProfileDisplayData.CreateFromProfile(profile);
			m_data = userProfileDisplayData;
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
