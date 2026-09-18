using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class PotionsManager : SceneSingleton<PotionsManager>
{
	[ReadOnly]
	public List<PotionController> potionControllersList = new List<PotionController>();

	[SerializeField]
	private float potionZSpacing = 2f;

	[SerializeField]
	private float potionXSpacing = 2f;

	[SerializeField]
	private float categorySpacing = 5f;

	private bool isDebugShufflingOn;

	private float debugSetPotionsToCabientsPercent = 0.25f;

	private int debugSetPotionsToCabientsCount = 10;

	private int setPotionsToCabientsIndex;

	[Space]
	[SerializeField]
	private List<PotionDebugChange> potionDebugChangesList = new List<PotionDebugChange>();

	public void LoadPotions(List<PotionSaveData> potionSaveDatasList)
	{
		foreach (PotionSaveData potionSaveDatas in potionSaveDatasList)
		{
			PotionController component = MasterPool.Get(potionSaveDatas.itemPrefabType).GetComponent<PotionController>();
			component.LoadItem(potionSaveDatas);
			potionControllersList.Add(component);
		}
		UpdateColorBlind();
	}

	public void UpdateColorBlind()
	{
		bool isColorBlindSetting = SaveSystem.GetIsColorBlindSetting();
		foreach (PotionController potionControllers in potionControllersList)
		{
			potionControllers.SetColorBlind(isColorBlindSetting);
		}
	}

	public List<PotionController> GetAllPotionsInSet(PotionController potionController)
	{
		List<PotionController> list = new List<PotionController>();
		foreach (PotionController potionControllers in potionControllersList)
		{
			if (potionControllers.potionCategoryType == potionController.potionCategoryType && potionControllers.sortingPuzzleType == potionController.sortingPuzzleType && potionControllers.prefabType == potionController.prefabType)
			{
				list.Add(potionControllers);
				if (list.Count > 8)
				{
					break;
				}
			}
		}
		return list;
	}

	public List<PotionController> GetAllPotionsInSetOnTheFloor(PotionController potionController)
	{
		List<PotionController> list = new List<PotionController>();
		foreach (PotionController potionControllers in potionControllersList)
		{
			if (!potionControllers.isPickedUp && !(potionControllers.shelfController != null) && potionControllers.potionCategoryType == potionController.potionCategoryType && potionControllers.sortingPuzzleType == potionController.sortingPuzzleType && potionControllers.prefabType == potionController.prefabType)
			{
				list.Add(potionControllers);
				if (list.Count > 8)
				{
					break;
				}
			}
		}
		return list;
	}

	public void ResetAllPotionsItemsToStartingPostions()
	{
		foreach (PotionSaveData potionSaveDatas in SaveSystem.GetStartingLevelSaveData().potionSaveDatasList)
		{
			PotionController potionController = GetPotionController(potionSaveDatas);
			if (potionController.shelfController != null)
			{
				continue;
			}
			if (potionController == null)
			{
				potionController = MasterPool.Get(potionSaveDatas.itemPrefabType).GetComponent<PotionController>();
				potionControllersList.Add(potionController);
				potionController.LoadItem(potionSaveDatas);
				continue;
			}
			potionController.DisablePhysics(instantDisable: true);
			potionController.transform.position = potionSaveDatas.position;
			potionController.transform.eulerAngles = potionSaveDatas.eulerAngles;
			if (potionSaveDatas.isInInventory)
			{
				SceneSingleton<CharacterInventory>.Instance.AddItemToInventory(potionController);
			}
		}
	}

	private PotionController GetPotionController(PotionSaveData potionSaveData)
	{
		foreach (PotionController potionControllers in potionControllersList)
		{
			if (potionControllers.prefabType == potionSaveData.itemPrefabType && potionControllers.potionCategoryType == potionSaveData.potionCategoryType && potionControllers.sortingPuzzleType == potionSaveData.sortingPuzzleType && potionControllers.orderInSet == potionSaveData.correctOrderInSet)
			{
				return potionControllers;
			}
		}
		return null;
	}

	[Button]
	public void SpawnAll()
	{
		Vector3 position = base.transform.position;
		float num = 0f;
		foreach (PotionCategory tutorialPotionCategorys in Singleton<PotionsDataHolder>.Instance.tutorialPotionCategorysList)
		{
			float num2 = 0f;
			float num3 = 0f;
			foreach (SortingPuzzleType sortingPuzzleType in tutorialPotionCategorys.sortingPuzzleTypeList)
			{
				PotionSet potionSet = new PotionSet();
				potionSet.sortingPuzzleType = sortingPuzzleType;
				potionSet.bottlePrefabType = Singleton<PotionsDataHolder>.Instance.GetPuzzleBottlesPrefabType(sortingPuzzleType);
				int puzzleBottlesInSet = Singleton<PotionsDataHolder>.Instance.GetPuzzleBottlesInSet(sortingPuzzleType);
				float num4 = 0f;
				for (int i = 0; i < puzzleBottlesInSet; i++)
				{
					Vector3 pos = position + new Vector3(num + num4, 0f, num3);
					PotionController component = MasterPool.Get(potionSet.bottlePrefabType, pos).GetComponent<PotionController>();
					component.SetPotion(tutorialPotionCategorys, potionSet, i);
					potionControllersList.Add(component);
					num4 += potionXSpacing;
				}
				num2 = Mathf.Max(num2, num4);
				num3 += potionZSpacing;
			}
			num += num2 + categorySpacing;
		}
	}

	public int GetShelvedPotionsCount()
	{
		int num = 0;
		foreach (PotionController potionControllers in potionControllersList)
		{
			if (potionControllers.shelfController != null)
			{
				num++;
			}
		}
		return num;
	}

	public string GetPotionsUIText()
	{
		return GetShelvedPotionsCount() + " / " + potionControllersList.Count;
	}

	public float GetPotionsUIPercent()
	{
		return (float)GetShelvedPotionsCount() / (float)potionControllersList.Count;
	}

	[Button]
	public void EnablePotionsPhysics()
	{
		foreach (PotionController potionControllers in potionControllersList)
		{
			if (potionControllers.shelfController == null)
			{
				potionControllers.EnablePhysics();
			}
		}
	}

	[Button]
	public void RandomizePotionsPlaces(float radius)
	{
		foreach (PotionController potionControllers in potionControllersList)
		{
			Vector3 vector = Random.insideUnitSphere * radius;
			potionControllers.transform.localPosition += new Vector3(vector.x, vector.y, vector.z);
			potionControllers.transform.localRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
		}
	}

	[Button]
	public void ShufflePotions()
	{
		List<Vector3> list = new List<Vector3>();
		List<Quaternion> list2 = new List<Quaternion>();
		foreach (PotionController potionControllers in potionControllersList)
		{
			list2.Add(potionControllers.transform.localRotation);
			list.Add(potionControllers.transform.localPosition);
		}
		for (int num = list.Count - 1; num > 0; num--)
		{
			int num2 = Random.Range(0, num + 1);
			int index = num;
			List<Vector3> list3 = list;
			int index2 = num2;
			Vector3 vector = list[num2];
			Vector3 vector2 = list[num];
			Vector3 vector3 = (list[index] = vector);
			vector3 = (list3[index2] = vector2);
			index2 = num;
			List<Quaternion> list4 = list2;
			index = num2;
			Quaternion quaternion = list2[num2];
			Quaternion quaternion2 = list2[num];
			Quaternion quaternion3 = (list2[index2] = quaternion);
			quaternion3 = (list4[index] = quaternion2);
		}
		for (int i = 0; i < potionControllersList.Count; i++)
		{
			potionControllersList[i].transform.localPosition = list[i];
			potionControllersList[i].transform.localRotation = list2[i];
		}
	}

	[Button]
	public void ShelveAllPotions()
	{
		for (int i = 0; i < 4; i++)
		{
			Debug_SetPotionsToCabients();
		}
	}

	[Button]
	public void Debug_SetPotionsToCabients()
	{
		if (!isDebugShufflingOn)
		{
			isDebugShufflingOn = true;
			potionControllersList = Randomizer.Randomize(potionControllersList);
			setPotionsToCabientsIndex = 0;
			debugSetPotionsToCabientsCount = Mathf.RoundToInt((float)potionControllersList.Count * debugSetPotionsToCabientsPercent);
		}
		for (int i = 0; i < debugSetPotionsToCabientsCount && setPotionsToCabientsIndex + i < potionControllersList.Count; i++)
		{
			Debug_SetPotionToCabinet(potionControllersList[setPotionsToCabientsIndex + i]);
		}
		setPotionsToCabientsIndex += debugSetPotionsToCabientsCount;
	}

	private void Debug_SetPotionToCabinet(PotionController potionController)
	{
		List<CabinetController> potionCabinetControllers = SceneSingleton<CabinetsManager>.Instance.GetPotionCabinetControllers(potionController);
		ShelfController shelfController = null;
		CabinetController cabinetController = null;
		foreach (CabinetController item in potionCabinetControllers)
		{
			foreach (ShelfController shelves in item.shelvesList)
			{
				if (shelves.shelfSortingPuzzleType != potionController.sortingPuzzleType)
				{
					continue;
				}
				foreach (ShelfSpace shelfSpaces in shelves.shelfSpacesList)
				{
					if (shelfSpaces.potionController != null && shelfSpaces.potionController.prefabType == potionController.prefabType)
					{
						shelfController = shelves;
						cabinetController = item;
						break;
					}
				}
				if (shelfController != null)
				{
					break;
				}
			}
			if (shelfController != null)
			{
				break;
			}
		}
		if (shelfController == null)
		{
			foreach (CabinetController item2 in potionCabinetControllers)
			{
				foreach (ShelfController shelves2 in item2.shelvesList)
				{
					if (shelves2.shelfSortingPuzzleType == SortingPuzzleType.None && shelves2.shelfSpacesList.Count == Singleton<PotionsDataHolder>.Instance.GetPuzzleBottlesInSet(potionController.sortingPuzzleType))
					{
						shelfController = shelves2;
						cabinetController = item2;
						break;
					}
				}
				if (shelfController != null)
				{
					break;
				}
			}
		}
		ShelfSpaceController shelfSpacePosition = shelfController.GetShelfSpacePosition(potionController.orderInSet);
		potionController.transform.localPosition = shelfSpacePosition.transform.position;
		potionController.transform.localEulerAngles = new Vector3(0f, shelfSpacePosition.transform.eulerAngles.y + 180f + potionController.pickupPotionRotationAngle.y, 0f);
		potionController.PutPotionOntoShelf(cabinetController.cabinetsID, shelfController.shelfID, potionController.orderInSet);
	}

	[Button]
	public void ChangePotionsPrefabs()
	{
		foreach (PotionDebugChange potionDebugChanges in potionDebugChangesList)
		{
			foreach (PotionController potionControllers in potionControllersList)
			{
				if (potionControllers.potionCategoryType == potionDebugChanges.potionCategoryType && potionControllers.sortingPuzzleType == potionDebugChanges.sortingPuzzleType && potionControllers.prefabType == potionDebugChanges.prefabType)
				{
					Debug.Log(potionControllers.prefabType.ToString() + " " + potionDebugChanges.newPrefabType, potionControllers.gameObject);
					potionControllers.prefabType = potionDebugChanges.newPrefabType;
					if (potionDebugChanges.newSortingPuzzleType != SortingPuzzleType.None)
					{
						potionControllers.sortingPuzzleType = potionDebugChanges.newSortingPuzzleType;
					}
				}
			}
		}
	}
}
