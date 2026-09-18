using System;
using UnityEngine;

[Serializable]
public class PotionSaveData : ItemSaveData
{
	[SerializeField]
	public int correctOrderInSet;

	[SerializeField]
	public PotionCategoryType potionCategoryType;

	[SerializeField]
	public SortingPuzzleType sortingPuzzleType;

	[SerializeField]
	public int cabinetID;

	[SerializeField]
	public int shelfID;

	[SerializeField]
	public int currentOrderOnShelf;

	[SerializeField]
	public bool isInteractedWith;

	public PotionSaveData(ItemController itemController)
		: base(itemController)
	{
		if (itemController is PotionController potionController)
		{
			potionCategoryType = potionController.potionCategoryType;
			sortingPuzzleType = potionController.sortingPuzzleType;
			correctOrderInSet = potionController.orderInSet;
			isInteractedWith = potionController.isInteractedWith;
			if (potionController.shelfController == null)
			{
				cabinetID = -1;
				shelfID = -1;
				currentOrderOnShelf = -1;
			}
			else
			{
				cabinetID = potionController.shelfController.cabinetController.cabinetsID;
				shelfID = potionController.shelfController.shelfID;
				currentOrderOnShelf = potionController.currentOrderOnShelf;
			}
		}
	}
}
