using System;
using UnityEngine;

public class GameStatePreloadingAssets : MonoBehaviour
{
	private const float MIN_SECONDS_TO_SHOW_PRELOADING_SCREEN = 0.1f;

	private static Action<string, FileSlot> m_Callback;

	private static string m_LayoutPath;

	private static FileSlot m_FileSlot;

	private static GameState m_PrevGameState;

	private static float m_ShowingPreloadingScreenTimer;

	public static void UpdateManual()
	{
		if (m_ShowingPreloadingScreenTimer > 0f)
		{
			m_ShowingPreloadingScreenTimer -= Time.unscaledDeltaTime;
		}
		else if (!Prefabs.AsyncLoadInProgress())
		{
			GameUI.m_Instance.m_PreloadingObject.SetActive(value: false);
			GameStateManager.BashState(m_PrevGameState);
			m_Callback?.Invoke(m_LayoutPath, m_FileSlot);
			GameStateManager.UpdateManual();
			GameStateManager.LateUpdateManual();
		}
	}

	public static void PreloadLevel(string layoutPath, FileSlot slot, Action<string, FileSlot> callback)
	{
		m_Callback = callback;
		m_LayoutPath = layoutPath;
		m_FileSlot = slot;
		if (Prefabs.m_Instance.IsLevelPreloaded(layoutPath))
		{
			Prefabs.m_Instance.UnloadAssetsNotInLayout(layoutPath);
			callback?.Invoke(layoutPath, m_FileSlot);
			return;
		}
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(layoutPath);
		if (sandboxLayoutData == null)
		{
			Debug.LogWarningFormat("Could not load: {0}", layoutPath);
			return;
		}
		GameUI.m_Instance.m_PreloadingObject.SetActive(value: true);
		m_ShowingPreloadingScreenTimer = 0.1f;
		m_PrevGameState = GameStateManager.GetState();
		GameStateManager.BashState(GameState.PRELOADING_LAYOUT_ASSETS);
		Prefabs.m_Instance.PreloadAssets(layoutPath, sandboxLayoutData);
		Prefabs.m_Instance.UnloadAssetsNotInLayout(layoutPath);
	}

	public static void PreloadLevelInBackground(string layoutPath)
	{
		if (!Prefabs.m_Instance.IsLevelPreloaded(layoutPath))
		{
			SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(layoutPath);
			if (sandboxLayoutData == null)
			{
				Debug.LogWarningFormat("Could not load: {0}", layoutPath);
			}
			else
			{
				Prefabs.m_Instance.PreloadAssets(layoutPath, sandboxLayoutData);
			}
		}
	}

	public static void PreloadTheme(string themeAdressableName, FileSlot slot, Action<string, FileSlot> callback)
	{
		m_Callback = callback;
		m_LayoutPath = themeAdressableName;
		m_FileSlot = slot;
		if (Prefabs.AsyncPrefabExists(themeAdressableName))
		{
			callback?.Invoke(themeAdressableName, m_FileSlot);
			return;
		}
		GameUI.m_Instance.m_PreloadingObject.SetActive(value: true);
		m_ShowingPreloadingScreenTimer = 0.1f;
		m_PrevGameState = GameStateManager.GetState();
		GameStateManager.BashState(GameState.PRELOADING_LAYOUT_ASSETS);
		Prefabs.m_Instance.PreloadSingleTheme(themeAdressableName, string.Empty, null);
	}
}
