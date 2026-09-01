using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;

namespace Landfall.TABS
{
	public class CustomContentFactionBrowser : CustomContentGridBrowser
	{
		public GameObject factionPrefab;

		public PageCounter pageCounter;

		public TMP_InputField filterInputField;

		private List<GameObject> spawnedObjects = new List<GameObject>();

		public int CustomFactionsCount
		{
			get
			{
				if (spawnedObjects == null)
				{
					return 0;
				}
				return spawnedObjects.Count;
			}
		}

		private void SpawnFaction(Faction faction)
		{
			spawnedObjects.Add(Object.Instantiate(factionPrefab, base.CurrentLayoutGroup.transform).GetComponent<CustomContentFactionButton>().Setup(faction)
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
				Faction[] array = ContentDatabase.Instance().GetUserFactionsByNamePartAndType(filterInputField.text, UnitCreatorFactionBrowser.showDownloaded ? WorkshopTypeFilter.Workshop : WorkshopTypeFilter.Local).ToArray();
				UpdateNewContentGraphic(default(DMNewContentManager.NewContentID), WorkshopContentType.Faction);
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
					SpawnFaction(array[num3]);
				}
				CheckContentArrayLength(array.Length);
			}
		}
	}
