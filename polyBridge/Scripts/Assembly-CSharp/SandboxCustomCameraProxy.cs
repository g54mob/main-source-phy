using UnityEngine;

public class SandboxCustomCameraProxy
{
	public Vector3 m_Pos;

	public Quaternion m_Rot;

	public float m_OrthographicSize;

	public SandboxCustomCameraProxy(Camera camera)
	{
		m_Pos = camera.transform.position;
		m_Rot = camera.transform.rotation;
		m_OrthographicSize = camera.orthographicSize;
	}
}
