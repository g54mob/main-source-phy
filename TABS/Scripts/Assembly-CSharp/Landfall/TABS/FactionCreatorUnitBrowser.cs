using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS.Workshop;
using TFBGames;
using TMPro;
using UnityEngine;

namespace Landfall.TABS
{
	public class FactionCreatorUnitBrowser : CustomContentGridBrowser
	{
		private struct SpawnedWrapper
		{
			public FactionCreatorUnitBrowserUnitButton spawnedObject;

			public UnitBlueprint unit;
		}

		public GameObject UnitButtonPrefab;

		public PageCounter pageCounter;

		public TMP_InputField filterInputField;

		public FactionCreatorManager factionCreator;

		[SerializeField]
		protected GameObject downloadedToggle;

		private bool showDownloaded;

		private PermissionsHelper permissionsHelper;

		private List<SpawnedWrapper> spawnedWrappers = new List<SpawnedWrapper>();

		protected override void Awake()
		{
			base.Awake();
			permissionsHelper = ServiceLocator.GetService<PermissionsHelper>();
			downloadedToggle.SetActive(permissionsHelper.CanViewDownloadTabs);
		}

		private void Start()
		{
			Populate();
		}

		private void Clear()
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < spawnedWrappers.Count; i++)
			{
				list.Add(spawnedWrappers[i].spawnedObject.gameObject);
			}
			DestroyDelayed(list);
			spawnedWrappers.Clear();
		}

		private void SpawnUnitButton(UnitBlueprint unit)
		{
			SpawnedWrapper item = new SpawnedWrapper
			{
				spawnedObject = (FactionCreatorUnitBrowserUnitButton)Object.Instantiate(UnitButtonPrefab, base.CurrentLayoutGroup.transform).GetComponent<UnitButtonBase>().Setup(unit),
				unit = unit
			};
			spawnedWrappers.Add(item);
			if (factionCreator.UnitAlreadySelected(unit, out var _))
			{
				item.spawnedObject.GetComponent<FactionCreatorUnitBrowserUnitButton>().SetSelected(newState: true);
			}
		}

		public void SetButtonState(UnitBlueprint unit, bool state)
		{
			if (TryFindUnit(unit.Entity.GUID, out var spawnedWrapper))
			{
				spawnedWrapper.spawnedObject.GetComponent<FactionCreatorUnitBrowserUnitButton>().SetSelected(state);
			}
		}

		private bool UnitIsActive(DatabaseID id)
		{
			SpawnedWrapper spawnedWrapper;
			return TryFindUnit(id, out spawnedWrapper);
		}

		private bool TryFindUnit(DatabaseID id, out SpawnedWrapper spawnedWrapper)
		{
			for (int i = 0; i < spawnedWrappers.Count; i++)
			{
				if (spawnedWrappers[i].unit.Entity.GUID == id)
				{
					spawnedWrapper = spawnedWrappers[i];
					return true;
				}
			}
			spawnedWrapper = default(SpawnedWrapper);
			return false;
		}

		public void ApplyNewColor()
		{
			for (int i = 0; i < spawnedWrappers.Count; i++)
			{
				spawnedWrappers[i].spawnedObject.ApplyNewColor();
			}
		}

		public void ApplyFilter()
		{
			instantClear = true;
			Populate(base.CurrentPage, currentLayoutGroup);
		}

		public override void Populate(int page = 0, int newLayoutGroup = 0)
		{
			base.Populate(page, newLayoutGroup);
			Clear();
			UnitBlueprint[] array = ContentDatabase.Instance().GetUserUnitBlueprintsByNamePartAndType(filterInputField.text, showDownloaded ? WorkshopTypeFilter.Workshop : WorkshopTypeFilter.Local).ToArray();
			if (customContentManager != null)
			{
				if (CheckShowLoadingIconOnPopulate(page, newLayoutGroup))
				{
					return;
				}
				if (array == null || array.Length == 0)
				{
					customContentManager?.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.NoContent);
					return;
				}
			}
			currentLayoutGroup = newLayoutGroup;
			totalPages = Mathf.CeilToInt((float)array.Length / (float)base.MaxItemsPerPage);
			base.CurrentPage = Mathf.Min(page, Mathf.Max(0, totalPages - 1));
			int num = array.Length - base.MaxItemsPerPage * base.CurrentPage;
			int num2 = Mathf.Min(num, base.MaxItemsPerPage);
			for (int i = 0; i < num2; i++)
			{
				int num3 = array.Length - (num - i);
				SpawnUnitButton(array[num3]);
			}
			CheckContentArrayLength(array.Length);
		}

		public void SetDownloaded(bool enabled)
		{
			showDownloaded = enabled;
			Refresh();
		}
	}
}
