using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class SeedRadialMenu : MonoBehaviour
	{
		public SeedCollectionTable seedCollectionTableInitial;

		private SeedCollectionTable seedCollectionTable;

		public RadialMenu radialMenuPrefabInitial;

		private RadialMenu radialMenuPrefab;

		public string defaultSeedCollection;

		private static RadialMenu radialMenuManager;

		private static DMEditor dmEditor;

		private void Awake()
		{
			seedCollectionTable = seedCollectionTableInitial;
			radialMenuPrefab = radialMenuPrefabInitial;
		}

		private void Start()
		{
			if (SeedCollection.selectedCollection == null)
			{
				SeedCollectionRow rowValue = seedCollectionTable.GetRowValue(defaultSeedCollection);
				if (rowValue != null)
				{
					SeedCollection.selectedCollection = rowValue.seeds;
					SeedCollection.spawnRate = rowValue.spawnRate;
					SeedCollection.densityRangeCheck = rowValue.densityRangeCheck;
				}
			}
		}

		public RadialMenu GetRadialMenu()
		{
			if (dmEditor == null)
			{
				dmEditor = DMEditor.Instance;
			}
			if (radialMenuManager == null)
			{
				radialMenuManager = Object.Instantiate(radialMenuPrefab, dmEditor.playerCanvasRenderer.transform);
				List<RadialMenuItem> radialMenuItems = new List<RadialMenuItem>();
				seedCollectionTable.ForEachRow(delegate(string key, SeedCollectionRow seedCollectionObj)
				{
					radialMenuItems.Add(new RadialMenuItem
					{
						Id = key,
						Path = seedCollectionObj.Path,
						DisplayName = seedCollectionObj.name,
						Tooltip = "",
						Icon = seedCollectionObj.icon,
						Tint = Color.white
					});
				});
				radialMenuManager.SetRadialMenuData(radialMenuItems);
				radialMenuManager.onItemSelected.AddListener(delegate(string id)
				{
					SeedCollectionRow rowValue2 = seedCollectionTable.GetRowValue(id);
					SeedCollection.selectedCollection = rowValue2.seeds;
					SeedCollection.spawnRate = rowValue2.spawnRate;
					SeedCollection.densityRangeCheck = rowValue2.densityRangeCheck;
				});
				if (SeedCollection.selectedCollection == null)
				{
					SeedCollectionRow rowValue = seedCollectionTable.GetRowValue(defaultSeedCollection);
					if (rowValue != null)
					{
						SeedCollection.selectedCollection = rowValue.seeds;
					}
				}
			}
			return radialMenuManager;
		}
	}
}
