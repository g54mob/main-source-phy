using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class AbilitiesManager : SceneSingleton<AbilitiesManager>
{
	private AbilitiesPanelUI abilitiesPanelUI;

	private UpgradesManager upgradesManager;

	private CharacterInventory characterInventory;

	private PotionsManager potionsManager;

	[ReadOnly]
	public int abilitiesUsed;

	[SerializeField]
	private float shelvesHighlightTime = 10f;

	private bool isHighlightUnlocked;

	private bool isAssembleUnlocked;

	private bool isShelveHighlightUnlocked;

	[Space(5f)]
	[ReadOnly]
	public float currentHighlightMaxTimer;

	[ReadOnly]
	public float currentHighlightTimer;

	[Space]
	[ReadOnly]
	public float currentAssembleMaxTimer;

	[ReadOnly]
	public float currentAssembleTimer;

	[Space]
	[ReadOnly]
	public float currentShelveHighlightMaxTimer;

	[ReadOnly]
	public float currentShelveHighlightTimer;

	public void LoadData()
	{
		abilitiesPanelUI = SceneSingleton<AbilitiesPanelUI>.Instance;
		upgradesManager = SceneSingleton<UpgradesManager>.Instance;
		characterInventory = SceneSingleton<CharacterInventory>.Instance;
		potionsManager = SceneSingleton<PotionsManager>.Instance;
		currentHighlightTimer = SaveSystem.currentlevelSaveData.currentHighlightTimer;
		currentAssembleTimer = SaveSystem.currentlevelSaveData.currentAssembleTimer;
		currentShelveHighlightMaxTimer = SaveSystem.currentlevelSaveData.currentShelveHighlightMaxTimer;
		abilitiesUsed = SaveSystem.currentlevelSaveData.abilitiesUsed;
		UpdateShelves(SceneSingleton<CabinetsManager>.Instance.GetCompletedShelvesCount(), isLoading: true);
	}

	public void UpdateShelves(int currentShelves, bool isLoading = false)
	{
		if (currentShelves >= upgradesManager.shelfsNeededToUnlockHighlight)
		{
			if (currentHighlightTimer <= 0f)
			{
				abilitiesPanelUI.ActivateHighlightPanel();
			}
			if (!isHighlightUnlocked)
			{
				isHighlightUnlocked = true;
				if (!isLoading)
				{
					SceneSingleton<AbilitiesUnlockPanelUI>.Instance.ActivateHightlightAbility();
				}
			}
		}
		else
		{
			abilitiesPanelUI.SetHighlightLocked(upgradesManager.shelfsNeededToUnlockHighlight - currentShelves);
		}
		if (currentShelves >= upgradesManager.shelfsNeededToUnlockAssemble)
		{
			if (currentAssembleTimer <= 0f)
			{
				abilitiesPanelUI.ActivateAssemblePanel();
			}
			if (!isAssembleUnlocked)
			{
				isAssembleUnlocked = true;
				if (!isLoading)
				{
					SceneSingleton<AbilitiesUnlockPanelUI>.Instance.ActivateAssembleAbility();
				}
			}
		}
		else
		{
			abilitiesPanelUI.SetAssembleLocked(upgradesManager.shelfsNeededToUnlockAssemble - currentShelves);
		}
		if (currentShelves >= upgradesManager.shelfsNeededToUnlockShelvesHighlight)
		{
			if (currentShelveHighlightTimer <= 0f)
			{
				abilitiesPanelUI.ActivateShelveHighlightPanel();
			}
			if (!isShelveHighlightUnlocked)
			{
				isShelveHighlightUnlocked = true;
				if (!isLoading)
				{
					SceneSingleton<AbilitiesUnlockPanelUI>.Instance.ActivateShelveHightlightAbility();
				}
			}
		}
		else
		{
			abilitiesPanelUI.SetShelveHighlightLocked(upgradesManager.shelfsNeededToUnlockShelvesHighlight - currentShelves);
		}
	}

	public void ActivateHighlightAbility()
	{
		if (currentHighlightTimer > 0f)
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		if (!isHighlightUnlocked)
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		if (SaveSystem.GetDisableAbilitiesSetting())
		{
			SceneSingleton<DisabledAbilitiesOptionsUI>.Instance.ShowUI();
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		if (!characterInventory.IsHoldingPotion())
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_TutorialComplete);
		SceneSingleton<HighlightManager>.Instance.HighlightPotions(characterInventory.GetSelectedPotion());
		abilitiesUsed++;
		currentHighlightTimer = upgradesManager.GetHighlightCooldown();
		currentHighlightMaxTimer = currentHighlightTimer;
	}

	public void ActivateShelveHighlightAbility()
	{
		if (currentShelveHighlightTimer > 0f)
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		if (!isShelveHighlightUnlocked)
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		if (SaveSystem.GetDisableAbilitiesSetting())
		{
			SceneSingleton<DisabledAbilitiesOptionsUI>.Instance.ShowUI();
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		abilitiesUsed++;
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_TutorialComplete);
		SceneSingleton<CabinetsManager>.Instance.ActivateShelveHighlightManager();
		currentShelveHighlightTimer = upgradesManager.GetShelvesHighlightCooldown();
		currentShelveHighlightMaxTimer = currentShelveHighlightTimer;
		StopAllCoroutines();
		StartCoroutine(DisableHighlightsCoroutine());
	}

	private IEnumerator DisableHighlightsCoroutine()
	{
		yield return new WaitForSeconds(shelvesHighlightTime);
		SceneSingleton<CabinetsManager>.Instance.DisableShelveHighlightManager();
	}

	public void ActivateAssembleAbility()
	{
		if (currentAssembleTimer > 0f)
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		if (!isAssembleUnlocked)
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		if (SaveSystem.GetDisableAbilitiesSetting())
		{
			SceneSingleton<DisabledAbilitiesOptionsUI>.Instance.ShowUI();
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		if (characterInventory.IsInventoryFull())
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		if (!characterInventory.IsHoldingPotion())
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_TutorialComplete);
		List<PotionController> allPotionsInSetOnTheFloor = potionsManager.GetAllPotionsInSetOnTheFloor(characterInventory.GetSelectedPotion());
		if (allPotionsInSetOnTheFloor.Count == 0)
		{
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
			return;
		}
		int num = (int)Mathf.Min(characterInventory.GetEmptyInventorySpace(), upgradesManager.GetAssembleCount());
		for (int i = 0; i < allPotionsInSetOnTheFloor.Count; i++)
		{
			characterInventory.AddItemToInventory(allPotionsInSetOnTheFloor[i]);
			num--;
			if (num == 0)
			{
				break;
			}
		}
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Potion_PickUp);
		characterInventory.RefreshItemArt();
		characterInventory.UpdateInventorySize();
		currentAssembleTimer = upgradesManager.GetAssembleCooldown();
		currentAssembleMaxTimer = currentAssembleTimer;
		abilitiesUsed++;
	}

	private void Update()
	{
		if (currentShelveHighlightTimer > 0f)
		{
			currentShelveHighlightTimer -= Time.deltaTime;
			if (currentShelveHighlightTimer < 0f)
			{
				currentShelveHighlightTimer = 0f;
				Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Ability1_Complition);
				abilitiesPanelUI.ActivateShelveHighlightPanel();
			}
			else
			{
				abilitiesPanelUI.SetShelveHighlightTimer((int)currentShelveHighlightTimer, currentShelveHighlightTimer / currentShelveHighlightMaxTimer);
			}
		}
		if (currentHighlightTimer > 0f)
		{
			currentHighlightTimer -= Time.deltaTime;
			if (currentHighlightTimer < 0f)
			{
				currentHighlightTimer = 0f;
				Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Ability2_Complition);
				abilitiesPanelUI.ActivateHighlightPanel();
			}
			else
			{
				abilitiesPanelUI.SetHighlightTimer((int)currentHighlightTimer, currentHighlightTimer / currentHighlightMaxTimer);
			}
		}
		if (currentAssembleTimer > 0f)
		{
			currentAssembleTimer -= Time.deltaTime;
			if (currentAssembleTimer < 0f)
			{
				currentAssembleTimer = 0f;
				Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Ability3_Complition);
				abilitiesPanelUI.ActivateAssemblePanel();
			}
			else
			{
				abilitiesPanelUI.SetAssembleTimer((int)currentAssembleTimer, currentAssembleTimer / currentAssembleMaxTimer);
			}
		}
	}
}
