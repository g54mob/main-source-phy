using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;

namespace Landfall.TABS
{
	public class CustomContentCampaignBrowser : CustomContentGridBrowser
	{
		public GameObject campaignPrefab;

		public PageCounter pageCounter;

		public TMP_InputField filterInputField;

		private List<GameObject> spawnedObjects = new List<GameObject>();

		private void SpawnCampaign(TABSCampaignAsset campaign)
		{
			spawnedObjects.Add(Object.Instantiate(campaignPrefab, base.CurrentLayoutGroup.transform).GetComponent<CustomContentCampaignButton>().Setup(campaign)
				.gameObject);
			}

			private void Clear()
			{
				spawnedObjects = new List<GameObject>();
			}

			public override void Populate(int page = 0, int newLayoutGroup = 0)
			{
				base.Populate(page, newLayoutGroup);
				DestroyDelayed(spawnedObjects);
				Clear();
				TABSCampaignAsset[] array = ContentDatabase.Instance().GetUserCampaignsByFilter(Filter.CreateMatchNamePartAndTypeFilter(filterInputField.text, UnitCreatorFactionBrowser.showDownloaded ? WorkshopTypeFilter.Workshop : WorkshopTypeFilter.Local)).ToArray();
				UpdateNewContentGraphic(default(DMNewContentManager.NewContentID), WorkshopContentType.Campaign);
				if (customContentManager != null)
				{
					if (CheckShowLoadingIconOnPopulate(page, newLayoutGroup))
					{
						return;
					}
					if (array == null || array.Length == 0)
					{
						customContentManager.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.NoContent);
						return;
					}
				}
				currentLayoutGroup = newLayoutGroup;
				totalPages = Mathf.CeilToInt((float)array.Length / (float)base.MaxItemsPerPage);
				base.CurrentPage = Mathf.Min(page, Mathf.Max(0, totalPages - 1));
				int num = array.Length - base.MaxItemsPerPage * base.CurrentPage;
				pageCounter.Set(base.CurrentPage + 1, totalPages);
				int num2 = Mathf.Min(num, base.MaxItemsPerPage);
				for (int i = 0; i < num2; i++)
				{
					int num3 = array.Length - (num - i);
					SpawnCampaign(array[num3]);
				}
				CheckContentArrayLength(array.Length);
			}
		}
	}
