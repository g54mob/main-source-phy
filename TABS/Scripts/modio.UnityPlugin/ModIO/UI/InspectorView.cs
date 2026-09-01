using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(ModView))]
	public class InspectorView : MonoBehaviour, IBrowserView, ICancelHandler, IEventSystemHandler, IModSubscriptionsUpdateReceiver
	{
		public GameObject configureButton;

		public GameObject playButton;

		public GameObject subscribePlayButton;

		public GameObject downloadingPlayButton;

		public UnityEvent onAttemptedProfileChange;

		private int m_modId;

		public List<Selectable> onFocusPriority = new List<Selectable>();

		[Obsolete("Use InspectorView.highlightedImage instead.")]
		[HideInInspector]
		public ImageDisplay selectedMediaPreview;

		[Obsolete("No longer supported. Try an ObjectActiverSetter component instead.")]
		[HideInInspector]
		public GameObject loadingDisplay;

		[Obsolete("Use a ModReleaseHistoryView instead.")]
		[HideInInspector]
		public GameObject versionHistoryItemPrefab;

		[Obsolete("Use ModfileView.emptyChangelogText instead.")]
		[HideInInspector]
		public string missingVersionChangelogText;

		[Obsolete("Use a ModReleaseHistoryView instead.")]
		[HideInInspector]
		public RectTransform versionHistoryContainer;

		[Obsolete("No longer used. Refer to InspectorView.m_modId instead.")]
		[HideInInspector]
		public ModProfile profile;

		public int modId
		{
			get
			{
				return m_modId;
			}
			set
			{
				StartCoroutine(SelectFirstChild());
				if (m_modId != value)
				{
					m_modId = value;
					modView.profile = null;
					modView.statistics = null;
					configureButton.SetActive(value: false);
					playButton.SetActive(value: false);
					if (m_modId != 0)
					{
						ModProfileRequestManager.instance.RequestModProfile(m_modId, delegate(ModProfile p)
						{
							if (this != null && m_modId == value)
							{
								modView.profile = p;
								if (p != null)
								{
									modView.statistics = p.statistics;
								}
								else
								{
									modView.statistics = null;
								}
								if (LocalUser.AuthenticationState == AuthenticationState.ValidToken)
								{
									ModManager.FetchAuthenticatedUserMods(delegate(List<ModProfile> profiles)
									{
										foreach (ModProfile profile in profiles)
										{
											if (profile.id == p.id)
											{
												configureButton.SetActive(value: true);
												return;
											}
										}
										configureButton.SetActive(value: false);
									}, WebRequestError.LogAsWarning);
									UpdatePlayButton();
								}
							}
						}, null);
					}
				}
				onAttemptedProfileChange?.Invoke();
				IEnumerator SelectFirstChild()
				{
					yield return null;
					playButton.transform.parent.GetChild(0).GetComponent<Selectable>().Select();
				}
			}
		}

		public ModView modView => base.gameObject.GetComponent<ModView>();

		public CanvasGroup canvasGroup => base.gameObject.GetComponent<CanvasGroup>();

		bool IBrowserView.resetSelectionOnHide => true;

		bool IBrowserView.isRootView => false;

		List<Selectable> IBrowserView.onFocusPriority => onFocusPriority;

		GameObject IBrowserView.gameObject => base.gameObject;

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public event Action<ModProfile> subscribeRequested;

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public event Action<ModProfile> unsubscribeRequested;

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public event Action<ModProfile> enableRequested;

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public event Action<ModProfile> disableRequested;

		public void Close()
		{
			ViewManager.instance.CloseWindowedView(this);
		}

		public void OnCancel(BaseEventData eventData)
		{
			Close();
		}

		public void OnModSubscriptionsUpdated(IList<int> addedSubscriptions, IList<int> removedSubscriptions)
		{
			UpdatePlayButton();
		}

		public void UpdatePlayButton(bool assertDownloadedFiles = true)
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			playButton.SetActive(value: false);
			subscribePlayButton.SetActive(value: false);
			downloadingPlayButton.SetActive(value: false);
			ModProfileRequestManager.instance.RequestModProfile(modId, delegate(ModProfile p)
			{
				if (p != null && p.tagNames != null)
				{
					bool isPlayable = p.tagNames.Contains("Battle") || p.tagNames.Contains("Campaign") || p.tagNames.Contains("Map");
					if (isPlayable)
					{
						subscribePlayButton.SetActive(value: true);
					}
					if (LocalUser.EnabledModIds.Contains(modId))
					{
						playButton.SetActive(value: false);
						subscribePlayButton.SetActive(value: false);
						downloadingPlayButton.SetActive(value: true);
						if (assertDownloadedFiles)
						{
							StartCoroutine(ModManager.AssertDownloadedAndInstalled_Coroutine(new Modfile[1] { p.currentBuild }, delegate
							{
								if (isPlayable)
								{
									playButton.SetActive(value: true);
								}
								subscribePlayButton.SetActive(value: false);
								downloadingPlayButton.SetActive(value: false);
							}));
						}
					}
				}
			}, WebRequestError.LogAsWarning);
		}

		[Obsolete("No longer necessary. Initialization occurs in Start().")]
		public void Initialize()
		{
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public void NotifySubscribeRequested()
		{
			if (this.subscribeRequested != null)
			{
				this.subscribeRequested(modView.profile);
			}
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public void NotifyUnsubscribeRequested()
		{
			if (this.unsubscribeRequested != null)
			{
				this.unsubscribeRequested(modView.profile);
			}
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public void NotifyEnableRequested()
		{
			if (this.enableRequested != null)
			{
				this.enableRequested(modView.profile);
			}
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public void NotifyDisableRequested()
		{
			if (this.disableRequested != null)
			{
				this.disableRequested(modView.profile);
			}
		}

		[Obsolete("Use OnModSubscriptionsUpdated() instead")]
		public void DisplayModSubscribed(bool isSubscribed)
		{
			if (base.isActiveAndEnabled)
			{
				ModDisplayData data = modView.data;
				if (data.isSubscribed != isSubscribed)
				{
					data.isSubscribed = isSubscribed;
					modView.data = data;
				}
			}
		}

		[Obsolete("No longer necessary.")]
		public void DisplayModEnabled(bool isEnabled)
		{
		}

		[Obsolete("Set the modId value and/or use Refresh() instead.")]
		public void DisplayMod(ModProfile profile, ModStatistics statistics, IEnumerable<ModTagCategory> tagCategories, bool isModSubscribed, bool isModEnabled)
		{
			modId = profile.id;
		}

		[Obsolete("No longer necessary.")]
		public void SetLoadingDisplay(bool visible)
		{
		}

		[Obsolete("No longer necessary.")]
		public void Refresh()
		{
		}
	}
}
