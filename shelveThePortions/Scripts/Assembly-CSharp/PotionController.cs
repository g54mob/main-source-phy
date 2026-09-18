using UnityEngine;
using VInspector;

[SelectionBase]
public class PotionController : ItemController, IOptimizable
{
	[Space(5f)]
	[Foldout("Potion Components")]
	[Space]
	public MeshFilter bodyMeshFilter;

	public MeshFilter corkMeshFilter;

	public MeshFilter liquidMeshFilter;

	public MeshRenderer liquedMeshRenderer;

	[EndFoldout]
	[Space(5f)]
	[Foldout("Puzzles Components")]
	[Space]
	public GameObject potionLiquid;

	public GameObject potionCork;

	[ReadOnly]
	public Sprite puzzleSprite;

	[ReadOnly]
	private bool isPuzzleColorBlind;

	[ReadOnly]
	[SerializeField]
	private SpriteRenderer puzzleSpriteRenderer;

	[ReadOnly]
	public bool needsInteractionToShowPuzzleSprite;

	[ReadOnly]
	public bool isInteractedWith;

	[ReadOnly]
	public Sprite interactPuzzleSprite;

	[ReadOnly]
	[SerializeField]
	private SpriteRenderer interactPuzzleSpriteRenderer;

	[ReadOnly]
	public ItemsInPotionPuzzleController itemsInPotionPuzzleController;

	[ReadOnly]
	public ParticlePuzzleController particlePuzzleController;

	[EndFoldout]
	[Space(10f)]
	[Foldout("Potion Data")]
	[ReadOnly]
	public PotionCategoryType potionCategoryType;

	public SortingPuzzleType sortingPuzzleType;

	[ReadOnly]
	public int orderInSet;

	[Space]
	[ReadOnly]
	public ShelfController shelfController;

	[ReadOnly]
	public int currentOrderOnShelf;

	[SerializeField]
	private PrefabTypes glowPrefabType;

	[EndFoldout]
	[Space(5f)]
	[Foldout("Potion Sprite Puzzle Sticker Data")]
	[SerializeField]
	public Vector3 stickerPostion;

	[SerializeField]
	public Vector3 stickerRotation;

	[SerializeField]
	public Vector3 stickerScale;

	[Space(5f)]
	[Foldout("Potion Sprite Tiny Puzzle Sticker Data")]
	[SerializeField]
	public Vector3 tinyStickerPostion;

	[SerializeField]
	public Vector3 tinyStickerRotation;

	[SerializeField]
	public Vector3 tinyStickerScale;

	private bool disableOptimization;

	[Space(10f)]
	[Header("Glow")]
	[SerializeField]
	private Color greenGlowColor;

	[SerializeField]
	private Color redGlowColor;

	private static MaterialPropertyBlock mpb;

	[Space(20f)]
	[SerializeField]
	private GameObject debugSticker;

	[EndFoldout]
	public void SetPotion(PotionCategory potionCategory, PotionSet potionSet, int orderInSet)
	{
		potionCategoryType = potionCategory.potionCategoryType;
		SetColor(potionCategory.color);
		sortingPuzzleType = potionSet.sortingPuzzleType;
		isPuzzleColorBlind = Singleton<PotionsDataHolder>.Instance.IsSortingPuzzleColorBlind(sortingPuzzleType);
		prefabType = potionSet.bottlePrefabType;
		this.orderInSet = orderInSet;
		SetPotionPuzzle();
	}

