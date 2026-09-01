using UnityEngine;

public class GameSettings : MonoBehaviour
{
	[Header("World Bounds")]
	public int m_WorldWidth;

	public int m_WorldMinY;

	public int m_WorldMaxY;

	[Header("Bridge")]
	public float m_NodeRadius;

	public float m_CollisionRadius;

	public float m_BridgeWidth;

	[Header("Terrain")]
	public float m_TerrainOverhang;

	public float m_MiddleTerrainWidth;

	public Material m_TerrainCollisionMaterial;

	public Material m_TerrainCollisionSolidMaterial;

	[Header("Vehicles")]
	public float m_MaxSecondsLightsOnAfterVictoryFlagReached;

	[Header("Paste")]
	public float m_PasteCancelThresholdX;

	public float m_PasteCancelThresholdY;

	public float m_PasttCancelThresholdSeconds;

	[Header("Camera")]
	public float m_TransitionTimeSeconds;

	public float m_CamDistFromPivot;

	public float m_TutorialOrthographicSize;

	public float m_TutorialHydraulicsOrthographicSize;

	public float m_MinOrthographicSize;

	public float m_MaxOrthographicSize;

	public float m_MaxOrthographicSizeToShowSplitJointNumbers;

	[Header("Controls")]
	[Range(0f, 1f)]
	public float m_DefaultMouseWheelSpeed;

	[Range(0f, 1f)]
	public float m_DefaultCameraRotateSpeed;

	[Range(0f, 1f)]
	public float m_DefaultCameraPanSpeed;

	[Header("Camera Pan")]
	public bool m_PanCameraAutomatically;

	public float m_PanCameraSpeedX;

	public float m_PanCameraSpeedY;

	[Header("Views")]
	public float m_AngleViewYaw;

	public float m_AngleViewPitch;

	public float m_CenterViewYaw;

	public float m_CenterViewPitch;

	public float m_PivotOffsetY;

	[Header("Platform Materials")]
	public Material m_SolidRock;

	public Material m_SolidWood;

	public Material m_SolidDirt;

	public static GameSettings m_Instance;

	private void Awake()
	{
		m_Instance = this;
	}

	public static float WorldWidth()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_WorldWidth;
	}

	public static float WorldMinY()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_WorldMinY;
	}

	public static float WorldMaxY()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_WorldMaxY;
	}

	public static float NodeRadius()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_NodeRadius;
	}

	public static float CollisionRadius()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_CollisionRadius;
	}

	public static float BridgeWidth()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_BridgeWidth;
	}

	public static float TerrainOverhang()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_TerrainOverhang;
	}

	public static float MaxSecondsLightsOnAfterVictoryFlagReached()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_MaxSecondsLightsOnAfterVictoryFlagReached;
	}

	public static float NodeDiameter()
	{
		return 2f * (m_Instance ? m_Instance.m_NodeRadius : 0f);
	}

	public static float PasteCancelThresholdX()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_PasteCancelThresholdX;
	}

	public static float PasteCancelThresholdY()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_PasteCancelThresholdY;
	}

	public static float PasteCancelThresholdSeconds()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_PasttCancelThresholdSeconds;
	}

	public static float TransitionTimeSeconds()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_TransitionTimeSeconds;
	}

	public static float CamDistFromPivot()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_CamDistFromPivot;
	}

	public static float TutorialOrthographicSize()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_TutorialOrthographicSize;
	}

	public static float TutorialHydraulicsOrthographicSize()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_TutorialHydraulicsOrthographicSize;
	}

	public static float MinOrthographicSize()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_MinOrthographicSize;
	}

	public static float MaxOrthographicSize()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_MaxOrthographicSize;
	}

	public static float MaxOrthographicSizeToShowSplitJointNumbers()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_MaxOrthographicSizeToShowSplitJointNumbers;
	}

	public static float DefaultMouseWheelSpeedNormalized()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_DefaultMouseWheelSpeed;
	}

	public static float DefaultCameraRotateSpeedNormalized()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_DefaultCameraRotateSpeed;
	}

	public static float DefaultCameraPanSpeedNormalized()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_DefaultCameraPanSpeed;
	}

	public static float PanCameraSpeedX()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_PanCameraSpeedX;
	}

	public static float PanCameraSpeedY()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_PanCameraSpeedY;
	}

	public static float AngleViewYaw()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_AngleViewYaw;
	}

	public static float AngleViewPitch()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_AngleViewPitch;
	}

	public static float CenterViewYaw()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_CenterViewYaw;
	}

	public static float CenterViewPitch()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_CenterViewPitch;
	}

	public static float PivotOffsetY()
	{
		if (!m_Instance)
		{
			return 0f;
		}
		return m_Instance.m_PivotOffsetY;
	}

	public static Material SolidRock()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_SolidRock;
	}

	public static Material SolidWood()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_SolidWood;
	}

	public static Material SolidDirt()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_SolidDirt;
	}
}
