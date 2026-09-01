using Steamworks;
using TFBGames;
using TMPro;
using UnityEngine;

public class UIBuildSHA : MonoBehaviour
{
	private void Start()
	{
		TextAsset textAsset = Resources.Load<TextAsset>("SHA");
		if (!(textAsset != null))
		{
			return;
		}
		TextMeshProUGUI component = GetComponent<TextMeshProUGUI>();
		if (!(component != null))
		{
			return;
		}
		string text = "Build: " + textAsset.text;
		SteamManager steamManager = (SteamManager)ServiceLocator.GetService<IPlatformManager>();
		if (steamManager != null && steamManager.Initialized)
		{
			text = text + "." + SteamApps.GetAppBuildId();
			if (SteamApps.GetCurrentBetaName(out var pchName, 128))
			{
				text = text + " " + pchName;
			}
		}
		component.text = text;
		Debug.Log(text);
	}
}
