using System.Collections.Generic;
using UnityEngine;

public class PotionsDataHolder : Singleton<PotionsDataHolder>
{
	public List<PotionCategory> tutorialPotionCategorysList = new List<PotionCategory>();

	public List<PotionCategory> potionCategorysList = new List<PotionCategory>();

	public List<PotionPrefabColorblindData> potionPrefabColorblindDatasList = new List<PotionPrefabColorblindData>();

	[Space]
	[SerializeField]
	private List<PuzzlesPotionsData> puzzlesPotionsDataList = new List<PuzzlesPotionsData>();

	private Dictionary<SortingPuzzleType, List<Sprite>> potionPuzzlesSpritesDict = new Dictionary<SortingPuzzleType, List<Sprite>>();

	private Dictionary<SortingPuzzleType, int> puzzlesBottleShapeIndexDict = new Dictionary<SortingPuzzleType, int>();

	[Space(10f)]
	[Header("Puzzles Data")]
	[Space(5f)]
	public List<SortingPuzzleType> puzzlesUIList = new List<SortingPuzzleType>();

	public List<SortingPuzzleType> solvedPuzzlesList = new List<SortingPuzzleType>();

	[Space]
	[SerializeField]
	private List<PotionPuzzlesSprites> potionPuzzlesSpritesList = new List<PotionPuzzlesSprites>();

	[SerializeField]
	private List<PotionPuzzlesSprites> colorBlindPotionPuzzlesSpritesList = new List<PotionPuzzlesSprites>();

	[SerializeField]
	private List<float> potionSizes = new List<float>();

	[SerializeField]
	private List<float> potionLiquedSize = new List<float>();

	[SerializeField]
	private List<float> potionCorkSize = new List<float>();

	[Space(10f)]
	[Header("Interaction Puzzles Data")]
	[Space(5f)]
	[SerializeField]
	private List<Sprite> interactPuzzleSpritesList = new List<Sprite>();

	[SerializeField]
	private Sprite interactLightPuzzleSprite;

	[SerializeField]
	private Sprite interactMusicPuzzleSprite;

	[SerializeField]
	private Sprite interactWeightPuzzleSprite;

	[SerializeField]
	private Sprite interactCrowPuzzleSprite;

	[Space]
	[SerializeField]
	private GameObject ItemsInPotionPuzzleControllerPrefab;

	[SerializeField]
	private List<PotionPuzzleItemData> itemsInPotionPuzzleDatasList = new List<PotionPuzzleItemData>();

	[Space(5f)]
	[SerializeField]
	private GameObject ParticlePuzzleControllerObject;

	[SerializeField]
	private List<PotionParticlesPuzzleData> potionParticlesPuzzleDatasList = new List<PotionParticlesPuzzleData>();

	[Space(20f)]
	[SerializeField]
	private List<PotionCategory> _fullpotionCategorysList = new List<PotionCategory>();

	[Space(10f)]
	[Header("Debug")]
	[Space(10f)]
	[SerializeField]
	private bool disablePuzzles;

	protected override void Awake()
	{
		base.Awake();
		foreach (PotionPuzzlesSprites potionPuzzlesSprites in potionPuzzlesSpritesList)
		{
			potionPuzzlesSpritesDict.Add(potionPuzzlesSprites.sortingPuzzleType, potionPuzzlesSprites.puzzleSpritesList);
		}
	}

	public int GetPuzzleBottlesInSet(SortingPuzzleType sortingPuzzleType)
	{
		foreach (PuzzlesPotionsData puzzlesPotionsData in puzzlesPotionsDataList)
		{
			if (puzzlesPotionsData.sortingPuzzleType == sortingPuzzleType)
			{
				return puzzlesPotionsData.bottlesInSet;
			}
		}
		Debug.LogError(sortingPuzzleType.ToString() + " sortingPuzzleType is not aviliable in dataHolder");
		return 0;
	}

	public PrefabTypes GetPuzzleBottlesPrefabType(SortingPuzzleType sortingPuzzleType)
	{
		foreach (PuzzlesPotionsData puzzlesPotionsData in puzzlesPotionsDataList)
		{
			if (puzzlesPotionsData.sortingPuzzleType == sortingPuzzleType)
			{
				if (!puzzlesBottleShapeIndexDict.ContainsKey(sortingPuzzleType))
				{
					puzzlesBottleShapeIndexDict.Add(sortingPuzzleType, -1);
					puzzlesPotionsData.potionsPrefabTypes = Randomizer.Randomize(puzzlesPotionsData.potionsPrefabTypes);
				}
				puzzlesBottleShapeIndexDict[sortingPuzzleType]++;
				if (puzzlesBottleShapeIndexDict[sortingPuzzleType] >= puzzlesPotionsData.potionsPrefabTypes.Count)
				{
					puzzlesBottleShapeIndexDict[sortingPuzzleType] = 0;
				}
				return puzzlesPotionsData.potionsPrefabTypes[puzzlesBottleShapeIndexDict[sortingPuzzleType]];
			}
		}
		Debug.LogError(sortingPuzzleType.ToString() + " sortingPuzzleType is not aviliable in dataHolder");
		return PrefabTypes.Bottle_A;
	}

