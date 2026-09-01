using UnityEngine;

public class SandboxSettings
{
	public static string m_Title;

	public static string m_Description;

	public static bool m_HydraulicControllerEnabled;

	public static bool m_Unbreakable;

	public static bool m_UnlimitedHeightFoundations;

	public static bool m_NoWater;

	public static bool m_NoReinforcedRoad;

	public static bool m_SpringAdjustmentsAllowed;

	public static bool m_HideDecor;

	public static float m_FogHeightNormalized;

	public static float m_FogHeightMinWorldY;

	public static float m_FogHeightMaxWorldY;

	public static float m_FogHeightEndRelativeY;

	public static bool m_ThumbnailCameraSaved;

	public static Vector3 m_ThumbnailCameraPos;

	public static Quaternion m_ThumbnailCameraRot;

	public static float m_ThumbnailCameraOrthographicSize;

	public static float m_MultiSelectMovementIncrement;

	public static float DEFAULT_FOG_HEIGHT_NORMALIZED = 0.5f;

	public static bool m_ThreeWaySplitJointsEnabled;

	public static void Init()
	{
		m_Title = string.Empty;
		m_Description = string.Empty;
		m_HydraulicControllerEnabled = false;
		m_Unbreakable = false;
		m_UnlimitedHeightFoundations = false;
		m_NoWater = false;
		m_NoReinforcedRoad = true;
		m_SpringAdjustmentsAllowed = false;
		m_HideDecor = false;
		m_FogHeightNormalized = DEFAULT_FOG_HEIGHT_NORMALIZED;
		m_FogHeightMinWorldY = HeightFog.DEFAULT_FOG_HEIGHT_START_MIN_WORLD_Y;
		m_FogHeightMaxWorldY = WaterBlocks.DEFAULT_HEIGHT;
		m_FogHeightEndRelativeY = HeightFog.DEFAULT_FOG_HEIGHT_END_RELATIVE_Y;
		m_ThumbnailCameraSaved = false;
		m_MultiSelectMovementIncrement = GameGrid.m_Spacing;
	}

	public static SandboxSettingsProxy Serialize()
	{
		return new SandboxSettingsProxy();
	}

	public static void Deserialize(SandboxSettingsProxy proxy)
	{
		if (proxy == null)
		{
			Init();
			return;
		}
		m_Title = proxy.m_Title;
		m_Description = proxy.m_Description;
		m_HydraulicControllerEnabled = proxy.m_HydraulicControllerEnabled;
		m_Unbreakable = proxy.m_Unbreakable;
		m_UnlimitedHeightFoundations = proxy.m_UnlimitedHeightFoundations;
		m_NoWater = proxy.m_NoWater;
		m_NoReinforcedRoad = proxy.m_NoReinforcedRoad;
		m_SpringAdjustmentsAllowed = proxy.m_SpringAdjustmentsAllowed;
		m_HideDecor = proxy.m_HideDecor;
		m_FogHeightNormalized = proxy.m_FogHeightNormalized;
		m_FogHeightMinWorldY = proxy.m_FogHeightMinWorldY;
		m_FogHeightMaxWorldY = proxy.m_FogHeightMaxWorldY;
		m_FogHeightEndRelativeY = proxy.m_FogHeightEndRelativeY;
		m_ThumbnailCameraSaved = proxy.m_ThumbnailCameraSaved;
		m_ThumbnailCameraPos = proxy.m_ThumbnailCameraPos;
		m_ThumbnailCameraRot = proxy.m_ThumbnailCameraRot;
		m_ThumbnailCameraOrthographicSize = proxy.m_ThumbnailCameraOrthographicSize;
		m_MultiSelectMovementIncrement = proxy.m_MultiSelectMovementIncrement;
		GameUI.m_Instance.m_BottomBar.m_HydraulicController.transform.parent.gameObject.SetActive(m_HydraulicControllerEnabled);
	}

	public static void SaveThumbnailCamera(Camera camera)
	{
		m_ThumbnailCameraSaved = true;
		m_ThumbnailCameraPos = camera.transform.position;
		m_ThumbnailCameraRot = camera.transform.rotation;
		m_ThumbnailCameraOrthographicSize = camera.orthographicSize;
	}
}
