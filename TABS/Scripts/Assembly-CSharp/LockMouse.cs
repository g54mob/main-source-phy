using TFBGames;
using UnityEngine;

public class LockMouse : MonoBehaviour
{
	private CursorVisibilityController m_cursorVisibility;

	private void Start()
	{
		m_cursorVisibility = ServiceLocator.GetService<CursorVisibilityController>();
		m_cursorVisibility.SetLockStateAndVisibility(CursorLockMode.Locked, visible: false);
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Mouse1))
		{
			m_cursorVisibility.SetLockStateAndVisibility(CursorLockMode.Locked, visible: false);
		}
	}
}