	public PotionPrefabColorblindData GetPotionPrefabColorblindData(PrefabTypes prefabType)
	{
		foreach (PotionPrefabColorblindData potionPrefabColorblindDatas in potionPrefabColorblindDatasList)
		{
			if (potionPrefabColorblindDatas.bottlePrefabType == prefabType)
			{
				return potionPrefabColorblindDatas;
			}
		}
		Debug.LogError(prefabType.ToString() + " GetPotionPrefabColorblindData is not aviliable in PotionPrefabColorblindDatasList");
		return null;
	}

	public Color GetCatagoryColor(PotionCategoryType potionCategoryType)
	{
		foreach (PotionCategory potionCategorys in potionCategorysList)
		{
			if (potionCategorys.potionCategoryType == potionCategoryType)
			{
				return potionCategorys.color;
			}
		}
		Debug.LogError(potionCategoryType.ToString() + " potionCategoryType is not aviliable in dataHolder");
		return Color.white;
	}

	public Texture2D GetCatagoryColorBlindSprite(PotionCategoryType potionCategoryType)
	{
		foreach (PotionCategory potionCategorys in potionCategorysList)
		{
			if (potionCategorys.potionCategoryType == potionCategoryType)
			{
				return potionCategorys.colorBlindSprite;
			}
		}
		Debug.LogError(potionCategoryType.ToString() + " potionCategoryType is not aviliable in dataHolder");
		return null;
	}

	public Color GetCatagoryColorBlindSpriteColor(PotionCategoryType potionCategoryType)
	{
		foreach (PotionCategory potionCategorys in potionCategorysList)
		{
			if (potionCategorys.potionCategoryType == potionCategoryType)
			{
				return potionCategorys.colorBlindSpriteColor;
			}
		}
		Debug.LogError(potionCategoryType.ToString() + " potionCategoryType is not aviliable in dataHolder");
		return Color.white;
	}

	public float GetCatagoryColorBlindSpriteOpacity(PotionCategoryType potionCategoryType)
	{
		foreach (PotionCategory potionCategorys in potionCategorysList)
		{
			if (potionCategorys.potionCategoryType == potionCategoryType)
			{
				return potionCategorys.colorBlindSpriteOpacity;
			}
		}
		Debug.LogError(potionCategoryType.ToString() + " potionCategoryType is not aviliable in dataHolder");
		return 0.2f;
	}

	public void SetPotionPuzzle(PotionController potionController)
	{
		switch (potionController.sortingPuzzleType)
		{
		case SortingPuzzleType.PotionSize_5:
			SetPotionSizePuzzle(potionController);
			break;
		case SortingPuzzleType.PotionLiquidSize_5:
			SetPotionLiquedSizePuzzle(potionController);
			break;
		case SortingPuzzleType.PotionCorkSize_5:
			SetPotionCorkSizePuzzle(potionController);
			break;
		case SortingPuzzleType.ItemsInPotion_8:
			SetItemsInPotionPuzzle(potionController);
			break;
		case SortingPuzzleType.PotionParticles_8:
			SetPotionParticlesPuzzle(potionController);
			break;
		case SortingPuzzleType.BasicNumbers_5:
		case SortingPuzzleType.Animals_5:
		case SortingPuzzleType.Flowers_8:
		case SortingPuzzleType.RomanNumerals_8:
		case SortingPuzzleType.MoonsShapes_8:
		case SortingPuzzleType.Compass_8:
		case SortingPuzzleType.Clock_8:
		case SortingPuzzleType.GeometricShapes_8:
		case SortingPuzzleType.Chess_8:
		case SortingPuzzleType.MusicNotes_8:
		case SortingPuzzleType.ChemicalGrades_8:
		case SortingPuzzleType.IngredientPrice_8:
		case SortingPuzzleType.Cards_8:
		case SortingPuzzleType.Weather_8:
		case SortingPuzzleType.Elements_8:
		case SortingPuzzleType.GibberishLanguage_8:
		case SortingPuzzleType.Rainbow_8:
		case SortingPuzzleType.HeroAdventure_8:
		case SortingPuzzleType.Spices_5:
			SetPotionPuzzleSprite(potionController);
			break;
		case SortingPuzzleType.LightInteraction_8:
		case SortingPuzzleType.MusicInteraction_8:
		case SortingPuzzleType.WeightInteraction_8:
		case SortingPuzzleType.CrowInteraction_5:
			SetPotionInteractPuzzle(potionController);
			break;
		}
	}

