using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(ModView))]
	public class InspectorView : MonoBehaviour, ICancelHandler, IEventSystemHandler, IBrowserView
	{
		private int m_modId;

		public List<Selectable> onFocusPriority = new List<Selectable>();

		[HideInInspector]
		[Obsolete("Use InspectorView.highlightedImage instead.")]
		public ImageDisplay selectedMediaPreview;

		[Obsolete("No longer supported. Try an ObjectActiverSetter component instead.")]
		[HideInInspector]
		public GameObject loadingDisplay;

		[HideInInspector]
		[Obsolete("Use a ModReleaseHistoryView instead.")]
		public GameObject versionHistoryItemPrefab;

		[Obsolete("Use ModfileView.emptyChangelogText instead.")]
		[HideInInspector]
		public string missingVersionChangelogText;

		[Obsolete("Use a ModReleaseHistoryView instead.")]
		[HideInInspector]
		public RectTransform versionHistoryContainer;

		[HideInInspector]
		[Obsolete("No longer used. Refer to InspectorView.m_modId instead.")]
		public ModProfile profile;

		bool IBrowserView.resetSelectionOnHide
		{
			get
			{
				return true;
			}
		}

		bool IBrowserView.isRootView
		{
			get
			{
				return false;
			}
		}

		List<Selectable> IBrowserView.onFocusPriority
		{
			get
			{
				return onFocusPriority;
			}
		}

		public int modId
		{
			get
			{
				return m_modId;
			}
			set
			{
				if (m_modId == value)
				{
					return;
				}
				m_modId = value;
				modView.profile = null;
				modView.statistics = null;
				if (m_modId == 0)
				{
					return;
				}
				ModManager.GetModProfile(m_modId, delegate(ModProfile p)
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
					}
				}, null);
			}
		}

		public ModView modView
		{
			get
			{
				return base.gameObject.GetComponent<ModView>();
			}
		}

		public CanvasGroup canvasGroup
		{
			get
			{
				return base.gameObject.GetComponent<CanvasGroup>();
			}
		}

		virtual GameObject IBrowserView.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

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
