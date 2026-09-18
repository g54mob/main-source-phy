public class PlaytestMenuUI : PanelUI<PlaytestMenuUI>
{
	public void Wishlist()
	{
		Singleton<GoToURL>.Instance.OpenURLSteam(URLHolder.SteamPageURL);
	}

	public void GoToMainMenu()
	{
		SceneSingleton<GameManager>.Instance.GoBackToMainMenu();
	}
}
