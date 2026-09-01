using System;
using System.Collections.Generic;
using SRDebugger.UI.Controls;
using SRF;
using UnityEngine;
using UnityEngine.UI;

namespace SRDebugger.UI.Other
{
	public class SRTabController : SRMonoBehaviourEx
	{
		private readonly SRList<SRTab> _tabs = new SRList<SRTab>();

		private SRTab _activeTab;

		[RequiredField]
		public RectTransform TabButtonContainer;

		[RequiredField]
		public SRTabButton TabButtonPrefab;

		[RequiredField]
		public RectTransform TabContentsContainer;

		[RequiredField]
		public RectTransform TabHeaderContentContainer;

		[RequiredField]
		public Text TabHeaderText;

		public SRTab ActiveTab
		{
			get
			{
				return _activeTab;
			}
			set
			{
				MakeActive(value);
			}
		}

		public IList<SRTab> Tabs
		{
			get
			{
				return _tabs.AsReadOnly();
			}
		}

		public event Action<SRTabController, SRTab> ActiveTabChanged;

		public void AddTab(SRTab tab, bool visibleInSidebar = true)
		{
			tab.CachedTransform.SetParent(TabContentsContainer, false);
			tab.CachedGameObject.SetActive(false);
			if (visibleInSidebar)
			{
				SRTabButton sRTabButton = SRInstantiate.Instantiate(TabButtonPrefab);
				sRTabButton.CachedTransform.SetParent(TabButtonContainer, false);
				sRTabButton.TitleText.text = tab.Title.ToUpper();
				if (tab.IconExtraContent != null)
				{
					RectTransform rectTransform = SRInstantiate.Instantiate(tab.IconExtraContent);
					rectTransform.SetParent(sRTabButton.ExtraContentContainer, false);
				}
				sRTabButton.IconStyleComponent.StyleKey = tab.IconStyleKey;
				sRTabButton.IsActive = false;
				sRTabButton.Button.onClick.AddListener(delegate
				{
					MakeActive(tab);
				});
				tab.TabButton = sRTabButton;
			}
			_tabs.Add(tab);
			SortTabs();
			if (_tabs.Count == 1)
			{
				ActiveTab = tab;
			}
		}

		private void MakeActive(SRTab tab)
		{
			if (!_tabs.Contains(tab))
			{
				throw new ArgumentException("tab is not a member of this tab controller", "tab");
			}
			if (_activeTab != null)
			{
				_activeTab.CachedGameObject.SetActive(false);
				if (_activeTab.TabButton != null)
				{
					_activeTab.TabButton.IsActive = false;
				}
				if (_activeTab.HeaderExtraContent != null)
				{
					_activeTab.HeaderExtraContent.gameObject.SetActive(false);
				}
			}
			_activeTab = tab;
			if (_activeTab != null)
			{
				_activeTab.CachedGameObject.SetActive(true);
				TabHeaderText.text = _activeTab.LongTitle;
				if (_activeTab.TabButton != null)
				{
					_activeTab.TabButton.IsActive = true;
				}
				if (_activeTab.HeaderExtraContent != null)
				{
					_activeTab.HeaderExtraContent.SetParent(TabHeaderContentContainer, false);
					_activeTab.HeaderExtraContent.gameObject.SetActive(true);
				}
			}
			if (this.ActiveTabChanged != null)
			{
				this.ActiveTabChanged(this, _activeTab);
			}
		}

		private void SortTabs()
		{
			_tabs.Sort((SRTab t1, SRTab t2) => t1.SortIndex.CompareTo(t2.SortIndex));
			for (int num = 0; num < _tabs.Count; num++)
			{
				if (_tabs[num].TabButton != null)
				{
					_tabs[num].TabButton.CachedTransform.SetSiblingIndex(num);
				}
			}
		}
	}
}
