using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class CharacterInventory : SceneSingleton<CharacterInventory>
{
	[Foldout("Scripts")]
	[SerializeField]
	private CharacterRaycast characterRaycast;

	private CharacterUI characterUI;

	private TutorialManager tutorialManager;

	[Space]
	[Foldout("Potinos")]
	[SerializeField]
	private Transform itemsParent;

	[SerializeField]
	private Vector3 baseItemsPos;

	[SerializeField]
	private float selectedItemYDelta = 0.05f;

	[SerializeField]
	private float xDeltaItemPos = 0.3f;

	[SerializeField]
	private float scaleMultipler = 3.5f;

	[Space]
	[ReadOnly]
	[SerializeField]
	private int selectedIndex;

	[ReadOnly]
	public List<ItemController> itemControllersList = new List<ItemController>();

	[SerializeField]
	public int invetorySize = 10;

	[Space]
	[Foldout("Cameras Layering")]
	[SerializeField]
	public int itemLayer = 6;

	[SerializeField]
	public int holdedItemLayer = 8;

	private QuickPuzzleSolutionPanelUI quickPuzzleSolutionPanelUI;

	private FirstPersonController firstPersonController;

	private CatsManager catManager;

	private PuzzlesManager puzzlesManager;

	private AudioManager audioManager;

	public void ChangeInventorySize(int newSize)
	{
		invetorySize = newSize;
		UpdateInventorySize();
	}

	private void Start()
	{
		audioManager = Singleton<AudioManager>.Instance;
		puzzlesManager = SceneSingleton<PuzzlesManager>.Instance;
		catManager = SceneSingleton<CatsManager>.Instance;
		firstPersonController = SceneSingleton<FirstPersonController>.Instance;
		characterUI = SceneSingleton<CharacterUI>.Instance;
		tutorialManager = SceneSingleton<TutorialManager>.Instance;
		quickPuzzleSolutionPanelUI = SceneSingleton<QuickPuzzleSolutionPanelUI>.Instance;
		UpdateInventorySize();
	}

	public bool IsInventoryEmpty()
	{
		return itemControllersList.Count == 0;
	}

	public bool IsInventoryFull()
	{
		return itemControllersList.Count == invetorySize;
	}

	public int GetEmptyInventorySpace()
	{
		return invetorySize - itemControllersList.Count;
	}

	public void InteractWithItem()
	{
		if (characterRaycast.currentObject == null)
		{
			return;
		}
		ItemController component2;
		PuzzleController component3;
		ShelfSpaceController component4;
		if (characterRaycast.currentObject.TryGetComponent<CatController>(out var component))
		{
			component.InteractWithCat();
		}
		else if (characterRaycast.currentObject.TryGetComponent<ItemController>(out component2))
		{
			if (itemControllersList.Count >= invetorySize)
			{
				return;
			}
			AddItemToInventory(component2);
			if (component2 is PotionController)
			{
				audioManager.PlayClip(AudioClipTypes.Potion_PickUp);
			}
			else
			{
				audioManager.PlayClip(AudioClipTypes.UI_TutorialComplete);
			}
		}
		else if (characterRaycast.currentObject.TryGetComponent<PuzzleController>(out component3))
		{
			if (component3.interactionSortingPuzzleType != SortingPuzzleType.None)
			{
				foreach (ItemController itemControllers in itemControllersList)
				{
					if (itemControllers is PotionController potionController && potionController.sortingPuzzleType == component3.interactionSortingPuzzleType)
					{
						potionController.InteractWithPuzzleControllerPuzzle();
					}
				}
				puzzlesManager.HighlightInteractionPuzzles(itemControllersList);
			}
			if (component3.CanInteractWithPuzzle())
			{
				component3.InteractWithPuzzle();
				return;
			}
			if (itemControllersList.Count == 0)
			{
				return;
			}
			ItemController itemController = itemControllersList[selectedIndex];
			if (itemController.itemType == ItemType.Potion || !component3.CanAddItem(itemController))
			{
				return;
			}
			RemoveItemToInventory(itemController);
			component3.AddItem(itemController);
		}
		else if (characterRaycast.currentObject.TryGetComponent<ShelfSpaceController>(out component4))
		{
			if (itemControllersList.Count == 0)
			{
				return;
			}
			ItemController itemController2 = itemControllersList[selectedIndex];
			if (itemController2.itemType != ItemType.Potion)
			{
				return;
			}
			RemoveItemToInventory(itemController2);
			component4.AddPotionController((PotionController)itemController2);
		}
		RefreshItemArt();
		UpdateInventorySize();
	}

	public void AddItemToInventory(ItemController itemController)
	{
		if (itemControllersList.Count == 0)
		{
			itemControllersList.Insert(0, itemController);
		}
		else
		{
			itemControllersList.Insert(selectedIndex + 1, itemController);
		}
		selectedIndex = itemControllersList.IndexOf(itemController);
		itemController.transform.parent = itemsParent;
		itemController.SetItemLayer(holdedItemLayer);
		itemController.PickItem();
		puzzlesManager.HighlightInteractionPuzzles(itemControllersList);
	}

	private void RemoveItemToInventory(ItemController itemController)
	{
		itemControllersList.Remove(itemController);
		if (selectedIndex >= itemControllersList.Count)
		{
			selectedIndex = itemControllersList.Count - 1;
		}
		if (itemControllersList.Count == 0)
		{
			selectedIndex = 0;
		}
		itemController.SetItemLayer(itemLayer);
		MasterPool.ReturnToPoolTransform(itemController.gameObject, itemController.prefabType, setActive: true);
		puzzlesManager.HighlightInteractionPuzzles(itemControllersList);
	}

	public void DropItem()
	{
		if (itemControllersList.Count != 0)
		{
			ItemController itemController = itemControllersList[selectedIndex];
			MasterPool.ReturnToPoolTransform(itemController.gameObject, itemController.prefabType, setActive: true);
			TweenController.KillTweens(itemController.gameObject);
			TweenController.ScaleV3(itemController.transform, itemController.transform.localScale.x, itemController.startingScale.x, TweenDuration.Medium);
			itemController.DropItem();
			itemController.SetItemLayer(itemLayer);
			itemControllersList.RemoveAt(selectedIndex);
			if (selectedIndex >= itemControllersList.Count)
			{
				selectedIndex = itemControllersList.Count - 1;
			}
			RefreshItemArt();
			UpdateInventorySize();
			puzzlesManager.HighlightInteractionPuzzles(itemControllersList);
		}
	}

	public void ShuffleItems(int direction)
	{
		if (itemControllersList.Count > 1)
		{
			tutorialManager.CompleteTutorialStep(TutorialStepType.Tutorial_Potion_Shuffle);
			selectedIndex += direction;
			if (selectedIndex < 0)
			{
				selectedIndex = itemControllersList.Count - 1;
			}
			else if (selectedIndex >= itemControllersList.Count)
			{
				selectedIndex = 0;
			}
			SceneSingleton<CatPanelUI>.Instance.UpdatePotionUI(IsInventoryEmpty(), IsHoldingPotion());
			RefreshItemArt();
		}
	}

	public void RefreshItemArt(bool isLoading = false)
	{
		if (itemControllersList.Count == 0)
		{
			SceneSingleton<HoveredPotionController>.Instance.UpdateZoomedPotion();
			quickPuzzleSolutionPanelUI.UpdateQuickPuzzleSolution();
			return;
		}
		int count = itemControllersList.Count;
		float num = (0f - (float)(count - 1) * xDeltaItemPos) * 0.5f;
		for (int i = 0; i < count; i++)
		{
			Vector3 vector = baseItemsPos;
			vector.x = num + (float)i * xDeltaItemPos;
			if (selectedIndex == i)
			{
				vector += new Vector3(0f, selectedItemYDelta, 0f);
			}
			if (isLoading)
			{
				TweenController.KillTweens(itemControllersList[i].gameObject);
				itemControllersList[i].transform.localPosition = vector;
				itemControllersList[i].transform.localEulerAngles = itemControllersList[i].pickupPotionRotationAngle;
				itemControllersList[i].transform.localScale = itemControllersList[i].startingScale / scaleMultipler;
			}
			else
			{
				itemControllersList[i].MoveItem(vector, itemControllersList[i].pickupPotionRotationAngle, itemControllersList[i].startingScale / scaleMultipler);
			}
		}
		quickPuzzleSolutionPanelUI.UpdateQuickPuzzleSolution();
		SceneSingleton<HoveredPotionController>.Instance.UpdateZoomedPotion();
	}

	public void UpdateInventorySize()
	{
		characterUI.UpdateInventory(itemControllersList.Count, invetorySize);
	}

	public PotionController GetSelectedPotion()
	{
		return (PotionController)itemControllersList[selectedIndex];
	}

	public ItemController GetSelectedItem()
	{
		return itemControllersList[selectedIndex];
	}

	public bool IsHoldingPotion()
	{
		if (itemControllersList.Count == 0)
		{
			return false;
		}
		if (selectedIndex >= itemControllersList.Count)
		{
			return false;
		}
		if (itemControllersList[selectedIndex].itemType != ItemType.Potion)
		{
			return false;
		}
		return true;
	}

	public bool IsHoldingAnyPotionsInInventory()
	{
		if (itemControllersList.Count == 0)
		{
			return false;
		}
		foreach (ItemController itemControllers in itemControllersList)
		{
			if (itemControllers is PotionController)
			{
				return true;
			}
		}
		return false;
	}
}
