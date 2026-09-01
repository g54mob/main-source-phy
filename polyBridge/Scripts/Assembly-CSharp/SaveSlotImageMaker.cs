using UnityEngine;
using UnityEngine.UI;

public class SaveSlotImageMaker
{
	private static Texture2D m_SaveSlotTexture;

	private static RenderTexture m_SaveSlotRenderTexture;

	private static readonly int IMAGE_WIDTH = 640;

	private static readonly int IMAGE_HEIGHT = 360;

	public static void Init()
	{
		m_SaveSlotTexture = new Texture2D(IMAGE_WIDTH, IMAGE_HEIGHT, TextureFormat.RGB24, mipChain: true);
		m_SaveSlotRenderTexture = new RenderTexture(IMAGE_WIDTH, IMAGE_HEIGHT, 16);
	}

	public static byte[] CaptureImage(GameState nextState)
	{
		Vector3 position = Cameras.MainCamera().transform.position;
		Quaternion rotation = Cameras.MainCamera().transform.rotation;
		float orthographicSize = Cameras.MainCamera().orthographicSize;
		Game.m_TakingScreenshotForAutoSave = true;
		GameStateBuild.Exit(GameState.SIM);
		GameStateManager.BashState(GameState.SIM);
		GameStateSim.m_SkipBridgeRestoreOnExit = true;
		GameStateSim.Enter(GameState.BUILD);
		BridgeEdges.SetOriginalColor();
		Cameras.MainCamera().targetTexture = m_SaveSlotRenderTexture;
		Cameras.ForegroundCamera().targetTexture = m_SaveSlotRenderTexture;
		Cameras.OutlinesCamera().targetTexture = m_SaveSlotRenderTexture;
		Cameras.BuildZoneCamera().targetTexture = m_SaveSlotRenderTexture;
		Cameras.RenderLastCamera().targetTexture = m_SaveSlotRenderTexture;
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = Cameras.MainCamera().targetTexture;
		bool stressViewEnabled = Profiles.m_ActiveProfile.m_StressViewEnabled;
		Profiles.m_ActiveProfile.m_StressViewEnabled = false;
		Cameras.MainCamera().Render();
		Cameras.ForegroundCamera().Render();
		Cameras.OutlinesCamera().Render();
		Cameras.BuildZoneCamera().Render();
		Cameras.RenderLastCamera().Render();
		Profiles.m_ActiveProfile.m_StressViewEnabled = stressViewEnabled;
		m_SaveSlotTexture.ReadPixels(new Rect(0f, 0f, Cameras.MainCamera().targetTexture.width, Cameras.MainCamera().targetTexture.height), 0, 0, recalculateMipMaps: false);
		m_SaveSlotTexture.Apply();
		RenderTexture.active = active;
		Cameras.MainCamera().targetTexture = null;
		Cameras.ForegroundCamera().targetTexture = null;
		Cameras.OutlinesCamera().targetTexture = null;
		Cameras.BuildZoneCamera().targetTexture = null;
		Cameras.RenderLastCamera().targetTexture = null;
		byte[] result = m_SaveSlotTexture.EncodeToJPG();
		GameStateSim.Exit(GameState.BUILD);
		GameStateManager.BashState(GameState.BUILD);
		if (nextState == GameState.BUILD)
		{
			GameStateBuild.Enter(GameState.SIM);
		}
		Cameras.MainCamera().transform.position = position;
		Cameras.MainCamera().transform.rotation = rotation;
		Cameras.SetOrthographicSize(orthographicSize);
		Game.m_TakingScreenshotForAutoSave = false;
		return result;
	}

	public static void GenerateImage(BridgeSaveSlotData bridgeSlot, RawImage image)
	{
		if (m_SaveSlotTexture.LoadImage(bridgeSlot.m_Thumb))
		{
			image.texture = m_SaveSlotTexture;
			image.uvRect = new Rect(image.uvRect.x, image.uvRect.y, image.uvRect.width, 0.999f);
		}
	}
}
