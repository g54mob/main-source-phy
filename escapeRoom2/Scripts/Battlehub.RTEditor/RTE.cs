using Battlehub.RTCommon;
using UnityEngine;

public class RTE : RTEBase
{
	[SerializeField]
	private Camera m_camera;

	protected override void Awake()
	{
		Debug.Log("RTE Awake");
		base.Awake();
		if (m_camera == null)
		{
			m_camera = Camera.main;
		}
		if (m_camera == null)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.SetParent(base.transform);
			gameObject.name = "RTECamera";
			m_camera = gameObject.AddComponent<Camera>();
		}
		base.WindowRegistered += OnWindowRegistered;
	}

	private void OnWindowRegistered(RuntimeWindow window)
	{
		if (window.WindowType == RuntimeWindowType.Scene && window != null && m_camera != null)
		{
			window.Camera = m_camera;
		}
	}
}
