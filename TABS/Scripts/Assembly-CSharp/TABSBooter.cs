using System;
using System.Collections;
using Landfall.TABS.Workshop;
using TFBGames;
using UnityEngine;

public class TABSBooter : MonoBehaviour
{
	public static bool _DisableGamepad;

	public ConditionalScene[] conditionalScenes;

	public bool loadOnAwake = true;

	public Action<float> LoadUpdate;

	private IPlayerPrefsPlatform m_PlayerPrefs;

	private WaitForStorage m_WaitForStorage;

	private AccountManager m_AccountManager;

	private CustomContentLoaderModIO m_CustomContentLoader;

	private bool doneLoadingCustomContent;

	private void Start()
	{
		m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		m_WaitForStorage = ServiceLocator.GetService<WaitForStorage>();
		m_CustomContentLoader = ServiceLocator.GetService<CustomContentLoaderModIO>();
		m_AccountManager = ServiceLocator.GetService<AccountManager>();
		if (loadOnAwake)
		{
			Init();
		}
	}

	public void Init()
	{
		LoadUpdate?.Invoke(0.1f);
		m_WaitForStorage.FireWhenReady(OnStorageReady);
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			Debug.Log($"Arg {i} : {commandLineArgs[i]}");
			if (string.Equals(commandLineArgs[i], "-disablegamepad"))
			{
				_DisableGamepad = true;
			}
		}
	}

	private void OnStorageReady()
	{
		Debug.Log(DateTime.Today.DayOfYear);
		LoadUpdate?.Invoke(0.25f);
		LoadUpdate?.Invoke(0.2f);
		if (!doneLoadingCustomContent)
		{
			m_CustomContentLoader.Refresh(delegate
			{
				LoadUpdate?.Invoke(0.2f);
				Debug.Log("Content Load Successful");
				doneLoadingCustomContent = true;
			});
		}
		SteamManager steamManager = (SteamManager)ServiceLocator.GetService<IPlatformManager>();
		m_CustomContentLoader.StartCoroutine(WaitForSteamInitialized());
		LoadUpdate?.Invoke(0.1f);
		StartCoroutine(WaitForCustomContent());
		IEnumerator WaitForCustomContent()
		{
			if (m_AccountManager.ActiveAccount != null)
			{
				yield return new WaitUntil(() => doneLoadingCustomContent);
			}
			LoadUpdate?.Invoke(0.15f);
			LoadScenes();
		}
		IEnumerator WaitForSteamInitialized()
		{
			yield return new WaitUntil(() => steamManager.Initialized);
			AchievementService.bypassMainMenuCheck = true;
			SecretUnlock.CheckAchievements();
			CampaignPlayerDataHolder.CheckAchievements();
			AchievementService.bypassMainMenuCheck = false;
		}
	}

	private void LoadScenes()
	{
		for (int i = 0; i < conditionalScenes.Length; i++)
		{
			bool flag = true;
			bool flag2 = false;
			for (int j = 0; j < conditionalScenes[i].conditions.Length; j++)
			{
				if (CheckCondition(conditionalScenes[i].conditions[j], conditionalScenes[i].conditionKey))
				{
					flag2 = true;
				}
				else
				{
					flag = false;
				}
			}
			if (conditionalScenes[i].conditionType == ConditionalScene.ConditionType.AllTrue && flag)
			{
				TABSSceneManager.LoadScene(conditionalScenes[i].sceneToLoad, forceInstantLoad: true);
			}
			else if (conditionalScenes[i].conditionType == ConditionalScene.ConditionType.OneTrue && flag2)
			{
				TABSSceneManager.LoadScene(conditionalScenes[i].sceneToLoad, forceInstantLoad: true);
			}
		}
		TABSSceneManager.LoadMainMenu(forceInstantLoad: true);
	}

	private bool CheckCondition(ConditionalScene.Condition condition, string key)
	{
		bool result = false;
		switch (condition)
		{
		case ConditionalScene.Condition.IsEarlyAccess:
			result = true;
			break;
		case ConditionalScene.Condition.HasNotBeenTriggered:
			if (m_PlayerPrefs.GetInt(key, -1) != 2 || Application.isEditor)
			{
				m_PlayerPrefs.SetInt(key, 2);
				result = true;
			}
			break;
		case ConditionalScene.Condition.IsBeforeAprilFirst:
			return false;
		default:
			_ = 4;
			break;
		case ConditionalScene.Condition.TitleSafeScreen:
			break;
		}
		return result;
	}
}
