using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ModIO.UI
{
	[DisallowMultipleComponent]
	public class ModView : MonoBehaviour
	{
		[Serializable]
		public class ProfileChangedEvent : UnityEvent<ModProfile>
		{
		}

		[Serializable]
		public class StatisticsChangedEvent : UnityEvent<ModStatistics>
		{
		}

		[Serializable]
		[Obsolete]
		public struct SubmittorDisplay
		{
			public UserProfileDisplayComponent profile;

			public ImageDisplay avatar;
		}

		[Serializable]
		[Obsolete]
		public struct UserRatingDisplay
		{
			public StateToggleDisplay positive;

			public StateToggleDisplay negative;
		}

		[SerializeField]
		private ModProfile m_profile;

		[SerializeField]
		private ModStatistics m_statistics;

		[Tooltip("If the profile has no description, the description can be filled with the summary instead.")]
		public bool replaceMissingDescription = true;

		public ProfileChangedEvent onProfileChanged;

		public StatisticsChangedEvent onStatisticsChanged;

		[Obsolete("No longer supported.")]
		[HideInInspector]
		[SerializeField]
		private ModDisplayData m_data;

		[Obsolete("Use ModProfileFieldDisplay components instead.")]
		[HideInInspector]
		public ModProfileDisplayComponent profileDisplay;

		[Obsolete("Use ModLogoDisplay component instead.")]
		[HideInInspector]
		public ImageDisplay logoDisplay;

		[Obsolete("Use ModLogoDisplay, GalleryImageContainer, and YouTubeThumbnailContainer components instead.")]
		[HideInInspector]
		public ModMediaCollectionDisplayComponent mediaContainer;

		[Obsolete("Use a ModSubmittorDisplay component instead.")]
		[HideInInspector]
		public SubmittorDisplay submittorDisplay;

		[Obsolete("Use a CurrentBuildDisplay component instead.")]
		[HideInInspector]
		public ModfileDisplayComponent buildDisplay;

		[Obsolete("Use a TagContainer or TagCollectionTextDisplay component instead.")]
		[HideInInspector]
		public ModTagCollectionDisplayComponent tagsDisplay;

		[Obsolete("Use a ModEnabledDisplay component instead.")]
		[HideInInspector]
		public StateToggleDisplay modEnabledDisplay;

		[Obsolete("Use a ModSubscribedDisplay component instead.")]
		[HideInInspector]
		public StateToggleDisplay subscriptionDisplay;

		[Obsolete("Use a ModUserRatingDisplay component instead.")]
		[HideInInspector]
		public UserRatingDisplay userRatingDisplay;

		[Obsolete("Use ModStatisticsFieldDisplay components instead.")]
		[HideInInspector]
		public ModStatisticsDisplayComponent statisticsDisplay;

		[Obsolete("Use ModBinaryDownloadDisplay instead.")]
		[HideInInspector]
		public DownloadDisplayComponent downloadDisplay;

		public ModProfile profile
		{
			get
			{
				return m_profile;
			}
			set
			{
				if (m_profile != value)
				{
					m_profile = value;
					if (replaceMissingDescription && m_profile != null && string.IsNullOrEmpty(m_profile.descriptionAsText) && string.IsNullOrEmpty(m_profile.descriptionAsHTML))
					{
						m_profile.descriptionAsText = m_profile.summary;
						m_profile.descriptionAsHTML = m_profile.summary;
					}
					if (onProfileChanged != null)
					{
						onProfileChanged.Invoke(m_profile);
					}
				}
			}
		}

		public ModStatistics statistics
		{
			get
			{
				return m_statistics;
			}
			set
			{
				if (m_statistics != value)
				{
					m_statistics = value;
					if (onStatisticsChanged != null)
					{
						onStatisticsChanged.Invoke(m_statistics);
					}
				}
			}
		}

		[Obsolete("No longer supported. Use ModView.profile and ModView.statistics instead.")]
		public ModDisplayData data
		{
			get
			{
				return m_data;
			}
			set
			{
				m_data = value;
			}
		}

		[Obsolete]
		public event Action<ModView> onClick;

		[Obsolete]
		public event Action<ModView> subscribeRequested;

		[Obsolete]
		public event Action<ModView> unsubscribeRequested;

		[Obsolete]
		public event Action<ModView> enableModRequested;

		[Obsolete]
		public event Action<ModView> disableModRequested;

		[Obsolete]
		public event Action<ModView> ratePositiveRequested;

		[Obsolete]
		public event Action<ModView> rateNegativeRequested;

		protected virtual void Start()
		{
			IModViewElement[] componentsInChildren = base.gameObject.GetComponentsInChildren<IModViewElement>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetModView(this);
			}
		}

		public void InspectMod()
		{
			if (m_profile != null)
			{
				ViewManager.instance.InspectMod(m_profile.id);
			}
		}

		public void ReportMod()
		{
			if (m_profile != null)
			{
				ViewManager.instance.ReportMod(m_profile.id);
			}
		}

		public void AttemptSubscribe()
		{
			if (m_profile != null)
			{
				ModBrowser.instance.SubscribeToMod(m_profile.id);
			}
		}

		public void AttemptUnsubscribe()
		{
			if (m_profile == null)
			{
				return;
			}
			Action warningButtonCallback = delegate
			{
				ModBrowser.instance.UnsubscribeFromMod(m_profile.id);
				ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
			};
			Action standardButtonCallback = delegate
			{
				ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
			};
			Action onClose = delegate
			{
				bool isSubscribed = LocalUser.SubscribedModIds.Contains(m_profile.id);
				ModSubscribedDisplay[] componentsInChildren = base.gameObject.GetComponentsInChildren<ModSubscribedDisplay>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].DisplayModSubscribed(m_profile.id, isSubscribed);
				}
			};
			MessageDialog.Data messageData = new MessageDialog.Data
			{
				header = "Unsubscribe Confirmation",
				message = "Do you wish to unsubscribe from " + m_profile.name + " and uninstall it from your system?",
				warningButtonText = "Unsubscribe",
				warningButtonCallback = warningButtonCallback,
				standardButtonText = "Cancel",
				standardButtonCallback = standardButtonCallback,
				onClose = onClose
			};
			ViewManager.instance.ShowMessageDialog(messageData);
		}

		public void AttemptEnableMod()
		{
			if (m_profile != null)
			{
				ModBrowser.instance.EnableMod(m_profile.id);
			}
		}

		public void AttemptDisableMod()
		{
			if (m_profile != null)
			{
				ModBrowser.instance.DisableMod(m_profile.id);
			}
		}

		public void AttemptRatePositive()
		{
			if (m_profile != null)
			{
				ModBrowser.instance.AttemptRateMod(m_profile.id, ModRatingValue.Positive);
			}
		}

		public void AttemptRateNegative()
		{
			if (m_profile != null)
			{
				ModBrowser.instance.AttemptRateMod(m_profile.id, ModRatingValue.Negative);
			}
		}

		[Obsolete("No longer necessary.")]
		public void Initialize()
		{
		}

		[Obsolete("Set via ModView.profile and ModView.statistics instead.")]
		public void DisplayMod(ModProfile profile, ModStatistics statistics, IEnumerable<ModTagCategory> tagCategories, bool isSubscribed, bool isModEnabled, ModRatingValue userRating = ModRatingValue.None)
		{
			this.profile = profile;
			this.statistics = statistics;
		}

		[Obsolete("No longer supported.")]
		public void DisplayLoading()
		{
			throw new NotImplementedException();
		}

		[Obsolete("No longer supported. Use a ModBinaryDownloadDisplay component instead.")]
		public void DisplayDownload(FileDownloadInfo downloadInfo)
		{
			throw new NotImplementedException();
		}

		[Obsolete("Use InspectMod() instead.")]
		public void NotifyClicked()
		{
			if (this.onClick != null)
			{
				this.onClick(this);
			}
		}

		[Obsolete("Use AttemptSubscribe() instead.")]
		public void NotifySubscribeRequested()
		{
			if (this.subscribeRequested != null)
			{
				this.subscribeRequested(this);
			}
		}

		[Obsolete("Use AttemptUnsubscribe() instead.")]
		public void NotifyUnsubscribeRequested()
		{
			if (this.unsubscribeRequested != null)
			{
				this.unsubscribeRequested(this);
			}
		}

		[Obsolete("Use AttemptEnableMod() instead.")]
		public void NotifyEnableModRequested()
		{
			if (this.enableModRequested != null)
			{
				this.enableModRequested(this);
			}
		}

		[Obsolete("Use AttemptDisableMod() instead.")]
		public void NotifyDisableModRequested()
		{
			if (this.disableModRequested != null)
			{
				this.disableModRequested(this);
			}
		}

		[Obsolete("Use AttemptRatePositive() instead.")]
		public void NotifyRatePositiveRequested()
		{
			if (this.ratePositiveRequested != null)
			{
				this.ratePositiveRequested(this);
			}
		}

		[Obsolete("Use AttemptRateNegative() instead.")]
		public void NotifyRateNegativeRequested()
		{
			if (this.rateNegativeRequested != null)
			{
				this.rateNegativeRequested(this);
			}
		}
	}
}