	public void AddSolvedPuzzle(SortingPuzzleType sortingPuzzleType)
	{
		if (!solvedPuzzlesList.Contains(sortingPuzzleType))
		{
			solvedPuzzlesList.Add(sortingPuzzleType);
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Puzzle_Completed);
			if (SceneSingleton<SolvedPuzzlesPanelUI>.Instance != null)
			{
				SceneSingleton<SolvedPuzzlesPanelUI>.Instance.SolvePuzzle(sortingPuzzleType);
			}
			if (!SceneSingleton<GameManager>.Instance.isLoadingGame && SceneSingleton<PuzzleUnlockPanelUI>.Instance != null)
			{
				SceneSingleton<PuzzleUnlockPanelUI>.Instance.SetPuzzle(sortingPuzzleType);
			}
			UpdateAchievements();
		}
	}

	public void UpdateColorBlind()
	{
	}

	private void UpdateAchievements()
	{
		int count = solvedPuzzlesList.Count;
		if (count >= 1)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Puzzle_1);
		}
		if (count >= 5)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Puzzle_5);
		}
		if (count >= 10)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Puzzle_10);
		}
		if (count >= puzzlesUIList.Count)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Puzzle_All);
		}
	}

	public bool IsSortingPuzzleColorBlind(SortingPuzzleType sortingPuzzleType)
	{
		foreach (PotionPuzzlesSprites colorBlindPotionPuzzlesSprites in colorBlindPotionPuzzlesSpritesList)
		{
			if (colorBlindPotionPuzzlesSprites.sortingPuzzleType == sortingPuzzleType)
			{
				return true;
			}
		}
		return false;
	}

	public Sprite GetPotionPuzzleColorBlindSprite(SortingPuzzleType sortingPuzzleType, int index)
	{
		foreach (PotionPuzzlesSprites colorBlindPotionPuzzlesSprites in colorBlindPotionPuzzlesSpritesList)
		{
			if (colorBlindPotionPuzzlesSprites.sortingPuzzleType == sortingPuzzleType)
			{
				if (index >= colorBlindPotionPuzzlesSprites.puzzleSpritesList.Count)
				{
					Debug.LogError("color blind puzzle has less data than number of potions is set");
					return null;
				}
				return colorBlindPotionPuzzlesSprites.puzzleSpritesList[index];
			}
		}
		Debug.LogError("puzzle has no color blind Sprites");
		return null;
	}

	private void SetPotionSizePuzzle(PotionController potionController)
	{
		if (potionController.orderInSet >= potionSizes.Count)
		{
			Debug.LogError("PotionSize puzzle has less data than number of potions is set");
			return;
		}
		potionController.transform.localScale = Vector3.one * potionSizes[potionController.orderInSet];
		potionController.startingScale = potionController.transform.localScale;
	}

	private void SetPotionLiquedSizePuzzle(PotionController potionController)
	{
		if (potionController.orderInSet >= potionLiquedSize.Count)
		{
			Debug.LogError("PotionLiquedSize puzzle has less data than number of potions is set");
		}
		else
		{
			potionController.potionLiquid.transform.localScale = new Vector3(1f, potionLiquedSize[potionController.orderInSet], 1f);
		}
	}

	private void SetPotionCorkSizePuzzle(PotionController potionController)
	{
		if (potionController.orderInSet >= potionCorkSize.Count)
		{
			Debug.LogError("PotionCorkSize puzzle has less data than number of potions is set");
		}
		else
		{
			potionController.potionCork.transform.localScale = Vector3.one * potionCorkSize[potionController.orderInSet];
		}
	}

	private void SetPotionPuzzleSprite(PotionController potionController)
	{
		if (potionController.orderInSet >= potionPuzzlesSpritesDict[potionController.sortingPuzzleType].Count)
		{
			Debug.LogError("puzzle has less data than number of potions is set", potionController.gameObject);
		}
		else
		{
			potionController.puzzleSprite = potionPuzzlesSpritesDict[potionController.sortingPuzzleType][potionController.orderInSet];
		}
	}

	public Sprite GetPotionPuzzleSprite(SortingPuzzleType sortingPuzzleType, int index)
	{
		if (index >= potionPuzzlesSpritesDict[sortingPuzzleType].Count)
		{
			Debug.LogError("puzzle has less data than number of potions is set");
			return null;
		}
		return potionPuzzlesSpritesDict[sortingPuzzleType][index];
	}

	public int GetPotionPuzzleSpriteCount(SortingPuzzleType sortingPuzzleType)
	{
		if (!potionPuzzlesSpritesDict.ContainsKey(sortingPuzzleType))
		{
			Debug.LogError("potionPuzzlesSpritesDict doesn't contain " + sortingPuzzleType);
			return 0;
		}
		return potionPuzzlesSpritesDict[sortingPuzzleType].Count;
	}

	private void SetPotionInteractPuzzle(PotionController potionController)
	{
		switch (potionController.sortingPuzzleType)
		{
		case SortingPuzzleType.LightInteraction_8:
			potionController.interactPuzzleSprite = interactLightPuzzleSprite;
			break;
		case SortingPuzzleType.MusicInteraction_8:
			potionController.interactPuzzleSprite = interactMusicPuzzleSprite;
			break;
		case SortingPuzzleType.WeightInteraction_8:
			potionController.interactPuzzleSprite = interactWeightPuzzleSprite;
			break;
		case SortingPuzzleType.CrowInteraction_5:
			potionController.interactPuzzleSprite = interactCrowPuzzleSprite;
			break;
		}
		potionController.puzzleSprite = interactPuzzleSpritesList[potionController.orderInSet];
		potionController.needsInteractionToShowPuzzleSprite = true;
		potionController.isInteractedWith = false;
	}

	private void SetItemsInPotionPuzzle(PotionController potionController)
	{
		potionController.potionLiquid.transform.localScale = new Vector3(1f, 0.75f, 1f);
		foreach (PotionPuzzleItemData itemsInPotionPuzzleDatas in itemsInPotionPuzzleDatasList)
		{
			if (itemsInPotionPuzzleDatas.potionCategoryType == potionController.potionCategoryType)
			{
				ItemsInPotionPuzzleController itemsInPotionPuzzleController = (potionController.itemsInPotionPuzzleController = Object.Instantiate(ItemsInPotionPuzzleControllerPrefab, potionController.transform).GetComponent<ItemsInPotionPuzzleController>());
				itemsInPotionPuzzleController.transform.localPosition = Vector3.zero;
				itemsInPotionPuzzleController.transform.localScale = Vector3.one;
				itemsInPotionPuzzleController.SetController(itemsInPotionPuzzleDatas.itemPrefabType, potionController.orderInSet);
				return;
			}
		}
		Debug.LogError($"{potionController.potionCategoryType} is not avilable in PotionPuzzleItemsController");
	}

	private void SetPotionParticlesPuzzle(PotionController potionController)
	{
		potionController.potionLiquid.transform.localScale = new Vector3(1f, 0.75f, 1f);
		foreach (PotionParticlesPuzzleData potionParticlesPuzzleDatas in potionParticlesPuzzleDatasList)
		{
			if (potionParticlesPuzzleDatas.potionCategoryType == potionController.potionCategoryType)
			{
				ParticlePuzzleController particlePuzzleController = (potionController.particlePuzzleController = Object.Instantiate(ParticlePuzzleControllerObject, potionController.transform).GetComponent<ParticlePuzzleController>());
				particlePuzzleController.transform.localPosition = Vector3.zero;
				particlePuzzleController.transform.localScale = Vector3.one;
				particlePuzzleController.SetController(potionParticlesPuzzleDatas.particleGemMesh, potionController.orderInSet);
				return;
			}
		}
		Debug.LogError($"{potionController.potionCategoryType} is not avilable in PotionPuzzleItemsController");
	}

	private void OnValidate()
	{
		foreach (PotionCategory tutorialPotionCategorys in tutorialPotionCategorysList)
		{
			tutorialPotionCategorys.OnValidate();
		}
		foreach (PotionCategory potionCategorys in potionCategorysList)
		{
			potionCategorys.OnValidate();
		}
		foreach (PotionPuzzlesSprites potionPuzzlesSprites in potionPuzzlesSpritesList)
		{
			potionPuzzlesSprites.OnValidate();
		}
		foreach (PuzzlesPotionsData puzzlesPotionsData in puzzlesPotionsDataList)
		{
			puzzlesPotionsData.OnValidate();
		}
		foreach (PotionPrefabColorblindData potionPrefabColorblindDatas in potionPrefabColorblindDatasList)
		{
			potionPrefabColorblindDatas.OnValidate();
		}
	}
}