	public override void LoadItem(ItemSaveData itemSaveData)
	{
		base.LoadItem(itemSaveData);
		if (itemSaveData is PotionSaveData potionSaveData)
		{
			potionCategoryType = potionSaveData.potionCategoryType;
			prefabType = potionSaveData.itemPrefabType;
			sortingPuzzleType = potionSaveData.sortingPuzzleType;
			isPuzzleColorBlind = Singleton<PotionsDataHolder>.Instance.IsSortingPuzzleColorBlind(sortingPuzzleType);
			orderInSet = potionSaveData.correctOrderInSet;
			SetColor(Singleton<PotionsDataHolder>.Instance.GetCatagoryColor(potionCategoryType));
			if (potionSaveData.cabinetID != -1)
			{
				PutPotionOntoShelf(potionSaveData.cabinetID, potionSaveData.shelfID, potionSaveData.currentOrderOnShelf);
			}
			SetPotionPuzzle();
			isInteractedWith = potionSaveData.isInteractedWith;
			if (itemSaveData.isInInventory)
			{
				SetOptimizationSetting(flag: true);
				SetItemLayer(SceneSingleton<CharacterInventory>.Instance.holdedItemLayer);
			}
		}
	}

	protected override bool CanShowHighlithIndicator()
	{
		if (shelfController != null)
		{
			return false;
		}
		return base.CanShowHighlithIndicator();
	}

	public void PutPotionOntoShelf(int cabinetID, int shelfID, int currentOrderOnShelf)
	{
		isPickedUp = false;
		CabinetController cabinet = SceneSingleton<CabinetsManager>.Instance.GetCabinet(cabinetID);
		if (!(cabinet == null))
		{
			ShelfController shelfController = (this.shelfController = cabinet.GetShelf(shelfID));
			this.currentOrderOnShelf = currentOrderOnShelf;
			shelfController.AddPotion(this, currentOrderOnShelf, isLoading: true);
			col.enabled = true;
			rbody.isKinematic = true;
			SetOptimizationSetting(flag: false);
		}
	}

	public void CopyPotion(PotionController potionController)
	{
		prefabType = potionController.prefabType;
		base.transform.localScale = potionController.startingScale;
		bodyMeshFilter.mesh = potionController.bodyMeshFilter.mesh;
		corkMeshFilter.mesh = potionController.corkMeshFilter.mesh;
		liquidMeshFilter.mesh = potionController.liquidMeshFilter.mesh;
		liquedMeshRenderer.material.color = potionController.liquedMeshRenderer.material.color;
		potionCork.transform.localPosition = potionController.potionCork.transform.localPosition;
		potionCork.transform.localScale = potionController.potionCork.transform.localScale;
		sortingPuzzleType = potionController.sortingPuzzleType;
		isPuzzleColorBlind = Singleton<PotionsDataHolder>.Instance.IsSortingPuzzleColorBlind(sortingPuzzleType);
		potionCategoryType = potionController.potionCategoryType;
		orderInSet = potionController.orderInSet;
		if (itemsInPotionPuzzleController != null)
		{
			Object.Destroy(itemsInPotionPuzzleController.gameObject);
			itemsInPotionPuzzleController = null;
		}
		if (particlePuzzleController != null)
		{
			Object.Destroy(particlePuzzleController.gameObject);
			particlePuzzleController = null;
		}
		SetColor(Singleton<PotionsDataHolder>.Instance.GetCatagoryColor(potionCategoryType));
		SetColorBlind(SaveSystem.GetIsColorBlindSetting());
		disableOptimization = false;
		SetOptimizationSetting(flag: false);
		SetPotionPuzzle();
		needsInteractionToShowPuzzleSprite = potionController.needsInteractionToShowPuzzleSprite;
		isInteractedWith = potionController.isInteractedWith;
		SetOptimizationSetting(flag: true);
		disableOptimization = true;
	}

	private void SetPotionPuzzle()
	{
		puzzleSprite = null;
		interactPuzzleSprite = null;
		puzzleSpriteRenderer = null;
		needsInteractionToShowPuzzleSprite = false;
		isInteractedWith = false;
		Singleton<PotionsDataHolder>.Instance.SetPotionPuzzle(this);
	}

