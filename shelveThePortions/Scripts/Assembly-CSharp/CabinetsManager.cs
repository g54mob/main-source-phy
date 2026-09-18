using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VInspector;

public class CabinetsManager : SceneSingleton<CabinetsManager>
{
	[SerializeField]
	private List<CabinetController> cabinetControllersList = new List<CabinetController>();

	[SerializeField]
	private List<CabinetController> tutorialCabinets = new List<CabinetController>();

	[ReadOnly]
	[SerializeField]
	private int levelPotionsCapacity;

	[ReadOnly]
	public int totalShelvesCounts;

	private TutorialManager tutorialManager;

	private AbilitiesManager abilitiesManager;

	private CabinetsManagerUI cabinetsManagerUI;

	private UpgradesManager upgradesManager;

	private void Start()
	{
		upgradesManager = SceneSingleton<UpgradesManager>.Instance;
		tutorialManager = SceneSingleton<TutorialManager>.Instance;
		abilitiesManager = SceneSingleton<AbilitiesManager>.Instance;
		cabinetsManagerUI = SceneSingleton<CabinetsManagerUI>.Instance;
		totalShelvesCounts = GetShelvesCount();
	}

	public void UpdateShelve()
	{
		upgradesManager.UpdateSkillPoints(GetCompletedShelvesCount(), isLoading: false);
		cabinetsManagerUI.UpdateShelveUI();
		abilitiesManager.UpdateShelves(GetCompletedShelvesCount());
		UpdateAchievements();
		if (!tutorialManager.isTutorialCompleted)
		{
			foreach (CabinetController tutorialCabinet in tutorialCabinets)
			{
				if (!tutorialCabinet.IsCabinetCompleted())
				{
					return;
				}
			}
			tutorialManager.CompleteTutorialStep(TutorialStepType.Tutorial_CompleteAll);
		}
		foreach (CabinetController cabinetControllers in cabinetControllersList)
		{
			if (!cabinetControllers.IsCabinetCompleted())
			{
				return;
			}
		}
		SceneSingleton<GameManager>.Instance.EndGame();
	}

	private void UpdateAchievements()
	{
		int completedShelvesCount = GetCompletedShelvesCount();
		if (completedShelvesCount >= 1)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Shelf_1);
		}
		if (completedShelvesCount >= 25)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Shelf_25);
		}
		if (completedShelvesCount >= 50)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Shelf_50);
		}
		if (completedShelvesCount >= 100)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Shelf_100);
		}
		if (completedShelvesCount >= 150)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Shelf_150);
		}
		if (completedShelvesCount >= 200)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Shelf_200);
		}
	}

	public int GetCompletedShelvesCount()
	{
		int num = 0;
		foreach (CabinetController cabinetControllers in cabinetControllersList)
		{
			num += cabinetControllers.GetCompletedShelvesCount();
		}
		return num;
	}

	public int GetShelvesCount()
	{
		int num = 0;
		foreach (CabinetController cabinetControllers in cabinetControllersList)
		{
			num += cabinetControllers.shelvesList.Count;
		}
		return num;
	}

	public string GetCompletedShelvesUIText()
	{
		return GetCompletedShelvesCount() + " / " + totalShelvesCounts;
	}

	public float GetCompletedShelvesUIPercent()
	{
		return (float)GetCompletedShelvesCount() / (float)totalShelvesCounts;
	}

	public List<CabinetController> GetPotionCabinetControllers(PotionController potionController)
	{
		List<CabinetController> list = new List<CabinetController>();
		foreach (CabinetController cabinetControllers in cabinetControllersList)
		{
			if (cabinetControllers.potionCategoryType == potionController.potionCategoryType)
			{
				list.Add(cabinetControllers);
			}
		}
		return list;
	}

	public CabinetController GetCabinet(int id)
	{
		foreach (CabinetController cabinetControllers in cabinetControllersList)
		{
			if (cabinetControllers.cabinetsID == id)
			{
				return cabinetControllers;
			}
		}
		Debug.LogError("Can't find cabinet with ID " + id);
		return null;
	}

	public void CheckShelves()
	{
		foreach (CabinetController cabinetControllers in cabinetControllersList)
		{
			cabinetControllers.CheckShelves();
		}
	}

	public void UpdateColorBlind()
	{
		bool isColorBlindSetting = SaveSystem.GetIsColorBlindSetting();
		foreach (CabinetController cabinetControllers in cabinetControllersList)
		{
			cabinetControllers.UpdateColorBlind(isColorBlindSetting);
		}
	}

	public void DisableShelveHighlightManager()
	{
		foreach (CabinetController cabinetControllers in cabinetControllersList)
		{
			foreach (ShelfController shelves in cabinetControllers.shelvesList)
			{
				shelves.DisableHighlight();
			}
		}
	}

	public void ActivateShelveHighlightManager()
	{
		List<CabinetController> list = new List<CabinetController>();
		list.AddRange(cabinetControllersList);
		Vector3 playerPos = SceneSingleton<FirstPersonController>.Instance.transform.position;
		list = list.OrderBy((CabinetController s) => (s.transform.position - playerPos).sqrMagnitude).ToList();
		StartCoroutine(PlayHighlightWave(list));
	}

	private IEnumerator PlayHighlightWave(List<CabinetController> sortedCabinetControllersList)
	{
		WaitForSeconds wait = new WaitForSeconds(0.02f);
		foreach (CabinetController sortedCabinetControllers in sortedCabinetControllersList)
		{
			foreach (ShelfController shelves in sortedCabinetControllers.shelvesList)
			{
				shelves.SetHighlight();
			}
			yield return wait;
		}
	}

	[Button]
	public void BEWARE_GetAllCabinets()
	{
		cabinetControllersList.Clear();
		cabinetControllersList.AddRange(Object.FindObjectsByType<CabinetController>(FindObjectsSortMode.None).ToList());
		CalculateLevelPotionCapacity();
	}

	[Button]
	public void BEWARE_SetCabinetsID()
	{
		for (int i = 0; i < cabinetControllersList.Count; i++)
		{
			cabinetControllersList[i].cabinetsID = i;
		}
	}

	private void CalculateLevelPotionCapacity()
	{
		levelPotionsCapacity = 0;
		foreach (CabinetController cabinetControllers in cabinetControllersList)
		{
			levelPotionsCapacity += cabinetControllers.GetPotionsCount();
		}
	}
}
