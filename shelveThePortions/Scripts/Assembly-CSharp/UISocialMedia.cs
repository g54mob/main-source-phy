using UnityEngine;

public class UISocialMedia : MonoBehaviour
{
	public void OpenDiscordURL()
	{
		Singleton<GoToURL>.Instance.GoToUrl(URLHolder.DiscordURL);
	}

	public void OpenYoutubeURL()
	{
		Singleton<GoToURL>.Instance.GoToUrl(URLHolder.YoutubeURL);
	}

	public void OpenStudioSteamURL()
	{
		Singleton<GoToURL>.Instance.OpenURLSteam(URLHolder.StudioSteamURL);
	}

	public void OpenTwitterURL()
	{
		Singleton<GoToURL>.Instance.GoToUrl(URLHolder.TwitterURL);
	}
}