	public void ShelfPotion(ShelfController shelfController, int currentOrderOnShelf)
	{
		isPickedUp = false;
		UpdateOutline();
		this.shelfController = shelfController;
		this.currentOrderOnShelf = currentOrderOnShelf;
		SceneSingleton<TutorialManager>.Instance.CompleteTutorialStep(TutorialStepType.Tutorial_Potion_Shelf);
		col.enabled = false;
		rbody.isKinematic = true;
		SetOptimizationSetting(flag: true);
	}

	public override void PickItem()
	{
		if (shelfController != null)
		{
			ShelfController obj = shelfController;
			shelfController = null;
			obj.RemovePotion(currentOrderOnShelf, this);
		}
		base.PickItem();
		SetOptimizationSetting(flag: true);
	}

	public void InteractWithPuzzleControllerPuzzle()
	{
		if (!isInteractedWith)
		{
			isInteractedWith = true;
			GameObject obj = MasterPool.Get(PrefabTypes.PotionInteractionParticle, base.transform, Vector3.zero);
			obj.transform.localScale = Vector3.one * 0.75f;
			obj.transform.localEulerAngles = Vector3.zero;
			switch (sortingPuzzleType)
			{
			case SortingPuzzleType.CrowInteraction_5:
				SceneSingleton<TutorialManager>.Instance.CompleteTutorialStep(TutorialStepType.Tutorial_Interact_Crow);
				Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Puzzle_Interaction_Crow);
				break;
			case SortingPuzzleType.LightInteraction_8:
				Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Puzzle_Interaction_Candle);
				break;
			case SortingPuzzleType.WeightInteraction_8:
				Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Puzzle_Interaction_Balance);
				break;
			}
			SetOptimizationSetting(flag: true);
			SetItemLayer(currentLayer);
		}
	}

	public void SetOptimizationSetting(bool flag)
	{
		if (disableOptimization)
		{
			return;
		}
		if (flag)
		{
			if (interactPuzzleSprite != null && interactPuzzleSpriteRenderer == null)
			{
				interactPuzzleSpriteRenderer = MasterPool.Get(PrefabTypes.PuzzleStickerSR, base.transform).GetComponent<SpriteRenderer>();
				interactPuzzleSpriteRenderer.transform.localPosition = tinyStickerPostion;
				interactPuzzleSpriteRenderer.transform.localEulerAngles = tinyStickerRotation;
				interactPuzzleSpriteRenderer.transform.localScale = tinyStickerScale;
				interactPuzzleSpriteRenderer.sprite = interactPuzzleSprite;
			}
			if (puzzleSprite != null)
			{
				bool flag2 = true;
				if (needsInteractionToShowPuzzleSprite)
				{
					flag2 = isInteractedWith;
				}
				if (puzzleSpriteRenderer == null && flag2)
				{
					puzzleSpriteRenderer = MasterPool.Get(PrefabTypes.PuzzleStickerSR, base.transform).GetComponent<SpriteRenderer>();
					puzzleSpriteRenderer.transform.localPosition = stickerPostion;
					puzzleSpriteRenderer.transform.localEulerAngles = stickerRotation;
					puzzleSpriteRenderer.transform.localScale = stickerScale;
					puzzleSpriteRenderer.sprite = puzzleSprite;
				}
			}
			if (itemsInPotionPuzzleController != null)
			{
				itemsInPotionPuzzleController.gameObject.SetActive(value: true);
			}
			if (particlePuzzleController != null)
			{
				particlePuzzleController.gameObject.SetActive(value: true);
			}
			SetItemLayer(currentLayer);
		}
		else
		{
			if (interactPuzzleSpriteRenderer != null)
			{
				MasterPool.ReturnToPoolTransform(interactPuzzleSpriteRenderer.gameObject, PrefabTypes.PuzzleStickerSR);
				interactPuzzleSpriteRenderer = null;
			}
			if (puzzleSpriteRenderer != null)
			{
				MasterPool.ReturnToPoolTransform(puzzleSpriteRenderer.gameObject, PrefabTypes.PuzzleStickerSR);
				puzzleSpriteRenderer = null;
			}
			if (itemsInPotionPuzzleController != null)
			{
				itemsInPotionPuzzleController.gameObject.SetActive(value: false);
			}
			if (particlePuzzleController != null)
			{
				particlePuzzleController.gameObject.SetActive(value: false);
			}
		}
	}

	public override void SetItemLayer(int layer)
	{
		base.SetItemLayer(layer);
		if (itemsInPotionPuzzleController != null)
		{
			itemsInPotionPuzzleController.SetControllerLayer(layer);
		}
		if (particlePuzzleController != null)
		{
			particlePuzzleController.SetControllerLayer(layer);
		}
	}

	public override void OnMoveComplete()
	{
		base.OnMoveComplete();
		if (shelfController != null)
		{
			col.enabled = true;
		}
	}

	public void ShowCompleteGlow(bool isComplete)
	{
		ShowGlow(isComplete ? greenGlowColor : redGlowColor);
	}

	public void ShowGreenGlow()
	{
		ShowGlow(greenGlowColor);
	}

	public void CheckWhenReachShlef()
	{
		if (!(shelfController == null) && shelfController.cabinetController.potionCategoryType != potionCategoryType)
		{
			ShowGlow(redGlowColor);
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Potion_Wrong);
		}
	}

	public void ShowGlow(Color color)
	{
		GameObject obj = MasterPool.Get(glowPrefabType, base.transform.position, base.transform.rotation);
		obj.transform.localScale = base.transform.localScale * 1f;
		obj.GetComponent<GlowController>().SetGlow(color);
	}

	public void SetColorBlind(bool flag)
	{
		if (mpb == null)
		{
			mpb = new MaterialPropertyBlock();
		}
		liquedMeshRenderer.GetPropertyBlock(mpb);
		if (flag)
		{
			mpb.SetTexture("_PatternTex", Singleton<PotionsDataHolder>.Instance.GetCatagoryColorBlindSprite(potionCategoryType));
			mpb.SetColor("_PatternColor", Singleton<PotionsDataHolder>.Instance.GetCatagoryColorBlindSpriteColor(potionCategoryType));
			mpb.SetFloat("_PatternOpacity", Singleton<PotionsDataHolder>.Instance.GetCatagoryColorBlindSpriteOpacity(potionCategoryType));
			PotionPrefabColorblindData potionPrefabColorblindData = Singleton<PotionsDataHolder>.Instance.GetPotionPrefabColorblindData(prefabType);
			mpb.SetFloat("_PatternScale", potionPrefabColorblindData.patternCount);
			mpb.SetFloat("_PatternSpacing", potionPrefabColorblindData.patternSpacing);
			mpb.SetVector("_PatternOffset", potionPrefabColorblindData.offset);
			if (isPuzzleColorBlind)
			{
				puzzleSprite = Singleton<PotionsDataHolder>.Instance.GetPotionPuzzleColorBlindSprite(sortingPuzzleType, orderInSet);
			}
		}
		else
		{
			mpb.SetFloat("_PatternOpacity", 0f);
			if (isPuzzleColorBlind)
			{
				puzzleSprite = Singleton<PotionsDataHolder>.Instance.GetPotionPuzzleSprite(sortingPuzzleType, orderInSet);
			}
		}
		liquedMeshRenderer.SetPropertyBlock(mpb);
		if (puzzleSpriteRenderer != null)
		{
			puzzleSpriteRenderer.sprite = puzzleSprite;
		}
	}

	public void SetColor(Color color)
	{
		if (mpb == null)
		{
			mpb = new MaterialPropertyBlock();
		}
		liquedMeshRenderer.GetPropertyBlock(mpb);
		mpb.SetColor("_BaseColor", color);
		liquedMeshRenderer.SetPropertyBlock(mpb);
	}

	[Button]
	public void SetDebugSticker()
	{
		debugSticker.transform.localPosition = stickerPostion;
		debugSticker.transform.localEulerAngles = stickerRotation;
		debugSticker.transform.localScale = stickerScale;
	}
}
