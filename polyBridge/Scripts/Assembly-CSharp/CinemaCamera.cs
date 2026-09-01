using System.Linq;
using UnityEngine;

public class CinemaCamera : MonoBehaviour
{
	public static float m_DurationSeconds;

	public static bool m_Ease;

	private static Vector3 m_StartPos;

	private static Vector3 m_EndPos;

	private static Quaternion m_StartRot;

	private static Quaternion m_EndRot;

	private static Vector3 m_StartPivot;

	private static Vector3 m_EndPivot;

	private static float m_StartOrthographicSize;

	private static float m_EndOrthographicSize;

	private static bool m_Started;

	private static bool m_HudOnWhenStarted;

	private static Vehicle m_Vehicle;

	private static float m_VehicleDistX;

	private static bool m_StartInitialized;

	private static bool m_EndInitialized;

	public static void Init()
	{
		m_DurationSeconds = 5f;
	}

	public static void UpdateManual()
	{
		if (Activated())
		{
			if (!m_StartInitialized || (!m_EndInitialized && !m_Vehicle))
			{
				Debug.LogWarningFormat("Use cin_start and cin_end to define start/end for camera animation");
				return;
			}
			if (!m_Started)
			{
				StartInterpolate();
				m_Started = true;
				m_HudOnWhenStarted = GameUI.HudIsActive();
				GameUI.EnableHud(on: false);
			}
			if ((bool)m_Vehicle)
			{
				float x = (from t in m_Vehicle.m_MeshRenderer.GetComponentsInChildren<VehicleSyncTarget>()
					where t.m_VehicleSyncPart == VehicleSyncPart.CHASSIS
					select t).ToArray()[0].transform.position.x - m_VehicleDistX;
				Cameras.MainCamera().transform.position = new Vector3(x, Cameras.MainCamera().transform.position.y, Cameras.MainCamera().transform.position.z);
			}
		}
		if (m_Started && Input.GetKeyUp(KeyCode.F5))
		{
			CameraInterpolate.Cancel();
			m_Started = false;
			if (m_HudOnWhenStarted)
			{
				GameUI.EnableHud(on: true);
				m_HudOnWhenStarted = false;
			}
		}
	}

	public static bool Activated()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			return Input.GetKey(KeyCode.F5);
		}
		return false;
	}

	public static void SaveStart(Vehicle vehicle)
	{
		m_StartPos = Cameras.MainCamera().transform.position;
		m_StartRot = Cameras.MainCamera().transform.rotation;
		m_StartPivot = PointsOfView.m_Pivot;
		m_StartOrthographicSize = Cameras.GetOrthographicSize();
		m_Vehicle = vehicle;
		if ((bool)m_Vehicle)
		{
			m_VehicleDistX = vehicle.m_SpawnPos.x - m_StartPos.x;
		}
		m_StartInitialized = true;
	}

	public static void RestoreStart()
	{
		Cameras.MainCamera().transform.position = m_StartPos;
		Cameras.MainCamera().transform.rotation = m_StartRot;
		PointsOfView.UpdatePivotBasedOnCamera();
		Cameras.SetOrthographicSize(m_StartOrthographicSize);
	}

	public static void SaveEnd()
	{
		m_EndPos = Cameras.MainCamera().transform.position;
		m_EndRot = Cameras.MainCamera().transform.rotation;
		PointsOfView.UpdatePivotBasedOnCamera();
		m_EndPivot = PointsOfView.m_Pivot;
		m_EndOrthographicSize = Cameras.GetOrthographicSize();
		m_EndInitialized = true;
	}

	public static void RestoreEnd()
	{
		Cameras.MainCamera().transform.position = m_EndPos;
		Cameras.MainCamera().transform.rotation = m_EndRot;
		PointsOfView.UpdatePivotBasedOnCamera();
		Cameras.SetOrthographicSize(m_StartOrthographicSize);
	}

	private static void StartInterpolate()
	{
		RestoreStart();
		if (!m_Vehicle)
		{
			CameraInterpolate.SlerpTo(m_EndPivot, m_EndPos, m_EndRot, m_EndOrthographicSize, m_DurationSeconds, m_Ease);
		}
	}
}
