using Steamworks;
using UnityEngine;

public class GoToURL : Singleton<GoToURL>
{
	protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

	private bool isOverLayActivated;

	private void OnEnable()
	{
		if (SteamManager.Initialized)
		{
			m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
		}
	}

	private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
	{
		isOverLayActivated = pCallback.m_bActive != 0;
	}

	public void GoToUrl(string URL)
	{
		OpenURLBrowser(URL);
	}

	public void OpenURLBrowser(string URL)
	{
		Application.OpenURL(URL);
	}

	public void OpenURLSteam(string URL)
	{
		Application.OpenURL("steam://openurl/" + URL);
	}
}
