using Poly.Game;
using UnityEngine;

public class CameraInterpolate
{
	private static Vector3 m_StartPos;

	private static Quaternion m_StartRot;

	private static Vector3 m_EndPos;

	private static Quaternion m_EndRot;

	private static Vector3 m_StartPivot;

	private static Vector3 m_EndPivot;

	private static float m_StartOrthographicSize;

	private static float m_EndOrthographicSize;

	private static bool m_Ease;

	private static float m_ElapsedSeconds;

	private static float m_TransitionSeconds;

	private static bool m_Slerping;

	public static void UpdateManual()
	{
		if (m_Slerping)
		{
			UpdateSlerp();
			GameStateCommonInput.m_RefreshClickPositionForPan = true;
		}
	}

	public static bool IsActive()
	{
		return m_Slerping;
	}

	public static void Cancel()
	{
		m_Slerping = false;
	}

	public static void SlerpTo(Vector3 pivot, Vector3 endPos, Quaternion endRot, float endOrthographicSize, float seconds, bool ease)
	{
		m_ElapsedSeconds = 0f;
		m_StartPivot = PointsOfView.m_Pivot;
		m_EndPivot = pivot;
		m_StartPos = Cameras.MainCamera().transform.position;
		m_StartRot = Cameras.MainCamera().transform.rotation;
		m_EndPos = endPos;
		m_EndRot = endRot;
		m_StartOrthographicSize = Cameras.MainCamera().orthographicSize;
		m_EndOrthographicSize = endOrthographicSize;
		m_TransitionSeconds = seconds;
		m_Ease = ease;
		m_Slerping = true;
	}

	private static void UpdateSlerp()
	{
		m_ElapsedSeconds += Time.unscaledDeltaTime;
		float num = CalculateLerpParameter();
		Vector3 vector = (PointsOfView.m_Pivot = Vector3.Lerp(m_StartPivot, m_EndPivot, num));
		Vector3 a = m_StartRot * Vector3.back;
		Vector3 b = m_EndRot * Vector3.back;
		Vector3 normalized = Vector3.Slerp(a, b, num).normalized;
		Cameras.MainCamera().transform.position = vector + normalized * GameSettings.CamDistFromPivot();
		Cameras.MainCamera().transform.LookAt(vector);
		Cameras.SetOrthographicSize(Mathf.SmoothStep(m_StartOrthographicSize, m_EndOrthographicSize, num));
		Bridge.RefreshZoomDependentVisibility();
		if (Mathf.Approximately(num, 1f))
		{
			Cameras.MainCamera().transform.position = m_EndPos;
			Cameras.MainCamera().transform.rotation = m_EndRot;
			Cameras.SetOrthographicSize(m_EndOrthographicSize);
			Game.RefreshAfterOrthographicSizeChange();
			m_Slerping = false;
		}
		CameraControl.RegisterTransformUpdate();
	}

	private static float CalculateLerpParameter()
	{
		float num = Mathf.Clamp01(m_ElapsedSeconds / m_TransitionSeconds);
		if (!m_Ease)
		{
			return num;
		}
		return Mathf.SmoothStep(0f, 1f, num);
	}
}
