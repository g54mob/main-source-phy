using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS.Workshop;
using LevelCreator;
using TMPro;
using UnityEngine;

public class CustomContentLevelBrowser : CustomContentGridBrowser
{
	public GameObject levelPrefab;

	public PageCounter pageCounter;

	public TMP_InputField filterInputField;

	private List<GameObject> spawnedObjects = new List<GameObject>();

	private int populateId;

	private void SpawnLevel(CustomMap customMap)
	{
		spawnedObjects.Add(Object.Instantiate(levelPrefab, base.CurrentLayoutGroup.transform).GetComponent<CustomContentLevelButton>().Setup(customMap)
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
			CustomMap[] array = ContentDatabase.Instance().GetUserMapsByFilter(Filter.CreateMatchNamePartAndTypeFilter(filterInputField.text, UnitCreatorFactionBrowser.showDownloaded ? WorkshopTypeFilter.Workshop : WorkshopTypeFilter.Local)).ToArray();
			UpdateNewContentGraphic(default(DMNewContentManager.NewContentID), WorkshopContentType.Map);
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
			if (pageCounter != null)
			{
				pageCounter.Set(base.CurrentPage + 1, totalPages);
			}
			else
			{
				Debug.LogError(pageCounter.name + " is null, please set in inspector");
			}
			int num2 = Mathf.Min(num, base.MaxItemsPerPage);
			for (int i = 0; i < num2; i++)
			{
				int num3 = array.Length - (num - i);
				SpawnLevel(array[num3]);
			}
			CheckContentArrayLength(array.Length);
		}
	}
