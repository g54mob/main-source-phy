using System.Collections.Generic;
using System.Linq;
using DM;
using TMPro;
using UIStateManager;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class FactionCreatorIconBrowser : CustomContentGridBrowser
	{
		[Space(10f)]
		public Image m_factionBanner01;

		public Image m_factionBanner02;

		public Image m_factionBannerIcon;

		public TextMeshProUGUI m_factionBannerName;

		[Space(10f)]
		public InterfaceStateManager interfaceManager;

		public FactionCreatorManager FactionCreator;

		public GameObject iconPrefab;

		public PageCounter pageCounter;

		private List<GameObject> spawnedIcons = new List<GameObject>();

		private void Start()
		{
			Populate();
		}

		private void Clear()
		{
			spawnedIcons = new List<GameObject>();
		}

		private void SpawnIcon(FactionIcon factionIcon)
		{
			spawnedIcons.Add(Object.Instantiate(iconPrefab, base.CurrentLayoutGroup.transform).GetComponent<FactionCreatorIconBrowserIcon>().Setup(factionIcon));
		}

		public void SelectIcon(FactionIcon icon)
		{
			if (!(FactionCreator == null) && !(customContentManager == null) && !(interfaceManager == null))
			{
				FactionCreator.SelectIcon(icon);
				customContentManager.NavigateToNewFaction(init: false);
				interfaceManager.OpenUIComponent(FactionCreator.GetComponentInParent<UIComponentMainMenu>());
			}
		}

		public override void Populate(int page = 0, int newLayoutGroup = 0)
		{
			base.Populate(page, newLayoutGroup);
			DestroyDelayed(spawnedIcons);
			Clear();
			FactionIcon[] array = ContentDatabase.Instance().GetFactionIcons().ToArray();
			currentLayoutGroup = newLayoutGroup;
			totalPages = Mathf.CeilToInt((float)array.Length / (float)base.MaxItemsPerPage);
			base.CurrentPage = Mathf.Min(page, Mathf.Max(0, totalPages - 1));
			int num = array.Length - base.MaxItemsPerPage * base.CurrentPage;
			pageCounter.Set(base.CurrentPage + 1, totalPages);
			int num2 = Mathf.Min(num, base.MaxItemsPerPage);
			for (int i = 0; i < num2; i++)
			{
				int num3 = array.Length - (num - i);
				SpawnIcon(array[num3]);
			}
		}

		public void SetupFactionBanner(FactionIcon factionIcon, string factionName, Color factionColor)
		{
			m_factionBanner01.color = factionColor;
			m_factionBanner02.color = factionColor;
			m_factionBannerName.text = factionName;
			factionIcon.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && m_factionBannerIcon != null)
				{
					m_factionBannerIcon.sprite = sprite;
				}
			});
		}
	}
}
