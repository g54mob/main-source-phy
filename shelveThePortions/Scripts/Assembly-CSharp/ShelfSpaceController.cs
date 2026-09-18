using UnityEngine;
using VInspector;

public class ShelfSpaceController : MonoBehaviour, IHighlight
{
	[ReadOnly]
	public ShelfController shelfController;

	[ReadOnly]
	public int spaceOrderIndex;

	[Space]
	private PotionGhostController potionGhostController;

	[Space]
	[ReadOnly]
	public ShelfSpacePlacementType shelfSpacePlacementType;

	public void SetShelfSpace(ShelfController shelf, int index)
	{
		shelfController = shelf;
		spaceOrderIndex = index;
		shelfSpacePlacementType = ShelfSpacePlacementType.None;
		SetHighlight(flag: false);
	}

	public void SetHintHighlight(bool flag)
	{
	}

	public void SetHighlight(bool flag)
	{
		if (flag)
		{
			if (SceneSingleton<CharacterInventory>.Instance.IsHoldingPotion())
			{
				if (potionGhostController == null)
				{
					GameObject gameObject = MasterPool.Get(PrefabTypes.Bottle_Ghost);
					gameObject.transform.position = base.transform.position;
					potionGhostController = gameObject.GetComponent<PotionGhostController>();
				}
				potionGhostController.GhostPotion(SceneSingleton<CharacterInventory>.Instance.GetSelectedPotion(), this);
			}
		}
		else if (potionGhostController != null)
		{
			MasterPool.ReturnToPoolTransform(potionGhostController.gameObject, PrefabTypes.Bottle_Ghost);
			potionGhostController = null;
		}
	}

	public void AddPotionController(PotionController potionController)
	{
		potionController.ShelfPotion(shelfController, spaceOrderIndex);
		shelfController.AddPotion(potionController, spaceOrderIndex, isLoading: false);
		potionController.transform.eulerAngles = new Vector3(0f, potionController.transform.eulerAngles.y, 0f);
		potionController.MoveItem(base.transform.position, new Vector3(0f, base.transform.eulerAngles.y + 180f + potionController.pickupPotionRotationAngle.y, 0f), potionController.startingScale, delegate
		{
			potionController.CheckWhenReachShlef();
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Potion_Shelve);
		});
	}
}
