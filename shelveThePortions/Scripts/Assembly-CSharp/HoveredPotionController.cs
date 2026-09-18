using UnityEngine;

public class HoveredPotionController : SceneSingleton<HoveredPotionController>
{
	private PotionController zoomedPotionController;

	[SerializeField]
	private Transform zoomedPotionParent;

	[SerializeField]
	private Transform zoomedPotionParentItemsInPotion;

	[Space(3f)]
	[SerializeField]
	private Transform zoomedCatPotionParent;

	[SerializeField]
	private Transform zoomedCatPotionParentItemsInPotion;

	private bool isCameraZoomed;

	private bool isCatPotionOn;

	private CharacterInventory characterInventory;

	private void Start()
	{
		characterInventory = SceneSingleton<CharacterInventory>.Instance;
	}

	public void SetSetIsCameraZoomed(bool flag)
	{
		isCameraZoomed = flag;
		UpdateZoomedPotion();
	}

	public void SetIsCatPotionOn(bool flag)
	{
		isCatPotionOn = flag;
		UpdateZoomedPotion();
	}

	private void DisableZoomedPotion()
	{
		if (zoomedPotionController != null)
		{
			MasterPool.ReturnToPoolTransform(zoomedPotionController.gameObject, zoomedPotionController.prefabType);
			zoomedPotionController.GetComponent<Collider>().enabled = true;
			zoomedPotionController = null;
		}
	}

	public void UpdateZoomedPotion()
	{
		DisableZoomedPotion();
		if (isCameraZoomed || isCatPotionOn)
		{
			if (characterInventory.IsHoldingPotion())
			{
				PotionController selectedPotion = characterInventory.GetSelectedPotion();
				zoomedPotionController = MasterPool.Get(selectedPotion.prefabType, GetParent(selectedPotion)).GetComponent<PotionController>();
				zoomedPotionController.transform.localPosition = Vector3.zero;
				zoomedPotionController.transform.localEulerAngles = zoomedPotionController.pickupPotionRotationAngle;
				zoomedPotionController.CopyPotion(selectedPotion);
				zoomedPotionController.GetComponent<Collider>().enabled = false;
				zoomedPotionController.SetItemLayer(characterInventory.holdedItemLayer);
			}
			else
			{
				DisableZoomedPotion();
			}
		}
	}

	private Transform GetParent(PotionController potionController)
	{
		if (isCatPotionOn)
		{
			if (potionController.sortingPuzzleType == SortingPuzzleType.ItemsInPotion_8)
			{
				return zoomedCatPotionParentItemsInPotion;
			}
			return zoomedCatPotionParent;
		}
		if (potionController.sortingPuzzleType == SortingPuzzleType.ItemsInPotion_8)
		{
			return zoomedPotionParentItemsInPotion;
		}
		return zoomedPotionParent;
	}
}
