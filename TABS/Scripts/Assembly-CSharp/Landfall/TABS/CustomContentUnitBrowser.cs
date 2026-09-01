using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;

namespace Landfall.TABS
{
	public class CustomContentUnitBrowser : CustomContentGridBrowser
	{
		public GameObject unitPrefab;

		public PageCounter pageCounter;

		private List<GameObject> spawnedObjects = new List<GameObject>();

		public TMP_InputField filterInputField;

		private void SpawnUnit(UnitBlueprint unitBlueprint)
		{
			spawnedObjects.Add(Object.Instantiate(unitPrefab, base.CurrentLayoutGroup.transform).GetComponent<CustomContentUnitButton>().Setup(unitBlueprint)
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
				UnitBlueprint[] array = ContentDatabase.Instance().GetUserUnitBlueprintsByNamePartAndType(filterInputField.text, UnitCreatorFactionBrowser.showDownloaded ? WorkshopTypeFilter.Workshop : WorkshopTypeFilter.Local).ToArray();
				UpdateNewContentGraphic(default(DMNewContentManager.NewContentID), WorkshopContentType.Unit);
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
					SpawnUnit(array[num3]);
				}
				CheckContentArrayLength(array.Length);
			}
		}
	}
