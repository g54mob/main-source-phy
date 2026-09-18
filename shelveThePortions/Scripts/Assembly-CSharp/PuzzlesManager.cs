using System.Collections.Generic;
using UnityEngine;

public class PuzzlesManager : SceneSingleton<PuzzlesManager>
{
	[SerializeField]
	public List<PuzzleController> puzzleControllersList = new List<PuzzleController>();

	[SerializeField]
	public List<PuzzleController> interactionPuzzleControllersList = new List<PuzzleController>();

	[SerializeField]
	public List<ItemController> puzzleItemsControllersList = new List<ItemController>();

	public void LoadData(PuzzleSaveData puzzleSaveData)
	{
		foreach (PrefabTypes avilablePuzzleItem in puzzleSaveData.avilablePuzzleItems)
		{
			foreach (PuzzleController puzzleControllers in puzzleControllersList)
			{
				puzzleControllers.LoadPuzzleItem(avilablePuzzleItem);
			}
		}
		foreach (ItemController puzzleItemsControllers in puzzleItemsControllersList)
		{
			if (puzzleSaveData.avilablePuzzleItems.Contains(puzzleItemsControllers.prefabType))
			{
				puzzleItemsControllers.gameObject.SetActive(value: false);
				continue;
			}
			ItemSaveData itemSaveData = puzzleSaveData.GetItemSaveData(puzzleItemsControllers.prefabType);
			if (itemSaveData != null)
			{
				puzzleItemsControllers.LoadItem(itemSaveData);
			}
		}
	}

	public void HighlightInteractionPuzzles(List<ItemController> potionControllers)
	{
		List<SortingPuzzleType> list = new List<SortingPuzzleType>();
		foreach (ItemController potionController2 in potionControllers)
		{
			if (potionController2 is PotionController { isInteractedWith: false } potionController)
			{
				if (potionController.sortingPuzzleType == SortingPuzzleType.LightInteraction_8)
				{
					list.Add(potionController.sortingPuzzleType);
				}
				if (potionController.sortingPuzzleType == SortingPuzzleType.WeightInteraction_8)
				{
					list.Add(potionController.sortingPuzzleType);
				}
				if (potionController.sortingPuzzleType == SortingPuzzleType.MusicInteraction_8)
				{
					list.Add(potionController.sortingPuzzleType);
				}
				if (potionController.sortingPuzzleType == SortingPuzzleType.CrowInteraction_5)
				{
					list.Add(potionController.sortingPuzzleType);
				}
			}
		}
		foreach (PuzzleController interactionPuzzleControllers in interactionPuzzleControllersList)
		{
			interactionPuzzleControllers.SetInteractionHighlight(list.Contains(interactionPuzzleControllers.interactionSortingPuzzleType));
		}
	}

	public void ResetAllPotionsItemsToStartingPostions()
	{
		foreach (ItemSaveData itemSaveDatas in SaveSystem.GetStartingLevelSaveData().puzzleSaveData.itemSaveDatasList)
		{
			if (!IsItemActiveInPuzzle(itemSaveDatas.itemPrefabType))
			{
				ItemController itemController = GetItemController(itemSaveDatas);
				if (itemController == null)
				{
					itemController = MasterPool.Get(itemSaveDatas.itemPrefabType).GetComponent<ItemController>();
					puzzleItemsControllersList.Add(itemController);
				}
				itemController.LoadItem(itemSaveDatas);
			}
		}
	}

	private bool IsItemActiveInPuzzle(PrefabTypes prefabType)
	{
		foreach (PuzzleController puzzleControllers in puzzleControllersList)
		{
			if (puzzleControllers.IsItemAlreadyPlaced(prefabType))
			{
				return true;
			}
		}
		return false;
	}

	private ItemController GetItemController(ItemSaveData itemSaveData)
	{
		foreach (ItemController puzzleItemsControllers in puzzleItemsControllersList)
		{
			if (puzzleItemsControllers.prefabType == itemSaveData.itemPrefabType)
			{
				return puzzleItemsControllers;
			}
		}
		return null;
	}
}
