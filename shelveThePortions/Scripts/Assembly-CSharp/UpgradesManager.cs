using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class UpgradesManager : SceneSingleton<UpgradesManager>
{
	[ReadOnly]
	public int aviliableSkillPoints;

	[Space]
	[SerializeField]
	private bool debug_UnlockEverthing;

	[SerializeField]
	private bool debug_IgnoreShelvesToUpgrade;

	[SerializeField]
	private bool debug_IsAddStartingPoints;

	[SerializeField]
	private int debug_StartingPoints = 50;

	[Space]
	[SerializeField]
	private List<int> shelveToGetSkillPointsList = new List<int>();

	[Space]
	public int shelfsNeededToUnlockHighlight = 15;

	public int shelfsNeededToUnlockAssemble = 25;

	public int shelfsNeededToUnlockShelvesHighlight = 50;

	[Space]
	public int shelfsNeededToUnlockHighlightPuzzle = 25;

	public int shelfsNeededToUnlockRevealSolution = 50;

	[Space]
	[ReadOnly]
	public int currentHighlightIndex;

	public List<float> highlightsCooldownLists = new List<float>();

	[Space]
	[ReadOnly]
	public int currentShelvesHighlightIndex;

	public List<float> shelvesHighlightsCooldownLists = new List<float>();

	[Space]
	[ReadOnly]
	public int currentAssembleIndex;

	public List<int> assemblesCountLists = new List<int>();

	public List<float> assemblesCooldownLists = new List<float>();

	[Space]
	[ReadOnly]
	public int currentInventoryUpgradeIndex;

	public List<int> inventoryUpgradesLists = new List<int>();

	[Space]
	[ReadOnly]
	public int currentSpeedUpgradeIndex;

	public List<float> speedUpgradesLists = new List<float>();

	[Space]
	[ReadOnly]
	public int skillPointsToUnlockSprinting = 2;

	[ReadOnly]
	public bool isSprintUpgraded;

	[SerializeField]
	private GameObject upgradeUIObject;

	public bool CanUpgradeHighlight()
	{
		return currentHighlightIndex < highlightsCooldownLists.Count - 1;
	}

	public float GetHighlightCooldown()
	{
		return highlightsCooldownLists[currentHighlightIndex];
	}

	public bool CanUpgradeShelvesHighlight()
	{
		return currentShelvesHighlightIndex < shelvesHighlightsCooldownLists.Count - 1;
	}

	public float GetShelvesHighlightCooldown()
	{
		return shelvesHighlightsCooldownLists[currentShelvesHighlightIndex];
	}

	public bool CanUpgradeAssemble()
	{
		return currentAssembleIndex < assemblesCooldownLists.Count - 1;
	}

	public float GetAssembleCount()
	{
		return assemblesCountLists[currentAssembleIndex];
	}

	public float GetAssembleCooldown()
	{
		return assemblesCooldownLists[currentAssembleIndex];
	}

	public bool CanUpgradeInventory()
	{
		return currentInventoryUpgradeIndex < inventoryUpgradesLists.Count - 1;
	}

	public bool CanUpgradeSpeed()
	{
		return currentSpeedUpgradeIndex < speedUpgradesLists.Count - 1;
	}

	public void LoadUpgradeManager(LevelSaveData levelSaveData)
	{
		currentInventoryUpgradeIndex = levelSaveData.inventoryUpgradeIndex;
		currentSpeedUpgradeIndex = levelSaveData.speedUpgradeIndex;
		currentAssembleIndex = levelSaveData.currentAssembleIndex;
		currentHighlightIndex = levelSaveData.currentHighlightIndex;
		currentShelvesHighlightIndex = levelSaveData.currentShelvesHighlightIndex;
		isSprintUpgraded = levelSaveData.isSprintUpgraded;
		UpdateSkillPoints(isLoading: true);
	}

	public void UpdateSkillPoints(bool isLoading)
	{
		UpdateSkillPoints(SceneSingleton<CabinetsManager>.Instance.GetCompletedShelvesCount(), isLoading);
	}

	public void UpdateSkillPoints(int currentShelves, bool isLoading)
	{
		int num = aviliableSkillPoints;
		int num2 = 0;
		using (List<int>.Enumerator enumerator = shelveToGetSkillPointsList.GetEnumerator())
		{
			while (enumerator.MoveNext() && enumerator.Current <= currentShelves)
			{
				num2++;
			}
		}
		aviliableSkillPoints = num2;
		if (isSprintUpgraded)
		{
			aviliableSkillPoints -= skillPointsToUnlockSprinting;
		}
		aviliableSkillPoints -= currentInventoryUpgradeIndex;
		aviliableSkillPoints -= currentSpeedUpgradeIndex;
		aviliableSkillPoints -= currentHighlightIndex;
		aviliableSkillPoints -= currentAssembleIndex;
		aviliableSkillPoints -= currentShelvesHighlightIndex;
		if (num2 == shelveToGetSkillPointsList.Count && aviliableSkillPoints == 0)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Upgrade_All);
		}
		if (num < aviliableSkillPoints && !isLoading)
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_UpgradeGained);
			upgradeUIObject.SetActive(value: false);
			upgradeUIObject.GetComponent<CanvasGroup>().alpha = 0f;
			upgradeUIObject.SetActive(value: true);
		}
		SceneSingleton<CatsManager>.Instance.UpdateUpgradeHighlightAllCats(aviliableSkillPoints > 0);
		ApplyUpgrades();
	}

	private void ApplyUpgrades()
	{
		SceneSingleton<CharacterInventory>.Instance.ChangeInventorySize(inventoryUpgradesLists[currentInventoryUpgradeIndex]);
		SceneSingleton<FirstPersonController>.Instance.walkSpeed = speedUpgradesLists[currentSpeedUpgradeIndex];
		SceneSingleton<FirstPersonController>.Instance.enableSprint = isSprintUpgraded;
	}

	public int GetShelvesTillNextSkillPoint()
	{
		int completedShelvesCount = SceneSingleton<CabinetsManager>.Instance.GetCompletedShelvesCount();
		foreach (int shelveToGetSkillPoints in shelveToGetSkillPointsList)
		{
			if (shelveToGetSkillPoints > completedShelvesCount)
			{
				return shelveToGetSkillPoints - completedShelvesCount;
			}
		}
		return -1;
	}

	public bool IsCarryUpgradeMaxed()
	{
		return currentInventoryUpgradeIndex == inventoryUpgradesLists.Count - 1;
	}

	public bool IsSpeedUpgradeMaxed()
	{
		return currentSpeedUpgradeIndex == speedUpgradesLists.Count - 1;
	}

	public bool IsSprintUpgradeMaxed()
	{
		return isSprintUpgraded;
	}

	public bool IsHighlightUpgradeMaxed()
	{
		return currentHighlightIndex == highlightsCooldownLists.Count - 1;
	}

	public bool IsShelveHighlightUpgradeMaxed()
	{
		return currentShelvesHighlightIndex == shelvesHighlightsCooldownLists.Count - 1;
	}

	public bool IsAssembleUpgradeMaxed()
	{
		return currentAssembleIndex == assemblesCooldownLists.Count - 1;
	}

	public bool CanClickCarryUpgrade()
	{
		if (aviliableSkillPoints < 1)
		{
			return false;
		}
		return currentInventoryUpgradeIndex < inventoryUpgradesLists.Count - 1;
	}

	public bool CanClickSpeedUpgrade()
	{
		if (aviliableSkillPoints < 1)
		{
			return false;
		}
		return currentSpeedUpgradeIndex < speedUpgradesLists.Count - 1;
	}

	public bool CanClickHightlightUpgrade()
	{
		if (aviliableSkillPoints < 1)
		{
			return false;
		}
		return currentHighlightIndex < highlightsCooldownLists.Count - 1;
	}

	public bool CanClickShelveHightlightUpgrade()
	{
		if (aviliableSkillPoints < 1)
		{
			return false;
		}
		return currentShelvesHighlightIndex < shelvesHighlightsCooldownLists.Count - 1;
	}

	public bool CanClickAssembleUpgrade()
	{
		if (aviliableSkillPoints < 1)
		{
			return false;
		}
		return currentAssembleIndex < assemblesCooldownLists.Count - 1;
	}

	public bool CanClickSprintUpgrade()
	{
		if (aviliableSkillPoints < skillPointsToUnlockSprinting)
		{
			return false;
		}
		return !isSprintUpgraded;
	}

	public void UnlockCarryUpgrade()
	{
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Upgrade_1);
		currentInventoryUpgradeIndex++;
		UpdateSkillPoints(isLoading: false);
	}

	public void UnlockSpeedUpgrade()
	{
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Upgrade_1);
		currentSpeedUpgradeIndex++;
		UpdateSkillPoints(isLoading: false);
	}

	public void UnlockHighlightUpgrade()
	{
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Upgrade_1);
		currentHighlightIndex++;
		UpdateSkillPoints(isLoading: false);
	}

	public void UnlockShelveHighlightUpgrade()
	{
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Upgrade_1);
		currentShelvesHighlightIndex++;
		UpdateSkillPoints(isLoading: false);
	}

	public void UnlockAssembleUpgrade()
	{
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Upgrade_1);
		currentAssembleIndex++;
		UpdateSkillPoints(isLoading: false);
	}

	public void UnlockSprintUpgrade()
	{
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Upgrade_1);
		isSprintUpgraded = true;
		UpdateSkillPoints(isLoading: false);
	}
}
