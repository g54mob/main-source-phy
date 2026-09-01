using System;
using System.Threading.Tasks;
using UnityEngine;

public class WorkshopPreview
{
	public static readonly int PREVIEW_IMAGE_WIDTH = 1280;

	public static readonly int PREVIEW_IMAGE_HEIGHT = 720;

	public static byte[] m_PreviewBytes;

	public static RenderTexture m_OverlayRenderTexture;

	public static RenderTexture m_PreviewRenderTexture;

	public static Texture2D m_PreviewTexture2D;

	public static Texture2D m_OverlayTexture2D;

	public static bool m_IsTakingScreenshot;

	private static int m_CachedScreenWidth;

	private static int m_CachedScreenHeight;

	public static void Init()
	{
		m_CachedScreenWidth = Screen.width;
		m_CachedScreenHeight = Screen.height;
		m_OverlayRenderTexture = new RenderTexture(Screen.width, Screen.height, 16);
		m_OverlayTexture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, mipChain: false);
		m_PreviewRenderTexture = new RenderTexture(PREVIEW_IMAGE_WIDTH, PREVIEW_IMAGE_HEIGHT, 16);
		m_PreviewTexture2D = new Texture2D(PREVIEW_IMAGE_WIDTH, PREVIEW_IMAGE_HEIGHT, TextureFormat.RGB24, mipChain: false);
	}

	public static async void Create(bool showBridge, bool showPrebuilds, PointOfViewType pointOfViewType, GameState returnState, BridgeSaveData bridgeSaveData, Action<BridgeSaveData> overlayCapturedCallback, Action completeCallback)
	{
		if (m_CachedScreenWidth != Screen.width || m_CachedScreenHeight != Screen.height)
		{
			Init();
		}
		Cameras.MainCamera().targetTexture = m_OverlayRenderTexture;
		Cameras.ForegroundCamera().targetTexture = m_OverlayRenderTexture;
		Cameras.OutlinesCamera().targetTexture = m_OverlayRenderTexture;
		Cameras.BuildZoneCamera().targetTexture = m_OverlayRenderTexture;
		Cameras.RenderLastCamera().targetTexture = m_OverlayRenderTexture;
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = Cameras.MainCamera().targetTexture;
		BridgeSelectionSet.CancelSelection();
		DisableSelectionUI();
		Cameras.MainCamera().Render();
		Cameras.ForegroundCamera().Render();
		Cameras.OutlinesCamera().Render();
		Cameras.BuildZoneCamera().Render();
		Cameras.RenderLastCamera().Render();
		m_OverlayTexture2D.ReadPixels(new Rect(0f, 0f, Cameras.MainCamera().targetTexture.width, Cameras.MainCamera().targetTexture.height), 0, 0, recalculateMipMaps: false);
		m_OverlayTexture2D.Apply();
		overlayCapturedCallback?.Invoke(bridgeSaveData);
		RenderTexture.active = active;
		Cameras.MainCamera().targetTexture = null;
		Cameras.ForegroundCamera().targetTexture = null;
		Cameras.OutlinesCamera().targetTexture = null;
		Cameras.BuildZoneCamera().targetTexture = null;
		Cameras.RenderLastCamera().targetTexture = null;
		m_IsTakingScreenshot = true;
		Cameras.EnableRenderOverAllWithTexture(m_OverlayTexture2D);
		Vector3 restoreCameraPos = Cameras.MainCamera().transform.position;
		Quaternion restoreCameraRot = Cameras.MainCamera().transform.rotation;
		float restoreCameraOrtho = Cameras.MainCamera().orthographicSize;
		GameStateManager.SwitchToStateImmediate(GameState.PHOTO);
		TakeBehindTheScenesScreenshot(showBridge, showPrebuilds, pointOfViewType);
		GameStateManager.SwitchToState(returnState);
		await AsyncWaitFrames(1);
		Cameras.MainCamera().transform.position = restoreCameraPos;
		Cameras.MainCamera().transform.rotation = restoreCameraRot;
		Cameras.SetOrthographicSize(restoreCameraOrtho);
		await AsyncWaitFrames(1);
		Cameras.DisableRenderOverAll();
		completeCallback?.Invoke();
		m_IsTakingScreenshot = false;
	}

	public static void TakeScreenshot(bool showBridge, bool showPrebuilds)
	{
		Vector3 position = Cameras.MainCamera().transform.position;
		Quaternion rotation = Cameras.MainCamera().transform.rotation;
		float orthographicSize = Cameras.MainCamera().orthographicSize;
		if (SandboxSettings.m_ThumbnailCameraSaved)
		{
			Cameras.MainCamera().transform.position = SandboxSettings.m_ThumbnailCameraPos;
			Cameras.MainCamera().transform.rotation = SandboxSettings.m_ThumbnailCameraRot;
			Cameras.SetOrthographicSize(SandboxSettings.m_ThumbnailCameraOrthographicSize);
		}
		Cameras.MainCamera().targetTexture = m_PreviewRenderTexture;
		Cameras.ForegroundCamera().targetTexture = m_PreviewRenderTexture;
		Cameras.OutlinesCamera().targetTexture = m_PreviewRenderTexture;
		Cameras.BuildZoneCamera().targetTexture = m_PreviewRenderTexture;
		Cameras.RenderLastCamera().targetTexture = m_PreviewRenderTexture;
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = Cameras.MainCamera().targetTexture;
		Game.SetCameraCullingMasks(GameState.SIM);
		GameStatePhoto.ForceDisableBridgeParts(showBridge, showPrebuilds);
		Cameras.MainCamera().Render();
		Cameras.ForegroundCamera().Render();
		Cameras.OutlinesCamera().Render();
		Cameras.BuildZoneCamera().Render();
		Cameras.RenderLastCamera().Render();
		GameStatePhoto.UndoForceDisabled();
		Game.SetCameraCullingMasks(GameState.SIM);
		Cameras.MainCamera().transform.position = position;
		Cameras.MainCamera().transform.rotation = rotation;
		Cameras.MainCamera().orthographicSize = orthographicSize;
		m_PreviewTexture2D.ReadPixels(new Rect(0f, 0f, Cameras.MainCamera().targetTexture.width, Cameras.MainCamera().targetTexture.height), 0, 0, recalculateMipMaps: false);
		m_PreviewTexture2D.Apply();
		RenderTexture.active = active;
		Cameras.MainCamera().targetTexture = null;
		Cameras.ForegroundCamera().targetTexture = null;
		Cameras.OutlinesCamera().targetTexture = null;
		Cameras.BuildZoneCamera().targetTexture = null;
		Cameras.RenderLastCamera().targetTexture = null;
		m_PreviewBytes = m_PreviewTexture2D.EncodeToJPG();
	}

	private static void TakeBehindTheScenesScreenshot(bool showBridge, bool showPrebuilds, PointOfViewType pointOfViewType)
	{
		Vector3 position = Cameras.MainCamera().transform.position;
		Quaternion rotation = Cameras.MainCamera().transform.rotation;
		float orthographicSize = Cameras.MainCamera().orthographicSize;
		PointsOfView.m_PointsOfView[pointOfViewType].FrameObjects(Game.GetLevelId());
		PointOfView pointOfView = PointsOfView.GetPointOfView(pointOfViewType);
		Cameras.SetOrthographicSize(pointOfView.m_OrthographicsSize);
		Cameras.MainCamera().transform.position = pointOfView.m_Pos;
		Cameras.MainCamera().transform.rotation = pointOfView.m_Rot;
		BridgeSelectionSet.CancelSelection();
		DisableSelectionUI();
		if (!SandboxSettings.m_ThumbnailCameraSaved)
		{
			SandboxSettings.SaveThumbnailCamera(Cameras.MainCamera());
		}
		TakeScreenshot(showBridge, showPrebuilds);
		Cameras.MainCamera().transform.position = position;
		Cameras.MainCamera().transform.rotation = rotation;
		Cameras.SetOrthographicSize(orthographicSize);
	}

	private static async Task AsyncWaitFrames(int numFramesToWait)
	{
		int startFrame = Time.frameCount;
		for (int i = 0; i < 1000; i++)
		{
			await Task.Delay(1);
			if (Time.frameCount - startFrame >= numFramesToWait)
			{
				break;
			}
		}
	}

	private static void DisableSelectionUI()
	{
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			edge.m_HighlightFX.SetActive(value: false);
			edge.m_LockFX.SetActive(value: false);
			edge.m_SoftLockFX.SetActive(value: false);
		}
		foreach (BridgePillar bridgePillar in BridgePillars.m_BridgePillars)
		{
			bridgePillar.DeSelect();
			bridgePillar.m_LockIcon.SetActive(value: false);
			bridgePillar.m_SoftLockIcon.SetActive(value: false);
		}
	}
}
