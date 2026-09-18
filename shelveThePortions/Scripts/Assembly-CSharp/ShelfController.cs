using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class ShelfController : MonoBehaviour
{
	public int shelfID;

	public Vector3 position;

	public PrefabTypes highlightPrefabType;

	[ReadOnly]
	public CabinetController cabinetController;

	public List<ShelfSpace> shelfSpacesList = new List<ShelfSpace>();

	public SortingPuzzleType shelfSortingPuzzleType;

	private float glowSpreadInterval = 0.1f;

	private ShelveHighlight shelveHighlight;

	public void SetShelf(CabinetController cabinetController, int id)
	{
		shelfID = id;
		this.cabinetController = cabinetController;
	}

	public void RefreshShelf(bool active)
	{
		int num = 0;
		foreach (ShelfSpace shelfSpaces in shelfSpacesList)
		{
			if (active)
			{
				if (shelfSpaces.potionController == null)
				{
					AddShelfSpaceController(shelfSpaces, num);
				}
				else
				{
					RemoveShelfSpaceController(shelfSpaces);
				}
			}
			else
			{
				RemoveShelfSpaceController(shelfSpaces);
			}
			num++;
		}
	}

	public void AddPotion(PotionController potionController, int index, bool isLoading)
	{
		ShelfSpace shelfSpace = shelfSpacesList[index];
		shelfSpace.potionController = potionController;
		RemoveShelfSpaceController(shelfSpace);
		if (potionController.potionCategoryType == cabinetController.potionCategoryType && shelfSortingPuzzleType == SortingPuzzleType.None)
		{
			shelfSortingPuzzleType = potionController.sortingPuzzleType;
		}
		if (!isLoading)
		{
			Invoke("CheckShelfAfterAddingPotionInvoke", potionController.itemMoveTweenDurationfloat);
			StopAllCoroutines();
			StartCoroutine(ShowShelveGlow(potionController));
		}
		DisableHighlight();
	}

	public void CheckShelfAfterAddingPotionInvoke()
	{
		CheckShelf(isLoadingGame: false);
	}

	public void CheckShelf(bool isLoadingGame)
	{
		RefreshAllShelfSpacePlacementType();
		if (IsShelveFull())
		{
			if (IsShelfCompletedRight())
			{
				if (Singleton<PotionsDataHolder>.Instance.solvedPuzzlesList.Contains(shelfSortingPuzzleType))
				{
					Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Potion_Shelve_Completed);
				}
				Singleton<PotionsDataHolder>.Instance.AddSolvedPuzzle(shelfSortingPuzzleType);
				SceneSingleton<TutorialManager>.Instance.CompleteTutorialStep(TutorialStepType.Tutorial_Complete_Shelf);
			}
			else
			{
				Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Potion_Shelve_Completed_wrong);
			}
		}
		if (!isLoadingGame)
		{
			SceneSingleton<CabinetsManager>.Instance.UpdateShelve();
		}
	}

	private IEnumerator ShowShelveGlow(PotionController potionController)
	{
		yield return new WaitForSeconds(potionController.itemMoveTweenDurationfloat);
		if (!IsShelveFull())
		{
			yield break;
		}
		int centerIndex = -1;
		for (int i = 0; i < shelfSpacesList.Count; i++)
		{
			if (shelfSpacesList[i].potionController == potionController)
			{
				centerIndex = i;
				break;
			}
		}
		if (centerIndex == -1)
		{
			yield break;
		}
		bool isShelfComplete = IsShelfCompletedRight();
		potionController.ShowCompleteGlow(isShelfComplete);
		yield return new WaitForSeconds(glowSpreadInterval);
		for (int distance = 1; distance < shelfSpacesList.Count; distance++)
		{
			bool flag = false;
			int num = centerIndex - distance;
			if (num >= 0 && shelfSpacesList[num].potionController != null)
			{
				shelfSpacesList[num].potionController.ShowCompleteGlow(isShelfComplete);
				flag = true;
			}
			int num2 = centerIndex + distance;
			if (num2 < shelfSpacesList.Count && shelfSpacesList[num2].potionController != null)
			{
				shelfSpacesList[num2].potionController.ShowCompleteGlow(isShelfComplete);
				flag = true;
			}
			if (flag)
			{
				yield return new WaitForSeconds(glowSpreadInterval);
			}
		}
	}

	public void RemovePotion(int index, PotionController potionController)
	{
		DisableHighlight();
		ShelfSpace shelfSpace = shelfSpacesList[index];
		shelfSpacesList[index].potionController = null;
		shelfSpacesList[index].shelfSpacePlacementType = ShelfSpacePlacementType.None;
		AddShelfSpaceController(shelfSpace, index);
		SortingPuzzleType sortingPuzzleType = potionController.sortingPuzzleType;
		bool flag = false;
		foreach (ShelfSpace shelfSpaces in shelfSpacesList)
		{
			if (!(shelfSpaces.potionController == null) && !(shelfSpaces.potionController == potionController) && shelfSpaces.potionController.sortingPuzzleType == sortingPuzzleType)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			shelfSortingPuzzleType = SortingPuzzleType.None;
			foreach (ShelfSpace shelfSpaces2 in shelfSpacesList)
			{
				if (!(shelfSpaces2.potionController == null) && !(shelfSpaces2.potionController == potionController) && shelfSpaces2.potionController.potionCategoryType == cabinetController.potionCategoryType)
				{
					shelfSortingPuzzleType = shelfSpaces2.potionController.sortingPuzzleType;
					break;
				}
			}
		}
		foreach (ShelfSpace shelfSpaces3 in shelfSpacesList)
		{
			RefreshShelfSpacePlacementType(shelfSpaces3);
		}
		SceneSingleton<CabinetsManagerUI>.Instance.UpdateShelveUI();
		if (shelveHighlight != null)
		{
			shelveHighlight = null;
		}
	}

	private void AddShelfSpaceController(ShelfSpace shelfSpace, int index)
	{
		if (!(shelfSpace.shelfSpaceController != null))
		{
			shelfSpace.shelfSpaceController = MasterPool.Get(PrefabTypes.ShelfSpaceController, base.transform).GetComponent<ShelfSpaceController>();
			shelfSpace.shelfSpaceController.SetShelfSpace(this, index);
			shelfSpace.shelfSpaceController.transform.localPosition = position + shelfSpace.position;
			shelfSpace.shelfSpaceController.transform.localEulerAngles = Vector3.zero;
		}
	}

	private void RemoveShelfSpaceController(ShelfSpace shelfSpace)
	{
		if (!(shelfSpace.shelfSpaceController == null))
		{
			MasterPool.ReturnToPoolTransform(shelfSpace.shelfSpaceController.gameObject, PrefabTypes.ShelfSpaceController);
			shelfSpace.shelfSpaceController = null;
		}
	}

	public bool IsShelveFull()
	{
		foreach (ShelfSpace shelfSpaces in shelfSpacesList)
		{
			if (shelfSpaces.potionController == null)
			{
				return false;
			}
		}
		return true;
	}

	public bool IsShelfCompletedRight()
	{
		foreach (ShelfSpace shelfSpaces in shelfSpacesList)
		{
			if (shelfSpaces.shelfSpacePlacementType != ShelfSpacePlacementType.Right)
			{
				return false;
			}
		}
		return true;
	}

	public ShelfSpaceController GetShelfSpacePosition(int id)
	{
		AddShelfSpaceController(shelfSpacesList[id], id);
		return shelfSpacesList[id].shelfSpaceController;
	}

	private void RefreshAllShelfSpacePlacementType()
	{
		foreach (ShelfSpace shelfSpaces in shelfSpacesList)
		{
			RefreshShelfSpacePlacementType(shelfSpaces);
		}
	}

	private void RefreshShelfSpacePlacementType(ShelfSpace shelfSpace)
	{
		if (shelfSpace.potionController == null)
		{
			shelfSpace.shelfSpacePlacementType = ShelfSpacePlacementType.None;
		}
		else if (shelfSpace.potionController.potionCategoryType != cabinetController.potionCategoryType)
		{
			shelfSpace.shelfSpacePlacementType = ShelfSpacePlacementType.WrongCatagory;
		}
		else if (shelfSpace.potionController.sortingPuzzleType != shelfSortingPuzzleType)
		{
			shelfSpace.shelfSpacePlacementType = ShelfSpacePlacementType.WrongCatagory;
		}
		else if (shelfSpace.potionController.orderInSet != shelfSpace.potionController.currentOrderOnShelf)
		{
			shelfSpace.shelfSpacePlacementType = ShelfSpacePlacementType.WrongOrder;
		}
		else
		{
			shelfSpace.shelfSpacePlacementType = ShelfSpacePlacementType.Right;
		}
	}

	public void SetHighlight()
	{
		if (IsShelfCompletedRight())
		{
			MasterPool.Get(highlightPrefabType, base.transform.position, base.transform.rotation).GetComponent<ShelveHighlight>().SetShelveHighlight(isCompleted: true);
			return;
		}
		foreach (ShelfSpace shelfSpaces in shelfSpacesList)
		{
			if (shelfSpaces.potionController == null)
			{
				return;
			}
		}
		DisableHighlight();
		shelveHighlight = MasterPool.Get(highlightPrefabType, base.transform.position, base.transform.rotation).GetComponent<ShelveHighlight>();
		shelveHighlight.SetShelveHighlight(isCompleted: false);
	}

	public void DisableHighlight()
	{
		if (!(shelveHighlight == null))
		{
			shelveHighlight.DisableHighlight(isCompleted: false);
			shelveHighlight = null;
		}
	}
}
