using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DumpPreviewImages
{
	public static bool m_Dumping;

	private static int THUMB_WIDTH = 1280;

	private static int THUMB_HEIGHT = 720;

	private static Texture2D m_PreviewTexture2D;

	private static List<CampaignLevel> m_LevelsToDump = new List<CampaignLevel>();

	private static int m_NextLevelToDumpIndex;

	private static string m_DumpingPath;

	private static DumpPreviewSubMode m_DumpSubMode;

	private static bool m_WaitOneMoreFrame;

	public static void DumpPreviews(List<string> levelIDs)
	{
		m_DumpingPath = Path.Combine(Application.persistentDataPath, "Previews");
		Utils.CreateDirectory(m_DumpingPath);
		m_LevelsToDump.Clear();
		foreach (string levelID in levelIDs)
		{
			CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(levelID);
			if (levelFromId == null)
			{
				Debug.LogWarning("Could not find level with ID '" + levelID + "'");
			}
			else
			{
				m_LevelsToDump.Add(levelFromId);
			}
		}
		m_NextLevelToDumpIndex = 0;
		m_Dumping = true;
		m_DumpSubMode = DumpPreviewSubMode.PRELOAD_LEVEL;
		CampaignWorlds.m_Instance.GetWorldWithLevelId(m_LevelsToDump[m_NextLevelToDumpIndex].m_Id);
		GameStatePreloadingAssets.PreloadLevel(m_LevelsToDump[m_NextLevelToDumpIndex].GetLayoutPath(), null, PreloadCallback);
	}

	public static void Process()
	{
		if (!m_Dumping)
		{
			return;
		}
		if (m_NextLevelToDumpIndex >= m_LevelsToDump.Count)
		{
			m_Dumping = false;
			GameStateManager.SwitchToState(GameState.MAIN_MENU);
		}
		else
		{
			if (m_DumpSubMode == DumpPreviewSubMode.PRELOAD_LEVEL)
			{
				return;
			}
			CampaignLevel campaignLevel = m_LevelsToDump[m_NextLevelToDumpIndex];
			if (m_DumpSubMode == DumpPreviewSubMode.LOAD_LEVEL)
			{
				GameManager.SetGameMode(GameMode.CAMPAIGN, GameSubMode.NONE);
				GameStateManager.SwitchToState(GameState.BUILD);
				Campaign.LoadLevel(campaignLevel);
				m_DumpSubMode = DumpPreviewSubMode.CAPTURE_PREVIEW;
				m_WaitOneMoreFrame = true;
				return;
			}
			if (m_WaitOneMoreFrame)
			{
				m_WaitOneMoreFrame = false;
				return;
			}
			int simulationSpeedIndex = BridgeSimSpeed.m_SimulationSpeedIndex;
			BridgeSimSpeed.m_SimulationSpeedIndex = 0;
			bool disableHud = GameUI.m_DisableHud;
			GameUI.m_DisableHud = true;
			Debug.LogFormat("Dumping {0}...", campaignLevel.m_Filename);
			Dump(Path.Combine(m_DumpingPath, Path.ChangeExtension(campaignLevel.m_Filename, ".png")));
			m_DumpSubMode = DumpPreviewSubMode.PRELOAD_LEVEL;
			m_NextLevelToDumpIndex++;
			if (m_NextLevelToDumpIndex < m_LevelsToDump.Count)
			{
				CampaignWorlds.m_Instance.GetWorldWithLevelId(m_LevelsToDump[m_NextLevelToDumpIndex].m_Id);
				GameStatePreloadingAssets.PreloadLevel(m_LevelsToDump[m_NextLevelToDumpIndex].GetLayoutPath(), null, PreloadCallback);
			}
			BridgeSimSpeed.m_SimulationSpeedIndex = simulationSpeedIndex;
			GameUI.m_DisableHud = disableHud;
		}
	}

	private static void Dump(string fullpath)
	{
		Game.m_TakingScreenshotForAutoSave = true;
		GameStateBuild.Exit(GameState.INVALID);
		GameStateManager.BashState(GameState.SIM);
		GameStateSim.m_SkipBridgeRestoreOnExit = true;
		GameStateSim.Enter(GameState.BUILD);
		ForceZedAxisVehiclesToCenter();
		RenderTexture targetTexture = new RenderTexture(THUMB_WIDTH, THUMB_HEIGHT, 16);
		Cameras.MainCamera().targetTexture = targetTexture;
		Cameras.ForegroundCamera().targetTexture = targetTexture;
		Cameras.RenderLastCamera().targetTexture = targetTexture;
		if (m_PreviewTexture2D == null)
		{
			m_PreviewTexture2D = new Texture2D(THUMB_WIDTH, THUMB_HEIGHT, TextureFormat.RGB24, mipChain: false);
		}
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = Cameras.MainCamera().targetTexture;
		Cameras.MainCamera().Render();
		Cameras.ForegroundCamera().Render();
		Cameras.RenderLastCamera().Render();
		m_PreviewTexture2D.ReadPixels(new Rect(0f, 0f, Cameras.MainCamera().targetTexture.width, Cameras.MainCamera().targetTexture.height), 0, 0, recalculateMipMaps: false);
		m_PreviewTexture2D.Apply();
		RenderTexture.active = active;
		byte[] bytes = m_PreviewTexture2D.EncodeToJPG();
		Utils.CreateDirectory(Path.GetDirectoryName(fullpath));
		File.WriteAllBytes(fullpath, bytes);
		Cameras.MainCamera().targetTexture = null;
		Cameras.ForegroundCamera().targetTexture = null;
		Cameras.RenderLastCamera().targetTexture = null;
		GameStateSim.Exit(GameState.BUILD);
		GameStateManager.BashState(GameState.BUILD);
		Game.m_TakingScreenshotForAutoSave = false;
	}

	private static void ForceZedAxisVehiclesToCenter()
	{
		foreach (ZedAxisVehicle vehicle in ZedAxisVehicles.m_Vehicles)
		{
			vehicle.gameObject.SetActive(value: true);
			vehicle.transform.position = new Vector3(vehicle.transform.position.x, vehicle.transform.position.y, vehicle.m_MeshRenderer.bounds.size.z / 2f);
		}
	}

	private static void PreloadCallback(string levelFilename, FileSlot slot)
	{
		m_DumpSubMode = DumpPreviewSubMode.LOAD_LEVEL;
	}
}
