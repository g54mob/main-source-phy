using UnityEngine;

public class PauseMenuUI : PanelUI<PauseMenuUI>
{
	[SerializeField]
	private GameObject wishlistButton;

	[SerializeField]
	private LocalizationTextActivator mainMenuActivator;

	[SerializeField]
	private LocalizationTextActivator exitActivator;

	private bool IsFreeVersion;

	private void Start()
	{
		if (Singleton<GameBuildManager>.Instance != null)
		{
			bool active = Singleton<GameBuildManager>.Instance.IsFreeVersion();
			wishlistButton.SetActive(active);
		}
	}

	public override void SetSelectedButton()
	{
		base.SetSelectedButton();
		bool flag = !SceneSingleton<TutorialManager>.Instance.isTutorialCompleted || IsFreeVersion;
		mainMenuActivator.SetIDString(flag ? "Menu_MainMenu" : "Menu_SaveMainMenu");
		exitActivator.SetIDString(flag ? "Menu_Exit" : "Menu_SaveExit");
	}

	public void GameResume()
	{
		SceneSingleton<GameManager>.Instance.ResumeGame();
	}

	public void GoToMainMenu()
	{
		SceneSingleton<GameManager>.Instance.GoBackToMainMenu();
	}

	public void ShowOptions()
	{
		SceneSingleton<UIManager>.Instance.ShowOptionMenu(isPauseMenu: true);
	}

	public void ResetAllPotionsItemsToStartingPostions()
	{
		SceneSingleton<UIManager>.Instance.ShowConfirmationPanelUI("Menu_ResetPositions_conformation", SceneSingleton<GameManager>.Instance.ResetAllPotionsItemsToStartingPostions);
	}

	public void ExitApplication()
	{
		SaveSystem.SaveLevel();
		ScenesManager.ExitApplication();
	}

	public void OpenWishListButton()
	{
		Singleton<GoToURL>.Instance.OpenURLSteam(URLHolder.SteamPageURL);
	}
}
