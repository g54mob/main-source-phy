using System.Collections.Generic;
using UnityEngine;

public class CabinetController : MonoBehaviour, IOptimizable
{
	public int cabinetsID;

	public PotionCategoryType potionCategoryType;

	public List<ShelfController> shelvesList = new List<ShelfController>();

	[SerializeField]
	private PotionSignController potionSignController;

	private void Start()
	{
		for (int i = 0; i < shelvesList.Count; i++)
		{
			shelvesList[i].SetShelf(this, i);
		}
		if (potionSignController != null)
		{
			PotionsDataHolder potionsDataHolder = Object.FindAnyObjectByType<PotionsDataHolder>();
			if (potionsDataHolder != null)
			{
				potionSignController.SetSignColor(potionsDataHolder.GetCatagoryColor(potionCategoryType));
				UpdateColorBlind(SaveSystem.GetIsColorBlindSetting());
			}
		}
		SetOptimizationSetting(flag: false);
	}

	public void UpdateColorBlind(bool flag)
	{
		potionSignController.UpdateColorBlind(flag);
	}

	public ShelfController GetShelf(int id)
	{
		if (id >= shelvesList.Count)
		{
			Debug.LogError("cabient has more shelves than the saved id " + id, base.gameObject);
			return null;
		}
		return shelvesList[id];
	}

	public void SetOptimizationSetting(bool flag)
	{
		foreach (ShelfController shelves in shelvesList)
		{
			shelves.RefreshShelf(flag);
		}
	}

	public bool IsCabinetCompleted()
	{
		foreach (ShelfController shelves in shelvesList)
		{
			if (!shelves.IsShelfCompletedRight())
			{
				return false;
			}
		}
		return true;
	}

	public int GetCompletedShelvesCount()
	{
		int num = 0;
		foreach (ShelfController shelves in shelvesList)
		{
			if (shelves.IsShelfCompletedRight())
			{
				num++;
			}
		}
		return num;
	}

	public int GetPotionsCount()
	{
		int num = 0;
		foreach (ShelfController shelves in shelvesList)
		{
			num += shelves.shelfSpacesList.Count;
		}
		return num;
	}

	public void CheckShelves()
	{
		foreach (ShelfController shelves in shelvesList)
		{
			shelves.CheckShelf(isLoadingGame: true);
		}
	}
}
