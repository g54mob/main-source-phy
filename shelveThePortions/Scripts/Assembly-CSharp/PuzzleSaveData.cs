using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PuzzleSaveData
{
	[SerializeField]
	public List<PrefabTypes> avilablePuzzleItems = new List<PrefabTypes>();

	[SerializeField]
	public List<ItemSaveData> itemSaveDatasList = new List<ItemSaveData>();

	public void SaveData()
	{
		avilablePuzzleItems = new List<PrefabTypes>();
		itemSaveDatasList = new List<ItemSaveData>();
		foreach (PuzzleController puzzleControllers in SceneSingleton<PuzzlesManager>.Instance.puzzleControllersList)
		{
			foreach (ItemPlacement itemPlacements in puzzleControllers.itemPlacementsList)
			{
				if (itemPlacements.isItemAvilable)
				{
					avilablePuzzleItems.Add(itemPlacements.prefabType);
				}
			}
		}
		foreach (ItemController puzzleItemsControllers in SceneSingleton<PuzzlesManager>.Instance.puzzleItemsControllersList)
		{
			if (!avilablePuzzleItems.Contains(puzzleItemsControllers.prefabType))
			{
				itemSaveDatasList.Add(new ItemSaveData(puzzleItemsControllers));
			}
		}
	}

	public ItemSaveData GetItemSaveData(PrefabTypes prefabType)
	{
		foreach (ItemSaveData itemSaveDatas in itemSaveDatasList)
		{
			if (itemSaveDatas.itemPrefabType == prefabType)
			{
				return itemSaveDatas;
			}
		}
		Debug.LogError("Can't Find Save Data for this Item " + prefabType);
		return null;
	}
}
